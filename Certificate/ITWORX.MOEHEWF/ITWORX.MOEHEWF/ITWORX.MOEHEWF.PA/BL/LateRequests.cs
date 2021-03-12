using ITWORX.MOEHE.Utilities;
using ITWORX.MOEHE.Utilities.Logging;
using ITWORX.MOEHEWF.PA.Entities;
using ITWORX.MOEHEWF.PA.Utilities;

using Microsoft.SharePoint;
using System;
using System.Collections.Generic;

namespace ITWORX.MOEHEWF.PA.BL
{
    public class LateRequests : UserControlBase
    {
        public static IEnumerable<SimilarRequest> GetAllLateRequests(string strQuery, int LCID)
        {
            List<Entities.SimilarRequest> Requests = new List<Entities.SimilarRequest>();
            using (SPSite oSite = new SPSite(SPContext.Current.Site.ID))
            {
                using (SPWeb oWeb = oSite.OpenWeb())
                {
                    try
                    {
                        Logging.GetInstance().Debug("Entering method PASearchSimilarRequests.GetAllLateRequests");
                        oWeb.AllowUnsafeUpdates = true;
                        string strViewFields = Utilities.BusinessHelper.GetstrViewFields(LCID);
                        SPQuery query = new SPQuery
                        {
                            Joins = "<Join Type='INNER' ListAlias='Applicants'>" +
                                      "<Eq>" +
                                      "<FieldRef Name='ApplicantID' RefType='Id'/>" +
                                      "<FieldRef List='Applicants' Name='ID'/>" +
                                      "</Eq>" +
                                      "</Join>" +
                                      "<Join Type='INNER' ListAlias='Nationality'>" +
                                      "<Eq>" +
                                      "<FieldRef List='Applicants' Name='Nationality' RefType='Id'/>" +
                                      "<FieldRef List='Nationality' Name='ID'/>" +
                                      "</Eq>" +
                                      "</Join>",

                            ProjectedFields = "<Field Name='Applicants_ArabicName' Type='Lookup' " +
                                                "List='Applicants' ShowField='ArabicName'/>" +
                                                "<Field Name='Applicants_EnglishName' Type='Lookup' " +
                                                "List='Applicants' ShowField='EnglishName'/>" +
                                                  "<Field Name='Applicants_ApplicantName' Type='Lookup' " +
                                                "List='Applicants' ShowField='ApplicantName'/>" +
                                                "<Field Name='Applicants_QatarID' Type='Lookup' " +
                                                "List='Applicants' ShowField='PersonalID'/>" +
                                                 "<Field Name='Applicants_QID' Type='Lookup' " +
                                                "List='Applicants' ShowField='ID'/>" +
                                                "<Field Name='Nationality_Title' Type='Lookup' " +
                                                "List='Nationality' ShowField='Title'/>" +
                                                "<Field Name='Nationality_TitleAr' Type='Lookup' " +
                                                "List='Nationality' ShowField='TitleAr'/>",

                            ViewFields = strViewFields,
                            Query = strQuery,
                        };
                        Requests = BL.Request.GetRequestsListing(query, LCID);
                    }
                    catch (Exception ex)
                    {
                        Logging.GetInstance().LogException(ex);
                    }
                    finally
                    {
                        oWeb.AllowUnsafeUpdates = false;
                        Logging.GetInstance().Debug("Exiting method PASearchSimilarRequests.GetAllLateRequests");
                    }
                    return Requests;
                }
            }
        }

        public static IEnumerable<SimilarRequest> GetAllLateRequests(string strQuery)
        {
            List<Entities.SimilarRequest> Requests = new List<Entities.SimilarRequest>();
            using (SPSite oSite = new SPSite(SPContext.Current.Site.ID))
            {
                using (SPWeb oWeb = oSite.OpenWeb())
                {
                    try
                    {
                        Logging.GetInstance().Debug("Entering method PASearchSimilarRequests.GetAllLateRequests");
                        oWeb.AllowUnsafeUpdates = true;
                        string strViewFields = Utilities.BusinessHelper.GetstrViewFields();
                        SPQuery query = new SPQuery
                        {
                           

                          ViewFields=strViewFields,
                            Query = strQuery
                        };
                        Requests = BL.Request.GetRequestsListing(query);
                    }
                    catch (Exception ex)
                    {
                        Logging.GetInstance().LogException(ex);
                    }
                    finally
                    {
                        oWeb.AllowUnsafeUpdates = false;
                        Logging.GetInstance().Debug("Exiting method PASearchSimilarRequests.GetAllLateRequests");
                    }
                    return Requests;
                }
            }
        }

        public static List<string> GetLateQueryPerRole()
        {
            List<string> objColumns = new List<string>();
            int delayedDays = int.Parse(HelperMethods.GetConfigurationValue(SPContext.Current.Site.Url + Common.Utilities.Constants.HEWebUrl, Common.Utilities.Constants.Configuration, "PADelayedDays"));
            objColumns.Add("DelayedDays;Number;Gt;" + delayedDays.ToString());
            return objColumns;
        }
    }
}