using ITWORX.MOEHE.Utilities;
using ITWORX.MOEHE.Utilities.Logging;
using Microsoft.SharePoint;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ITWORX.MOEHEWF.UCE.BL
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
                            //SPList delegationDocumentLibrary = web.Lists[Utilities.Constants.DelegationDocuments];
                            if (delegationTemplateLibrary == null)
                                throw new Exception();

                            delegationTemplate = (from SPListItem item in delegationTemplateLibrary.Items
                                                  select new Entities.DelegationTemplate
                                                  {
                                                      ID = item.ID,
                                                      FileName = item.File.Name,
                                                      FileTitle = ( (LCID == (int)Language.English) ? Convert.ToString(item["Title"]) : Convert.ToString(item["TitleAr"]) ),
                                                      FileExtension= Path.GetExtension(item.File.Name),
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

        public static string GetDelegationTemplateUrlById(int templateId)
        {
            Logging.GetInstance().Debug("Entering DelegationTemplates.GetDelegationTemplateUrlById");
            string templateString = string.Empty;
            try
            {
                SPSecurity.RunWithElevatedPrivileges(delegate ()
                {
                    using (SPSite site = new SPSite(SPContext.Current.Site.Url))
                    {
                        using (SPWeb web = site.OpenWeb())
                        {
                            SPList delegationTemplateLibrary = web.Lists[Common.Utilities.Constants.DelegationTemplate];
                           
                            if (delegationTemplateLibrary == null)
                                throw new Exception();

                            SPListItem delegationItem = delegationTemplateLibrary.GetItemById(templateId);
                            if (delegationItem!=null)
                            {
                                
                               templateString = delegationItem["FileRef"] != null ? (site.Url + delegationItem["FileRef"].ToString()) : string.Empty;
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
                Logging.GetInstance().Debug("Exit DelegationTemplates.GetDelegationTemplateUrlById");
            }
            return templateString;
        }
    }
}
