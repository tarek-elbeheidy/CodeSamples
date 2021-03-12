using ITWORX.MOEHE.Utilities.Logging;
using ITWORX.MOEHEWF.PA.Entities;
using Microsoft.SharePoint;
using Microsoft.SharePoint.Utilities;
using System;
using System.Collections.Generic;

namespace ITWORX.MOEHEWF.PA.BL
{
    public class AllProcedures
    {
        //Receptionist Procedures
        public static List<Procedures> GetProceduresbyReqID(string lstName, string reqID, bool hasProcedure = true)
        {
            List<Procedures> Proc = new List<Procedures>();
            SPSecurity.RunWithElevatedPrivileges(delegate ()
            {
                using (SPSite site = new SPSite(SPContext.Current.Site.Url))
                {
                    using (SPWeb web = site.OpenWeb())
                    {
                        try
                        {
                            Logging.GetInstance().Debug("Entering method AllProcedures.GetProcedurebyRequestID");
                            SPList list = web.Lists[lstName];
                            var q = Common.Utilities.BusinessHelper.GetQueryObject("<Where><Eq><FieldRef Name='RequestID' /><Value Type='Lookup'>" + reqID + "</Value></Eq></Where><OrderBy><FieldRef Name='ProcedureDate' Ascending='False' /></OrderBy>");

                            SPListItemCollection collListItems = list.GetItems(q);
                            if (collListItems.Count != 0)
                                foreach (SPListItem item in collListItems)
                                {
                                    SPFieldLookupValue RequestID = new SPFieldLookupValue((item["RequestID"] != null) ? item["RequestID"].ToString() : string.Empty);
                                    Proc.Add(new Procedures()
                                    {
                                        RequestID = RequestID.LookupValue,
                                        ID = item["ID"] == null ? string.Empty : item["ID"].ToString(),
                                        ProcedureDate = item["ProcedureDate"] == null ? DateTime.MinValue.ToString() : Convert.ToDateTime(item["ProcedureDate"].ToString()).ToShortDateString(),
                                        ProcedureCreatedby = item["ProcedureCreatedBy"] == null ? string.Empty : item["ProcedureCreatedBy"].ToString(),
                                        ProcedureComments = item["ProcedureComments"] == null ? string.Empty : item["ProcedureComments"].ToString(),
                                    });
                                }
                        }
                        catch (Exception ex)
                        {
                            Logging.GetInstance().LogException(ex);
                        }
                        finally
                        {
                            Logging.GetInstance().Debug("Exiting method AllProcedures.GetProcedurebyRequestID");
                        }
                    }
                }
            });
            return Proc;
        }

        public static Procedures GetProcedureReceptionistbyID(string procID)
        {
            Procedures Proc = new Procedures();
            SPSecurity.RunWithElevatedPrivileges(delegate ()
            {
                using (SPSite site = new SPSite(SPContext.Current.Site.Url))
                {
                    using (SPWeb web = site.OpenWeb())
                    {
                        try
                        {
                            Logging.GetInstance().Debug("Entering method AllProcedures.GetProcedurebyID");
                            SPList list = web.Lists[Utilities.Constants.PAReceptionProcedures];
                            SPListItem item = list.GetItemById(Convert.ToInt32(procID));
                            if (item != null)
                            {
                                SPFieldLookupValue ApplicantsQatarID = new SPFieldLookupValue((item["RequestID"] != null) ? item["RequestID"].ToString() : string.Empty);
                                Proc.RequestID = ApplicantsQatarID.LookupValue;
                                Proc.ProcedureDate = item["ProcedureDate"] == null ? DateTime.MinValue.ToString() : Convert.ToDateTime(item["ProcedureDate"].ToString()).ToShortDateString();
                                Proc.ProcedureCreatedby = item["ProcedureCreatedBy"] == null ? string.Empty : item["ProcedureCreatedBy"].ToString();
                                Proc.ProcedureComments = item["ProcedureComments"] == null ? string.Empty : item["ProcedureComments"].ToString();
                            }
                        }
                        catch (Exception ex)
                        {
                            Logging.GetInstance().LogException(ex);
                        }
                        finally
                        {
                            Logging.GetInstance().Debug("Exiting method AllProcedures.GetProcedurebyID");
                        }
                    }
                }
            });
            return Proc;
        }

        public static void AddProcedureReceptionist(string Comments, string ReqID)
        {
            SPSecurity.RunWithElevatedPrivileges(delegate ()
            {
                using (SPSite site = new SPSite(SPContext.Current.Site.Url))
                {
                    using (SPWeb web = site.OpenWeb())
                    {
                        try
                        {
                            SPList list = web.Lists[Utilities.Constants.PAReceptionProcedures];
                            SPListItem newitem = list.Items.Add();
                            newitem["ProcedureCreatedBy"] = SPContext.Current.Web.CurrentUser.Name;
                            newitem["ProcedureComments"] = SPHttpUtility.HtmlEncode(Comments);
                            newitem["RequestID"] = new SPFieldLookupValue(Convert.ToInt32(ReqID), ReqID);
                            newitem["ProcedureDate"] = DateTime.Now;
                            web.AllowUnsafeUpdates = true;
                            newitem.Update();
                            list.Update();
                            Logging.GetInstance().Debug("Entering method AllProcedures.AddProcedureReceptionist");
                        }
                        catch (Exception ex)
                        {
                            Logging.GetInstance().LogException(ex);
                        }
                        finally
                        {
                            web.AllowUnsafeUpdates = false;
                            Logging.GetInstance().Debug("Exiting method AllProcedures.AddProcedureReceptionist");
                        }
                    }
                }
            });
        }

        //Legal Affairs Initial Opinion
        public static void AddLegalAffairsProcedureOpinion(string Opinion, string ReqID)
        {
            SPSecurity.RunWithElevatedPrivileges(delegate ()
            {
                using (SPSite site = new SPSite(SPContext.Current.Site.Url))
                {
                    using (SPWeb web = site.OpenWeb())
                    {
                        try
                        {
                            Logging.GetInstance().Debug("Entering method AllProcedures.AddLegalOrEquationOfficerProcedure");
                            SPList list = web.Lists[Utilities.Constants.PALegalOfficerProcedures];
                            SPListItem newitem = list.Items.Add();
                            newitem["ProcedureCreatedBy"] = SPContext.Current.Web.CurrentUser.Name;
                            newitem["InitialOpinion"] = SPHttpUtility.HtmlEncode(Opinion);
                            newitem["RequestID"] = new SPFieldLookupValue(Convert.ToInt32(ReqID), ReqID);
                            newitem["ProcedureDate"] = DateTime.Now;
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
                            Logging.GetInstance().Debug("Exiting method AllProcedures.AddLegalOrEquationOfficerProcedure");
                        }
                    }
                }
            });
        }

        public static List<Entities.Procedures> GetLegalAffairsOpinionbyReqID(string ReqID)
        {
            List<Entities.Procedures> Proc = new List<Entities.Procedures>();
            SPSecurity.RunWithElevatedPrivileges(delegate ()
            {
                using (SPSite site = new SPSite(SPContext.Current.Site.Url))
                {
                    using (SPWeb web = site.OpenWeb())
                    {
                        try
                        {
                            Logging.GetInstance().Debug("Entering method AllProcedures.GetLegalAffairsOpinionbyReqID");
                            SPList list = web.Lists[Utilities.Constants.PALegalOfficerProcedures];
                            var q = Common.Utilities.BusinessHelper.GetQueryObject("<Where><Eq><FieldRef Name='RequestID' /><Value Type='Lookup'>" + ReqID + "</Value></Eq></Where><OrderBy><FieldRef Name='ProcedureDate' Ascending='False' /></OrderBy>");

                            q.RowLimit = 1;
                            SPListItemCollection collListItems = list.GetItems(q);
                            if (collListItems.Count != 0)
                                foreach (SPListItem item in collListItems)
                                {
                                    SPFieldLookupValue RequestID = new SPFieldLookupValue((item["RequestID"] != null) ? item["RequestID"].ToString() : string.Empty);
                                    Proc.Add(new Entities.Procedures()
                                    {
                                        //RecommendationDate = item["RecommendationDate"] == null ? DateTime.MinValue.ToString() : Convert.ToDateTime(item["RecommendationDate"].ToString()).ToShortDateString(),
                                        Opinion = item["InitialOpinion"] == null ? string.Empty : item["InitialOpinion"].ToString(),
                                        RequestID = RequestID.LookupValue,
                                        //Recommendation = item["Recommendation"] == null ? string.Empty : item["Recommendation"].ToString(),
                                    });
                                }
                        }
                        catch (Exception ex)
                        {
                            Logging.GetInstance().LogException(ex);
                        }
                        finally
                        {
                            Logging.GetInstance().Debug("Exiting method AllProcedures.GetLegalAffairsOpinionbyReqID");
                        }
                    }
                }
            });
            return Proc;
        }

        //Recommendation and Status
        public static void AddPARecommendationstatus(string Opinion, string ReqID, string Recommendation, string Status,string DecisionForPrint, string earnedHours, string onlineHours, string onlineHoursPer)
        {
         
            SPSecurity.RunWithElevatedPrivileges(delegate ()
            {
                using (SPSite site = new SPSite(SPContext.Current.Site.Url))
                {
                    using (SPWeb web = site.OpenWeb())
                    {
                        try
                        {
                            Logging.GetInstance().Debug("Entering method AllProcedures.AddPARecommendationstatus");
                            SPList list = web.Lists[Utilities.Constants.PAEquationOfficerProcedures];
                            SPListItem newitem = list.Items.Add();
                            newitem["ProcedureCreatedBy"] = SPContext.Current.Web.CurrentUser.Name;
                            newitem["InitialOpinion"] = SPHttpUtility.HtmlEncode(Opinion);
                            newitem["RequestID"] = new SPFieldLookupValue(Convert.ToInt32(ReqID), ReqID);
                            newitem["ProcedureDate"] = DateTime.Now;
                            newitem["Procedure"] = SPHttpUtility.HtmlEncode(Recommendation);
                            newitem["DecisionForPrint"] = SPHttpUtility.HtmlEncode(DecisionForPrint);
                            newitem["PARecommendationstatus"] = Status;
                            newitem["EarnedHours"] = earnedHours;
                            newitem["OnlineHours"] = onlineHours;
                            newitem["OnlineHoursPer"] = onlineHoursPer;
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
                            Logging.GetInstance().Debug("Exiting method AllProcedures.AddPARecommendationstatus");
                        }
                    }
                }
            });
          
        }

        public static List<Procedures> GetSavedPARecommendationstatusProc(string reqID, string userName)
        {
            List<Procedures> Proc = new List<Procedures>();
            SPSecurity.RunWithElevatedPrivileges(delegate ()
            {
                using (SPSite site = new SPSite(SPContext.Current.Site.Url))
                {
                    using (SPWeb web = site.OpenWeb())
                    {
                        try
                        {
                            Logging.GetInstance().Debug("Entering method AllProcedures.GetSavedPARecommendationstatusProc");
                            SPList list = web.Lists[Utilities.Constants.PAEquationOfficerProcedures];
                            var q = Common.Utilities.BusinessHelper.GetQueryObject("<Where><And><Eq><FieldRef Name='RequestID' /><Value Type='Lookup'>" + reqID + "</Value></Eq><Eq><FieldRef Name='ProcedureCreatedDate' /><Value Type='Text'>" + userName + "</Value></Eq></And></Where><OrderBy><FieldRef Name='ProcedureDate' Ascending='False' /></OrderBy>");

                            SPListItemCollection collListItems = list.GetItems(q);
                            if (collListItems.Count != 0)
                                foreach (SPListItem item in collListItems)
                                {
                                    SPFieldLookupValue RequestID = new SPFieldLookupValue((item["RequestID"] != null) ? item["RequestID"].ToString() : string.Empty);
                                    Proc.Add(new Procedures()
                                    {
                                        Procedure = item["Procedure"] == null ? string.Empty : item["Procedure"].ToString(),
                                        ID = item["ID"] == null ? string.Empty : item["ID"].ToString(),
                                        Opinion = item["InitialOpinion"] == null ? string.Empty : item["InitialOpinion"].ToString(),
                                        RequestID = RequestID.LookupValue,
                                        DecisionForPrint= item["DecisionForPrint"] == null ? string.Empty : item["DecisionForPrint"].ToString(),
                                        PARecommendationstatus = item["PARecommendationstatus"] == null ? string.Empty : item["PARecommendationstatus"].ToString(),
                                        EarnedHours = item["EarnedHours"] == null ? string.Empty : item["EarnedHours"].ToString(),
                                        OnlineHours = item["OnlineHours"] == null ? string.Empty : item["OnlineHours"].ToString(),
                                        OnlineHoursPer = item["OnlineHoursPer"] == null ? string.Empty : item["OnlineHoursPer"].ToString(),
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
                            Logging.GetInstance().Debug("Exiting method AllProcedures.GetSavedPARecommendationstatusProc");
                        }
                    }
                }
            });
            return Proc;
        }

        public static List<Procedures> GetApprovedPARecommendationstatus(string reqID, string status)
        {
            List<Procedures> Proc = new List<Procedures>();
            SPSecurity.RunWithElevatedPrivileges(delegate ()
            {
                using (SPSite site = new SPSite(SPContext.Current.Site.Url))
                {
                    using (SPWeb web = site.OpenWeb())
                    {
                        try
                        {
                            Logging.GetInstance().Debug("Entering method AllProcedures.GetSavedPARecommendationstatusProc");
                            SPList list = web.Lists[Utilities.Constants.PAEquationOfficerProcedures];
                            var q = Common.Utilities.BusinessHelper.GetQueryObject("<Where><And><Eq><FieldRef Name='RequestID' /><Value Type='Lookup'>" + reqID + "</Value></Eq><Eq><FieldRef Name='PARecommendationstatus' /><Value Type='Text'>" + status + "</Value></Eq></And></Where><OrderBy><FieldRef Name='ProcedureDate' Ascending='False' /></OrderBy>");

                            SPListItemCollection collListItems = list.GetItems(q);
                            if (collListItems.Count != 0)
                                foreach (SPListItem item in collListItems)
                                {
                                    SPFieldLookupValue RequestID = new SPFieldLookupValue((item["RequestID"] != null) ? item["RequestID"].ToString() : string.Empty);
                                    Proc.Add(new Procedures()
                                    {
                                        Procedure = item["Procedure"] == null ? string.Empty : item["Procedure"].ToString(),
                                        ID = item["ID"] == null ? string.Empty : item["ID"].ToString(),
                                        Opinion = item["InitialOpinion"] == null ? string.Empty : item["InitialOpinion"].ToString(),
                                        RequestID = RequestID.LookupValue,
                                        DecisionForPrint = item["DecisionForPrint"].ToString(),
                                        EarnedHours = item["EarnedHours"] == null ? string.Empty : item["EarnedHours"].ToString(),
                                        OnlineHours = item["OnlineHours"] == null ? string.Empty : item["OnlineHours"].ToString(),
                                        OnlineHoursPer = item["OnlineHoursPer"] == null ? string.Empty : item["OnlineHoursPer"].ToString(),
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
                            Logging.GetInstance().Debug("Exiting method AllProcedures.GetSavedPARecommendationstatusProc");
                        }
                    }
                }
            });
            return Proc;
        }

        public static void UpdatePARecommendationstatusProc(string id, string status, string Opinion, string Procedure, string DecisionforPrint,string earnedHours,string onlineHours,string onlineHoursPer)
        {
           
            SPSecurity.RunWithElevatedPrivileges(delegate ()
            {
                using (SPSite site = new SPSite(SPContext.Current.Site.Url))
                {
                    using (SPWeb web = site.OpenWeb())
                    {
                        try
                        {
                            Logging.GetInstance().Debug("Entering method AllProcedures.UpdateLegalOrEquationOfficerProc");
                            SPList list = web.Lists[Utilities.Constants.PAEquationOfficerProcedures];
                            SPListItem itemToUpdate = list.GetItemById(int.Parse(id));
                            itemToUpdate["PARecommendationstatus"] = status;
                            itemToUpdate["DecisionForPrint"] = SPHttpUtility.HtmlEncode(DecisionforPrint);
                            itemToUpdate["InitialOpinion"] = SPHttpUtility.HtmlEncode(Opinion);
                            itemToUpdate["Procedure"] = SPHttpUtility.HtmlEncode(Procedure);
                            itemToUpdate["EarnedHours"] = earnedHours;
                            itemToUpdate["OnlineHours"] = onlineHours;
                            itemToUpdate["OnlineHoursPer"] = onlineHoursPer;
                            web.AllowUnsafeUpdates = true;
                            itemToUpdate.Update();
                            list.Update();
                            
                        }
                        catch (Exception ex)
                        {
                            Logging.GetInstance().LogException(ex);
                        }
                        finally
                        {
                            web.AllowUnsafeUpdates = false;
                            Logging.GetInstance().Debug("Exiting method AllProcedures.UpdateLegalOrEquationOfficerProc");
                        }
                    }
                }
            });
           
        }

        //Program Manager Procedures
        public static void AddProgramManagerProcedure(string Comments, string ReqID, string Procedure, string EmpName)
        {
            SPSecurity.RunWithElevatedPrivileges(delegate ()
            {
                using (SPSite site = new SPSite(SPContext.Current.Site.Url))
                {
                    using (SPWeb web = site.OpenWeb())
                    {
                        try
                        {
                            Logging.GetInstance().Debug("Entering method AllProcedures.AddProsProgProcedure");
                            SPList list = web.Lists[Utilities.Constants.PAProgramManagerProcedures];
                            SPListItem newitem = list.Items.Add();
                            newitem["ProcedureCreatedBy"] = SPContext.Current.Web.CurrentUser.Name;
                            newitem["EmployeeAssignedTo"] = EmpName;
                            newitem["ProcedureComments"] = SPHttpUtility.HtmlEncode(Comments);
                            newitem["RequestID"] = new SPFieldLookupValue(Convert.ToInt32(ReqID), ReqID);
                            newitem["ProcedureDate"] = DateTime.Now;
                            newitem["Procedure"] = SPHttpUtility.HtmlEncode(Procedure);
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
                            Logging.GetInstance().Debug("Exiting method AllProcedures.AddProsProgProcedure");
                        }
                    }
                }
            });
        }

        public static Procedures GetProgramManagerProcedurebyID(string procID)
        {
            Procedures Proc = new Procedures();
            SPSecurity.RunWithElevatedPrivileges(delegate ()
            {
                using (SPSite site = new SPSite(SPContext.Current.Site.Url))
                {
                    using (SPWeb web = site.OpenWeb())
                    {
                        try
                        {
                            Logging.GetInstance().Debug("Entering method AllProcedures.GetProcedurebyID");
                            SPList list = web.Lists[Utilities.Constants.PAProgramManagerProcedures];
                            SPListItem item = list.GetItemById(Convert.ToInt32(procID));
                            if (item != null)
                            {
                                SPFieldLookupValue RequestID = new SPFieldLookupValue((item["RequestID"] != null) ? item["RequestID"].ToString() : string.Empty);
                                Proc.RequestID = RequestID.LookupValue;
                                Proc.ProcedureDate = item["ProcedureDate"] == null ? DateTime.MinValue.ToString() : Convert.ToDateTime(item["ProcedureDate"].ToString()).ToShortDateString();
                                Proc.ProcedureCreatedby = item["ProcedureCreatedBy"] == null ? string.Empty : item["ProcedureCreatedBy"].ToString();
                                Proc.ProcedureComments = item["ProcedureComments"] == null ? string.Empty : item["ProcedureComments"].ToString();
                                Proc.Procedure = item["Procedure"] == null ? string.Empty : item["Procedure"].ToString();
                                Proc.EmpAssignedTo = item["EmployeeAssignedTo"] == null ? string.Empty : item["EmployeeAssignedTo"].ToString();
                            }
                        }
                        catch (Exception ex)
                        {
                            Logging.GetInstance().LogException(ex);
                        }
                        finally
                        {
                            Logging.GetInstance().Debug("Exiting method AllProcedures.GetProcedurebyID");
                        }
                    }
                }
            });
            return Proc;
        }

        public static List<Procedures> GetProgManagerProceduresbyReqID(string lstName, string reqID, bool hasProcedure = true)
        {
            List<Procedures> Proc = new List<Procedures>();
            SPSecurity.RunWithElevatedPrivileges(delegate ()
            {
                using (SPSite site = new SPSite(SPContext.Current.Site.Url))
                {
                    using (SPWeb web = site.OpenWeb())
                    {
                        try
                        {
                            Logging.GetInstance().Debug("Entering method AllProcedures.GetProcedurebyRequestID");
                            SPList list = web.Lists[lstName];
                            var q = Common.Utilities.BusinessHelper.GetQueryObject("<Where><Eq><FieldRef Name='RequestID' /><Value Type='Lookup'>" + reqID + "</Value></Eq></Where><OrderBy><FieldRef Name='ProcedureDate' Ascending='False' /></OrderBy>");

                            SPListItemCollection collListItems = list.GetItems(q);
                            if (collListItems.Count != 0)
                                foreach (SPListItem item in collListItems)
                                {
                                    SPFieldLookupValue RequestID = new SPFieldLookupValue((item["RequestID"] != null) ? item["RequestID"].ToString() : string.Empty);
                                    Proc.Add(new Procedures()
                                    {
                                        RequestID = RequestID.LookupValue,
                                        ID = item["ID"] == null ? string.Empty : item["ID"].ToString(),
                                        ProcedureDate = item["ProcedureDate"] == null ? DateTime.MinValue.ToString() : Convert.ToDateTime(item["ProcedureDate"].ToString()).ToShortDateString(),
                                        ProcedureCreatedby = item["ProcedureCreatedBy"] == null ? string.Empty : item["ProcedureCreatedBy"].ToString(),
                                        ProcedureComments = item["ProcedureComments"] == null ? string.Empty : item["ProcedureComments"].ToString(),
                                        Procedure = item["Procedure"] == null ? string.Empty : item["Procedure"].ToString(),
                                    });
                                }
                        }
                        catch (Exception ex)
                        {
                            Logging.GetInstance().LogException(ex);
                        }
                        finally
                        {
                            Logging.GetInstance().Debug("Exiting method AllProcedures.GetProcedurebyRequestID");
                        }
                    }
                }
            });
            return Proc;
        }

        //Procedures(UnderSecretary,TechCommittee)
        public static List<Procedures> GetProcedurebyReqID(string listName, string reqID)
        {
            List<Procedures> Proc = new List<Procedures>();
            SPSecurity.RunWithElevatedPrivileges(delegate ()
            {
                using (SPSite site = new SPSite(SPContext.Current.Site.Url))
                {
                    using (SPWeb web = site.OpenWeb())
                    {
                        try
                        {
                            Logging.GetInstance().Debug("Entering method AllProcedures.GetProcedurebyReqID");
                            SPList list = web.Lists[listName];
                            var q = Common.Utilities.BusinessHelper.GetQueryObject("<Where><Eq><FieldRef Name='RequestID' /><Value Type='Lookup'>" + reqID + "</Value></Eq></Where><OrderBy><FieldRef Name='ProcedureDate' Ascending='False' /></OrderBy>");

                            SPListItemCollection collListItems = list.GetItems(q);
                            if (collListItems.Count != 0)
                                foreach (SPListItem item in collListItems)
                                {
                                    Proc.Add(new Procedures()
                                    {
                                        RequestID = item["RequestID"] == null ? string.Empty : item["RequestID"].ToString(),
                                        ID = item["ID"] == null ? string.Empty : item["ID"].ToString(),
                                        Procedure = item["Procedure"] == null ? string.Empty : item["Procedure"].ToString(),
                                        ProcedureDate = item["ProcedureDate"] == null ? DateTime.MinValue.ToString() : Convert.ToDateTime(item["ProcedureDate"].ToString()).ToShortDateString(),
                                        ProcedureCreatedby = item["ProcedureCreatedBy"] == null ? string.Empty : item["ProcedureCreatedBy"].ToString(),
                                        ProcedureComments = item["ProcedureComments"] == null ? string.Empty : item["ProcedureComments"].ToString(),
                                    });
                                }
                        }
                        catch (Exception ex)
                        {
                            Logging.GetInstance().LogException(ex);
                        }
                        finally
                        {
                            Logging.GetInstance().Debug("Exiting method AllProcedures.GetProcedurebyReqID");
                        }
                    }
                }
            });
            return Proc;
        }

        public static Procedures GetProcedurebyID(string listName, string procID)
        {
            Procedures Proc = new Procedures();
            SPSecurity.RunWithElevatedPrivileges(delegate ()
            {
                using (SPSite site = new SPSite(SPContext.Current.Site.Url))
                {
                    using (SPWeb web = site.OpenWeb())
                    {
                        try
                        {
                            Logging.GetInstance().Debug("Entering method GetProcedurebyID.GetProcedurebyID");
                            SPList list = web.Lists[listName];
                            SPListItem item = list.GetItemById(Convert.ToInt32(procID));
                            if (item != null)
                            {
                                SPFieldLookupValue ApplicantsQatarID = new SPFieldLookupValue((item["RequestID"] != null) ? item["RequestID"].ToString() : string.Empty);
                                Proc.RequestID = ApplicantsQatarID.LookupValue;
                                Proc.ProcedureDate = item["ProcedureDate"] == null ? DateTime.MinValue.ToString() : Convert.ToDateTime(item["ProcedureDate"].ToString()).ToShortDateString();
                                Proc.ProcedureCreatedby = item["ProcedureCreatedBy"] == null ? string.Empty : item["ProcedureCreatedBy"].ToString();
                                Proc.ProcedureComments = item["ProcedureComments"] == null ? string.Empty : item["ProcedureComments"].ToString();
                                Proc.Procedure = item["Procedure"] == null ? string.Empty : item["Procedure"].ToString();
                                Proc.RejectionReason = item["RejectionReason"] == null ? string.Empty : item["RejectionReason"].ToString();
                            }
                        }
                        catch (Exception ex)
                        {
                            Logging.GetInstance().LogException(ex);
                        }
                        finally
                        {
                            Logging.GetInstance().Debug("Exiting method GetProcedurebyID.GetProcedurebyID");
                        }
                    }
                }
            });
            return Proc;
        }

        public static void AddProcedure(string listName, string Comments, string ReqID, string Procedure, string RejectionReason)
        {
            SPSecurity.RunWithElevatedPrivileges(delegate ()
            {
                using (SPSite site = new SPSite(SPContext.Current.Site.Url))
                {
                    using (SPWeb web = site.OpenWeb())
                    {
                        try
                        {
                            SPList list = web.Lists[listName];
                            SPListItem newitem = list.Items.Add();
                            newitem["ProcedureCreatedBy"] = SPContext.Current.Web.CurrentUser.Name;
                            newitem["ProcedureComments"] = SPHttpUtility.HtmlEncode(Comments);
                            newitem["RequestID"] = new SPFieldLookupValue(Convert.ToInt32(ReqID), ReqID);
                            newitem["ProcedureDate"] = DateTime.Now;
                            if (!string.IsNullOrEmpty(Procedure))
                                newitem["Procedure"] = SPHttpUtility.HtmlEncode(Procedure);
                            if (!string.IsNullOrEmpty(RejectionReason))
                                newitem["RejectionReason"] = SPHttpUtility.HtmlEncode(RejectionReason);
                            web.AllowUnsafeUpdates = true;
                            newitem.Update();
                            list.Update();
                            Logging.GetInstance().Debug("Entering method AllProcedures.AddProcedure");
                        }
                        catch (Exception ex)
                        {
                            Logging.GetInstance().LogException(ex);
                        }
                        finally
                        {
                            web.AllowUnsafeUpdates = false;
                            Logging.GetInstance().Debug("Exiting method AllProcedures.AddProcedure");
                        }
                    }
                }
            });
        }
    }
}