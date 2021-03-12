using ITWORX.MOEHE.Utilities;
using ITWORX.MOEHE.Utilities.Logging;
using ITWORX.MOEHEWF.UCE.Entities;
using ITWORX.MOEHEWF.UCE.Utilities;
using Microsoft.SharePoint;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ITWORX.MOEHEWF.UCE.BL
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
                        Logging.GetInstance().Debug("Entering method SearchSimilarRequests.GetAllLateRequests");
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

                            ProjectedFields = "<Field Name='Applicants_ApplicantName' Type='Lookup' " +
                                                "List='Applicants' ShowField='ApplicantName'/>" +

                                                "<Field Name='Applicants_QatarID' Type='Lookup' " +
                                                "List='Applicants' ShowField='PersonalID'/>" +

                                                 "<Field Name='Applicants_ArabicName' Type='Lookup' " +
                                                "List='Applicants' ShowField='ArabicName'/>" +

                                                "<Field Name='Applicants_EnglishName' Type='Lookup' " +
                                                "List='Applicants' ShowField='EnglishName'/>" +

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
                        Logging.GetInstance().Debug("Exiting method SearchSimilarRequests.GetAllLateRequests");
                    }
                    return Requests;
                }
            }
        }

        public static IEnumerable<SimilarRequest> GetAllLateRequests(string strQuery)
        {
            List<Entities.SimilarRequest> Requests = new List<Entities.SimilarRequest>();
             try
                    {
                        Logging.GetInstance().Debug("Entering method SearchSimilarRequests.GetAllLateRequests");
                        //string strViewFields = Utilities.BusinessHelper.GetstrViewFields(LCID);
                        SPQuery query = new SPQuery
                        {
                            Query = strQuery
                        };
                        Requests = BL.Request.GetRequestsListingCount(query);
                    }

                    catch (Exception ex)
                    {
                        Logging.GetInstance().LogException(ex);
                    }
                    finally
                    {
                        Logging.GetInstance().Debug("Exiting method SearchSimilarRequests.GetAllLateRequests");
                    }
                    return Requests;
                
        }
        public static List<string> GetLateQueryPerRole()
        {
            List<string> objColumns = new List<string>();
            //SPUser user = SPContext.Current.Web.EnsureUser(SPContext.Current.Web.CurrentUser.Name);
            int delayedDays = int.Parse(HelperMethods.GetConfigurationValue(SPContext.Current.Site.Url + Common.Utilities.Constants.HEWebUrl, Common.Utilities.Constants.Configuration, "DelayedDays"));
            objColumns.Add("DelayedDays;Number;Gt;" + delayedDays.ToString());
            return objColumns;
        }

    }
}
