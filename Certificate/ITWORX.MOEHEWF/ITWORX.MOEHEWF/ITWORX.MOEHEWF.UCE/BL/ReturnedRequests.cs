using ITWORX.MOEHE.Utilities;
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
    public class ReturnedRequests : UserControlBase
    {
        public static IEnumerable<Entities.SimilarRequest> GetAllReturnedRequests(string strQuery, int LCID)
        {
            List<Entities.SimilarRequest> Requests = new List<Entities.SimilarRequest>();
            using (SPSite oSite = new SPSite(SPContext.Current.Site.ID))
            {
                using (SPWeb oWeb = oSite.OpenWeb())
                {
                    try
                    {
                        Logging.GetInstance().Debug("Entering method SampleUserControl.Page_Load");
                        string strViewFields = Utilities.BusinessHelper.GetstrViewFields(LCID);
                        oWeb.AllowUnsafeUpdates = true;
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
                        Logging.GetInstance().Debug("Exiting method SampleUserControl.Page_Load");
                    }
                    return Requests;
                }
            }
        }
        public static IEnumerable<Entities.SimilarRequest> GetAllReturnedRequests(string strQuery)
        {
            List<Entities.SimilarRequest> Requests = new List<Entities.SimilarRequest>();
           
                    try
                    {
                        Logging.GetInstance().Debug("Entering method SampleUserControl.Page_Load");
                      
                       
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
                        Logging.GetInstance().Debug("Exiting method SampleUserControl.Page_Load");
                    }
                    return Requests;
              
        }
        public static List<string> GetReturnedQueryPerRole(string SPGroupName)
        {
            List<string> objColumns = new List<string>();
            string strQuery = string.Empty;
            //Program Manager Query
            if (Common.Utilities.Constants.ProgramManagerGroupName == SPGroupName)
            {
                objColumns.Add("RequestStatusId;Lookup;Eq;" + (int)Common.Utilities.RequestStatus.UCEProgramManagerMissingRecommendationFromAssistantUndersecretary);
                objColumns.Add("RequestStatusId;Lookup;Eq;" + (int)Common.Utilities.RequestStatus.UCEProgramManagerMissingRecommendationFromHeadManager);
                objColumns.Add("RequestStatusId;Lookup;Eq;" + (int)Common.Utilities.RequestStatus.UCEProgramManagerReviewRecommendation);
            }

            //Program Employees Query
            else if (Common.Utilities.Constants.ArabicProgEmployeeGroupName == SPGroupName || Common.Utilities.Constants.EuropeanProgEmployeeGroupName == SPGroupName)
            {
                objColumns.Add("RequestStatusId;Lookup;Eq;" + (int)Common.Utilities.RequestStatus.UCProgramEmployeeClarificationReply);
                objColumns.Add("RequestStatusId;Lookup;Eq;" + (int)Common.Utilities.RequestStatus.UCEAsianAndEuropianEmployeeMissingInformation);
                objColumns.Add("RequestStatusId;Lookup;Eq;" + (int)Common.Utilities.RequestStatus.UCEProgramEmployeeMissingInformation);
                objColumns.Add("RequestStatusId;Lookup;Eq;" + (int)Common.Utilities.RequestStatus.UCEHeadManagerAccepted);
                objColumns.Add("RequestStatusId;Lookup;Eq;" + (int)Common.Utilities.RequestStatus.UCEHeadManagerRejected);
                objColumns.Add("RequestStatusId;Lookup;Eq;" + (int)Common.Utilities.RequestStatus.UCEAssistantUndersecretaryAccepted);
                objColumns.Add("RequestStatusId;Lookup;Eq;" + (int)Common.Utilities.RequestStatus.UCEAssistantUndersecretaryRejected);
                objColumns.Add("RequestStatusId;Lookup;Eq;" + (int)Common.Utilities.RequestStatus.UCECulturalMissionStatementReply);
                objColumns.Add("RequestStatusId;Lookup;Eq;" + (int)Common.Utilities.RequestStatus.UCEHigherEduInstitutesStatementReply);
                objColumns.Add("RequestStatusId;Lookup;Eq;" + (int)Common.Utilities.RequestStatus.UCECulturalMissionNeedsStatement);
                objColumns.Add("RequestStatusId;Lookup;Eq;" + (int)Common.Utilities.RequestStatus.UCEHigherEduInstitutesNeedsStatement);

                

            }

            //Legal Affairs Query
            else if (Common.Utilities.Constants.LegalAffairsGroupName == SPGroupName)
                objColumns.Add("RequestStatusId;Lookup;Eq;" + (int)Common.Utilities.RequestStatus.UCELegalEmployeeMissingInformation);

            //Receptionist Query
            else if (Common.Utilities.Constants.ReceptionistGroupName == SPGroupName)
            {
                objColumns.Add("RequestStatusId;Lookup;Eq;" + (int)Common.Utilities.RequestStatus.UCReceptionistClarificationReply);
                objColumns.Add("RequestStatusId;Lookup;Eq;" + (int)Common.Utilities.RequestStatus.UCEReceptionistMissingInformation);
            }

            //Department Manager Query
            else if (Common.Utilities.Constants.DepartmentManagerGroupName == SPGroupName)
            {
                objColumns.Add("RequestStatusId;Lookup;Eq;" + (int)Common.Utilities.RequestStatus.UCEHeadManagerMissingInformationTechnicalCommittee);
                objColumns.Add("RequestStatusId;Lookup;Eq;" + (int)Common.Utilities.RequestStatus.UCELegalEmployeeSendNotes);
            }

            //Assistant Undersecretary Query
            else if (Common.Utilities.Constants.AssistUndersecretaryGroupName == SPGroupName)
                objColumns.Add("RequestStatusId;Lookup;Eq;" + (int)Common.Utilities.RequestStatus.UCEAssistantUndersecretaryMissingInformation);
            
            return objColumns;
        }

    }
}
