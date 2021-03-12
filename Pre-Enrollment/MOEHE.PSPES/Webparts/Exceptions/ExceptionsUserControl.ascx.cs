using Microsoft.SharePoint;
using Microsoft.SharePoint.WebControls;
using MOEHE.PSPES.Models;
using MOEHE.PSPES.Repository;

using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;

namespace MOEHE.PSPES.Webparts.Exceptions
{
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
    public partial class ExceptionsUserControl : UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                //dtExceptionExpiryDate.Value = DateTime.Now.Date.ToString("dd/MM/yyyy", CultureInfo.GetCultureInfo(2057));
                try
                {
                    ListItem nextyear = new ListItem(TermRepository.GetTerms().Result.Where(s => s.ACADEMIC_YEAR_DESC == "PREENROLLMENT").Select(d => d).FirstOrDefault().TermName, TermRepository.GetTerms().Result.Where(s => s.ACADEMIC_YEAR_DESC == "PREENROLLMENT").Select(d => d).FirstOrDefault().TermCode);
                    ddlPreEnrollmentTerm.Items.Add(nextyear);

                    string NextYear = string.Format("{0}", DateTime.Now.Year + 1);
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

                }
                catch (Exception ex)
                { }
            }
        }
        private void ClearControls()
        {



            foreach (Control c in this.Controls)
            {
                c.Clear();
            }


        }
        protected void txtQID_TextChanged(object sender, EventArgs e)
        {
            try
            {
                string QID = txtQID.Text;

                ClearControls();
                txtQID.Text = QID;
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

                        }

                        else
                        {
                            txtName.Text = Bio_Model.MOE_ENGLISH_NAME;
                            txtNationality.Text = Bio_Model.MOE_COUNTRY_ENGLISH_NAME;

                        }
                        englishstudentname.Value = Bio_Model.MOE_ENGLISH_NAME;
                        arabicstudentname.Value = Bio_Model.MOE_ARABIC_NAME;

                        txtNationality.Attributes.Add("CountryCode", Bio_Model.MOE_COUNTRY_CODE.ToString());
                        txtGender.Text = Bio_Model.MOE_GENDER;
                        txtDOB.Text = Bio_Model.MOE_DOB;//.Date.ToString("dd/MM/yyyy");

                        pnlExceptionDetails.Visible = true;




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

                        ClearControls();



                        txtQID.Text = "";
                        return;
                    }
                    #endregion








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

                    ClearControls();


                    txtQID.Text = "";
                    return;
                }

                pnlExceptionBoxes.Visible = false;
            }
            catch (Exception ex) { }

        }

        protected void SchoolCodesDropDownList_SelectedIndexChanged(object sender, EventArgs e)
        {
            List<ListOfValues_Model> AllCurricullms = ListOfValues_Repository.GetListOfValues(PSPESConstants.CurriculumsCodesetID).Result;
            List<SupportingDocsModel> SchoolsWithDocumentName = new List<SupportingDocsModel>();
           

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
                List<MOEHE.PSPES.Models.V_Schools_Grades_Ages> SchoolGradesAndAges = V_Schools_Grades_Ages_Repository.GetSchoolGradesAndAges(int.Parse(ddlPreEnrollmentTerm.SelectedValue), SchoolCodesDropDownList.SelectedValue).Result;

                if (SchoolGradesAndAges != null && SchoolGradesAndAges.Count > 0)
                {
                    List<MOEHE.PSPES.Models.V_Schools_Grades_Ages> SortedSchoolDataList = SchoolGradesAndAges.OrderBy(s => s.Weight).ToList();
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
                #endregion

                //load curriculum dropdown and select the school curriculum
                List<ListOfValues_Model> Curriculums_lov = ListOfValues_Repository.GetListOfValues(PSPESConstants.CurriculumsCodesetID).Result;
                MOE_SCHOOL_Model selected_School = MOE_SCHOOL_Repository.GetSchoolInfo(ddlPreEnrollmentTerm.SelectedValue, SchoolCodesDropDownList.SelectedValue, "false").Result;
                ListOfValues_Model SchoolCurriculumModel = Curriculums_lov.Where(D => D.ID == selected_School.MOE_SCHOOL_CURRICULUM_ID).Select(D => D).FirstOrDefault();
                if (SchoolCurriculumModel != null)
                {
                    if ((uint)CultureInfo.CurrentUICulture.LCID == 1025)
                    {
                        txtSchoolCurriculum.Text = SchoolCurriculumModel.DescriptionArabic;
                    }
                    else
                    {
                        txtSchoolCurriculum.Text = SchoolCurriculumModel.DescriptionEnglish;
                    }

                }


            }
        }

        protected void SchoolGradesDropDownList_SelectedIndexChanged(object sender, EventArgs e)
        {

            //clear checked controls
            chkAgeException.Checked = false;
            chkAllowRegWhileCloseException.Checked = false;
            chkAllowRepeatYearException.Checked = false;
            ChkGenderException.Checked = false;
            chkNationalityException.Checked = false;

            //AgeExceptionAttachmentRow.Visible = false;
            //AllowRegWhileCloseExceptionAttachmentRow.Visible = false;
            //NationalityExceptionAttachmentRow.Visible = false;
            //AllowRepeatYearExceptionAttachmentRow.Visible = false;
            //GenderExceptionAttachmentRow.Visible = false;


            AgeExceptionAttachmentRow.Style.Add("display", "none");
            AllowRegWhileCloseExceptionAttachmentRow.Style.Add("display", "none");
            NationalityExceptionAttachmentRow.Style.Add("display", "none");
            AllowRepeatYearExceptionAttachmentRow.Style.Add("display", "none");
            GenderExceptionAttachmentRow.Style.Add("display", "none");


            //AgeExceptionAttachmentLink.Visible = false;
            //AllowRegistrationWhileEnrollmentClosedAttachmentLink.Visible = false;
            //NationalityExceptionAttachmentLink.Visible = false;
            //AllowRepeatYearExceptionAttachmentLink.Visible = false;
            //GenderExceptionAttachmentLink.Visible = false;


            AgeExceptionAttachmentLink.Style.Add("display", "none");
            AllowRegistrationWhileEnrollmentClosedAttachmentLink.Style.Add("display", "none");
            NationalityExceptionAttachmentLink.Style.Add("display", "none");
            AllowRepeatYearExceptionAttachmentLink.Style.Add("display", "none");
            GenderExceptionAttachmentLink.Style.Add("display", "none");


            #region Load Exception Attachments Requirment

            List<MOE_Enrollment_Exception_Types> ExceptionTypes = MOE_ENROLLMENT_EXCEPTION_TYPES_Repository.Get().Result;

            if (ExceptionTypes !=null && ExceptionTypes.Count>0)
            {
                foreach (MOE_Enrollment_Exception_Types exceptionType in ExceptionTypes)
                {
                    if (exceptionType.ExceptionTypeName.ToLower() == "age" )
                    {
                        hdnAgeAttachmentRequired.Value =  exceptionType.AttachmentRequired.ToString();
                        hdnAgeAttachmentTypeID.Value =  exceptionType.ID.ToString();

                        continue;
                    }
                    if (exceptionType.ExceptionTypeName.ToLower() == "allow registration while enrollment closed")
                    {
                        hdnAllowEnrollmentWhileCloseAttachmentRequired.Value = exceptionType.AttachmentRequired.ToString();
                        hdnAllowEnrollmentWhileCloseAttachmentTypeID.Value = exceptionType.ID.ToString();
                        continue;
                    }
                    if (exceptionType.ExceptionTypeName.ToLower() == "nationality")
                    {
                        hdnNationalityAttachmentRequired.Value = exceptionType.AttachmentRequired.ToString();
                        hdnNationalityAttachmentTypeID.Value = exceptionType.ID.ToString();
                        continue;
                    }
                    if (exceptionType.ExceptionTypeName.ToLower() == "allow repeat year")
                    {
                       hdnAllowRepeatYearAttachmentRequired.Value=  exceptionType.AttachmentRequired.ToString();
                        hdnAllowRepeatYearAttachmentTypeID.Value=  exceptionType.ID.ToString();
                        continue;
                    }
                    if (exceptionType.ExceptionTypeName.ToLower() == "gender" )
                    {
                       hdnGenderAttachmentRequired.Value =  exceptionType.AttachmentRequired.ToString();
                        hdnGenderAttachmentTypeID.Value =  exceptionType.ID.ToString();
                        continue;
                    }
                }

            }

            #endregion


            //dtExceptionExpiryDate.Value = DateTime.Now.Date.ToString("dd/MM/yyyy", CultureInfo.GetCultureInfo(2057));
            MOE_ENROLLMENT_EXCEPTION_Model student_exception = MOE_ENROLLMENT_EXCEPTION_Repository.GetEnrollmentExceptionBySchoolAndGrade(txtQID.Text, int.Parse(ddlPreEnrollmentTerm.SelectedValue), SchoolCodesDropDownList.SelectedValue, SchoolGradesDropDownList.SelectedValue).Result;
            
            if (student_exception !=null && student_exception.National_ID!=null)
            {
                chkAgeException.Checked = student_exception.AGE_EXCEPTION != null ? student_exception.AGE_EXCEPTION.Value : false;
                chkAllowRegWhileCloseException.Checked = student_exception.ENROLLMENT_WHILE_CLOSED_EXCEPTION != null ? student_exception.ENROLLMENT_WHILE_CLOSED_EXCEPTION.Value: false;
                chkAllowRepeatYearException.Checked = student_exception.REPEAT_YEAR_EXCEPTION != null ? student_exception.REPEAT_YEAR_EXCEPTION.Value: false;
                ChkGenderException.Checked = student_exception.GENDER_EXCEPTION != null ? student_exception.GENDER_EXCEPTION.Value: false;
                chkNationalityException.Checked =  student_exception.NATIONALITY_EXCEPTION != null ? student_exception.NATIONALITY_EXCEPTION.Value: false;
                dtExceptionExpiryDate.Value = ((DateTime)student_exception.MOE_EXCEPTION_EXPIRY).ToString("dd/MM/yyyy", CultureInfo.GetCultureInfo(2057));


                if (chkAgeException.Checked|| chkAllowRegWhileCloseException.Checked || chkAllowRepeatYearException.Checked || chkNationalityException.Checked || chkNationalityException.Checked)
                {
                    SaveLinkButton.Style.Add("display", "block");
                }

                #region get exception attachemtns if any

                List<Enrollment_Exception_Attachment_Model> ExceptionAttachments = ENROLLMENT_EXCEPTION_ATTACHMENT_Repository.Get(student_exception.ID).Result;
                if (ExceptionAttachments !=null && ExceptionAttachments.Count>0)
                {
                    foreach (Enrollment_Exception_Attachment_Model Attachment in ExceptionAttachments)
                    {
                        if (Attachment.ExceptionTypeName.ToLower()=="age" && Attachment.ExceptionTypeRequired == true && chkAgeException.Checked == true)
                        {
                            //AgeExceptionAttachmentRow.Visible = true;
                            AgeExceptionAttachmentRow.Style.Add("display", "block");
                           

                            if (Attachment.DOCUMENT_LOCATION!=null && Attachment.DOCUMENT_LOCATION!="")
                            {
                                //AgeExceptionAttachmentLink.Visible = true;
                                AgeExceptionAttachmentLink.Style.Add("display", "block");

                                AgeExceptionAttachmentLink.NavigateUrl = Attachment.DOCUMENT_LOCATION;
                            }
                           
                            continue;
                        }
                      
                        if (Attachment.ExceptionTypeName.ToLower() == "allow registration while enrollment closed" && Attachment.ExceptionTypeRequired == true && chkAllowRegWhileCloseException.Checked == true)
                        {

                            //AllowRegWhileCloseExceptionAttachmentRow.Visible = true;
                            AllowRegWhileCloseExceptionAttachmentRow.Style.Add("display", "block");
                           

                            if (Attachment.DOCUMENT_LOCATION!=null && Attachment.DOCUMENT_LOCATION!="")
                            {
                                //AllowRegistrationWhileEnrollmentClosedAttachmentLink.Visible = true;
                                AllowRegistrationWhileEnrollmentClosedAttachmentLink.Style.Add("display", "block");

                                AllowRegistrationWhileEnrollmentClosedAttachmentLink.NavigateUrl = Attachment.DOCUMENT_LOCATION;
                            }
                           
                            continue;
                        }

                        if (Attachment.ExceptionTypeName.ToLower() == "nationality" && Attachment.ExceptionTypeRequired == true && chkNationalityException.Checked == true)
                        {
                            //NationalityExceptionAttachmentRow.Visible = true;
                            NationalityExceptionAttachmentRow.Style.Add("display", "block");
                           

                            if (Attachment.DOCUMENT_LOCATION != null && Attachment.DOCUMENT_LOCATION != "")
                            {
                                //NationalityExceptionAttachmentLink.Visible = true;
                                NationalityExceptionAttachmentLink.Style.Add("display", "block");

                                NationalityExceptionAttachmentLink.NavigateUrl = Attachment.DOCUMENT_LOCATION;
                            }
                           
                            continue;
                        }
                       
                        if (Attachment.ExceptionTypeName.ToLower() == "allow repeat year" && Attachment.ExceptionTypeRequired == true && chkAllowRepeatYearException.Checked == true)
                        {
                            //AllowRepeatYearExceptionAttachmentRow.Visible = true;
                            AllowRepeatYearExceptionAttachmentRow.Style.Add("display", "block");
                         
                            if (Attachment.DOCUMENT_LOCATION != null && Attachment.DOCUMENT_LOCATION != "")
                            {
                               // AllowRepeatYearExceptionAttachmentLink.Visible = true;
                                AllowRepeatYearExceptionAttachmentLink.Style.Add("display", "block");

                                AllowRepeatYearExceptionAttachmentLink.NavigateUrl = Attachment.DOCUMENT_LOCATION;
                            }
                            continue;
                        }
                      
                        if (Attachment.ExceptionTypeName.ToLower() == "gender" && Attachment.ExceptionTypeRequired == true && ChkGenderException.Checked == true)
                        {
                            GenderExceptionAttachmentRow.Visible = true;
                            GenderExceptionAttachmentRow.Style.Add("display", "block");
                            if (Attachment.DOCUMENT_LOCATION != null && Attachment.DOCUMENT_LOCATION != "")
                            {
                                //GenderExceptionAttachmentLink.Visible = true;
                                GenderExceptionAttachmentLink.Style.Add("display", "block");
                                GenderExceptionAttachmentLink.NavigateUrl = Attachment.DOCUMENT_LOCATION;
                            }
                            continue;
                        }
                        
                    }
                }
                #endregion

            }

            
            pnlExceptionBoxes.Visible = true;
        }

        protected void SaveLinkButton_Click(object sender, EventArgs e)
        {
            try
            {

                MOE_ENROLLMENT_EXCEPTION_Model student_exception = new MOE_ENROLLMENT_EXCEPTION_Model()
                {
                    National_ID = decimal.Parse(txtQID.Text),
                    MOE_SCHOOL_CODE=SchoolCodesDropDownList.SelectedValue,
                    MOE_GRADE = SchoolGradesDropDownList.SelectedValue,
                    MOE_TERM = int.Parse(ddlPreEnrollmentTerm.SelectedValue),
                    MOE_EXCEPTION_EXPIRY = DateTime.ParseExact(dtExceptionExpiryDate.Value, "dd/MM/yyyy", CultureInfo.GetCultureInfo(2057)),
                    AGE_EXCEPTION = chkAgeException.Checked,
                    ENROLLMENT_WHILE_CLOSED_EXCEPTION = chkAllowRegWhileCloseException.Checked,
                    GENDER_EXCEPTION= ChkGenderException.Checked,
                    NATIONALITY_EXCEPTION = chkNationalityException.Checked,
                    REPEAT_YEAR_EXCEPTION = chkAllowRepeatYearException.Checked,
                    MOE_User_Name = SPContext.Current.Site.RootWeb.CurrentUser.LoginName
                };

                   DBOperationResult result = MOE_ENROLLMENT_EXCEPTION_Repository.Insert(student_exception).Result;

                #region Save Exception Attachments If Any

                if (hdnAgeAttachmentRequired.Value.ToLower() =="true" )
                {
                    if (fuAgeAttachment.PostedFile != null)
                    {


                        if (fuAgeAttachment.PostedFile.ContentLength > 0)
                        {
                            System.IO.Stream strm = fuAgeAttachment.PostedFile.InputStream;

                            byte[] ApplicantByte = new byte[
                                    Convert.ToInt32(fuAgeAttachment.PostedFile.ContentLength)];

                            strm.Read(ApplicantByte, 0, Convert.ToInt32
                                                               (fuAgeAttachment.PostedFile.ContentLength));
                            string ApplicantFileExtension = System.IO.Path.GetExtension(fuAgeAttachment.FileName);
                            //string ApplicantReference = string.Format("{0}-{1}{2}", DateTime.Now.Year.ToString(), DateTime.Now.Minute, DateTime.Now.Second);
                            string ApplicantReference = String.Format("{0:d9}", (DateTime.Now.Ticks / 10) % 1000000000); ;

                            string FilePath = SaveToDocumentLibrary(ApplicantByte, ApplicantFileExtension, ApplicantReference);
                            Enrollment_Exception_Attachment_Model ageAttachment = new Enrollment_Exception_Attachment_Model()
                            {
                                Enrollment_Exception_ID = result.InsertedID,
                                Enrollment_Exception_Type_ID = int.Parse(hdnAgeAttachmentTypeID.Value),
                                DOCUMENT_LOCATION = FilePath

                            };

                            DBOperationResult AgeAttachmentResult = ENROLLMENT_EXCEPTION_ATTACHMENT_Repository.Insert(ageAttachment).Result;

                        }
                    }
                           
                }


                if (hdnAllowEnrollmentWhileCloseAttachmentRequired.Value.ToLower() == "true")
                {
                    if (fuAllowRegistrationWhileEnrollmentClosedAttachment.PostedFile != null)
                    {


                        if (fuAllowRegistrationWhileEnrollmentClosedAttachment.PostedFile.ContentLength > 0)
                        {
                            System.IO.Stream strm = fuAllowRegistrationWhileEnrollmentClosedAttachment.PostedFile.InputStream;

                            byte[] ApplicantByte = new byte[
                                    Convert.ToInt32(fuAllowRegistrationWhileEnrollmentClosedAttachment.PostedFile.ContentLength)];

                            strm.Read(ApplicantByte, 0, Convert.ToInt32
                                                               (fuAllowRegistrationWhileEnrollmentClosedAttachment.PostedFile.ContentLength));
                            string ApplicantFileExtension = System.IO.Path.GetExtension(fuAllowRegistrationWhileEnrollmentClosedAttachment.FileName);
                            //string ApplicantReference = string.Format("{0}-{1}{2}", DateTime.Now.Year.ToString(), DateTime.Now.Minute, DateTime.Now.Second);
                            string ApplicantReference = String.Format("{0:d9}", (DateTime.Now.Ticks / 10) % 1000000000); ;

                            string FilePath = SaveToDocumentLibrary(ApplicantByte, ApplicantFileExtension, ApplicantReference);
                            Enrollment_Exception_Attachment_Model allowRegisterWhileCloseAttachment = new Enrollment_Exception_Attachment_Model()
                            {
                                Enrollment_Exception_ID = result.InsertedID,
                                Enrollment_Exception_Type_ID = int.Parse(hdnAllowEnrollmentWhileCloseAttachmentTypeID.Value),
                                DOCUMENT_LOCATION = FilePath

                            };

                            DBOperationResult allowRegisterWhileCloseResult = ENROLLMENT_EXCEPTION_ATTACHMENT_Repository.Insert(allowRegisterWhileCloseAttachment).Result;

                        }
                    }

                }

                if (hdnNationalityAttachmentRequired.Value.ToLower() == "true")
                {
                    if (fuNationalityExceptionAttachment.PostedFile != null)
                    {


                        if (fuNationalityExceptionAttachment.PostedFile.ContentLength > 0)
                        {
                            System.IO.Stream strm = fuNationalityExceptionAttachment.PostedFile.InputStream;

                            byte[] ApplicantByte = new byte[
                                    Convert.ToInt32(fuNationalityExceptionAttachment.PostedFile.ContentLength)];

                            strm.Read(ApplicantByte, 0, Convert.ToInt32
                                                               (fuNationalityExceptionAttachment.PostedFile.ContentLength));
                            string ApplicantFileExtension = System.IO.Path.GetExtension(fuNationalityExceptionAttachment.FileName);
                            //string ApplicantReference = string.Format("{0}-{1}{2}", DateTime.Now.Year.ToString(), DateTime.Now.Minute, DateTime.Now.Second);
                            string ApplicantReference = String.Format("{0:d9}", (DateTime.Now.Ticks / 10) % 1000000000); ;

                            string FilePath = SaveToDocumentLibrary(ApplicantByte, ApplicantFileExtension, ApplicantReference);
                            Enrollment_Exception_Attachment_Model nationalityAttachment = new Enrollment_Exception_Attachment_Model()
                            {
                                Enrollment_Exception_ID = result.InsertedID,
                                Enrollment_Exception_Type_ID = int.Parse(hdnNationalityAttachmentTypeID.Value),
                                DOCUMENT_LOCATION = FilePath

                            };

                            DBOperationResult nationalityResult = ENROLLMENT_EXCEPTION_ATTACHMENT_Repository.Insert(nationalityAttachment).Result;

                        }
                    }

                }

                if (hdnAllowRepeatYearAttachmentRequired.Value.ToLower() == "true")
                {
                    if (fuAllowRepeatYearExceptionAttachment.PostedFile != null)
                    {


                        if (fuAllowRepeatYearExceptionAttachment.PostedFile.ContentLength > 0)
                        {
                            System.IO.Stream strm = fuAllowRepeatYearExceptionAttachment.PostedFile.InputStream;

                            byte[] ApplicantByte = new byte[
                                    Convert.ToInt32(fuAllowRepeatYearExceptionAttachment.PostedFile.ContentLength)];

                            strm.Read(ApplicantByte, 0, Convert.ToInt32
                                                               (fuAllowRepeatYearExceptionAttachment.PostedFile.ContentLength));
                            string ApplicantFileExtension = System.IO.Path.GetExtension(fuAllowRepeatYearExceptionAttachment.FileName);
                            //string ApplicantReference = string.Format("{0}-{1}{2}", DateTime.Now.Year.ToString(), DateTime.Now.Minute, DateTime.Now.Second);
                            string ApplicantReference = String.Format("{0:d9}", (DateTime.Now.Ticks / 10) % 1000000000); ;

                            string FilePath = SaveToDocumentLibrary(ApplicantByte, ApplicantFileExtension, ApplicantReference);
                            Enrollment_Exception_Attachment_Model allowRepeatYearAttachment = new Enrollment_Exception_Attachment_Model()
                            {
                                Enrollment_Exception_ID = result.InsertedID,
                                Enrollment_Exception_Type_ID = int.Parse(hdnAllowRepeatYearAttachmentTypeID.Value),
                                DOCUMENT_LOCATION = FilePath

                            };

                            DBOperationResult allowRepeatYearResult = ENROLLMENT_EXCEPTION_ATTACHMENT_Repository.Insert(allowRepeatYearAttachment).Result;

                        }
                    }

                }

                if (hdnGenderAttachmentRequired.Value.ToLower() == "true")
                {
                    if (fuGenderExceptionAttachment.PostedFile != null)
                    {


                        if (fuGenderExceptionAttachment.PostedFile.ContentLength > 0)
                        {
                            System.IO.Stream strm = fuGenderExceptionAttachment.PostedFile.InputStream;

                            byte[] ApplicantByte = new byte[
                                    Convert.ToInt32(fuGenderExceptionAttachment.PostedFile.ContentLength)];

                            strm.Read(ApplicantByte, 0, Convert.ToInt32
                                                               (fuGenderExceptionAttachment.PostedFile.ContentLength));
                            string ApplicantFileExtension = System.IO.Path.GetExtension(fuGenderExceptionAttachment.FileName);
                            //string ApplicantReference = string.Format("{0}-{1}{2}", DateTime.Now.Year.ToString(), DateTime.Now.Minute, DateTime.Now.Second);
                            string ApplicantReference = String.Format("{0:d9}", (DateTime.Now.Ticks / 10) % 1000000000); ;

                            string FilePath = SaveToDocumentLibrary(ApplicantByte, ApplicantFileExtension, ApplicantReference);
                            Enrollment_Exception_Attachment_Model genderAttachment = new Enrollment_Exception_Attachment_Model()
                            {
                                Enrollment_Exception_ID = result.InsertedID,
                                Enrollment_Exception_Type_ID = int.Parse(hdnGenderAttachmentTypeID.Value),
                                DOCUMENT_LOCATION = FilePath

                            };

                            DBOperationResult genderResult = ENROLLMENT_EXCEPTION_ATTACHMENT_Repository.Insert(genderAttachment).Result;

                        }
                    }

                }

                #endregion
                if (result.EnglishResult == PSPESConstants.InsertionDone)
                {
                    if ((uint)CultureInfo.CurrentUICulture.LCID == 1033)
                    {
                        CustomMessageBoxHelper.Show(this.Page, "Data Saved Successfully");
                        ClearControls();
                        return;

                    }
                    else
                    {
                        // CustomMessageBoxHelper.Show(this.Page, "من فضلك ت");
                        CustomMessageBoxHelper.Show(this.Page, "تم الحفظ بنجاح");
                        ClearControls();
                        return;

                    }
                }
            }
            catch (Exception ex)
            {
                DBOperationResult ReturnedResult2 = TransactionLogsRepository.Insert(new MOEHE.PSPES.Models.TransactionsLog { CreatedDate = DateTime.Now, ShortDescription = "Exception Control", Description = "Exceptions Control Exception, Exception:" + ex.Message, UserID = SPContext.Current.Site.RootWeb.CurrentUser.LoginName }).Result;
                if ((uint)CultureInfo.CurrentUICulture.LCID == 1033)
                {
                    CustomMessageBoxHelper.Show(this.Page, PSPESConstants.EnglishSaveError);

                }
                else
                {
                    // CustomMessageBoxHelper.Show(this.Page, "من فضلك ت");
                    CustomMessageBoxHelper.Show(this.Page, PSPESConstants.ArabicSaveError);

                }
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
    }
}

