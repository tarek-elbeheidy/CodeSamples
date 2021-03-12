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
using System.Data;
using System.Text;
using System.Threading;

namespace MOEHE.PSPES.Webparts.AllinoneReport
{
    public partial class AllinoneReportUserControl : UserControl
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
            try
            {
                if (!Page.IsPostBack)
                {
                    isMinistryUser = SharePointUtilityRepository.IsUserInGroup(PSPESConstants.PrivateSchoolOfficeSharePointGroup, SPContext.Current.Site.RootWeb.CurrentUser.LoginName);
                    isSchoolUser = SharePointUtilityRepository.IsUserInGroup(PSPESConstants.PrivateSchoolUsersSharePointGroup, SPContext.Current.Site.RootWeb.CurrentUser.LoginName);

                    UserHelper userhelper = ADutility.GetUserDetails(SPContext.Current.Site.RootWeb.CurrentUser);
                    string schoolCode = userhelper.DepartmentID;
                    if ((isMinistryUser) || (isSchoolUser))
                    {
                        BindTerms();
                        BindSchools(ddlTerm.SelectedValue);
                        BindGrades(ddlTerm.SelectedValue, ddlSchool.SelectedValue);
                        BindCurriculum(ddlTerm.SelectedValue, ddlSchool.SelectedValue);
                        BindGender();
                        BindNationalities();
                        BindStatus();
                        BindSchoolGenders();

                        //ddlGrade.Items.Add(new ListItem { Text = GetGlobalResourceObject("MOEHE.PSPES", "All").ToString(), Value = "All" });
                        //ddlCurriculum.Items.Add(new ListItem { Text = GetGlobalResourceObject("MOEHE.PSPES", "All").ToString(), Value = "All" });
                        //ddlGender.Items.Add(new ListItem { Text = GetGlobalResourceObject("MOEHE.PSPES", "All").ToString(), Value = "All" });
                        //ddlNationality.Items.Add(new ListItem { Text = GetGlobalResourceObject("MOEHE.PSPES", "All").ToString(), Value = "All" });
                        //ddlStatus.Items.Add(new ListItem { Text = GetGlobalResourceObject("MOEHE.PSPES", "All").ToString(), Value = "All" });

                        if (isMinistryUser)
                        {
                            ViewState.Add("isMinistryUser", "true");
                            ViewState.Add("isSchoolUser", "false");
                            ddlSchool.Enabled = true;
                            ddlCurriculum.Enabled = true;
                            ddlSchoolGender.Enabled = true;

                        }
                        if (isSchoolUser)
                        {
                            ViewState.Add("isSchoolUser", "true");
                            ViewState.Add("isMinistryUser", "false");
                            ddlSchool.Enabled = false;
                            ddlSchool.SelectedValue = schoolCode;
                            BindGrades(ddlTerm.SelectedValue, ddlSchool.SelectedValue);
                            BindCurriculum(ddlTerm.SelectedValue, ddlSchool.SelectedValue);
                            BindGender();
                            BindNationalities();
                            BindStatus();
                            ddlCurriculum.Enabled = false;
                            MOE_SCHOOL_Model school_data = MOE_SCHOOL_Repository.GetSchoolInfo(ddlTerm.SelectedValue, ddlSchool.SelectedValue, "false").Result;
                            if (school_data != null && school_data.MOE_SCHOOL_CODE != null && school_data.MOE_SCHOOL_GENDER != null)
                            {
                                ddlSchoolGender.SelectedValue = school_data.MOE_SCHOOL_GENDER;
                            }
                            ddlSchoolGender.Enabled = false;

                        }
                    }
                    else
                    {
                        Response.Redirect(PSPESConstants.RedirectPage, true);
                    }
                }
            }
            catch (Exception)
            {

            }
        }

        protected void lnkExportExcel_Click(object sender, EventArgs e)
        {
            try
            {
                DataTable dt = new DataTable();
                dt = GenerateReport();
                string filename = "PRE-ENROLLMENT_REPORT_" + DateTime.Now.ToString("yyyyMMddHHmmss");
                Utility.ExportToExcel(dt, filename);
            }
            catch (Exception ex)
            {

            }
        }

        protected void lnkExportPDF_Click(object sender, EventArgs e)
        {
            StringBuilder sb = new StringBuilder();
            DataTable dt = new DataTable();
            dt = GenerateReport();
            sb = Utility.ExportToPDF(dt);
            byte[] pdfBytes = new byte[1];
            var htmlToPdf = new NReco.PdfGenerator.HtmlToPdfConverter();
            pdfBytes = htmlToPdf.GeneratePdf(sb.ToString());
        }
        //private void UploadFileToSharePoint()
        //{
        //    string filename = "PRE-ENROLLMENT_REPORT_" + DateTime.Now.ToString("yyyyMMddHHmmss") + ".pdf";
        //    SPSecurity.RunWithElevatedPrivileges(delegate ()
        //    {
        //        using (SPSite oSite = new SPSite(SPContext.Current.Web.Url))
        //        {
        //            using (SPWeb oWeb = oSite.OpenWeb())
        //            {
        //                SPFolder oLib = oWeb.GetList("Documents").RootFolder;
        //                oWeb.AllowUnsafeUpdates = true;
        //                SPFile oFile = oLib.Files.Add(filename, pdfBytes, true);
        //                oLib.Update();
        //                oWeb.AllowUnsafeUpdates = false;

        //                if (oLib.RequiresCheckout)
        //                {
        //                    oFile.CheckIn("Upload Comment", SPCheckinType.MajorCheckIn);
        //                    oFile.Publish("Publish Comment");
        //                }
        //            }
        //        }
        //    });
        //}

        private DataTable GenerateReport()
        {
            DataTable dt = new DataTable();
            try
            {
                StringBuilder sbQuery = new StringBuilder();
                StringBuilder sbCondition = new StringBuilder();

                if (CultureInfo.CurrentUICulture.Name == "ar-sa" || CultureInfo.CurrentCulture.Name == "ar-sa")
                {
                    //FILTERS
                    sbQuery.Append(PSPESConstants.REPORT_SCHOOL_TERM);
                    sbQuery.Append(PSPESConstants.REPORT_SCHOOL_SCHOOLCODE);
                    sbQuery.Append(PSPESConstants.REPORT_SCHOOL_SCHOOLNAME_AR);
                    sbQuery.Append(PSPESConstants.REPORT_STUDENT_GRADE);
                    sbQuery.Append(PSPESConstants.REPORT_SCHOOL_CURRICULUM_AR);
                    sbQuery.Append(PSPESConstants.REPORT_STUDENT_NATIONALITY_AR);
                    sbQuery.Append(PSPESConstants.REPORT_STUDENT_GENDER);
                    sbQuery.Append(PSPESConstants.REPORT_SCHOOL_STATUS);

                    if (chkStudentQID.Checked) { sbQuery.Append(PSPESConstants.REPORT_STUDENT_QID); }
                    if (chkStudentName.Checked) { sbQuery.Append(PSPESConstants.REPORT_STUDENT_NAME_AR); }
                    if (chkStudentName.Checked) { sbQuery.Append(PSPESConstants.REPORT_STUDENT_GENDER); }

                    if (chkRefNo.Checked) { sbQuery.Append(PSPESConstants.REPORT_STUDENT_APPLICATIONNUMBER); }
                    if (chkStudentBirthDate.Checked) { sbQuery.Append(PSPESConstants.REPORT_STUDENT_BIRTHDATE); }
                    //if (chkStudentNationality.Checked) { sbQuery.Append(PSPESConstants.REPORT_STUDENT_NATIONALITY_AR); }
                    if (chkStudentArea.Checked) { sbQuery.Append(PSPESConstants.REPORT_STUDENT_REGIONALAREA); }
                    if (chkStudentTransport.Checked) { sbQuery.Append(PSPESConstants.REPORT_STUDENT_TRANSPORTATION); }
                    if (chkApplicationDate.Checked) { sbQuery.Append(PSPESConstants.REPORT_STUDENT_APPLICATIONDATE); }

                    if (chkGuardianQID.Checked) { sbQuery.Append(PSPESConstants.REPORT_GUARDIAN_QID); }
                    if (chkGuardianName.Checked) { sbQuery.Append(PSPESConstants.REPORT_GUARDIAN_NAME_AR); }
                    if (chkGuardianGender.Checked) { sbQuery.Append(PSPESConstants.REPORT_GUARDIAN_GENDER); }
                    if (chkGuardianRelation.Checked) { sbQuery.Append(PSPESConstants.REPORT_GUARDIAN_RELATIONSHIP_AR); }
                    if (chkGuardianNationality.Checked) { sbQuery.Append(PSPESConstants.REPORT_GUARDIAN_NATIONALITY_AR); }
                    if (chkGuardianMobile.Checked) { sbQuery.Append(PSPESConstants.REPORT_GUARDIAN_MOBILE); }
                    if (chkGuardianHome.Checked) { sbQuery.Append(PSPESConstants.REPORT_GUARDIAN_HOMENUMBER); }
                    if (chkGuardianEmail.Checked) { sbQuery.Append(PSPESConstants.REPORT_GUARDIAN_EMAIL); }
                    if (chkGuardianEmployer.Checked) { sbQuery.Append(PSPESConstants.REPORT_GUARDIAN_EMPLOYERSECTOR_NAME_AR); }
                    if (chkGuardianEmployer.Checked) { sbQuery.Append(PSPESConstants.REPORT_GUARDIAN_EMPLOYER_NAME_AR); }


                    if (chkCurrentYearSchool.Checked) { sbQuery.Append(PSPESConstants.REPORT_CURRENTYEAR_SCHOOLCODE); }
                    if (chkCurrentYearSchool.Checked) { sbQuery.Append(PSPESConstants.REPORT_CURRENTYEAR_SCHOOLNAME_AR); }
                    if (chkCurrentYearGrade.Checked) { sbQuery.Append(PSPESConstants.REPORT_CURRENTYEAR_GRADE); }
                    if (chkCurrentYearCurriculum.Checked) { sbQuery.Append(PSPESConstants.REPORT_CURRENTYEAR_CURRICULUM_AR); }
                    if (chkCurrentYearResults.Checked) { sbQuery.Append(PSPESConstants.REPORT_CURRENTYEAR_RESULTS); }

                    if (chkPreviousYearSchool.Checked) { sbQuery.Append(PSPESConstants.REPORT_PREVIOUSYEAR_SCHOOLCODE); }
                    if (chkPreviousYearSchool.Checked) { sbQuery.Append(PSPESConstants.REPORT_PREVIOUSYEAR_SCHOOLNAME_AR); }
                    if (chkPreviousYearGrade.Checked) { sbQuery.Append(PSPESConstants.REPORT_PREVIOUSYEAR_GRADE); }
                    if (chkPreviousYearCurriculum.Checked) { sbQuery.Append(PSPESConstants.REPORT_PREVIOUSYEAR_CURRICULUM_AR); }
                    if (chkPreviousYearResults.Checked) { sbQuery.Append(PSPESConstants.REPORT_PREVIOUSYEAR_RESULTS); }

                    if (chkHealthCard.Checked) { sbQuery.Append(PSPESConstants.REPORT_HEALTHINFO_CARD); }
                    if (chkHealthInfo.Checked) { sbQuery.Append(PSPESConstants.REPORT_HEALTHINFO_CENTER); }
                    if (chkFit.Checked) { sbQuery.Append(PSPESConstants.REPORT_HEALTHINFO_FITTOSTUDY); }
                    if (chkSpecialNeed.Checked) { sbQuery.Append(PSPESConstants.REPORT_HEALTHINFO_SPECIALNEED); }
                    if (chkLearning.Checked) { sbQuery.Append(PSPESConstants.REPORT_HEALTHINFO_LEARNINGDIFFICULTIES); }
                    if (chkLearning.Checked) { sbQuery.Append(PSPESConstants.REPORT_HEALTHINFO_HEALTHISSUES); }

                    if (chkDocList.Checked) { sbQuery.Append(PSPESConstants.REPORT_FINALIZE_DOCUMENTLIST); }

                    if (chkTestDate.Checked) { sbQuery.Append(PSPESConstants.REPORT_TEST_DATETIME); }
                    if (chkTestRejected.Checked) { sbQuery.Append(PSPESConstants.REPORT_TEST_FINALRESULTS); }

                    if (chkInterviewDate.Checked) { sbQuery.Append(PSPESConstants.REPORT_INTERVIEW_DATETIME); }
                    if (chkInterviewResults.Checked) { sbQuery.Append(PSPESConstants.REPORT_INTERVIEW_FINALRESULTS); }

                    if (chkFee.Checked) { sbQuery.Append(PSPESConstants.REPORT_RESERVE_AMOUNT); }
                    if (chkInterviewDate.Checked) { sbQuery.Append(PSPESConstants.REPORT_RESERVE_DATETIME); }
                }
                else
                {
                    //FILTERS
                    sbQuery.Append(PSPESConstants.REPORT_SCHOOL_TERM);
                    sbQuery.Append(PSPESConstants.REPORT_SCHOOL_SCHOOLCODE);
                    sbQuery.Append(PSPESConstants.REPORT_SCHOOL_SCHOOLNAME_EN);
                    sbQuery.Append(PSPESConstants.REPORT_STUDENT_GRADE);
                    sbQuery.Append(PSPESConstants.REPORT_SCHOOL_CURRICULUM_EN);
                    sbQuery.Append(PSPESConstants.REPORT_STUDENT_NATIONALITY_EN);
                    sbQuery.Append(PSPESConstants.REPORT_STUDENT_GENDER);
                    sbQuery.Append(PSPESConstants.REPORT_SCHOOL_STATUS);

                    if (chkStudentQID.Checked) { sbQuery.Append(PSPESConstants.REPORT_STUDENT_QID); }
                    if (chkStudentName.Checked)
                    {
                        sbQuery.Append(PSPESConstants.REPORT_STUDENT_NAME_EN);
                        sbQuery.Append(PSPESConstants.REPORT_STUDENT_GENDER);
                    }

                    if (chkRefNo.Checked) { sbQuery.Append(PSPESConstants.REPORT_STUDENT_APPLICATIONNUMBER); }
                    if (chkStudentBirthDate.Checked) { sbQuery.Append(PSPESConstants.REPORT_STUDENT_BIRTHDATE); }
                    //if (chkStudentNationality.Checked) { sbQuery.Append(PSPESConstants.REPORT_STUDENT_NATIONALITY_EN); }
                    if (chkStudentArea.Checked) { sbQuery.Append(PSPESConstants.REPORT_STUDENT_REGIONALAREA); }
                    if (chkStudentTransport.Checked) { sbQuery.Append(PSPESConstants.REPORT_STUDENT_TRANSPORTATION); }
                    if (chkApplicationDate.Checked) { sbQuery.Append(PSPESConstants.REPORT_STUDENT_APPLICATIONDATE); }

                    if (chkGuardianQID.Checked) { sbQuery.Append(PSPESConstants.REPORT_GUARDIAN_QID); }
                    if (chkGuardianName.Checked) { sbQuery.Append(PSPESConstants.REPORT_GUARDIAN_NAME_EN); }
                    if (chkGuardianGender.Checked) { sbQuery.Append(PSPESConstants.REPORT_GUARDIAN_GENDER); }
                    if (chkGuardianRelation.Checked) { sbQuery.Append(PSPESConstants.REPORT_GUARDIAN_RELATIONSHIP_EN); }
                    if (chkGuardianNationality.Checked) { sbQuery.Append(PSPESConstants.REPORT_GUARDIAN_NATIONALITY_EN); }
                    if (chkGuardianMobile.Checked) { sbQuery.Append(PSPESConstants.REPORT_GUARDIAN_MOBILE); }
                    if (chkGuardianHome.Checked) { sbQuery.Append(PSPESConstants.REPORT_GUARDIAN_HOMENUMBER); }
                    if (chkGuardianEmail.Checked) { sbQuery.Append(PSPESConstants.REPORT_GUARDIAN_EMAIL); }
                    if (chkGuardianSector.Checked) { sbQuery.Append(PSPESConstants.REPORT_GUARDIAN_EMPLOYERSECTOR_NAME_EN); }
                    if (chkGuardianEmployer.Checked) { sbQuery.Append(PSPESConstants.REPORT_GUARDIAN_EMPLOYER_NAME_EN); }

                    if (chkCurrentYearSchool.Checked) { sbQuery.Append(PSPESConstants.REPORT_CURRENTYEAR_SCHOOLCODE); }
                    if (chkCurrentYearSchool.Checked) { sbQuery.Append(PSPESConstants.REPORT_CURRENTYEAR_SCHOOLNAME_EN); }
                    if (chkCurrentYearGrade.Checked) { sbQuery.Append(PSPESConstants.REPORT_CURRENTYEAR_GRADE); }
                    if (chkCurrentYearCurriculum.Checked) { sbQuery.Append(PSPESConstants.REPORT_CURRENTYEAR_CURRICULUM_EN); }
                    if (chkCurrentYearResults.Checked) { sbQuery.Append(PSPESConstants.REPORT_CURRENTYEAR_RESULTS); }

                    if (chkPreviousYearSchool.Checked) { sbQuery.Append(PSPESConstants.REPORT_PREVIOUSYEAR_SCHOOLCODE); }
                    if (chkPreviousYearSchool.Checked) { sbQuery.Append(PSPESConstants.REPORT_PREVIOUSYEAR_SCHOOLNAME_EN); }
                    if (chkPreviousYearGrade.Checked) { sbQuery.Append(PSPESConstants.REPORT_PREVIOUSYEAR_GRADE); }
                    if (chkPreviousYearCurriculum.Checked) { sbQuery.Append(PSPESConstants.REPORT_PREVIOUSYEAR_CURRICULUM_EN); }
                    if (chkPreviousYearResults.Checked) { sbQuery.Append(PSPESConstants.REPORT_PREVIOUSYEAR_RESULTS); }


                    if (chkHealthCard.Checked) { sbQuery.Append(PSPESConstants.REPORT_HEALTHINFO_CARD); }
                    if (chkHealthInfo.Checked) { sbQuery.Append(PSPESConstants.REPORT_HEALTHINFO_CENTER); }
                    if (chkFit.Checked) { sbQuery.Append(PSPESConstants.REPORT_HEALTHINFO_FITTOSTUDY); }
                    if (chkSpecialNeed.Checked) { sbQuery.Append(PSPESConstants.REPORT_HEALTHINFO_SPECIALNEED); }
                    if (chkLearning.Checked) { sbQuery.Append(PSPESConstants.REPORT_HEALTHINFO_LEARNINGDIFFICULTIES); }
                    if (chkLearning.Checked) { sbQuery.Append(PSPESConstants.REPORT_HEALTHINFO_HEALTHISSUES); }

                    if (chkDocList.Checked) { sbQuery.Append(PSPESConstants.REPORT_FINALIZE_DOCUMENTLIST); }

                    if (chkTestDate.Checked) { sbQuery.Append(PSPESConstants.REPORT_TEST_DATETIME); }
                    if (chkTestRejected.Checked) { sbQuery.Append(PSPESConstants.REPORT_TEST_FINALRESULTS); }

                    if (chkInterviewDate.Checked) { sbQuery.Append(PSPESConstants.REPORT_INTERVIEW_DATETIME); }
                    if (chkInterviewResults.Checked) { sbQuery.Append(PSPESConstants.REPORT_INTERVIEW_FINALRESULTS); }

                    if (chkFee.Checked) { sbQuery.Append(PSPESConstants.REPORT_RESERVE_AMOUNT); }
                    if (chkInterviewDate.Checked) { sbQuery.Append(PSPESConstants.REPORT_RESERVE_DATETIME); }
                }
                //TERM
                if (sbCondition.Length > 0)
                {
                    if (ddlTerm.SelectedValue.ToLower() != "all")
                    {
                        sbCondition.Append(" AND MOE_TERM = '" + ddlTerm.SelectedValue + "'");
                    }
                }
                else
                {
                    if (ddlTerm.SelectedValue.ToLower() != "all")
                    {
                        sbCondition.Append("MOE_TERM = '" + ddlTerm.SelectedValue + "'");
                    }
                }
                //SCHOOL CODE
                if (sbCondition.Length > 0)
                {
                    if (ddlSchool.SelectedValue.ToLower() != "all")
                    {
                        sbCondition.Append(" AND MOE_SCHOOL_CODE = '" + ddlSchool.SelectedValue + "'");
                    }
                }
                else
                {
                    if (ddlSchool.SelectedValue.ToLower() != "all")
                    {
                        sbCondition.Append("MOE_SCHOOL_CODE = '" + ddlSchool.SelectedValue + "'");
                    }
                }

                //GRADE
                if (sbCondition.Length > 0)
                {
                    if (ddlGrade.SelectedValue.ToLower() != "all")
                    {
                        sbCondition.Append(" AND MOE_APPLIED_GRADE = '" + ddlGrade.SelectedValue + "'");
                    }
                }
                else
                {
                    if (ddlGrade.SelectedValue.ToLower() != "all")
                    {
                        sbCondition.Append("MOE_APPLIED_GRADE = '" + ddlGrade.SelectedValue + "'");
                    }
                }

                //CURRICULUM
                if (sbCondition.Length > 0)
                {
                    if (ddlCurriculum.SelectedValue.ToLower() != "all")
                    {
                        sbCondition.Append(" AND MOE_CURRICULUM_ID = '" + ddlCurriculum.SelectedValue + "'");
                    }
                }
                else
                {
                    if (ddlCurriculum.SelectedValue.ToLower() != "all")
                    {
                        sbCondition.Append("MOE_CURRICULUM_ID = '" + ddlCurriculum.SelectedValue + "'");
                    }
                }

                //GENDER
                if (sbCondition.Length > 0)
                {
                    if (ddlGender.SelectedValue.ToLower() != "all")
                    {
                        sbCondition.Append(" AND MOE_GENDER = '" + ddlGender.SelectedValue + "'");
                    }
                }
                else
                {
                    if (ddlGender.SelectedValue.ToLower() != "all")
                    {
                        sbCondition.Append("MOE_GENDER = '" + ddlGender.SelectedValue + "'");
                    }
                }

                //SCHOOL GENDER
                if (sbCondition.Length > 0)
                {
                    if (ddlSchoolGender.SelectedValue.ToLower() != "all")
                    {
                        sbCondition.Append(" AND MOE_SCHOOL_GENDER = '" + ddlSchoolGender.SelectedValue + "'");
                    }
                }
                else
                {
                    if (ddlSchoolGender.SelectedValue.ToLower() != "all")
                    {
                        sbCondition.Append("MOE_SCHOOL_GENDER = '" + ddlSchoolGender.SelectedValue + "'");
                    }
                }




                //NATIONALITY
                if (sbCondition.Length > 0)
                {
                    if (ddlNationality.SelectedValue.ToLower() != "all")
                    {
                        sbCondition.Append(" AND MOE_COUNTRY_CODE = '" + ddlNationality.SelectedValue + "'");
                    }
                }
                else
                {
                    if (ddlNationality.SelectedValue.ToLower() != "all")
                    {
                        sbCondition.Append("MOE_COUNTRY_CODE = '" + ddlNationality.SelectedValue + "'");
                    }
                }

                //STATUS
                if (sbCondition.Length > 0)
                {
                    if (ddlStatus.SelectedValue.ToLower() != "all")
                    {
                        sbCondition.Append(" AND IsApplicationFinalized = '" + ddlStatus.SelectedValue + "'");
                    }
                }
                else
                {
                    if (ddlStatus.SelectedValue.ToLower() != "all")
                    {
                        sbCondition.Append("IsApplicationFinalized = '" + ddlStatus.SelectedValue + "'");
                    }
                }
                Dictionary<string, string> dictionary = new Dictionary<string, string>();
                dictionary.Add("query", sbQuery.ToString());
                dictionary.Add("condition", sbCondition.ToString());
                dt = MOE_PREENROLLMENT_REPORT_VIEW_Repository.GetAllData(dictionary).Result;
            }
            catch (Exception ex)
            {
                throw;
            }
            return dt;
        }

        #region Bind Controls
        private void BindCurriculum(string term, string schoolCode)
        {
            ddlCurriculum.Items.Clear();
            ddlCurriculum.Items.Add(new ListItem { Text = GetGlobalResourceObject("MOEHE.PSPES", "All").ToString(), Value = "All" });

            List<ListOfValues_Model> oCurriculum = ListOfValues_Repository.GetListOfValues(PSPESConstants.CurriculumsCodesetID).Result;
            if ((oCurriculum != null) && (oCurriculum.Count > 0))
            {
                if (CultureInfo.CurrentUICulture.Name.ToLower() == "ar-sa" || CultureInfo.CurrentCulture.Name.ToLower() == "ar-sa")
                {
                    ddlCurriculum.DataTextField = "DescriptionArabic";
                }
                else
                {
                    ddlCurriculum.DataTextField = "DescriptionEnglish";
                }
                ddlCurriculum.AppendDataBoundItems = true;
                ddlCurriculum.DataSource = oCurriculum;
                ddlCurriculum.DataValueField = "ID";
                ddlCurriculum.DataBind();
                ddlCurriculum.Enabled = true;
                if ((term.ToLower() != "all") && (schoolCode.ToLower() != "all"))
                {
                    ddlCurriculum.SelectedValue = MOE_AGE_VALIDATE_Repository.GetCurriculumIDBySchoolCode(term, schoolCode).Result;
                    ddlCurriculum.Enabled = false;
                }
            }
        }
        private void BindGrades(string term, string schoolCode)
        {
            ddlGrade.Items.Clear();
            ddlGrade.Items.Add(new ListItem { Text = GetGlobalResourceObject("MOEHE.PSPES", "All").ToString(), Value = "All" });
            List<V_Schools_Grades_Ages> oSchoolGrades = V_Schools_Grades_Ages_Repository.GetSchoolGradesAndAges(int.Parse(term), schoolCode).Result;
            if (oSchoolGrades != null && oSchoolGrades.Count > 0)
            {
                if (CultureInfo.CurrentUICulture.Name.ToLower() == "ar-sa" || CultureInfo.CurrentCulture.Name.ToLower() == "ar-sa")
                {
                    ddlGrade.DataTextField = "MOE_GRADE_DESC_ARA";
                }
                else
                {
                    ddlGrade.DataTextField = "MOE_GRADE_DESC_ENG";
                }
                ddlGrade.AppendDataBoundItems = true;
                ddlGrade.DataSource = oSchoolGrades;
                ddlGrade.DataValueField = "MOE_SCHOOL_GRADE";
                ddlGrade.DataBind();
                ddlGrade.Enabled = true;
            }
            else
            {
                ddlGrade.Items.Add(new ListItem { Text = "N", Value = "N" });
                ddlGrade.Items.Add(new ListItem { Text = "PK", Value = "PK" });
                ddlGrade.Items.Add(new ListItem { Text = "KG", Value = "KG" });
                ddlGrade.Items.Add(new ListItem { Text = "01", Value = "01" });
                ddlGrade.Items.Add(new ListItem { Text = "02", Value = "02" });
                ddlGrade.Items.Add(new ListItem { Text = "03", Value = "03" });
                ddlGrade.Items.Add(new ListItem { Text = "04", Value = "04" });
                ddlGrade.Items.Add(new ListItem { Text = "05", Value = "05" });
                ddlGrade.Items.Add(new ListItem { Text = "06", Value = "06" });
                ddlGrade.Items.Add(new ListItem { Text = "07", Value = "07" });
                ddlGrade.Items.Add(new ListItem { Text = "08", Value = "08" });
                ddlGrade.Items.Add(new ListItem { Text = "09", Value = "09" });
                ddlGrade.Items.Add(new ListItem { Text = "10", Value = "10" });
                ddlGrade.Items.Add(new ListItem { Text = "11", Value = "11" });
                ddlGrade.Items.Add(new ListItem { Text = "12", Value = "12" });
                ddlGrade.Items.Add(new ListItem { Text = "13", Value = "13" });
            }

        }
        private void BindGender()
        {
            ddlGender.Items.Clear();
            ddlGender.Items.Add(new ListItem { Text = GetGlobalResourceObject("MOEHE.PSPES", "All").ToString(), Value = "All" });

            if (CultureInfo.CurrentUICulture.Name.ToLower() == "ar-sa" || CultureInfo.CurrentCulture.Name.ToLower() == "ar-sa")
            {
                ddlGender.Items.Add(new ListItem { Text = "Male", Value = "Male" });
                ddlGender.Items.Add(new ListItem { Text = "Female", Value = "Female" });
            }
            else
            {
                ddlGender.Items.Add(new ListItem { Text = "Male", Value = "Male" });
                ddlGender.Items.Add(new ListItem { Text = "Female", Value = "Female" });
            }
        }
        private void BindSchools(string termValue)
        {
            ddlSchool.Items.Clear();
            ddlSchool.Items.Add(new ListItem { Text = GetGlobalResourceObject("MOEHE.PSPES", "All").ToString(), Value = "All" });
            ddlSchool.Enabled = false;
            if (termValue.ToLower() != "all")
            {
                List<MOE_SCHOOL_Model> Schools = MOE_SCHOOL_Repository.GetSchools(termValue, "false").Result;
                List<SchoolModel> SchoolsForBind = new List<SchoolModel>();
                if (CultureInfo.CurrentUICulture.Name.ToLower() == "ar-sa" || CultureInfo.CurrentCulture.Name.ToLower() == "ar-sa")
                {
                    SchoolsForBind = Schools.Select(p => new SchoolModel { ShcoolCode = p.MOE_SCHOOL_CODE, ArabicShcoolName = string.Format("{0} - {1}", p.MOE_SCHOOL_CODE, p.MOE_SCHOOL_NAME_ARA) }).Distinct().ToList();
                    ddlSchool.DataTextField = "ArabicShcoolName";
                }
                else
                {
                    SchoolsForBind = Schools.Select(p => new SchoolModel { ShcoolCode = p.MOE_SCHOOL_CODE, EnglishShcoolName = string.Format("{0} - {1}", p.MOE_SCHOOL_NAME_ENG, p.MOE_SCHOOL_CODE) }).Distinct().ToList();
                    ddlSchool.DataTextField = "EnglishShcoolName";
                }
                ddlSchool.AppendDataBoundItems = true;
                ddlSchool.DataValueField = "ShcoolCode";
                ddlSchool.DataSource = SchoolsForBind;
                ddlSchool.DataBind();
                ddlSchool.Enabled = true;
            }
        }
        private void BindTerms()
        {
            ddlTerm.Items.Clear();
            List<TermModel> AllTerms = TermRepository.GetTerms().Result;
            ddlTerm.DataSource = AllTerms;
            ddlTerm.DataTextField = "TermName";
            ddlTerm.DataValueField = "TermCode";
            ddlTerm.DataBind();
        }
        private void BindStatus()
        {
            ddlStatus.Items.Clear();
            ddlStatus.Items.Add(new ListItem { Text = GetGlobalResourceObject("MOEHE.PSPES", "All").ToString(), Value = "All" });

            if (CultureInfo.CurrentUICulture.Name.ToLower() == "ar-sa" || CultureInfo.CurrentCulture.Name.ToLower() == "ar-sa")
            {
                ddlStatus.Items.Add(new ListItem { Text = "قائمة الانتظار", Value = "0" });
                ddlStatus.Items.Add(new ListItem { Text = "تم استكمال الطلب", Value = "1" });
            }
            else
            {
                ddlStatus.Items.Add(new ListItem { Text = "Waiting List", Value = "0" });
                ddlStatus.Items.Add(new ListItem { Text = "Finalized Application", Value = "1" });
            }
        }
        private void BindNationalities()
        {
            ddlNationality.Items.Clear();
            ddlNationality.Items.Add(new ListItem { Text = GetGlobalResourceObject("MOEHE.PSPES", "All").ToString(), Value = "All" });

            List<MOI_COUNTRY_MAP> Countries = MOI_COUNTRY_MAP_Repository.GetCountries().Result;
            if (CultureInfo.CurrentUICulture.Name.ToLower() == "ar-sa" || CultureInfo.CurrentCulture.Name.ToLower() == "ar-sa")
            {
                ddlNationality.AppendDataBoundItems = true;
                ddlNationality.DataSource = Countries;
                ddlNationality.DataTextField = "COUNTRY_ARA";
                ddlNationality.DataValueField = "MOI_COUNTRY_CODE";
                ddlNationality.DataBind();
            }
            else
            {
                ddlNationality.AppendDataBoundItems = true;
                ddlNationality.DataSource = Countries;
                ddlNationality.DataTextField = "COUNTRY";
                ddlNationality.DataValueField = "MOI_COUNTRY_CODE";
                ddlNationality.DataBind();
            }
        }

        private void BindSchoolGenders()
        {
            ddlSchoolGender.Items.Clear();
            IEnumerable<string> AllSchoolsGenders = MOE_SCHOOL_Repository.GetSchoolsDB().Result.Select(s => s.MOE_SCHOOL_GENDER).Distinct();
            ddlSchoolGender.Items.Add(new ListItem { Text = GetGlobalResourceObject("MOEHE.PSPES", "All").ToString(), Value = "All" });
            ddlSchoolGender.AppendDataBoundItems = true;
            ddlSchoolGender.DataSource = AllSchoolsGenders;

            ddlSchoolGender.DataBind();
        }
        #endregion

        protected void ddlSchool_SelectedIndexChanged(object sender, EventArgs e)
        {
            BindGrades(ddlTerm.SelectedValue, ddlSchool.SelectedValue);
            BindCurriculum(ddlTerm.SelectedValue, ddlSchool.SelectedValue);
            BindGender();
            BindNationalities();
            BindStatus();

            if (ddlSchool.SelectedValue.ToLower() != "all")
            {


                MOE_SCHOOL_Model school_data = MOE_SCHOOL_Repository.GetSchoolInfo(ddlTerm.SelectedValue, ddlSchool.SelectedValue, "false").Result;
                if (school_data != null && school_data.MOE_SCHOOL_CODE != null && school_data.MOE_SCHOOL_GENDER != null)
                {
                    ddlSchoolGender.SelectedValue = school_data.MOE_SCHOOL_GENDER;
                }
                ddlSchoolGender.Enabled = false;
            }

            else
            {
                ddlSchoolGender.Enabled = true;
                ddlSchoolGender.SelectedValue = "All";
            }
        }

        protected void ddlTerm_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlTerm.SelectedValue.ToLower() != "all")
            {
                BindSchools(ddlTerm.SelectedValue);
            }
        }
    }
}
