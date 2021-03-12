using ITWORX.MOEHE.Utilities;
using ITWORX.MOEHE.Utilities.Logging;
using ITWORX.MOEHEWF.PA.Utilities;
using System;
using System.Linq;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using ITWORX.MOEHEWF.PA.CONTROLTEMPLATES.ITWORX.MOEHEWF.PA;
using Microsoft.SharePoint;
using System.ComponentModel;
using System.Collections.Generic;
using Microsoft.SharePoint.Utilities;
using System.Web;

namespace ITWORX.MOEHEWF.PA.WebParts.PAEmployeeDashboard
{
    public partial class PAEmployeeDashboardUserControl : UserControlBase
    {
        private const string BASE_PATH = "~/_controltemplates/15/ITWORX.MOEHEWF.PA/";

        #region Public Properties
        public string SPGroupName { get; set; }
        #endregion
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
            if (!string.IsNullOrEmpty(controlPath))
            {

                if (controlPath.Contains("PANewRequests"))
                {
                    PANewRequests uc = (PANewRequests)LoadControl(controlPath);
                    uc.ID = "UC_ITWorxEducation";
                    uc.SPGroupName = GroupName;
                    PlaceHolder_RequestsUC.Controls.Add(uc);
                }
                else if (controlPath.Contains("PAReturnedRequests"))
                {
                    PAReturnedRequests uc = (PAReturnedRequests)LoadControl(controlPath);
                    uc.ID = "UC_ITWorxEducation";
                    uc.SPGroupName = GroupName;
                    PlaceHolder_RequestsUC.Controls.Add(uc);
                }
                else if (controlPath.Contains("PALateRequests"))
                {
                    PALateRequests uc = (PALateRequests)LoadControl(controlPath);
                    uc.ID = "UC_ITWorxEducation";
                    uc.SPGroupName = GroupName;
                    PlaceHolder_RequestsUC.Controls.Add(uc);
                }
                else if (controlPath.Contains("PAClarificationRequests"))
                {
                    CONTROLTEMPLATES.ITWORX.MOEHEWF.PA.PAClarificationRequests uc = (CONTROLTEMPLATES.ITWORX.MOEHEWF.PA.PAClarificationRequests)LoadControl(controlPath);
                    uc.ID = "UC_ITWorxEducation";
                    uc.SPGroupName = GroupName;
                    PlaceHolder_RequestsUC.Controls.Add(uc);
                }
                else if (controlPath.Contains("PASearchRequests"))
                {
                    PASearchRequests uc = (PASearchRequests)LoadControl(controlPath);
                    uc.ID = "UC_ITWorxEducation";
                    uc.SPGroupName = GroupName;
                    PlaceHolder_Requests_Search.Controls.Add(uc);
                }
            }
            else if (controlPath == null)
            {

                PANewRequests uc = (PANewRequests)LoadControl(BASE_PATH + "PANewRequests.ascx");
                uc.ID = "UC_ITWorxEducation";
                uc.SPGroupName = GroupName;
                PlaceHolder_RequestsUC.Controls.Add(uc); 
            }
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
        protected void Page_Load(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(SPGroupName))
            {
                return;
            }
            if (SPGroupName.Contains(","))
            {
                var stringList = SPGroupName.Split(',').ToList();
                var knownGrps = loggedInUserGroups();
                stringList = stringList.Where(x => knownGrps.Contains(x)).ToList();
                if (stringList.Count>0)
                {
                    foreach (var item in stringList)
                    {

                        if (!IsPostBack)
                        {
                            if (LCID == (int)Language.English)
                                hprLnkEquivalence.NavigateUrl = SPContext.Current.Site.Url + "/en/Pages/HomeDashboard.aspx";
                            else
                                hprLnkEquivalence.NavigateUrl = SPContext.Current.Site.Url + "/ar/Pages/HomeDashboard.aspx";
                            BindbtnsCount(item);
                            //BindConunt(item);
                            // BindbtnRedirct();
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
                if (knownGrps.Contains(SPGroupName))
                {
                    if (!IsPostBack)
                    {
                        if (LCID == (int)Language.English)
                            hprLnkEquivalence.NavigateUrl = SPContext.Current.Site.Url + "/en/Pages/HomeDashboard.aspx";
                        else
                            hprLnkEquivalence.NavigateUrl = SPContext.Current.Site.Url + "/ar/Pages/HomeDashboard.aspx";
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
        private bool userInGroup(string GroupName)
        {
            bool exists = false;
            List<SPUser> users = HelperMethods.GetGroupUsers(GroupName);
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
                    hprLnkEquivalence.NavigateUrl = SPContext.Current.Web.Url + "/Pages/CulturalMissionEmployeeDashboard.aspx";
                }
                else if (userInGroup(Common.Utilities.Constants.HigherEducationalInstitutionsGroupName))
                {
                    hprLnkEquivalence.NavigateUrl = SPContext.Current.Web.Url + "/Pages/HigherEducationalInstitutionsDashboard.aspx";

                }
                else if (userInGroup(Common.Utilities.Constants.ProgramManagerGroupName))
                {
                    hprLnkEquivalence.NavigateUrl = SPContext.Current.Web.Url + "/Pages/ProgramManagerDashboard.aspx";
                }
                else if (userInGroup(Common.Utilities.Constants.DepartmentManagerGroupName))
                {
                    hprLnkEquivalence.NavigateUrl = SPContext.Current.Web.Url + "/Pages/HeadManagerDashboard.aspx";
                }
                else if (userInGroup(Common.Utilities.Constants.ArabicProgEmployeeGroupName))
                {
                    hprLnkEquivalence.NavigateUrl = SPContext.Current.Web.Url + "/Pages/ArabicAsianProgramEmployeeDashboard.aspx";

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
        private void BindbtnsCount(string GroupName)
        {
            try
            {

                Logging.GetInstance().Debug("Enter PAMyRequests.BindbtnsCount");
                int delayedDays = int.Parse(HelperMethods.GetConfigurationValue(SPContext.Current.Site.Url + Common.Utilities.Constants.HEWebUrl, Common.Utilities.Constants.Configuration, "PADelayedDays"));

                Menu_Links.Items.Add(new MenuItem(HelperMethods.LocalizedText("ITWORX_MOEHEWF_PA", "PANewRequests", (uint)LCID) + "(" + BL.PANewRequests.GetAllPANewRequests(Common.Utilities.BusinessHelper.CreateCAMLQuery(BL.PANewRequests.GetNewQueryPerRole(GroupName), "Or", true)
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
                    Menu_Links.Items.Add(new MenuItem(HelperMethods.LocalizedText("ITWORX_MOEHEWF_PA", "ReturnedRequests", (uint)LCID) + "(" + BL.ReturnedRequests.GetAllReturnedRequests(Common.Utilities.BusinessHelper.CreateCAMLQuery(BL.ReturnedRequests.GetReturnedQueryPerRole(GroupName), "Or", true)
                + "<OrderBy><FieldRef Name='SubmitDate' Ascending='False' /></OrderBy>").ToList().Where(x => (x.AssignedTo.ToLower().Equals(SPContext.Current.Web.CurrentUser.LoginName.ToLower()) || x.AssignedTo.ToLower() == GroupName.ToLower()) && (string.IsNullOrEmpty(x.DelayedDays) || int.Parse(x.DelayedDays) < delayedDays)).ToList().Count + ")", "1"));

                }
                    Menu_Links.Items.Add(new MenuItem(HelperMethods.LocalizedText("ITWORX_MOEHEWF_PA", "LateRequests", (uint)LCID) + "(" + BL.LateRequests.GetAllLateRequests(Common.Utilities.BusinessHelper.CreateCAMLQuery(BL.LateRequests.GetLateQueryPerRole(), "And", true)
                  + "<OrderBy><FieldRef Name='SubmitDate' Ascending='False' /></OrderBy>").ToList().Where(x => (x.AssignedTo.ToLower().Equals(SPContext.Current.Web.CurrentUser.LoginName.ToLower()) || x.AssignedTo.ToLower() == GroupName.ToLower()) && (int.Parse(x.DelayedDays) > delayedDays)).ToList().Count + ")", "2"));

                    if (HelperMethods.InGroup(Common.Utilities.Constants.ReceptionistGroupName) || HelperMethods.InGroup(Common.Utilities.Constants.ArabicProgEmployeeGroupName)
                        || HelperMethods.InGroup(Common.Utilities.Constants.EuropeanProgEmployeeGroupName))
                        Menu_Links.Items.Add(new MenuItem(HelperMethods.LocalizedText("ITWORX_MOEHEWF_PA", "PAClarificationRequests", (uint)LCID) + "(" + BL.PAClarificationRequests.GetAllPAClarificationRequests(Common.Utilities.BusinessHelper.CreateCAMLQuery(BL.PAClarificationRequests.GetClarQueryPerRole(GroupName), "And", true)
                      + "<OrderBy><FieldRef Name='ClarificationDate' Ascending='False' /></OrderBy>", LCID).ToList().Where(x => x.RequestSender.ToLower().Equals(SPContext.Current.Web.CurrentUser.Name.ToLower()) || x.RequestSender.ToLower() == GroupName.ToLower()).ToList().Count + ")", "3"));
                
            }
            catch (Exception ex)
            {
                Logging.GetInstance().LogException(ex);
            }
            finally
            {

                Logging.GetInstance().Debug("Exit PAMyRequests.BindbtnsCount");
            }
        }

        private void BindConunt(string GroupName)
        {
            try
            {

                Logging.GetInstance().Debug("Enter PAMyRequests.BindConunt");
                Menu_Links.Items.Add(new MenuItem(HelperMethods.LocalizedText("ITWORX_MOEHEWF_PA", "PANewRequests", (uint)LCID), "0"));

                if (userInGroup(Common.Utilities.Constants.CulturalMissionBritainGroupName) ||
                   userInGroup(Common.Utilities.Constants.CulturalMissionAustraliaGroupName) ||
                   userInGroup(Common.Utilities.Constants.CulturalMissionFranceGroupName) ||
                   userInGroup(Common.Utilities.Constants.CulturalMissionUSAGroupName) ||
                   userInGroup(Common.Utilities.Constants.CulturalMissionCanadaGroupName) ||
                   userInGroup(Common.Utilities.Constants.CulturalMissionJordanGroupName) ||
                   userInGroup(Common.Utilities.Constants.HigherEducationalInstitutionsGroupName)) { }
                else
                {
                    Menu_Links.Items.Add(new MenuItem(HelperMethods.LocalizedText("ITWORX_MOEHEWF_PA", "ReturnedRequests", (uint)LCID),"1"));

                }
                Menu_Links.Items.Add(new MenuItem(HelperMethods.LocalizedText("ITWORX_MOEHEWF_PA", "LateRequests", (uint)LCID),"2"));

                if (HelperMethods.InGroup(Common.Utilities.Constants.ReceptionistGroupName) || HelperMethods.InGroup(Common.Utilities.Constants.ArabicProgEmployeeGroupName)
                    || HelperMethods.InGroup(Common.Utilities.Constants.EuropeanProgEmployeeGroupName))
                    Menu_Links.Items.Add(new MenuItem(HelperMethods.LocalizedText("ITWORX_MOEHEWF_PA", "PAClarificationRequests", (uint)LCID),"3"));


            }
            catch (Exception ex)
            {
                Logging.GetInstance().LogException(ex);
            }
            finally
            {

                Logging.GetInstance().Debug("Exit PAMyRequests.BindConunt");
            }
        }
        protected void Menu_Links_MenuItemClick(object sender, MenuEventArgs e)
        {
            MenuItem menu = e.Item;

            string controlPath = string.Empty;

            switch (menu.Value)
            {
                case "0":
                    controlPath = BASE_PATH + "PANewRequests.ascx";
                    break;
                case "1":
                    controlPath = BASE_PATH + "PAReturnedRequests.ascx";
                    break;
                case "2":
                    controlPath = BASE_PATH + "PALateRequests.ascx";
                    break;
                case "3":
                    controlPath = BASE_PATH + "PAClarificationRequests.ascx";
                    break;
                    
                default:
                    controlPath = BASE_PATH + "PANewRequests.ascx";
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
        }

        protected void Menu_ParentLinks_MenuItemClick(object sender, MenuEventArgs e)
        {
            MenuItem menu = e.Item;

            string controlPath = string.Empty;

            switch (menu.Value)
            {
                case "0":
                    controlPath = BASE_PATH + "PANewRequests.ascx";
                    break;
                case "1":
                    controlPath = BASE_PATH + "PASearchRequests.ascx";
                    break;
                default:
                    controlPath = BASE_PATH + "PANewRequests.ascx";
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
            {
                Menu_Links.Visible = true; 
            }
            else
                Menu_Links.Visible = false;

        }
    }
}
