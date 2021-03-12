using Microsoft.SharePoint;
using System;
using ITWORX.MOEHEWF.SuspendedRequests.Utilities; 
using ITWORX.MOEHE.Utilities;
using ITWORX.MOEHE.Utilities.Logging;
using ITWORX.MOEHEWF.Common.Utilities;

namespace ITWORX.MOEHEWF.SuspendedRequests
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                SuspendedRequestsLogger.GetInstance().Debug("Entering Method Program.Main , Namespace ITWORX.MOEHEWF.SuspendedRequests");
                using (SPSite site = new SPSite(Helper.GetConfigValue("siteURL")))
                {
                    string DelayedApplicantReplyDuration = HelperMethods.GetConfigurationValue(site.Url, Helper.GetConfigValue("ConfigurationList"), Helper.GetConfigValue("DelayedApplicantReply"));
                    string ExternalCommunicationsSuspend = HelperMethods.GetConfigurationValue(site.Url, Helper.GetConfigValue("ConfigurationList"), Helper.GetConfigValue("ExternalCommunicationsSuspend"));
                    string CulturalMissionSuspend = HelperMethods.GetConfigurationValue(site.Url, Helper.GetConfigValue("ConfigurationList"), Helper.GetConfigValue("CulturalMissionSuspend"));
                    using (SPWeb web = site.OpenWeb())
                    {
                        SPList requestsList = web.Lists[Helper.GetConfigValue("RequestsList")];
                        _suspendDelayedApplicantReply(requestsList, DelayedApplicantReplyDuration);
                        _culturalMissionSuspend(requestsList,CulturalMissionSuspend);
                        _externalCommunicationSuspend(requestsList, ExternalCommunicationsSuspend);
                    }
                }
            }
            catch (Exception ex)
            {
                Logging.GetInstance().LogException(ex);
            }
            finally
            {
                SuspendedRequestsLogger.GetInstance().Debug("Exit Method Program.Main, Namespace ITWORX.MOEHEWF.SuspendedRequests");
            }
        }


        static void _suspendDelayedApplicantReply(SPList requestsList, string duration)
        {
            try
            {
                SuspendedRequestsLogger.GetInstance().Debug("Entering Method Program._suspendDelayedApplicantReply, Namespace ITWORX.MOEHEWF.SuspendedRequests");
                if (requestsList != null)
                {
                    var q = Common.Utilities.BusinessHelper.GetQueryObject("<Where><Or><Eq><FieldRef Name='RequestStatus' /><Value Type='Lookup'>" + RequestStatus.UCEProgramEmployeeReview + "</Value></Eq>" +
                                 "<Eq><FieldRef Name='RequestStatus' /><Value Type='Lookup'>" + RequestStatus.UCEAsianAndEuropianEmployeeMissingInformation + " </Value></Eq></Or></Where>");

                    var items = requestsList.GetItems(q);
                    foreach (SPListItem item in items)
                    {
                        if ((DateTime.Now - Convert.ToDateTime(item["ActionDate"])).TotalDays >= Convert.ToInt32(duration))
                        {
                            item["RequestStatus"] = new SPFieldLookupValue(Convert.ToInt32(RequestStatus.UCEApplicantClarificationReply).ToString());
                            item.Update();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Logging.GetInstance().LogException(ex);
            }
            finally
            {
                SuspendedRequestsLogger.GetInstance().Debug("Exit Method Program._suspendDelayedApplicantReply, Namespace ITWORX.MOEHEWF.SuspendedRequests");
            }
        }
        static void _culturalMissionSuspend(SPList requestsList, string duration)
        {
            try
            {
                SuspendedRequestsLogger.GetInstance().Debug("Entering Method Program._culturalMissionSuspend, Namespace ITWORX.MOEHEWF.SuspendedRequests");
                if (requestsList != null)
                {
                    var q = Common.Utilities.BusinessHelper.GetQueryObject("<Where><Eq><FieldRef Name='RequestStatus' /><Value Type='Lookup'>" + RequestStatus.UCECulturalMissionNeedsStatement + " </Value></Eq></Where>");

                    var items = requestsList.GetItems(q);
                    foreach (SPListItem item in items)
                    {
                        if ((DateTime.Now - Convert.ToDateTime(item["ActionDate"])).TotalDays >= Convert.ToInt32(duration))
                        {
                            item["RequestStatus"] = new SPFieldLookupValue(Convert.ToInt32(RequestStatus.UCECulturalMissionSuspend).ToString());
                            item.Update();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Logging.GetInstance().LogException(ex);
            }
            finally
            {
                SuspendedRequestsLogger.GetInstance().Debug("Exit Method Program._culturalMissionSuspend, Namespace ITWORX.MOEHEWF.SuspendedRequests");
            }
        }
        static void _externalCommunicationSuspend(SPList requestsList, string duration)
        {
            try
            {
                SuspendedRequestsLogger.GetInstance().Debug("Entering Method Program._externalCommunicationSuspend, Namespace ITWORX.MOEHEWF.SuspendedRequests");
                if (requestsList != null)
                {
                    var q = Common.Utilities.BusinessHelper.GetQueryObject("<Where><Eq><FieldRef Name='RequestStatus' /><Value Type='Lookup'>" + RequestStatus.UCEExternalCommunicationAddBook + " </Value></Eq></Where>");

                    var items = requestsList.GetItems(q);
                    foreach (SPListItem item in items)
                    {
                        if ((DateTime.Now - Convert.ToDateTime(item["ActionDate"])).TotalDays >= Convert.ToInt32(duration))
                        {
                            item["RequestStatus"] = new SPFieldLookupValue(Convert.ToInt32(RequestStatus.UCEExternalCommunicationSuspend).ToString());
                            item.Update();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Logging.GetInstance().LogException(ex);
            }
            finally
            {
                SuspendedRequestsLogger.GetInstance().Debug("Exit Method Program._externalCommunicationSuspend, Namespace ITWORX.MOEHEWF.SuspendedRequests");
            }
        }
    }
}
