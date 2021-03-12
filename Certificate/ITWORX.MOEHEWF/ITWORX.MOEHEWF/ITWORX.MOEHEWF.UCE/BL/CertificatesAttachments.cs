using ITWORX.MOEHE.Utilities;
using ITWORX.MOEHE.Utilities.Logging;
using Microsoft.SharePoint;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ITWORX.MOEHEWF.UCE.BL
{
    public class CertificatesAttachments
    {
        public static void DeleteCertificatesAttachmentsByGroupAndRequestID(int requestId, string certGroupName,  string certSequenceGroupName, string diplomaGroup, string interDiplomaGroup,string gradesGroup, string certEquivalentGroup  ,string certType)
        {
            try
            {
                Logging.GetInstance().Debug("Entering method CertificatesAttachments.DeleteCertificatesAttachmentsByGroupAndRequestID");
                SPSecurity.RunWithElevatedPrivileges(delegate ()
                {
                    using (SPSite site = new SPSite(SPContext.Current.Site.Url))
                    {
                        using (SPWeb web = site.OpenWeb())
                        {
                            SPList certificatesAttachments = web.Lists[Utilities.Constants.CertificatesAttachments];
                            if (certificatesAttachments == null)
                                throw new Exception();
                            SPQuery sPQuery = new SPQuery();
                            string bachelor = HelperMethods.LocalizedText("ITWORX_MOEHEWF_UCE", "Bachelor", SPContext.Current.Web.Language);
                            if (certType.Equals(bachelor))
                            {
                                sPQuery = Common.Utilities.BusinessHelper.GetQueryObject("<Where><And><Eq><FieldRef Name='RequestID' /><Value Type='Lookup'>"+requestId+
                                    "</Value></Eq><And><Neq><FieldRef Name='Group' /><Value Type='Text'>"+certGroupName+
                                    "</Value></Neq><And><Neq><FieldRef Name='Group' /><Value Type='Text'>"+certSequenceGroupName+
                                    "</Value></Neq><And><Neq><FieldRef Name='Group' /><Value Type='Text'>"+diplomaGroup+
                                     "</Value></Neq><And><Neq><FieldRef Name='Group' /><Value Type='Text'>" + certEquivalentGroup +
                                      "</Value></Neq><And><Neq><FieldRef Name='Group' /><Value Type='Text'>" + gradesGroup +
                                    "</Value></Neq><Neq><FieldRef Name='Group' /><Value Type='Text'>" +interDiplomaGroup+"</Value></Neq></And></And></And></And></And></And></Where>");
                            }
                            else
                            {
                                sPQuery = Common.Utilities.BusinessHelper.GetQueryObject("<Where><And><Eq><FieldRef Name='RequestID' /><Value Type='Lookup'>"+requestId
                                    +"</Value></Eq><And><Neq><FieldRef Name='Group' /><Value Type='Text'>"+certGroupName+
                                    "</Value></Neq><And><Neq><FieldRef Name='Group' /><Value Type='Text'>" + certEquivalentGroup +
                                      "</Value></Neq><And><Neq><FieldRef Name='Group' /><Value Type='Text'>" + gradesGroup 
                                    +"</Value></Neq><Neq><FieldRef Name='Group' /><Value Type='Text'>"+certSequenceGroupName+"</Value></Neq></And></And></And></And></Where>");
                             
                            }

                            SPListItemCollection certItems=  certificatesAttachments.GetItems(sPQuery);
                            if(certItems!=null && certItems.Count>0)
                            {
                                foreach (SPListItem item in certItems)
                                {
                                    certificatesAttachments.Items.DeleteItemById(item.ID);
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
                Logging.GetInstance().Debug("Exiting method CertificatesAttachments.DeleteCertificatesAttachmentsByGroupAndRequestID");
            }
        }
        }
}
