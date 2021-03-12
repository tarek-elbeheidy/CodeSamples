using MOEHE.PSPES.Models;
using MOEHE.PSPES.Repository;
using System.Linq;
using System;
using System.Collections.Generic;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Globalization;
using Microsoft.SharePoint;
using Microsoft.SharePoint.Utilities;
using System.Reflection;
using System.Resources;
using System.Web;
using Microsoft.SharePoint.WebControls;
using System.Configuration;
using System.Threading;

namespace MOEHE.PSPES.Webparts.AddApplication
{
    public class test
    {
        public string text { get; set; }
        public int value { get; set; }


    }



    public static class ControlExtensions
    {
        public static void Clear(this Control c)
        {
            if (c == null)
            {
                throw new ArgumentNullException("c");
            }
            if (c is TextBox)
            {
                TextBox tb = (TextBox)c;
                tb.Text = "";
            }
            else if (c is DropDownList)
            {
                DropDownList ddl = (DropDownList)c;
                ddl.SelectedIndex = -1;
            }
            else if (c is GridView)
            {
                GridView gv = (GridView)c;
                gv.Controls.Clear();
            }
            else if (c is DateTimeControl)
            {
                DateTimeControl dt = (DateTimeControl)c;
                dt.SelectedDate = DateTime.Now.Date;
            }

            else if (c is CheckBox)
            {
                CheckBox chk = (CheckBox)c;
                chk.Checked = false;
            }
            else if (c is HtmlInputText)
            {
                HtmlInputText input = (HtmlInputText)c;
                input.Value = "";
            }

            else if (c is HtmlTextArea)
            {
                HtmlTextArea input = (HtmlTextArea)c;
                input.Value = "";
            }

            //else if (c is FileUpload)
            //{
            //    FileUpload fu = (FileUpload)c;

            //}
            // etc....
        }
    }
    public partial class AddApplicationUserControl : UserControl
    {

        public int getWaitListNumber()
        {
            if (ViewState["WaitListNumber"] != null)
            {
                return int.Parse(ViewState["WaitListNumber"].ToString());
            }
            else
            {
                int currentApplicationsCount = MOE_APPLICATION_DATA_Repository.GetCurrentWaitListNumber(int.Parse(ddlPreEnrollmentTerm.SelectedValue), getSchoolCode(), ddlPreEnrolmentGrade.SelectedItem.Value.Split(';')[0]).Result;
                int WaitListNumber = currentApplicationsCount;// + 1;
                ViewState.Add("WaitListNumber", WaitListNumber);
                return WaitListNumber;

            }
        }

        public int VS_WaitListNumber
        {
            get
            {

                return int.Parse(ViewState["WaitListNumber"].ToString());
            }
            set
            {
                ViewState["WaitListNumber"] = value;
            }
        }

        //public string schoolCode = "10097";
        public List<ListOfValues_Model> employertypes_status_lov = new List<ListOfValues_Model>();

        List<MOE_GUARDIAN_DATA_Model> studentContacts = new List<MOE_GUARDIAN_DATA_Model>();
        List<MOE_GUARDIAN_DATA_Model> studentParents = new List<MOE_GUARDIAN_DATA_Model>();

        const string resourceFile = "MOEHE.PSPES";

        public ResourceManager rm = new ResourceManager("MOEHE.PSPES", Assembly.GetExecutingAssembly());
        uint currLocale = SPContext.Current.Web.RegionalSettings.LocaleId;
        private string getSchoolCode()
        {
            if (ViewState["schoolCode"] != null)
                return ViewState["schoolCode"].ToString();
            else
            {
                UserHelper userhelper = ADutility.GetUserDetails(SPContext.Current.Site.RootWeb.CurrentUser);
                string schoolCode = userhelper.DepartmentID;
                ViewState.Add("schoolCode", schoolCode);
                return schoolCode;
            }
        }
        /*private string getSchoolCode()
        {
            return "30173";
        }*/
        protected void Page_Load(object sender, EventArgs e)
        {

            //UserHelper userhelper = ADutility.GetUserDetails(SPContext.Current.Site.RootWeb.CurrentUser);
            string schoolCode = getSchoolCode();//userhelper.DepartmentID;
            //schoolCode = "30140";//userhelper.DepartmentID;
            if (schoolCode == "-1")
            {
                string errorMessage = "";
                if ((uint)CultureInfo.CurrentUICulture.LCID == 1025)
                {
                    errorMessage = PSPESConstants.ArabicNoSchoolError;
                }
                else
                {
                    errorMessage = PSPESConstants.EnglishNoSchoolError;
                }
                txtQID.Enabled = false;
                CustomMessageBoxHelper.Show(this.Page, errorMessage);
            }
            else
            {
                try
                {
                    ////Check if The enrollment is open for the school
                    //EnrollmentControllingModel enrollment_controlling_model = EnorllmentControllingRepository.GetEnrollmentControllingData(schoolCode).Result;
                    //if (enrollment_controlling_model != null && enrollment_controlling_model.SchoolCode == schoolCode)
                    //{
                    //    DateTime Today;
                    //    DateTime enrollmentFrom;
                    //    DateTime enrollmentTo;
                    //    if ((uint)CultureInfo.CurrentUICulture.LCID == 1025)
                    //    {
                    //        Today = DateTime.ParseExact(DateTime.Now.Date.ToShortDateString(), "dd/MM/yy", CultureInfo.InvariantCulture);
                    //        enrollmentFrom = DateTime.ParseExact(((DateTime)enrollment_controlling_model.FromDate).Date.ToShortDateString(), "dd/MM/yy", CultureInfo.InvariantCulture);
                    //        enrollmentTo = DateTime.ParseExact(((DateTime)enrollment_controlling_model.ToDate).Date.ToShortDateString(), "dd/MM/yy", CultureInfo.InvariantCulture);
                    //    }

                    //    else
                    //    {
                    //        Today = DateTime.ParseExact(DateTime.Now.Date.ToString("dd/MM/yy"), "dd/MM/yy", CultureInfo.InvariantCulture);
                    //        enrollmentFrom = DateTime.ParseExact(((DateTime)enrollment_controlling_model.FromDate).Date.ToString("dd/MM/yy"), "dd/MM/yy", CultureInfo.InvariantCulture);
                    //        enrollmentTo = DateTime.ParseExact(((DateTime)enrollment_controlling_model.ToDate).Date.ToString("dd/MM/yy"), "dd/MM/yy", CultureInfo.InvariantCulture);

                    //    }

                    //    if (Today < enrollmentFrom || Today > enrollmentTo)
                    //    {

                    //        //show error message of out of date range
                    //        string confirmationMessage = "";
                    //        if ((uint)CultureInfo.CurrentUICulture.LCID == 1025)
                    //        {
                    //            confirmationMessage = PSPESConstants.ArabicEnrollmentOutOfDateError;
                    //        }
                    //        else
                    //        {
                    //            confirmationMessage = PSPESConstants.EnglishEnrollmentOutOfDateError;
                    //        }
                    //        CustomMessageBoxHelper.Show(this.Page, confirmationMessage);
                    //        ddlPreEnrolmentGrade.SelectedIndex = 0;
                    //        txtAvailableSeatsInRequestedGrade.Value = "";
                    //        txtWaitListNumber.Value = "";
                    //        txtQID.Enabled = false;
                    //        return;
                    //    }
                    //}


                    if (!Page.IsPostBack)
                    {

                        try
                        {
                            //Old Term Code Generation
                            //ListItem nextyear = new ListItem(DateTime.Now.Year.ToString() + "-" + (DateTime.Now.Year + 1).ToString(), (DateTime.Now.Year + 1).ToString());
                            //ListItem currentyear = new ListItem((DateTime.Now.Year - 1).ToString() + "-" + (DateTime.Now.Year).ToString(), (DateTime.Now.Year).ToString());
                            //ListItem previousyear = new ListItem((DateTime.Now.Year - 2).ToString() + "-" + (DateTime.Now.Year - 1).ToString(), (DateTime.Now.Year - 1).ToString());
                            //ddlPreEnrollmentTerm.Items.Add(nextyear);
                            //ddlCurrentTerm.Items.Add(currentyear);
                            //ddlPreviousTerm.Items.Add(previousyear);
                            //End Old Term Code Generation

                            //New Term Code Generation

                            List<TermModel> AllTerms = TermRepository.GetTerms().Result;
                            TermModel PreEnrollmentTerm = AllTerms.Where(s => s.ACADEMIC_YEAR_DESC == "PREENROLLMENT").Select(d => d).FirstOrDefault();
                            TermModel CurrentTerm = AllTerms.Where(s => s.ACADEMIC_YEAR_DESC == "CURRENT").Select(d => d).FirstOrDefault();
                            TermModel PreviousTerm = AllTerms.Where(s => s.ACADEMIC_YEAR_DESC == "PREVIOUS").Select(d => d).FirstOrDefault();
                            ListItem nextyear = new ListItem(PreEnrollmentTerm.TermName, PreEnrollmentTerm.TermCode);
                            ListItem currentyear = new ListItem(CurrentTerm.TermName, CurrentTerm.TermCode);
                            ListItem previousyear = new ListItem(PreviousTerm.TermName, PreviousTerm.TermCode);
                            ddlPreEnrollmentTerm.Items.Add(nextyear);
                            ddlCurrentTerm.Items.Add(currentyear);
                            ddlPreviousTerm.Items.Add(previousyear);
                            //End New Term Code Generation






                            dtApplicationDate2.Value = DateTime.Now.Date.ToString("dd/MM/yyyy", CultureInfo.GetCultureInfo(2057));

                            getSchoolData(schoolCode);
                            getListofValues();
                        }
                        catch (Exception ex)
                        {
                            //DBOperationResult ReturnedResult2 = TransactionLogsRepository.Insert(new TransactionsLog { CreatedDate = DateTime.Now, ShortDescription = "Add Application Control", Description = "Add Application Page Load Exception: " + ex.Message, UserID = SPContext.Current.Site.RootWeb.CurrentUser.LoginName }).Result;
                            //if ((uint)CultureInfo.CurrentUICulture.LCID == 1033)
                            //{
                            //    CustomMessageBoxHelper.Show(this.Page, "Service is currently unavaliable, please contact helpdesk");

                            //}
                            //else
                            //{
                            //    // CustomMessageBoxHelper.Show(this.Page, "من فضلك ت");
                            //    CustomMessageBoxHelper.Show(this.Page, " الخدمة غير متاحة حاليا، برجاء التواصل مع ادارة الدعم الفني");

                            //}
                            //txtQID.Enabled = false;
                            //LinkButton1.Visible = false;
                        }
                    }
                }
                catch (Exception ex)
                {
                    if ((uint)CultureInfo.CurrentUICulture.LCID == 1033)
                    {
                        CustomMessageBoxHelper.Show(this.Page, "Service is currently unavaliable, please contact helpdesk");

                    }
                    else
                    {
                        // CustomMessageBoxHelper.Show(this.Page, "من فضلك ت");
                        CustomMessageBoxHelper.Show(this.Page, " الخدمة غير متاحة حاليا، برجاء التواصل مع ادارة الدعم الفني");

                    }
                    txtQID.Enabled = false;
                    LinkButton1.Visible = false;
                }

            }


        }

        private void getListofValues()
        {
            try
            {
                List<ListOfValues_Model> marital_status_lov = ListOfValues_Repository.GetListOfValues(PSPESConstants.MaritalStatusCodesetID).Result;
                List<ListOfValues_Model> relationship_status_lov = ListOfValues_Repository.GetListOfValues(PSPESConstants.RelationshipsCodesetID).Result;
                List<ListOfValues_Model> Irelationship_status_lov = ListOfValues_Repository.GetListOfValues(PSPESConstants.RelationshipsCodesetID).Result;
                List<ListOfValues_Model> Erelationship_status_lov = ListOfValues_Repository.GetListOfValues(PSPESConstants.RelationshipsCodesetID).Result;
                List<ListOfValues_Model> Nrelationship_status_lov = ListOfValues_Repository.GetListOfValues(PSPESConstants.RelationshipsCodesetID).Result;
                List<ListOfValues_Model> Curriculums_lov = ListOfValues_Repository.GetListOfValues(PSPESConstants.CurriculumsCodesetID).Result;
                employertypes_status_lov = ListOfValues_Repository.GetListOfValues(PSPESConstants.EmployerTypesCodesetID).Result;

                ViewState.Add("employertypes_status_lov", employertypes_status_lov);

                string dataTextField = "DescriptionEnglish";

                if ((uint)CultureInfo.CurrentUICulture.LCID == 1025)
                {
                    dataTextField = "DescriptionArabic";
                }


                if (marital_status_lov.Count > 0)
                {
                    ddlGuardianMaritalStatus.DataSource = marital_status_lov;
                    ddlGuardianMaritalStatus.DataTextField = dataTextField;
                    ddlGuardianMaritalStatus.DataValueField = "ID";
                    ddlGuardianMaritalStatus.DataBind();
                    ddlGuardianMaritalStatus.Items.Insert(0, new ListItem(Convert.ToString(this.GetGlobalResourceObject("MOEHE.PSPES", "PleaseSelect")), "-1"));
                }

                if (relationship_status_lov.Count > 0)
                {
                    ddlGuardianRelationship.DataSource = relationship_status_lov;
                    ddlGuardianRelationship.DataTextField = dataTextField;
                    ddlGuardianRelationship.DataValueField = "ID";
                    ddlGuardianRelationship.DataBind();
                    ddlGuardianRelationship.Items.Insert(0, new ListItem(Convert.ToString(this.GetGlobalResourceObject("MOEHE.PSPES", "PleaseSelect")), "-1"));
                }

                if (employertypes_status_lov.Count > 0)
                {
                    ddlGuardianEmployerType.DataSource = employertypes_status_lov;
                    ddlGuardianEmployerType.DataTextField = dataTextField;
                    ddlGuardianEmployerType.DataValueField = "ID";
                    ddlGuardianEmployerType.DataBind();
                    ddlGuardianEmployerType.Items.Insert(0, new ListItem(Convert.ToString(this.GetGlobalResourceObject("MOEHE.PSPES", "PleaseSelect")), "-1"));

                }

                if (Curriculums_lov.Count > 0)
                {
                    //filling currentCurriculum DropDown
                    ddlCurrentCurriculum.DataSource = Curriculums_lov;
                    ddlCurrentCurriculum.DataTextField = dataTextField;
                    ddlCurrentCurriculum.DataValueField = "ID";
                    ddlCurrentCurriculum.DataBind();
                    ddlCurrentCurriculum.Items.Insert(0, new ListItem(Convert.ToString(this.GetGlobalResourceObject("MOEHE.PSPES", "PleaseSelect")), "-1"));


                    //filling previousCurriculum DropDown
                    ddlPreviousCurriculum.DataSource = Curriculums_lov;
                    ddlPreviousCurriculum.DataTextField = dataTextField;
                    ddlPreviousCurriculum.DataValueField = "ID";
                    ddlPreviousCurriculum.DataBind();
                    ddlPreviousCurriculum.Items.Insert(0, new ListItem(Convert.ToString(this.GetGlobalResourceObject("MOEHE.PSPES", "PleaseSelect")), "-1"));

                }



                ViewState.Add("relationship_status_lov", relationship_status_lov);
                ViewState.Add("Irelationship_status_lov", Irelationship_status_lov);
                ViewState.Add("Erelationship_status_lov", Erelationship_status_lov);
                ViewState.Add("Nrelationship_status_lov", Nrelationship_status_lov);

            }
            catch (Exception ex) { }

        }

        private void getSchoolData(string schoolCode)
        {
            try
            {
                //txtPreEnrollmentSchool.Text += "," + schoolCode;
                MOE_SCHOOL_Model schoolInfo = MOE_SCHOOL_Repository.GetSchoolInfo(/*DateTime.Now.Year.ToString()*/TermRepository.GetTerms().Result.Where(s => s.ACADEMIC_YEAR_DESC == "CURRENT").Select(d => d).FirstOrDefault().TermCode, schoolCode, "false").Result;
                // txtPreEnrollmentSchool.Text += ",after calling school information get,";
                if (schoolInfo != null)
                {
                    try
                    {
                        //List<test> t = new List<test>() { new test { text = "text", value = 1 } };

                        if ((uint)CultureInfo.CurrentUICulture.LCID == 1025)
                        {
                            txtPreEnrollmentSchool.Text = schoolInfo.MOE_SCHOOL_NAME_ARA.ToString();
                        }
                        else
                        {
                            txtPreEnrollmentSchool.Text = schoolInfo.MOE_SCHOOL_NAME_ENG.ToString();
                        }

                        lblPreEnrollmentSchoolCurriculumID.Text = schoolInfo.MOE_SCHOOL_CURRICULUM_ID;
                        txtschoolgender.Value = schoolInfo.MOE_SCHOOL_GENDER.ToLower();

                        #region New Grades from SeatCapacity Table to include Newly added Grades
                        //=>tarek.elbeheidy 18.4.2018 adjusted the grades for preenrollment school to read from seat capacity insted of NSIS to reflect the newly added grades
                        List<V_Schools_Grades_Ages> SchoolGradesAndAges = V_Schools_Grades_Ages_Repository.GetSchoolGradesAndAges(int.Parse(ddlPreEnrollmentTerm.SelectedValue), schoolCode).Result;

                        if (SchoolGradesAndAges != null && SchoolGradesAndAges.Count > 0)
                        {
                            List<V_Schools_Grades_Ages> SortedSchoolDataList = SchoolGradesAndAges.OrderBy(s => s.Weight).ToList();
                            string dataTextField = "";
                            if ((uint)CultureInfo.CurrentUICulture.LCID == 1025)
                            {

                                dataTextField = "MOE_GRADE_DESC_ARA";
                            }
                            else
                            {
                                dataTextField = "MOE_GRADE_DESC_ENG";
                            }

                            ddlPreEnrolmentGrade.DataTextField = dataTextField;
                            ddlPreEnrolmentGrade.DataValueField = "GradeValue";
                            ddlPreEnrolmentGrade.DataSource = SortedSchoolDataList;
                            ddlPreEnrolmentGrade.DataBind();
                            ddlPreEnrolmentGrade.Items.Insert(0, new ListItem(Convert.ToString(this.GetGlobalResourceObject("MOEHE.PSPES", "PleaseSelect")), "0"));
                        }

                        #endregion
                        #region old Grades from NSIS
                        /*List<schoolGrade> SchoolDataList = new List<schoolGrade>();
                        SchoolDataList = schoolInfo.schoolGrades;

                        //List<schoolGrade> SortedSchoolDataList = new List<schoolGrade>();


                        //var SortedSchoolDataList = SchoolDataList.OrderBy(s => s.Grade.Where(p=>(s.Grade != "PK" && s.Grade != "KG")));

                        var newSortedList = SchoolDataList.Select(x => new
                        {
                            x.CurrentCapacity,
                            x.CurrentEnrollments,
                            x.Grade,
                            GradeMapping = getGradeMapping(x.Grade)
                        });


                        //var SortedSchoolDataList = SchoolDataList.OrderBy((s => (s.Grade != "KG" && s.Grade != "PK"))).ThenBy(s => s.Grade);
                        var SortedSchoolDataList = newSortedList.OrderBy(s => s.GradeMapping).Select(p => new schoolGrade { CurrentCapacity = p.CurrentCapacity, Grade = p.Grade, CurrentEnrollments = p.CurrentEnrollments });

                        //schoolGrade PKSchoolGrade = SortedSchoolDataList.ElementAt(1);
                        List<schoolGrade> newSortedSchoolGrades = SortedSchoolDataList.ToList();
                        //newSortedSchoolGrades.RemoveAt(1);

                        //newSortedSchoolGrades.Insert(0, PKSchoolGrade);

                        ddlPreEnrolmentGrade.DataTextField = "Grade";
                        ddlPreEnrolmentGrade.DataValueField = "Grade";
                        ddlPreEnrolmentGrade.DataSource = newSortedSchoolGrades;
                        ddlPreEnrolmentGrade.DataBind();*/
                        #endregion
                    }
                    catch (Exception ex)
                    {
                        DBOperationResult ReturnedResult2 = TransactionLogsRepository.Insert(new TransactionsLog { CreatedDate = DateTime.Now, ShortDescription = "Add Application Control", Description = "Add Application getSchoolData NSIS Exception: " + ex.Message, UserID = SPContext.Current.Site.RootWeb.CurrentUser.LoginName }).Result;
                        if ((uint)CultureInfo.CurrentUICulture.LCID == 1033)
                        {
                            CustomMessageBoxHelper.Show(this.Page, "No School Information returned from NSIS, please contact helpdesk");

                        }
                        else
                        {
                            // CustomMessageBoxHelper.Show(this.Page, "من فضلك ت");
                            CustomMessageBoxHelper.Show(this.Page, " لم يتم توفير معلومات المدرسة من نظام NSIS، برجاء التواصل مع ادارة الدعم الفني");

                        }
                        txtQID.Enabled = false;
                        LinkButton1.Visible = false;
                        return;
                    }
                    #region old grades age retrieve before 18.4.2018
                    /*
                     try
                     {
                         if (ddlPreEnrolmentGrade.Items.Count > 0)
                         {



                             foreach (ListItem item in ddlPreEnrolmentGrade.Items)
                             {
                                 MOE_AGE_VALIDATE_Model Age_Model = MOE_AGE_VALIDATE_Repository.GetGradeAgeRange(ddlPreEnrollmentTerm.SelectedValue,getSchoolCode(), "Private", lblPreEnrollmentSchoolCurriculumID.Text, item.Value).Result;
                                 if ((uint)CultureInfo.CurrentUICulture.LCID == 1025)
                                 {
                                     item.Text = Age_Model.MOE_GRADE_DESC_ARA;
                                 }
                                 else
                                 {
                                     item.Text = Age_Model.MOE_GRADE_DESC_ENG;
                                 }
                                 item.Value = item.Value + ";" + Age_Model.Weight.ToString();
                                 //item.Attributes.Add("Weight", Age_Model.Weight.ToString());
                             }





                         }

                         ddlPreEnrolmentGrade.Items.Insert(0, new ListItem(Convert.ToString(this.GetGlobalResourceObject("MOEHE.PSPES", "PleaseSelect")), "0"));

                     }
                     catch (Exception ex)
                     {
                         DBOperationResult ReturnedResult2 = TransactionLogsRepository.Insert(new TransactionsLog { CreatedDate = DateTime.Now, ShortDescription = "Add Application Control", Description = "Add Application getSchoolData School Grade-Age Mapping Exception, CurriculumID: " + lblPreEnrollmentSchoolCurriculumID.Text + ", Exception:" + ex.Message, UserID = SPContext.Current.Site.RootWeb.CurrentUser.LoginName }).Result;
                         if ((uint)CultureInfo.CurrentUICulture.LCID == 1033)
                         {
                             CustomMessageBoxHelper.Show(this.Page, "No School Grade Age Mapping Available, please contact helpdesk");

                         }
                         else
                         {
                             // CustomMessageBoxHelper.Show(this.Page, "من فضلك ت");
                             CustomMessageBoxHelper.Show(this.Page, " لم يتم توفير معلومات معادلة الصفوف والعمر للمدرسة ، برجاء التواصل مع ادارة الدعم الفني");

                         }
                         txtQID.Enabled = false;
                         LinkButton1.Visible = false;
                         return;
                     }

                     //ddlPreEnrolmentGrade.DataTextField = "text";
                     //ddlPreEnrolmentGrade.DataValueField = "value";
                     //ddlPreEnrolmentGrade.DataSource = t;
                     //ddlPreEnrolmentGrade.DataBind();


                     //ddlPreEnrolmentGrade.data
                     */
                    #endregion
                }
                else
                {
                    DBOperationResult ReturnedResult2 = TransactionLogsRepository.Insert(new TransactionsLog { CreatedDate = DateTime.Now, ShortDescription = "Add Application Control", Description = "Add Application getSchoolData NSIS school Null Exception, school code : " + schoolCode, UserID = SPContext.Current.Site.RootWeb.CurrentUser.LoginName }).Result;
                    if ((uint)CultureInfo.CurrentUICulture.LCID == 1033)
                    {
                        CustomMessageBoxHelper.Show(this.Page, "No School Information returned from NSIS, please contact helpdesk");

                    }
                    else
                    {
                        // CustomMessageBoxHelper.Show(this.Page, "من فضلك ت");
                        CustomMessageBoxHelper.Show(this.Page, " لم يتم توفير معلومات المدرسة من نظام NSIS، برجاء التواصل مع ادارة الدعم الفني");

                    }
                    txtQID.Enabled = false;
                    LinkButton1.Visible = false;
                    return;
                }
            }
            catch (Exception ex)
            {
                //DBOperationResult ReturnedResult2 = TransactionLogsRepository.Insert(new TransactionsLog { CreatedDate = DateTime.Now, ShortDescription = "Add Application Control", Description = "Add Application getSchoolData Exception: " + ex.Message, UserID = SPContext.Current.Site.RootWeb.CurrentUser.LoginName }).Result;
                //if ((uint)CultureInfo.CurrentUICulture.LCID == 1033)
                //{
                //    CustomMessageBoxHelper.Show(this.Page, "Service is currently unavaliable, please contact helpdesk");

                //}
                //else
                //{
                //    // CustomMessageBoxHelper.Show(this.Page, "من فضلك ت");
                //    CustomMessageBoxHelper.Show(this.Page, " الخدمة غير متاحة حاليا، برجاء التواصل مع ادارة الدعم الفني");

                //}
                //txtQID.Enabled = false;
                //LinkButton1.Visible = false;
            }

        }

        public string Hamada { get; set; }

        private int getGradeMapping(string gradeCode)
        {
            int gradeMapping = -20;
            switch (gradeCode)
            {
                case "N":
                    gradeMapping = -2; break;
                case "PK":
                    gradeMapping = -1; break;
                case "KG":
                    gradeMapping = 0; break;
                case "01":
                    gradeMapping = 1; break;
                case "02":
                    gradeMapping = 2; break;
                case "03":
                    gradeMapping = 3; break;
                case "04":
                    gradeMapping = 4; break;
                case "05":
                    gradeMapping = 5; break;
                case "06":
                    gradeMapping = 6; break;
                case "07":
                    gradeMapping = 7; break;
                case "08":
                    gradeMapping = 8; break;
                case "09":
                    gradeMapping = 9; break;
                case "10":
                    gradeMapping = 10; break;
                case "11":
                    gradeMapping = 11; break;
                case "12":
                    gradeMapping = 12; break;
                case "13":
                    gradeMapping = 13; break;

                default:
                    break;
            }

            return gradeMapping;

        }

        protected void txtQID_TextChanged(object sender, EventArgs e)
        {
            try
            {
                QID_Changed();
            }
            catch (Exception ex) { string message = ex.Message; }
        }

        private void QID_Changed()
        {

            try
            {
                if (txtQID.Text.Length == 11)
                {
                    string QID = txtQID.Text;
                    string schoolgender = txtschoolgender.Value;
                    string applicationDate = dtApplicationDate2.Value;
                    ClearControls();
                    txtQID.Text = QID;
                    txtschoolgender.Value = schoolgender;
                    dtApplicationDate2.Value = applicationDate;


                    List<SeatReservationFee> confirmedSeat = SeatReservationFeeRepository.CheckConfirmedApplications(txtQID.Text).Result;


                    if (confirmedSeat.Count > 0)
                    {
                        string confirmationMessage = "";
                        if ((uint)CultureInfo.CurrentUICulture.LCID == 1025)
                        {
                            confirmationMessage = PSPESConstants.ArabicConfirmedApplicationExistError;
                        }
                        else
                        {
                            confirmationMessage = PSPESConstants.EnglishConfirmedApplicationExistError;
                        }
                        ddlPreEnrolmentGrade.SelectedIndex = 0;
                        txtAvailableSeatsInRequestedGrade.Value = "";
                        txtWaitListNumber.Value = "";
                        txtQID.Text = "";
                        CustomMessageBoxHelper.Show(this.Page, confirmationMessage);
                        return;
                    }
                    else
                    {
                        #region  Bind Student Bio Information from MOI
                        MOE_BIO_DATA_Model Bio_Model = MOE_BIO_DATA_Repository.GetBioDataByQID(txtQID.Text).Result;
                        if (Bio_Model != null && (Bio_Model.MOE_ARABIC_NAME != null || Bio_Model.MOE_ENGLISH_NAME != null))
                        {
                            //tarek el beheidy 24.5.2018,  commenting gender check as moved to pre enrollment grade selected function

                            //if (txtschoolgender.Value == "mixed" || Bio_Model.MOE_GENDER.ToLower() == txtschoolgender.Value)
                            //{


                            if ((uint)CultureInfo.CurrentUICulture.LCID == 1025)
                            {
                                txtName.Text = Bio_Model.MOE_ARABIC_NAME;
                                txtNationality.Text = Bio_Model.MOE_COUNTRY_ARABIC_NAME;
                                txtName2.Text = Bio_Model.MOE_ARABIC_NAME;
                                txtNationality2.Text = Bio_Model.MOE_COUNTRY_ARABIC_NAME;
                                txtName3.Text = Bio_Model.MOE_ARABIC_NAME;
                                txtNationality3.Text = Bio_Model.MOE_COUNTRY_ARABIC_NAME;
                                txtName4.Text = Bio_Model.MOE_ARABIC_NAME;
                                txtNationality4.Text = Bio_Model.MOE_COUNTRY_ARABIC_NAME;
                            }

                            else
                            {
                                txtName.Text = Bio_Model.MOE_ENGLISH_NAME;
                                txtNationality.Text = Bio_Model.MOE_COUNTRY_ENGLISH_NAME;
                                txtName2.Text = Bio_Model.MOE_ENGLISH_NAME;
                                txtNationality2.Text = Bio_Model.MOE_COUNTRY_ENGLISH_NAME;
                                txtName3.Text = Bio_Model.MOE_ENGLISH_NAME;
                                txtNationality3.Text = Bio_Model.MOE_COUNTRY_ENGLISH_NAME;
                                txtName4.Text = Bio_Model.MOE_ENGLISH_NAME;
                                txtNationality4.Text = Bio_Model.MOE_COUNTRY_ENGLISH_NAME;
                            }
                            englishstudentname.Value = Bio_Model.MOE_ENGLISH_NAME;
                            arabicstudentname.Value = Bio_Model.MOE_ARABIC_NAME;

                            txtNationality.Attributes.Add("CountryCode", Bio_Model.MOE_COUNTRY_CODE.ToString());
                            txtGender.Text = Bio_Model.MOE_GENDER;
                            txtDOB.Text = Bio_Model.MOE_DOB;//.Date.ToString("dd/MM/yyyy");


                            txtQID2.Text = Convert.ToInt64(Bio_Model.NATIONAL_ID).ToString();

                            txtGender2.Text = Bio_Model.MOE_GENDER;
                            txtDOB2.Text = Bio_Model.MOE_DOB;//.Date.ToString("dd/MM/yyyy");


                            txtQID3.Text = Convert.ToInt64(Bio_Model.NATIONAL_ID).ToString();

                            txtGender3.Text = Bio_Model.MOE_GENDER;
                            txtDOB3.Text = Bio_Model.MOE_DOB;//.Date.ToString("dd/MM/yyyy");


                            txtQID4.Text = Convert.ToInt64(Bio_Model.NATIONAL_ID).ToString();

                            txtGender4.Text = Bio_Model.MOE_GENDER;
                            txtDOB4.Text = Bio_Model.MOE_DOB;//.Date.ToString("dd/MM/yyyy");


                            //txtCitzenship.Text = Bio_Model.MOE_COUNTRY_ENGLISH_NAME;
                            //}
                            //else
                            //{
                            //    string confirmationMessage = "";
                            //    if ((uint)CultureInfo.CurrentUICulture.LCID == 1025)
                            //    {
                            //        confirmationMessage = "هذا الطالب لا يمكن تسجيله  لعدم تطابق النوع (ذكر/أنثى) مع هذه المدرسة";
                            //    }
                            //    else
                            //    {
                            //        confirmationMessage = "This student can not be registered in this school for violating the Gender rule";
                            //    }
                            //    CustomMessageBoxHelper.Show(this.Page, confirmationMessage);

                            //    string schoolgender2 = txtschoolgender.Value;
                            //    string applicationDate2 = dtApplicationDate2.Value;
                            //    ClearControls();

                            //    txtschoolgender.Value = schoolgender2;
                            //    dtApplicationDate2.Value = applicationDate2;
                            //    txtQID.Text = "";
                            //    return;
                            //}

                        }
                        else
                        {
                            string confirmationMessage = "";
                            if ((uint)CultureInfo.CurrentUICulture.LCID == 1025)
                            {
                                confirmationMessage = "لا يوجد بيانات متاحة للطالب في وزارة الداخلية، برجاء التواصل مع ادارة الدعم الفني ";
                            }
                            else
                            {
                                confirmationMessage = "No available data for this student in Ministry of Interior, please contact helpdesk";
                            }
                            CustomMessageBoxHelper.Show(this.Page, confirmationMessage);
                            string schoolgender3 = txtschoolgender.Value;
                            string applicationDate3 = dtApplicationDate2.Value;
                            ClearControls();

                            txtschoolgender.Value = schoolgender3;
                            dtApplicationDate2.Value = applicationDate3;
                            txtQID.Text = "";
                            return;
                        }
                        #endregion


                        #region  Bind Student Academic Information from NSIS else keeb editable
                        MOE_APPL_ACAD_INFO_Model CurrentAcad_Model = MOE_APPL_ACAD_INFO_Repository.GetAcadInfoByQIDAndTerm(txtQID.Text, /*DateTime.Now.Year.ToString()*/TermRepository.GetTerms().Result.Where(s => s.ACADEMIC_YEAR_DESC == "CURRENT").Select(d => d).FirstOrDefault().TermCode).Result;
                        MOE_APPL_ACAD_INFO_Model PrevAcad_Model = MOE_APPL_ACAD_INFO_Repository.GetAcadInfoByQIDAndTerm(txtQID.Text, /*DateTime.Now.Year.ToString()*/TermRepository.GetTerms().Result.Where(s => s.ACADEMIC_YEAR_DESC == "PREVIOUS").Select(d => d).FirstOrDefault().TermCode).Result;
                        if (CurrentAcad_Model != null && CurrentAcad_Model.MOE_TERM != 0 && CurrentAcad_Model.MOE_TERM.ToString() != "")
                        {
                            if ((uint)CultureInfo.CurrentUICulture.LCID == 1025)
                            {
                                txtCurrentSchool.Text = CurrentAcad_Model.MOE_SCHOOL_ARABIC_NAME;
                                ddlCurrentCurriculum.SelectedValue = CurrentAcad_Model.MOE_SCHOOL_CURRICULUM_ID;
                                //txtCurrentCurriculum.Text = CurrentAcad_Model.MOE_SCHOOL_ARABIC_CURRICULUM;//needs to be updated with school curriculum from school model
                            }
                            else
                            {
                                txtCurrentSchool.Text = CurrentAcad_Model.MOE_SCHOOL_NAME;
                                ddlCurrentCurriculum.SelectedValue = CurrentAcad_Model.MOE_SCHOOL_CURRICULUM_ID;

                                //txtCurrentCurriculum.Text = CurrentAcad_Model.MOE_SCHOOL_CURRICULUM;//needs to be updated with school curriculum from school model
                            }

                            //txtCurrentTerm.Text = (CurrentAcad_Model.MOE_TERM-1).ToString()+"/"+CurrentAcad_Model.MOE_TERM.ToString();
                            //txtCurrentTerm.Attributes.Add("NSISTerm", CurrentAcad_Model.MOE_TERM.ToString());

                            string SpecialCurriculumIDs = ConfigurationManager.AppSettings["SpecialCurriculumIDs"];
                            if (!SpecialCurriculumIDs.Contains(ddlCurrentCurriculum.SelectedValue))
                            {


                                LoadGrades(CurrentAcad_Model.MOE_TERM.ToString(), CurrentAcad_Model.MOE_SCHOOL_CODE, ddlCurrentGrade, ddlCurrentCurriculum.SelectedValue);
                            }

                            else
                            {
                                LoadGradesWithCurriculumOnly(CurrentAcad_Model.MOE_TERM.ToString(), ddlCurrentGrade, ddlCurrentCurriculum.SelectedValue);
                            }

                            txtCurrentSchoolCode.Text = CurrentAcad_Model.MOE_SCHOOL_CODE;
                            // ddlCurrentGrade.SelectedValue = CurrentAcad_Model.MOE_GRADE;
                            ddlCurrentGrade.Items.Cast<ListItem>()
                                .Where(x => x.Value.Contains(CurrentAcad_Model.MOE_GRADE))
                                .LastOrDefault().Selected = true;

                            //txtCurrentGrade.Text = CurrentAcad_Model.MOE_GRADE.ToString();

                            if (CurrentAcad_Model.MOE_STU_RSLT == "Promoted")//ask about status values from NSIS
                            {
                                ddlCurrentYearResult.ClearSelection();
                                ddlCurrentYearResult.Items.FindByValue("True").Selected = true;
                                ddlCurrentYearResult.Enabled = false;
                            }
                            ddlCurrentYearResult.Enabled = false;

                            //disabling all fields after filled from NSIS                    
                            txtCurrentSchoolCode.Enabled = false;
                            ddlCurrentGrade.Enabled = false;
                            txtCurrentSchool.Enabled = false;
                            ddlCurrentCurriculum.Enabled = false;
                            ddlCurrentTerm.Enabled = false;


                        }
                        else //no NSIS Data
                        {
                            //Disable SchoolCode not to enter rubbish data
                            txtCurrentSchoolCode.Enabled = false;
                        }


                        if (PrevAcad_Model != null && PrevAcad_Model.MOE_TERM != 0 && PrevAcad_Model.MOE_TERM.ToString() != "")
                        {

                            if ((uint)CultureInfo.CurrentUICulture.LCID == 1025)
                            {
                                txtPreviousSchool.Text = PrevAcad_Model.MOE_SCHOOL_ARABIC_NAME;
                                ddlPreviousCurriculum.SelectedValue = PrevAcad_Model.MOE_SCHOOL_CURRICULUM_ID;
                                //txtPreviousCurriculum.Text = PrevAcad_Model.MOE_SCHOOL_ARABIC_CURRICULUM;//needs to be updated with school curriculum from school model
                            }
                            else
                            {
                                txtPreviousSchool.Text = PrevAcad_Model.MOE_SCHOOL_NAME;
                                ddlPreviousCurriculum.SelectedValue = PrevAcad_Model.MOE_SCHOOL_CURRICULUM_ID;

                                //txtPreviousCurriculum.Text = PrevAcad_Model.MOE_SCHOOL_CURRICULUM;//needs to be updated with school curriculum from school model
                            }
                            //txtPreviousTerm.Text = (PrevAcad_Model.MOE_TERM-1).ToString()+"/"+PrevAcad_Model.MOE_TERM.ToString();
                            //txtPreviousTerm.Attributes.Add("NSISTerm", PrevAcad_Model.MOE_TERM.ToString());
                            LoadGrades(PrevAcad_Model.MOE_TERM.ToString(), PrevAcad_Model.MOE_SCHOOL_CODE, ddlPreviousGrade, ddlPreviousCurriculum.SelectedValue);
                            txtPreviousSchoolCode.Text = PrevAcad_Model.MOE_SCHOOL_CODE;
                            //ddlPreviousGrade.SelectedValue = PrevAcad_Model.MOE_GRADE;
                            ddlPreviousGrade.Items.Cast<ListItem>()
                                .Where(x => x.Value.Contains(PrevAcad_Model.MOE_GRADE))
                                .LastOrDefault().Selected = true;
                            //txtPreviousGrade.Text = PrevAcad_Model.MOE_GRADE;

                            if (PrevAcad_Model.MOE_STU_RSLT == "Promoted")//ask about status values from NSIS
                            {
                                ddlPreviousYearResult.ClearSelection();
                                ddlPreviousYearResult.Items.FindByValue("True").Selected = true;
                                ddlPreviousYearResult.Enabled = false;
                            }


                            //disabling all fields after filled from NSIS                    
                            txtPreviousSchoolCode.Enabled = false;
                            //txtPreviousGrade.Enabled = false;
                            ddlPreviousGrade.Enabled = false;
                            txtPreviousSchool.Enabled = false;
                            ddlPreviousCurriculum.Enabled = false;
                            ddlPreviousTerm.Enabled = false;
                            //txtPreviousCurriculum.Enabled = false;
                            //txtPreviousTerm.Enabled = false;
                        }
                        else //no NSIS Data
                        {
                            //Disable SchoolCode not to enter rubbish data
                            txtPreviousSchoolCode.Enabled = false;
                        }
                        #endregion


                        #region Bind Student Parents / Guardian Data

                        studentContacts = MOE_GUARDIAN_DATA_Repository.GetStudentContactsInfo(txtQID.Text).Result;
                        studentParents = MOE_GUARDIAN_DATA_Repository.GetStudentContactsInfo(txtQID.Text).Result;

                        if (studentContacts.Count > 0)
                        {

                            MOE_GUARDIAN_DATA_Model guardian = studentContacts.Where(D => D.MOE_ISGUARDIAN == true).Select(D => D).FirstOrDefault();

                            if (guardian != null)
                            {
                                //MOE_BIO_DATA_Model guardian_bio = MOE_BIO_DATA_Repository.GetBioDataByQID(Convert.ToInt64(guardian.MOE_RELATED_QID).ToString()).Result;
                                //if(guardian_bio != null)
                                //{

                                if ((uint)CultureInfo.CurrentUICulture.LCID == 1025)
                                {
                                    GuardianName.Value = guardian.bio_data.MOE_ARABIC_NAME;
                                    GuardianCountry.Value = guardian.bio_data.MOE_COUNTRY_ARABIC_NAME;
                                }
                                else
                                {
                                    GuardianName.Value = guardian.bio_data.MOE_ENGLISH_NAME;
                                    GuardianCountry.Value = guardian.bio_data.MOE_COUNTRY_ENGLISH_NAME;
                                }
                                txtGuardianQID.Text = Convert.ToInt64(guardian.bio_data.NATIONAL_ID).ToString();

                                txtGuardianGender.Text = guardian.bio_data.MOE_GENDER;
                                englishguardianname.Value = guardian.bio_data.MOE_ENGLISH_NAME;
                                arabicguardianname.Value = guardian.bio_data.MOE_ARABIC_NAME;



                                //}
                                GuardianEmail.Value = guardian.MOE_EMAIL;
                                GuardianLandLine.Value = guardian.MOE_HOME_CONTACT_NBR;

                                // GuardianMobile.Value = guardian.MOE_MOBILE_CONTACT_NBR;

                                if (guardian.MOE_RELATIONSHIP_TYPE_ID != null)
                                {
                                    //ddlGuardianRelationship.Items.Insert(0, new ListItem(Convert.ToString(this.GetGlobalResourceObject("MOEHE.PSPES", "PleaseSelect")), "-1"));
                                    ddlGuardianRelationship.ClearSelection();
                                    ddlGuardianRelationship.Items.FindByValue(guardian.MOE_RELATIONSHIP_TYPE_ID).Selected = true;

                                }

                                if (guardian.MOE_MARITAL_STATUS_ID != null)
                                {
                                    //ddlGuardianMaritalStatus.Items.Insert(0, new ListItem(Convert.ToString(this.GetGlobalResourceObject("MOEHE.PSPES", "PleaseSelect")), "-1"));
                                    ddlGuardianMaritalStatus.ClearSelection();
                                    ddlGuardianMaritalStatus.Items.FindByValue(guardian.MOE_MARITAL_STATUS_ID).Selected = true;

                                }

                                if (guardian.MOE_EMPLOYER_TYPE_ID != null)
                                {
                                    //ddlGuardianEmployerType.Items.Insert(0, new ListItem(Convert.ToString(this.GetGlobalResourceObject("MOEHE.PSPES", "PleaseSelect")), "-1"));
                                    ddlGuardianEmployerType.ClearSelection();
                                    ddlGuardianEmployerType.Items.FindByValue(guardian.MOE_EMPLOYER_TYPE_ID).Selected = true;

                                }



                                FillEmployerData(guardian.MOE_EMPLOYER_TYPE_ID);
                                ddlGuardianEmployer.ClearSelection();
                                ddlGuardianEmployer.Items.FindByValue(guardian.MOE_EMPLOYER).Selected = true;
                                //ddlGuardianEmployer.SelectedItem.Value = guardian.MOE_EMPLOYER;

                                //remove guardian information from the list of contacts
                                studentContacts.Remove(guardian);

                            }

                            //rptParents.DataSource = studentContacts;
                            //rptParents.DataBind();



                            gvParents.DataSource = studentContacts;
                            gvParents.DataBind();

                            ViewState.Add("studentContacts", studentContacts);

                        }
                        #endregion

                        #region Bind Student Health Information from PHCC webservice else keep editable

                        MOE_HEALTH_DATA_Model health_Model = MOE_HEALTH_DATA_Repository.GetHealthInfoByQID(txtQID.Text).Result;
                        if (health_Model != null)
                        {
                            txtStudentHealthCard.Value = health_Model.MOE_HLTH_CARD_NBR;
                            txtHealthCenterName.Value = health_Model.MOE_HLTH_CTR_NAME;
                            //((HtmlInputCheckBox)this.FindControl("chkSpecialNeed")).Checked = health_Model.MOE_SPL_NEEDS;
                            //((HtmlInputCheckBox)this.FindControl("chkPHCCData")).Checked = health_Model.MOE_FIT_FOR_SCHOOLING;

                            bool specialNeed = health_Model.MOE_SPL_NEEDS.HasValue ? health_Model.MOE_SPL_NEEDS.Value : false;
                            if (specialNeed)
                                ddlSpecialNeed.Items.FindByValue(health_Model.MOE_SPL_NEEDS.Value.ToString()).Selected = true;
                            PHCCData.Checked = health_Model.MOE_FIT_FOR_SCHOOLING.HasValue ? health_Model.MOE_FIT_FOR_SCHOOLING.Value : false;


                        }
                        #endregion
                    }
                    //clear grade selection if any


                }

                else
                {
                    //ClearControls();
                    string confirmationMessage = "";
                    if ((uint)CultureInfo.CurrentUICulture.LCID == 1025)
                    {
                        confirmationMessage = "الرجاء ادخال رقم شخصي صحيح للطالب";
                    }
                    else
                    {
                        confirmationMessage = "Please enter a valid Student QID";
                    }
                    CustomMessageBoxHelper.Show(this.Page, confirmationMessage);
                    string schoolgender = txtschoolgender.Value;
                    string applicationDate = dtApplicationDate2.Value;
                    ClearControls();

                    txtschoolgender.Value = schoolgender;
                    dtApplicationDate2.Value = applicationDate;
                    txtQID.Text = "";
                    return;
                }
            }
            catch (Exception ex) { }

        }

        private void ClearControls()
        {

            string SchoolName = txtPreEnrollmentSchool.Text;

            foreach (Control c in this.Controls)
            {
                c.Clear();
            }

            txtPreEnrollmentSchool.Text = SchoolName;
            txtAvailableSeatsInRequestedGrade.Value = "";
            txtWaitListNumber.Value = "";
        }

        private void BindRequiredSupportedDocuments(string requestedGrade)
        {
            try
            {
                #region Bind Required Supporting Documents by School for the requested Grade

                //string SchoolCode = "10097";
                string Term = ddlPreEnrollmentTerm.SelectedItem.Value;
                string schoolCode = getSchoolCode();
                string Grade = requestedGrade;
                bool isMinistryUser = true;
                MOE_SCHOOL_Model SchoolInfo = MOE_SCHOOL_Repository.GetSchoolInfo(Term, schoolCode, "false").Result;

                //Then View  All schools Document type for specific grade
                if ((uint)CultureInfo.CurrentUICulture.LCID == 1025)
                {
                    BindingUtility.LoadRequiredSupportingDocs(gvRequiredDocuments, new SupportingDocsModel { SchoolCode = schoolCode, Term = Term, Grade = Grade, SeacrhByTermAndSchoolCodeAndGrade = true }, SchoolInfo, false, isMinistryUser, !isMinistryUser, SPContext.Current.Site.Url);
                }
                else
                {
                    BindingUtility.LoadRequiredSupportingDocs(gvRequiredDocuments, new SupportingDocsModel { SchoolCode = schoolCode, Term = Term, Grade = Grade, SeacrhByTermAndSchoolCodeAndGrade = true }, SchoolInfo, true, isMinistryUser, !isMinistryUser, SPContext.Current.Site.Url);


                }

                if (gvRequiredDocuments != null && gvRequiredDocuments.Rows != null && gvRequiredDocuments.Rows.Count == 0)
                {
                    pnlNoRequiredDocuments.Visible = true;
                }
                else
                    pnlNoRequiredDocuments.Visible = false;

                #endregion
            }
            catch (Exception ex) { }

        }

        protected void ddlPreEnrolmentGrade_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                string schoolCode = getSchoolCode();
                bool ApplicationExist = MOE_APPLICATION_DATA_Repository.CheckApplicationExist(int.Parse(ddlPreEnrollmentTerm.SelectedValue), schoolCode, ddlPreEnrolmentGrade.SelectedValue.Split(';')[0], txtQID.Text).Result;

                if (ApplicationExist)
                {

                    string confirmationMessage = "";
                    if ((uint)CultureInfo.CurrentUICulture.LCID == 1025)
                    {
                        confirmationMessage = PSPESConstants.ArabicApplicationExistError;
                    }
                    else
                    {
                        confirmationMessage = PSPESConstants.EnglishApplicationExistError;
                    }
                    ddlPreEnrolmentGrade.SelectedIndex = 0;
                    txtAvailableSeatsInRequestedGrade.Value = "";
                    txtWaitListNumber.Value = "";
                    CustomMessageBoxHelper.Show(this.Page, confirmationMessage);


                }
                else
                {
                    //Check if The enrollment is open for the student nationality & selected grade or not


                    EnrollmentControllingModel enrollment_controlling_model = EnorllmentControllingRepository.GetEnrollmentControllingData(schoolCode).Result;
                    MOE_ENROLLMENT_EXCEPTION_Model student_exception = MOE_ENROLLMENT_EXCEPTION_Repository.GetEnrollmentExceptionBySchoolAndGrade(txtQID.Text, int.Parse(ddlPreEnrollmentTerm.SelectedValue), schoolCode, ddlPreEnrolmentGrade.SelectedValue.Split(';')[0]).Result;
                    bool studentCanApply = false;
                    DateTime Today;
                    DateTime enrollmentFrom;
                    DateTime enrollmentTo;
                    DateTime exceptionExpiry = new DateTime();


                    //tarek el beheidy 24.5.2018 moved check for gender to here instead of QID Changed and added the check for Gender Exception
                    if ((uint)CultureInfo.CurrentUICulture.LCID == 1025)
                    {
                        Today = DateTime.ParseExact(DateTime.Now.Date.ToShortDateString(), "dd/MM/yy", CultureInfo.InvariantCulture);
                        if (student_exception != null && student_exception.MOE_EXCEPTION_EXPIRY != null)
                        {
                            exceptionExpiry = DateTime.ParseExact(((DateTime)student_exception.MOE_EXCEPTION_EXPIRY).Date.ToShortDateString(), "dd/MM/yy", CultureInfo.InvariantCulture);
                        }
                    }
                    else
                    {
                        Today = DateTime.ParseExact(DateTime.Now.Date.ToString("dd/MM/yy"), "dd/MM/yy", CultureInfo.InvariantCulture);
                        if (student_exception != null && student_exception.MOE_EXCEPTION_EXPIRY != null)
                        {
                            exceptionExpiry = DateTime.ParseExact(((DateTime)student_exception.MOE_EXCEPTION_EXPIRY).Date.ToString("dd/MM/yy"), "dd/MM/yy", CultureInfo.InvariantCulture);
                        }
                    }


                    if (txtschoolgender.Value == "mixed" || txtGender.Text.ToLower() == txtschoolgender.Value)
                    {
                        //do nothing, the student gender is matching
                    }
                    else
                    {

                        if (student_exception != null && student_exception.GENDER_EXCEPTION != null && student_exception.GENDER_EXCEPTION.Value == true && Today <= exceptionExpiry)
                        {
                            //do nothing, student has gender exception
                        }
                        else if (student_exception != null && student_exception.GENDER_EXCEPTION != null && student_exception.GENDER_EXCEPTION.Value == true && Today > exceptionExpiry)
                        {
                            string confirmationMessage = "";
                            if ((uint)CultureInfo.CurrentUICulture.LCID == 1025)
                            {
                                confirmationMessage = PSPESConstants.ArabicExceptionExpired;
                            }
                            else
                            {
                                confirmationMessage = PSPESConstants.EnglishExceptionExpired;
                            }
                            CustomMessageBoxHelper.Show(this.Page, confirmationMessage);

                            string schoolgender2 = txtschoolgender.Value;
                            string applicationDate2 = dtApplicationDate2.Value;
                            ClearControls();

                            txtschoolgender.Value = schoolgender2;
                            dtApplicationDate2.Value = applicationDate2;
                            txtQID.Text = "";
                            return;
                        }
                        else
                        {
                            string confirmationMessage = "";
                            if ((uint)CultureInfo.CurrentUICulture.LCID == 1025)
                            {
                                confirmationMessage = "هذا الطالب لا يمكن تسجيله  لعدم تطابق النوع (ذكر/أنثى) مع هذه المدرسة";
                            }
                            else
                            {
                                confirmationMessage = "This student can not be registered in this school for violating the Gender rule";
                            }
                            CustomMessageBoxHelper.Show(this.Page, confirmationMessage);

                            string schoolgender2 = txtschoolgender.Value;
                            string applicationDate2 = dtApplicationDate2.Value;
                            ClearControls();

                            txtschoolgender.Value = schoolgender2;
                            dtApplicationDate2.Value = applicationDate2;
                            txtQID.Text = "";
                            return;
                        }
                    }


                    if (enrollment_controlling_model != null && enrollment_controlling_model.SchoolCode == schoolCode)
                    {

                        if ((uint)CultureInfo.CurrentUICulture.LCID == 1025)
                        {
                            Today = DateTime.ParseExact(DateTime.Now.Date.ToShortDateString(), "dd/MM/yy", CultureInfo.InvariantCulture);
                            enrollmentFrom = DateTime.ParseExact(((DateTime)enrollment_controlling_model.FromDate).Date.ToShortDateString(), "dd/MM/yy", CultureInfo.InvariantCulture);
                            enrollmentTo = DateTime.ParseExact(((DateTime)enrollment_controlling_model.ToDate).Date.ToShortDateString(), "dd/MM/yy", CultureInfo.InvariantCulture);
                            if (student_exception != null && student_exception.MOE_EXCEPTION_EXPIRY != null)
                            {
                                exceptionExpiry = DateTime.ParseExact(((DateTime)student_exception.MOE_EXCEPTION_EXPIRY).Date.ToShortDateString(), "dd/MM/yy", CultureInfo.InvariantCulture);
                            }
                        }

                        else
                        {
                            Today = DateTime.ParseExact(DateTime.Now.Date.ToString("dd/MM/yy"), "dd/MM/yy", CultureInfo.InvariantCulture);
                            enrollmentFrom = DateTime.ParseExact(((DateTime)enrollment_controlling_model.FromDate).Date.ToString("dd/MM/yy"), "dd/MM/yy", CultureInfo.InvariantCulture);
                            enrollmentTo = DateTime.ParseExact(((DateTime)enrollment_controlling_model.ToDate).Date.ToString("dd/MM/yy"), "dd/MM/yy", CultureInfo.InvariantCulture);
                            if (student_exception != null && student_exception.MOE_EXCEPTION_EXPIRY != null)
                            {
                                exceptionExpiry = DateTime.ParseExact(((DateTime)student_exception.MOE_EXCEPTION_EXPIRY).Date.ToString("dd/MM/yy"), "dd/MM/yy", CultureInfo.InvariantCulture);
                            }

                        }





                        string studentCountryCode = txtNationality.Attributes["CountryCode"] != null ? txtNationality.Attributes["CountryCode"].ToString() : "-1";



                        if (Today < enrollmentFrom || Today > enrollmentTo)
                        {
                            //Tarek El Beheidy => 21.5.2018 , addded the check for exception  
                            if (student_exception != null && student_exception.ENROLLMENT_WHILE_CLOSED_EXCEPTION != null && student_exception.ENROLLMENT_WHILE_CLOSED_EXCEPTION.Value == true && Today <= exceptionExpiry)
                            {
                                //Do Nothing, the student has an exception
                            }
                            else if (student_exception != null && student_exception.ENROLLMENT_WHILE_CLOSED_EXCEPTION != null && student_exception.ENROLLMENT_WHILE_CLOSED_EXCEPTION.Value == true && Today > exceptionExpiry)
                            {
                                //exception expired
                                string confirmationMessage = "";
                                if ((uint)CultureInfo.CurrentUICulture.LCID == 1025)
                                {
                                    confirmationMessage = PSPESConstants.ArabicExceptionExpired;
                                }
                                else
                                {
                                    confirmationMessage = PSPESConstants.EnglishExceptionExpired;
                                }
                                CustomMessageBoxHelper.Show(this.Page, confirmationMessage);

                                string schoolgender2 = txtschoolgender.Value;
                                string applicationDate2 = dtApplicationDate2.Value;
                                //ClearControls();
                                ddlPreEnrolmentGrade.ClearSelection();
                                txtschoolgender.Value = schoolgender2;
                                dtApplicationDate2.Value = applicationDate2;
                                //txtQID.Text = "";
                                return;
                            }
                            else
                            {
                                //show error message of out of date range
                                string confirmationMessage = "";
                                if ((uint)CultureInfo.CurrentUICulture.LCID == 1025)
                                {
                                    confirmationMessage = PSPESConstants.ArabicEnrollmentOutOfDateError;
                                }
                                else
                                {
                                    confirmationMessage = PSPESConstants.EnglishEnrollmentOutOfDateError;
                                }
                                CustomMessageBoxHelper.Show(this.Page, confirmationMessage);
                                ddlPreEnrolmentGrade.SelectedIndex = 0;
                                txtAvailableSeatsInRequestedGrade.Value = "";
                                txtWaitListNumber.Value = "";
                                return;
                            }
                        }

                        if (studentCountryCode == "-1")
                        {
                            //show error message no country code from MOI
                            string confirmationMessage = "";
                            if ((uint)CultureInfo.CurrentUICulture.LCID == 1025)
                            {
                                confirmationMessage = PSPESConstants.ArabicEnrollmentMOICountryError;
                            }
                            else
                            {
                                confirmationMessage = PSPESConstants.EnglishEnrollmentMOICountryError;
                            }
                            CustomMessageBoxHelper.Show(this.Page, confirmationMessage);
                            ddlPreEnrolmentGrade.SelectedIndex = 0;
                            txtAvailableSeatsInRequestedGrade.Value = "";
                            txtWaitListNumber.Value = "";
                            return;

                        }
                        if (studentCountryCode != "-1" && !(enrollment_controlling_model.CountryCodes.Contains(studentCountryCode) || enrollment_controlling_model.CountryCodes == "ALL"))
                        {
                            //Tarek El Beheidy => 21.5.2018 , addded the check for exception
                            if (student_exception != null && student_exception.NATIONALITY_EXCEPTION != null && student_exception.NATIONALITY_EXCEPTION.Value == true && Today <= exceptionExpiry)
                            {
                                //Do Nothing, the student has an exception
                            }
                            else if (student_exception != null && student_exception.NATIONALITY_EXCEPTION != null && student_exception.NATIONALITY_EXCEPTION.Value == true && Today > exceptionExpiry)
                            {
                                //exception expired
                                string confirmationMessage = "";
                                if ((uint)CultureInfo.CurrentUICulture.LCID == 1025)
                                {
                                    confirmationMessage = PSPESConstants.ArabicExceptionExpired;
                                }
                                else
                                {
                                    confirmationMessage = PSPESConstants.EnglishExceptionExpired;
                                }
                                CustomMessageBoxHelper.Show(this.Page, confirmationMessage);

                                string schoolgender2 = txtschoolgender.Value;
                                string applicationDate2 = dtApplicationDate2.Value;
                                //ClearControls();
                                ddlPreEnrolmentGrade.ClearSelection();
                                txtschoolgender.Value = schoolgender2;
                                dtApplicationDate2.Value = applicationDate2;
                                //txtQID.Text = "";
                                return;
                            }
                            else
                            {
                                //show error message nationality not allowed
                                string confirmationMessage = "";
                                if ((uint)CultureInfo.CurrentUICulture.LCID == 1025)
                                {
                                    confirmationMessage = PSPESConstants.ArabicEnrollmentNationalityError;
                                }
                                else
                                {
                                    confirmationMessage = PSPESConstants.EnglishEnrollmentNationalityError;
                                }
                                CustomMessageBoxHelper.Show(this.Page, confirmationMessage);
                                ddlPreEnrolmentGrade.SelectedIndex = 0;
                                txtAvailableSeatsInRequestedGrade.Value = "";
                                txtWaitListNumber.Value = "";
                                return;
                            }
                        }
                        if (!(enrollment_controlling_model.GradeCodes.Contains(ddlPreEnrolmentGrade.SelectedValue.Split(';')[0]) || enrollment_controlling_model.GradeCodes == "ALL"))
                        {
                            if (student_exception != null && student_exception.MOE_GRADE != null && student_exception.MOE_GRADE == ddlPreEnrolmentGrade.SelectedValue.Split(';')[0] && Today <= exceptionExpiry)
                            {
                                //Do Nothing, the student has an exception
                            }
                            else if (student_exception != null && student_exception.MOE_GRADE != null && student_exception.MOE_GRADE == ddlPreEnrolmentGrade.SelectedValue.Split(';')[0] && Today <= exceptionExpiry)
                            {
                                //exception expired
                                string confirmationMessage = "";
                                if ((uint)CultureInfo.CurrentUICulture.LCID == 1025)
                                {
                                    confirmationMessage = PSPESConstants.ArabicExceptionExpired;
                                }
                                else
                                {
                                    confirmationMessage = PSPESConstants.EnglishExceptionExpired;
                                }
                                CustomMessageBoxHelper.Show(this.Page, confirmationMessage);

                                string schoolgender2 = txtschoolgender.Value;
                                string applicationDate2 = dtApplicationDate2.Value;
                               // ClearControls();

                                txtschoolgender.Value = schoolgender2;
                                dtApplicationDate2.Value = applicationDate2;
                                //txtQID.Text = "";
                                return;
                            }
                            else
                            {
                                //show error message grade not allowed
                                string confirmationMessage = "";
                                if ((uint)CultureInfo.CurrentUICulture.LCID == 1025)
                                {
                                    confirmationMessage = PSPESConstants.ArabicEnrollmentGradeError;
                                }
                                else
                                {
                                    confirmationMessage = PSPESConstants.EnglishEnrollmentGradeError;
                                }
                                CustomMessageBoxHelper.Show(this.Page, confirmationMessage);
                                ddlPreEnrolmentGrade.SelectedIndex = 0;
                                txtAvailableSeatsInRequestedGrade.Value = "";
                                txtWaitListNumber.Value = "";
                                return;
                            }
                        }
                        //begin of else
                        //else
                        //{
                        //check if the selected grade is available for this student based on Age Validation Rules or not
                        //if (ddlCurrentGrade.Items.Count > 0 && ddlCurrentGrade.SelectedIndex > 0)
                        //{
                        //    if (ddlPreEnrolmentGrade.SelectedIndex == ddlCurrentGrade.SelectedIndex || ddlPreEnrolmentGrade.SelectedIndex == ddlCurrentGrade.SelectedIndex + 1)
                        //    {


                        //Tarek El Beheidy => 4.4.2018  changing the age validation for handling same curriculum and no history check
                        //check if there is history from NSIS Or Not
                        int currentGradeWeight = -1;
                        int selectedGradeWeight = -1;
                        bool isValidGrade = false;
                        //if there is academic history
                        if (ddlCurrentCurriculum.SelectedIndex > 0)
                        {
                            //check if the current curriculum same as the pre enrollment school 
                            if (ddlCurrentCurriculum.SelectedValue == lblPreEnrollmentSchoolCurriculumID.Text)
                            {
                                //check if there is selected grade or not
                                if (ddlCurrentGrade.SelectedIndex > 0)
                                {
                                    currentGradeWeight = int.Parse(ddlCurrentGrade.SelectedValue.Split(';')[1]);
                                    selectedGradeWeight = int.Parse(ddlPreEnrolmentGrade.SelectedValue.Split(';')[1]);
                                    if (selectedGradeWeight == currentGradeWeight + 1)
                                    {
                                        isValidGrade = true;
                                        txtValidGrade.Value = "1";
                                    }
                                    //tarek el beheidy 22.5.2018, checking if this student has a repeat year exception for the selected grade or not
                                    else if (selectedGradeWeight == currentGradeWeight)
                                    {
                                        //tarek el beheidy 22.5.2018, checking if this student has a repeat year exception for the selected grade or not
                                        if (student_exception != null && student_exception.MOE_GRADE != null && student_exception.MOE_GRADE == ddlPreEnrolmentGrade.SelectedValue.Split(';')[0] && student_exception.REPEAT_YEAR_EXCEPTION != null && student_exception.REPEAT_YEAR_EXCEPTION.Value == true && Today <= exceptionExpiry)
                                        {
                                            //the student has a repeat year exception
                                            isValidGrade = true;
                                            txtValidGrade.Value = "1";
                                        }
                                        else if (student_exception != null && student_exception.MOE_GRADE != null && student_exception.MOE_GRADE == ddlPreEnrolmentGrade.SelectedValue.Split(';')[0] && student_exception.REPEAT_YEAR_EXCEPTION != null && student_exception.REPEAT_YEAR_EXCEPTION.Value == true && Today <= exceptionExpiry)
                                        {
                                            //exception expired
                                            string confirmationMessage = "";
                                            if ((uint)CultureInfo.CurrentUICulture.LCID == 1025)
                                            {
                                                confirmationMessage = PSPESConstants.ArabicExceptionExpired;
                                            }
                                            else
                                            {
                                                confirmationMessage = PSPESConstants.EnglishExceptionExpired;
                                            }
                                            CustomMessageBoxHelper.Show(this.Page, confirmationMessage);

                                            string schoolgender2 = txtschoolgender.Value;
                                            string applicationDate2 = dtApplicationDate2.Value;
                                            //ClearControls();
                                            ddlPreEnrolmentGrade.ClearSelection();
                                            txtschoolgender.Value = schoolgender2;
                                            dtApplicationDate2.Value = applicationDate2;
                                            //txtQID.Text = "";
                                            return;
                                        }
                                        else
                                        {
                                            //student trying to repeat the year error
                                            string confirmationMessage = "";
                                            if ((uint)CultureInfo.CurrentUICulture.LCID == 1025)
                                            {
                                                confirmationMessage = PSPESConstants.ArabicRepeatYearError;
                                            }
                                            else
                                            {
                                                confirmationMessage = PSPESConstants.EnglishRepeatYearError;
                                            }
                                            CustomMessageBoxHelper.Show(this.Page, confirmationMessage);

                                            string schoolgender2 = txtschoolgender.Value;
                                            string applicationDate2 = dtApplicationDate2.Value;
                                            //ClearControls();
                                            ddlPreEnrolmentGrade.ClearSelection();
                                            txtschoolgender.Value = schoolgender2;
                                            dtApplicationDate2.Value = applicationDate2;
                                            //txtQID.Text = "";
                                            return;
                                        }
                                    }
                                    else if (selectedGradeWeight < currentGradeWeight || selectedGradeWeight > currentGradeWeight + 1)
                                    {
                                        //tarek el beheidy 22.5.2018, checking if this student has an age exception for the selected grade or not
                                        if (student_exception != null && student_exception.MOE_GRADE != null && student_exception.MOE_GRADE == ddlPreEnrolmentGrade.SelectedValue.Split(';')[0] && student_exception.AGE_EXCEPTION != null && student_exception.AGE_EXCEPTION.Value == true && Today <= exceptionExpiry)
                                        {
                                            //the student has an age exception
                                            isValidGrade = true;
                                            txtValidGrade.Value = "1";
                                        }

                                        //tarek el beheidy 22.5.2018, checking if this student has an age exception for the selected grade or not
                                        else if (student_exception != null && student_exception.MOE_GRADE != null && student_exception.MOE_GRADE == ddlPreEnrolmentGrade.SelectedValue.Split(';')[0] && student_exception.AGE_EXCEPTION != null && student_exception.AGE_EXCEPTION.Value == true && Today > exceptionExpiry)
                                        {
                                            //exception expired
                                            string confirmationMessage = "";
                                            if ((uint)CultureInfo.CurrentUICulture.LCID == 1025)
                                            {
                                                confirmationMessage = PSPESConstants.ArabicExceptionExpired;
                                            }
                                            else
                                            {
                                                confirmationMessage = PSPESConstants.EnglishExceptionExpired;
                                            }
                                            CustomMessageBoxHelper.Show(this.Page, confirmationMessage);

                                            string schoolgender2 = txtschoolgender.Value;
                                            string applicationDate2 = dtApplicationDate2.Value;
                                            //ClearControls();
                                            ddlPreEnrolmentGrade.ClearSelection();
                                            txtschoolgender.Value = schoolgender2;
                                            dtApplicationDate2.Value = applicationDate2;
                                            //txtQID.Text = "";
                                            return;
                                        }
                                        else
                                        {
                                            isValidGrade = false;
                                            txtValidGrade.Value = "0";
                                        }
                                    }
                                    if (isValidGrade)
                                    {
                                        LoadPreEnrolmentGradeData(ddlPreEnrolmentGrade.SelectedItem.Value.Split(';')[0]);
                                        return;
                                    }
                                    else
                                    {
                                        string confirmationMessage = "";
                                        if ((uint)CultureInfo.CurrentUICulture.LCID == 1025)
                                        {
                                            confirmationMessage = PSPESConstants.ArabicAgeError;
                                        }
                                        else
                                        {
                                            confirmationMessage = PSPESConstants.EnglishAgeError;
                                        }
                                        CustomMessageBoxHelper.Show(this.Page, confirmationMessage);
                                        ddlPreEnrolmentGrade.SelectedIndex = 0;
                                        txtAvailableSeatsInRequestedGrade.Value = "";
                                        txtWaitListNumber.Value = "";
                                        return;

                                    }

                                }
                            }
                            //if the current curriculum is different from the pre enrollment school
                            else
                            {
                                if (ddlCurrentGrade.SelectedIndex > 0)
                                {
                                    currentGradeWeight = int.Parse(ddlCurrentGrade.SelectedValue.Split(';')[1]);
                                }
                                isValidGrade = ValidateAge(ddlPreEnrollmentTerm.SelectedValue, "Private", lblPreEnrollmentSchoolCurriculumID.Text, ddlPreEnrolmentGrade.SelectedValue.Split(';')[0], int.Parse(ddlPreEnrolmentGrade.SelectedValue.Split(';')[1]), currentGradeWeight);


                                if (isValidGrade)
                                {
                                    txtValidGrade.Value = "1";
                                    LoadPreEnrolmentGradeData(ddlPreEnrolmentGrade.SelectedItem.Value.Split(';')[0]);
                                    return;
                                }
                                else
                                {
                                    //tarek el beheidy 22.5.2018, checking if this student has an age exception for the selected grade or not
                                    if (student_exception != null && student_exception.MOE_GRADE != null && student_exception.MOE_GRADE == ddlPreEnrolmentGrade.SelectedValue.Split(';')[0] && student_exception.AGE_EXCEPTION != null && student_exception.AGE_EXCEPTION.Value == true && Today <= exceptionExpiry)
                                    {
                                        //the student has an age exception

                                        txtValidGrade.Value = "1";
                                    }
                                    else if (student_exception != null && student_exception.MOE_GRADE != null && student_exception.MOE_GRADE == ddlPreEnrolmentGrade.SelectedValue.Split(';')[0] && student_exception.AGE_EXCEPTION != null && student_exception.AGE_EXCEPTION.Value == true && Today > exceptionExpiry)
                                    {
                                        //exception expired
                                        string confirmationMessage = "";
                                        if ((uint)CultureInfo.CurrentUICulture.LCID == 1025)
                                        {
                                            confirmationMessage = PSPESConstants.ArabicExceptionExpired;
                                        }
                                        else
                                        {
                                            confirmationMessage = PSPESConstants.EnglishExceptionExpired;
                                        }
                                        CustomMessageBoxHelper.Show(this.Page, confirmationMessage);

                                        string schoolgender2 = txtschoolgender.Value;
                                        string applicationDate2 = dtApplicationDate2.Value;
                                        //ClearControls();
                                        ddlPreEnrolmentGrade.ClearSelection();
                                        txtschoolgender.Value = schoolgender2;
                                        dtApplicationDate2.Value = applicationDate2;
                                        //txtQID.Text = "";
                                        return;
                                    }
                                    else
                                    {


                                        txtValidGrade.Value = "0";

                                        string confirmationMessage = "";
                                        if ((uint)CultureInfo.CurrentUICulture.LCID == 1025)
                                        {
                                            confirmationMessage = PSPESConstants.ArabicAgeError;
                                        }
                                        else
                                        {
                                            confirmationMessage = PSPESConstants.EnglishAgeError;
                                        }
                                        CustomMessageBoxHelper.Show(this.Page, confirmationMessage);
                                        ddlPreEnrolmentGrade.SelectedIndex = 0;
                                        txtAvailableSeatsInRequestedGrade.Value = "";
                                        txtWaitListNumber.Value = "";
                                        return;
                                    }


                                }

                            }
                        }

                        //if no academic history
                        else
                        {
                            //check if the selected pre enrollment grade is PK,KG or 01 to validate only on age
                            if (ddlPreEnrolmentGrade.SelectedValue.ToLower().Contains("n") ||
                                ddlPreEnrolmentGrade.SelectedValue.ToLower().Contains("pk") ||
                                ddlPreEnrolmentGrade.SelectedValue.ToLower().Contains("kg") ||
                                ddlPreEnrolmentGrade.SelectedValue.ToLower().Contains("01"))
                            {
                                isValidGrade = ValidateAge(ddlPreEnrollmentTerm.SelectedValue, "Private", lblPreEnrollmentSchoolCurriculumID.Text, ddlPreEnrolmentGrade.SelectedValue.Split(';')[0], int.Parse(ddlPreEnrolmentGrade.SelectedValue.Split(';')[1]), currentGradeWeight);
                                if (isValidGrade)
                                {
                                    txtValidGrade.Value = "1";
                                    LoadPreEnrolmentGradeData(ddlPreEnrolmentGrade.SelectedItem.Value.Split(';')[0]);
                                    return;
                                }
                                else
                                {
                                    //tarek el beheidy 22.5.2018, checking if this student has an age exception for the selected grade or not
                                    if (student_exception != null && student_exception.MOE_GRADE != null && student_exception.MOE_GRADE == ddlPreEnrolmentGrade.SelectedValue.Split(';')[0] && student_exception.AGE_EXCEPTION != null && student_exception.AGE_EXCEPTION.Value == true && Today <= exceptionExpiry)
                                    {
                                        //the student has an age exception

                                        txtValidGrade.Value = "1";
                                    }
                                    else if (student_exception != null && student_exception.MOE_GRADE != null && student_exception.MOE_GRADE == ddlPreEnrolmentGrade.SelectedValue.Split(';')[0] && student_exception.AGE_EXCEPTION != null && student_exception.AGE_EXCEPTION.Value == true && Today > exceptionExpiry)
                                    {
                                        //exception expired
                                        string confirmationMessage = "";
                                        if ((uint)CultureInfo.CurrentUICulture.LCID == 1025)
                                        {
                                            confirmationMessage = PSPESConstants.ArabicExceptionExpired;
                                        }
                                        else
                                        {
                                            confirmationMessage = PSPESConstants.EnglishExceptionExpired;
                                        }
                                        CustomMessageBoxHelper.Show(this.Page, confirmationMessage);

                                        string schoolgender2 = txtschoolgender.Value;
                                        string applicationDate2 = dtApplicationDate2.Value;
                                        //ClearControls();
                                        ddlPreEnrolmentGrade.ClearSelection();
                                        txtschoolgender.Value = schoolgender2;
                                        dtApplicationDate2.Value = applicationDate2;
                                        //txtQID.Text = "";
                                        return;
                                    }
                                    else
                                    {


                                        txtValidGrade.Value = "0";

                                        string confirmationMessage = "";
                                        if ((uint)CultureInfo.CurrentUICulture.LCID == 1025)
                                        {
                                            confirmationMessage = PSPESConstants.ArabicAgeError;
                                        }
                                        else
                                        {
                                            confirmationMessage = PSPESConstants.EnglishAgeError;
                                        }
                                        CustomMessageBoxHelper.Show(this.Page, confirmationMessage);
                                        ddlPreEnrolmentGrade.SelectedIndex = 0;
                                        txtAvailableSeatsInRequestedGrade.Value = "";
                                        txtWaitListNumber.Value = "";
                                        return;
                                    }



                                }
                            }

                            else
                            {
                                //nothing to do, waiting for current curriculum and current grade to be entered
                            }
                        }




                        #region old Age Validation
                        /*
                        if (ddlCurrentGrade.SelectedIndex > 0)
                        {
                            currentGradeWeight = int.Parse(ddlCurrentGrade.SelectedValue.Split(';')[1]);

                        }
                        isValidGrade = ValidateAge(ddlPreEnrollmentTerm.SelectedValue, "Private", lblPreEnrollmentSchoolCurriculumID.Text, ddlPreEnrolmentGrade.SelectedValue.Split(';')[0], int.Parse(ddlPreEnrolmentGrade.SelectedValue.Split(';')[1]), currentGradeWeight);
                        if (isValidGrade)
                            LoadPreEnrolmentGradeData(ddlPreEnrolmentGrade.SelectedItem.Value.Split(';')[0]);
                        else
                        {
                            string confirmationMessage = "";
                            if ((uint)CultureInfo.CurrentUICulture.LCID == 1025)
                            {
                                confirmationMessage = PSPESConstants.ArabicAgeError;
                            }
                            else
                            {
                                confirmationMessage = PSPESConstants.EnglishAgeError;
                            }
                            CustomMessageBoxHelper.Show(this.Page, confirmationMessage);
                            ddlPreEnrolmentGrade.SelectedIndex = 0;
                            txtAvailableSeatsInRequestedGrade.Value = "";
                            txtWaitListNumber.Value = "";

                        }*/
                        #endregion
                        //}
                        //    else
                        //    {
                        //        string confirmationMessage = "";
                        //        if ((uint)CultureInfo.CurrentUICulture.LCID == 1025)
                        //        {
                        //            confirmationMessage = PSPESConstants.ArabicAgeError;
                        //        }
                        //        else
                        //        {
                        //            confirmationMessage = PSPESConstants.EnglishAgeError;
                        //        }
                        //        CustomMessageBoxHelper.Show(this.Page, confirmationMessage);
                        //        ddlPreEnrolmentGrade.SelectedIndex = 0;
                        //        txtAvailableSeatsInRequestedGrade.Value = "";
                        //        txtWaitListNumber.Value = "";

                        //    }
                        //}
                        //else
                        //{

                        //    bool isValidGrade = ValidateAge(ddlPreEnrollmentTerm.SelectedValue, "Private", lblPreEnrollmentSchoolCurriculumID.Text, ddlPreEnrolmentGrade.SelectedValue.Split(';')[0]);
                        //    if (isValidGrade)
                        //        LoadPreEnrolmentGradeData(ddlPreEnrolmentGrade.SelectedItem.Value);
                        //    else
                        //    {
                        //        string confirmationMessage = "";
                        //        if ((uint)CultureInfo.CurrentUICulture.LCID == 1025)
                        //        {
                        //            confirmationMessage = PSPESConstants.ArabicAgeError;
                        //        }
                        //        else
                        //        {
                        //            confirmationMessage = PSPESConstants.EnglishAgeError;
                        //        }
                        //        CustomMessageBoxHelper.Show(this.Page, confirmationMessage);
                        //        ddlPreEnrolmentGrade.SelectedIndex = 0;
                        //        txtAvailableSeatsInRequestedGrade.Value = "";
                        //        txtWaitListNumber.Value = "";

                        //    }
                        //}


                        //else
                        //{
                        //string confirmationMessage = "";
                        //if ((uint)CultureInfo.CurrentUICulture.LCID == 1025)
                        //{
                        //    confirmationMessage = PSPESConstants.ArabicAgeError;
                        //}
                        //else
                        //{
                        //    confirmationMessage = PSPESConstants.EnglishAgeError;
                        //}
                        //CustomMessageBoxHelper.Show(this.Page, confirmationMessage);
                        //ddlPreEnrolmentGrade.SelectedIndex = 0;
                        //lblAvailableSeatsInRequestedGrade.Text = "";
                        //lblWaitListNumber.Text = "";

                        //}


                        //}//end of else

                    }



                }








            }
            catch (Exception ex) { }

        }

        private bool ValidateAge(string term, string schoolType, string schoolCurriculum, string selectedGrade, int selectedGradeWeight, int currentGradeWeigh)
        {
            MOE_AGE_VALIDATE_Model validate_age_model = MOE_AGE_VALIDATE_Repository.GetGradeAgeRange(term, getSchoolCode(), schoolType, schoolCurriculum, selectedGrade).Result;

            bool isValidGrade = false;

            if (validate_age_model != null)
            {
                DateTime cut_off_date = DateTime.Parse(validate_age_model.MOE_CALCULATE_AGE_ASOF_DT + "-" + (int.Parse(term) - 1).ToString(), CultureInfo.InvariantCulture);
                DateTime studentDOB = DateTime.ParseExact(txtDOB.Text, "dd/MM/yyyy", CultureInfo.InvariantCulture);
                double StudentAgeDays = Age(cut_off_date, studentDOB);
                DateTime dt = new DateTime();
                //tarek.elbeheidy 3.4.2018  change how age is calculated and validated to accomodate months and days as well


                string[] gradeMinAge = validate_age_model.MOE_MIN_AGE_STRING.Split(',');
                string[] gradeMaxAge = validate_age_model.MOE_MAX_AGE_STRING.Split(',');

                DateTime BaseDate = new DateTime(1, 1, 1);
                DateTime gradeMinAgeDate = new DateTime();
                DateTime gradeMaxAgeDate = new DateTime();
                if (gradeMinAge.Length == 3 && gradeMaxAge.Length == 3)
                {
                    gradeMinAgeDate = BaseDate.AddYears(int.Parse(gradeMinAge[0])).AddMonths(int.Parse(gradeMinAge[1])).AddDays(int.Parse(gradeMinAge[2]));
                    gradeMaxAgeDate = BaseDate.AddYears(int.Parse(gradeMaxAge[0])).AddMonths(int.Parse(gradeMaxAge[1])).AddDays(int.Parse(gradeMaxAge[2]));


                }

                double gradeMinAgeDays = (gradeMinAgeDate - BaseDate).TotalDays;
                double gradeMaxAgeDays = (gradeMaxAgeDate - BaseDate).TotalDays;

                #region New Age Calculation

                if (StudentAgeDays > gradeMaxAgeDays || StudentAgeDays < gradeMinAgeDays)
                {


                    isValidGrade = false;
                }

                else if (StudentAgeDays <= gradeMaxAgeDays && StudentAgeDays >= gradeMinAgeDays)
                {
                    if (currentGradeWeigh != -1)
                    {


                        if (selectedGradeWeight < currentGradeWeigh || selectedGradeWeight > currentGradeWeigh + 1)
                        {
                            isValidGrade = false;
                        }

                        #region tarek el beheidy 31.5.2018, added check of same grade in different curriculums as it wasn not added before
                        //check for repeat year exception as selectedGradeWeight == currentGradeWeigh
                        else if (selectedGradeWeight == currentGradeWeigh)
                        {
                            MOE_ENROLLMENT_EXCEPTION_Model student_exception = MOE_ENROLLMENT_EXCEPTION_Repository.GetEnrollmentExceptionBySchoolAndGrade(txtQID.Text, int.Parse(ddlPreEnrollmentTerm.SelectedValue), getSchoolCode(), ddlPreEnrolmentGrade.SelectedValue.Split(';')[0]).Result;
                            DateTime Today;
                            DateTime exceptionExpiry = new DateTime();

                            if ((uint)CultureInfo.CurrentUICulture.LCID == 1025)
                            {
                                Today = DateTime.ParseExact(DateTime.Now.Date.ToShortDateString(), "dd/MM/yy", CultureInfo.InvariantCulture);
                                if (student_exception != null && student_exception.MOE_EXCEPTION_EXPIRY != null)
                                {
                                    exceptionExpiry = DateTime.ParseExact(((DateTime)student_exception.MOE_EXCEPTION_EXPIRY).Date.ToShortDateString(), "dd/MM/yy", CultureInfo.InvariantCulture);
                                }
                            }
                            else
                            {
                                Today = DateTime.ParseExact(DateTime.Now.Date.ToString("dd/MM/yy"), "dd/MM/yy", CultureInfo.InvariantCulture);
                                if (student_exception != null && student_exception.MOE_EXCEPTION_EXPIRY != null)
                                {
                                    exceptionExpiry = DateTime.ParseExact(((DateTime)student_exception.MOE_EXCEPTION_EXPIRY).Date.ToString("dd/MM/yy"), "dd/MM/yy", CultureInfo.InvariantCulture);
                                }
                            }

                            if (student_exception != null && student_exception.MOE_GRADE != null && student_exception.MOE_GRADE == ddlPreEnrolmentGrade.SelectedValue.Split(';')[0] && student_exception.REPEAT_YEAR_EXCEPTION != null && student_exception.REPEAT_YEAR_EXCEPTION.Value == true && Today <= exceptionExpiry)
                            {
                                //the student has a repeat year exception
                                isValidGrade = true;
                                txtValidGrade.Value = "1";
                            }
                            else if (student_exception != null && student_exception.MOE_GRADE != null && student_exception.MOE_GRADE == ddlPreEnrolmentGrade.SelectedValue.Split(';')[0] && student_exception.REPEAT_YEAR_EXCEPTION != null && student_exception.REPEAT_YEAR_EXCEPTION.Value == true && Today > exceptionExpiry)
                            {
                                //exception expired
                                string confirmationMessage = "";
                                if ((uint)CultureInfo.CurrentUICulture.LCID == 1025)
                                {
                                    confirmationMessage = PSPESConstants.ArabicExceptionExpired;
                                }
                                else
                                {
                                    confirmationMessage = PSPESConstants.EnglishExceptionExpired;
                                }
                                CustomMessageBoxHelper.Show(this.Page, confirmationMessage);

                                string schoolgender2 = txtschoolgender.Value;
                                string applicationDate2 = dtApplicationDate2.Value;
                                //ClearControls();
                                ddlPreEnrolmentGrade.ClearSelection();
                                txtschoolgender.Value = schoolgender2;
                                dtApplicationDate2.Value = applicationDate2;
                                //txtQID.Text = "";
                                //return;
                            }
                            else
                            {
                                isValidGrade = false;
                                txtValidGrade.Value = "0";
                            }

                            #endregion

                        }
                         else if (selectedGradeWeight == currentGradeWeigh + 1)
                        {
                            isValidGrade = true;
                        }
                    }
                    else
                    {
                        isValidGrade = true;
                    }
                }
                #endregion


                #region Old Age Calculation
                /*
                                if (StudentAge >= validate_age_model.MOE_MAX_AGE || StudentAge < validate_age_model.MOE_MIN_AGE)
                                {


                                    isValidGrade = false;
                                }

                                else if (StudentAge < validate_age_model.MOE_MAX_AGE && StudentAge >= validate_age_model.MOE_MIN_AGE)
                                {
                                    if (currentGradeWeigh != -1)
                                    {


                                        if (selectedGradeWeight < currentGradeWeigh || selectedGradeWeight > currentGradeWeigh + 1)
                                        {
                                            isValidGrade = false;
                                        }
                                        else
                                        {
                                            isValidGrade = true;
                                        }
                                    }
                                    else
                                    {
                                        isValidGrade = true;
                                    }
                                }*/

                #endregion
            }
            return isValidGrade;

        }
        public static double Age(DateTime cut_off_date, DateTime studentDOB)
        {

            double ageDays = (cut_off_date - studentDOB).TotalDays;
            //if (cut_off_date < studentDOB.AddYears(age)) age--;

            return ageDays;
        }

        private void LoadPreEnrolmentGradeData(string selectedGrade)
        {
            try
            {
                //get the brequested grade available seats, current wait list number and required supporting documents
                string schoolCode = getSchoolCode();
                List<SeatCapacityModel> SeatCapMd = SeatCapacityRepository.CheckExistsSeatCapacity(Convert.ToInt32(ddlPreEnrollmentTerm.SelectedValue), schoolCode, ddlPreEnrolmentGrade.SelectedValue.Split(';')[0]).Result;
                int CountofConfirmedApps = SeatReservationFeeRepository.GetCountOfApplientApplications(int.Parse(ddlPreEnrollmentTerm.SelectedValue), schoolCode, ddlPreEnrolmentGrade.SelectedValue.Split(';')[0]).Result;

                if (SeatCapMd.Count > 0)
                {
                    int Availableseats = SeatCapMd[0].MOE_SEAT_DISTRIBUTION - CountofConfirmedApps;
                    if (Availableseats < 0)
                        Availableseats = 0;
                    txtAvailableSeatsInRequestedGrade.Value = Availableseats.ToString();
                }
                int currentApplicationsCount = MOE_APPLICATION_DATA_Repository.GetCurrentWaitListNumber(int.Parse(ddlPreEnrollmentTerm.SelectedValue), schoolCode, ddlPreEnrolmentGrade.SelectedItem.Value.Split(';')[0]).Result;
                int WaitListNumber = currentApplicationsCount;// + 1;
                txtWaitListNumber.Value = WaitListNumber.ToString();

                if (ViewState["WaitListNumber"] != null)
                {
                    ViewState["WaitListNumber"] = WaitListNumber;
                }
                else
                {

                    ViewState.Add("WaitListNumber", WaitListNumber);


                }
                //VS_WaitListNumber = WaitListNumber;

                BindRequiredSupportedDocuments(ddlPreEnrolmentGrade.SelectedItem.Value.Split(';')[0]);
            }
            catch (Exception ex) { }

        }

        protected void ddlGuardianEmployerType_SelectedIndexChanged(object sender, EventArgs e)
        {
            FillEmployerData(ddlGuardianEmployerType.SelectedItem.Value);
            //List<CustomCodes> employers_lov = new List<CustomCodes>();
            //if (employertypes_status_lov != null)
            //{
            //    employers_lov = employers_lov.
            //}
        }

        private void FillEmployerData(string selectedEmployerType)
        {
            try
            {
                if (selectedEmployerType != "-1")
                {
                    string dataTextField = "DescriptionEnglish";

                    if ((uint)CultureInfo.CurrentUICulture.LCID == 1025)
                    {
                        dataTextField = "DescriptionArabic";
                    }



                    List<ListOfValues_Model> employertypes_status_lov = (List<ListOfValues_Model>)ViewState["employertypes_status_lov"];
                    ListOfValues_Model Employer_Type = employertypes_status_lov.Where(D => D.ID == selectedEmployerType).Select(D => D).FirstOrDefault();

                    if (Employer_Type != null)
                    {
                        if (Employer_Type.customCodes.Count > 0)
                        {
                            ddlGuardianEmployer.DataSource = Employer_Type.customCodes;
                            ddlGuardianEmployer.DataTextField = dataTextField;
                            ddlGuardianEmployer.DataValueField = "Code";
                            ddlGuardianEmployer.DataBind();

                            ddlGuardianEmployer.Items.Insert(0, new ListItem(Convert.ToString(this.GetGlobalResourceObject("MOEHE.PSPES", "PleaseSelect")), "-1"));
                        }
                    }
                }
                else
                {
                    ddlGuardianEmployer.Items.Clear();
                    ddlGuardianEmployer.Items.Insert(0, new ListItem(Convert.ToString(this.GetGlobalResourceObject("MOEHE.PSPES", "PleaseSelect")), "-1"));
                }
            }
            catch (Exception ex) { }

        }

        protected void gvParents_RowEditing(object sender, GridViewEditEventArgs e)
        {
            try
            {
                gvParents.EditIndex = e.NewEditIndex;
                gvParents.DataBind();
            }
            catch (Exception ex) { }


        }

        protected void gvParents_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            //studentContacts.Where(D => D.MOE_RELATED_QID == GridView1.DataKeys[e.RowIndex].Value.ToString()).Select(D => D).FirstOrDefault();
        }

        protected void gvParents_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            try
            {
                gvParents.EditIndex = -1;
                gvParents.DataBind();
            }
            catch (Exception ex) { }

        }

        protected void NtxtParentID_TextChanged(object sender, EventArgs e)
        {

        }

        protected void AddNewParent(object sender, EventArgs e)

        {

            try
            {

                MOE_GUARDIAN_DATA_Model newParent = new MOE_GUARDIAN_DATA_Model();
                newParent.MOE_RELATED_QID = Convert.ToInt64(((TextBox)gvParents.FooterRow.FindControl("NtxtParentID")).Text);
                newParent.MOE_ENGLISH_NAME = ((TextBox)gvParents.FooterRow.FindControl("NtxtParentName")).Text;
                newParent.MOE_RELATIONSHIP_TYPE_ID = ((DropDownList)gvParents.FooterRow.FindControl("NddlRelationship")).SelectedItem.Value;
                newParent.MOE_COUNTRY_ENGLISH_NAME = ((TextBox)gvParents.FooterRow.FindControl("NtxtCountry")).Text;
                newParent.MOE_MOBILE_CONTACT_NBR = ((TextBox)gvParents.FooterRow.FindControl("NtxtParentMobileNumber")).Text;
                newParent.MOE_EMAIL = ((TextBox)gvParents.FooterRow.FindControl("NtxtParentEmail")).Text;

                studentContacts = (List<MOE_GUARDIAN_DATA_Model>)ViewState["studentContacts"];
                studentContacts.Add(newParent);
                gvParents.DataSource = studentContacts;
                gvParents.DataBind();
                ViewState["studentContacts"] = studentContacts;

            }
            catch (Exception ex) { }

        }

        protected void EditParent(object sender, GridViewEditEventArgs e)

        {
            try
            {
                gvParents.EditIndex = e.NewEditIndex;
                studentContacts = (List<MOE_GUARDIAN_DATA_Model>)ViewState["studentContacts"];
                gvParents.DataSource = studentContacts;

                gvParents.DataBind();
                ViewState["studentContacts"] = studentContacts;
            }
            catch (Exception ex) { }


        }

        protected void CancelEdit(object sender, GridViewCancelEditEventArgs e)

        {
            try
            {
                gvParents.EditIndex = -1;
                studentContacts = (List<MOE_GUARDIAN_DATA_Model>)ViewState["studentContacts"];
                gvParents.DataSource = studentContacts;
                gvParents.DataBind();
                ViewState["studentContacts"] = studentContacts;
            }
            catch (Exception ex) { }


        }

        protected void UpdateParent(object sender, GridViewUpdateEventArgs e)

        {
            try
            {

                decimal ParentIDDecimal = decimal.Parse(((TextBox)gvParents.Rows[e.RowIndex].FindControl("ItxtParentID")).Text);

                string ParentID = decimal.Truncate(ParentIDDecimal).ToString();

                string ParentNewMobileNumber = ((TextBox)gvParents.Rows[e.RowIndex].FindControl("EtxtParentMobileNumber")).Text;

                string ParentNewEmail = ((TextBox)gvParents.Rows[e.RowIndex].FindControl("EtxtParentEmail")).Text;

                string ParentNewRelationshipID = ((DropDownList)gvParents.Rows[e.RowIndex].FindControl("EddlRelationship")).SelectedItem.Value;

                studentContacts = (List<MOE_GUARDIAN_DATA_Model>)ViewState["studentContacts"];
                MOE_GUARDIAN_DATA_Model parentToUpdate = studentContacts.Where(D => D.MOE_RELATED_QID == Convert.ToInt64(ParentID)).Select(D => D).FirstOrDefault();

                if (parentToUpdate != null)
                {

                    parentToUpdate.MOE_MOBILE_CONTACT_NBR = ParentNewMobileNumber;
                    parentToUpdate.MOE_EMAIL = ParentNewEmail;
                    parentToUpdate.MOE_RELATIONSHIP_TYPE_ID = ParentNewRelationshipID;
                }


                gvParents.EditIndex = -1;

                gvParents.DataSource = studentContacts;

                gvParents.DataBind();

                ViewState["studentContacts"] = studentContacts;
            }
            catch (Exception ex) { }


        }

        protected void DeleteParent(object sender, EventArgs e)

        {

            try
            {
                LinkButton lnkRemove = (LinkButton)sender;
                if (lnkRemove != null)
                {
                    studentContacts = (List<MOE_GUARDIAN_DATA_Model>)ViewState["studentContacts"];
                    MOE_GUARDIAN_DATA_Model parentToDelete = studentContacts.Where(D => D.MOE_RELATED_QID == Convert.ToInt64(lnkRemove.CommandArgument)).Select(D => D).FirstOrDefault();
                    if (parentToDelete != null)
                    {
                        studentContacts.Remove(parentToDelete);
                        gvParents.DataSource = studentContacts;

                        gvParents.DataBind();

                        ViewState["studentContacts"] = studentContacts;

                    }
                }
            }
            catch (Exception ex) { }


        }

        protected void gvParents_RowDataBound(object sender, GridViewRowEventArgs e)
        {

            try
            {
                List<ListOfValues_Model> Irelationship_status_lov = (List<ListOfValues_Model>)ViewState["Irelationship_status_lov"];
                List<ListOfValues_Model> Erelationship_status_lov = (List<ListOfValues_Model>)ViewState["Erelationship_status_lov"];
                string dataTextField = "DescriptionEnglish";


                if ((uint)CultureInfo.CurrentUICulture.LCID == 1025)
                {
                    dataTextField = "DescriptionArabic";
                }





                if (e.Row.RowType == DataControlRowType.DataRow && e.Row.RowState == DataControlRowState.Edit)
                {
                    DropDownList EddlRelationship = (e.Row.FindControl("EddlRelationship") as DropDownList);
                    if (Erelationship_status_lov.Count > 0)
                    {


                        EddlRelationship.DataSource = Erelationship_status_lov;
                        EddlRelationship.DataTextField = dataTextField;
                        EddlRelationship.DataValueField = "ID";
                        EddlRelationship.DataBind();

                        string ElblRelationID = (e.Row.FindControl("ElblRelationID") as Label).Text;
                        ListItem Efound = EddlRelationship.Items.FindByValue(ElblRelationID);
                        if (Efound != null)
                        {
                            Efound.Selected = true;
                        }
                    }
                    ViewState["Erelationship_status_lov"] = Erelationship_status_lov;
                }

                else if (e.Row.RowType == DataControlRowType.DataRow)
                {
                    ////Find the DropDownList in the Row
                    DropDownList IddlRelationship = (e.Row.FindControl("IddlRelationship") as DropDownList);



                    if (Irelationship_status_lov.Count > 0)
                    {
                        IddlRelationship.DataSource = Irelationship_status_lov;
                        IddlRelationship.DataTextField = dataTextField;
                        IddlRelationship.DataValueField = "ID";
                        IddlRelationship.DataBind();


                        string lblRelationID = (e.Row.FindControl("IlblRelationID") as Label).Text;
                        ListItem found = IddlRelationship.Items.FindByValue(lblRelationID);
                        if (found != null)
                        {
                            found.Selected = true;
                        }

                    }
                    ViewState["Irelationship_status_lov"] = Irelationship_status_lov;
                }



            }
            catch (Exception ex) { }




        }

        protected void gvParents_DataBound(object sender, EventArgs e)
        {
            try
            {
                DropDownList NddlRelationship = (gvParents.FooterRow.FindControl("NddlRelationship") as DropDownList);

                List<ListOfValues_Model> Nrelationship_status_lov = (List<ListOfValues_Model>)ViewState["Nrelationship_status_lov"];
                string dataTextField = "DescriptionEnglish";


                if ((uint)CultureInfo.CurrentUICulture.LCID == 1025)
                {
                    dataTextField = "DescriptionArabic";
                }

                if (Nrelationship_status_lov.Count > 0)
                {
                    NddlRelationship.DataSource = Nrelationship_status_lov;
                    NddlRelationship.DataTextField = dataTextField;
                    NddlRelationship.DataValueField = "ID";
                    NddlRelationship.DataBind();



                }
                ViewState["Nrelationship_status_lov"] = Nrelationship_status_lov;
            }
            catch (Exception ex) { }

        }

        protected void gvParents_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            CancelEdit(sender, e);
        }

        protected void gvParents_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "cmdDeleteParent")
            {
                try
                {
                    LinkButton lnkRemove = (LinkButton)sender;
                    if (lnkRemove != null)
                    {
                        studentContacts = (List<MOE_GUARDIAN_DATA_Model>)ViewState["studentContacts"];
                        MOE_GUARDIAN_DATA_Model parentToDelete = studentContacts.Where(D => D.MOE_RELATED_QID == Convert.ToInt64(lnkRemove.CommandArgument)).Select(D => D).FirstOrDefault();
                        if (parentToDelete != null)
                        {
                            studentContacts.Remove(parentToDelete);
                            gvParents.DataSource = studentContacts;

                            gvParents.DataBind();

                            ViewState["studentContacts"] = studentContacts;

                        }
                    }
                }
                catch (Exception ex) { }
            }
            else if (e.CommandName == "cmdAddNewPArent")
            {
                try
                {

                    MOE_GUARDIAN_DATA_Model newParent = new MOE_GUARDIAN_DATA_Model();
                    newParent.MOE_RELATED_QID = Convert.ToInt64(((TextBox)gvParents.FooterRow.FindControl("NtxtParentID")).Text);
                    newParent.MOE_ENGLISH_NAME = ((TextBox)gvParents.FooterRow.FindControl("NtxtParentName")).Text;
                    newParent.MOE_RELATIONSHIP_TYPE_ID = ((DropDownList)gvParents.FooterRow.FindControl("NddlRelationship")).SelectedItem.Value;
                    newParent.MOE_COUNTRY_ENGLISH_NAME = ((TextBox)gvParents.FooterRow.FindControl("NtxtCountry")).Text;
                    newParent.MOE_MOBILE_CONTACT_NBR = ((TextBox)gvParents.FooterRow.FindControl("NtxtParentMobileNumber")).Text;
                    newParent.MOE_EMAIL = ((TextBox)gvParents.FooterRow.FindControl("NtxtParentEmail")).Text;

                    studentContacts = (List<MOE_GUARDIAN_DATA_Model>)ViewState["studentContacts"];
                    studentContacts.Add(newParent);
                    gvParents.DataSource = studentContacts;
                    gvParents.DataBind();
                    ViewState["studentContacts"] = studentContacts;

                }
                catch (Exception ex) { }


            }
        }

        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            #region Alhanafi=>28-2-2018 Check if the application exist before save
            string schoolCode = getSchoolCode();
            bool ApplicationExist = MOE_APPLICATION_DATA_Repository.CheckApplicationExist(int.Parse(ddlPreEnrollmentTerm.SelectedValue), schoolCode, ddlPreEnrolmentGrade.SelectedValue.Split(';')[0], txtQID.Text).Result;


            #endregion

            if (ApplicationExist)
            {
                if ((uint)CultureInfo.CurrentUICulture.LCID == 1025)
                {
                    CustomMessageBoxHelper.Show(this.Page, PSPESConstants.ArabicApplicationExistError);
                }
                else
                {
                    CustomMessageBoxHelper.Show(this.Page, PSPESConstants.EnglishApplicationExistError);

                }
            }



            else
            {
                SaveApplication();


            }
        }

        private void SaveApplication()
        {
            DBOperationResult ApplicationInsertResult = new DBOperationResult();
            DBOperationResult CurrentAcadInfoResult = new DBOperationResult();
            try
            {
                #region Save Application Data
                string schoolCode = getSchoolCode();
                int currentApplicationsCount = 0;
                currentApplicationsCount = MOE_APPLICATION_DATA_Repository.GetCurrentWaitListNumber(int.Parse(ddlPreEnrollmentTerm.SelectedValue), schoolCode, ddlPreEnrolmentGrade.SelectedItem.Value.Split(';')[0]).Result;
                int WaitListNumber_beforeSave = currentApplicationsCount + 1;
                string Application_Reference_Number = ddlPreEnrollmentTerm.SelectedValue + "-" + schoolCode + "-" + ddlPreEnrolmentGrade.SelectedValue.Split(';')[0] + "-" + WaitListNumber_beforeSave.ToString();//txtWaitListNumber.Value;
                DBOperationResult ReturnedResult2 = TransactionLogsRepository.Insert(new TransactionsLog { CreatedDate = DateTime.Now, ShortDescription = "Add Application Control", Description = "Start Save Application Data", UserID = SPContext.Current.Site.RootWeb.CurrentUser.LoginName }).Result;
                try
                {
                    MOE_APPLICATION_DATA_Model application_Data_Model = new MOE_APPLICATION_DATA_Model
                    {
                        MOE_APPLIED_GRADE = ddlPreEnrolmentGrade.SelectedValue.Split(';')[0],
                        MOE_APPL_DATE = DateTime.ParseExact(dtApplicationDate2.Value, "dd/MM/yyyy", CultureInfo.GetCultureInfo(2057)),// Convert.ToDateTime(txtApplicationDate.Text),//dtApplicationDate.SelectedDate,

                        MOE_APPL_YEAR = (ddlPreEnrollmentTerm.SelectedValue != "-1") ? int.Parse(ddlPreEnrollmentTerm.SelectedValue) : -1,
                        MOE_AVAIL_TRANSPORT = ddlTransportation.SelectedValue == "true" ? true : false,
                        MOE_RESIDENTIAL_AREA = txtResedentialArea.Text,
                        NATIONAL_ID = (txtQID.Text != "") ? decimal.Parse(txtQID.Text) : 0,
                        MOE_TERM = (ddlPreEnrollmentTerm.SelectedValue != "-1") ? int.Parse(ddlPreEnrollmentTerm.SelectedValue) : -1,
                        MOE_SCHOOL_CODE = schoolCode,
                        //MOE_WAITLIST_NUMBER = (txtWaitListNumber.Value != "") ? int.Parse(txtWaitListNumber.Value) : -1,
                        //MOE_WAITLIST_NUMBER = (VS_WaitListNumber != 0) ? VS_WaitListNumber : -1,
                        MOE_WAITLIST_NUMBER = WaitListNumber_beforeSave,
                        MOE_APPL_REF_NBR = Application_Reference_Number,
                        MOE_TRANSACTION_DTTM = DateTime.Now,
                        MOE_USERID = SPContext.Current.Site.RootWeb.CurrentUser.LoginName,
                        IsActive = true,
                        MOE_STUDENT_NAME_ARA = arabicstudentname.Value,
                        MOE_STUDENT_NAME_ENG = englishstudentname.Value


                    };

                    ApplicationInsertResult = MOE_APPLICATION_DATA_Repository.Insert(application_Data_Model).Result;
                    ReturnedResult2 = TransactionLogsRepository.Insert(new TransactionsLog { CreatedDate = DateTime.Now, ShortDescription = "Add Application Control", Description = "finish Save Application Data", UserID = SPContext.Current.Site.RootWeb.CurrentUser.LoginName }).Result;
                }
                catch (Exception ex)
                {
                    ReturnedResult2 = TransactionLogsRepository.Insert(new TransactionsLog { CreatedDate = DateTime.Now, ShortDescription = "Add Application Control", Description = "Save Application Data Exception: " + ex.Message, UserID = SPContext.Current.Site.RootWeb.CurrentUser.LoginName }).Result;
                    string confirmationMessage = "";

                    if ((uint)CultureInfo.CurrentUICulture.LCID == 1025)
                    {

                        confirmationMessage = PSPESConstants.ArabicApplicationError;
                    }
                    else
                    {

                        confirmationMessage = PSPESConstants.EnglishApplicationError;
                    }
                    CustomMessageBoxHelper.Show(this.Page, confirmationMessage);
                    return;
                }

                if (ApplicationInsertResult.EnglishResult == PSPESConstants.InsertionDone)
                {
                    #region Save Application Academic Information Data
                    //saving the current year academic info
                    try
                    {
                        CurrentAcadInfoResult = new DBOperationResult();
                        if (txtCurrentSchool.Text != "")
                        {
                            ReturnedResult2 = TransactionLogsRepository.Insert(new TransactionsLog { CreatedDate = DateTime.Now, ShortDescription = "Add Application Control", Description = "Start Save Application Current Academic Data", UserID = SPContext.Current.Site.RootWeb.CurrentUser.LoginName }).Result;
                            MOE_APPL_ACAD_INFO_Model currentYearAcadInfo = new MOE_APPL_ACAD_INFO_Model()
                            {

                                MOE_APPL_REF_NBR = Application_Reference_Number,
                                MOE_GRADE = (ddlCurrentGrade.SelectedValue != "-1") ? ddlCurrentGrade.SelectedValue.Split(';')[0] : "",// txtCurrentGrade.Text,
                                MOE_SCHOOL_CODE = txtCurrentSchoolCode.Text,//, Needs to have school code 
                                MOE_SCHOOL_NAME = txtCurrentSchool.Text,
                                MOE_STU_RSLT = ddlCurrentYearResult.SelectedValue,
                                MOE_TERM = (ddlCurrentTerm.SelectedValue != "-1") ? int.Parse(ddlCurrentTerm.SelectedValue) : 0,// (txtCurrentTerm.Text != "") ? int.Parse(txtCurrentTerm.Attributes["NSISTerm"]):0,
                                NATIONAL_ID = (txtQID.Text != "") ? decimal.Parse(txtQID.Text) : 0,
                                MOE_SCHOOL_CURRICULUM_ID = (ddlCurrentCurriculum.SelectedValue != "-1") ? ddlCurrentCurriculum.SelectedValue : "",//txtCurrentCurriculum.Text, //needs to have curriculum ID
                                MOE_TRANSACTION_DTTM = DateTime.Now,
                                MOE_USERID = SPContext.Current.Site.RootWeb.CurrentUser.LoginName

                            };

                            CurrentAcadInfoResult = MOE_APPL_ACAD_INFO_Repository.Insert(currentYearAcadInfo).Result;
                        }
                        else
                        {
                            CurrentAcadInfoResult.EnglishResult = PSPESConstants.NoAcadData;
                        }
                    }
                    catch (Exception ex)
                    {
                        ReturnedResult2 = TransactionLogsRepository.Insert(new TransactionsLog { CreatedDate = DateTime.Now, ShortDescription = "Add Application Control", Description = "Save Application current academic information Data Exception: " + ex.Message, UserID = SPContext.Current.Site.RootWeb.CurrentUser.LoginName }).Result;
                        string confirmationMessage = "";

                        if ((uint)CultureInfo.CurrentUICulture.LCID == 1025)
                        {

                            confirmationMessage = PSPESConstants.ArabicApplicationError;
                        }
                        else
                        {

                            confirmationMessage = PSPESConstants.EnglishApplicationError;
                        }
                        CustomMessageBoxHelper.Show(this.Page, confirmationMessage);
                        return;
                    }


                    //saving the previous year academic info
                    DBOperationResult PrevAcadInfoResult = new DBOperationResult();
                    try
                    {

                        ReturnedResult2 = TransactionLogsRepository.Insert(new TransactionsLog { CreatedDate = DateTime.Now, ShortDescription = "Add Application Control", Description = "Save Application Previous Academic Information Data", UserID = SPContext.Current.Site.RootWeb.CurrentUser.LoginName }).Result;

                        if (txtPreviousSchool.Text != "")
                        {
                            MOE_APPL_ACAD_INFO_Model previousYearAcadInfo = new MOE_APPL_ACAD_INFO_Model()
                            {

                                MOE_APPL_REF_NBR = Application_Reference_Number,
                                MOE_GRADE = (ddlPreviousGrade.SelectedValue != "-1") ? ddlPreviousGrade.SelectedValue.Split(';')[0] : "",//txtPreviousGrade.Text,
                                MOE_SCHOOL_CODE = txtPreviousSchoolCode.Text,//, Needs to have school code 
                                MOE_SCHOOL_NAME = txtPreviousSchool.Text,
                                MOE_STU_RSLT = ddlPreviousYearResult.SelectedValue,
                                MOE_TERM = (ddlPreviousTerm.SelectedValue != "-1") ? int.Parse(ddlPreviousTerm.SelectedValue) : 0,
                                NATIONAL_ID = (txtQID.Text != "") ? decimal.Parse(txtQID.Text) : 0,
                                MOE_SCHOOL_CURRICULUM_ID = (ddlPreviousCurriculum.SelectedValue != "-1") ? ddlPreviousCurriculum.SelectedValue : "", //needs to have curriculum ID
                                MOE_TRANSACTION_DTTM = DateTime.Now,
                                MOE_USERID = SPContext.Current.Site.RootWeb.CurrentUser.LoginName

                            };

                            PrevAcadInfoResult = MOE_APPL_ACAD_INFO_Repository.Insert(previousYearAcadInfo).Result;
                        }
                        else
                        {

                            PrevAcadInfoResult.EnglishResult = PSPESConstants.NoAcadData;
                        }
                    }
                    catch (Exception ex)
                    {
                        ReturnedResult2 = TransactionLogsRepository.Insert(new TransactionsLog { CreatedDate = DateTime.Now, ShortDescription = "Add Application Control", Description = "Save Application Previous Academic Information Data Exception: " + ex.Message, UserID = SPContext.Current.Site.RootWeb.CurrentUser.LoginName }).Result;
                        string confirmationMessage = "";

                        if ((uint)CultureInfo.CurrentUICulture.LCID == 1025)
                        {

                            confirmationMessage = PSPESConstants.ArabicApplicationError;
                        }
                        else
                        {

                            confirmationMessage = PSPESConstants.EnglishApplicationError;
                        }
                        CustomMessageBoxHelper.Show(this.Page, confirmationMessage);
                        return;
                    }

                    #endregion

                    #region Save Application Guardian/Parents Data

                    //save guardian information
                    DBOperationResult GuardianResult = new DBOperationResult();
                    try
                    {

                        ReturnedResult2 = TransactionLogsRepository.Insert(new TransactionsLog { CreatedDate = DateTime.Now, ShortDescription = "Add Application Control", Description = "Save Application Guardian Data", UserID = SPContext.Current.Site.RootWeb.CurrentUser.LoginName }).Result;

                        if (txtGuardianQID.Text != "")
                        {
                            MOE_GUARDIAN_DATA_Model_Not_Serializable guardian_model = new MOE_GUARDIAN_DATA_Model_Not_Serializable()
                            {
                                MOE_EMAIL = GuardianEmail.Value,

                                MOE_ISGUARDIAN = true,
                                NATIONAL_ID = (txtQID.Text != "") ? decimal.Parse(txtQID.Text) : 0,
                                MOE_RELATED_QID = (txtGuardianQID.Text != "") ? decimal.Parse(txtGuardianQID.Text) : 0,
                                MOE_RELATIONSHIP_TYPE_ID = ddlGuardianRelationship.SelectedValue,
                                MOE_MOBILE_CONTACT_NBR = txtGuardianMobile.Text,
                                MOE_HOME_CONTACT_NBR = GuardianLandLine.Value,
                                MOE_EMPLOYER_TYPE_ID = ddlGuardianEmployerType.SelectedValue,
                                MOE_EMPLOYER = ddlGuardianEmployer.SelectedValue,
                                MOE_APPL_NBR = Application_Reference_Number,
                                MOE_TRANSACTION_DTTM = DateTime.Now,
                                MOE_USERID = SPContext.Current.Site.RootWeb.CurrentUser.LoginName,
                                MOE_MARITAL_STATUS_ID = ddlGuardianMaritalStatus.SelectedValue,
                                MOE_GUARDIAN_NAME_ARA = arabicguardianname.Value,
                                MOE_GUARDIAN_NAME_ENG = englishguardianname.Value




                            };


                            GuardianResult = MOE_GUARDIAN_DATA_Repository.Insert(guardian_model).Result;

                        }
                        else
                            GuardianResult.EnglishResult = PSPESConstants.NoGuardianData;
                    }
                    catch (Exception ex)
                    {
                        ReturnedResult2 = TransactionLogsRepository.Insert(new TransactionsLog { CreatedDate = DateTime.Now, ShortDescription = "Add Application Control", Description = "Save Application Guardian Data Exception: " + ex.Message, UserID = SPContext.Current.Site.RootWeb.CurrentUser.LoginName }).Result;
                        string confirmationMessage = "";

                        if ((uint)CultureInfo.CurrentUICulture.LCID == 1025)
                        {

                            confirmationMessage = PSPESConstants.ArabicApplicationError;
                        }
                        else
                        {

                            confirmationMessage = PSPESConstants.EnglishApplicationError;
                        }
                        CustomMessageBoxHelper.Show(this.Page, confirmationMessage);
                        return;
                    }

                    DBOperationResult contactResult = new DBOperationResult();
                    try
                    {
                        List<MOE_GUARDIAN_DATA_Model> Contacts = (List<MOE_GUARDIAN_DATA_Model>)ViewState["studentContacts"];

                        ReturnedResult2 = TransactionLogsRepository.Insert(new TransactionsLog { CreatedDate = DateTime.Now, ShortDescription = "Add Application Control", Description = "Save Application Student Contacts Data ", UserID = SPContext.Current.Site.RootWeb.CurrentUser.LoginName }).Result;


                        if (Contacts != null && Contacts.Count < 0)

                        {

                            foreach (MOE_GUARDIAN_DATA_Model contact in Contacts)
                            {
                                MOE_GUARDIAN_DATA_Model_Not_Serializable NS_guardian_model = new MOE_GUARDIAN_DATA_Model_Not_Serializable()
                                {
                                    MOE_EMAIL = contact.MOE_EMAIL,

                                    MOE_ISGUARDIAN = false,
                                    NATIONAL_ID = (txtQID.Text != "") ? decimal.Parse(txtQID.Text) : 0,
                                    MOE_RELATED_QID = contact.MOE_RELATED_QID,
                                    MOE_RELATIONSHIP_TYPE_ID = contact.MOE_RELATIONSHIP_TYPE_ID,
                                    MOE_MOBILE_CONTACT_NBR = contact.MOE_MOBILE_CONTACT_NBR,
                                    MOE_APPL_NBR = Application_Reference_Number,
                                    MOE_TRANSACTION_DTTM = DateTime.Now,
                                    MOE_USERID = SPContext.Current.Site.RootWeb.CurrentUser.LoginName,
                                    MOE_GUARDIAN_NAME_ARA = contact.bio_data.MOE_ARABIC_NAME,
                                    MOE_ENGLISH_NAME = contact.bio_data.MOE_ENGLISH_NAME


                                };

                                contactResult = MOE_GUARDIAN_DATA_Repository.Insert(NS_guardian_model).Result;
                            }
                        }
                        else
                        {

                            contactResult.EnglishResult = PSPESConstants.NoContactData;
                        }
                    }
                    catch (Exception ex)
                    {
                        ReturnedResult2 = TransactionLogsRepository.Insert(new TransactionsLog { CreatedDate = DateTime.Now, ShortDescription = "Add Application Control", Description = "Save Application Student Contacts Data Exception: " + ex.Message, UserID = SPContext.Current.Site.RootWeb.CurrentUser.LoginName }).Result;
                        string confirmationMessage = "";

                        if ((uint)CultureInfo.CurrentUICulture.LCID == 1025)
                        {

                            confirmationMessage = PSPESConstants.ArabicApplicationError;
                        }
                        else
                        {

                            confirmationMessage = PSPESConstants.EnglishApplicationError;
                        }
                        CustomMessageBoxHelper.Show(this.Page, confirmationMessage);
                        return;
                    }
                    #endregion

                    #region Save Application Health Data

                    DBOperationResult HealthInfoResult = new DBOperationResult();
                    try
                    {

                        ReturnedResult2 = TransactionLogsRepository.Insert(new TransactionsLog { CreatedDate = DateTime.Now, ShortDescription = "Add Application Control", Description = "Save Application Health Data", UserID = SPContext.Current.Site.RootWeb.CurrentUser.LoginName }).Result;


                        MOE_HEALTH_DATA_Model health_model = new MOE_HEALTH_DATA_Model()
                        {
                            MOE_APPL_NBR = Application_Reference_Number,
                            MOE_FIT_FOR_SCHOOLING = PHCCData.Checked,
                            MOE_HLTH_CARD_NBR = txtStudentHealthCard.Value,
                            MOE_HLTH_CTR_NAME = txtHealthCenterName.Value,
                            MOE_HLTH_PROBLEMS = HealthIssue.Checked,
                            MOE_HLTH_PROBLEMS_DETAILS = MentionHealthIssues.Value,
                            MOE_LEARNING_DIFFICULTIES = (ddlLearningDifficulties.SelectedValue != "0") ? bool.Parse(ddlLearningDifficulties.SelectedValue) : false,
                            MOE_SPL_NEEDS = (ddlSpecialNeed.SelectedValue != "0") ? bool.Parse(ddlSpecialNeed.SelectedValue) : false,
                            NATIONAL_ID = (txtQID.Text != "") ? decimal.Parse(txtQID.Text) : 0,
                            MOE_TRANSACTION_DTTM = DateTime.Now,
                            MOE_USERID = SPContext.Current.Site.RootWeb.CurrentUser.LoginName



                        };

                        HealthInfoResult = MOE_HEALTH_DATA_Repository.Insert(health_model).Result;
                    }
                    catch (Exception ex)
                    {
                        ReturnedResult2 = TransactionLogsRepository.Insert(new TransactionsLog { CreatedDate = DateTime.Now, ShortDescription = "Add Application Control", Description = "Save Application Health Data Exception: " + ex.Message, UserID = SPContext.Current.Site.RootWeb.CurrentUser.LoginName }).Result;
                        string confirmationMessage = "";

                        if ((uint)CultureInfo.CurrentUICulture.LCID == 1025)
                        {

                            confirmationMessage = PSPESConstants.ArabicApplicationError;
                        }
                        else
                        {

                            confirmationMessage = PSPESConstants.EnglishApplicationError;
                        }
                        CustomMessageBoxHelper.Show(this.Page, confirmationMessage);
                        return;
                    }

                    #endregion

                    #region Save Application Documents

                    try
                    {


                        ReturnedResult2 = TransactionLogsRepository.Insert(new TransactionsLog { CreatedDate = DateTime.Now, ShortDescription = "Add Application Control", Description = "Save Application Documents Data ", UserID = SPContext.Current.Site.RootWeb.CurrentUser.LoginName }).Result;

                        foreach (GridViewRow item in gvRequiredDocuments.Rows)
                        {
                            FileUpload RequiredFileUpload = (FileUpload)item.FindControl("fuRequiredDocument");
                            HiddenField DocumentTypeIDHiddenField = (HiddenField)item.FindControl("DocumentTypeIDHiddenField");

                            if (RequiredFileUpload.PostedFile != null)
                            {


                                if (RequiredFileUpload.PostedFile.ContentLength > 0)
                                {
                                    System.IO.Stream strm = RequiredFileUpload.PostedFile.InputStream;

                                    byte[] ApplicantByte = new byte[
                                            Convert.ToInt32(RequiredFileUpload.PostedFile.ContentLength)];

                                    strm.Read(ApplicantByte, 0, Convert.ToInt32
                                                                       (RequiredFileUpload.PostedFile.ContentLength));
                                    string ApplicantFileExtension = System.IO.Path.GetExtension(RequiredFileUpload.FileName);
                                    //string ApplicantReference = string.Format("{0}-{1}{2}", DateTime.Now.Year.ToString(), DateTime.Now.Minute, DateTime.Now.Second);
                                    string ApplicantReference = String.Format("{0:d9}", (DateTime.Now.Ticks / 10) % 1000000000); ;

                                    string FilePath = SaveToDocumentLibrary(ApplicantByte, ApplicantFileExtension, ApplicantReference);

                                    MOE_APPL_SUPPORT_DOCS_Model_Not_Serializable appl_support_document = new MOE_APPL_SUPPORT_DOCS_Model_Not_Serializable()
                                    {

                                        NATIONAL_ID = decimal.Parse(txtQID.Text),
                                        MOE_APPL_NBR = Application_Reference_Number,
                                        MOE_DOCUMENT_LOCATION = FilePath,
                                        MOE_DOCUMENT_TYPE_ID = DocumentTypeIDHiddenField.Value
                                    };


                                    DBOperationResult ApplicationSupportDocsResult = MOE_APPL_SUPPORT_DOCS_Repository.Insert(appl_support_document).Result;
                                }
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        ReturnedResult2 = TransactionLogsRepository.Insert(new TransactionsLog { CreatedDate = DateTime.Now, ShortDescription = "Add Application Control", Description = "Save Application Documents Data Exception: " + ex.Message, UserID = SPContext.Current.Site.RootWeb.CurrentUser.LoginName }).Result;
                        string confirmationMessage = "";

                        if ((uint)CultureInfo.CurrentUICulture.LCID == 1025)
                        {

                            confirmationMessage = PSPESConstants.ArabicApplicationError;
                        }
                        else
                        {

                            confirmationMessage = PSPESConstants.EnglishApplicationError;
                        }
                        CustomMessageBoxHelper.Show(this.Page, confirmationMessage);
                        return;
                    }

                    #endregion

                    #region Save Student & Guardian Bio Information
                    MOE_BIO_DATA_Model student_Bio_Model = MOE_BIO_DATA_Repository.GetBioDataByQID(txtQID.Text).Result;
                    MOE_BIO_DATA_Model guardian_Bio_Model = MOE_BIO_DATA_Repository.GetBioDataByQID(txtGuardianQID.Text).Result;
                    DBOperationResult student_Insertion = MOE_BIO_DATA_Repository.Insert(student_Bio_Model).Result;
                    DBOperationResult guardian_Insertion = MOE_BIO_DATA_Repository.Insert(guardian_Bio_Model).Result;

                    #endregion

                    ReturnedResult2 = TransactionLogsRepository.Insert(new TransactionsLog
                    {
                        CreatedDate = DateTime.Now,
                        ShortDescription = "Add Application Control",
                        Description = "Save Application Before if confirmation,ApplicationInsertResult.EnglishResult: " + ApplicationInsertResult.EnglishResult
    + "CurrentAcadInfoResult.EnglishResult:" + CurrentAcadInfoResult.EnglishResult +
    "PrevAcadInfoResult.EnglishResult:" + PrevAcadInfoResult.EnglishResult +
    "GuardianResult.EnglishResult:" + GuardianResult.EnglishResult +
    "HealthInfoResult.EnglishResult:" + HealthInfoResult.EnglishResult,
                        UserID = SPContext.Current.Site.RootWeb.CurrentUser.LoginName
                    }).Result;
                    if (ApplicationInsertResult.EnglishResult == PSPESConstants.InsertionDone &&
                       (CurrentAcadInfoResult.EnglishResult == PSPESConstants.InsertionDone || CurrentAcadInfoResult.EnglishResult == PSPESConstants.NoAcadData) &&
                       (PrevAcadInfoResult.EnglishResult == PSPESConstants.InsertionDone || PrevAcadInfoResult.EnglishResult == PSPESConstants.NoAcadData) &&
                       (GuardianResult.EnglishResult == PSPESConstants.InsertionDone || GuardianResult.EnglishResult == PSPESConstants.NoGuardianData) &&
                       HealthInfoResult.EnglishResult == PSPESConstants.InsertionDone/* &&
                   ApplicationSupportDocsResult.EnglishResult == PSPESConstants.InsertionDone*/ )
                    {


                        string confirmationMessage = "";
                        MessageTemplate msgTmplt = new MessageTemplate();
                        string msgBody = "";
                        msgTmplt = MessageTemplateRepository.Getby("ApplicationCreated", 11).Result;

                        if ((uint)CultureInfo.CurrentUICulture.LCID == 1025)
                        {
                            msgBody = msgTmplt.ArabicMessage;
                            msgBody = msgBody.Replace("%StudentName% ", txtName.Text);
                            msgBody = msgBody.Replace("%StudentQID%", txtQID.Text);
                            msgBody = msgBody.Replace("%referencenumber%", System.Environment.NewLine + Application_Reference_Number + System.Environment.NewLine);
                            msgBody = msgBody.Replace("%schoolname%", txtPreEnrollmentSchool.Text);
                            msgBody = msgBody.Replace("%Grade%", System.Environment.NewLine + ddlPreEnrolmentGrade.SelectedItem.Text + System.Environment.NewLine);
                            msgBody = msgBody.Replace("%Academicyear%", ddlPreEnrollmentTerm.SelectedItem.Text);

                            string smsStatus = SendSMS.SendMessage("Application Created", txtGuardianMobile.Text, msgBody);

                            if (smsStatus == "1000")
                            {
                                SMSHistoryModel smsHist = new SMSHistoryModel()
                                {
                                    MOE_APPL_REF_NBR = Application_Reference_Number,
                                    MsgStatus = "SENT",
                                    MsgText = msgBody,
                                    MsgTime = DateTime.Now.ToString("dd/MM/yyyy", CultureInfo.GetCultureInfo(2057)),
                                    MsgType = "ApplicationCreated",
                                    MsgTitle = "Application Created",
                                    MobileNumber = txtGuardianMobile.Text,
                                    MsgSender = SPContext.Current.Site.RootWeb.CurrentUser.Name


                                };

                                var insertSMSHistory = SMSHistoryRepository.Insert(smsHist).Result;

                                confirmationMessage = string.Format(PSPESConstants.ArabicApplicationConfirmation, txtName.Text, txtPreEnrollmentSchool.Text, "\n" + Application_Reference_Number + "\n", txtGuardianMobile.Text);
                                string schoolgender = txtschoolgender.Value;
                                string applicationDate = dtApplicationDate2.Value;
                                ClearControls();

                                txtschoolgender.Value = schoolgender;
                                dtApplicationDate2.Value = applicationDate;
                                CustomMessageBoxHelper.Show(this.Page, confirmationMessage);
                                return;





                            }
                            else
                            {
                                SMSHistoryModel smsHist = new SMSHistoryModel()
                                {
                                    MOE_APPL_REF_NBR = Application_Reference_Number,
                                    MsgStatus = "FAIL",
                                    MsgText = msgBody,
                                    MsgTime = DateTime.Now.ToString("dd/MM/yyyy", CultureInfo.GetCultureInfo(2057)),
                                    MsgType = "ApplicationCreated",
                                    MsgTitle = "Application Created",
                                    MobileNumber = txtGuardianMobile.Text,
                                    MsgSender = SPContext.Current.Site.RootWeb.CurrentUser.Name


                                };

                                var insertSMSHistory = SMSHistoryRepository.Insert(smsHist).Result;

                                if ((uint)CultureInfo.CurrentUICulture.LCID == 1025)
                                { CustomMessageBoxHelper.Show(this.Page, "لم يتم ارسال الرسالة"); return; }
                                else { CustomMessageBoxHelper.Show(this.Page, "Message sent fail "); return; }


                            }



                        }
                        else
                        {
                            msgBody = msgTmplt.EnglishMessage;
                            msgBody = msgBody.Replace("%StudentName% ", txtName.Text);
                            msgBody = msgBody.Replace("%StudentQID%", txtQID.Text);
                            msgBody = msgBody.Replace("%referencenumber%", Application_Reference_Number);
                            msgBody = msgBody.Replace("%schoolname%", txtPreEnrollmentSchool.Text);
                            msgBody = msgBody.Replace("%Grade%", ddlPreEnrolmentGrade.SelectedItem.Text);
                            msgBody = msgBody.Replace("%Academicyear%", ddlPreEnrollmentTerm.SelectedItem.Text);
                            //SendSMS("Application Created", GuardianMobile.Value,msgBody);
                            string smsStatus = SendSMS.SendMessage("Application Created", txtGuardianMobile.Text, msgBody);


                            if (smsStatus == "1000")
                            {
                                SMSHistoryModel smsHist = new SMSHistoryModel()
                                {
                                    MOE_APPL_REF_NBR = Application_Reference_Number,
                                    MsgStatus = "SENT",
                                    MsgText = msgBody,
                                    MsgTime = DateTime.Now.ToString("dd/MM/yyyy", CultureInfo.GetCultureInfo(2057)),
                                    MsgType = "ApplicationCreated",
                                    MsgTitle = "Application Created",
                                    MobileNumber = txtGuardianMobile.Text,
                                    MsgSender = SPContext.Current.Site.RootWeb.CurrentUser.Name


                                };

                                var insertSMSHistory = SMSHistoryRepository.Insert(smsHist).Result;

                                confirmationMessage = string.Format(PSPESConstants.EnglishApplicationConfirmation, txtName.Text, txtPreEnrollmentSchool.Text, Application_Reference_Number, txtGuardianMobile.Text);
                                string schoolgender = txtschoolgender.Value;
                                string applicationDate = dtApplicationDate2.Value;
                                ClearControls();

                                txtschoolgender.Value = schoolgender;
                                dtApplicationDate2.Value = applicationDate;
                                CustomMessageBoxHelper.Show(this.Page, confirmationMessage);
                                return;





                            }
                            else
                            {
                                SMSHistoryModel smsHist = new SMSHistoryModel()
                                {
                                    MOE_APPL_REF_NBR = Application_Reference_Number,
                                    MsgStatus = "FAIL",
                                    MsgText = msgBody,
                                    MsgTime = DateTime.Now.ToString("dd/MM/yyyy", CultureInfo.GetCultureInfo(2057)),
                                    MsgType = "ApplicationCreated",
                                    MsgTitle = "Application Created",
                                    MobileNumber = txtGuardianMobile.Text,
                                    MsgSender = SPContext.Current.Site.RootWeb.CurrentUser.Name


                                };

                                var insertSMSHistory = SMSHistoryRepository.Insert(smsHist).Result;

                                if ((uint)CultureInfo.CurrentUICulture.LCID == 1025)
                                { CustomMessageBoxHelper.Show(this.Page, "لم يتم ارسال الرسالة"); return; }
                                else { CustomMessageBoxHelper.Show(this.Page, "Message sent fail "); return; }


                            }





                        }










                        //Response.Redirect(Request.RawUrl);


                    }
                }


                #endregion




            }
            catch (Exception ex)
            {
                string confirmationMessage = "";

                if ((uint)CultureInfo.CurrentUICulture.LCID == 1025)
                {

                    confirmationMessage = PSPESConstants.ArabicApplicationError;
                }
                else
                {

                    confirmationMessage = PSPESConstants.EnglishApplicationError;
                }



                DBOperationResult ReturnedResult2 = TransactionLogsRepository.Insert(new TransactionsLog { CreatedDate = DateTime.Now, ShortDescription = "Add Application Control", Description = "Save Application Data General Exception: " + ex.Message, UserID = SPContext.Current.Site.RootWeb.CurrentUser.LoginName }).Result;
                CustomMessageBoxHelper.Show(this.Page, confirmationMessage);
                return;

            }
        }

        public string SaveToDocumentLibrary(byte[] ApplicantByte, string ApplicantFileExtension, string ApplicantReference)

        {
            string ApplicantFilePath = "";
            SPSecurity.RunWithElevatedPrivileges(new SPSecurity.CodeToRunElevated(delegate ()
            {
                // Open site where document library is created.
                using (SPSite objSite = new SPSite(SPContext.Current.Site.Url))
                {
                    using (SPWeb objWeb = objSite.OpenWeb())
                    {
                        SPFolder mylibrary = objWeb.Folders["ApplicantAttachedDocuments"];
                        if (mylibrary != null)
                        {


                            Random rd = new Random();

                            // Set AllowUnsafeUpdates = true to avoid security error

                            objWeb.AllowUnsafeUpdates = true;


                            string ApplicantFileName = string.Format("{0}{1}", ApplicantReference, ApplicantFileExtension);

                            SPFile ApplicantFile = mylibrary.Files.Add(ApplicantFileName, ApplicantByte);
                            int ApplicantFileID = ApplicantFile.Item.ID;
                            ApplicantFilePath = SPContext.Current.Site.Url + "/" + ApplicantFile.Item.Url.ToString();

                            objWeb.AllowUnsafeUpdates = false;

                        }
                    }

                }
            }));
            return ApplicantFilePath;
        }

        protected void LinkButton1_Click(object sender, EventArgs e)
        {
            #region Alhanafi=>28-2-2018 Check if the application exist before save
            string schoolCode = getSchoolCode();
            bool ApplicationExist = MOE_APPLICATION_DATA_Repository.CheckApplicationExist(int.Parse(ddlPreEnrollmentTerm.SelectedValue), schoolCode, ddlPreEnrolmentGrade.SelectedValue.Split(';')[0], txtQID.Text).Result;


            #endregion

            if (ApplicationExist)
            {
                if ((uint)CultureInfo.CurrentUICulture.LCID == 1025)
                {
                    CustomMessageBoxHelper.Show(this.Page, PSPESConstants.ArabicApplicationExistError);
                }
                else
                {
                    CustomMessageBoxHelper.Show(this.Page, PSPESConstants.EnglishApplicationExistError);

                }
            }

            //check if guardian mobile number is filled or show error message

            else if (txtGuardianMobile.Text == "")
            {
                if ((uint)CultureInfo.CurrentUICulture.LCID == 1025)
                {
                    CustomMessageBoxHelper.Show(this.Page, PSPESConstants.ArabicApplicationNoGuardianMobile);
                }
                else
                {
                    CustomMessageBoxHelper.Show(this.Page, PSPESConstants.EnglishApplicationNoGuardianMobile);

                }

            }

            //check if special need & learning difficulties are provided or not


            else if (ddlSpecialNeed.SelectedValue == "0" || ddlLearningDifficulties.SelectedValue == "0")
            {
                if ((uint)CultureInfo.CurrentUICulture.LCID == 1025)
                {
                    CustomMessageBoxHelper.Show(this.Page, PSPESConstants.ArabicApplicationNoSpecialNeedLearningDifficulties);
                }
                else
                {
                    CustomMessageBoxHelper.Show(this.Page, PSPESConstants.EnglishApplicationNoSpecialNeedLearningDifficulties);

                }

            }


            else
            {
                SaveApplication();


            }
        }

        protected void LinkButton2_Click(object sender, EventArgs e)
        {
            SendSMS.SendMessage("test title 2", "33493587", "this is testing 123456");

        }

        //protected void SendSMS(string title, string mobileNumber, string txtMessageBody)
        //{
        //    try
        //    {
        //        // txtPreEnrollmentSchool.Text += "start";

        //        int BulkID = 0;
        //        var BulkReturnedValue = Repository.SendSMS.InsertBulk(new MessageBulkModel { UserID = long.Parse("28135610324"), CreateDate = DateTime.Now, IsBulkSet = false, IsCompleted = false }).Result;
        //        //if (BulkReturnedValue[1] == Constants.New)
        //        //{
        //        //foreach (var item in BulkReturnedValue)
        //        //{
        //        //    txtPreEnrollmentSchool.Text += item;

        //        //}


        //        BulkID = BulkReturnedValue[0];

        //        //}

        //        string textBody = string.Format("{0}  ", "test message");
        //        // txtPreEnrollmentSchool.Text += "before insert message";
        //        var task = Repository.SendSMS.Insert(new MessageDetailsModel
        //        {
        //            UserID = long.Parse("28135610324"),
        //            Title = title,
        //            MobileNumber = mobileNumber,
        //            PriorityID = 1,

        //            MessageID = "",
        //            IsBulk = true,
        //            SenderCode = "11500",
        //            BulkID = BulkID,
        //            ContactSourceID = 1,
        //            TextBody = txtMessageBody
        //        });

        //        string MessageID = task.Result;
        //        //txtPreEnrollmentSchool.Text += MessageID;
        //        MessageDetailsModel messageDetails = new MessageDetailsModel
        //        {
        //            UserID = long.Parse("28135610324"),
        //            Title = title,
        //            MobileNumber = mobileNumber,
        //            PriorityID = 1,

        //            MessageID = MessageID,
        //            IsBulk = true,
        //            SenderCode = "11500",
        //            IsBulkSet = false,
        //            BulkID = BulkID,
        //            ContactSourceID = 1,
        //            TextBody = txtMessageBody
        //        };


        //        var messagetask = Repository.SendSMS.Send(messageDetails).Result;
        //        //txtPreEnrollmentSchool.Text += "after send";
        //    }
        //    catch (Exception ex)
        //    {
        //        //txtPreEnrollmentSchool.Text += ex.Message;
        //    }
        //}

        protected void LinkButton2_Click1(object sender, EventArgs e)
        {
            CustomMessageBoxHelper.Show(this.Page, DateTime.Now.ToString() + "," + Convert.ToDateTime(DateTime.Now.ToString(), new CultureInfo(2057)).ToString());
        }

        public static void EditApplication(bool editMode)
        {
            var page = HttpContext.Current.CurrentHandler as Page;
            CustomMessageBoxHelper.Show(page, "EditMode= " + editMode.ToString());

        }

        protected void ddlCurrentCurriculum_SelectedIndexChanged(object sender, EventArgs e)
        {
            LoadGradesWithCurriculumOnly(ddlCurrentTerm.SelectedValue, ddlCurrentGrade, ddlCurrentCurriculum.SelectedValue);

        }

        private void LoadGrades(string schoolYearID, string paramSchoolCode, DropDownList ddlGrade, string selectedCurriculum)
        {
            //List<MOE_SCHOOL_Model> Schools = MOE_SCHOOL_Repository.GetQatarSchools(schoolYearID).Result;
            //List<MOE_SCHOOL_Model> uniqueSchools = new List<MOE_SCHOOL_Model>();
            //uniqueSchools = Schools.Where(P => P.MOE_SCHOOL_CURRICULUM_ID.Contains(selectedCurriculum)).Select(p => p)
            //                   .Distinct().ToList();

            //MOE_SCHOOL_Model schoolWithMaxGardes = new MOE_SCHOOL_Model();
            //int maxGradeCounts = 0;

            //foreach (MOE_SCHOOL_Model school in uniqueSchools)
            //{
            //    if (school.schoolGrades.Count > maxGradeCounts)
            //    {
            //        maxGradeCounts = school.schoolGrades.Count;
            //        schoolWithMaxGardes = school;
            //    }
            //}





            List<MOE_AGE_VALIDATE_Model> AgeValidateModelCurriculumGrades = MOE_AGE_VALIDATE_Repository.GetGradeList(ddlPreEnrollmentTerm.SelectedValue, paramSchoolCode, "Private", selectedCurriculum).Result;




            //List<schoolGrade> SchoolDataList = new List<schoolGrade>();
            //SchoolDataList = schoolWithMaxGardes.schoolGrades;

            //List<schoolGrade> SortedSchoolDataList = new List<schoolGrade>();



            var newSortedList = AgeValidateModelCurriculumGrades.Select(x => new
            {

                x.MOE_GRADE,
                x.MOE_GRADE_DESC_ARA,
                x.MOE_GRADE_DESC_ENG,
                x.Weight,
                GradeMapping = getGradeMapping(x.MOE_GRADE)
            });


            //var SortedSchoolDataList = SchoolDataList.OrderBy((s => (s.Grade != "KG" && s.Grade != "PK"))).ThenBy(s => s.Grade);
            var SortedSchoolDataList = newSortedList.OrderBy(s => s.GradeMapping).Select(p => new MOE_AGE_VALIDATE_Model { MOE_GRADE = p.MOE_GRADE, MOE_GRADE_DESC_ENG = p.MOE_GRADE_DESC_ENG, MOE_GRADE_DESC_ARA = p.MOE_GRADE_DESC_ARA, Weight = p.Weight });




            //var SortedSchoolDataList = SchoolDataList.OrderBy(s => s.Grade.Where(p=>(s.Grade != "PK" && s.Grade != "KG")));
            //var SortedSchoolDataList = SchoolDataList.OrderBy((s => (s.Grade != "KG" && s.Grade != "PK"))).ThenBy(s => s.Grade);

            //schoolGrade PKSchoolGrade = SortedSchoolDataList.ElementAt(1);
            List<MOE_AGE_VALIDATE_Model> newSortedSchoolGrades = SortedSchoolDataList.ToList();
            //newSortedSchoolGrades.RemoveAt(1);

            //newSortedSchoolGrades.Insert(0, PKSchoolGrade);

            string datatextField = "";
            if ((uint)CultureInfo.CurrentUICulture.LCID == 1025)
            {

                datatextField = "MOE_GRADE_DESC_ARA";
            }
            else
            {

                datatextField = "MOE_GRADE_DESC_ENG";
            }


            ddlGrade.Items.Clear();
            ddlGrade.DataSource = newSortedSchoolGrades;
            ddlGrade.DataTextField = datatextField;
            ddlGrade.DataValueField = "MOE_GRADE";
            ddlGrade.DataBind();
            foreach (ListItem item in ddlGrade.Items)
            {
                //item.Attributes.Add("Weight",newSortedSchoolGrades.Where(x => x.MOE_GRADE == item.Value).Select(p => p).FirstOrDefault().Weight.ToString());
                item.Value = item.Value + ";" + newSortedSchoolGrades.Where(x => x.MOE_GRADE == item.Value).Select(p => p).FirstOrDefault().Weight.ToString();

            }
            ddlGrade.Items.Insert(0, new ListItem(Convert.ToString(this.GetGlobalResourceObject("MOEHE.PSPES", "PleaseSelect")), "-1"));



        }

        private void LoadGradesWithCurriculumOnly(string schoolYearID, DropDownList ddlGrade, string selectedCurriculum)
        {
            //List<MOE_SCHOOL_Model> Schools = MOE_SCHOOL_Repository.GetQatarSchools(schoolYearID).Result;
            //List<MOE_SCHOOL_Model> uniqueSchools = new List<MOE_SCHOOL_Model>();
            //uniqueSchools = Schools.Where(P => P.MOE_SCHOOL_CURRICULUM_ID.Contains(selectedCurriculum)).Select(p => p)
            //                   .Distinct().ToList();

            //MOE_SCHOOL_Model schoolWithMaxGardes = new MOE_SCHOOL_Model();
            //int maxGradeCounts = 0;

            //foreach (MOE_SCHOOL_Model school in uniqueSchools)
            //{
            //    if (school.schoolGrades.Count > maxGradeCounts)
            //    {
            //        maxGradeCounts = school.schoolGrades.Count;
            //        schoolWithMaxGardes = school;
            //    }
            //}





            List<MOE_AGE_VALIDATE_Model> AgeValidateModelCurriculumGrades = MOE_AGE_VALIDATE_Repository.GetGradeListWithCurriculumOnly(ddlPreEnrollmentTerm.SelectedValue, "Private", selectedCurriculum).Result;




            //List<schoolGrade> SchoolDataList = new List<schoolGrade>();
            //SchoolDataList = schoolWithMaxGardes.schoolGrades;

            //List<schoolGrade> SortedSchoolDataList = new List<schoolGrade>();



            var newSortedList = AgeValidateModelCurriculumGrades.Select(x => new
            {

                x.MOE_GRADE,
                x.MOE_GRADE_DESC_ARA,
                x.MOE_GRADE_DESC_ENG,
                x.Weight,
                GradeMapping = getGradeMapping(x.MOE_GRADE)
            });


            //var SortedSchoolDataList = SchoolDataList.OrderBy((s => (s.Grade != "KG" && s.Grade != "PK"))).ThenBy(s => s.Grade);
            var SortedSchoolDataList = newSortedList.OrderBy(s => s.GradeMapping).Select(p => new MOE_AGE_VALIDATE_Model { MOE_GRADE = p.MOE_GRADE, MOE_GRADE_DESC_ENG = p.MOE_GRADE_DESC_ENG, MOE_GRADE_DESC_ARA = p.MOE_GRADE_DESC_ARA, Weight = p.Weight });




            //var SortedSchoolDataList = SchoolDataList.OrderBy(s => s.Grade.Where(p=>(s.Grade != "PK" && s.Grade != "KG")));
            //var SortedSchoolDataList = SchoolDataList.OrderBy((s => (s.Grade != "KG" && s.Grade != "PK"))).ThenBy(s => s.Grade);

            //schoolGrade PKSchoolGrade = SortedSchoolDataList.ElementAt(1);
            List<MOE_AGE_VALIDATE_Model> newSortedSchoolGrades = SortedSchoolDataList.ToList();
            //newSortedSchoolGrades.RemoveAt(1);

            //newSortedSchoolGrades.Insert(0, PKSchoolGrade);

            string datatextField = "";
            if ((uint)CultureInfo.CurrentUICulture.LCID == 1025)
            {

                datatextField = "MOE_GRADE_DESC_ARA";
            }
            else
            {

                datatextField = "MOE_GRADE_DESC_ENG";
            }


            ddlGrade.Items.Clear();
            ddlGrade.DataSource = newSortedSchoolGrades;
            ddlGrade.DataTextField = datatextField;
            ddlGrade.DataValueField = "MOE_GRADE";
            ddlGrade.DataBind();
            foreach (ListItem item in ddlGrade.Items)
            {
                //item.Attributes.Add("Weight",newSortedSchoolGrades.Where(x => x.MOE_GRADE == item.Value).Select(p => p).FirstOrDefault().Weight.ToString());
                item.Value = item.Value + ";" + newSortedSchoolGrades.Where(x => x.MOE_GRADE == item.Value).Select(p => p).FirstOrDefault().Weight.ToString();

            }
            ddlGrade.Items.Insert(0, new ListItem(Convert.ToString(this.GetGlobalResourceObject("MOEHE.PSPES", "PleaseSelect")), "-1"));



        }

        protected void ddlPreviousCurriculum_SelectedIndexChanged(object sender, EventArgs e)
        {
            LoadGradesWithCurriculumOnly(ddlPreviousTerm.SelectedValue, ddlPreviousGrade, ddlPreviousCurriculum.SelectedValue);
        }

        protected void ddlCurrentGrade_SelectedIndexChanged(object sender, EventArgs e)
        {
            int currentGradeWeight = -1;
            int selectedGradeWeight = -1;
            bool isValidGrade = false;
            string schoolCode = getSchoolCode();
            DateTime Today;
            MOE_ENROLLMENT_EXCEPTION_Model student_exception = MOE_ENROLLMENT_EXCEPTION_Repository.GetEnrollmentExceptionBySchoolAndGrade(txtQID.Text, int.Parse(ddlPreEnrollmentTerm.SelectedValue), schoolCode, ddlPreEnrolmentGrade.SelectedValue.Split(';')[0]).Result;
            DateTime exceptionExpiry = new DateTime();
            if ((uint)CultureInfo.CurrentUICulture.LCID == 1025)
            {
                Today = DateTime.ParseExact(DateTime.Now.Date.ToShortDateString(), "dd/MM/yy", CultureInfo.InvariantCulture);
                if (student_exception != null && student_exception.MOE_EXCEPTION_EXPIRY != null)
                {
                    exceptionExpiry = DateTime.ParseExact(((DateTime)student_exception.MOE_EXCEPTION_EXPIRY).Date.ToShortDateString(), "dd/MM/yy", CultureInfo.InvariantCulture);
                }
            }
            else
            {
                Today = DateTime.ParseExact(DateTime.Now.Date.ToString("dd/MM/yy"), "dd/MM/yy", CultureInfo.InvariantCulture);
                if (student_exception != null && student_exception.MOE_EXCEPTION_EXPIRY != null)
                {
                    exceptionExpiry = DateTime.ParseExact(((DateTime)student_exception.MOE_EXCEPTION_EXPIRY).Date.ToString("dd/MM/yy"), "dd/MM/yy", CultureInfo.InvariantCulture);
                }
            }

            if (ddlPreEnrolmentGrade.Items.Count > 0 && ddlPreEnrolmentGrade.SelectedIndex > 0)
            {

                if (ddlCurrentCurriculum.SelectedIndex > 0)
                {
                    //check if the current curriculum same as the pre enrollment school 
                    if (ddlCurrentCurriculum.SelectedValue == lblPreEnrollmentSchoolCurriculumID.Text)
                    {
                        //check if there is selected grade or not
                        if (ddlCurrentGrade.SelectedIndex > 0)
                        {
                            currentGradeWeight = int.Parse(ddlCurrentGrade.SelectedValue.Split(';')[1]);
                            selectedGradeWeight = int.Parse(ddlPreEnrolmentGrade.SelectedValue.Split(';')[1]);
                            if (selectedGradeWeight == currentGradeWeight + 1)
                            {
                                isValidGrade = true;
                                txtValidGrade.Value = "1";
                            }
                            //tarek el beheidy 22.5.2018, checking if this student has a repeat year exception for the selected grade or not
                            else if (selectedGradeWeight == currentGradeWeight)
                            {
                                //tarek el beheidy 22.5.2018, checking if this student has a repeat year exception for the selected grade or not
                                if (student_exception != null && student_exception.MOE_GRADE != null && student_exception.MOE_GRADE == ddlPreEnrolmentGrade.SelectedValue.Split(';')[0] && student_exception.REPEAT_YEAR_EXCEPTION != null && student_exception.REPEAT_YEAR_EXCEPTION.Value == true && Today <= exceptionExpiry)
                                {
                                    //the student has a repeat year exception
                                    isValidGrade = true;
                                    txtValidGrade.Value = "1";
                                }
                                else if (student_exception != null && student_exception.MOE_GRADE != null && student_exception.MOE_GRADE == ddlPreEnrolmentGrade.SelectedValue.Split(';')[0] && student_exception.REPEAT_YEAR_EXCEPTION != null && student_exception.REPEAT_YEAR_EXCEPTION.Value == true && Today > exceptionExpiry)
                                {
                                    //exception expired
                                    string confirmationMessage = "";
                                    if ((uint)CultureInfo.CurrentUICulture.LCID == 1025)
                                    {
                                        confirmationMessage = PSPESConstants.ArabicExceptionExpired;
                                    }
                                    else
                                    {
                                        confirmationMessage = PSPESConstants.EnglishExceptionExpired;
                                    }
                                    CustomMessageBoxHelper.Show(this.Page, confirmationMessage);

                                    string schoolgender2 = txtschoolgender.Value;
                                    string applicationDate2 = dtApplicationDate2.Value;
                                    //ClearControls();
                                    ddlPreEnrolmentGrade.ClearSelection();
                                    txtschoolgender.Value = schoolgender2;
                                    dtApplicationDate2.Value = applicationDate2;
                                    //txtQID.Text = "";
                                    return;
                                }
                                else
                                {
                                    //exception expired
                                    string confirmationMessage = "";
                                    if ((uint)CultureInfo.CurrentUICulture.LCID == 1025)
                                    {
                                        confirmationMessage = PSPESConstants.ArabicRepeatYearError;
                                    }
                                    else
                                    {
                                        confirmationMessage = PSPESConstants.EnglishRepeatYearError;
                                    }
                                    CustomMessageBoxHelper.Show(this.Page, confirmationMessage);

                                    string schoolgender2 = txtschoolgender.Value;
                                    string applicationDate2 = dtApplicationDate2.Value;
                                    //ClearControls();
                                    ddlPreEnrolmentGrade.ClearSelection();
                                    txtschoolgender.Value = schoolgender2;
                                    dtApplicationDate2.Value = applicationDate2;
                                    //txtQID.Text = "";
                                    return;
                                }
                            }
                            else if (selectedGradeWeight < currentGradeWeight || selectedGradeWeight > currentGradeWeight + 1)
                            {
                                //tarek el beheidy 22.5.2018, checking if this student has an age exception for the selected grade or not
                                if (student_exception != null && student_exception.MOE_GRADE != null && student_exception.MOE_GRADE == ddlPreEnrolmentGrade.SelectedValue.Split(';')[0] && student_exception.AGE_EXCEPTION != null && student_exception.AGE_EXCEPTION.Value == true && Today <= exceptionExpiry)
                                {
                                    //the student has an age exception
                                    isValidGrade = true;
                                    txtValidGrade.Value = "1";
                                }
                                else if (student_exception != null && student_exception.MOE_GRADE != null && student_exception.MOE_GRADE == ddlPreEnrolmentGrade.SelectedValue.Split(';')[0] && student_exception.AGE_EXCEPTION != null && student_exception.AGE_EXCEPTION.Value == true && Today > exceptionExpiry)
                                {
                                    //exception expired
                                    string confirmationMessage = "";
                                    if ((uint)CultureInfo.CurrentUICulture.LCID == 1025)
                                    {
                                        confirmationMessage = PSPESConstants.ArabicExceptionExpired;
                                    }
                                    else
                                    {
                                        confirmationMessage = PSPESConstants.EnglishExceptionExpired;
                                    }
                                    CustomMessageBoxHelper.Show(this.Page, confirmationMessage);

                                    string schoolgender2 = txtschoolgender.Value;
                                    string applicationDate2 = dtApplicationDate2.Value;
                                    //ClearControls();
                                    ddlPreEnrolmentGrade.ClearSelection();
                                    txtschoolgender.Value = schoolgender2;
                                    dtApplicationDate2.Value = applicationDate2;
                                    //txtQID.Text = "";
                                    return;
                                }
                                else
                                {
                                    isValidGrade = false;
                                    txtValidGrade.Value = "0";
                                }
                            }
                            if (isValidGrade)
                            {
                                LoadPreEnrolmentGradeData(ddlPreEnrolmentGrade.SelectedItem.Value.Split(';')[0]);
                                return;
                            }
                            else
                            {
                                string confirmationMessage = "";
                                if ((uint)CultureInfo.CurrentUICulture.LCID == 1025)
                                {
                                    confirmationMessage = PSPESConstants.ArabicAgeError;
                                }
                                else
                                {
                                    confirmationMessage = PSPESConstants.EnglishAgeError;
                                }
                                CustomMessageBoxHelper.Show(this.Page, confirmationMessage);
                                ddlPreEnrolmentGrade.SelectedIndex = 0;
                                txtAvailableSeatsInRequestedGrade.Value = "";
                                txtWaitListNumber.Value = "";
                                return;

                            }

                        }
                    }
                    //if the current curriculum is different from the pre enrollment school
                    else
                    {
                        isValidGrade = ValidateAge(ddlPreEnrollmentTerm.SelectedValue, "Private", lblPreEnrollmentSchoolCurriculumID.Text, ddlPreEnrolmentGrade.SelectedValue.Split(';')[0], int.Parse(ddlPreEnrolmentGrade.SelectedValue.Split(';')[1]), currentGradeWeight);
                        if (isValidGrade)
                        {
                            txtValidGrade.Value = "1";
                            LoadPreEnrolmentGradeData(ddlPreEnrolmentGrade.SelectedItem.Value.Split(';')[0]);
                            return;
                        }
                        else
                        {
                            //tarek el beheidy 22.5.2018, checking if this student has an age exception for the selected grade or not
                            if (student_exception != null && student_exception.MOE_GRADE != null && student_exception.MOE_GRADE == ddlPreEnrolmentGrade.SelectedValue.Split(';')[0] && student_exception.AGE_EXCEPTION != null && student_exception.AGE_EXCEPTION.Value == true && Today <= exceptionExpiry)
                            {
                                //the student has an age exception

                                txtValidGrade.Value = "1";
                            }
                            else if (student_exception != null && student_exception.MOE_GRADE != null && student_exception.MOE_GRADE == ddlPreEnrolmentGrade.SelectedValue.Split(';')[0] && student_exception.AGE_EXCEPTION != null && student_exception.AGE_EXCEPTION.Value == true && Today > exceptionExpiry)
                            {
                                //exception expired
                                string confirmationMessage = "";
                                if ((uint)CultureInfo.CurrentUICulture.LCID == 1025)
                                {
                                    confirmationMessage = PSPESConstants.ArabicExceptionExpired;
                                }
                                else
                                {
                                    confirmationMessage = PSPESConstants.EnglishExceptionExpired;
                                }
                                CustomMessageBoxHelper.Show(this.Page, confirmationMessage);

                                string schoolgender2 = txtschoolgender.Value;
                                string applicationDate2 = dtApplicationDate2.Value;
                                //ClearControls();
                                ddlPreEnrolmentGrade.ClearSelection();
                                txtschoolgender.Value = schoolgender2;
                                dtApplicationDate2.Value = applicationDate2;
                                //txtQID.Text = "";
                                return;
                            }
                            else
                            {


                                txtValidGrade.Value = "0";

                                string confirmationMessage = "";
                                if ((uint)CultureInfo.CurrentUICulture.LCID == 1025)
                                {
                                    confirmationMessage = PSPESConstants.ArabicAgeError;
                                }
                                else
                                {
                                    confirmationMessage = PSPESConstants.EnglishAgeError;
                                }
                                CustomMessageBoxHelper.Show(this.Page, confirmationMessage);
                                ddlPreEnrolmentGrade.SelectedIndex = 0;
                                txtAvailableSeatsInRequestedGrade.Value = "";
                                txtWaitListNumber.Value = "";
                                return;
                            }

                        }

                    }
                }

                #region old current grade selected index changed
                /*
                //    if (ddlPreEnrolmentGrade.SelectedIndex == ddlCurrentGrade.SelectedIndex || ddlPreEnrolmentGrade.SelectedIndex == ddlCurrentGrade.SelectedIndex + 1)
                //    {

                bool isValidGrade = ValidateAge(ddlPreEnrollmentTerm.SelectedValue, "Private", lblPreEnrollmentSchoolCurriculumID.Text, ddlPreEnrolmentGrade.SelectedValue.Split(';')[0], int.Parse(ddlPreEnrolmentGrade.SelectedValue.Split(';')[1]), int.Parse(ddlCurrentGrade.SelectedValue.Split(';')[1]));
                if (isValidGrade)
                {
                    txtValidGrade.Value = "1";
                }
                // LoadPreEnrolmentGradeData(ddlPreEnrolmentGrade.SelectedValue.Split(';')[0]);
                else
                {
                    txtValidGrade.Value = "0";
                    string confirmationMessage = "";
                    if ((uint)CultureInfo.CurrentUICulture.LCID == 1025)
                    {
                        confirmationMessage = PSPESConstants.ArabicAgeError;
                    }
                    else
                    {
                        confirmationMessage = PSPESConstants.EnglishAgeError;
                    }
                    CustomMessageBoxHelper.Show(this.Page, confirmationMessage);
                    ddlPreEnrolmentGrade.SelectedIndex = 0;
                    txtAvailableSeatsInRequestedGrade.Value = "";
                    txtWaitListNumber.Value = "";
                    return;

                }
                //}
                //else
                //{
                //    string confirmationMessage = "";
                //    if ((uint)CultureInfo.CurrentUICulture.LCID == 1025)
                //    {
                //        confirmationMessage = PSPESConstants.ArabicAgeError;
                //    }
                //    else
                //    {
                //        confirmationMessage = PSPESConstants.EnglishAgeError;
                //    }
                //    CustomMessageBoxHelper.Show(this.Page, confirmationMessage);
                //    ddlPreEnrolmentGrade.SelectedIndex = 0;
                //    txtAvailableSeatsInRequestedGrade.Value = "";
                //    txtWaitListNumber.Value = "";

                //}
                */
                #endregion
            }
        }

        protected void txtGuardianQID_TextChanged(object sender, EventArgs e)
        {
            GuardianQIDChanged();
        }

        private void GuardianQIDChanged()
        {
            try
            {
                if (txtGuardianQID.Text.Length == 11)
                {
                    if (txtGuardianQID.Text != txtQID2.Text)
                    {

                   
                    MOE_BIO_DATA_Model Bio_Model = MOE_BIO_DATA_Repository.GetBioDataByQID(txtGuardianQID.Text).Result;
                    if (Bio_Model != null && (Bio_Model.MOE_ARABIC_NAME != null || Bio_Model.MOE_ENGLISH_NAME != null))
                    {
                            #region tarek el beheidy 24.09.2018 add age condition for guardian must be 18 and above
                            if (CultureInfo.CurrentUICulture.Name == "ar-SA" || CultureInfo.CurrentCulture.Name == "ar-SA")
                            {
                                Thread.CurrentThread.CurrentCulture = CultureInfo.CreateSpecificCulture("ar-SA");
                                Thread.CurrentThread.CurrentCulture.DateTimeFormat = CultureInfo.CreateSpecificCulture("en-US").DateTimeFormat;
                                Thread.CurrentThread.CurrentUICulture = new System.Globalization.CultureInfo("ar-SA");
                                Thread.CurrentThread.CurrentUICulture.DateTimeFormat = CultureInfo.CreateSpecificCulture("en-US").DateTimeFormat;
                            }


                            DateTime Today = DateTime.ParseExact(DateTime.Now.Date.ToString("dd/MM/yy"), "dd/MM/yy", CultureInfo.InvariantCulture);
                            DateTime guardianDOB = DateTime.ParseExact(Bio_Model.MOE_DOB, "dd/MM/yyyy", CultureInfo.InvariantCulture);
                            double guardianAge = Age(Today, guardianDOB);
                            double minimumGuardianAgeInDays = double.Parse(ConfigurationManager.AppSettings["MinimumGuardianAgeInDays"]);
                            if (guardianAge>=minimumGuardianAgeInDays)
                            {


                                if ((uint)CultureInfo.CurrentUICulture.LCID == 1025)
                                {
                                    GuardianName.Value = Bio_Model.MOE_ARABIC_NAME;
                                    GuardianCountry.Value = Bio_Model.MOE_COUNTRY_ARABIC_NAME;
                                }
                                else
                                {
                                    GuardianName.Value = Bio_Model.MOE_ENGLISH_NAME;
                                    GuardianCountry.Value = Bio_Model.MOE_COUNTRY_ENGLISH_NAME;
                                }
                                txtGuardianGender.Text = Bio_Model.MOE_GENDER;
                                englishguardianname.Value = Bio_Model.MOE_ENGLISH_NAME;
                                arabicguardianname.Value = Bio_Model.MOE_ARABIC_NAME;
                            }
                            else
                            {
                                txtGuardianQID.Text = "";
                                string confirmationMessage = "";
                                string MinimumGuardianAgeInYears = ConfigurationManager.AppSettings["MinimumGuardianAgeInYears"];
                                if ((uint)CultureInfo.CurrentUICulture.LCID == 1025)
                                {
                                    confirmationMessage = string.Format("يجب أن يكون عمر ولي الأمر {0} عامًا أو أكثر", MinimumGuardianAgeInYears);
                                }
                                else
                                {
                                    confirmationMessage = string.Format("Guardian age must be {0} years or above", MinimumGuardianAgeInYears);
                                }
                                CustomMessageBoxHelper.Show(this.Page, confirmationMessage);

                                return;
                            }
                            #endregion
                        }
                    else
                    {
                        txtGuardianQID.Text = "";
                        string confirmationMessage = "";
                        if ((uint)CultureInfo.CurrentUICulture.LCID == 1025)
                        {
                            confirmationMessage = "لا يوجد بيانات متاحة لولي الامر في وزارة الداخلية، برجاء التواصل مع ادارة الدعم الفني";
                        }
                        else
                        {
                            confirmationMessage = "No available data for this guardian in Ministry of Interior, please contact helpdesk";
                        }
                        CustomMessageBoxHelper.Show(this.Page, confirmationMessage);

                        return;
                    }
                    }
                    else
                    {
                        txtGuardianQID.Text = "";
                        string confirmationMessage = "";
                        if ((uint)CultureInfo.CurrentUICulture.LCID == 1025)
                        {
                            confirmationMessage = " الرقم الشخصي لولي الأمر لا يمكن ان يكوم هو نفسه الرقم الشخصي للطالب، الرجاء ادخال رقم شخصي صحيح لولي الأمر";
                        }
                        else
                        {
                            confirmationMessage = "Guardian QID cannot be the same as Student QID, Please provide correct Guardian QID";
                        }
                        CustomMessageBoxHelper.Show(this.Page, confirmationMessage);
                        txtGuardianQID.Text = "";
                        return;
                    }


                }
                else
                {
                    txtGuardianQID.Text = "";
                    string confirmationMessage = "";
                    if ((uint)CultureInfo.CurrentUICulture.LCID == 1025)
                    {
                        confirmationMessage = "الرجاء ادخال رقم شخصي صحيح لولي الأمر";
                    }
                    else
                    {
                        confirmationMessage = "Please enter a valid guardian QID";
                    }
                    CustomMessageBoxHelper.Show(this.Page, confirmationMessage);
                    return;
                }
            }
            catch (Exception ex)
            { }
        }


        protected void btnCancelApplication_Click(object sender, EventArgs e)
        {
            string schoolgender = txtschoolgender.Value;
            string applicationDate = dtApplicationDate2.Value;
            ClearControls();

            txtschoolgender.Value = schoolgender;
            dtApplicationDate2.Value = applicationDate;
        }

        protected void LnkCancel_Click(object sender, EventArgs e)
        {
            Response.Redirect(Request.RawUrl);
        }
    }
}
