using ITWORX.MOEHE.Utilities.Logging;
using ITWORX.MOEHEWF.UCE.Entities;
using Microsoft.SharePoint;
using Microsoft.SharePoint.Utilities;
using System;
using System.Collections.Generic;
using System.Globalization;

namespace ITWORX.MOEHEWF.UCE.BL
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

                            var q = Common.Utilities.BusinessHelper.GetQueryObject("<Where><Eq><FieldRef Name='RequestID' /><Value Type='Lookup'>" +
                                reqID + "</Value></Eq></Where><OrderBy><FieldRef Name='ProcedureDate' Ascending='False' /></OrderBy>");

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
                            SPList list = web.Lists[Utilities.Constants.ReceptionProcedures];
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
                            SPList list = web.Lists[Utilities.Constants.ReceptionProcedures];
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
                            SPList list = web.Lists[Utilities.Constants.LegalOfficerProcedures];
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
                            SPList list = web.Lists[Utilities.Constants.LegalOfficerProcedures];
                            var q = Common.Utilities.BusinessHelper.GetQueryObject("<Where><Eq><FieldRef Name='RequestID' /><Value Type='Lookup'>" +
                                ReqID + "</Value></Eq></Where><OrderBy><FieldRef Name='ProcedureDate' Ascending='False' /></OrderBy>");

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
        public static void AddRecommendationStatus(string Opinion, string ReqID, string Recommendation, string Status, string DecisionForPrint, string OccupationName, string NumberOfHoursGained, string NumberOfOnlineHours, string PercentageOfOnlineHours, string rblOwners, string HeHavePA, string rblUniversity,string bookNum,string bookDate,string HeadManagerName, bool hasException, string exceptionFrom, string sirValue, string respectedValue)
        {
            SPSecurity.RunWithElevatedPrivileges(delegate ()
            {
                using (SPSite site = new SPSite(SPContext.Current.Site.Url))
                {
                    using (SPWeb web = site.OpenWeb())
                    {
                        try
                        {
                            Logging.GetInstance().Debug("Entering method AllProcedures.AddRecommendationStatus");
                            SPList list = web.Lists[Utilities.Constants.EquationOfficerProcedures];
                            SPListItem newitem = list.Items.Add();
                            newitem["ProcedureCreatedBy"] = SPContext.Current.Web.CurrentUser.Name;
                            newitem["DecisionForPrint"] = SPHttpUtility.HtmlEncode(DecisionForPrint);
                            newitem["InitialOpinion"] = SPHttpUtility.HtmlEncode(Opinion);
                            newitem["RequestID"] = new SPFieldLookupValue(Convert.ToInt32(ReqID), ReqID);
                            newitem["ProcedureDate"] = DateTime.Now;
                            newitem["Procedure"] = SPHttpUtility.HtmlEncode(Recommendation);
                            newitem["RecommendationStatus"] = Status;
                            newitem["OccupationName"] = SPHttpUtility.HtmlEncode(OccupationName);
                            newitem["HeadManagerName"] = HeadManagerName;

                            if (!string.IsNullOrEmpty(NumberOfHoursGained))
                                newitem["NumberOfHoursGained"] = Convert.ToInt32(NumberOfHoursGained);
                            if (!string.IsNullOrEmpty(NumberOfOnlineHours))
                                newitem["NumberOfOnlineHours"] = Convert.ToInt32(NumberOfOnlineHours);

                            newitem["PercentageOfOnlineHours"] = PercentageOfOnlineHours;
                            newitem["OrdinaryOrOwners"] = rblOwners;
                            newitem["HavePA"] = HeHavePA;
                            newitem["TypeUniversity"] = rblUniversity;
                            newitem["bookNum"] = bookNum;
                            newitem["HasException"] = hasException;
                            newitem["ExceptionFrom"] = exceptionFrom;
                            newitem["SirValue"] = sirValue;
                            newitem["RespectedValue"] = respectedValue;


                            if (!string.IsNullOrEmpty(bookDate))
                            {
                                newitem["bookDate"] = SPUtility.CreateISO8601DateTimeFromSystemDateTime(DateTime.ParseExact(bookDate, "d/M/yyyy", CultureInfo.CurrentCulture));
                            }
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
                            Logging.GetInstance().Debug("Exiting method AllProcedures.AddRecommendationStatus");
                        }
                    }
                }
            });
        }

        public static List<Procedures> GetSavedRecommendationStatusProc(string reqID, string userName)
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
                            Logging.GetInstance().Debug("Entering method AllProcedures.GetSavedRecommendationStatusProc");
                            SPList list = web.Lists[Utilities.Constants.EquationOfficerProcedures];

                            var q = Common.Utilities.BusinessHelper.GetQueryObject("<Where><And><Eq><FieldRef Name='RequestID' /><Value Type='Lookup'>" +
                                reqID + "</Value></Eq><Eq><FieldRef Name='ProcedureCreatedDate' /><Value Type='Text'>" +
                                userName + "</Value></Eq></And></Where><OrderBy><FieldRef Name='ProcedureDate' Ascending='False' /></OrderBy>");

                            SPListItemCollection collListItems = list.GetItems(q);
                            if (collListItems.Count != 0)
                                foreach (SPListItem item in collListItems)
                                {
                                    SPFieldLookupValue RequestID = new SPFieldLookupValue((item["RequestID"] != null) ? item["RequestID"].ToString() : string.Empty);
                                    Proc.Add(new Procedures()
                                    {
                                        Procedure = item["Procedure"] == null ? string.Empty : item["Procedure"].ToString(),
                                        ID = item["ID"] == null ? string.Empty : item["ID"].ToString(),
                                        DecisionForPrint = item["DecisionForPrint"] == null ? string.Empty : item["DecisionForPrint"].ToString(),
                                        Opinion = item["InitialOpinion"] == null ? string.Empty : item["InitialOpinion"].ToString(),
                                        RequestID = RequestID.LookupValue,
                                        RecommendationStatus = item["RecommendationStatus"] == null ? string.Empty : item["RecommendationStatus"].ToString(),
                                        OccupationName = item["OccupationName"] == null ? string.Empty : item["OccupationName"].ToString(),
                                        HeadManagerName = item["HeadManagerName"] == null ? string.Empty : item["HeadManagerName"].ToString(),
                                        BookNum = item["bookNum"] == null ? string.Empty : item["bookNum"].ToString(),
                                        BookDate = item["bookDate"] == null ? string.Empty : item["bookDate"].ToString(),
                                        NumberOfHoursGained = item["NumberOfHoursGained"] == null ? string.Empty : item["NumberOfHoursGained"].ToString(),
                                        NumberOfOnlineHours = item["NumberOfOnlineHours"] == null ? string.Empty : item["NumberOfOnlineHours"].ToString(),
                                        PercentageOfOnlineHours = item["PercentageOfOnlineHours"] == null ? string.Empty : item["PercentageOfOnlineHours"].ToString(),
                                        OrdinaryOrOwners = item["OrdinaryOrOwners"] == null ? string.Empty : item["OrdinaryOrOwners"].ToString(),
                                        HavePA = item["HavePA"] == null ? string.Empty : item["HavePA"].ToString(),
                                        TypeUniversity = item["TypeUniversity"] == null ? string.Empty : item["TypeUniversity"].ToString(),
                                        HasException= item["HasException"] == null ? string.Empty : item["HasException"].ToString(),
                                        ExceptionFrom=item["ExceptionFrom"] == null ? string.Empty : item["ExceptionFrom"].ToString(),
                                        SirValue= item["SirValue"] == null ? string.Empty : item["SirValue"].ToString(),
                                        RespectedValue= item["RespectedValue"] == null ? string.Empty : item["RespectedValue"].ToString()

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

        public static List<Procedures> GetApprovedRecommendationStatus(string reqID, string status)
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
                            Logging.GetInstance().Debug("Entering method AllProcedures.GetSavedRecommendationStatusProc");
                            SPList list = web.Lists[Utilities.Constants.EquationOfficerProcedures];

                            var q = Common.Utilities.BusinessHelper.GetQueryObject("<Where><And><Eq><FieldRef Name='RequestID' /><Value Type='Lookup'>" +
                                reqID + "</Value></Eq><Eq><FieldRef Name='RecommendationStatus' /><Value Type='Text'>" +
                                status + "</Value></Eq></And></Where><OrderBy><FieldRef Name='ProcedureDate' Ascending='False' /></OrderBy>");

                            SPListItemCollection collListItems = list.GetItems(q);
                            if (collListItems.Count != 0)
                                foreach (SPListItem item in collListItems)
                                {
                                    SPFieldLookupValue RequestID = new SPFieldLookupValue((item["RequestID"] != null) ? item["RequestID"].ToString() : string.Empty);
                                    Proc.Add(new Procedures()
                                    {
                                        Procedure = item["Procedure"] == null ? string.Empty : item["Procedure"].ToString(),
                                        ID = item["ID"] == null ? string.Empty : item["ID"].ToString(),
                                        DecisionForPrint = item["DecisionForPrint"] == null ? string.Empty : item["DecisionForPrint"].ToString(),
                                        Opinion = item["InitialOpinion"] == null ? string.Empty : item["InitialOpinion"].ToString(),
                                        RequestID = RequestID.LookupValue,
                                        OccupationName = item["OccupationName"] == null ? string.Empty : item["OccupationName"].ToString(),
                                        HeadManagerName= item["HeadManagerName"] == null ? string.Empty : item["HeadManagerName"].ToString(),
                                        BookNum = item["bookNum"] == null ? string.Empty : item["bookNum"].ToString(),
                                        BookDate = item["bookDate"] == null ? string.Empty : item["bookDate"].ToString(),
                                        NumberOfHoursGained = item["NumberOfHoursGained"] == null ? string.Empty : item["NumberOfHoursGained"].ToString(),
                                        NumberOfOnlineHours = item["NumberOfOnlineHours"] == null ? string.Empty : item["NumberOfOnlineHours"].ToString(),
                                        PercentageOfOnlineHours = item["PercentageOfOnlineHours"] == null ? string.Empty : item["PercentageOfOnlineHours"].ToString(),
                                        OrdinaryOrOwners = item["OrdinaryOrOwners"] == null ? string.Empty : item["OrdinaryOrOwners"].ToString(),
                                        HavePA = item["HavePA"] == null ? string.Empty : item["HavePA"].ToString(),
                                        TypeUniversity = item["TypeUniversity"] == null ? string.Empty : item["TypeUniversity"].ToString(),
                                        SirValue = item["SirValue"] == null ? string.Empty : item["SirValue"].ToString(),
                                        RespectedValue = item["RespectedValue"] == null ? string.Empty : item["RespectedValue"].ToString(),
                                        HasException = item["HasException"] == null ? string.Empty : item["HasException"].ToString(),
                                        ExceptionFrom= item["ExceptionFrom"] == null ? string.Empty : item["ExceptionFrom"].ToString()


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

        public static void UpdateRecommendationStatusProc(string id, string status, string Opinion, string Procedure, string DecisionforPrint, string OccupationName, string NumberOfHoursGained, string NumberOfOnlineHours, string PercentageOfOnlineHours, string rblOwners, string HeHavePA, string rblUniversity, string bookNum, string bookDate, string HeadManagerName,bool hasException, string exceptionFrom,string sirValue,string respectedValue)
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
                            SPList list = web.Lists[Utilities.Constants.EquationOfficerProcedures];
                            SPListItem itemToUpdate = list.GetItemById(int.Parse(id));
                            itemToUpdate["RecommendationStatus"] = status;
                            itemToUpdate["DecisionForPrint"] = SPHttpUtility.HtmlEncode(DecisionforPrint);
                            itemToUpdate["InitialOpinion"] = SPHttpUtility.HtmlEncode(Opinion);
                            itemToUpdate["Procedure"] = SPHttpUtility.HtmlEncode(Procedure);
                            itemToUpdate["OccupationName"] = OccupationName;
                            itemToUpdate["HeadManagerName"] = HeadManagerName;
                            if (!string.IsNullOrEmpty(NumberOfHoursGained))
                                itemToUpdate["NumberOfHoursGained"] = Convert.ToInt32(NumberOfHoursGained);
                            if (!string.IsNullOrEmpty(NumberOfOnlineHours))
                                itemToUpdate["NumberOfOnlineHours"] = Convert.ToInt32(NumberOfOnlineHours);
                            itemToUpdate["PercentageOfOnlineHours"] = PercentageOfOnlineHours;
                            itemToUpdate["OrdinaryOrOwners"] = rblOwners;
                            itemToUpdate["HavePA"] = HeHavePA;
                            itemToUpdate["TypeUniversity"] = rblUniversity;
                            itemToUpdate["bookNum"] = bookNum;
                            itemToUpdate["HasException"] = hasException;
                            itemToUpdate["ExceptionFrom"] = exceptionFrom;
                            itemToUpdate["SirValue"] = sirValue;
                            itemToUpdate["RespectedValue"] = respectedValue;
                            
                            if (!string.IsNullOrEmpty(bookDate))
                            {
                                itemToUpdate["bookDate"] = SPUtility.CreateISO8601DateTimeFromSystemDateTime(DateTime.ParseExact(bookDate, "d/M/yyyy", CultureInfo.CurrentCulture));
                            }
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

        public static void UpdateRecommendationStatusDecision(string id, string DecisionforPrint, string OccupationName,string bookNum,string bookDate,string HeadManagerName)
        {
            SPSecurity.RunWithElevatedPrivileges(delegate ()
            {
                using (SPSite site = new SPSite(SPContext.Current.Site.Url))
                {
                    using (SPWeb web = site.OpenWeb())
                    {
                        try
                        {
                            Logging.GetInstance().Debug("Entering method AllProcedures.UpdateRecommendationStatusDecision");
                            SPList list = web.Lists[Utilities.Constants.EquationOfficerProcedures];
                            SPListItem itemToUpdate = list.GetItemById(int.Parse(id));
                            itemToUpdate["DecisionForPrint"] = SPHttpUtility.HtmlEncode(DecisionforPrint);
                            itemToUpdate["OccupationName"] = OccupationName;
                            itemToUpdate["HeadManagerName"] = HeadManagerName;
                            itemToUpdate["bookNum"] = bookNum;
                            itemToUpdate["bookDate"] = bookDate;
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
                            Logging.GetInstance().Debug("Exiting method AllProcedures.UpdateRecommendationStatusDecision");
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
                            SPList list = web.Lists[Utilities.Constants.ProgramManagerProcedures];
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
                            SPList list = web.Lists[Utilities.Constants.ProgramManagerProcedures];
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
                            var q = Common.Utilities.BusinessHelper.GetQueryObject(@"<Where><Eq><FieldRef Name='RequestID' /><Value Type='Lookup'>" +
                                reqID + "</Value></Eq></Where><OrderBy><FieldRef Name='ProcedureDate' Ascending='False' /></OrderBy>");

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

                            var q = Common.Utilities.BusinessHelper.GetQueryObject("<Where><Eq><FieldRef Name='RequestID' /><Value Type='Lookup'>" +
                                reqID + "</Value></Eq></Where><OrderBy><FieldRef Name='ProcedureDate' Ascending='False' /></OrderBy>");

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