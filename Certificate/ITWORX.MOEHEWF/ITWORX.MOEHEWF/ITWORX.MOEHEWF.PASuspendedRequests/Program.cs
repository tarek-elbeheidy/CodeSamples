using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.SharePoint;
using System;
using ITWORX.MOEHEWF.PASuspendedRequests.Utilities;
using ITWORX.MOEHE.Utilities;
using ITWORX.MOEHE.Utilities.Logging;
using ITWORX.MOEHEWF.Common.Utilities;

namespace ITWORX.MOEHEWF.PASuspendedRequests
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                PASuspendedRequestsLogger.GetInstance().Debug("Entering Method Program.Main , Namespace ITWORX.MOEHEWF.SuspendedRequests");
                using (SPSite site = new SPSite(Helper.GetConfigValue("siteURL")))
                {
                    string DelayedApplicantReplyDuration = HelperMethods.GetConfigurationValue(site.Url, Helper.GetConfigValue("ConfigurationList"), Helper.GetConfigValue("PADelayedApplicantReply"));
                    string ExternalCommunicationsSuspend = HelperMethods.GetConfigurationValue(site.Url, Helper.GetConfigValue("ConfigurationList"), Helper.GetConfigValue("PAExternalCommunicationsSuspend"));
                    string CulturalMissionSuspend = HelperMethods.GetConfigurationValue(site.Url, Helper.GetConfigValue("ConfigurationList"), Helper.GetConfigValue("PACulturalMissionSuspend"));
                    using (SPWeb web = site.OpenWeb())
                    {
                        SPList requestsList = web.Lists[Helper.GetConfigValue("RequestsList")];
                        _suspendDelayedApplicantReply(requestsList, DelayedApplicantReplyDuration);
                        _culturalMissionSuspend(requestsList, CulturalMissionSuspend);
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
                PASuspendedRequestsLogger.GetInstance().Debug("Exit Method Program.Main, Namespace ITWORX.MOEHEWF.SuspendedRequests");
            }
        }


        static void _suspendDelayedApplicantReply(SPList requestsList, string duration)
        {
            try
            {
                PASuspendedRequestsLogger.GetInstance().Debug("Entering Method Program._suspendDelayedApplicantReply, Namespace ITWORX.MOEHEWF.SuspendedRequests");
                if (requestsList != null)
                {
                    var q = new SPQuery()
                    {
                        Query = @"<Where><Or><Eq><FieldRef Name='RequestStatus' /><Value Type='Lookup'>" + RequestStatus.PAEmployeeReviewInformation + "</Value></Eq>" +
                                 "<Eq><FieldRef Name='RequestStatus' /><Value Type='Lookup'>" + RequestStatus.PAProgramEmployeeMissingInformation + " </Value></Eq></Or></Where>"
                    };
                    var items = requestsList.GetItems(q);
                    foreach (SPListItem item in items)
                    {
                        if ((DateTime.Now - Convert.ToDateTime(item["ActionDate"])).TotalDays >= Convert.ToInt32(duration))
                        {
                            item["RequestStatus"] = new SPFieldLookupValue(Convert.ToInt32(RequestStatus.PAEmployeeClarificationReplay).ToString());
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
                PASuspendedRequestsLogger.GetInstance().Debug("Exit Method Program._suspendDelayedApplicantReply, Namespace ITWORX.MOEHEWF.SuspendedRequests");
            }
        }
        static void _culturalMissionSuspend(SPList requestsList, string duration)
        {
            try
            {
                PASuspendedRequestsLogger.GetInstance().Debug("Entering Method Program._culturalMissionSuspend, Namespace ITWORX.MOEHEWF.SuspendedRequests");
                if (requestsList != null)
                {
                    var q = new SPQuery()
                    {
                        Query = @"<Where><Eq><FieldRef Name='RequestStatus' /><Value Type='Lookup'>" + RequestStatus.PACulturalMissionNeedsStatement + " </Value></Eq></Where>"
                    };
                    var items = requestsList.GetItems(q);
                    foreach (SPListItem item in items)
                    {
                        if ((DateTime.Now - Convert.ToDateTime(item["ActionDate"])).TotalDays >= Convert.ToInt32(duration))
                        {
                            item["RequestStatus"] = new SPFieldLookupValue(Convert.ToInt32(RequestStatus.PACulturalMissionSuspend).ToString());
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
                PASuspendedRequestsLogger.GetInstance().Debug("Exit Method Program._culturalMissionSuspend, Namespace ITWORX.MOEHEWF.SuspendedRequests");
            }
        }
        static void _externalCommunicationSuspend(SPList requestsList, string duration)
        {
            try
            {
                PASuspendedRequestsLogger.GetInstance().Debug("Entering Method Program._externalCommunicationSuspend, Namespace ITWORX.MOEHEWF.SuspendedRequests");
                if (requestsList != null)
                {
                    var q = new SPQuery()
                    {
                        Query = @"<Where><Eq><FieldRef Name='RequestStatus' /><Value Type='Lookup'>" + RequestStatus.PAExternalCommunicationAddBook + " </Value></Eq></Where>"
                    };
                    var items = requestsList.GetItems(q);
                    foreach (SPListItem item in items)
                    {
                        if ((DateTime.Now - Convert.ToDateTime(item["ActionDate"])).TotalDays >= Convert.ToInt32(duration))
                        {
                            item["RequestStatus"] = new SPFieldLookupValue(Convert.ToInt32(RequestStatus.PAExternalCommunicationSuspend).ToString());
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
                PASuspendedRequestsLogger.GetInstance().Debug("Exit Method Program._externalCommunicationSuspend, Namespace ITWORX.MOEHEWF.SuspendedRequests");
            }
        }
    }
}
