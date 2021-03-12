using ITWORX.MOEHE.Utilities.Logging;
using ITWORX.MOEHEWF.PA.Utilities;
using Microsoft.SharePoint;
using System;
using System.Collections.Generic;

namespace ITWORX.MOEHEWF.PA.BL
{
    public class PANewRequests : UserControlBase
    {
        public static IEnumerable<Entities.SimilarRequest> GetAllPANewRequests(string strQuery, int LCID)
        {
            List<Entities.SimilarRequest> Requests = new List<Entities.SimilarRequest>();
            using (SPSite oSite = new SPSite(SPContext.Current.Site.ID))
            {
                using (SPWeb oWeb = oSite.OpenWeb())
                {
                    try
                    {
                        Logging.GetInstance().Debug("Entering method PASearchSimilarRequests.GetAllPANewRequests");
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

                            ProjectedFields =  "<Field Name='Applicants_QatarID' Type='Lookup' " +
                                                "List='Applicants' ShowField='PersonalID'/>" +
                                                  "<Field Name='Applicants_ApplicantName' Type='Lookup' " +
                                                "List='Applicants' ShowField='ApplicantName'/>" +
                                                 "<Field Name='Applicants_ArabicName' Type='Lookup' " +
                                                "List='Applicants' ShowField='ArabicName'/>" +

                                                "<Field Name='Applicants_EnglishName' Type='Lookup' " +
                                                "List='Applicants' ShowField='EnglishName'/>" +
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
                        Logging.GetInstance().Debug("Exiting method PASearchSimilarRequests.GetAllPANewRequests");
                    }
                }
            }
            return Requests;
        }


        public static IEnumerable<Entities.SimilarRequest> GetAllPANewRequests(string strQuery)
        {
            List<Entities.SimilarRequest> Requests = new List<Entities.SimilarRequest>();
            using (SPSite oSite = new SPSite(SPContext.Current.Site.ID))
            {
                using (SPWeb oWeb = oSite.OpenWeb())
                {
                    try
                    {
                        Logging.GetInstance().Debug("Entering method PASearchSimilarRequests.GetAllPANewRequests");
                        oWeb.AllowUnsafeUpdates = true;
                        string strViewFields = Utilities.BusinessHelper.GetstrViewFields();
                        SPQuery query = new SPQuery
                        {
                            //Joins = "<Join Type='INNER' ListAlias='Applicants'>" +
                            //          "<Eq>" +
                            //          "<FieldRef Name='ApplicantID' RefType='Id'/>" +
                            //          "<FieldRef List='Applicants' Name='ID'/>" +
                            //          "</Eq>" +
                            //          "</Join>" +
                            //          "<Join Type='INNER' ListAlias='Nationality'>" +
                            //          "<Eq>" +
                            //          "<FieldRef List='Applicants' Name='Nationality' RefType='Id'/>" +
                            //          "<FieldRef List='Nationality' Name='ID'/>" +
                            //          "</Eq>" +
                            //          "</Join>",

                            //ProjectedFields = "<Field Name='Applicants_QatarID' Type='Lookup' " +
                            //                    "List='Applicants' ShowField='PersonalID'/>" +
                            //                      "<Field Name='Applicants_ApplicantName' Type='Lookup' " +
                            //                    "List='Applicants' ShowField='ApplicantName'/>" +
                            //                     "<Field Name='Applicants_ArabicName' Type='Lookup' " +
                            //                    "List='Applicants' ShowField='ArabicName'/>" +

                            //                    "<Field Name='Applicants_EnglishName' Type='Lookup' " +
                            //                    "List='Applicants' ShowField='EnglishName'/>" +
                            //                    "<Field Name='Applicants_QID' Type='Lookup' " +
                            //                    "List='Applicants' ShowField='ID'/>" +
                            //                    "<Field Name='Nationality_Title' Type='Lookup' " +
                            //                    "List='Nationality' ShowField='Title'/>" +
                            //                    "<Field Name='Nationality_TitleAr' Type='Lookup' " +
                            //                    "List='Nationality' ShowField='TitleAr'/>",

                            ViewFields = strViewFields,
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
                        Logging.GetInstance().Debug("Exiting method PASearchSimilarRequests.GetAllPANewRequests");
                    }
                }
            }
            return Requests;
        }

        public static List<string> GetNewQueryPerRole(string SPGroupName)
        {
            List<string> objColumns = new List<string>();
            string strQuery = string.Empty;

            //Program Manager Query
            if (Common.Utilities.Constants.ProgramManagerGroupName.ToLower() == SPGroupName.ToLower())
            {
                objColumns.Add("RequestStatusId;Lookup;Eq;" + (int)Common.Utilities.RequestStatus.PAProgramManagerReviewRecommendation); 
            }

            //Program Employees Query
            else if (Common.Utilities.Constants.ArabicProgEmployeeGroupName.ToLower() == SPGroupName.ToLower() || Common.Utilities.Constants.EuropeanProgEmployeeGroupName.ToLower() == SPGroupName.ToLower())
            {
                objColumns.Add("RequestStatusId;Lookup;Eq;" + (int)Common.Utilities.RequestStatus.PASubmitted); 
            }

            //Legal Affairs Query
            else if (Common.Utilities.Constants.LegalAffairsGroupName.ToLower() == SPGroupName.ToLower())
                objColumns.Add("RequestStatusId;Lookup;Eq;" + (int)Common.Utilities.RequestStatus.PALegalEmployeePendingReview);

            //Receptionist Query
            else if (Common.Utilities.Constants.ReceptionistGroupName.ToLower() == SPGroupName.ToLower())
            {
                objColumns.Add("RequestStatusId;Lookup;Eq;" + (int)Common.Utilities.RequestStatus.PASubmitted);
                objColumns.Add("RequestStatusId;Lookup;Eq;" + (int)Common.Utilities.RequestStatus.PAReceptionistReviewInformation);
            }

            //Technical Committee Query
            else if (Common.Utilities.Constants.TechnicalCommitteeGroupName.ToLower() == SPGroupName.ToLower())
            {
                objColumns.Add("RequestStatusId;Lookup;Eq;" + (int)Common.Utilities.RequestStatus.PATechnicalCommitteeReviewRecommendationFromHeadManager);
                objColumns.Add("RequestStatusId;Lookup;Eq;" + (int)Common.Utilities.RequestStatus.PATechnicalCommitteeReviewRecommendationAssistantUndersecretary);
            }

            //Department Manager Query
            else if (Common.Utilities.Constants.DepartmentManagerGroupName.ToLower() == SPGroupName.ToLower())
            {
                objColumns.Add("RequestStatusId;Lookup;Eq;" + (int)Common.Utilities.RequestStatus.PAProgramManagerAcceptance);
                objColumns.Add("RequestStatusId;Lookup;Eq;" + (int)Common.Utilities.RequestStatus.PAProgramManagerReject); 
            }

            //Cultural Mission Query
            else if (Common.Utilities.Constants.CulturalMissionBritainGroupName.ToLower() == SPGroupName.ToLower() ||
                 Common.Utilities.Constants.CulturalMissionAustraliaGroupName.ToLower() == SPGroupName.ToLower() ||
                 Common.Utilities.Constants.CulturalMissionFranceGroupName.ToLower() == SPGroupName.ToLower() ||
                 Common.Utilities.Constants.CulturalMissionUSAGroupName.ToLower() == SPGroupName.ToLower() ||
                 Common.Utilities.Constants.CulturalMissionCanadaGroupName.ToLower() == SPGroupName.ToLower() ||
                 Common.Utilities.Constants.CulturalMissionJordanGroupName.ToLower() == SPGroupName.ToLower())
            {
                objColumns.Add("RequestStatusId;Lookup;Eq;" + (int)Common.Utilities.RequestStatus.PACulturalMissionNeedsStatement);
            }

            //Higher Educational Institutions Query
            else if (Common.Utilities.Constants.HigherEducationalInstitutionsGroupName.ToLower() == SPGroupName.ToLower())
            {
                objColumns.Add("RequestStatusId;Lookup;Eq;" + (int)Common.Utilities.RequestStatus.PAHigherEduInstitutesNeedsStatement);
            }
             
            return objColumns;
        }

        public static void CheckOutRequest(string ReqID)
        {
            SPSecurity.RunWithElevatedPrivileges(delegate ()
            {
                using (SPSite site = new SPSite(SPContext.Current.Site.Url))
                {
                    using (SPWeb web = site.OpenWeb())
                    {
                        Logging.GetInstance().Debug("Enter PANewRequests.lnk_CheckOut_Click");
                        try
                        {
                            SPList list = web.Lists[Utilities.Constants.PARequests];
                            SPListItem newitem = list.Items.GetItemById(Convert.ToInt32(ReqID));
                            SPFieldLookupValue value = new SPFieldLookupValue((int)Common.Utilities.RequestStatus.PAReceptionistReviewInformation, ((int)Common.Utilities.RequestStatus.PAReceptionistReviewInformation).ToString());
                            newitem["RequestStatus"] = Common.Utilities.BusinessHelper.GetLookupFieldFromValue(((int)Common.Utilities.RequestStatus.PAReceptionistReviewInformation).ToString(), "ID", Utilities.Constants.RequestStatus);
                            newitem["DelayedDays"] = "0";
                            web.AllowUnsafeUpdates = true;
                            newitem.Update();
                            list.Update();
                        }
                        catch (Exception ex)
                        {
                            Logging.GetInstance().LogException(ex);
                        }
                        finally
                        {
                            Logging.GetInstance().Debug("Exit PANewRequests.lnk_CheckOut_Click");
                        }
                    }
                }
            });
        }

        public static void AssignTo(string id, string AssignTo)
        {
            SPSecurity.RunWithElevatedPrivileges(delegate ()
            {
                using (SPSite site = new SPSite(SPContext.Current.Site.Url))
                {
                    using (SPWeb web = site.OpenWeb())
                    {
                        try
                        {
                            List<string> reqIDstr = new List<string>();
                            Logging.GetInstance().Debug("Entering method PANewRequests.lnk_AssignTo_Click");
                            SPList list = web.Lists[Utilities.Constants.PARequests];
                            SPListItem newitem = list.Items.GetItemById(Convert.ToInt32(id));
                            newitem["EmployeeAssignedTo"] = AssignTo;
                            //newitem["RequestStatus"] = new SPFieldLookupValue((int)Common.Utilities.RequestStatus.PAProgramEmployeeReview, ((int)Common.Utilities.RequestStatus.PAProgramEmployeeReview).ToString());
                            newitem["DelayedDays"] = "0";
                            web.AllowUnsafeUpdates = true;
                            newitem.Update();
                            list.Update();
                        }
                        catch (Exception ex)
                        {
                            Logging.GetInstance().LogException(ex);
                        }
                        finally
                        {
                            web.AllowUnsafeUpdates = false;
                            Logging.GetInstance().Debug("Exiting method PANewRequests.lnk_AssignTo_Click");
                        }
                    }
                }
            });
        }
    }
}