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
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;

namespace MOEHE.PSPES.Webparts.Search.SearchApplications
{
    public partial class SearchApplicationsUserControl : UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {


            if (!IsPostBack)
            {

                bool isMinistryUser = false;
                bool isSchoolUser = false;
                isMinistryUser = SharePointUtilityRepository.IsUserInGroup(PSPESConstants.PrivateSchoolOfficeSharePointGroup, SPContext.Current.Site.RootWeb.CurrentUser.LoginName);
                isSchoolUser = SharePointUtilityRepository.IsUserInGroup(PSPESConstants.PrivateSchoolUsersSharePointGroup, SPContext.Current.Site.RootWeb.CurrentUser.LoginName);
                UserHelper userhelper = ADutility.GetUserDetails(SPContext.Current.Site.RootWeb.CurrentUser);
                string schoolCode = userhelper.DepartmentID;

                string NextYear =
                    NextYear = string.Format("{0}", DateTime.Now.Year + 1);

                if (SPContext.Current.Site.RootWeb.CurrentUser.LoginName.Contains("t-m.elhana"))
                {
                    schoolCode = "30120";
                }



                if ((uint)CultureInfo.CurrentUICulture.LCID == 1025)
                {
                    BindingUtility.LoadTerms(TermDropDownList, PSPESConstants.ArabicLanguage);
                    BindingUtility.LoadApplicationStatus(ApplicationStatusDropDownList, PSPESConstants.ArabicLanguage);

                }
                else
                {
                    BindingUtility.LoadTerms(TermDropDownList, PSPESConstants.EnglishLanguage);
                    BindingUtility.LoadApplicationStatus(ApplicationStatusDropDownList, PSPESConstants.EnglishLanguage);

                }

                if (schoolCode == "-1")
                {
                    if ((uint)CultureInfo.CurrentUICulture.LCID == 1025)
                    {
                        CustomMessageBoxHelper.Show(this.Page, PSPESConstants.ArabicNoSchoolError);
                    }
                    else
                    {
                        CustomMessageBoxHelper.Show(this.Page, PSPESConstants.EnglishNoSchoolError);

                    }
                    SearchLinkButton.Visible = false;
                }
                else
                {
                    if (isMinistryUser)
                    {

                        ViewState["IsMinisteryUser"] = "true";
                        SchoolCodesDropDownList.Enabled = true;




                        List<MOE_SCHOOL_Model> schools = MOE_SCHOOL_Repository.GetSchools(NextYear, "false").Result;

                        if (schools.Count > 0)
                        {
                            if ((uint)CultureInfo.CurrentUICulture.LCID == 1025)
                            {

                                BindingUtility.LoadSchools(schools, SchoolCodesDropDownList, PSPESConstants.ArabicLanguage);

                            }
                            else
                            {
                                BindingUtility.LoadSchools(schools, SchoolCodesDropDownList, PSPESConstants.EnglishLanguage);

                            }
                        }

                        else
                        {
                            DBOperationResult ReturnedResult2 = TransactionLogsRepository.Insert(new TransactionsLog { CreatedDate = DateTime.Now, ShortDescription = "Search Applications Control", Description = "Search Applications Page No Schools", UserID = SPContext.Current.Site.RootWeb.CurrentUser.LoginName }).Result;
                            if ((uint)CultureInfo.CurrentUICulture.LCID == 1033)
                            {
                                CustomMessageBoxHelper.Show(this.Page, "Service is currently unavaliable, please contact helpdesk");
                            }
                            else
                            {
                                // CustomMessageBoxHelper.Show(this.Page, "من فضلك ت");
                                CustomMessageBoxHelper.Show(this.Page, " الخدمة غير متاحة حاليا، برجاء التواصل مع ادارة الدعم الفني");

                            }
                            SearchLinkButton.Visible = false;
                        }
                    }

                    else if (isSchoolUser)
                    {


                        ViewState["IsMinisteryUser"] = "false";
                        SchoolCodesDropDownList.Enabled = false;


                        try
                        {
                            string CurrentSchoolCode = schoolCode;
                            MOE_SCHOOL_Model SchoolInfo = MOE_SCHOOL_Repository.GetSchoolInfo(NextYear, CurrentSchoolCode, "false").Result;
                            List<ListOfValues_Model> AllCurricullms = ListOfValues_Repository.GetListOfValues(PSPESConstants.CurriculumsCodesetID).Result;

                            List<SchoolModel> CurrentSchool = new List<SchoolModel>();
                            SchoolModel schoolModel = new SchoolModel();
                            //List<schoolGrade> schoolGrades = MOE_SCHOOL_Repository.GetSchoolGrade(NextYear, CurrentSchoolCode, "false").Result;


                            List<CurriculumModel> Curriculums = new List<CurriculumModel>();
                            if ((uint)CultureInfo.CurrentUICulture.LCID == 1025)
                            {
                                schoolModel = new SchoolModel { ShcoolCode = CurrentSchoolCode, ArabicShcoolName = string.Format("{0} - {1}", SchoolInfo.MOE_SCHOOL_NAME_ARA.ToString(), CurrentSchoolCode) };
                                SchoolCodesDropDownList.DataTextField = "ArabicShcoolName";

                                SchoolCodesDropDownList.DataValueField = "ShcoolCode";
                                //BindingUtility.LoadGrades(schoolGrades, SchoolGradesDropDownList, PSPESConstants.ArabicLanguage);





                            }
                            else
                            {
                                schoolModel = new SchoolModel { ShcoolCode = CurrentSchoolCode, EnglishShcoolName = string.Format("{0} - {1}", CurrentSchoolCode, SchoolInfo.MOE_SCHOOL_NAME_ENG.ToString()) };
                                SchoolCodesDropDownList.DataTextField = "EnglishShcoolName";

                                SchoolCodesDropDownList.DataValueField = "ShcoolCode";
                                //BindingUtility.LoadGrades(schoolGrades, SchoolGradesDropDownList, PSPESConstants.EnglishLanguage);



                            }




                            CurrentSchool.Add(schoolModel);

                            SchoolCodesDropDownList.DataSource = CurrentSchool;
                            SchoolCodesDropDownList.DataBind();

                            #region New Grades from SeatCapacity Table to include Newly added Grades
                            //tarek.elbeheidy 18.4.2018 => changed the way grades are loaded to show the newly added grades
                            List<V_Schools_Grades_Ages> SchoolGradesAndAges = V_Schools_Grades_Ages_Repository.GetSchoolGradesAndAges(int.Parse(TermDropDownList.SelectedValue), SchoolCodesDropDownList.SelectedValue).Result;

                            if (SchoolGradesAndAges != null && SchoolGradesAndAges.Count > 0)
                            {
                                List<V_Schools_Grades_Ages> SortedSchoolDataList = SchoolGradesAndAges.OrderBy(s => s.Weight).ToList();
                                if ((uint)CultureInfo.CurrentUICulture.LCID == 1025)
                                {
                                    BindingUtility.LoadGradesFromSeatCapacity(SortedSchoolDataList, SchoolGradesDropDownList, PSPESConstants.ArabicLanguage);
                                }
                                else
                                {
                                    BindingUtility.LoadGradesFromSeatCapacity(SortedSchoolDataList, SchoolGradesDropDownList, PSPESConstants.EnglishLanguage);
                                }
                            }
                            #endregion
                        }
                        catch (Exception ex)
                        {
                            DBOperationResult ReturnedResult2 = TransactionLogsRepository.Insert(new TransactionsLog { CreatedDate = DateTime.Now, ShortDescription = "Search Applications Control", Description = "Search Applications Page Load Exception: " + ex.Message, UserID = SPContext.Current.Site.RootWeb.CurrentUser.LoginName }).Result;
                            if ((uint)CultureInfo.CurrentUICulture.LCID == 1033)
                            {
                                CustomMessageBoxHelper.Show(this.Page, "Service is currently unavaliable, please contact helpdesk");
                            }
                            else
                            {
                                // CustomMessageBoxHelper.Show(this.Page, "من فضلك ت");
                                CustomMessageBoxHelper.Show(this.Page, " الخدمة غير متاحة حاليا، برجاء التواصل مع ادارة الدعم الفني");

                            }
                            SearchLinkButton.Visible = false;
                        }
                    }

                    else
                    {
                        if ((uint)CultureInfo.CurrentUICulture.LCID == 1025)
                        {
                            CustomMessageBoxHelper.Show(this.Page, "You do not have permission to access this page");
                        }
                        else
                        {
                            // CustomMessageBoxHelper.Show(this.Page, "من فضلك ت");
                            CustomMessageBoxHelper.Show(this.Page, "ليس لديك صلاحية الدخول لهذه الصفحه ");

                        }
                        SearchLinkButton.Visible = false;
                    }
                }



            }


        }


        protected void TermTextBox_TextChanged(object sender, EventArgs e)
        {
            bool isMinistryUser = false;
            bool isSchoolUser = false;
            isMinistryUser = SharePointUtilityRepository.IsUserInGroup(PSPESConstants.PrivateSchoolOfficeSharePointGroup, SPContext.Current.Site.RootWeb.CurrentUser.LoginName);
            isSchoolUser = SharePointUtilityRepository.IsUserInGroup(PSPESConstants.PrivateSchoolUsersSharePointGroup, SPContext.Current.Site.RootWeb.CurrentUser.LoginName);




            UserHelper userhelper = ADutility.GetUserDetails(SPContext.Current.Site.RootWeb.CurrentUser);
            string schoolCode = "-1";


            try
            {
                schoolCode = userhelper.DepartmentID;
            }
            catch { }

            if (isMinistryUser)
            {
                schoolCode = "";
            }

            if ((uint)CultureInfo.CurrentUICulture.LCID == 1025)
            {
                //  BindingUtility.LoadApplicationsData(ApplicationsDataRepeater, new ApplicationDataModel { SchoolName = schoolCode, SearchText = SearchTextBox.Text, Term = TermTextBox.Text, IsEnglish = false });
            }
            else
            {
                //BindingUtility.LoadApplicationsData(ApplicationsDataRepeater, new ApplicationDataModel { SchoolName = schoolCode, SearchText = SearchTextBox.Text, Term = TermTextBox.Text, IsEnglish = true });

            }
        }






        protected void SearchTextBox_TextChanged(object sender, EventArgs e)
        {
            bool isMinistryUser = false;
            bool isSchoolUser = false;
            isMinistryUser = SharePointUtilityRepository.IsUserInGroup(PSPESConstants.PrivateSchoolOfficeSharePointGroup, SPContext.Current.Site.RootWeb.CurrentUser.LoginName);
            isSchoolUser = SharePointUtilityRepository.IsUserInGroup(PSPESConstants.PrivateSchoolUsersSharePointGroup, SPContext.Current.Site.RootWeb.CurrentUser.LoginName);




            UserHelper userhelper = ADutility.GetUserDetails(SPContext.Current.Site.RootWeb.CurrentUser);
            string schoolCode = "-1";


            try
            {
                schoolCode = userhelper.DepartmentID;
            }
            catch { }

            if (isMinistryUser)
            {
                schoolCode = "";
            }

            if ((uint)CultureInfo.CurrentUICulture.LCID == 1025)
            {
                //      BindingUtility.LoadApplicationsData(ApplicationsDataRepeater, new ApplicationDataModel { SchoolName=schoolCode, SearchText=SearchTextBox.Text.ToUpper(), Term = TermTextBox.Text, IsEnglish = false });
            }
            else
            {
                //  BindingUtility.LoadApplicationsData(ApplicationsDataRepeater, new ApplicationDataModel { SchoolName = schoolCode, SearchText = SearchTextBox.Text.ToUpper(), Term = TermTextBox.Text, IsEnglish = true });

            }
        }

        protected void EditApplicationLinkButton_Click(object sender, EventArgs e)
        {
            RepeaterItem item = (sender as LinkButton).Parent as RepeaterItem;
            Label ApplicationRefNumberLabel = (Label)item.FindControl("ApplicationRefNoLabel");
            Label QIDLabel = (Label)item.FindControl("QIDLabel");

            try
            {
                string serviceURL = ConfigurationManager.AppSettings["EditApplicationPageLink"];
                CustomMessageBoxHelper.Show(this.Page, string.Format("QID:{0}  - Ref :{1}", QIDLabel.Text, ApplicationRefNumberLabel.Text));
                string EditApplicationPageLink = string.Format("{0}?ApplicationRefNumber={1}&QID={2}", serviceURL, ApplicationRefNumberLabel.Text, QIDLabel.Text);
                Response.Redirect(EditApplicationPageLink);
                //
            }
            catch { }
        }

        protected void CompleteApplicationLinkButton_Click(object sender, EventArgs e)
        {
            RepeaterItem item = (sender as LinkButton).Parent as RepeaterItem;
            Label ApplicationRefNumberLabel = (Label)item.FindControl("ApplicationRefNoLabel");
            Label QIDLabel = (Label)item.FindControl("QIDLabel");

            try
            {
                string serviceURL = ConfigurationManager.AppSettings["AddApplicationCompletionPageLink"];
                CustomMessageBoxHelper.Show(this.Page, string.Format("QID:{0}  - Ref :{1}", QIDLabel.Text, ApplicationRefNumberLabel.Text));
                //string AddApplicationPageLink = string.Format("{0}?ApplicationRefNumber={1}&StudentID={2}", serviceURL, ApplicationRefNumberLabel.Text,QIDLabel.Text);
                string AddApplicationPageLink = string.Format("{0}?ApplicationRefNumber={1}&QID={2}", serviceURL, ApplicationRefNumberLabel.Text, QIDLabel.Text);
                Response.Redirect(AddApplicationPageLink);
            }
            catch { }

        }

        protected void lnkSearchButton_Click(object sender, EventArgs e)
        {
            bool IsCompletedApplication = false;
            bool SeacrhByApplicationStatus = true;
            if (ApplicationStatusDropDownList.SelectedValue == "1")
            {
                IsCompletedApplication = true;
            }

            string QID = QIDTextBox.Text;
            string ApplicationRefNumber = ApplicationRefNumberTextBox.Text;
            string SchoolCode = SchoolCodesDropDownList.SelectedValue;
            bool searchByDate = false;
            DateTime ApplicationDate = DateTime.Now;
            try
            {
                ApplicationDate = DateTime.Parse(Request.Form["PaidFeesDateTime"].ToString());
                searchByDate = true;
            }
            catch { }
            string Grade = SchoolGradesDropDownList.SelectedValue;
            int Term = int.Parse(TermDropDownList.SelectedValue);
            bool SeacrhBySchoolCode = true;
            if (SchoolCode == "All" || SchoolCode == "")
            {
                SeacrhBySchoolCode = false;
            }
            bool SeacrhByGrade = true;
            if (Grade == "All" || Grade == "")
            {
                SeacrhByGrade = false;
            }

            SearchApplicationPageFilters searchApplicationPageFilters = new SearchApplicationPageFilters
            {
                IsCompletedApplication = IsCompletedApplication,
                SeacrhByStudentNationID = QID.Length > 1,
                SeacrhByApplicationReference = ApplicationRefNumber.Length > 0,
                SeacrhByAppliedDate = searchByDate,
                SeacrhByGrade = SeacrhByGrade,
                SeacrhByTerm = Term > 0,
                SearchBySchoolCode = SeacrhBySchoolCode,
                SchoolCode = SchoolCode,
                SeacrhByApplicationStatus = SeacrhByApplicationStatus,
                Term = Term.ToString(),
                Grade = Grade,
                ApplicationReference = ApplicationRefNumber,
                AppliedDate = ApplicationDate,
                StudentNationID = QID

            };

            if ((uint)CultureInfo.CurrentUICulture.LCID == 1025)
            {
                searchApplicationPageFilters.IsEnglish = false;
                BindingUtility.LoadApplicationsData(ApplicationsDataRepeater, searchApplicationPageFilters);
            }
            else
            {
                searchApplicationPageFilters.IsEnglish = true;

                BindingUtility.LoadApplicationsData(ApplicationsDataRepeater, searchApplicationPageFilters);

            }


        }

        protected void SchoolCodesDropDownList_SelectedIndexChanged(object sender, EventArgs e)
        {
            List<ListOfValues_Model> AllCurricullms = ListOfValues_Repository.GetListOfValues(PSPESConstants.CurriculumsCodesetID).Result;
            List<SupportingDocsModel> SchoolsWithDocumentName = new List<SupportingDocsModel>();
            bool isMinistryUser = true;

            string NextYear = string.Format("{0}", DateTime.Now.Year + 1);

            MOE_SCHOOL_Model SchoolInfo = MOE_SCHOOL_Repository.GetSchoolInfo(NextYear, SchoolCodesDropDownList.SelectedValue, "false").Result;

            if (SchoolCodesDropDownList.SelectedValue == "All")
            {


                SchoolGradesDropDownList.Items.Clear();

                List<MOE_SCHOOL_Model> schools = MOE_SCHOOL_Repository.GetSchools(NextYear, "false").Result;

                if ((uint)CultureInfo.CurrentUICulture.LCID == 1025)
                {
                    BindingUtility.LoadSchools(schools, SchoolCodesDropDownList, PSPESConstants.ArabicLanguage);

                }
                else
                {
                    BindingUtility.LoadSchools(schools, SchoolCodesDropDownList, PSPESConstants.EnglishLanguage);

                }
                //MOE_SCHOOL_Model SchoolInfo = MOE_SCHOOL_Repository.GetSchoolInfo(NextYear, SchoolCodesDropDownList.SelectedValue, "false").Result;


            }
            else
            {
                //tarek.elbeheidy 18.4.2018 => changed the way grades are loaded to show the newly added grades

                //List<schoolGrade> schoolGrades = MOE_SCHOOL_Repository.GetSchoolGrade(NextYear, SchoolCodesDropDownList.SelectedValue, "false").Result;

                #region New Grades from SeatCapacity Table to include Newly added Grades
                //=>tarek.elbeheidy 18.4.2018 adjusted the grades for preenrollment school to read from seat capacity insted of NSIS to reflect the newly added grades
                List<V_Schools_Grades_Ages> SchoolGradesAndAges = V_Schools_Grades_Ages_Repository.GetSchoolGradesAndAges(int.Parse(TermDropDownList.SelectedValue), SchoolCodesDropDownList.SelectedValue).Result;
                #endregion
                if (SchoolGradesAndAges != null && SchoolGradesAndAges.Count > 0)
                {
                    List<V_Schools_Grades_Ages> SortedSchoolDataList = SchoolGradesAndAges.OrderBy(s => s.Weight).ToList();
                    List<CurriculumModel> Curriculums = new List<CurriculumModel>();
                    if ((uint)CultureInfo.CurrentUICulture.LCID == 1025)
                    {
                        BindingUtility.LoadGradesFromSeatCapacity(SortedSchoolDataList, SchoolGradesDropDownList, PSPESConstants.ArabicLanguage);




                    }
                    else
                    {
                        BindingUtility.LoadGradesFromSeatCapacity(SortedSchoolDataList, SchoolGradesDropDownList, PSPESConstants.EnglishLanguage);



                    }
                }



            }
        }

        protected void ApplicationsDataRepeater_DataBinding(object sender, EventArgs e)
        {

        }
    }
}
