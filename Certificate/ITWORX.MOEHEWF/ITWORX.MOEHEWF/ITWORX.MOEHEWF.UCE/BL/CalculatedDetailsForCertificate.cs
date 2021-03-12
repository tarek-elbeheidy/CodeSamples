using ITWORX.MOEHE.Utilities.Logging;
using ITWORX.MOEHEWF.Common.Entities;
using ITWORX.MOEHEWF.UCE.Utilities;
using Microsoft.SharePoint;
using Microsoft.SharePoint.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ITWORX.MOEHEWF.UCE.BL
{
    public class CalculatedDetailsForCertificate
    {
        public static List<Entities.CalculatedDetailsForCertificate> GetCalculatedDetailsForCertificate(int requestId,int LCID)
        {
            Logging.GetInstance().Debug("Entering CalculatedDetailsForCertificate.GetCalculatedDetailsForCertificate");
            SPListItemCollection calculatedCollection = null;
            List<Entities.CalculatedDetailsForCertificate> calculatedDetailsForCertificate = new List<Entities.CalculatedDetailsForCertificate>();
            try
            {
                SPSecurity.RunWithElevatedPrivileges(delegate ()
                {
                    using (SPSite site = new SPSite(SPContext.Current.Site.Url))
                    {
                        using (SPWeb web = site.OpenWeb())
                        {
                            SPQuery universitiesQuery = Common.Utilities.BusinessHelper.GetQueryObject("<Where><Eq><FieldRef Name='RequestID' /><Value Type='Lookup'>" +
                                requestId + "</Value></Eq></Where>");

                            SPList calculatedDataList = web.Lists[Constants.CalculatedDetailsForCertificate];
                            calculatedCollection = calculatedDataList.GetItems(universitiesQuery);
                            if (calculatedCollection !=null || calculatedCollection.Count >0)
                            {
                                foreach (SPListItem item in calculatedCollection)
                                {
                                    calculatedDetailsForCertificate.Add(new Entities.CalculatedDetailsForCertificate()
                                    {
                                        ID = item.ID,

                                        Country = new CountryOfStudy()
                                        {

                                            SelectedID = item["Country"] != null ? new SPFieldLookupValue(item["Country"].ToString()).LookupId.ToString() : string.Empty,
                                            SelectedTitle = item["Country"] != null ? (LCID == 1033 ? new SPFieldLookupValue(item["Country"].ToString()).LookupValue : new SPFieldLookupValue(item["CountryAr"].ToString()).LookupValue) : string.Empty
                                        },
                                        OtherCountry = item["OtherCountry"] != null ? Convert.ToString(item["OtherCountry"]):string.Empty,
                                        Univesrity = new University()
                                        {
                                            SelectedID = item["University"] != null ? new SPFieldLookupValue(item["University"].ToString()).LookupId.ToString() : string.Empty,
                                            SelectedTitle = item["University"] != null ?(LCID==1033 ? new SPFieldLookupValue(item["University"].ToString()).LookupValue : new SPFieldLookupValue(item["UniversityAr"].ToString()).LookupValue ): string.Empty
                                        },
                                        OtherUniversity = item["OtherUniversity"] != null ? Convert.ToString(item["OtherUniversity"]) : string.Empty,
                                
                                     Faculty = LCID == 1033 ? Convert.ToString(item["Faculty"]) : Convert.ToString(item["FacultyAr"]),
                                    OtherFaculty = item["OtherFaculty"] != null ? Convert.ToString(item["OtherFaculty"]) : string.Empty,
                                        StudySystem = new Entities.StudySystem()
                                        {
                                            SelectedID = item["StudyingSystem"] != null ? new SPFieldLookupValue(item["StudyingSystem"].ToString()).LookupId.ToString() : string.Empty,
                                            SelectedTitle = item["StudyingSystem"] != null ? (LCID == 1033 ? new SPFieldLookupValue(item["StudyingSystem"].ToString()).LookupValue : new SPFieldLookupValue(item["StudyingSystemAr"].ToString()).LookupValue ) : string.Empty
                                        }
                                    });
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

                Logging.GetInstance().Debug("Exit CalculatedDetailsForCertificate.GetCalculatedDetailsForCertificate");
            }
            return calculatedDetailsForCertificate;
        }

        public static int SaveCalculatedDetailsForCertificate(Entities.CalculatedDetailsForCertificate calculatedDetailsForCertificate, int requestId)
        {
            Logging.GetInstance().Debug("Enter CalculatedDetailsForCertificate.SaveCalculatedDetailsForCertificate");
            SPWeb web = null;
            SPListItem item = null;
            try
            {
                SPSecurity.RunWithElevatedPrivileges(delegate ()
                {
                    using (SPSite site = new SPSite(SPContext.Current.Site.Url))
                    {
                        using (web = site.OpenWeb())
                        {
                            SPList calculatedDataList = web.Lists[Constants.CalculatedDetailsForCertificate];
                            if (calculatedDataList == null)
                                throw new Exception();
                            item = calculatedDataList.AddItem();
                            bool hasValue = false;
                           

                            if (calculatedDetailsForCertificate.Country != null || !string.IsNullOrEmpty(calculatedDetailsForCertificate.OtherCountry))
                            {

                                if (!string.IsNullOrEmpty(calculatedDetailsForCertificate.OtherCountry))
                                {
                                    item["OtherCountry"] = SPHttpUtility.HtmlEncode(calculatedDetailsForCertificate.OtherCountry);
                                    hasValue = true;
                                }
                                else
                                {
                                    item["OtherCountry"] = string.Empty;
                                }
                                if (!string.IsNullOrEmpty(calculatedDetailsForCertificate.Country.SelectedID))
                                {
                                    item["Country"] = new SPFieldLookupValue(int.Parse(calculatedDetailsForCertificate.Country.SelectedID), calculatedDetailsForCertificate.Country.SelectedTitle);
                                    hasValue = true;
                                }
                                else
                                {

                                    item["Country"] = string.Empty;
                                }

                            }

                          
                            if (calculatedDetailsForCertificate.Univesrity != null || !string.IsNullOrEmpty(calculatedDetailsForCertificate.OtherUniversity))
                            {

                                if (!string.IsNullOrEmpty(calculatedDetailsForCertificate.OtherCountry))
                                {
                                    item["OtherUniversity"] = SPHttpUtility.HtmlEncode(calculatedDetailsForCertificate.OtherUniversity);
                                    hasValue = true;
                                }
                                else
                                {
                                    item["OtherUniversity"] = string.Empty;
                                }
                                if (!string.IsNullOrEmpty(calculatedDetailsForCertificate.Univesrity.SelectedID))
                                {
                                    item["University"] = new SPFieldLookupValue(int.Parse(calculatedDetailsForCertificate.Univesrity.SelectedID), calculatedDetailsForCertificate.Univesrity.SelectedTitle);
                                    hasValue = true;
                                }
                                else
                                {

                                    item["University"] = string.Empty;
                                }

                            }

                            if (calculatedDetailsForCertificate.Faculty != null || !string.IsNullOrEmpty(calculatedDetailsForCertificate.OtherFaculty))
                            {
                                item["Faculty"] =
                                item["FacultyAr"] =
                                SPHttpUtility.HtmlEncode(calculatedDetailsForCertificate.Faculty);
                            }

                            if (calculatedDetailsForCertificate.StudySystem!=null && !string.IsNullOrEmpty(calculatedDetailsForCertificate.StudySystem.SelectedID))
                            {
                                item["StudyingSystem"] = new SPFieldLookupValue(int.Parse(calculatedDetailsForCertificate.StudySystem.SelectedID), calculatedDetailsForCertificate.StudySystem.SelectedTitle);
                                hasValue = true;
                            }
                          
                            if (hasValue == true)
                            {
                                item["RequestID"] = new SPFieldLookupValue(requestId, requestId.ToString());
                                item.Update();

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
                Logging.GetInstance().Debug("Exit CalculatedDetailsForCertificate.SaveCalculatedDetailsForCertificate");
            }

            return item.ID;
        }

        public static void DeleteRequestCalculatedDetailsForCertificate(int requestNumber)
        {
            Logging.GetInstance().Debug("Entering CalculatedDetailsForCertificate.DeleteRequestCalculatedDetailsForCertificate");
            SPWeb web = null;
          
            try
            {
                SPSecurity.RunWithElevatedPrivileges(delegate ()
                {
                    using (SPSite site = new SPSite(SPContext.Current.Site.Url))
                    {
                        using (web = site.OpenWeb())
                        {
                            SPQuery universitiesQuery = Common.Utilities.BusinessHelper.GetQueryObject("<Where><Eq><FieldRef Name='RequestID' /><Value Type='Lookup'>" +
                                requestNumber + "</Value></Eq></Where>");

                            SPList calculatedDataList = web.Lists[Constants.CalculatedDetailsForCertificate];
                            SPListItemCollection calculatedCollection = calculatedDataList.GetItems(universitiesQuery);
                            web.AllowUnsafeUpdates = true;
                            if (calculatedCollection!=null && calculatedCollection.Count > 0)
                            {
                                foreach (SPListItem item in calculatedCollection)
                                {
                                    calculatedDataList.Items.DeleteItemById(item.ID);
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
                Logging.GetInstance().Debug("Exit CalculatedDetailsForCertificate.DeleteRequestCalculatedDetailsForCertificate");
            }

        }
        public static void DeleteCalculatedDetailsForCertificateItem(int calcItemId)
        {
            Logging.GetInstance().Debug("Entering method CalculatedDetailsForCertificate.DeleteCalculatedDetailsForCertificateItem");
            SPWeb web = null;
            try
            {
                SPSecurity.RunWithElevatedPrivileges(delegate ()
                {
                    using (SPSite site = new SPSite(SPContext.Current.Site.Url))
                    {
                        using (web = site.OpenWeb())
                        {

                            SPList universitiesDataList = web.Lists[Constants.CalculatedDetailsForCertificate];
                            SPListItem uniItem = universitiesDataList.GetItemById(calcItemId);
                            web.AllowUnsafeUpdates = true;
                            uniItem.Delete();

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
                if (web != null)
                {
                    web.AllowUnsafeUpdates = false;
                }

                Logging.GetInstance().Debug("Exiting method CalculatedDetailsForCertificate.DeleteCalculatedDetailsForCertificateItem");
            }
        }

            public static Entities.CalculatedDetailsForCertificate GetCalculatedDetailsForCertificateItem(int calculatedDeatailsId, int LCID)
        {
            Logging.GetInstance().Debug("Entering CalculatedDetailsForCertificate.GetCalculatedDetailsForCertificateItem");

            Entities.CalculatedDetailsForCertificate calculatedDetails = null;
            try
            {
                SPSecurity.RunWithElevatedPrivileges(delegate ()
                {
                    using (SPSite site = new SPSite(SPContext.Current.Site.Url))
                    {
                        using (SPWeb web = site.OpenWeb())
                        {

                            SPList calculatedDataList = web.Lists[Constants.CalculatedDetailsForCertificate];
                            SPListItem item = calculatedDataList.GetItemById(calculatedDeatailsId);
                            if (item != null)
                            {
                                calculatedDetails = new Entities.CalculatedDetailsForCertificate()
                                {
                                    ID = item.ID,
                                    Country = new CountryOfStudy()
                                    {

                                        SelectedID = item["Country"] != null ? new SPFieldLookupValue(item["Country"].ToString()).LookupId.ToString() : string.Empty,
                                        SelectedTitle = item["Country"] != null ? (LCID == 1033 ? new SPFieldLookupValue(item["Country"].ToString()).LookupValue : new SPFieldLookupValue(item["CountryAr"].ToString()).LookupValue) : string.Empty
                                    },
                                    Univesrity = new University()
                                    {
                                        SelectedID = item["University"] != null ? new SPFieldLookupValue(item["University"].ToString()).LookupId.ToString() : string.Empty,
                                        SelectedTitle = item["University"] != null ? (LCID == 1033 ? new SPFieldLookupValue(item["University"].ToString()).LookupValue : new SPFieldLookupValue(item["UniversityAr"].ToString()).LookupValue) : string.Empty
                                    },

                                    Faculty = LCID == 1033 ? Convert.ToString(item["Faculty"]) : Convert.ToString(item["FacultyAr"]),

                                    StudySystem = new Entities.StudySystem()
                                    {
                                        SelectedID = item["StudyingSystem"] != null ? new SPFieldLookupValue(item["StudyingSystem"].ToString()).LookupId.ToString() : string.Empty,
                                        SelectedTitle = item["StudyingSystem"] != null ? (LCID == 1033 ? new SPFieldLookupValue(item["StudyingSystem"].ToString()).LookupValue : new SPFieldLookupValue(item["StudyingSystemAr"].ToString()).LookupValue) : string.Empty
                                    }
                                };



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
              
                Logging.GetInstance().Debug("Exit CalculatedDetailsForCertificate.GetCalculatedDetailsForCertificateItem");
            }
            return calculatedDetails;
        }
    }
}
