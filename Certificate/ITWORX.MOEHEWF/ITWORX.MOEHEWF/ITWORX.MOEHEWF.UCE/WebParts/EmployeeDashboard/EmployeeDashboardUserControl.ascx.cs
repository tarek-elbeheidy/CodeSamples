using ITWORX.MOEHE.Utilities;
using ITWORX.MOEHE.Utilities.Logging;
using ITWORX.MOEHEWF.UCE.CONTROLTEMPLATES.ITWORX.MOEHEWF.UCE;
using ITWORX.MOEHEWF.UCE.Utilities;
using Microsoft.SharePoint;
using Microsoft.SharePoint.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI.WebControls;

namespace ITWORX.MOEHEWF.UCE.WebParts.EmployeeDashboard
{
    public partial class EmployeeDashboardUserControl : UserControlBase
    {
        private const string BASE_PATH = "~/_controltemplates/15/ITWORX.MOEHEWF.UCE/";

        #region Public Properties

        public string SPGroupName { get; set; }

        #endregion Public Properties

        private string LastLoadedControl
        {
            get
            {
                return ViewState["LastLoaded"] as string;
            }
            set
            {
                ViewState["LastLoaded"] = value;
            }
        }

        private void LoadUserControl(string GroupName)
        { 
            string controlPath = LastLoadedControl;
            PlaceHolder_RequestsUC.Controls.Clear();
            PlaceHolder_Requests_Search.Controls.Clear();
            //PlaceHolder_RequestsChart.Controls.Clear();
            if (!string.IsNullOrEmpty(controlPath))
            {
                if (controlPath.Contains("NewRequests"))
                {
                    NewRequests uc = (NewRequests)LoadControl(controlPath);
                    uc.ID = "UC_ITWorxEducation";
                    uc.SPGroupName = GroupName;
                    PlaceHolder_RequestsUC.Controls.Add(uc);
                }
                else if (controlPath.Contains("ReturnedRequests"))
                {
                    ReturnedRequests uc = (ReturnedRequests)LoadControl(controlPath);
                    uc.ID = "UC_ITWorxEducation";
                    uc.SPGroupName = GroupName;
                    PlaceHolder_RequestsUC.Controls.Add(uc);
                 

                   
                }
                else if (controlPath.Contains("LateRequests"))
                {
                    LateRequests uc = (LateRequests)LoadControl(controlPath);
                    uc.ID = "UC_ITWorxEducation";
                    uc.SPGroupName = GroupName;
                    PlaceHolder_RequestsUC.Controls.Add(uc);
                }
                else if (controlPath.Contains("ClarificationRequests"))
                {
                    CONTROLTEMPLATES.ITWORX.MOEHEWF.UCE.ClarificationRequests uc = (CONTROLTEMPLATES.ITWORX.MOEHEWF.UCE.ClarificationRequests)LoadControl(controlPath);
                    uc.ID = "UC_ITWorxEducation";
                    uc.SPGroupName = GroupName;
                    PlaceHolder_RequestsUC.Controls.Add(uc);
                }
                else if (controlPath.Contains("SearchRequests"))
                {
                    SearchRequests uc = (SearchRequests)LoadControl(controlPath);
                    uc.ID = "UC_ITWorxEducation";
                    uc.SPGroupName = GroupName;
                    PlaceHolder_Requests_Search.Controls.Add(uc);
                }
            }
            else if (controlPath == null)
            {
                NewRequests uc = (NewRequests)LoadControl(BASE_PATH + "NewRequests.ascx");
                uc.ID = "UC_ITWorxEducation";
                uc.SPGroupName = GroupName;
                PlaceHolder_RequestsUC.Controls.Add(uc);
                //BindChart();
            }
        }

        //private void BindChart()
        //{
        //    PlaceHolder_RequestsChart.Controls.Clear();
        //    RequestsChart uc = (RequestsChart)LoadControl(BASE_PATH + "RequestsChart.ascx");
        //    uc.ID = BASE_PATH + "RequestsChart.ascx_Name";
        //    uc.SPGroupName = SPGroupName;
        //    PlaceHolder_RequestsChart.Controls.Add(uc);
        //}
        protected void Page_Load(object sender, EventArgs e)
        {
            if (SPGroupName.Contains(","))
            {
                var stringList = SPGroupName.Split(',').ToList();
                var knownGrps = loggedInUserGroups();
                stringList = stringList.Where(x => knownGrps.Contains(x)).ToList();
                if (stringList.Count > 0)
                {
                    foreach (var item in stringList)
                    {
                        if (!IsPostBack)
                        {
                            if (LCID == (int)Language.English)
                                hprLnkPriorApproval.NavigateUrl = SPContext.Current.Site.Url + "/en/Pages/HomeDashboard.aspx";
                            else
                                hprLnkPriorApproval.NavigateUrl = SPContext.Current.Site.Url + "/ar/Pages/HomeDashboard.aspx";
                            //BindbtnRedirct();
                            BindbtnsCount(item);
                           // BindConunt(item);
                        }

                        LoadUserControl(item);
                    }
                }
                else
                {
                    if (LCID == (int)Language.English)
                        SPUtility.Redirect(SPContext.Current.Site.Url + "/en/_layouts/15/AccessDenied.aspx", SPRedirectFlags.DoNotEndResponse, HttpContext.Current);
                    else
                        SPUtility.Redirect(SPContext.Current.Site.Url + "/ar/_layouts/15/AccessDenied.aspx", SPRedirectFlags.DoNotEndResponse, HttpContext.Current);

                }
            }
            else if (!SPGroupName.Contains(","))
            {
                var knownGrps = loggedInUserGroups();
                //var lst = knownGrps.Contains(SPGroupName);
                if (knownGrps.Contains(SPGroupName))
                {
                    if (!IsPostBack)
                    {
                        if (LCID == (int)Language.English)
                            hprLnkPriorApproval.NavigateUrl = SPContext.Current.Site.Url + "/en/Pages/HomeDashboard.aspx";
                        else
                            hprLnkPriorApproval.NavigateUrl = SPContext.Current.Site.Url + "/ar/Pages/HomeDashboard.aspx";
                        //BindbtnRedirct();
                        BindbtnsCount(SPGroupName);
                        //BindConunt(SPGroupName);
                    }
                  
                        LoadUserControl(SPGroupName);
                }
                else
                {

                    if (LCID == (int)Language.English)
                        SPUtility.Redirect(SPContext.Current.Site.Url + "/en/_layouts/15/AccessDenied.aspx", SPRedirectFlags.DoNotEndResponse, HttpContext.Current);
                    else
                        SPUtility.Redirect(SPContext.Current.Site.Url + "/ar/_layouts/15/AccessDenied.aspx", SPRedirectFlags.DoNotEndResponse, HttpContext.Current);
                }
            }
            else
            {
                Response.Redirect(@"~/_layouts/15/AccessDenied.aspx");
            }
        }

        private bool inGroup(string GroupName)
        {
            SPGroupCollection _SPGroupCollection = SPContext.Current.Web.CurrentUser.Groups;
            bool groupExist = false;
            //return _SPGroupCollection.GetByName(GroupName) != null;
            foreach (SPGroup group in _SPGroupCollection)
            {
                if (GroupName.Equals(group.Name))
                {
                    groupExist = true;
                    //return true;
                }
            }
            return groupExist;
        }

        private List<string> loggedInUserGroups()
        {
            List<string> result = new List<string>();
            SPGroupCollection _SPGroupCollection = SPContext.Current.Web.CurrentUser.Groups;
            foreach (SPGroup group in _SPGroupCollection)
            {
                result.Add(group.Name);
            }
            return result;
        }

        private bool userInGroup(string GroupName)
        {
            bool exists = false;
            List<SPUser> users = HelperMethods.GetGroupUsers(GroupName);
            //List<SPUser> Europeancusers = HelperMethods.GetGroupUsers(Common.Utilities.Constants.EuropeanProgEmployeeGroupName);
            foreach (SPUser user in users)
            {
                if (user.LoginName == SPContext.Current.Web.CurrentUser.LoginName)
                    exists = true;
            }
            return exists;
        }

        private void BindbtnRedirct()
        {
            try
            {
                Logging.GetInstance().Debug("Enter MyRequests.BindbtnRedirct");
                if (userInGroup(Common.Utilities.Constants.CulturalMissionBritainGroupName) ||
                    userInGroup(Common.Utilities.Constants.CulturalMissionAustraliaGroupName) ||
                    userInGroup(Common.Utilities.Constants.CulturalMissionFranceGroupName) ||
                    userInGroup(Common.Utilities.Constants.CulturalMissionUSAGroupName) ||
                    userInGroup(Common.Utilities.Constants.CulturalMissionCanadaGroupName) ||
                    userInGroup(Common.Utilities.Constants.CulturalMissionJordanGroupName))
                {
                    hprLnkPriorApproval.NavigateUrl = SPContext.Current.Web.Url + "/Pages/PACulturalMissionEmployeeDashboard.aspx";
                }
                else if (userInGroup(Common.Utilities.Constants.HigherEducationalInstitutionsGroupName))
                {
                    hprLnkPriorApproval.NavigateUrl = SPContext.Current.Web.Url + "/Pages/PAHigherEducationalInstitutionsDashboard.aspx";
                }
                else if (userInGroup(Common.Utilities.Constants.ProgramManagerGroupName))
                {
                    hprLnkPriorApproval.NavigateUrl = SPContext.Current.Web.Url + "/Pages/PAProgramManagerDashboard.aspx";
                }
                else if (userInGroup(Common.Utilities.Constants.DepartmentManagerGroupName))
                {
                    hprLnkPriorApproval.NavigateUrl = SPContext.Current.Web.Url + "/Pages/PAHeadManagerDashboard.aspx";
                }
                else if (userInGroup(Common.Utilities.Constants.ArabicProgEmployeeGroupName))
                {
                    hprLnkPriorApproval.NavigateUrl = SPContext.Current.Web.Url + "/Pages/PAArabicAsianProgramEmployeeDashboard.aspx";
                }
            }
            catch (Exception ex)
            {
                Logging.GetInstance().LogException(ex);
            }
            finally
            {
                Logging.GetInstance().Debug("Exit MyRequests.BindbtnRedirct");
            }
        }

        private void BindConunt(string GroupName)
        {
            try
            {
                Logging.GetInstance().Debug("Enter MyRequests.BindConunt");
                Menu_Links.Items.Add(new MenuItem(HelperMethods.LocalizedText("ITWORX_MOEHEWF_UCE", "NewRequests", (uint)LCID), "0"));

                if (userInGroup(Common.Utilities.Constants.CulturalMissionBritainGroupName) ||
                    userInGroup(Common.Utilities.Constants.CulturalMissionAustraliaGroupName) ||
                    userInGroup(Common.Utilities.Constants.CulturalMissionFranceGroupName) ||
                    userInGroup(Common.Utilities.Constants.CulturalMissionUSAGroupName) ||
                    userInGroup(Common.Utilities.Constants.CulturalMissionCanadaGroupName) ||
                    userInGroup(Common.Utilities.Constants.CulturalMissionJordanGroupName) ||
                    userInGroup(Common.Utilities.Constants.HigherEducationalInstitutionsGroupName)) { }
                else
                {
                    Menu_Links.Items.Add(new MenuItem(HelperMethods.LocalizedText("ITWORX_MOEHEWF_UCE", "ReturnedRequests", (uint)LCID) , "1"));
                }

                Menu_Links.Items.Add(new MenuItem(HelperMethods.LocalizedText("ITWORX_MOEHEWF_UCE", "LateRequests", (uint)LCID) , "2"));

                if (userInGroup(Common.Utilities.Constants.ReceptionistGroupName) || userInGroup(Common.Utilities.Constants.ArabicProgEmployeeGroupName)
                    || userInGroup(Common.Utilities.Constants.EuropeanProgEmployeeGroupName))
                    Menu_Links.Items.Add(new MenuItem(HelperMethods.LocalizedText("ITWORX_MOEHEWF_UCE", "ClarificationRequests", (uint)LCID) , "3"));


            }
            catch (Exception ex)
            {
                Logging.GetInstance().LogException(ex);
            }
            finally
            {
                Logging.GetInstance().Debug("Exit MyRequests.BindConunt");
            }
        }

        private void BindbtnsCount(string GroupName)
        {
            try
            {
                Logging.GetInstance().Debug("Enter MyRequests.BindbtnsCount");
                int delayedDays = int.Parse(HelperMethods.GetConfigurationValue(SPContext.Current.Site.Url + Common.Utilities.Constants.HEWebUrl, Common.Utilities.Constants.Configuration, "DelayedDays"));
                //#region
                //List<Entities.SimilarRequest> userRequests=  BL.NewRequests.GetAllNewRequests("", this.LCID).ToList<Entities.SimilarRequest>();
                //switch(GroupName.ToLower())
                //{
                //    case "ProgramManagerGroupName.ToLower()":
                //        userRequests.Where(r => r.RequestStatusId == (int)Common.Utilities.RequestStatus.UCEProgramManagerReview);
                //        break;
                //}

                //#endregion
                Menu_Links.Items.Add(new MenuItem(HelperMethods.LocalizedText("ITWORX_MOEHEWF_UCE", "NewRequests", (uint)LCID) + "(" + BL.NewRequests.GetAllNewRequests(Common.Utilities.BusinessHelper.CreateCAMLQuery(BL.NewRequests.GetNewQueryPerRole(GroupName), "Or", true)
               + "<OrderBy><FieldRef Name='SubmitDate' Ascending='False' /></OrderBy>").ToList().Where(x => (x.AssignedTo.ToLower().Equals(SPContext.Current.Web.CurrentUser.LoginName.ToLower()) || x.AssignedTo.ToLower() == GroupName.ToLower()) && (string.IsNullOrEmpty(x.DelayedDays) || int.Parse(x.DelayedDays) < delayedDays)).ToList().Count + ")", "0"));

                if (userInGroup(Common.Utilities.Constants.CulturalMissionBritainGroupName) ||
                    userInGroup(Common.Utilities.Constants.CulturalMissionAustraliaGroupName) ||
                    userInGroup(Common.Utilities.Constants.CulturalMissionFranceGroupName) ||
                    userInGroup(Common.Utilities.Constants.CulturalMissionUSAGroupName) ||
                    userInGroup(Common.Utilities.Constants.CulturalMissionCanadaGroupName) ||
                    userInGroup(Common.Utilities.Constants.CulturalMissionJordanGroupName) ||
                    userInGroup(Common.Utilities.Constants.HigherEducationalInstitutionsGroupName)) { }
                else
                {
                    Menu_Links.Items.Add(new MenuItem(HelperMethods.LocalizedText("ITWORX_MOEHEWF_UCE", "ReturnedRequests", (uint)LCID) + "(" + BL.ReturnedRequests.GetAllReturnedRequests(Common.Utilities.BusinessHelper.CreateCAMLQuery(BL.ReturnedRequests.GetReturnedQueryPerRole(GroupName), "Or", true)
                + "<OrderBy><FieldRef Name='SubmitDate' Ascending='False' /></OrderBy>").ToList().Where(x => (x.AssignedTo.ToLower().Equals(SPContext.Current.Web.CurrentUser.LoginName.ToLower()) || x.AssignedTo.ToLower() == GroupName.ToLower()) && (string.IsNullOrEmpty(x.DelayedDays) || int.Parse(x.DelayedDays) < delayedDays)).ToList().Count + ")", "1"));
                }

                Menu_Links.Items.Add(new MenuItem(HelperMethods.LocalizedText("ITWORX_MOEHEWF_UCE", "LateRequests", (uint)LCID) + "(" + BL.LateRequests.GetAllLateRequests(Common.Utilities.BusinessHelper.CreateCAMLQuery(BL.LateRequests.GetLateQueryPerRole(), "And", true)
              + "<OrderBy><FieldRef Name='SubmitDate' Ascending='False' /></OrderBy>").ToList().Where(x => (x.AssignedTo.ToLower().Equals(SPContext.Current.Web.CurrentUser.LoginName.ToLower()) || x.AssignedTo.ToLower() == GroupName.ToLower()) && x.IsRequestClosed== false  && (string.IsNullOrEmpty(x.DelayedDays) || int.Parse(x.DelayedDays) > delayedDays)).ToList().Count + ")", "2"));

                if (userInGroup(Common.Utilities.Constants.ReceptionistGroupName) || userInGroup(Common.Utilities.Constants.ArabicProgEmployeeGroupName)
                    || userInGroup(Common.Utilities.Constants.EuropeanProgEmployeeGroupName))
                    Menu_Links.Items.Add(new MenuItem(HelperMethods.LocalizedText("ITWORX_MOEHEWF_UCE", "ClarificationRequests", (uint)LCID) + "(" + BL.ClarificationRequests.GetAllClarificationRequests(Common.Utilities.BusinessHelper.CreateCAMLQuery(BL.ClarificationRequests.GetClarQueryPerRole(GroupName), "And", true)
                  + "<OrderBy><FieldRef Name='ClarificationDate' Ascending='False' /></OrderBy>",LCID).ToList().Where(x => x.RequestSender.ToLower().Equals(SPContext.Current.Web.CurrentUser.Name.ToLower()) || x.RequestSender.ToLower() == GroupName.ToLower()).ToList().Count + ")", "3"));
            }
            catch (Exception ex)
            {
                Logging.GetInstance().LogException(ex);
            }
            finally
            {
                Logging.GetInstance().Debug("Exit MyRequests.BindbtnsCount");
            }
        }

        protected void Menu_Links_MenuItemClick(object sender, MenuEventArgs e)
        {
            MenuItem menu = e.Item;

            string controlPath = string.Empty;

            switch (menu.Value)
            {
                case "0":
                    controlPath = BASE_PATH + "NewRequests.ascx";
                    break;

                case "1":
                    controlPath = BASE_PATH + "ReturnedRequests.ascx";
                    break;

                case "2":
                    controlPath = BASE_PATH + "LateRequests.ascx";
                    break;

                case "3":
                    controlPath = BASE_PATH + "ClarificationRequests.ascx";
                    break;

                default:
                    controlPath = BASE_PATH + "NewRequests.ascx";
                    break;
            }

            LastLoadedControl = controlPath;
            if (SPGroupName.Contains(","))
            {
                var stringList = SPGroupName.Split(',').ToList();
                var knownGrps = loggedInUserGroups();
                stringList = stringList.Where(x => knownGrps.Contains(x)).ToList();
                foreach (var item in stringList)
                {
                    LoadUserControl(item);
                }
            }
            else
                LoadUserControl(SPGroupName);
            //BindChart();
        }

        protected void Menu_ParentLinks_MenuItemClick(object sender, MenuEventArgs e)
        {
            MenuItem menu = e.Item;

            string controlPath = string.Empty;

            switch (menu.Value)
            {
                case "0":
                    controlPath = BASE_PATH + "NewRequests.ascx";
                    break;

                case "1":
                    controlPath = BASE_PATH + "SearchRequests.ascx";
                    break;

                default:
                    controlPath = BASE_PATH + "NewRequests.ascx";
                    break;
            }

            LastLoadedControl = controlPath;
            if (SPGroupName.Contains(","))
            {
                var stringList = SPGroupName.Split(',').ToList();
                var knownGrps = loggedInUserGroups();
                stringList = stringList.Where(x => knownGrps.Contains(x)).ToList();
                foreach (var item in stringList)
                {
                    LoadUserControl(item);
                }
            }
            else
                LoadUserControl(SPGroupName);

            if (menu.Value == "0")
                Menu_Links.Visible = true;
            //BindChart();
            else
                Menu_Links.Visible = false;
        }
    }
}