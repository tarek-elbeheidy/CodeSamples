using ITWORX.MOEHE.Utilities;
using ITWORX.MOEHE.Utilities.Logging;
using Microsoft.SharePoint;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace ITWORX.MOEHEWF.PA.BL
{
    public class DelegationTemplate
    {
        public static List<Entities.DelegationTemplate> GetDelegationTemplatesData(int LCID)
        {
            Logging.GetInstance().Debug("Entering DelegationTemplates.GetDelegationTemplatesData");
            List<Entities.DelegationTemplate> delegationTemplate = new List<Entities.DelegationTemplate>();
            try
            {
                SPSecurity.RunWithElevatedPrivileges(delegate ()
                {
                    using (SPSite site = new SPSite(SPContext.Current.Site.Url))
                    {
                        using (SPWeb web = site.OpenWeb())
                        {
                            SPList delegationTemplateLibrary = web.Lists[Common.Utilities.Constants.DelegationTemplate];
                            //SPList delegationDocumentLibrary = web.Lists[Utilities.Constants.PADelegationDocuments];
                            if (delegationTemplateLibrary == null)
                                throw new Exception();

                            delegationTemplate = (from SPListItem item in delegationTemplateLibrary.Items
                                                  select new Entities.DelegationTemplate
                                                  {
                                                      ID = item.ID,
                                                      FileName = item.File.Name,
                                                      FileTitle = ((LCID == (int)Language.English) ? Convert.ToString(item["Title"]) : Convert.ToString(item["TitleAr"])),
                                                      FileExtension = Path.GetExtension(item.File.Name),
                                                      //  item.File.ServerRelativeUrl
                                                      Url = item["FileRef"] != null ? (site.Url + item["FileRef"].ToString()) : string.Empty
                                                  }).ToList();
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
                Logging.GetInstance().Debug("Exit DelegationTemplates.GetDelegationTemplatesData");
            }
            return delegationTemplate;
        }
    }
}