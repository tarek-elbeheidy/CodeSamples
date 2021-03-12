using ITWORX.MOEHE.Utilities.Logging;
using Microsoft.SharePoint;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ITWORX.MOEHEWF.Common.BL
{
   public static class Procedure
    {
        public static List<Entities.Proc> GetApprovedRecommendationStatus(string reqID, string status)
        {
            List<Entities.Proc> Proc = new List<Entities.Proc>();
            SPSecurity.RunWithElevatedPrivileges(delegate ()
            {
                using (SPSite site = new SPSite(SPContext.Current.Site.Url))
                {
                    using (SPWeb web = site.OpenWeb())
                    {
                        try
                        {
                            Logging.GetInstance().Debug("Entering method AllProcedures.GetSavedRecommendationStatusProc");
                            SPList list = web.Lists["EquationOfficerProcedures"];
                            var q = Common.Utilities.BusinessHelper.GetQueryObject("<Where><And><Eq><FieldRef Name='RequestID' /><Value Type='Lookup'>" +
                                reqID + "</Value></Eq><Eq><FieldRef Name='RecommendationStatus' /><Value Type='Text'>" +
                                status + "</Value></Eq></And></Where><OrderBy><FieldRef Name='ProcedureDate' Ascending='False' /></OrderBy>");

                            SPListItemCollection collListItems = list.GetItems(q);
                            if (collListItems.Count != 0)
                                foreach (SPListItem item in collListItems)
                                {
                                    SPFieldLookupValue RequestID = new SPFieldLookupValue((item["RequestID"] != null) ? item["RequestID"].ToString() : string.Empty);
                                    Proc.Add(new Entities.Proc()
                                    {
                                        Procedure = item["Procedure"] == null ? string.Empty : item["Procedure"].ToString(),
                                        ID = item["ID"] == null ? string.Empty : item["ID"].ToString(),
                                        DecisionForPrint = item["DecisionForPrint"] == null ? string.Empty : item["DecisionForPrint"].ToString(),
                                        Opinion = item["InitialOpinion"] == null ? string.Empty : item["InitialOpinion"].ToString(),
                                        RequestID = RequestID.LookupValue,
                                        OccupationName= item["OccupationName"] == null ? string.Empty : item["OccupationName"].ToString(),
                                        HeadManagerName = item["HeadManagerName"] == null ? string.Empty : item["HeadManagerName"].ToString(),
                                        BookDate = item["bookDate"] == null ? string.Empty : item["bookDate"].ToString(),
                                        BookNum = item["bookNum"] == null ? string.Empty : item["bookNum"].ToString(),
                                        SirValue = item["SirValue"] == null ? string.Empty : item["SirValue"].ToString(),
                                        RespectedValue = item["RespectedValue"] == null ? string.Empty : item["RespectedValue"].ToString()
                                    });

                                }
                        }
                        catch (Exception ex)
                        {
                            Logging.GetInstance().LogException(ex);
                            //handle exception as the business require.
                        }
                        finally
                        {
                            //dispose any objects that require dispose.
                            Logging.GetInstance().Debug("Exiting method AllProcedures.GetSavedRecommendationStatusProc");
                        }
                    }
                }
            });
            return Proc;
        }
        public static List<Entities.Proc> GetPAApprovedRecommendationStatus(string reqID, string status)
        {
            List<Entities.Proc> Proc = new List<Entities.Proc>();
            SPSecurity.RunWithElevatedPrivileges(delegate ()
            {
                using (SPSite site = new SPSite(SPContext.Current.Site.Url))
                {
                    using (SPWeb web = site.OpenWeb())
                    {
                        try
                        {
                            Logging.GetInstance().Debug("Entering method AllProcedures.GetPAApprovedRecommendationStatus");
                            SPList list = web.Lists["PAEquationOfficerProcedures"];
                            var q = Common.Utilities.BusinessHelper.GetQueryObject("<Where><And><Eq><FieldRef Name='RequestID' /><Value Type='Lookup'>" +
                                reqID + "</Value></Eq><Eq><FieldRef Name='PARecommendationstatus' /><Value Type='Text'>" +
                                status + "</Value></Eq></And></Where><OrderBy><FieldRef Name='ProcedureDate' Ascending='False' /></OrderBy>");

                            SPListItemCollection collListItems = list.GetItems(q);
                            if (collListItems.Count != 0)
                                foreach (SPListItem item in collListItems)
                                {
                                    SPFieldLookupValue RequestID = new SPFieldLookupValue((item["RequestID"] != null) ? item["RequestID"].ToString() : string.Empty);
                                    Proc.Add(new Entities.Proc()
                                    {
                                        Procedure = item["Procedure"] == null ? string.Empty : item["Procedure"].ToString(),
                                        ID = item["ID"] == null ? string.Empty : item["ID"].ToString(),
                                        DecisionForPrint = item["DecisionForPrint"] == null ? string.Empty : item["DecisionForPrint"].ToString(),
                                        Opinion = item["InitialOpinion"] == null ? string.Empty : item["InitialOpinion"].ToString(),
                                        RequestID = RequestID.LookupValue,
                                    });

                                }
                        }
                        catch (Exception ex)
                        {
                            Logging.GetInstance().LogException(ex);
                            //handle exception as the business require.
                        }
                        finally
                        {
                            //dispose any objects that require dispose.
                            Logging.GetInstance().Debug("Exiting method AllProcedures.GetPAApprovedRecommendationStatus");
                        }
                    }
                }
            });
            return Proc;
        }
    }
}
