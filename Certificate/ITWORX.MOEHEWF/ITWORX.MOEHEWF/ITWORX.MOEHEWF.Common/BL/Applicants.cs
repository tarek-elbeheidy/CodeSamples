using ITWORX.MOEHE.Utilities;
using ITWORX.MOEHE.Utilities.Logging;
using ITWORX.MOEHEWF.Common.Utilities;
using Microsoft.SharePoint;
using Microsoft.SharePoint.Administration.Claims;
using System;
using System.DirectoryServices;
using System.Globalization;
using System.Threading;

namespace ITWORX.MOEHEWF.Common.BL
{
    public class Applicants
    {
        public static SPListItem GetApplicantItemByID(int id)
        {
            Logging.GetInstance().Debug("Entering method Applicants.GetApplicantItemByID");
            SPListItem applicantItem = null;
            try
            {
                SPSecurity.RunWithElevatedPrivileges(delegate ()
                {
                    using (SPSite site = new SPSite(SPContext.Current.Site.Url))
                    {
                        using (SPWeb web = site.OpenWeb())
                        {
                            SPList applicantsList = web.Lists[Common.Utilities.Constants.Applicants];
                            if (applicantsList == null)
                                throw new Exception();
                            applicantItem = applicantsList.GetItemById(id);
                        }
                    }
                });
            }
            catch (Exception ex)
            {
                Logging.GetInstance().LogException(ex);
            }
            finally
            {
                Logging.GetInstance().Debug("Exiting method Applicants.GetApplicantItemByID");
            }
            return applicantItem;
        }

        public static bool IsFemale(string personID)
        {
            Logging.GetInstance().Debug("Entering method Applicants.IsMale");
            bool isFemale = false;
            try
            {
                SPSecurity.RunWithElevatedPrivileges(delegate ()
                {
                    using (SPSite site = new SPSite(SPContext.Current.Site.Url))
                    {
                        using (SPWeb web = site.OpenWeb())
                        {
                            SPList applicantsList = web.Lists[Common.Utilities.Constants.Applicants];
                            if (applicantsList != null)
                            {
                                SPQuery spQuery = Common.Utilities.BusinessHelper.GetQueryObject("<Where><Eq><FieldRef Name='PersonalID' /><Value Type='Text'>" +
                                                        personID + "</Value></Eq></Where>"); 
                                SPListItemCollection applicantItems = applicantsList.GetItems(spQuery);
                                if (applicantItems != null && applicantItems.Count > 0)
                                {
                                    var applicantItem = applicantItems[0];
                                    SPFieldBoolean boolField = applicantItem.Fields["ApplicantGender"] as SPFieldBoolean;
                                    isFemale = (bool)boolField.GetFieldValue(applicantItem["ApplicantGender"].ToString());
                                }

                            }
                        }
                    }
                }); 
            }
            catch (Exception ex)
            {
                Logging.GetInstance().LogException(ex);
            }
            finally
            {
                Logging.GetInstance().Debug("Exiting method Applicants.IsMale");
            }
            return isFemale;
        }

        public static bool ViewNationalService(string personID)
        {
            Logging.GetInstance().Debug("Entering method Applicants.ViewNationalService");
            bool isMale = false;
            bool isQatari = false;
            try
            {
                SPSecurity.RunWithElevatedPrivileges(delegate ()
                {
                    using (SPSite site = new SPSite(SPContext.Current.Site.Url))
                    {
                        using (SPWeb web = site.OpenWeb())
                        {
                            SPList applicantsList = web.Lists[Common.Utilities.Constants.Applicants];
                            if (applicantsList != null)
                            {
                                SPQuery spQuery = Common.Utilities.BusinessHelper.GetQueryObject("<Where><Eq><FieldRef Name='PersonalID' /><Value Type='Text'>" +
                                                        personID + "</Value></Eq></Where>");
                                SPListItemCollection applicantItems = applicantsList.GetItems(spQuery);
                                if (applicantItems != null && applicantItems.Count > 0)
                                {
                                    var applicantItem = applicantItems[0];
                                    SPFieldBoolean boolField = applicantItem.Fields["ApplicantGender"] as SPFieldBoolean;
                                    isMale = !(bool)boolField.GetFieldValue(applicantItem["ApplicantGender"].ToString());

                                    SPFieldLookupValue SingleValue = new SPFieldLookupValue(applicantItem["Nationality"].ToString());
                                    string Nationality = SingleValue.LookupValue;


                                    isQatari = Nationality == "Qatar";
                                }

                            }
                        }
                    }
                });
            }
            catch (Exception ex)
            {
                Logging.GetInstance().LogException(ex);
            }
            finally
            {
                Logging.GetInstance().Debug("Exiting method Applicants.ViewNationalService");
            }
            return isMale && isQatari;
        }

        public static int GetApplicantItemByPersonalID(string personalID)
        {
            Logging.GetInstance().Debug("Entering method Applicants.GetApplicantItemByID");
            int applicantId = 0;
            try
            {
                SPSecurity.RunWithElevatedPrivileges(delegate ()
                {
                    using (SPSite site = new SPSite(SPContext.Current.Site.Url))
                    {
                        using (SPWeb web = site.OpenWeb())
                        {
                            SPList applicantsList = web.Lists[Common.Utilities.Constants.Applicants];
                            if (applicantsList == null)
                                throw new Exception();
                            SPQuery spQuery = Common.Utilities.BusinessHelper.GetQueryObject("<Where><Eq><FieldRef Name='PersonalID' /><Value Type='Text'>" +
                                personalID + "</Value></Eq></Where>");

                            SPListItemCollection applicantItems = applicantsList.GetItems(spQuery);
                            if (applicantItems != null && applicantItems.Count > 0)
                            {
                                applicantId = applicantItems[0].ID;
                            }
                        }
                    }
                });
            }
            catch (Exception ex)
            {
                Logging.GetInstance().LogException(ex);
            }
            finally
            {
                Logging.GetInstance().Debug("Exiting method Applicants.GetApplicantItemByID");
            }
            return applicantId;
        }

        public static int AddApplicant(Entities.Applicants applicant, int applicantId)
        {
            Logging.GetInstance().Debug("Entering method Applicants.AddApplicant");
            SPWeb web = null;
            SPListItem applicantItem = null;
            try
            {
                SPSecurity.RunWithElevatedPrivileges(delegate ()
                {
                    using (SPSite site = new SPSite(SPContext.Current.Site.Url))
                    {
                        using (web = site.OpenWeb())
                        {
                            SPList applicantsList = web.Lists[Common.Utilities.Constants.Applicants];
                            if (applicantsList == null)
                                throw new Exception();
                            web.AllowUnsafeUpdates = true;
                            if (applicantId != 0)
                                applicantItem = GetApplicantItemByID(applicantId);

                            else
                            {
                                applicantItem = applicantsList.AddItem();
                                applicantItem["PersonalID"] = applicant.PersonalID;
                                if (applicant.BirthDate == DateTime.MinValue)
                                {
                                    applicantItem["BirthDate"] = null;
                                }
                                else
                                {
                                    applicantItem["BirthDate"] = applicant.BirthDate;
                                }
                                applicantItem["ApplicantName"] = applicant.ApplicantName;
                                if (applicant.Nationality != null)
                                    applicantItem["Nationality"] = new SPFieldLookupValue(int.Parse(applicant.Nationality.SelectedID), applicant.Nationality.SelectedTitle);
                                if (applicant.NationalityCategory != null)
                                    applicantItem["NationalityCategory"] = new SPFieldLookupValue(int.Parse(applicant.NationalityCategory.SelectedID), applicant.NationalityCategory.SelectedTitle);
                                applicantItem["MobileNumber"] = applicant.MobileNumber;
                                applicantItem["ApplicantEmail"] = applicant.ApplicantEmail;
                                applicantItem["ApplicantGender"] = applicant.ApplicantGender;
                                applicantItem["ArabicName"] = applicant.ArabicName;
                                applicantItem["EnglishName"] = applicant.EnglishName;

                            }

                            applicantItem["Region"] = applicant.Region;
                            applicantItem["Street"] = applicant.Street;
                            applicantItem["BuildingNo"] = applicant.BuildingNo;
                            applicantItem["ApartmentNo"] = applicant.ApartmentNo;
                            applicantItem["PostalNumber"] = applicant.PostalNumber;
                            applicantItem["DetailedAddress"] = applicant.DetailedAddress;

                            applicantItem.Update();
                        }
                    }
                });
            }
            catch (Exception ex)
            {
                Logging.GetInstance().LogException(ex);
            }
            finally
            {
                web.AllowUnsafeUpdates = false;
                Logging.GetInstance().Debug("Exiting method Applicants.AddApplicant");
            }
            return applicantItem.ID;
        }

        public static Entities.Applicants GetApplicantByID(int id, int LCID)
        {
            Logging.GetInstance().Debug("Entering method Applicants.GetApplicantByID");
            Entities.Applicants applicant = null;
            try
            {
                SPListItem applicantItem = GetApplicantItemByID(id);
                if (applicantItem != null)
                {
                    applicant = new Entities.Applicants();

                    applicant.ID = applicantItem.ID;
                    applicant.PersonalID = Convert.ToString(applicantItem["PersonalID"]);
                    applicant.ApplicantName = Convert.ToString(applicantItem["ApplicantName"]);
                    applicant.ApplicantEmail = Convert.ToString(applicantItem["ApplicantEmail"]);
                    applicant.EnglishName = Convert.ToString(applicantItem["EnglishName"]);
                    applicant.ArabicName = Convert.ToString(applicantItem["ArabicName"]);
                    if (applicantItem["BirthDate"] !=  null)
                        applicant.BirthDate = Convert.ToDateTime(applicantItem["BirthDate"].ToString());//, "dd/MM/yyyy", Thread.CurrentThread.CurrentCulture); //Convert.ToDateTime(applicantItem["BirthDate"]) != DateTime.MinValue ? ExtensionMethods.QatarFormatedDate(Convert.ToDateTime(applicantItem["BirthDate"])) : string.Empty;

                    if (applicantItem["Nationality"] != null)
                        applicant.Nationality = new Entities.Nationality() { SelectedID = new SPFieldLookupValue(applicantItem["Nationality"].ToString()).LookupId.ToString(), SelectedTitle = LCID == 1033 ? new SPFieldLookupValue(applicantItem["Nationality"].ToString()).LookupValue : new SPFieldLookupValue(applicantItem["NationalityAr"].ToString()).LookupValue };

                    if (applicantItem["NationalityCategory"] != null)
                        applicant.NationalityCategory = new Entities.NationalityCategory() { SelectedID = new SPFieldLookupValue(applicantItem["NationalityCategory"].ToString()).LookupId.ToString(), SelectedTitle = LCID == 1033 ? new SPFieldLookupValue(applicantItem["NationalityCategory"].ToString()).LookupValue : new SPFieldLookupValue(applicantItem["NationalityCategoryAr"].ToString()).LookupValue };

                    applicant.Region = Convert.ToInt32(applicantItem["Region"]);
                    applicant.Street = Convert.ToInt32(applicantItem["Street"]);
                    applicant.BuildingNo = Convert.ToInt32(applicantItem["BuildingNo"]);
                    applicant.PostalNumber = Convert.ToInt32(applicantItem["PostalNumber"]);
                    applicant.ApartmentNo = Convert.ToInt32(applicantItem["ApartmentNo"]);
                    applicant.DetailedAddress = Convert.ToString(applicantItem["DetailedAddress"]);
                    applicant.MobileNumber = Convert.ToString(applicantItem["MobileNumber"]);

                }
            }
            catch (Exception ex)
            {
                Logging.GetInstance().LogException(ex);
            }
            finally
            {
                Logging.GetInstance().Debug("Exiting method Applicants.GetApplicantByID");
            }
            return applicant;
        }

        public static Entities.Applicants GetApplicantProfilefromADByLoginName(string LoginName)
        {
            Logging.GetInstance().Debug("Entering method Applicants.GetApplicantProfilefromADByLoginName");
            Entities.Applicants applicant = null;
            try
            {
                #region Get User from Active Directory for External Users

                string claimUsername = string.Empty;
                string userWithoutDomain = string.Empty;
                SPClaimProviderManager mgr = SPClaimProviderManager.Local;
                if (mgr != null)
                {
                    string username = mgr.DecodeClaim(LoginName).Value;
                    if (username.IndexOf('\\') > -1)
                    {
                        userWithoutDomain = username.Substring(username.IndexOf('\\') + 1);
                    }
                    else if (username.IndexOf('@') > -1)
                    {
                        userWithoutDomain = username.Substring(0, username.IndexOf('@'));
                    }
                    string siteUrl = SPContext.Current.Site.Url;
                    string userNameADCredential = HelperMethods.GetWebAppConfigValue(siteUrl, "UserName");
                    string passwordADCredential = HelperMethods.GetWebAppConfigValue(siteUrl, "Password");
                    string domainController = HelperMethods.GetWebAppConfigValue(siteUrl, "DomainController");
                    if (string.IsNullOrEmpty(domainController))
                    {
                        domainController = "";
                    }
                    else
                    {
                        domainController += "/";
                    }
                    string LDAPPath = "LDAP://" + domainController + HelperMethods.GetWebAppConfigValue(siteUrl, "LDAPPath");// DC =MOEHE,DC=EDU,DC=QA";

                    DirectoryEntry adEntry = new DirectoryEntry(LDAPPath);
                    adEntry.Username = userNameADCredential;
                    adEntry.Password = passwordADCredential;
                    DirectorySearcher adSearcher = new DirectorySearcher(adEntry);
                    adSearcher.Filter = ("SAMAccountName=" + userWithoutDomain);
                    SearchResult searchResultADUser = adSearcher.FindOne();
                    if (searchResultADUser != null)
                    {
                        Logging.GetInstance().Debug("searchResultADUser Not NULL");
                        applicant = new Entities.Applicants();
                        if (searchResultADUser.Properties["extensionAttribute1"] != null && searchResultADUser.Properties["extensionAttribute1"].Count > 0)
                        {
                            applicant.PersonalID = searchResultADUser.Properties["extensionAttribute1"][0].ToString();
                        }

                        if (searchResultADUser.Properties["extensionAttribute2"] != null && searchResultADUser.Properties["extensionAttribute2"].Count > 0)
                        {
                            var BirthDate = searchResultADUser.Properties["extensionAttribute2"][0].ToString();
                            applicant.BirthDate = Convert.ToDateTime(DateTime.ParseExact(BirthDate, "dd/MM/yyyy", CultureInfo.InvariantCulture).ToString("MM/dd/yyyy", CultureInfo.InvariantCulture), CultureInfo.InvariantCulture);
                        }

                        if (searchResultADUser.Properties["displayname"] != null && searchResultADUser.Properties["displayname"].Count > 0)
                        {
                            applicant.ApplicantName = searchResultADUser.Properties["displayname"][0].ToString();
                        }
                        if (searchResultADUser.Properties["extensionAttribute3"] != null && searchResultADUser.Properties["extensionAttribute3"].Count > 0)
                        {
                            var nationality = searchResultADUser.Properties["extensionAttribute3"][0].ToString();
                            int nationalityID = Common.BL.Nationality.GetNationalityID(nationality);
                            applicant.Nationality = new Entities.Nationality();
                            applicant.Nationality.SelectedID = (nationalityID != -1) ? nationalityID.ToString() : "";
                        }

                        if (searchResultADUser.Properties["extensionAttribute4"] != null && searchResultADUser.Properties["extensionAttribute4"].Count > 0)
                        {
                            var nationalityCategory = searchResultADUser.Properties["extensionAttribute4"][0].ToString();
                            int nationalityCategoryID = Common.BL.NationalityCategory.GetNationalityCategoryID(nationalityCategory);
                            applicant.NationalityCategory = new Entities.NationalityCategory();
                            applicant.NationalityCategory.SelectedID = (nationalityCategoryID != -1) ? nationalityCategoryID.ToString() : "";
                        }

                        if (searchResultADUser.Properties["mail"] != null && searchResultADUser.Properties["mail"].Count > 0)
                        {
                            applicant.ApplicantEmail = searchResultADUser.Properties["mail"][0].ToString();
                        }

                        if (searchResultADUser.Properties["mobile"] != null && searchResultADUser.Properties["Mobile"].Count > 0)
                        {
                            applicant.MobileNumber = searchResultADUser.Properties["Mobile"][0].ToString();
                        }
                        if (searchResultADUser.Properties["extensionAttribute5"] != null && searchResultADUser.Properties["extensionAttribute5"].Count > 0)
                        {
                            string gender = searchResultADUser.Properties["extensionAttribute5"][0].ToString();
                            applicant.ApplicantGender = !gender.Equals("M", StringComparison.OrdinalIgnoreCase);
                            // Gender genderValue = (Gender)Enum.Parse(typeof(Gender), gender);
                            // applicant.ApplicantGender = Convert.ToBoolean(genderValue);
                        }
                    }
                }

                #endregion Get User from Active Directory for External Users
            }
            catch (Exception ex)
            {
                Logging.GetInstance().LogException(ex);
            }
            finally
            {
                Logging.GetInstance().Debug("Exiting method Applicants.GetApplicantProfilefromADByLoginName");
            }
            return applicant;
        }

        public static long GetApplicantQatarIDfromADByLoginName(string LoginName)
        {
            Logging.GetInstance().Debug("Entering method Applicants.GetApplicantQatarIDfromADByLoginName");
            long qatarID = 0;
            try
            {
                #region Get User from Active Directory for External Users

                string claimUsername = string.Empty;
                string userWithoutDomain = string.Empty;
                SPClaimProviderManager mgr = SPClaimProviderManager.Local;
                if (mgr != null)
                {
                    string username = mgr.DecodeClaim(LoginName).Value;
                    if (username.IndexOf('\\') > -1)
                    {
                        userWithoutDomain = username.Substring(username.IndexOf('\\') + 1);
                    }
                    else if (username.IndexOf('@') > -1)
                    {
                        userWithoutDomain = username.Substring(0, username.IndexOf('@'));
                    }
                    string siteUrl = SPContext.Current.Site.Url;
                    string userNameADCredential = HelperMethods.GetWebAppConfigValue(siteUrl, "UserName");
                    string passwordADCredential = HelperMethods.GetWebAppConfigValue(siteUrl, "Password");
                    string domainController = HelperMethods.GetWebAppConfigValue(siteUrl, "DomainController");
                    if (string.IsNullOrEmpty(domainController))
                    {
                        domainController = "";
                    }
                    else
                    {
                        domainController += "/";
                    }
                    string LDAPPath = "LDAP://" + domainController + HelperMethods.GetWebAppConfigValue(siteUrl, "LDAPPath");// DC =MOEHE,DC=EDU,DC=QA";

                    DirectoryEntry adEntry = new DirectoryEntry(LDAPPath);
                    adEntry.Username = userNameADCredential;
                    adEntry.Password = passwordADCredential;
                    DirectorySearcher adSearcher = new DirectorySearcher(adEntry);
                    adSearcher.Filter = ("SAMAccountName=" + userWithoutDomain);
                    SearchResult searchResultADUser = adSearcher.FindOne();
                    if (searchResultADUser != null)
                    {
                        if (searchResultADUser.Properties["extensionAttribute1"] != null && searchResultADUser.Properties["extensionAttribute1"].Count > 0)
                        {
                            qatarID = long.Parse(searchResultADUser.Properties["extensionAttribute1"][0].ToString());
                            Logging.GetInstance().Debug("Qatar ID:" + qatarID.ToString());
                        }
                    }
                }

                #endregion Get User from Active Directory for External Users
            }
            catch (Exception ex)
            {
                Logging.GetInstance().LogException(ex);
            }
            finally
            {
                Logging.GetInstance().Debug("Exiting method Applicants.GetApplicantQatarIDfromADByLoginName");
            }
            return qatarID;
        }

        public static bool inApplicantGroup(string GroupName)
        {
            Logging.GetInstance().Debug("Entering Applicants.inApplicantGroup");
            bool returnValue = false;
            string currentSiteUrl = SPContext.Current.Site.Url;
            string currentLoginName = SPContext.Current.Web.CurrentUser.LoginName;
            try
            {
                SPGroup webGroup = null;

                SPSecurity.RunWithElevatedPrivileges(delegate
                {
                    using (SPWeb web = new SPSite(currentSiteUrl).OpenWeb())
                    {
                        webGroup = web.Groups[GroupName];

                        if (webGroup != null)
                        {

                            SPUserCollection _users = webGroup.Users;
                            foreach (SPUser user in _users)
                                if (user.LoginName.Equals(currentLoginName))
                                {
                                    returnValue = true;
                                    break;
                                }

                        }
                    }
                });

                if (webGroup != null && SPContext.Current.Web.IsCurrentUserMemberOfGroup(webGroup.ID))
                    return true;

                if (!returnValue)
                {
                    SPGroupCollection _SPGroupCollection = SPContext.Current.Web.CurrentUser.Groups;
                    foreach (SPGroup group in _SPGroupCollection)
                        if (GroupName.Equals(group.Name))
                            return true;
                }
            }
            catch (Exception ex)
            {
                Logging.GetInstance().LogException(ex);
            }
            finally
            {
                Logging.GetInstance().Debug("Exit Applicants.inApplicantGroup");
            }
            return returnValue;
        }

    }
}