using ITWORX.MOEHE.Utilities.Logging;
using ITWORX.MOEHEWF.Common.Utilities;
using Microsoft.SharePoint;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ITWORX.MOEHEWF.Common.BL
{
    public class OrganizationlLettersAttachments
    {
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
        
        }
    }
