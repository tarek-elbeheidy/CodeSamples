using Microsoft.SharePoint;
using MOEHE.PSPES.Models;
using MOEHE.PSPES.Repository;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Reflection;
using System.Threading;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace MOEHE.PSPES.Webparts.ScheduleWindow
{
    public partial class ScheduleWindowUserControl : UserControl
    {
        public bool isMinistryUser { get; set; }
        public bool isSchoolUser { get; set; }
        private void page_Init(object sender, EventArgs e)
        {
            //Set the cuklture of the thread as to get the correct language from the resource file
            if (CultureInfo.CurrentUICulture.Name == "ar-sa" || CultureInfo.CurrentCulture.Name == "ar-sa")
            {
                Thread.CurrentThread.CurrentCulture = CultureInfo.CreateSpecificCulture("ar-sa");
                Thread.CurrentThread.CurrentCulture.DateTimeFormat = CultureInfo.CreateSpecificCulture("en-US").DateTimeFormat;
                Thread.CurrentThread.CurrentUICulture = new System.Globalization.CultureInfo("ar-sa");
                Thread.CurrentThread.CurrentUICulture.DateTimeFormat = CultureInfo.CreateSpecificCulture("en-US").DateTimeFormat;
            }
        }
        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {

                isMinistryUser = SharePointUtilityRepository.IsUserInGroup(PSPESConstants.PrivateSchoolOfficeSharePointGroup, SPContext.Current.Site.RootWeb.CurrentUser.LoginName);
                isSchoolUser = SharePointUtilityRepository.IsUserInGroup(PSPESConstants.PrivateSchoolUsersSharePointGroup, SPContext.Current.Site.RootWeb.CurrentUser.LoginName);
                UserHelper userhelper = ADutility.GetUserDetails(SPContext.Current.Site.RootWeb.CurrentUser);
                if (isSchoolUser)
                {
                    string currentYear = DateTime.Now.Year.ToString();
                    DateTime nextYear = DateTime.Now.AddYears(1);
                    txtTerm.Text = currentYear + " (" + currentYear + " - " + nextYear.Year.ToString() + ")";
                    List<SchoolModel> CurrentSchool = new List<SchoolModel>();
                    string CurrentSchoolCode = userhelper.DepartmentID;
                    hdnSchoolCode.Value = CurrentSchoolCode;
                    MOE_SCHOOL_Model SchoolInfo = MOE_SCHOOL_Repository.GetSchoolInfo(nextYear.Year.ToString(), CurrentSchoolCode, "false").Result;
                    SchoolModel schoolModel = new SchoolModel();
                    if (CultureInfo.CurrentUICulture.Name.ToLower() == "ar-sa" || CultureInfo.CurrentCulture.Name.ToLower() == "ar-sa")
                    {
                        schoolModel = new SchoolModel { ShcoolCode = CurrentSchoolCode, ArabicShcoolName = string.Format("{0} - {1}", SchoolInfo.MOE_SCHOOL_NAME_ARA.ToString(), CurrentSchoolCode) };
                        ddlSchool.DataTextField = "ArabicShcoolName";
                    }
                    else
                    {
                        schoolModel = new SchoolModel { ShcoolCode = CurrentSchoolCode, EnglishShcoolName = string.Format("{0} - {1}", CurrentSchoolCode, SchoolInfo.MOE_SCHOOL_NAME_ENG.ToString()) };
                        ddlSchool.DataTextField = "EnglishShcoolName";
                    }

                    ddlSchool.DataValueField = "ShcoolCode";
                    CurrentSchool.Add(schoolModel);

                    ddlSchool.DataSource = CurrentSchool;
                    ddlSchool.DataBind();

                    txtUserName.Text = SPContext.Current.Site.RootWeb.CurrentUser.Name;
                    txtDate.Text = DateTime.Now.ToShortDateString();
                    dtCloseDate.Value = DateTime.Now.ToShortDateString();
                }
                else
                {
                    Response.Redirect("/_layouts/15/closeConnection.aspx?loginasanotheruser=true", true);
                }
            }
        }

        protected void lbSubmit_Click(object sender, EventArgs e)
        {
            string errMsg = string.Empty;
            if (string.IsNullOrEmpty(txtDate.Text))
            {
                if (string.IsNullOrEmpty(errMsg))
                {
                    if (CultureInfo.CurrentUICulture.Name.ToLower() == "ar-sa" || CultureInfo.CurrentCulture.Name.ToLower() == "ar-sa")
                    {
                        errMsg = "Please select completion date";
                    }
                    else
                    {
                        errMsg = "Please select completion date";
                    }
                    CustomMessageBoxHelper.Show(this.Page, errMsg);
                }
            }
            else
            {
                DateTime dtCloseDate = Convert.ToDateTime(txtDate.Text);
                string loginName = SPContext.Current.Site.RootWeb.CurrentUser.LoginName.Split('|')[1];
                MOE_SCHOOL_Model oModel = new MOE_SCHOOL_Model
                {
                    MOE_SCHOOL_CODE = hdnSchoolCode.Value,
                    MOE_CLOSE_DATE = dtCloseDate,
                    MOE_USERID = loginName
                };
                var isSuccess = MOE_SCHOOL_Repository.Update(oModel);
                if (isSuccess != null)
                {
                    if (CultureInfo.CurrentUICulture.Name.ToLower() == "ar-sa" || CultureInfo.CurrentCulture.Name.ToLower() == "ar-sa")
                    {
                        CustomMessageBoxHelper.Show(this.Page, "Sent data to NSIS");
                    }
                    else
                    {
                        CustomMessageBoxHelper.Show(this.Page, "Sent data to NSIS");
                    }
                }
            }
        }

        protected void lbCancel_Click(object sender, EventArgs e)
        {
            dtCloseDate.Value = DateTime.Now.ToShortDateString();
        }
    }
}
