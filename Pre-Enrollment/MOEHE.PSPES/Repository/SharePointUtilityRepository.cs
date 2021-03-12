using Microsoft.SharePoint;
using Microsoft.SharePoint.WebControls;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace MOEHE.PSPES.Repository
{
    class SharePointUtilityRepository
    {
        /// <summary>
        /// we use this method to get all sharepoint groups
        /// </summary>
        /// <returns></returns>
        public static List<SharePointGroup> GetAllSPGroup()
        {
            List<SharePointGroup> AllSPSgoups = new List<SharePointGroup>();
            using (SPSite spSite = new SPSite(SPControl.GetContextSite(HttpContext.Current).Url))
            {
                using (SPWeb spWeb = spSite.OpenWeb())
                {
                    foreach (SPGroup spGroup in spWeb.Groups)
                    {
                        AllSPSgoups.Add(new SharePointGroup { Name = spGroup.Name });
                    }
                }
            }
            return AllSPSgoups;
        }
        /// <summary>
        /// we use this method to remove user from sharepoint group
        /// </summary>
        /// <param name="userLoginName"></param>
        /// <param name="userSharePointGroupName"></param>
        /// <param name="SharePointGroupsForADGroup"></param>
        /// <returns></returns>
        public static bool RemoveUserFromSharePointAGroup(string userLoginName, string userSharePointGroupName, string SharePointGroupsForADGroup)
        {
            bool IsUserExistInADGroup = false;


            //Executes this method with Full Control rights even if the user does not otherwise have Full Control
            SPSecurity.RunWithElevatedPrivileges(delegate
            {
                //Don't use context to create the spSite object since it won't create the object with elevated privileges but with the privileges of the user who execute the this code, which may casues an exception
                using (SPSite spSite = new SPSite(SPControl.GetContextSite(HttpContext.Current).Url))
                {
                    using (SPWeb spWeb = spSite.RootWeb)
                    {

                        //Allow updating of some sharepoint lists, (here spUsers, spGroups etc...)
                        spWeb.AllowUnsafeUpdates = true;


                        SPUser spUser = spWeb.EnsureUser(userLoginName);
                        //check if user is member of active directory group
                        if (!spUser.IsDomainGroup)
                        {
                            //check if user is member of active directory group
                            IsUserExistInADGroup = AllowAccessToSharePoint(userLoginName, SharePointGroupsForADGroup);
                        }
                        if (!IsUserExistInADGroup)
                        {
                            try
                            {


                                if (spUser != null)
                                {
                                    SPGroup spGroup = null;
                                    try
                                    {
                                        spGroup = spWeb.SiteGroups[userSharePointGroupName];
                                    }
                                    catch
                                    {
                                        CreateSharePointGroup(userSharePointGroupName);

                                        spGroup = spWeb.SiteGroups[userSharePointGroupName];
                                    }
                                    if (spGroup != null)
                                    {
                                        spGroup.RemoveUser(spUser);

                                    }

                                }
                            }
                            catch (Exception ex)
                            {
                                string message = ex.Message;
                                //Error handling logic should go here
                            }
                            finally
                            {
                                spWeb.AllowUnsafeUpdates = false;
                            }
                        }

                    }

                }

            });
            return IsUserExistInADGroup;
        }
        /// <summary>
        /// we use this method to give access to AD user inside sharepoint GRoup
        /// </summary>
        /// <param name="LoginName"></param>
        /// <param name="GroupName"></param>
        /// <returns></returns>
        public static bool AllowAccessToSharePoint(string LoginName, string GroupName)
        {
            bool IsUserExistInADGroup = false;


            //Executes this method with Full Control rights even if the user does not otherwise have Full Control
            SPSecurity.RunWithElevatedPrivileges(delegate
            {
                //Don't use context to create the spSite object since it won't create the object with elevated privileges but with the privileges of the user who execute the this code, which may casues an exception
                using (SPSite spSite = new SPSite(SPControl.GetContextSite(HttpContext.Current).Url))
                {
                    using (SPWeb spWeb = spSite.RootWeb)
                    {

                        //check if user is member of active directory group
                        SPGroup spAllowAccessGroup = null;


                        try
                        {
                            spAllowAccessGroup = spWeb.SiteGroups[GroupName];
                        }
                        catch
                        {
                            CreateSharePointGroup(GroupName);


                        }
                        spAllowAccessGroup = spWeb.SiteGroups[GroupName];
                        foreach (SPUser item in spAllowAccessGroup.Users)
                        {
                            if (item.IsDomainGroup && !IsUserExistInADGroup)
                            {
                                IsUserExistInADGroup = ADutility.IsUserMemebrOFActiveDirectoryGroup(LoginName, item.Name);
                            }
                        }
                    }
                }
            });
            return IsUserExistInADGroup;
        }

        /// <summary>
        /// we use this method to add user to sharepoint group and check if this user inside AD Group
        /// </summary>
        /// <param name="userLoginName"></param>
        /// <param name="userSharePointGroupName"></param>
        /// <param name="SharePointGroupsForADGroup"></param>
        /// <returns></returns>
        public static bool AddUserToSharePointAGroup(string userLoginName, string userSharePointGroupName, string SharePointGroupsForADGroup)
        {
            bool IsUserExistInADGroup = false;


            //Executes this method with Full Control rights even if the user does not otherwise have Full Control
            SPSecurity.RunWithElevatedPrivileges(delegate
            {
                //Don't use context to create the spSite object since it won't create the object with elevated privileges but with the privileges of the user who execute the this code, which may casues an exception
                using (SPSite spSite = new SPSite(SPControl.GetContextSite(HttpContext.Current).Url))
                {
                    using (SPWeb spWeb = spSite.RootWeb)
                    {
                        //Allow updating of some sharepoint lists, (here spUsers, spGroups etc...)

                        spWeb.AllowUnsafeUpdates = true;


                        SPUser spUser = spWeb.EnsureUser(userLoginName);
                        //check if user is member of active directory group
                        if (!spUser.IsDomainGroup)
                        {
                            IsUserExistInADGroup = AllowAccessToSharePoint(userLoginName, SharePointGroupsForADGroup);
                        }
                        if (!IsUserExistInADGroup)
                        {
                            try
                            {




                                if (spUser != null)
                                {
                                    SPGroup spGroup = null;
                                    try
                                    {
                                        spGroup = spWeb.SiteGroups[userSharePointGroupName];
                                    }
                                    catch
                                    {
                                        CreateSharePointGroup(userSharePointGroupName);

                                        spGroup = spWeb.SiteGroups[userSharePointGroupName];
                                    }
                                    if (spGroup != null)
                                    {
                                        spGroup.AddUser(spUser);

                                    }

                                }
                            }
                            catch (Exception ex)
                            {
                                string Message = ex.Message;
                                //Error handling logic should go here
                            }
                            finally
                            {
                                spWeb.AllowUnsafeUpdates = false;
                            }
                        }

                    }

                }

            });
            return IsUserExistInADGroup;
        }
        /// <summary>
        /// we use this method to create sharepoint group
        /// </summary>
        /// <param name="SharePointGroupName"></param>
        public static void CreateSharePointGroup(string SharePointGroupName)
        {
            SPWeb root = SPContext.Current.Site.RootWeb;
            SPGroup group = null;

            // Check if the group exists
            try
            {
                group = root.SiteGroups[SharePointGroupName];
            }
            catch { }

            // If it doesn't, add it
            if (group == null)
            {
                root.SiteGroups.Add(SharePointGroupName, SPContext.Current.Web.CurrentUser, root.Author, SharePointGroupName);
                group = root.SiteGroups[SharePointGroupName];
                SPRoleDefinition roleDefinition = null;
                SPRoleAssignment roleAssignment = null;
                // Add the group's permissions
                //if (SharePointGroupName == Constants.SharePointAllowAccessGroup || SharePointGroupName == Constants.SharePointGroupForManualUsers)
                //{
                //    roleDefinition = root.RoleDefinitions.GetByType(SPRoleType.Reader);

                //    //}
                //    //else
                //    //{
                //    //    roleDefinition = root.RoleDefinitions.GetByType(SPRoleType.None);

                //    //}
                //    roleAssignment = new SPRoleAssignment(group);
                //    roleAssignment.RoleDefinitionBindings.Add(roleDefinition);
                //    root.RoleAssignments.Add(roleAssignment);
                //    root.Update();
                //}
            }
        }

            /// <summary>
            /// we use this method to get all users inside sharepoint Group
            /// </summary>
            /// <param name="userGroupName"></param>
            /// <returns></returns>
            public static List<string> GetAllGroupsInSharePoint(string userGroupName)
            {
                List<string> Groups = new List<string>();
                SPSecurity.RunWithElevatedPrivileges(delegate
                {
                    //Don't use context to create the spSite object since it won't create the object with elevated privileges but with the privileges of the user who execute the this code, which may casues an exception
                    using (SPSite spSite = new SPSite(SPControl.GetContextSite(HttpContext.Current).Url))
                    {
                        using (SPWeb spWeb = spSite.OpenWeb())
                        {
                            try
                            {
                                //Allow updating of some sharepoint lists, (here spUsers, spGroups etc...)
                                spWeb.AllowUnsafeUpdates = true;




                                SPGroup spGroup = null;
                                try
                                {
                                    spGroup = spWeb.Groups[userGroupName];
                                    foreach (SPUser item in spGroup.Users)
                                    {
                                        string LoginName = item.LoginName;

                                        Groups.Add(LoginName);
                                    }
                                }
                                catch
                                {
                                    CreateSharePointGroup(userGroupName);


                                }



                            }
                            catch (Exception ex)
                            {
                                string mesage = ex.Message;
                                //Error handling logic should go here
                            }
                            finally
                            {
                                spWeb.AllowUnsafeUpdates = false;
                            }
                        }
                    }

                });
                return Groups;
            }

            /// <summary>
            /// we use this method yo get all AD groups inside sharepoint group
            /// </summary>
            /// <param name="GroupName"></param>
            /// <returns></returns>
            public static List<SharePointGroupMembers> GetAllGroupUsers(string GroupName)
            {
                List<SharePointGroupMembers> groupMembers = new List<SharePointGroupMembers>();
                using (SPSite spSite = new SPSite(SPControl.GetContextSite(HttpContext.Current).Url))
                {
                    using (SPWeb spWeb = spSite.OpenWeb())
                    {
                        SPGroup spGroup = spWeb.Groups.GetByName(GroupName);
                        foreach (SPUser spUser in spGroup.Users)
                        {
                            if (spUser.IsDomainGroup)
                            {
                                groupMembers.Add(new SharePointGroupMembers { MemberName = spUser.Name, IsAtiveDirectoryGroup = spUser.IsDomainGroup });

                            }
                        }
                    }
                }
                return groupMembers;
            }


            public static bool IsUserInGroup(string groupName, string sUserLoginName)
            {
                bool isMember = false;
                SPSecurity.RunWithElevatedPrivileges(delegate ()
                {
                    using (SPSite site = new SPSite(SPContext.Current.Site.Url))
                    {

                    using (SPWeb web = site.OpenWeb())
                    {

                            SPUser userToCheck = web.AllUsers[sUserLoginName];
                           
                            var spGroup = web.Groups[groupName];
                            isMember=  userToCheck.Groups.Cast<SPGroup>().Any(g => g.Name.ToLower() == spGroup.Name.ToLower());
                        //isMember = web.IsCurrentUserMemberOfGroup(spGroup.ID);
                    }
                }
            });
            return isMember;

            }
        }

    }


    /// <summary>
    /// we use class to get AD users and check if it is group or no
    /// </summary>
    public class SharePointGroupMembers
    {
        public string MemberName { get; set; }
        public bool IsAtiveDirectoryGroup { get; set; }

    }

    public class SharePointGroup
    {
        public string Name { get; set; }

    }

