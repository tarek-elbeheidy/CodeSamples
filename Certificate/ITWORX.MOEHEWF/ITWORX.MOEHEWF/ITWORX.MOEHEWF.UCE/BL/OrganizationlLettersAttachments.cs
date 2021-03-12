using ITWORX.MOEHE.Utilities.Logging;
using ITWORX.MOEHEWF.UCE.Utilities;
using Microsoft.SharePoint;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ITWORX.MOEHEWF.UCE.BL
{
  public class OrganizationlLettersAttachments
    {
        public static List<Entities.OrganizationlLettersAttachments> GetOrganizationLetterData(int requestNumber)
        {

            Logging.GetInstance().Debug("Entering OrganizationlLettersAttachments.GetOrganizationLetterData");
            List<Entities.OrganizationlLettersAttachments> organizationlLettersAttachments = new List<Entities.OrganizationlLettersAttachments>();
            try
            {
                SPSecurity.RunWithElevatedPrivileges(delegate ()
                {
                    using (SPSite site = new SPSite(SPContext.Current.Site.Url))
                    {
                        using (SPWeb web = site.OpenWeb())
                        {
                            SPList copyOfOranizationList = web.Lists[Constants.OrganizationalLettersAttachments];
                            if (copyOfOranizationList == null)
                                throw new Exception();

                            SPQuery copyOfOrganizationQuery = Common.Utilities.BusinessHelper.GetQueryObject("<Where><Eq><FieldRef Name='RequestID' /><Value Type='Lookup'>" +
                                requestNumber + "</Value></Eq></Where>");

                            SPListItemCollection copyOfOrganizationCollection = copyOfOranizationList.GetItems(copyOfOrganizationQuery);

                            if (copyOfOrganizationCollection != null && copyOfOrganizationCollection.Count > 0)
                            {
                                organizationlLettersAttachments = (from SPListItem item in copyOfOrganizationCollection
                                                                   select new Entities.OrganizationlLettersAttachments
                                                                   {
                                                                       ID = item.ID,
                                                                       FileName = item.File.Name,
                                                                       FileUrl = item.File.ServerRelativeUrl,
                                                                       LoginName = web.SiteUsers[Convert.ToString(item["LoginName"])].Name,
                                                                       CreatedOn = Convert.ToDateTime(item["Created"])

                                                                   }).ToList();
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

                Logging.GetInstance().Debug("Exit OrganizationlLettersAttachments.GetOrganizationLetterData");
            }
            return organizationlLettersAttachments;

        }
        public static void DeleteOrganizationalLetter(int requestNumber)
        {
            Logging.GetInstance().Debug("Entering OrganizationlLettersAttachments.DeleteOrganizationalLetter");
            SPWeb web = null;

            try
            {
                SPSecurity.RunWithElevatedPrivileges(delegate ()
                {
                    using (SPSite site = new SPSite(SPContext.Current.Site.Url))
                    {
                        using (web = site.OpenWeb())
                        {
                            SPQuery organizationalLetterQuery = Common.Utilities.BusinessHelper.GetQueryObject("<Where><Eq><FieldRef Name='RequestID' /><Value Type='Lookup'>" +
                                requestNumber + "</Value></Eq></Where>");

                            SPList organizationalList = web.Lists[Constants.OrganizationalLettersAttachments];
                            SPListItemCollection organizationalCollection = organizationalList.GetItems(organizationalLetterQuery);
                            web.AllowUnsafeUpdates = true;
                            if (organizationalCollection != null && organizationalCollection.Count > 0)
                            {
                                foreach (SPListItem item in organizationalCollection)
                                {
                                    organizationalList.Items.DeleteItemById(item.ID);
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
                web.AllowUnsafeUpdates = false;
                Logging.GetInstance().Debug("Exit OrganizationlLettersAttachments.DeleteOrganizationalLetter");
            }

        }
        public static void DeleteOrganizationalAttachmentsByGroupAndRequestID(int requestId, string groupName)
        {
            SPWeb web = null;
            try
            {
                Logging.GetInstance().Debug("Entering method OrganizationlLettersAttachments.DeleteOrganizationalAttachmentsByGroupAndRequestID");
                SPSecurity.RunWithElevatedPrivileges(delegate ()
                {
                    using (SPSite site = new SPSite(SPContext.Current.Site.Url))
                    {
                        using ( web = site.OpenWeb())
                        {
                            SPList requestsAttachments = web.Lists[Utilities.Constants.OrganizationalLettersAttachments];
                            if (requestsAttachments == null)
                                throw new Exception();

                            SPQuery sPQuery = Common.Utilities.BusinessHelper.GetQueryObject("<Where><And><Eq><FieldRef Name='RequestID' /><Value Type='Lookup'>" + 
                            requestId + "</Value></Eq><Eq><FieldRef Name='Group' /><Value Type='Text'>" + groupName + "</Value></Eq></And></Where>");

                            SPListItemCollection attachItems = requestsAttachments.GetItems(sPQuery);
                            if (attachItems != null && attachItems.Count > 0)
                            {
                                web.AllowUnsafeUpdates = true;
                                foreach (SPListItem item in attachItems)
                                {
                                    requestsAttachments.Items.DeleteItemById(item.ID);
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
                web.AllowUnsafeUpdates = false;
                Logging.GetInstance().Debug("Exiting method OrganizationlLettersAttachments.DeleteOrganizationalAttachmentsByGroupAndRequestID");
            }
        }
    }
}
