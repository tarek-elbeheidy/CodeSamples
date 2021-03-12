using ITWORX.MOEHE.Utilities;
using ITWORX.MOEHE.Utilities.Logging;
using ITWORX.MOEHEWF.SCE.CONTROLTEMPLATES.ITWORX.MOEHEWF.SCE;
using ITWORX.MOEHEWF.SCE.Utilities;
using Microsoft.SharePoint;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;

namespace ITWORX.MOEHEWF.SCE.WebParts.EmployeeDashboard
{
    public partial class EmployeeDashboardUserControl : UserControlBase
    {
        private const string BASE_PATH = "~/_controltemplates/15/ITWORX.MOEHEWF.SCE/";

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
                    ClarificationRequests uc = (ClarificationRequests)LoadControl(controlPath);
                    uc.ID = "UC_ITWorxEducation";
                    uc.SPGroupName = GroupName;
                    PlaceHolder_RequestsUC.Controls.Add(uc);
                }
                else if (controlPath.Contains("ReassignRequests"))
                {
                    ReassignRequests uc = (ReassignRequests)LoadControl(controlPath);
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

                
                else if ( controlPath.Contains("ExceptionRequests"))
                {
                    ExceptionRequests uc = (ExceptionRequests)LoadControl(controlPath);
                    uc.ID = "UC_ITWorxEducation";
                    uc.SPGroupName = GroupName;
                    PlaceHolder_RequestsUC.Controls.Add(uc);
                }
                else if (controlPath.Contains("SCEMyRequests"))
                {
                    NewRequests uc = (NewRequests)LoadControl(BASE_PATH + "NewRequests.ascx");
                    uc.ID = "UC_ITWorxEducation";
                    uc.SPGroupName = GroupName;
                    PlaceHolder_RequestsUC.Controls.Add(uc);

                }
            }
            else if (controlPath == null)
            {

                NewRequests uc = (NewRequests)LoadControl(BASE_PATH + "NewRequests.ascx");
                uc.ID = "UC_ITWorxEducation";
                uc.SPGroupName = GroupName;
                PlaceHolder_RequestsUC.Controls.Add(uc);
            }
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            if (LCID == (uint)Language.English)
            {
                hprLnkEquivalence.NavigateUrl = SPContext.Current.Site.Url + "/en/Pages/HomeDashboard.aspx";
            }
            else
            {
                hprLnkEquivalence.NavigateUrl = SPContext.Current.Site.Url + "/ar/Pages/HomeDashboard.aspx";
            }
            if (!HelperMethods.InGroup(Common.Utilities.Constants.EmployeeAsApplicant))
            {
                if (Menu_ParentLinks.Items.Count > 2)
                {
                    Menu_ParentLinks.Items.RemoveAt(2);
                }

            }


                    //Menu_ParentLinks.Items.RemoveAt(2);//s.Items(1).Visible = False
         
                string groupName = string.Empty;
            List<string> groupNameList = new List<string>();
            if (string.IsNullOrEmpty(SPGroupName))
            {
                return;
            }
            if (!SPGroupName.Contains(","))
            {
                groupName = SPGroupName.Trim();

                if (/*!groupName.Equals(Common.Utilities.Constants.ReassignEmployees) &&*/ HelperMethods.InGroup(groupName))
                {
                    if (!IsPostBack)
                    {
                        BindbtnsCount(groupName);
                        // BindbtnRedirct();
                    }
                    LoadUserControl(groupName);
                }
            }
            else
            {
                groupNameList = SPGroupName.Split(',').ToList();
                foreach (var item in groupNameList)
                {

                    if (/*!item.Equals(Common.Utilities.Constants.ReassignEmployees) &&*/ HelperMethods.InGroup(item))
                    {
                        if (!IsPostBack)
                        {
                            BindbtnsCount(item);
                            //BindbtnRedirct();
                        }
                        LoadUserControl(item);
                    }
                }
            }



        }



        private void BindbtnsCount(string GroupName)
        {
            try
            {

                Logging.GetInstance().Debug("Enter EmployeeDashboard.BindbtnsCount");
                int delayedDays = int.Parse(HelperMethods.GetConfigurationValue(SPContext.Current.Site.Url + Common.Utilities.Constants.HEWebUrl, Common.Utilities.Constants.Configuration, "SCEDelayedDays"));

                #region NewQuery
                // string newQuery = BL.NewRequests.GetNewRequestsQuery(GroupName, delayedDays);

                Menu_Links.Items.Add(new MenuItem(HelperMethods.LocalizedText("ITWORX_MOEHEWF_SCE", "SCENewRequests", (uint)LCID) +
              /*"(" + BL.NewRequests.GetAllNewRequests(newQuery, LCID).Count() + ")" */string.Empty, "0"));



                #endregion

                if (!HelperMethods.InGroup(Common.Utilities.Constants.SCECulturalMissionBritainGroupName) &&
                   !HelperMethods.InGroup(Common.Utilities.Constants.SCECulturalMissionAustraliaGroupName) &&
                   !HelperMethods.InGroup(Common.Utilities.Constants.SCECulturalMissionFranceGroupName) &&
                   !HelperMethods.InGroup(Common.Utilities.Constants.SCECulturalMissionUSAGroupName) &&
                   !HelperMethods.InGroup(Common.Utilities.Constants.SCECulturalMissionCanadaGroupName) &&
                   !HelperMethods.InGroup(Common.Utilities.Constants.SCECulturalMissionJordanGroupName) &&
                   !HelperMethods.InGroup(Common.Utilities.Constants.SCEHigherEducationalInstitutionsGroupName) &&
                   !HelperMethods.InGroup(Common.Utilities.Constants.SCEDepartmentManagersGroupName)&&
                   !HelperMethods.InGroup(Common.Utilities.Constants.SCESectionManagers))
                {

                    //  string returnedQuery = BL.ReturnedRequests.GetReturnedRequestsQuery(GroupName, delayedDays);

                    //  Menu_Links.Items.Add(new MenuItem(HelperMethods.LocalizedText("ITWORX_MOEHEWF_SCE", "ReturnedRequests", (uint)LCID) +
                    //"(" + BL.ReturnedRequests.GetAllReturnedRequests(returnedQuery,LCID).Count() + ")", "1"));

                    Menu_Links.Items.Add(new MenuItem(HelperMethods.LocalizedText("ITWORX_MOEHEWF_SCE", "ReturnedRequests", (uint)LCID) +
                       string.Empty, "1"));


                }
                /*
              

               /* to be implemented later 



               if (HelperMethods.InGroup(Common.Utilities.Constants.SCECulturalMissionBritainGroupName) ||
                   HelperMethods.InGroup(Common.Utilities.Constants.SCECulturalMissionAustraliaGroupName) ||
                   HelperMethods.InGroup(Common.Utilities.Constants.SCECulturalMissionFranceGroupName) ||
                   HelperMethods.InGroup(Common.Utilities.Constants.SCECulturalMissionUSAGroupName) ||
                   HelperMethods.InGroup(Common.Utilities.Constants.SCECulturalMissionCanadaGroupName) ||
                   HelperMethods.InGroup(Common.Utilities.Constants.SCECulturalMissionJordanGroupName) ||
                   HelperMethods.InGroup(Common.Utilities.Constants.SCEHigherEducationalInstitutionsGroupName)) { }
               else
               {
                   Menu_Links.Items.Add(new MenuItem(HelperMethods.LocalizedText("ITWORX_MOEHEWF_SCE", "ReturnedRequests", (uint)LCID) + "(" + BL.ReturnedRequests.GetAllReturnedRequests(Common.Utilities.BusinessHelper.CreateCAMLQuery(BL.ReturnedRequests.GetReturnedQueryPerRole(GroupName), "Or", true)
               + "<OrderBy><FieldRef Name='SubmitDate' Ascending='False' /></OrderBy>", LCID).ToList().Where(x => (x.AssignedTo.ToLower().Equals(SPContext.Current.Web.CurrentUser.LoginName.ToLower()) || x.AssignedTo.ToLower() == GroupName.ToLower()) && (string.IsNullOrEmpty(x.DelayedDays) || int.Parse(x.DelayedDays) < delayedDays)).ToList().Count + ")", "1"));

               }
               Menu_Links.Items.Add(new MenuItem(HelperMethods.LocalizedText("ITWORX_MOEHEWF_SCE", "LateRequests", (uint)LCID) + "(" + BL.LateRequests.GetAllLateRequests(Common.Utilities.BusinessHelper.CreateCAMLQuery(BL.LateRequests.GetLateQueryPerRole(), "And", true)
             + "<OrderBy><FieldRef Name='SubmitDate' Ascending='False' /></OrderBy>", LCID).ToList().Where(x => x.AssignedTo.ToLower().Equals(SPContext.Current.Web.CurrentUser.LoginName.ToLower()) || x.AssignedTo.ToLower() == GroupName.ToLower()).ToList().Count + ")", "2"));
             */

                Menu_Links.Items.Add(new MenuItem(HelperMethods.LocalizedText("ITWORX_MOEHEWF_SCE", "LateRequests", (uint)LCID) +
                          string.Empty, "2"));
                if (HelperMethods.InGroup(Common.Utilities.Constants.SCEEquivalenceEmployeesGroupName))// || HelperMethods.InGroup(Common.Utilities.Constants.ArabicProgEmployeeGroupName)
                                                                                                       //  || HelperMethods.InGroup(Common.Utilities.Constants.EuropeanProgEmployeeGroupName))
                {
                    Menu_Links.Items.Add(new MenuItem(HelperMethods.LocalizedText("ITWORX_MOEHEWF_SCE", "SCEClarificationRequests", (uint)LCID) +
                  /*"(" + BL.ClarificationRequests.GetAllClarificationRequests( LCID,GroupName).Count() + ")"*/string.Empty, "3"));
                }

                if (HelperMethods.InGroup(Common.Utilities.Constants.ReassignEmployees))// || HelperMethods.InGroup(Common.Utilities.Constants.ArabicProgEmployeeGroupName)
                                                                                                       //  || HelperMethods.InGroup(Common.Utilities.Constants.EuropeanProgEmployeeGroupName))
                {
                    Menu_Links.Items.Add(new MenuItem(HelperMethods.LocalizedText("ITWORX_MOEHEWF_SCE", "SCEReassignRequests", (uint)LCID) + string.Empty, "4"));
               
                }
            }
            catch (Exception ex)
            {
                Logging.GetInstance().LogException(ex);
            }
            finally
            {

                Logging.GetInstance().Debug("Exit EmployeeDashboard.BindbtnsCount");
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
                case "4":
                    controlPath = BASE_PATH + "ReassignRequests.ascx";
                    break;

                default:
                    controlPath = BASE_PATH + "NewRequests.ascx";
                    break;
            }

            LastLoadedControl = controlPath;
            if (SPGroupName.Contains(","))
            {
                var stringList = SPGroupName.Split(',').ToList();
                foreach (var item in stringList)
                {
                    if (HelperMethods.InGroup(item))
                    {
                        LoadUserControl(item);
                    }
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
                    controlPath = "SCEMyRequests";// BASE_PATH + "NewRequests.ascx";
                    break;
                case "1":
                    controlPath = BASE_PATH + "SearchRequests.ascx";
                    break;
                case "2":
                    controlPath = BASE_PATH + "ExceptionRequests.ascx";
                    break;

                default:
                    controlPath = "SCEMyRequests";//BASE_PATH + "NewRequests.ascx";
                    break;
            }

            LastLoadedControl = controlPath;
            if (SPGroupName.Contains(","))
            {
                var stringList = SPGroupName.Split(',').ToList();
                foreach (var item in stringList)
                {
                    if (HelperMethods.InGroup(item))
                    {
                        LoadUserControl(item);
                    }
                }
            }
            else
                LoadUserControl(SPGroupName);

            if (menu.Value == "0")
            {
                Menu_Links.Visible = true;
                Menu_Links.Items[0].Selected = true;
            }
            else
                Menu_Links.Visible = false;

        }

    }
}
