using ITWORX.MOEHE.Utilities.Logging;
using ITWORX.MOEHEWF.Common.Utilities;
using Microsoft.SharePoint;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ITWORX.MOEHEWF.Common.BL
{
    public class RequestStatus
    {
        public static SPListItemCollection GetRequestStatusItems()
        {
            Logging.GetInstance().Debug("Entering RequestStatus.GetRequestStatusItems");
            SPListItemCollection itemsCollection = null;
            try
            {
                SPSecurity.RunWithElevatedPrivileges(delegate ()
                {
                    using (SPSite site = new SPSite(SPContext.Current.Site.Url))
                    {
                        using (SPWeb web = site.OpenWeb())
                        {
                            SPList requestStatusList = web.Lists[Constants.RequestStatus];


                            if (requestStatusList == null)
                                throw new Exception();

                            SPQuery spQuery = Common.Utilities.BusinessHelper.GetQueryObject(string.Empty, "<FieldRef Name='ID' /><FieldRef Name='Code' /><FieldRef Name='ApplicantDescriptionEn' /><FieldRef Name='ApplicantDescriptionAr' /><FieldRef Name='FinalDecisionAr' /><FieldRef Name='FinalDecisionEn' />");
                            itemsCollection = requestStatusList.GetItems(spQuery);

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
                Logging.GetInstance().Debug("Exit RequestStatus.GetRequestStatusItems");
            }
            return itemsCollection;
        }
        public static List<Entities.RequestStatus> GetAll()
        {
            Logging.GetInstance().Debug("Entering method RequestStatus.GetAll");
            List<Entities.RequestStatus> requestStatus = new List<Entities.RequestStatus>();
            try
            {
                SPListItemCollection requestStatusItemsCollection = GetRequestStatusItems();

                if (requestStatusItemsCollection != null && requestStatusItemsCollection.Count > 0)
                {
                    foreach (SPListItem item in requestStatusItemsCollection)
                    {
                        requestStatus.Add(new Entities.RequestStatus()
                        {
                            ID = item.ID,
                            ApplicantDescriptionAr = Convert.ToString(item["ApplicantDescriptionAr"]),
                            ApplicantDescriptionEn = Convert.ToString(item["ApplicantDescriptionEn"]),
                            //ReviewerDescriptionEn = Convert.ToString(item["ReviewerDescriptionEn"]),
                            //ReviewerDescriptionAr = Convert.ToString(item["ReviewerDescriptionAr"]),
                            Code = Convert.ToString(item["Code"]),
                            FinalDecisionAr = Convert.ToString(item["FinalDecisionAr"]),
                            FinalDecisionEn = Convert.ToString(item["FinalDecisionEn"])
                        });
                    }
                }
                // requestStatus.Select(s => new Entities.RequestStatus { ID = s.ID, ApplicantDescriptionAr = s.ApplicantDescriptionAr, ApplicantDescriptionEn = s.ApplicantDescriptionEn, Code = s.Code, FinalDecisionAr = s.FinalDecisionAr, FinalDecisionEn = s.FinalDecisionEn }).Distinct();
            }

            catch (Exception ex)
            {
                Logging.GetInstance().LogException(ex);
            }
            finally
            {

                Logging.GetInstance().Debug("Exiting method RequestStatus.GetAll");
            }
            return requestStatus;
        }

        public static List<Entities.RequestStatus> GetStatusFinalDecision()
        {
            Logging.GetInstance().Debug("Entering method RequestStatus.GetStatusFinalDecision");
            List<Entities.RequestStatus> requestStatus = GetAll();
            try
            {
                requestStatus = requestStatus.FindAll(s => !string.IsNullOrEmpty(s.FinalDecisionEn) && !string.IsNullOrEmpty(s.FinalDecisionAr));
            }
            catch (Exception ex)
            {
                Logging.GetInstance().LogException(ex);
            }
            finally
            {

                Logging.GetInstance().Debug("Exiting method RequestStatus.GetStatusFinalDecision");
            }

            return requestStatus;
        }
        public static List<Entities.RequestStatus> GetDistinctStatus()
        {
            Logging.GetInstance().Debug("Entering method RequestStatus.GetDistinctStatus");
            List<Entities.RequestStatus> requestStatus = GetAll();
            try
            {
                requestStatus = requestStatus.GroupBy(r => r.ApplicantDescriptionEn).Select(r => r.FirstOrDefault()).ToList();

                requestStatus.FindAll(a => a.Code == Utilities.RequestStatus.UCESubmitted.ToString() &&
                a.Code == Utilities.RequestStatus.UCESubmitted.ToString());


            }
            catch (Exception ex)
            {
                Logging.GetInstance().LogException(ex);
            }
            finally
            {

                Logging.GetInstance().Debug("Exiting method RequestStatus.GetDistinctStatus");
            }

            return requestStatus;
        }
        public static List<Entities.RequestStatus> GetDistinctReviewerStatus(int LCID)
        {
            Logging.GetInstance().Debug("Entering method RequestStatus.GetDistinctStatus");
            List<Entities.RequestStatus> requestStatus = GetAll();
            try
            {
                requestStatus.RemoveAll(r => r.Code == Utilities.RequestStatus.CHEDMessage.ToString() || r.Code == Utilities.RequestStatus.HEDDMessage.ToString()
                || r.Code == Utilities.RequestStatus.RegistrationPassword.ToString() || r.Code == Utilities.RequestStatus.RegistrationVerificationCode.ToString()
                || r.Code == Utilities.RequestStatus.UCEAssistantUndersecretaryAccepted.ToString()
                || r.Code == Utilities.RequestStatus.UCEAssistantUndersecretaryAcceptDecisionTechnicalCommittee.ToString()
                || r.Code == Utilities.RequestStatus.UCEHeadManagereAcceptDecisionTechnicalCommitte.ToString()
                || r.Code == Utilities.RequestStatus.UCEHeadManagerRejectDecisionTechnicalCommittee.ToString()
                || r.Code == Utilities.RequestStatus.UCEAssistantUndersecretaryRejected.ToString()
                || r.Code == Utilities.RequestStatus.UCEAssistantUndersecretaryRejectDecisionTechnicalCommittee.ToString()
                || r.Code== Utilities.RequestStatus.ForgetPassword.ToString());
                if (LCID == (int)MOEHE.Utilities.Language.English)
                    requestStatus = requestStatus.GroupBy(r => r.ApplicantDescriptionEn).Select(r => r.FirstOrDefault()).ToList();
                else
                    requestStatus = requestStatus.GroupBy(r => r.ApplicantDescriptionAr).Select(r => r.FirstOrDefault()).ToList();

            }
            catch (Exception ex)
            {
                Logging.GetInstance().LogException(ex);
            }
            finally
            {

                Logging.GetInstance().Debug("Exiting method RequestStatus.GetDistinctStatus");
            }

            return requestStatus;
        }
        public static SPListItem GetRequestStatusItem(int requestStatusId)
        {
            Logging.GetInstance().Debug("Entering method RequestStatus.GetRequestStatusItem");
            SPListItem requestStatusItem = null;
            try
            {
                SPSecurity.RunWithElevatedPrivileges(delegate ()
                {
                    using (SPSite site = new SPSite(SPContext.Current.Site.Url))
                    {
                        using (SPWeb web = site.OpenWeb())
                        {
                            SPList requestStatusList = web.Lists[Constants.RequestStatus];
                            requestStatusItem = requestStatusList.GetItemById(requestStatusId);

                        }

                    }
                });
            }
            catch (Exception ex)
            {
                Logging.GetInstance().LogException(new Exception("Status ID: " + requestStatusId + " Exception Message: "));                
                Logging.GetInstance().LogException(ex);
            }
            finally
            {

                Logging.GetInstance().Debug("Exiting method RequestStatus.GetRequestStatusItem");
            }
            return requestStatusItem;

        }
        public static Entities.RequestStatus GetRequestStatusById(int requestStatusId)

        {
            Logging.GetInstance().Debug("Entering method RequestStatus.GetRequestStatusById");
            Entities.RequestStatus requestStatus = null;
            try
            {

                SPListItem requestStatusItem = GetRequestStatusItem(requestStatusId);
                if (requestStatusItem != null)
                {
                    requestStatus = new Entities.RequestStatus()
                    {
                        Code = requestStatusItem["Code"].ToString(),
                        HistoryDescriptionEn = requestStatusItem["HistoryDescriptionEn"] == null ? string.Empty : requestStatusItem["HistoryDescriptionEn"].ToString(),
                        HistoryDescriptionAr = requestStatusItem["HistoryDescriptionAr"] == null ? string.Empty : requestStatusItem["HistoryDescriptionAr"].ToString(),
                        FinalDecisionEn = requestStatusItem["FinalDecisionEn"] == null ? string.Empty : requestStatusItem["FinalDecisionEn"].ToString(),
                        FinalDecisionAr = requestStatusItem["FinalDecisionAr"] == null ? string.Empty : requestStatusItem["FinalDecisionAr"].ToString(),
                        CanReviewerEditRequest = requestStatusItem["CanReviewerEditRequest"] == null ? false : Convert.ToBoolean(requestStatusItem["CanReviewerEditRequest"]),
                        CanApplicantEditRequest = requestStatusItem["CanApplicantEditRequest"] == null ? false : Convert.ToBoolean(requestStatusItem["CanApplicantEditRequest"]),
                        CanDeleteRequest = requestStatusItem["CanDeleteRequest"] == null ? false : Convert.ToBoolean(requestStatusItem["CanDeleteRequest"]),
                        ApplicantRequestPhaseEn = requestStatusItem["ApplicantRequestPhaseEn"] == null ? string.Empty : requestStatusItem["ApplicantRequestPhaseEn"].ToString(),
                        ApplicantRequestPhaseAr = requestStatusItem["ApplicantRequestPhaseAr"] == null ? string.Empty : requestStatusItem["ApplicantRequestPhaseAr"].ToString(),
                        ApplicantDescriptionEn = requestStatusItem["ApplicantDescriptionEn"] == null ? string.Empty : requestStatusItem["ApplicantDescriptionEn"].ToString(),
                        ApplicantDescriptionAr = requestStatusItem["ApplicantDescriptionAr"] == null ? string.Empty : requestStatusItem["ApplicantDescriptionAr"].ToString(),
                        TargetPageUrl = requestStatusItem["TargetPageUrl"] == null ? string.Empty : requestStatusItem["TargetPageUrl"].ToString(),
                        ReviewerTargetPageURL = requestStatusItem["ReviewerTargetPageURL"] == null ? string.Empty : requestStatusItem["ReviewerTargetPageURL"].ToString(),
                        ReviewerDescriptionEn = requestStatusItem["ReviewerDescriptionEn"] == null ? string.Empty : requestStatusItem["ReviewerDescriptionEn"].ToString(),
                        ReviewerDescriptionAr = requestStatusItem["ReviewerDescriptionAr"] == null ? string.Empty : requestStatusItem["ReviewerDescriptionAr"].ToString()

                    };

                }


            }
            catch (Exception ex)
            {
                Logging.GetInstance().LogException(ex);
            }
            finally
            {

                Logging.GetInstance().Debug("Exiting method RequestStatus.GetRequestStatusById");
            }
            return requestStatus;
        }

        public static bool IsRequestEditable(int requestNumber)

        {
            Logging.GetInstance().Debug("Entering method RequestStatus.IsRequestEditable");
            bool canEditRequest = false;
            try
            {
                SPSecurity.RunWithElevatedPrivileges(delegate ()
                {
                    using (SPSite site = new SPSite(SPContext.Current.Site.Url))
                    {
                        using (SPWeb web = site.OpenWeb())
                        {
                            SPList requestStatusList = web.Lists[Constants.RequestStatus];

                            if (requestStatusList == null)
                                throw new Exception();

                            //SPListItem requestStatusItem = requestStatusList.GetItemById(requestStatusId);
                            int requestStatusId = Request.GetRequesStaustByRequestNumber(requestNumber);
                            if (requestStatusId > 0)
                            {
                                SPListItem requestStausItem = GetRequestStatusItem(requestStatusId);
                                if (requestStausItem != null)
                                {
                                    canEditRequest = Convert.ToBoolean(requestStausItem["CanApplicantEditRequest"]);
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

                Logging.GetInstance().Debug("Exiting method RequestStatus.IsRequestEditable");
            }
            return canEditRequest;
        }

        public static List<Entities.RequestStatus> GetDistinctStatusToApplicant()
        {
            Logging.GetInstance().Debug("Entering method RequestStatus.GetDistinctStatus");
            List<Entities.RequestStatus> requestStatus = GetAll();
            try
            {
                requestStatus = requestStatus.GroupBy(r => r.ApplicantDescriptionEn).Select(r => r.FirstOrDefault()).ToList();

                requestStatus = requestStatus.FindAll(fl => fl.Code == Utilities.RequestStatus.UCEDraft.ToString()
                || fl.Code == Utilities.RequestStatus.UCESubmitted.ToString()
                || fl.Code == Utilities.RequestStatus.UCEReceptionistReviewInformation.ToString()
                || fl.Code == Utilities.RequestStatus.UCEReceptionistNeedsClarification.ToString()
                || fl.Code == Utilities.RequestStatus.UCEClosedByRejection.ToString()
                || fl.Code == Utilities.RequestStatus.UCEClosedByAcceptance.ToString()
                || fl.Code == Utilities.RequestStatus.UCEHeadManagerAccepted.ToString()
                || fl.Code == Utilities.RequestStatus.UCECulturalMissionNeedsStatement.ToString()
               );


            }
            catch (Exception ex)
            {
                Logging.GetInstance().LogException(ex);
            }
            finally
            {

                Logging.GetInstance().Debug("Exiting method RequestStatus.GetDistinctStatus");
            }

            return requestStatus;
        }

        public static List<Entities.RequestStatus> GetStatusFinalDecisionToApplicant()
        {
            Logging.GetInstance().Debug("Entering method RequestStatus.GetStatusFinalDecision");
            List<Entities.RequestStatus> requestStatus = GetAll();
            try
            {
                requestStatus = requestStatus.FindAll(s => !string.IsNullOrEmpty(s.FinalDecisionEn) && !string.IsNullOrEmpty(s.FinalDecisionAr));

                requestStatus = requestStatus.GroupBy(r => r.FinalDecisionEn).Select(r => r.FirstOrDefault()).ToList();

                requestStatus = requestStatus.FindAll(fl => fl.Code != Utilities.RequestStatus.UCEExternalCommunicationAddBook.ToString());
            }
            catch (Exception ex)
            {
                Logging.GetInstance().LogException(ex);
            }
            finally
            {

                Logging.GetInstance().Debug("Exiting method RequestStatus.GetStatusFinalDecision");
            }

            return requestStatus;
        }
    }
}
