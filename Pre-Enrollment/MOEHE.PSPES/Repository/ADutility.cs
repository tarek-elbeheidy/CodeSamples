using Microsoft.SharePoint;
using Microsoft.SharePoint.Administration.Claims;
using MOEHE.PSPES.Models;
using System;
using System.Collections.Generic;
using System.DirectoryServices;
using System.DirectoryServices.AccountManagement;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Hosting;

namespace MOEHE.PSPES.Repository
{
    /// <summary>
    /// ADUtility is class used for active directory transactions
    /// </summary>
    public class ADutility
    {
        /// <summary>
        /// we use this method to get user details using QID we get department number , display name and LoginName
        /// </summary>
        /// <param name="QID"></param>
        /// <returns></returns>
        public static UsersModel GetUserModel(string QID)
        {
            UsersModel usersModel = new UsersModel();
            using (HostingEnvironment.Impersonate())
            {

                string departmentNumber = "";
                string DisplayName = "";
                string LoginName = "";

                //DirectoryEntry entry = new DirectoryEntry("LDAP://sec.gov.qa");
                try
                {
                    DirectoryEntry entry = new DirectoryEntry(PSPESConstants.QsecDomain);

                    DirectorySearcher Dsearch = new DirectorySearcher(entry);




                    Dsearch.Filter = "(&((&(objectCategory=Person)(objectClass=User)))(EmployeeID=" + QID + "))"; ;
                    SearchResult rs = Dsearch.FindOne();
                    if (rs != null)
                    {
                        DisplayName = rs.GetDirectoryEntry().Properties["DisplayName"].Value.ToString();

                        // department=  "Username : " + rs.GetDirectoryEntry().Properties["samaccountname"].Value.ToString();
                        departmentNumber = rs.GetDirectoryEntry().Properties["DepartmentNumber"].Value.ToString();

                        LoginName = rs.GetDirectoryEntry().Properties["SamAccountName"].Value.ToString();
                        LoginName = string.Format(@"qsec\{0}", LoginName);
                        usersModel = new UsersModel { LoginName = LoginName, Name = DisplayName, SenderCode = departmentNumber };


                    }
                    else
                    {
                        entry = new DirectoryEntry(PSPESConstants.EdusecDomain);

                        Dsearch = new DirectorySearcher(entry);




                        Dsearch.Filter = "(&((&(objectCategory=Person)(objectClass=User)))(EmployeeID=" + QID + "))"; ;
                        rs = Dsearch.FindOne();
                        if (rs != null)
                        {
                            DisplayName = rs.GetDirectoryEntry().Properties["DisplayName"].Value.ToString();

                            // department=  "Username : " + rs.GetDirectoryEntry().Properties["samaccountname"].Value.ToString();
                            departmentNumber = rs.GetDirectoryEntry().Properties["DepartmentNumber"].Value.ToString();

                            LoginName = rs.GetDirectoryEntry().Properties["SamAccountName"].Value.ToString();
                            LoginName = string.Format(@"secedu\{0}", LoginName);

                            usersModel = new UsersModel { LoginName = LoginName, Name = DisplayName, SenderCode = departmentNumber };

                        }
                    }

                }
                catch { }

            }


            return usersModel;
        }

        /// <summary>
        /// we use this method to get DepartmentNumber and QID(UserID) from active direcoty using samaccountname and Domain name(QSEC or SECEDU)
        /// </summary>
        /// <param name="DomainName"></param>
        /// <param name="UserName"></param>
        /// <returns></returns>
        public static UserHelper GetProperty(string DomainName, string UserName)
        {
            using (HostingEnvironment.Impersonate())
            {

                string department = string.Empty;
                string userID = string.Empty;

                //DirectoryEntry entry = new DirectoryEntry("LDAP://sec.gov.qa");
                DirectoryEntry entry = new DirectoryEntry(DomainName);

                DirectorySearcher Dsearch = new DirectorySearcher(entry);


                String Name = UserName;

                Dsearch.Filter = "(&((&(objectCategory=Person)(objectClass=User)))(samaccountname=" + Name + "))"; ;
                SearchResult rs = Dsearch.FindOne();
                userID = rs.GetDirectoryEntry().Properties["EmployeeID"].Value.ToString();

                // department=  "Username : " + rs.GetDirectoryEntry().Properties["samaccountname"].Value.ToString();
                department = rs.GetDirectoryEntry().Properties["DepartmentNumber"].Value.ToString();


                UserHelper userHelper = new UserHelper { DepartmentID = department, UserID = userID };


                return userHelper;
            }
        }
        /// <summary>
        /// we use this method to get DepartmentNumber And QID(UserID) of current loggedin user
        /// </summary>
        /// <returns></returns>
        //Note : We will use this method only to get department nuumber (school code) => GetUserDetails
        //You have to send SPUser using below line and send this to the method to get your details
        //SPUser User = SPContext.Current.Site.RootWeb.EnsureUser("qsec\\t-t.elbeheidy");

        public static UserHelper GetUserDetails(SPUser sPUser )
        {
            UserData userData = GetUserData(sPUser);
            string LoginName = userData.LoginName;
            string userWithoutDomain = LoginName.Substring(LoginName.IndexOf('\\') + 1);

            UserHelper userHelper = new UserHelper();
            try
            {
                if (LoginName.Contains("qsec"))
                {
                    userHelper = ADutility.GetProperty(PSPESConstants.QsecDomain, userWithoutDomain);

                }
                else
                {
                    userHelper = ADutility.GetProperty(PSPESConstants.EdusecDomain, userWithoutDomain);


                }
            }
            catch
            {
                userHelper.DepartmentID = "-1"; // for my account
                userHelper.UserID = "-1"; // for my account

            }
            return userHelper;
        }
        /// <summary>
        /// we use this method to get all the Active directory groups that user is member of it using user name and Domain name (QSEC or SECEDU)
        /// </summary>
        /// <param name="userName"></param>
        /// <param name="DomainName"></param>
        /// <returns></returns>
        private static string[] GetGroupNames(string userName, string DomainName)
        {
            List<string> result = new List<string>();

            using (PrincipalContext pc = new PrincipalContext(ContextType.Domain, DomainName))
            {
                using (PrincipalSearchResult<Principal> src = UserPrincipal.FindByIdentity(pc, userName).GetGroups(pc))
                {
                    src.ToList().ForEach(sr => result.Add(sr.SamAccountName));
                }
            }

            return result.ToArray();
        }
        /// <summary>
        /// we use this meothod to check if specific user is member of active directory Group or no 
        /// </summary>
        /// <param name="LoginName"></param>
        /// <param name="GroupName"></param>
        /// <returns></returns>
        public static bool IsUserMemebrOFActiveDirectoryGroup(string LoginName, string GroupName)
        {
            using (HostingEnvironment.Impersonate())
            {

                string userWithoutDomain = LoginName.Substring(LoginName.IndexOf('\\') + 1);
                string GroupNameWithoutDomainName = GroupName.Substring(GroupName.IndexOf('\\') + 1);


                string DomainName = "";
                bool isUserMember = false;

                try
                {
                    if (LoginName.Contains("qsec"))
                    {
                        DomainName = PSPESConstants.QsecDomain;
                        string[] GroupNames = GetGroupNames(userWithoutDomain, "sec.gov.qa");
                        foreach (var item in GroupNames)
                        {
                            if (item.ToLower() == GroupNameWithoutDomainName.ToLower())
                            {
                                isUserMember = true;
                            }
                        }
                    }
                    else
                    {
                        DomainName = PSPESConstants.EdusecDomain;
                        string[] GroupNames = GetGroupNames(userWithoutDomain, "secedu.qa");
                        foreach (var item in GroupNames)
                        {
                            if (item.ToLower() == GroupNameWithoutDomainName.ToLower())
                            {
                                isUserMember = true;
                            }
                        }
                    }
                    //DirectoryEntry entry = new DirectoryEntry(DomainName);


                    //DirectorySearcher mySearcher = new DirectorySearcher(entry);
                    //mySearcher.Filter = "(&((&(objectCategory=Person)(objectClass=User)))(samaccountname=" + userWithoutDomain + "))";
                    //SearchResult result = mySearcher.FindOne();

                    //foreach (string GroupPath in result.Properties["memberOf"])
                    //{
                    //    if (GroupPath.Contains(GroupNameWithoutDomainName))
                    //    {
                    //        isUserMember = true;
                    //    }
                    //}
                }
                catch
                {
                }



                return isUserMember;
            }
        }



        /// <summary>
        /// we user this meothd to get the login name and display name of current loged in user 
        /// </summary>
        /// <returns></returns>
        public static UserData GetUserData(SPUser sPUser)
        {
            UserData userData = new UserData();
            SPContext currentContext;
            try
            {
                //Getting the current context                
                currentContext = SPContext.Current;
            }
            catch (InvalidOperationException)
            {
                currentContext = null;
            }
            if (currentContext != null && currentContext.Web.CurrentUser != null)
            {
                userData.DisplayName = sPUser.Name;

            }

            //string Username;
            SPClaimProviderManager mgr = SPClaimProviderManager.Local;
            if (mgr != null && SPClaimProviderManager.IsEncodedClaim(sPUser.LoginName))
            {
                userData.LoginName = mgr.DecodeClaim(sPUser.LoginName).Value;
            }
            else
            {
                userData.LoginName = sPUser.LoginName;
            }

            return userData;
        }


    }
    /// <summary>
    /// we use this class as entity contains QID (userID) and DepartmentCode(DepartmentNumber in AD)
    /// </summary>
    public class UserHelper
    {
        public string UserID { get; set; }
        public string DepartmentID { get; set; }

    }

    public class UserData
    {
        public string LoginName { set; get; }
        public string DisplayName { set; get; }

    }
    public class UsersModel
    {
        public long QID { get; set; }
        public long LoggedInUserID { get; set; }

        public string Name { get; set; }
        public string LoginName { get; set; }

        public string Password { get; set; }
        public string SenderCode { get; set; }
        public Nullable<int> UserRoleID { get; set; }
        public bool? IsSendSMSAllowed { get; set; }
        public bool? IsAcessAllowed { get; set; }
        public bool? IsInternationalSMSAllowed { get; set; }
        public bool? IsManageUsersAllowed { get; set; }
        public bool? IsSuperAdmin { get; set; }

        public bool? IsMemberOfADGroup { get; set; }

        public int TotalCount { get; set; }
        public int StartRecored { get; set; }
        public int EndRecored { get; set; }
        public System.DateTime CreateDateTime { get; set; }
    }
}
