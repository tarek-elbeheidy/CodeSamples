using ITWORX.MOEHE.Utilities.Logging;
using Microsoft.SharePoint;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ITWORX.MOEHEWF.Common.BL
{
    public class HETemplates
    {
        public static Entities.HETemplates GetAttachmentByType(string templateType)
        {
            Logging.GetInstance().Debug("Entering method HETemplates.GetAttachmentByType");
            Entities.HETemplates template = null;
            try
            {
                SPSecurity.RunWithElevatedPrivileges(delegate () {
                    using (SPSite site = new SPSite(SPContext.Current.Site.Url))
                    {
                        using (SPWeb web = site.OpenWeb())
                        {
                            SPList uceTemplatesList = web.Lists[Utilities.Constants.HETemplates];
                            if (uceTemplatesList == null)
                                throw new Exception();
                            SPQuery sPQuery = new SPQuery();
                            sPQuery.Query = @"<Where><Eq><FieldRef Name='Type' /><Value Type='Choice'>" + templateType + "</Value></Eq></Where>";
                            SPListItemCollection templatesColl = uceTemplatesList.GetItems(sPQuery);
                            if (templatesColl != null && templatesColl.Count > 0)
                            {
                                SPListItem tempItem = templatesColl[0];
                                template = new Entities.HETemplates() { ID = tempItem.ID, FileName = tempItem.File.Name, FileUrl = tempItem.File.ServerRelativeUrl };

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

                Logging.GetInstance().Debug("Exiting method HETemplates.GetAttachmentByType");
            }
            return template;
        }
    }
}
