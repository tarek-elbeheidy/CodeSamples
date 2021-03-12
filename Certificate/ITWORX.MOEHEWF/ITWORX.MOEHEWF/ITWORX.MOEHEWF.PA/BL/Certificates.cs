using ITWORX.MOEHE.Utilities.Logging;
using ITWORX.MOEHEWF.PA.Utilities;
using Microsoft.SharePoint;
using System;
using System.Collections.Generic;

namespace ITWORX.MOEHEWF.PA.BL
{
    public class Certificates
    {
        public static List<Entities.Certificates> GetAll()
        {
            Logging.GetInstance().Debug("Entering method Certificates.GetAll");
            List<Entities.Certificates> certificates = new List<Entities.Certificates>();
            try
            {
                SPListItemCollection certificatesItemsCollection = BusinessHelper.GetLookupData(Constants.Certificates);

                if (certificatesItemsCollection != null && certificatesItemsCollection.Count > 0)
                {
                    foreach (SPListItem item in certificatesItemsCollection)
                    {
                        certificates.Add(new Entities.Certificates() { ID = item.ID, ArabicTitle = Convert.ToString(item["TitleAr"]), EnglishTitle = item.Title });
                    }
                }
            }
            catch (Exception ex)
            {
                Logging.GetInstance().LogException(ex);
            }
            finally
            {
                Logging.GetInstance().Debug("Exiting method Certificates.GetAll");
            }
            return certificates;
        }

        public static List<Entities.Certificates> GetCertificatesByAcademicDegree(int academicId, int LCID)
        {
            Logging.GetInstance().Debug("Entering method Certificates.GetCertificatesByAcademicDegree");
            List<Entities.Certificates> certificates = new List<Entities.Certificates>();
            try
            {
                SPSecurity.RunWithElevatedPrivileges(delegate ()
                {
                    using (SPSite site = new SPSite(SPContext.Current.Site.Url))
                    {
                        using (SPWeb web = site.OpenWeb())
                        {
                            SPList academicList = web.Lists[Constants.AcademicDegree];
                            if (academicList == null)
                                throw new Exception();
                            SPListItem academicItem = academicList.GetItemById(academicId);
                            if (academicItem != null)
                            {
                                SPFieldLookupValueCollection valueCollection = (LCID == 1033 ? academicItem["Certificates"] : academicItem["CertificatesAr"]) as SPFieldLookupValueCollection;
                                foreach (SPFieldLookupValue val in valueCollection)
                                {
                                    certificates.Add(new Entities.Certificates() { ID = val.LookupId, EnglishTitle = val.LookupValue, ArabicTitle = val.LookupValue });
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
                Logging.GetInstance().Debug("Exiting method Certificates.GetCertificatesByAcademicDegree");
            }
            return certificates;
        }
    }
}