using System;
using System.ComponentModel;
using System.DirectoryServices.AccountManagement;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using ITWORX.MOEHE.Utilities.Logging;
using ITWORX.MOEHEWF.PA.Utilities;

using Microsoft.SharePoint;
using Microsoft.SharePoint.Utilities;
using Microsoft.SharePoint.WebControls;

namespace ITWORX.MOEHEWF.PA.WebParts.Sample
{
    [ToolboxItemAttribute(false)]
    public class Sample : WebPartBase
    {
        // Visual Studio might automatically update this path when you change the Visual Web Part project item.
        private const string _ascxPath = @"~/_CONTROLTEMPLATES/15/ITWORX.MOEHEWF.PA.WebParts/Sample/SampleUserControl.ascx";

        protected override void CreateChildControls()
        {
            string currgroupName = "Applicants";

            bool IsInGroup = InGroup(currgroupName);

            try
            {
                Logging.GetInstance().Debug("Entering method Sample.CreateChildControls");
                // function logic goes here
                Control control = Page.LoadControl(_ascxPath);
                Controls.Add(control);
            }
            catch (Exception ex)
            {
                Logging.GetInstance().LogException(ex);
                //handle exception as the business require.
            }
            finally
            {
                //dispose any objects that require dispose.
                Logging.GetInstance().Debug("Exiting method Sample.CreateChildControls");
            }



            // SPUtility.Redirect("/myurl.aspx", SPRedirectFlags.DoNotEndResponse, System.Web.HttpContext.Current);
            //try
            //{
            //    SPContext.Current.Web.AllowUnsafeUpdates = true;
            //    // State-changing operation occurs here.
            //}
            //catch
            //{
            //    // Handle or re-throw an exception.
            //}
            //finally
            //{
            //    SPContext.Current.Web.AllowUnsafeUpdates = false;
            //}
        }
        public static bool InGroup(string currgroupName)
        {
            Logging.GetInstance().Debug("Entering HelperMethods.InGroup");
            bool returnValue = false;
            try
            {
                SPSecurity.RunWithElevatedPrivileges(delegate
                {
                    using (SPWeb web = new SPSite(SPContext.Current.Web.Url).OpenWeb())
                    {
                        SPGroup siteGroup = web.SiteGroups[currgroupName];

                        if (siteGroup != null)
                        {
                            if (SPContext.Current.Web.IsCurrentUserMemberOfGroup(siteGroup.ID))
                                returnValue = true;
                            else
                            {
                                SPUserCollection _users = siteGroup.Users;
                                foreach (SPUser user in _users)
                                    if (user.LoginName.Equals(SPContext.Current.Web.CurrentUser.LoginName))
                                    {
                                        returnValue = true;
                                        break;
                                    }
                            }
                        }
                    }
                });
                if (!returnValue)
                {
                    SPGroupCollection _SPGroupCollection = SPContext.Current.Web.CurrentUser.Groups;
                    foreach (SPGroup group in _SPGroupCollection)
                        if (currgroupName.Equals(group.Name))
                            return true;


                    SPSecurity.RunWithElevatedPrivileges(delegate ()
                    {
                        var principalContext = new PrincipalContext(ContextType.Domain);
                        var userPrincipal = UserPrincipal.FindByIdentity(principalContext, System.DirectoryServices.AccountManagement.IdentityType.SamAccountName, SPContext.Current.Web.CurrentUser.LoginName);
                        var group = GroupPrincipal.FindByIdentity(principalContext, currgroupName);
                        returnValue = group.Members.Any(x => x.SamAccountName == userPrincipal.SamAccountName);
                    });
                }
            }
            catch (Exception ex)
            {
                Logging.GetInstance().LogException(ex);
            }
            finally
            {
                Logging.GetInstance().Debug("Exit HelperMethods.InGroup");
            }
            return returnValue;
        }
    }
}
