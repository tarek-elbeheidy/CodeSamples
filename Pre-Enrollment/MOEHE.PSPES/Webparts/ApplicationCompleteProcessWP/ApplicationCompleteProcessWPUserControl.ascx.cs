using Microsoft.SharePoint;
using MOEHE.PSPES.Models;
using MOEHE.PSPES.Repository;
using System;
using System.Linq;
using System.Collections.Generic;
using System.Globalization;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Data;
using System.Configuration;
using Microsoft.SharePoint.WebControls;

namespace MOEHE.PSPES.Webparts.ApplicationCompleteProcessWP
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
            // etc....
        }
        public static void Disable(this Control c)
        {
            if (c == null)
            {
                throw new ArgumentNullException("c");
            }
            if (c is TextBox)
            {
                TextBox tb = (TextBox)c;
                tb.Enabled = false;
            }
            else if (c is DropDownList)
            {
                DropDownList ddl = (DropDownList)c;
                ddl.Enabled = false;
            }
            else if (c is GridView)
            {
                GridView gv = (GridView)c;
                gv.Enabled = false;
            }
            else if (c is DateTimeControl)
            {
                DateTimeControl dt = (DateTimeControl)c;
                dt.Enabled = false;
            }

            else if (c is CheckBox)
            {
                CheckBox chk = (CheckBox)c;
                chk.Enabled = false;
            }

            else if (c is Button)
            {
                Button btn = (Button)c;
                btn.Enabled = false;
            }

            else if (c is LinkButton)
            {
                LinkButton lnkbtn = (LinkButton)c;
                lnkbtn.Enabled = false;
            }

            // etc....
        }
    }


    public partial class ApplicationCompleteProcessWPUserControl : UserControl
    {
        //string ApplicantReferenceNumber = "2019-30120-10-1";
        //decimal NationalID = 3323232323;
        public string QID = "";
        public string StudentName = "";
        public string PreviousSchools = "";
        void FillTestResultDDL()
        {

            ListItem ItemTemp1 = new ListItem();
            ItemTemp1.Value = "0";
            if ((uint)CultureInfo.CurrentUICulture.LCID == 1025)
            {
                ItemTemp1.Text = "--اختر--";
            }
            else
            {
                ItemTemp1.Text = "--Select--";
            }

            // ItemTemp1.Selected = true;
            DDLTestResult.Items.Add(ItemTemp1);

            ListItem ItemTemp2 = new ListItem();
            ItemTemp2.Value = "1";
            if ((uint)CultureInfo.CurrentUICulture.LCID == 1025)
            {
                ItemTemp2.Text = "ناجح";
            }
            else
            {
                ItemTemp2.Text = "Pass";
            }


            DDLTestResult.Items.Add(ItemTemp2);

            ListItem ItemTemp3 = new ListItem();
            ItemTemp3.Value = "2";
            if ((uint)CultureInfo.CurrentUICulture.LCID == 1025)
            {
                ItemTemp3.Text = "راسب";
            }
            else
            {
                ItemTemp3.Text = "Fail";
            }

            DDLTestResult.Items.Add(ItemTemp3);


        }


        void FillInterviewResultDDL()
        {

            ListItem ItemTemp1 = new ListItem();
            ItemTemp1.Value = "0";
            if ((uint)CultureInfo.CurrentUICulture.LCID == 1025)
            {
                ItemTemp1.Text = "--اختر--";
            }
            else
            {
                ItemTemp1.Text = "--Select--";
            }

            // ItemTemp1.Selected = true;
            DDLinterviewReslt.Items.Add(ItemTemp1);

            ListItem ItemTemp2 = new ListItem();
            ItemTemp2.Value = "1";
            if ((uint)CultureInfo.CurrentUICulture.LCID == 1025)
            {
                ItemTemp2.Text = "ناجح";
            }
            else
            {
                ItemTemp2.Text = "Pass";
            }


            DDLinterviewReslt.Items.Add(ItemTemp2);

            ListItem ItemTemp3 = new ListItem();
            ItemTemp3.Value = "2";
            if ((uint)CultureInfo.CurrentUICulture.LCID == 1025)
            {
                ItemTemp3.Text = "راسب";
            }
            else
            {
                ItemTemp3.Text = "Fail";
            }

            DDLinterviewReslt.Items.Add(ItemTemp3);


        }


        protected void Page_Load(object sender, EventArgs e)
        {

            if (!IsPostBack)
            {


                //if (gvRequiredDocuments.Rows.Count > 0)
                //{
                //    PanelDocuments.Visible = true;

                //}
                //else
                //{
                //    LblSupportingDocs.Visible = true;
                //    PanelDocuments.Visible = false;
                //}
            }
            //if (PayFeesRequestRadioHiddenField.Value!="")
            //{
            //    ViewState["PayFeesRequestRadioHiddenField"] = PayFeesRequestRadioHiddenField.Value;

            //}
            //if (NoActionTakenRadioHiddenField.Value!="")
            //{
            //    ViewState["NoActionTakenRadioHiddenField"] = NoActionTakenRadioHiddenField.Value;

            //}
            //if (!Page.IsPostBack || PayFeesRequestRadioHiddenField.Value == ""|| NoActionTakenRadioHiddenField.Value == "")
            //{

            //    if (PayFeesRequestRadioHiddenField.Value == "")
            //    {
            //        if (ViewState["PayFeesRequestRadioHiddenField"] != null)
            //        {
            //            PayFeesRequestRadioHiddenField.Value = ViewState["PayFeesRequestRadioHiddenField"].ToString();
            //        }

            //    }
            //    else
            //    {
            //        ViewState["PayFeesRequestRadioHiddenField"] = PayFeesRequestRadioHiddenField.Value;
            //    }
            //    if (NoActionTakenRadioHiddenField.Value == "")
            //    {
            //        if (ViewState["NoActionTakenRadioHiddenField"] != null)
            //        {
            //            NoActionTakenRadioHiddenField.Value = ViewState["NoActionTakenRadioHiddenField"].ToString();
            //        }

            //    }
            //    else
            //    {
            //        ViewState["NoActionTakenRadioHiddenField"] = PayFeesRequestRadioHiddenField.Value;
            //    }

            if (Request.QueryString["ApplicationRefNumber"] != null && Request.QueryString["QID"] != null)
            {
                EditClicked(Request.QueryString["ApplicationRefNumber"], Request.QueryString["QID"]);
            }



            if (!Page.IsPostBack)
            {

                FillTestResultDDL();
                FillInterviewResultDDL();


            }

            bool isMinistryUser = true;
            //string Date= PayByDateTime.Value;
            // bool PayFeesRE = PayFeesNoActionRadio.Checked;
            // MOE_APPLICATION_DATA_Model mOE_APPLICATION_DATA_Model = MOE_APPLICATION_DATA_Repository.GetBy(ApplicantReferenceNumber).Result;
            //Then View  All schools Document type for specific grade



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

                            SPFile ApplicantFile = mylibrary.Files.Add(ApplicantFileName, ApplicantByte, true);
                            int ApplicantFileID = ApplicantFile.Item.ID;
                            ApplicantFilePath = SPContext.Current.Site.Url + "/" + ApplicantFile.Item.Url.ToString();

                            objWeb.AllowUnsafeUpdates = false;

                        }
                    }

                }
            }));
            return ApplicantFilePath;
        }

        protected void SaveDocumentsLinkButton_Click(object sender, EventArgs e)
        {
            try
            {
                MOE_APPLICATION_DATA_Model mOE_APPLICATION_DATA_Model = MOE_APPLICATION_DATA_Repository.GetByRefAndID(TxtAppRef.Text, TxtQID.Text).Result;

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
                            string ApplicantReference = string.Format("{0}-{1}{2}", DateTime.Now.Year.ToString(), DateTime.Now.Minute, DateTime.Now.Second);

                            string FilePath = SaveToDocumentLibrary(ApplicantByte, ApplicantFileExtension, ApplicantReference);


                            var Task = MOE_APPL_SUPPORT_DOCS_Repository.Insert(new MOE_APPL_SUPPORT_DOCS_Model_Not_Serializable { DeactiveOtherApplications = false, MOE_APPL_NBR = TxtAppRef.Text, MOE_DOCUMENT_LOCATION = FilePath, MOE_DOCUMENT_TYPE_ID = DocumentTypeIDHiddenField.Value, MOE_TRANSACTION_DTTM = DateTime.Now, MOE_USERID = SPContext.Current.Site.RootWeb.CurrentUser.LoginName, NATIONAL_ID = Convert.ToDecimal(TxtQID.Text) }).Result;


                        }
                    }
                }
            }
            catch { }
        }

        public string GetQID(string ApplicantReferenceNumber)
        {
            string StudentQID = "";
            StudentQID = MOE_APPLICATION_DATA_Repository.GetBy(ApplicantReferenceNumber).Result.NATIONAL_ID.ToString();
            return StudentQID;
        }
        protected void FeesPaidConfirmationLinkButton_Click(object sender, EventArgs e)
        {
            try
            {
                string PaidReservationFeesText = PaidFeesTextBox.Text; //Request.Form["PaidFeesTextBox"].ToString();
                string PayFeesOnDateTime = Request.Form["PaidFeesDateTime"].ToString();
                MessageTemplate messageTemplate = new MessageTemplate();
                #region get Guardian info: Mobile number
                MOE_APPLICATION_DATA_Model appDTModel = MOE_APPLICATION_DATA_Repository.GetByRefAndID(TxtAppRef.Text, TxtQID.Text).Result;

                List<SeatCapacityModel> seatCapacityModel = SeatCapacityRepository.CheckExistsSeatCapacity(DateTime.Now.Year + 1, appDTModel.MOE_SCHOOL_CODE, appDTModel.MOE_APPLIED_GRADE).Result;
                int TotalOfReservation = SeatReservationFeeRepository.GetCountOfApplientApplications(DateTime.Now.Year + 1, appDTModel.MOE_SCHOOL_CODE, appDTModel.MOE_APPLIED_GRADE).Result;
                int AvailableSeats = 0;
                if (seatCapacityModel.Count > 0)
                {
                    if (seatCapacityModel[0].MOE_SEAT_DISTRIBUTION - TotalOfReservation > 0)
                    {
                        AvailableSeats = seatCapacityModel[0].MOE_SEAT_DISTRIBUTION;
                    }
                }
                List<SeatReservationFee> CheckIFReservationDone = SeatReservationFeeRepository.GetbyRefAndID(TxtAppRef.Text, TxtQID.Text).Result;

                if (CheckIFReservationDone.Count <= 0)
                {

                    if (AvailableSeats > 0)
                    {

                        MOE_APPL_ACAD_INFO_Model CurrentAcad_Model = MOE_APPL_ACAD_INFO_Repository.GetAcadInfoByQIDAndTerm(double.Parse(appDTModel.NATIONAL_ID.Value.ToString()).ToString(), /*DateTime.Now.Year.ToString()*/TermRepository.GetTerms().Result.Where(s => s.ACADEMIC_YEAR_DESC == "CURRENT").Select(d => d).FirstOrDefault().TermCode).Result;
                        if (CurrentAcad_Model != null && CurrentAcad_Model.MOE_TERM != 0 && CurrentAcad_Model.MOE_TERM.ToString() != "" && CurrentAcad_Model.HasPendingPayment)
                        {
                            if ((uint)CultureInfo.CurrentUICulture.LCID == 1025)
                            { CustomMessageBoxHelper.Show(this.Page, "لا يمكن الاستمرار، هذا الطالب عليه مستحقات مالية ، يرجى التواصل مع ولي الأمر لمراجعة المدرسة السابقة "); return; }
                            else { CustomMessageBoxHelper.Show(this.Page, "Can not continue, this student has financial dues, please communicate with the guardian to review the previous school"); return; }
                        }
                        else
                        {
                            List<MOE_GUARDIAN_DATA_Model> Guardian = new List<MOE_GUARDIAN_DATA_Model>();
                            string QIDD = appDTModel.NATIONAL_ID.Value.ToString();
                            Guardian = MOE_GUARDIAN_DATA_Repository.GetStudentContactsInfo(double.Parse(appDTModel.NATIONAL_ID.Value.ToString()).ToString()).Result;
                            MOE_GUARDIAN_DATA_Model mOE_GUARDIAN_DATA_Model = Guardian.Where(G => G.MOE_ISGUARDIAN).Select(G => G).FirstOrDefault();

                            #endregion


                            if (PaidReservationFeesText.Length > 0)
                            {
                                if (PayFeesOnDateTime.Length > 0)
                                {
                                    DateTime PayFeesOnDateTimeParsed = DateTime.ParseExact(PayFeesOnDateTime, "dd/mm/yyyy", System.Globalization.CultureInfo.InvariantCulture);

                                    var InsretSeatReservationTaskResult = SeatReservationFeeRepository.Insert(new SeatReservationFeeModel { ApplicantReferenceNumber = TxtAppRef.Text, CreatedBy = SPContext.Current.Site.RootWeb.CurrentUser.LoginName, CreatedDate = DateTime.Now, FeesPaidDate = PayFeesOnDateTimeParsed, NationalID = Convert.ToDecimal(TxtQID.Text), ReservationFeesAmount = long.Parse(PaidReservationFeesText) }).Result;
                                    string messageTitle = PSPESConstants.Seatreservationconfirmation;
                                    messageTemplate = MessageTemplateRepository.Getby(messageTitle, 9).Result;
                                    #region Create the message
                                    string ArabicSchoolName = MOE_SCHOOL_Repository.GetSchoolInfo(/*DateTime.Now.Year.ToString()*/TermRepository.GetTerms().Result.Where(s => s.ACADEMIC_YEAR_DESC == "CURRENT").Select(d => d).FirstOrDefault().TermCode, appDTModel.MOE_SCHOOL_CODE, "false").Result.MOE_SCHOOL_NAME_ARA;
                                    string EnglishSchoolName = MOE_SCHOOL_Repository.GetSchoolInfo(/*DateTime.Now.Year.ToString()*/TermRepository.GetTerms().Result.Where(s => s.ACADEMIC_YEAR_DESC == "CURRENT").Select(d => d).FirstOrDefault().TermCode, appDTModel.MOE_SCHOOL_CODE, "false").Result.MOE_SCHOOL_NAME_ENG;

                                    string smsMessage = "";
                                    if ((uint)CultureInfo.CurrentUICulture.LCID == 1025)
                                    {
                                        smsMessage = messageTemplate.ArabicMessage;
                                        smsMessage = smsMessage.Replace("%parentName%", mOE_GUARDIAN_DATA_Model.bio_data.MOE_ARABIC_NAME);
                                        smsMessage = smsMessage.Replace("%StudentName%", TxtStudentNm.Text);
                                        smsMessage = smsMessage.Replace("%School Name%", ArabicSchoolName);
                                    }
                                    else
                                    {
                                        smsMessage = messageTemplate.EnglishMessage;
                                        smsMessage = smsMessage.Replace("%parentName%", mOE_GUARDIAN_DATA_Model.bio_data.MOE_ENGLISH_NAME);
                                        smsMessage = smsMessage.Replace("%StudentName%", TxtStudentNm.Text);
                                        smsMessage = smsMessage.Replace("%School Name%", EnglishSchoolName);
                                    }



                                    smsMessage = smsMessage.Replace("%StudentQID%", TxtQID.Text);
                                    smsMessage = smsMessage.Replace("%reference number%", appDTModel.MOE_APPL_REF_NBR);
                                    //smsMsg = smsMsg.Replace("%school name%", TxtAppRef.Text);
                                    smsMessage = smsMessage.Replace("%Grade%", appDTModel.MOE_APPLIED_GRADE);
                                    smsMessage = smsMessage.Replace("%xxxx%", PaidReservationFeesText);
                                    smsMessage = smsMessage.Replace("%Academic year%", (appDTModel.MOE_TERM - 1).ToString() + "-" + appDTModel.MOE_TERM.ToString());

                                    #endregion
                                    string smsStatus = SendSMS.SendMessage(messageTitle, mOE_GUARDIAN_DATA_Model.MOE_MOBILE_CONTACT_NBR, smsMessage);
                                    //  smsStatus = SendSMS.SendMessage(messageTitle, "55531606", smsMessage);

                                    if (smsStatus == "1000")
                                    {
                                        SMSHistoryModel smsHist = new SMSHistoryModel()
                                        {
                                            MOE_APPL_REF_NBR = appDTModel.MOE_APPL_REF_NBR,
                                            MsgStatus = "SENT",
                                            MsgText = smsMessage,
                                            MsgTime = DateTime.Now.ToString(),
                                            MsgType = "PayFees",
                                            MsgTitle = messageTitle,
                                            MobileNumber = mOE_GUARDIAN_DATA_Model.MOE_MOBILE_CONTACT_NBR,
                                            MsgSender = SPContext.Current.Site.RootWeb.CurrentUser.Name


                                        };

                                        var insertSMSHistory = SMSHistoryRepository.Insert(smsHist).Result;

                                        ////update seats
                                        //DBOperationResult dBOperationResult = SeatCapacityRepository.Insert(new SeatCapacityModel
                                        //{
                                        //    MOE_SCHOOL_CODE = seatCapacityModel[0].MOE_SCHOOL_CODE,
                                        //    ID = seatCapacityModel[0].ID,
                                        //    MOE_PREENROL_SEATS = AvailableSeats - 1,
                                        //    MOE_SCHOOL_GRADE = seatCapacityModel[0].MOE_SCHOOL_GRADE,
                                        //    MOE_SEAT_DISTRIBUTION = seatCapacityModel[0].MOE_SEAT_DISTRIBUTION
                                        //         ,
                                        //    MOE_TERM = seatCapacityModel[0].MOE_TERM,
                                        //    MOE_TRANSACTION_DTMM = DateTime.Now,
                                        //    MOE_USERID = SPContext.Current.Site.RootWeb.CurrentUser.LoginName
                                        //}).Result;


                                        BindingUtility.LoadMessageHistory(PayFessConfirmationMessageHistoryGridView, TxtAppRef.Text, "null", PSPESConstants.Seatreservationconfirmation, mOE_GUARDIAN_DATA_Model.MOE_MOBILE_CONTACT_NBR, TxtQID.Text);

                                    }
                                    else
                                    {
                                        SMSHistoryModel smsHist = new SMSHistoryModel()
                                        {
                                            MOE_APPL_REF_NBR = appDTModel.MOE_APPL_REF_NBR,
                                            MsgStatus = "Fail",
                                            MsgText = smsMessage,
                                            MsgTime = DateTime.Now.ToString(),
                                            MsgType = "PayFees",
                                            MsgTitle = messageTitle,
                                            MobileNumber = mOE_GUARDIAN_DATA_Model.MOE_MOBILE_CONTACT_NBR,
                                            MsgSender = SPContext.Current.Site.RootWeb.CurrentUser.Name


                                        };

                                        var insertSMSHistory = SMSHistoryRepository.Insert(smsHist).Result;
                                        CustomMessageBoxHelper.Show(this.Page, "Message sent fail ");

                                    }//deactive other applicaitons

                                    DBOperationResult dBOperationResultForApplcationData = MOE_APPLICATION_DATA_Repository.Insert(new MOE_APPLICATION_DATA_Model
                                    {
                                        DeactiveOtherApplications = true,
                                        IsActive = false,
                                        MOE_APPL_REF_NBR = TxtAppRef.Text,
                                        NATIONAL_ID = appDTModel.NATIONAL_ID
                                    }).Result;

                                }
                                else
                                {
                                    CustomMessageBoxHelper.Show(this.Page, "Please enter the date");

                                }
                            }
                            else
                            {

                                if ((uint)CultureInfo.CurrentUICulture.LCID == 1025)
                                { CustomMessageBoxHelper.Show(this.Page, "الرجاء إدخال المبلغ"); }
                                else { CustomMessageBoxHelper.Show(this.Page, "Please enter the amount"); }


                            }
                        }

                    }
                    else
                    {

                        if ((uint)CultureInfo.CurrentUICulture.LCID == 1025)
                        { CustomMessageBoxHelper.Show(this.Page, "قد وصلت إلى الحد الأقصى لقدرة المقاعد المخصصة للصف. لا يمكنك تسجيل هذا الطالب ما لم تتم زيادة المقاعد في هذا الصف"); }
                        else { CustomMessageBoxHelper.Show(this.Page, "You have reached the maximum capacity of the seats that is allocated for the grade. You cannot enroll this student unless seats are increased in this grade"); }

                        //no seats avialble
                    }
                }
                else
                {
                    if ((uint)CultureInfo.CurrentUICulture.LCID == 1025)
                    { CustomMessageBoxHelper.Show(this.Page, "رسوم الحجز مدفوعة "); }
                    else { CustomMessageBoxHelper.Show(this.Page, "This Applcation reservation fees Already Paid"); }



                }
            }
            catch { }
        }

        protected void PayFeesSMSLinkButton_Click(object sender, EventArgs e)
        {

            try
            {
                //we need to check also if he already pay the fees
                if (PayFeesRequestRadioHiddenField.Value == "true")
                {
                    #region get Guardian info: Mobile number
                    MOE_APPLICATION_DATA_Model appDTModel = MOE_APPLICATION_DATA_Repository.GetByRefAndID(TxtAppRef.Text, TxtQID.Text).Result;

                    List<MOE_GUARDIAN_DATA_Model> Guardian = new List<MOE_GUARDIAN_DATA_Model>();
                    //string QIDD = appDTModel.NATIONAL_ID.Value.ToString();
                    Guardian = MOE_GUARDIAN_DATA_Repository.GetStudentContactsInfo(double.Parse(appDTModel.NATIONAL_ID.Value.ToString()).ToString()).Result;
                    MOE_GUARDIAN_DATA_Model mOE_GUARDIAN_DATA_Model = Guardian.Where(G => G.MOE_ISGUARDIAN).Select(G => G).FirstOrDefault();

                    #endregion

                    string ReservationFeesTextBoxForSMS = Request.Form["ReservationFeesTextBoxForSMS"].ToString();
                    string PayFeesOnDateTime = Request.Form["PayFeesOnDateTime"].ToString();
                    MessageTemplate messageTemplate = new MessageTemplate();
                    List<SeatCapacityModel> seatCapacityModel = SeatCapacityRepository.CheckExistsSeatCapacity(DateTime.Now.Year + 1, appDTModel.MOE_SCHOOL_CODE, appDTModel.MOE_APPLIED_GRADE).Result;
                    int AvailableSeats = 0;
                    if (seatCapacityModel.Count > 0)
                    {
                        if (seatCapacityModel[0].MOE_SEAT_DISTRIBUTION > 0)
                        {
                            AvailableSeats = seatCapacityModel[0].MOE_SEAT_DISTRIBUTION;
                        }
                    }
                    List<SeatReservationFee> CheckIFReservationDone = SeatReservationFeeRepository.GetbyRefAndID(TxtAppRef.Text, TxtQID.Text).Result;

                    if (CheckIFReservationDone.Count <= 0)
                    {
                        if (AvailableSeats > 0)
                        {
                            //check if the student has pending payment or not
                            MOE_APPL_ACAD_INFO_Model CurrentAcad_Model = MOE_APPL_ACAD_INFO_Repository.GetAcadInfoByQIDAndTerm(double.Parse(appDTModel.NATIONAL_ID.Value.ToString()).ToString(), /*DateTime.Now.Year.ToString()*/TermRepository.GetTerms().Result.Where(s => s.ACADEMIC_YEAR_DESC == "CURRENT").Select(d => d).FirstOrDefault().TermCode).Result;
                            if (CurrentAcad_Model != null && CurrentAcad_Model.MOE_TERM != 0 && CurrentAcad_Model.MOE_TERM.ToString() != "" && CurrentAcad_Model.HasPendingPayment)
                            {
                                if ((uint)CultureInfo.CurrentUICulture.LCID == 1025)
                                { CustomMessageBoxHelper.Show(this.Page, "لا يمكن الاستمرار، هذا الطالب عليه مستحقات مالية ، يرجى التواصل مع ولي الأمر لمراجعة المدرسة السابقة "); return; }
                                else { CustomMessageBoxHelper.Show(this.Page, "Can not continue, this student has financial dues, please communicate with the guardian to review the previous school"); return; }
                            }
                            else
                            {

                                if (ReservationFeesTextBoxForSMS.Length > 0)
                                {
                                    if (PayFeesOnDateTime.Length > 0)
                                    {


                                        string messageTitle = PSPESConstants.SeatreservationFeesInvite;
                                        messageTemplate = MessageTemplateRepository.Getby(messageTitle, 8).Result;
                                        string ArabicSchoolName = MOE_SCHOOL_Repository.GetSchoolInfo(/*DateTime.Now.Year.ToString()*/TermRepository.GetTerms().Result.Where(s => s.ACADEMIC_YEAR_DESC == "CURRENT").Select(d => d).FirstOrDefault().TermCode, appDTModel.MOE_SCHOOL_CODE, "false").Result.MOE_SCHOOL_NAME_ARA;
                                        string EnglishSchoolName = MOE_SCHOOL_Repository.GetSchoolInfo(/*DateTime.Now.Year.ToString()*/TermRepository.GetTerms().Result.Where(s => s.ACADEMIC_YEAR_DESC == "CURRENT").Select(d => d).FirstOrDefault().TermCode, appDTModel.MOE_SCHOOL_CODE, "false").Result.MOE_SCHOOL_NAME_ENG;
                                        #region Create the message
                                        string smsMessage = "";
                                        if ((uint)CultureInfo.CurrentUICulture.LCID == 1025)
                                        {
                                            smsMessage = messageTemplate.ArabicMessage;

                                            smsMessage = smsMessage.Replace("%parentName%", mOE_GUARDIAN_DATA_Model.bio_data.MOE_ARABIC_NAME);
                                            smsMessage = smsMessage.Replace("%StudentName%", TxtStudentNm.Text);
                                            smsMessage = smsMessage.Replace("%School Name%", ArabicSchoolName);

                                        }
                                        else
                                        {
                                            smsMessage = messageTemplate.EnglishMessage;

                                            smsMessage = smsMessage.Replace("%parentName%", mOE_GUARDIAN_DATA_Model.bio_data.MOE_ENGLISH_NAME);
                                            smsMessage = smsMessage.Replace("%StudentName%", TxtStudentNm.Text);
                                            smsMessage = smsMessage.Replace("%School Name%", EnglishSchoolName);
                                        }


                                        smsMessage = smsMessage.Replace("%StudentQID%", TxtQID.Text);
                                        smsMessage = smsMessage.Replace("%reference number%", appDTModel.MOE_APPL_REF_NBR);
                                        smsMessage = smsMessage.Replace("%Academic year%", (appDTModel.MOE_TERM - 1).ToString() + "-" + appDTModel.MOE_TERM.ToString());
                                        smsMessage = smsMessage.Replace("%Grade%", appDTModel.MOE_APPLIED_GRADE);
                                        smsMessage = smsMessage.Replace("%xxxx%", ReservationFeesTextBoxForSMS);
                                        smsMessage = smsMessage.Replace("%Paybydate%", PayFeesOnDateTime);
                                        #endregion

                                        // string smsStatus = SendSMS.SendMessage(messageTitle, "55531606", smsMessage);

                                        string smsStatus = SendSMS.SendMessage(messageTitle, mOE_GUARDIAN_DATA_Model.MOE_MOBILE_CONTACT_NBR, smsMessage);
                                        if (smsStatus == "1000")
                                        {
                                            SMSHistoryModel smsHist = new SMSHistoryModel()
                                            {
                                                MOE_APPL_REF_NBR = appDTModel.MOE_APPL_REF_NBR,
                                                MsgStatus = "SENT",
                                                MsgText = smsMessage,
                                                MsgTime = DateTime.Now.ToString(),
                                                MsgType = "PayFees",
                                                MsgTitle = messageTitle,
                                                MobileNumber = mOE_GUARDIAN_DATA_Model.MOE_MOBILE_CONTACT_NBR,
                                                MsgSender = SPContext.Current.Site.RootWeb.CurrentUser.Name,
                                                Description = ReservationFeesTextBoxForSMS



                                            };

                                            var insertSMSHistory = SMSHistoryRepository.Insert(smsHist).Result;

                                            BindingUtility.LoadMessageHistory(PayFessMessageHistoryGridView, TxtAppRef.Text, "null", PSPESConstants.SeatreservationFeesInvite, mOE_GUARDIAN_DATA_Model.MOE_MOBILE_CONTACT_NBR, TxtQID.Text);



                                        }
                                        else
                                        {
                                            SMSHistoryModel smsHist = new SMSHistoryModel()
                                            {
                                                MOE_APPL_REF_NBR = appDTModel.MOE_APPL_REF_NBR,
                                                MsgStatus = "Fail",
                                                MsgText = smsMessage,
                                                MsgTime = DateTime.Now.ToString(),
                                                MsgType = "PayFees",
                                                MsgTitle = messageTitle,
                                                MobileNumber = mOE_GUARDIAN_DATA_Model.MOE_MOBILE_CONTACT_NBR,
                                                MsgSender = SPContext.Current.Site.RootWeb.CurrentUser.Name,
                                                Description = ReservationFeesTextBoxForSMS


                                            };

                                            var insertSMSHistory = SMSHistoryRepository.Insert(smsHist).Result;

                                            if ((uint)CultureInfo.CurrentUICulture.LCID == 1025)
                                            { CustomMessageBoxHelper.Show(this.Page, "لم يتم ارسال الرسالة"); }
                                            else { CustomMessageBoxHelper.Show(this.Page, "Message sent fail "); }


                                        }
                                    }
                                    else
                                    {
                                        if ((uint)CultureInfo.CurrentUICulture.LCID == 1025)
                                        { CustomMessageBoxHelper.Show(this.Page, "الرجاء إدخال التاريخ"); }
                                        else { CustomMessageBoxHelper.Show(this.Page, "Please enter the date "); }


                                    }
                                }
                                else
                                {

                                    if ((uint)CultureInfo.CurrentUICulture.LCID == 1025)
                                    { CustomMessageBoxHelper.Show(this.Page, "الرجاء إدخال المبلغ"); }
                                    else { CustomMessageBoxHelper.Show(this.Page, "Please enter the amount "); }


                                }
                            }
                        }
                        else
                        {

                            //if ((uint)CultureInfo.CurrentUICulture.LCID == 1025)
                            //{ CustomMessageBoxHelper.Show(this.Page, "لا توجد مقاعد متاحة لهذا الصف"); }
                            //else { CustomMessageBoxHelper.Show(this.Page, "No Seats Available for this grade "); }

                            if ((uint)CultureInfo.CurrentUICulture.LCID == 1025)
                            { CustomMessageBoxHelper.Show(this.Page, "قد وصلت إلى الحد الأقصى لقدرة المقاعد المخصصة للصف. لا يمكنك استكمال هذا الطلب ما لم تتم زيادة المقاعد في هذا الصف"); }
                            else { CustomMessageBoxHelper.Show(this.Page, "You have reached the maximum capacity of the seats that is allocated for the grade. You cannot finalize this application unless seats are increased in this grade"); }

                            //no seats avialble
                        }
                    }
                    else
                    {

                        if ((uint)CultureInfo.CurrentUICulture.LCID == 1025)
                        { CustomMessageBoxHelper.Show(this.Page, "رسوم الحجز مدفوعة "); }
                        else { CustomMessageBoxHelper.Show(this.Page, "This Application Already Paid "); }


                    }
                }
                else
                {

                    if ((uint)CultureInfo.CurrentUICulture.LCID == 1025)
                    { CustomMessageBoxHelper.Show(this.Page, "الرجاء تحديد الإجراء "); }
                    else { CustomMessageBoxHelper.Show(this.Page, "Please Select The Action  "); }


                }
            }
            catch { }
        }

        protected void ViewFeesPaidConfirmationLLinkButton_Click(object sender, EventArgs e)
        {
            if (PayFessConfirmationMessageHistoryGridView.Rows.Count > 0)
            {
                PayFessConfirmationMessageHistoryGridView.Visible = !PayFessConfirmationMessageHistoryGridView.Visible;
            }
            else
            {
                if ((uint)CultureInfo.CurrentUICulture.LCID == 1025)
                { CustomMessageBoxHelper.Show(this.Page, "لم يتم إرسال أي رسالة من قبل "); }
                else { CustomMessageBoxHelper.Show(this.Page, "No Message Sent Before "); }

            }

            if (PayFessConfirmationMessageHistoryGridView.Visible)
            {


                if ((uint)CultureInfo.CurrentUICulture.LCID == 1025)
                { ViewFeesPaidConfirmationLLinkButton.Text = "إخفاء تاريخ الرسائل القصيرة"; }
                else { ViewFeesPaidConfirmationLLinkButton.Text = "Hide SMS History"; }
            }
            else
            {
                if ((uint)CultureInfo.CurrentUICulture.LCID == 1025)
                { ViewFeesPaidConfirmationLLinkButton.Text = "تظهر تاريخ الرسائل القصيرة"; }
                else { ViewFeesPaidConfirmationLLinkButton.Text = "Show SMS History"; }



            }
        }

        protected void ViewPayFeesSMSLinkButton1_Click(object sender, EventArgs e)
        {
            if (PayFessMessageHistoryGridView.Rows.Count > 0)
            {
                PayFessMessageHistoryGridView.Visible = !PayFessMessageHistoryGridView.Visible;
            }
            else
            {
                if ((uint)CultureInfo.CurrentUICulture.LCID == 1025)
                { CustomMessageBoxHelper.Show(this.Page, "لم يتم إرسال أي رسالة من قبل "); }
                else { CustomMessageBoxHelper.Show(this.Page, "No Message Sent Before "); }

            }

            if (PayFessMessageHistoryGridView.Visible)
            {
                if ((uint)CultureInfo.CurrentUICulture.LCID == 1025)
                { ViewPayFeesSMSLinkButton1.Text = "إخفاء تاريخ الرسائل القصيرة"; }
                else { ViewPayFeesSMSLinkButton1.Text = "Hide SMS History"; }


            }
            else
            {
                if ((uint)CultureInfo.CurrentUICulture.LCID == 1025)
                { ViewPayFeesSMSLinkButton1.Text = "تظهر تاريخ الرسائل القصيرة"; }
                else { ViewPayFeesSMSLinkButton1.Text = "Show SMS History"; }



            }
        }

        protected void SearchLinkButton_Click(object sender, EventArgs e)
        {
            //initializing controls

            DDLTestResult.Visible = true;
            FuploadTestReslt.Visible = true;
            SendTestResultLinkButton.Visible = true;
            DDLinterviewReslt.Visible = true;
            SendInterviewResultLinkButton1.Visible = true;

            //search
            ////search

            if (TxtAppRef.Text.Length > 0)
            {
                EditClicked(TxtAppRef.Text, TxtQID.Text);
                //MOE_APPLICATION_DATA_Model appDTModel = MOE_APPLICATION_DATA_Repository.GetBy(TxtAppRef.Text).Result;



                //if (appDTModel == null)  //|| appDTModel.Count==0
                //{
                //    //LblappRefNum.Text = "This Application Reference Number does not exist.";
                //    //LblappRefNum.Visible = true;
                //    CustomMessageBoxHelper.Show(this.Page, "This Application Reference Number does not exist ");
                //    return;
                //}
                //else
                //{
                //    //disabling the search criteria and button
                //    //TxtAppRef.Text = ApplictionRefernceNumber;
                //    //TxtQID.Text = decimal.Truncate((decimal)appDTModel.NATIONAL_ID).ToString();

                //    //LblappRefNum.Visible = false;
                //    TxtWListNum.Text = appDTModel.MOE_WAITLIST_NUMBER.ToString();
                //    TxtPreEnGrade.Text = appDTModel.MOE_APPLIED_GRADE.ToString();
                //    TxtAppDT.Text = appDTModel.MOE_APPL_DATE.ToString();
                //    TxtPreEnScl.Text = appDTModel.MOE_SCHOOL_CODE;

                //    bool isMinistryUser = true;
                //    string ApplicantReferenceNumber = TxtAppRef.Text;



                //    #region loading TestResult Document  and SMS hISTORY if exists in the db
                //    gvTestRst.DataSource = null;
                //    gvTestRst.DataBind();
                //    List<TestResultModel> StudentTestResult = TestResultRepository.GetTestResult(ApplicantReferenceNumber).Result;
                //    if (StudentTestResult.Count > 0)
                //    {
                //        DDLTestResult.Visible = false;
                //        FuploadTestReslt.Visible = false;
                //        SendTestResultLinkButton.Visible = false;


                //        gvTestRst.DataSource = StudentTestResult;
                //        gvTestRst.DataBind();
                //        List<SMSHistoryModel> SMSHistModl = SMSHistoryRepository.GetSMSHistory(ApplicantReferenceNumber, "Test").Result;
                //        if (SMSHistModl.Count > 0)  // != null)
                //        {
                //            //can add further logic to check msgtype
                //            SendTestInvatationLinkButton.Enabled = false;
                //        }

                //    }
                //    BindingUtility.LoadMessageHistory(TestResultSMSGridView, ApplicantReferenceNumber, "Test");

                //    #endregion

                //    #region loading InterviewResult AND SMS History if exists in the db
                //    GVinterviewResult.DataSource = null;
                //    GVinterviewResult.DataBind();
                //    List<InterviewResultModel> StudentIntResult = InterviewResultRepository.GetInterviewResult(ApplicantReferenceNumber).Result;
                //    if (StudentIntResult.Count > 0)
                //    {
                //        DDLinterviewReslt.Visible = false;
                //        SendInterviewResultLinkButton1.Visible = false;
                //        GVinterviewResult.DataSource = StudentIntResult;
                //        GVinterviewResult.DataBind();
                //        List<SMSHistoryModel> SMSHistModl2 = SMSHistoryRepository.GetSMSHistory(ApplicantReferenceNumber, "Interview").Result;
                //        if (SMSHistModl2.Count > 0)  // != null)
                //        {
                //            //can add further logic to check msgtype
                //            SendInterviewSMSLinkButton1.Enabled = false;
                //        }

                //    }
                //    BindingUtility.LoadMessageHistory(InterviewSMSHistoryGridView, ApplicantReferenceNumber, "Interview");

                //    #endregion




                //    MOE_APPLICATION_DATA_Model mOE_APPLICATION_DATA_Model = MOE_APPLICATION_DATA_Repository.GetBy(ApplicantReferenceNumber).Result;
                //    //Then View  All schools Document type for specific grade

                //    BindingUtility.LoadMessageHistory(PayFessMessageHistoryGridView, ApplicantReferenceNumber, "null", PSPESConstants.SeatreservationFeesInvite);


                //    BindingUtility.LoadMessageHistory(PayFessConfirmationMessageHistoryGridView, ApplicantReferenceNumber, "null", PSPESConstants.Seatreservationconfirmation);

                //    if ((uint)CultureInfo.CurrentUICulture.LCID == 1025)
                //    {
                //        BindingUtility.LoadRequiredSupportingDocs(gvRequiredDocuments, ApplicantReferenceNumber, Convert.ToDecimal(TxtQID.Text), new SupportingDocsModel { SchoolCode = mOE_APPLICATION_DATA_Model.MOE_SCHOOL_CODE, Term = mOE_APPLICATION_DATA_Model.MOE_TERM.ToString(), Grade = mOE_APPLICATION_DATA_Model.MOE_APPLIED_GRADE, SeacrhByTermAndSchoolCodeAndGrade = true }, false, isMinistryUser, SPContext.Current.Site.Url);

                //    }
                //    else
                //    {
                //        BindingUtility.LoadRequiredSupportingDocs(gvRequiredDocuments, ApplicantReferenceNumber, Convert.ToDecimal(TxtQID.Text), new SupportingDocsModel { SchoolCode = mOE_APPLICATION_DATA_Model.MOE_SCHOOL_CODE, Term = mOE_APPLICATION_DATA_Model.MOE_TERM.ToString(), Grade = mOE_APPLICATION_DATA_Model.MOE_APPLIED_GRADE, SeacrhByTermAndSchoolCodeAndGrade = true }, true, isMinistryUser, SPContext.Current.Site.Url);


                //    }




                //}
            }
        }

        private void ClearControls()
        {

            //string SchoolName = txtPreEnrollmentSchool.Text;

            foreach (Control c in this.Controls)
            {
                c.Clear();
            }

            //txtPreEnrollmentSchool.Text = SchoolName;
            //txtAvailableSeatsInRequestedGrade.Value = "";
            //txtWaitListNumber.Value = "";
        }

        private void DisableControls()
        {

            //string SchoolName = txtPreEnrollmentSchool.Text;

            foreach (Control c in this.Controls)
            {
                c.Disable();
            }


        }

        private void EditClicked(string ApplictionRefernceNumber, string QID)
        {
            //if (TxtQID.Text.Length > 0)
            //{
            MOE_APPLICATION_DATA_Model appDTModel = MOE_APPLICATION_DATA_Repository.GetByRefAndID(ApplictionRefernceNumber, QID).Result;
            List<SeatCapacityModel> seatCapacityModel = SeatCapacityRepository.CheckExistsSeatCapacity(DateTime.Now.Year + 1, appDTModel.MOE_SCHOOL_CODE, appDTModel.MOE_APPLIED_GRADE).Result;
            int AvailableSeats = 0;
            if (seatCapacityModel.Count > 0)
            {
                if (seatCapacityModel[0].MOE_SEAT_DISTRIBUTION > 0)
                {
                    AvailableSeats = seatCapacityModel[0].MOE_SEAT_DISTRIBUTION;
                }
            }

            if (AvailableSeats > 0)
            {
                //SaveDocumentsLinkButton.Visible = true;
                //SendTestInvatationLinkButton.Visible = true;
                //SendTestResultLinkButton.Visible = true;
                //SendInterviewSMSLinkButton1.Visible = true;
                //SendInterviewResultLinkButton1.Visible = true;
                //PayFeesSMSLinkButton.Visible = true;
                //CLinkButton.Visible = true;

                if (ApplictionRefernceNumber.Length > 0)
                {




                    if (appDTModel == null)  //|| appDTModel.Count==0
                    {
                        //LblappRefNum.Text = "This Application Reference Number does not exist.";
                        //LblappRefNum.Visible = true;

                        if ((uint)CultureInfo.CurrentUICulture.LCID == 1025)
                        { CustomMessageBoxHelper.Show(this.Page, "هذا الرقم المرجعي غير موجود"); }
                        else { CustomMessageBoxHelper.Show(this.Page, "This Application Reference Number does not exist"); }

                        return;
                    }
                    else
                    {

                        UserHelper userhelper = ADutility.GetUserDetails(SPContext.Current.Site.RootWeb.CurrentUser);
                        string schoolCode = userhelper.DepartmentID;
                        if (schoolCode != appDTModel.MOE_SCHOOL_CODE)
                        {
                            string errorMessage = "";
                            if ((uint)CultureInfo.CurrentUICulture.LCID == 1025)
                            {
                                errorMessage = "هذا الرقم المرجعي غير تابع لمدرستكم";
                            }
                            else
                            {
                                errorMessage = "This reference number is not within your school";
                            }

                            CustomMessageBoxHelper.Show(this.Page, errorMessage);
                            return;
                        }
                        else
                        {

                            //tarek el beheidy => 06.03.2018 check if application is finalized or not

                            List<SeatReservationFee> finalizedApplication = SeatReservationFeeRepository.GetbyRefAndID(ApplictionRefernceNumber, QID).Result;

                            if (finalizedApplication.Count > 0)
                            {
                                //TxtAppRef.Enabled = false;
                                //TxtQID.Enabled = false;

                                //TxtWListNum.Enabled = false;
                                //TxtPreEnGrade.Enabled = false;
                                //TxtAppDT.Enabled = false;
                                //TxtPreEnScl.Enabled = false;

                                //DisableControls();
                                //TxtAppRef.Enabled = true;
                                //TxtQID.Enabled = true;
                                //SearchLinkButton.Enabled = true;
                                SaveDocumentsLinkButton.Visible = false;
                                SendTestInvatationLinkButton.Visible = false;
                                SendTestResultLinkButton.Visible = false;
                                SendInterviewSMSLinkButton1.Visible = false;
                                SendInterviewResultLinkButton1.Visible = false;
                                PayFeesSMSLinkButton.Visible = false;
                                CLinkButton.Visible = false;

                                TxtWListNum.Text = Convert.ToString(this.GetGlobalResourceObject("MOEHE.PSPES", "FinalizedStatus"));
                                string errorMessage = "";
                                if ((uint)CultureInfo.CurrentUICulture.LCID == 1025)
                                {
                                    errorMessage = "هذا الطلب تم استكماله من قبل";
                                }
                                else
                                {
                                    errorMessage = "This application has been finalized";
                                }
                                CustomMessageBoxHelper.Show(this.Page, errorMessage);
                            }

                            else
                            {

                                TxtWListNum.Text = Convert.ToString(this.GetGlobalResourceObject("MOEHE.PSPES", "WaitList")) + "-" + appDTModel.MOE_WAITLIST_NUMBER.ToString();
                            }





                            //disabling the search criteria and button
                            TxtAppRef.Text = ApplictionRefernceNumber;
                            TxtQID.Text = decimal.Truncate((decimal)appDTModel.NATIONAL_ID).ToString();

                            List<MOE_GUARDIAN_DATA_Model> Guardian = new List<MOE_GUARDIAN_DATA_Model>();
                            Guardian = MOE_GUARDIAN_DATA_Repository.GetStudentContactsInfo(TxtQID.Text).Result;
                            string GuardianMobileNumber = "";
                            string confirmationMessage = "";
                            try
                            {
                                GuardianMobileNumber = Guardian[0].MOE_MOBILE_CONTACT_NBR;
                            }
                            catch (Exception ex)
                            {
                                if ((uint)CultureInfo.CurrentUICulture.LCID == 1025)
                                {
                                    confirmationMessage = ":لا تتوفر معلومات ولي الأمر ، يرجى اتباع الخطوات أدناه لاستكمال الطلب \n\n تعديل الطلب - \n إضافة معلومات ولي الأمر - \n حفظ الطلب - \n استكمال الطلب -";
                                }
                                else
                                {
                                    confirmationMessage = "The guardian information is not available, please follow the below steps to finalize the application:\n\n - Edit the application\n - Add the guardian information \n - Save the application\n - Finalize the application ";
                                }
                                CustomMessageBoxHelper.Show(this.Page, confirmationMessage);

                                Page.ClientScript.RegisterStartupScript(
                                                    Page.GetType(),
                                                    "MessageBox",
                                                    "<script language='javascript'>window.history.back();</script>"
                                                    );
                               // return;
                            }


                            //LblappRefNum.Visible = false;
                            // TxtWListNum.Text = appDTModel.MOE_WAITLIST_NUMBER.ToString();
                            TxtPreEnGrade.Text = appDTModel.MOE_APPLIED_GRADE.ToString();
                            TxtAppDT.Text = ((DateTime)appDTModel.MOE_APPL_DATE).ToString("dd/MM/yyyy", CultureInfo.GetCultureInfo(2057));
                            TxtPreEnScl.Text = appDTModel.MOE_SCHOOL_CODE;

                            bool isMinistryUser = true;
                            string ApplicantReferenceNumber = ApplictionRefernceNumber;



                            #region loading TestResult Document  and SMS hISTORY if exists in the db


                            gvTestRst.DataSource = null;
                            gvTestRst.DataBind();
                            List<TestResultModel> StudentTestResult = TestResultRepository.GetTestResult(ApplicantReferenceNumber, TxtQID.Text).Result;
                            if (StudentTestResult.Count > 0)
                            {
                                PanelTestRslt.Visible = false;
                                // LbltestResult.Visible = true;


                                DDLTestResult.Visible = false;
                                FuploadTestReslt.Visible = false;
                                SendTestResultLinkButton.Visible = false;


                                gvTestRst.DataSource = StudentTestResult;
                                gvTestRst.DataBind();
                                List<SMSHistoryModel> SMSHistModl = SMSHistoryRepository.GetSMSHistory(ApplicantReferenceNumber, "Test", GuardianMobileNumber, TxtQID.Text).Result;
                                if (SMSHistModl.Count > 0)  // != null)
                                {
                                    //can add further logic to check msgtype
                                    //SendTestInvatationLinkButton.Enabled = false;
                                }

                            }

                            BindingUtility.LoadMessageHistory(TestResultSMSGridView, ApplicantReferenceNumber, "Test", GuardianMobileNumber, TxtQID.Text);

                            #endregion

                            #region loading InterviewResult AND SMS History if exists in the db
                            List<InterviewResultModel> StudentIntResult = InterviewResultRepository.GetInterviewResult(ApplicantReferenceNumber, TxtQID.Text).Result;
                            if (StudentIntResult.Count > 0)
                            {
                                PanelInterviewrslt.Visible = false;
                                //LblInterviewresult.Visible = true;

                                DDLinterviewReslt.Visible = false;
                                SendInterviewResultLinkButton1.Visible = false;
                                GVinterviewResult.DataSource = StudentIntResult;
                                GVinterviewResult.DataBind();
                                List<SMSHistoryModel> SMSHistModl2 = SMSHistoryRepository.GetSMSHistory(ApplicantReferenceNumber, "Interview", GuardianMobileNumber, TxtQID.Text).Result;
                                if (SMSHistModl2.Count > 0)  // != null)
                                {
                                    //can add further logic to check msgtype
                                    //SendInterviewSMSLinkButton1.Enabled = false;
                                }

                            }

                            BindingUtility.LoadMessageHistory(InterviewSMSHistoryGridView, ApplicantReferenceNumber, "Interview", GuardianMobileNumber, TxtQID.Text);

                            #endregion




                            MOE_APPLICATION_DATA_Model mOE_APPLICATION_DATA_Model = MOE_APPLICATION_DATA_Repository.GetByRefAndID(ApplicantReferenceNumber, QID).Result;
                            //Then View  All schools Document type for specific grade

                            BindingUtility.LoadMessageHistory(PayFessMessageHistoryGridView, ApplicantReferenceNumber, "null", PSPESConstants.SeatreservationFeesInvite, GuardianMobileNumber, TxtQID.Text);


                            BindingUtility.LoadMessageHistory(PayFessConfirmationMessageHistoryGridView, ApplicantReferenceNumber, "null", PSPESConstants.Seatreservationconfirmation, GuardianMobileNumber, TxtQID.Text);

                            if ((uint)CultureInfo.CurrentUICulture.LCID == 1025)
                            {
                                BindingUtility.LoadRequiredSupportingDocs(gvRequiredDocuments, ApplicantReferenceNumber, Convert.ToDecimal(TxtQID.Text), new SupportingDocsModel { SchoolCode = mOE_APPLICATION_DATA_Model.MOE_SCHOOL_CODE, Term = mOE_APPLICATION_DATA_Model.MOE_TERM.ToString(), Grade = mOE_APPLICATION_DATA_Model.MOE_APPLIED_GRADE, SeacrhByTermAndSchoolCodeAndGrade = true }, false, isMinistryUser, SPContext.Current.Site.Url);

                            }
                            else
                            {
                                BindingUtility.LoadRequiredSupportingDocs(gvRequiredDocuments, ApplicantReferenceNumber, Convert.ToDecimal(TxtQID.Text), new SupportingDocsModel { SchoolCode = mOE_APPLICATION_DATA_Model.MOE_SCHOOL_CODE, Term = mOE_APPLICATION_DATA_Model.MOE_TERM.ToString(), Grade = mOE_APPLICATION_DATA_Model.MOE_APPLIED_GRADE, SeacrhByTermAndSchoolCodeAndGrade = true }, true, isMinistryUser, SPContext.Current.Site.Url);


                            }

                            if (gvRequiredDocuments.Rows.Count > 0)
                            {
                                PanelDocuments.Visible = true;

                            }
                            else
                            {
                                LblSupportingDocs.Visible = true;
                                dvNoDocuments.Visible = true;
                                PanelDocuments.Visible = false;
                            }


                            //tarek.elbeheidy 07.03.2018    if Pay fees invite was sent before, default the fees text box to the fees amount


                            List<SMSHistoryModel> SMSMessageHistoryList = SMSHistoryRepository.GetSMSHistory(ApplicantReferenceNumber, "null", PSPESConstants.SeatreservationFeesInvite, GuardianMobileNumber, TxtQID.Text).Result;
                            if (SMSMessageHistoryList.Count > 0)
                            {
                                //Request.Form["PaidFeesTextBox"] = SMSMessageHistoryList.FirstOrDefault().Description;
                                if ((SMSMessageHistoryList.Where(p => p.MsgStatus == "SENT").OrderByDescending(x => x.MsgTime).FirstOrDefault()) != null)
                                {


                                    PaidFeesTextBox.Text = SMSMessageHistoryList.Where(p => p.MsgStatus == "SENT").OrderByDescending(x => x.MsgTime).FirstOrDefault().Description;
                                }
                            }



                            //#region If exists, Load sms history into Test- Grid


                            //if ((SMSHistoryRepository.GetSMSHistory(ApplicantReferenceNumber, "Test")).Result.Count > 0)  // != null)
                            //{
                            //    //List<SMSHistoryModel> SMSHistModl = SMSHistoryRepository.GetSMSHistory(ApplicantReferenceNumber, "Test").Result;

                            //    //DataTable dt = new DataTable();

                            //    //dt.Columns.Add("Message Type", typeof(string));
                            //    //dt.Columns.Add("Message Text", typeof(string));
                            //    //dt.Columns.Add("Sent Time", typeof(string));
                            //    //dt.Columns.Add("Status", typeof(string));


                            //    ////foreach (schoolGrade sc in SchoolDataList)
                            //    //foreach (var item in SMSHistModl)
                            //    //{
                            //    //    DataRow dr = dt.NewRow();



                            //    //    dr["Message Type"] = item.MsgType;
                            //    //    dr["Message Text"] = item.MsgText;
                            //    //    dr["Sent Time"] = item.MsgTime;
                            //    //    dr["Status"] = item.MsgStatus;

                            //    //    dt.Rows.Add(dr);
                            //    //}
                            //    BindingUtility.LoadMessageHistory(TestResultSMSGridView, ApplicantReferenceNumber, "Test");

                            //}
                            //else
                            //{
                            //}

                            //#endregion

                            //#region If exists, Load sms history into Interview- Grid
                            //if ((SMSHistoryRepository.GetSMSHistory(ApplicantReferenceNumber, "Interview")).Result.Count > 0)  // != null)
                            //{
                            //    //    List<SMSHistoryModel> SMSHistModl = SMSHistoryRepository.GetSMSHistory(ApplicantReferenceNumber, "Interview").Result;

                            //    //    DataTable dt = new DataTable();

                            //    //    dt.Columns.Add("Message Type", typeof(string));
                            //    //    dt.Columns.Add("Message Text", typeof(string));
                            //    //    dt.Columns.Add("Sent Time", typeof(string));
                            //    //    dt.Columns.Add("Status", typeof(string));


                            //    //    //foreach (schoolGrade sc in SchoolDataList)
                            //    //    foreach (var item in SMSHistModl)
                            //    //    {
                            //    //        DataRow dr = dt.NewRow();



                            //    //        dr["Message Type"] = item.MsgType;
                            //    //        dr["Message Text"] = item.MsgText;
                            //    //        dr["Sent Time"] = item.MsgTime;
                            //    //        dr["Status"] = item.MsgStatus;

                            //    //        dt.Rows.Add(dr);
                            //    //    }
                            //    //    //GrdVInterview.DataSource = dt;
                            //    //    //GrdVInterview.DataBind();
                            //}
                            ////else
                            ////{
                            ////}

                            //#endregion

                            #region GetAdditional Information from MOE_biodata

                            MOE_BIO_DATA_Model BioDataMdl = MOE_BIO_DATA_Repository.GetBioDataByQID(TxtQID.Text).Result;


                            #endregion

                            //add arabic /english


                            if ((uint)CultureInfo.CurrentUICulture.LCID == 1025)
                            {
                                TxtNationality.Text = BioDataMdl.MOE_COUNTRY_ARABIC_NAME;
                                TxtStudentNm.Text = BioDataMdl.MOE_ARABIC_NAME;
                            }
                            else
                            {
                                TxtNationality.Text = BioDataMdl.MOE_COUNTRY_ENGLISH_NAME;
                                TxtStudentNm.Text = BioDataMdl.MOE_ENGLISH_NAME;
                            }

                            TxtGender.Text = BioDataMdl.MOE_GENDER;


                        }
                    }
                }

                else
                {
                    if ((uint)CultureInfo.CurrentUICulture.LCID == 1025)
                    { CustomMessageBoxHelper.Show(this.Page, "يرجى ادخال رقم الطلب"); }
                    else { CustomMessageBoxHelper.Show(this.Page, "Please Enter Application Reference Number "); }


                    return;
                }
            }
            else
            {

                if ((uint)CultureInfo.CurrentUICulture.LCID == 1025)
                { CustomMessageBoxHelper.Confirm(this.Page, "قد وصلت إلى الحد الأقصى لقدرة المقاعد المخصصة للصف. لا يمكنك استكمال هذا الطلب ما لم تتم زيادة المقاعد في هذا الصف", "SearchApplications.aspx"); }
                else { CustomMessageBoxHelper.Confirm(this.Page, "You have reached the maximum capacity of the seats that is allocated for the grade. You cannot finalize this application unless seats are increased in this grade", "SearchApplications.aspx"); }


                /*SaveDocumentsLinkButton.Visible = false;
                SendTestInvatationLinkButton.Visible = false;
                SendTestResultLinkButton.Visible = false;
                SendInterviewSMSLinkButton1.Visible = false;
                SendInterviewResultLinkButton1.Visible = false;
                PayFeesSMSLinkButton.Visible = false;
                CLinkButton.Visible = false;
                if (ApplictionRefernceNumber.Length > 0)
                {




                    if (appDTModel == null)  //|| appDTModel.Count==0
                    {
                        //LblappRefNum.Text = "This Application Reference Number does not exist.";
                        //LblappRefNum.Visible = true;

                        if ((uint)CultureInfo.CurrentUICulture.LCID == 1025)
                        { CustomMessageBoxHelper.Show(this.Page, "هذا الرقم المرجعي غير موجود"); }
                        else { CustomMessageBoxHelper.Show(this.Page, "This Application Reference Number does not exist"); }

                        return;
                    }
                    else
                    {

                        //tarek el beheidy => 06.03.2018 check if application is finalized or not

                        List<SeatReservationFee> finalizedApplication = SeatReservationFeeRepository.Getby(ApplictionRefernceNumber).Result;

                        if (finalizedApplication.Count > 0)
                        {
                            //TxtAppRef.Enabled = false;
                            //TxtQID.Enabled = false;

                            //TxtWListNum.Enabled = false;
                            //TxtPreEnGrade.Enabled = false;
                            //TxtAppDT.Enabled = false;
                            //TxtPreEnScl.Enabled = false;

                            DisableControls();
                            TxtAppRef.Enabled = true;
                            TxtQID.Enabled = true;
                            SearchLinkButton.Enabled = true;
                            TxtWListNum.Text = Convert.ToString(this.GetGlobalResourceObject("MOEHE.PSPES", "FinalizedStatus"));
                        }

                        else
                        {

                            TxtWListNum.Text = Convert.ToString(this.GetGlobalResourceObject("MOEHE.PSPES", "WaitList")) + "-" + appDTModel.MOE_WAITLIST_NUMBER.ToString();
                        }


                        //disabling the search criteria and button
                        TxtAppRef.Text = ApplictionRefernceNumber;
                        TxtQID.Text = decimal.Truncate((decimal)appDTModel.NATIONAL_ID).ToString();

                        //LblappRefNum.Visible = false;
                        // TxtWListNum.Text = appDTModel.MOE_WAITLIST_NUMBER.ToString();
                        TxtPreEnGrade.Text = appDTModel.MOE_APPLIED_GRADE.ToString();
                        TxtAppDT.Text = ((DateTime)appDTModel.MOE_APPL_DATE).ToString("dd/MM/yyyy", CultureInfo.GetCultureInfo(2057));
                        TxtPreEnScl.Text = appDTModel.MOE_SCHOOL_CODE;

                        bool isMinistryUser = true;
                        string ApplicantReferenceNumber = ApplictionRefernceNumber;



                        #region loading TestResult Document  and SMS hISTORY if exists in the db


                        gvTestRst.DataSource = null;
                        gvTestRst.DataBind();
                        List<TestResultModel> StudentTestResult = TestResultRepository.GetTestResult(ApplicantReferenceNumber).Result;
                        if (StudentTestResult.Count > 0)
                        {
                            PanelTestRslt.Visible = false;
                            // LbltestResult.Visible = true;


                            DDLTestResult.Visible = false;
                            FuploadTestReslt.Visible = false;
                            SendTestResultLinkButton.Visible = false;


                            gvTestRst.DataSource = StudentTestResult;
                            gvTestRst.DataBind();
                            List<SMSHistoryModel> SMSHistModl = SMSHistoryRepository.GetSMSHistory(ApplicantReferenceNumber, "Test").Result;
                            if (SMSHistModl.Count > 0)  // != null)
                            {
                                //can add further logic to check msgtype
                                //SendTestInvatationLinkButton.Enabled = false;
                            }

                        }

                        BindingUtility.LoadMessageHistory(TestResultSMSGridView, ApplicantReferenceNumber, "Test");

                        #endregion

                        #region loading InterviewResult AND SMS History if exists in the db
                        List<InterviewResultModel> StudentIntResult = InterviewResultRepository.GetInterviewResult(ApplicantReferenceNumber).Result;
                        if (StudentIntResult.Count > 0)
                        {
                            PanelInterviewrslt.Visible = false;
                            //LblInterviewresult.Visible = true;

                            DDLinterviewReslt.Visible = false;
                            SendInterviewResultLinkButton1.Visible = false;
                            GVinterviewResult.DataSource = StudentIntResult;
                            GVinterviewResult.DataBind();
                            List<SMSHistoryModel> SMSHistModl2 = SMSHistoryRepository.GetSMSHistory(ApplicantReferenceNumber, "Interview").Result;
                            if (SMSHistModl2.Count > 0)  // != null)
                            {
                                //can add further logic to check msgtype
                                //SendInterviewSMSLinkButton1.Enabled = false;
                            }

                        }

                        BindingUtility.LoadMessageHistory(InterviewSMSHistoryGridView, ApplicantReferenceNumber, "Interview");

                        #endregion




                        MOE_APPLICATION_DATA_Model mOE_APPLICATION_DATA_Model = MOE_APPLICATION_DATA_Repository.GetBy(ApplicantReferenceNumber).Result;
                        //Then View  All schools Document type for specific grade

                        BindingUtility.LoadMessageHistory(PayFessMessageHistoryGridView, ApplicantReferenceNumber, "null", PSPESConstants.SeatreservationFeesInvite);


                        BindingUtility.LoadMessageHistory(PayFessConfirmationMessageHistoryGridView, ApplicantReferenceNumber, "null", PSPESConstants.Seatreservationconfirmation);

                        if ((uint)CultureInfo.CurrentUICulture.LCID == 1025)
                        {
                            BindingUtility.LoadRequiredSupportingDocs(gvRequiredDocuments, ApplicantReferenceNumber, Convert.ToDecimal(TxtQID.Text), new SupportingDocsModel { SchoolCode = mOE_APPLICATION_DATA_Model.MOE_SCHOOL_CODE, Term = mOE_APPLICATION_DATA_Model.MOE_TERM.ToString(), Grade = mOE_APPLICATION_DATA_Model.MOE_APPLIED_GRADE, SeacrhByTermAndSchoolCodeAndGrade = true }, false, isMinistryUser, SPContext.Current.Site.Url);

                        }
                        else
                        {
                            BindingUtility.LoadRequiredSupportingDocs(gvRequiredDocuments, ApplicantReferenceNumber, Convert.ToDecimal(TxtQID.Text), new SupportingDocsModel { SchoolCode = mOE_APPLICATION_DATA_Model.MOE_SCHOOL_CODE, Term = mOE_APPLICATION_DATA_Model.MOE_TERM.ToString(), Grade = mOE_APPLICATION_DATA_Model.MOE_APPLIED_GRADE, SeacrhByTermAndSchoolCodeAndGrade = true }, true, isMinistryUser, SPContext.Current.Site.Url);


                        }

                        if (gvRequiredDocuments.Rows.Count > 0)
                        {
                            PanelDocuments.Visible = true;

                        }
                        else
                        {
                            LblSupportingDocs.Visible = true;
                            PanelDocuments.Visible = false;
                        }


                        //tarek.elbeheidy 07.03.2018    if Pay fees invite was sent before, default the fees text box to the fees amount


                        List<SMSHistoryModel> SMSMessageHistoryList = SMSHistoryRepository.GetSMSHistory(ApplicantReferenceNumber, "null", PSPESConstants.SeatreservationFeesInvite).Result;
                        if (SMSMessageHistoryList.Count > 0)
                        {
                            //Request.Form["PaidFeesTextBox"] = SMSMessageHistoryList.FirstOrDefault().Description;
                            PaidFeesTextBox.Text = SMSMessageHistoryList.Where(p => p.MsgStatus == "SENT").OrderByDescending(x => x.MsgTime).FirstOrDefault().Description;
                        }



                        //#region If exists, Load sms history into Test- Grid


                        //if ((SMSHistoryRepository.GetSMSHistory(ApplicantReferenceNumber, "Test")).Result.Count > 0)  // != null)
                        //{
                        //    //List<SMSHistoryModel> SMSHistModl = SMSHistoryRepository.GetSMSHistory(ApplicantReferenceNumber, "Test").Result;

                        //    //DataTable dt = new DataTable();

                        //    //dt.Columns.Add("Message Type", typeof(string));
                        //    //dt.Columns.Add("Message Text", typeof(string));
                        //    //dt.Columns.Add("Sent Time", typeof(string));
                        //    //dt.Columns.Add("Status", typeof(string));


                        //    ////foreach (schoolGrade sc in SchoolDataList)
                        //    //foreach (var item in SMSHistModl)
                        //    //{
                        //    //    DataRow dr = dt.NewRow();



                        //    //    dr["Message Type"] = item.MsgType;
                        //    //    dr["Message Text"] = item.MsgText;
                        //    //    dr["Sent Time"] = item.MsgTime;
                        //    //    dr["Status"] = item.MsgStatus;

                        //    //    dt.Rows.Add(dr);
                        //    //}
                        //    BindingUtility.LoadMessageHistory(TestResultSMSGridView, ApplicantReferenceNumber, "Test");

                        //}
                        //else
                        //{
                        //}

                        //#endregion

                        //#region If exists, Load sms history into Interview- Grid
                        //if ((SMSHistoryRepository.GetSMSHistory(ApplicantReferenceNumber, "Interview")).Result.Count > 0)  // != null)
                        //{
                        //    //    List<SMSHistoryModel> SMSHistModl = SMSHistoryRepository.GetSMSHistory(ApplicantReferenceNumber, "Interview").Result;

                        //    //    DataTable dt = new DataTable();

                        //    //    dt.Columns.Add("Message Type", typeof(string));
                        //    //    dt.Columns.Add("Message Text", typeof(string));
                        //    //    dt.Columns.Add("Sent Time", typeof(string));
                        //    //    dt.Columns.Add("Status", typeof(string));


                        //    //    //foreach (schoolGrade sc in SchoolDataList)
                        //    //    foreach (var item in SMSHistModl)
                        //    //    {
                        //    //        DataRow dr = dt.NewRow();



                        //    //        dr["Message Type"] = item.MsgType;
                        //    //        dr["Message Text"] = item.MsgText;
                        //    //        dr["Sent Time"] = item.MsgTime;
                        //    //        dr["Status"] = item.MsgStatus;

                        //    //        dt.Rows.Add(dr);
                        //    //    }
                        //    //    //GrdVInterview.DataSource = dt;
                        //    //    //GrdVInterview.DataBind();
                        //}
                        ////else
                        ////{
                        ////}

                        //#endregion

                        #region GetAdditional Information from MOE_biodata

                        MOE_BIO_DATA_Model BioDataMdl = MOE_BIO_DATA_Repository.GetBioDataByQID(TxtQID.Text).Result;


                        #endregion

                        //add arabic /english
                        if ((uint)CultureInfo.CurrentUICulture.LCID == 1025)
                        {
                            TxtNationality.Text = BioDataMdl.MOE_COUNTRY_ARABIC_NAME;
                            TxtStudentNm.Text = BioDataMdl.MOE_ARABIC_NAME;
                        }
                        else
                        {
                            TxtNationality.Text = BioDataMdl.MOE_COUNTRY_ENGLISH_NAME;
                            TxtStudentNm.Text = BioDataMdl.MOE_ENGLISH_NAME;
                        }

                        TxtGender.Text = BioDataMdl.MOE_GENDER;


                    }
                }*/


                //if ((uint)CultureInfo.CurrentUICulture.LCID == 1025)
                //{ CustomMessageBoxHelper.Show(this.Page, "لا توجد مقاعد متاحة لهذا الصف"); }
                //else { CustomMessageBoxHelper.Show(this.Page, "No Seats Available for this grade "); }

                //no seats avialble
            }
            //}
            //else
            //{
            //    CustomMessageBoxHelper.Show(this.Page, "Please Enter Application QID  ");
            //    return;
            //}
        }

        protected void SendTestInvatationLinkButton_Click(object sender, EventArgs e)
        {
            try
            {
                MOE_APPLICATION_DATA_Model appDTModel = MOE_APPLICATION_DATA_Repository.GetByRefAndID(TxtAppRef.Text, TxtQID.Text).Result;
                List<SeatCapacityModel> seatCapacityModel = SeatCapacityRepository.CheckExistsSeatCapacity(DateTime.Now.Year + 1, appDTModel.MOE_SCHOOL_CODE, appDTModel.MOE_APPLIED_GRADE).Result;
                int AvailableSeats = 0;
                if (seatCapacityModel.Count > 0)
                {
                    if (seatCapacityModel[0].MOE_SEAT_DISTRIBUTION > 0)
                    {
                        AvailableSeats = seatCapacityModel[0].MOE_SEAT_DISTRIBUTION;
                    }
                }

                if (AvailableSeats > 0)
                {
                    string TestDate = Request.Form["TestDateTextBox"].ToString();
                    string TestTime = Request.Form["TestTimeText"].ToString();

                    string NoAction = NoactionTestRadioHiddenField.Value;
                    string Invitaion = InviationTestaRadioHiddenField.Value;
                    string Rejection = RejecttionTestRadioHiddenField.Value;
                    string ReasonOfRejection = "";


                    #region get Guardian info: Mobile number

                    List<MOE_GUARDIAN_DATA_Model> Guardian = new List<MOE_GUARDIAN_DATA_Model>();
                    Guardian = MOE_GUARDIAN_DATA_Repository.GetStudentContactsInfo(TxtQID.Text).Result;
                    #endregion

                    MessageTemplate msgTmplt = new MessageTemplate();
                    string msgTitle = "";

                    string sendmessage = "no";


                    if (TestDate.Length > 0)
                    {
                        if (TestTime.Length > 0)
                        {

                            if (NoAction != "" || Invitaion != "" || Rejection != "")
                            {
                                ReasonOfRejection = Request.Form["TestRejectionReasonText"].ToString();

                                if (Rejection == "true" && (ReasonOfRejection == "" || ReasonOfRejection == null))
                                {
                                    if ((uint)CultureInfo.CurrentUICulture.LCID == 1025)
                                    { CustomMessageBoxHelper.Show(this.Page, "يرجى إدخال سبب الرفض"); }
                                    else { CustomMessageBoxHelper.Show(this.Page, "Please enter the rejection Reason "); }


                                }
                                else
                                {

                                    if (NoAction == "true")
                                    {
                                        TestRejectionReasonPanel.Visible = false;
                                        SendTestInvatationLinkButton.Enabled = false;
                                        //here noaction code
                                    }

                                    if (Invitaion == "true")
                                    {
                                        TestRejectionReasonPanel.Visible = false;
                                        SendTestInvatationLinkButton.Enabled = true;
                                        msgTitle = PSPESConstants.TestInvitation;
                                        msgTmplt = MessageTemplateRepository.Getby(msgTitle, 1).Result;
                                        sendmessage = "yes";
                                        //here inivtation code
                                    }
                                    if (Rejection == "true")
                                    {

                                        TestRejectionReasonPanel.Visible = true;
                                        SendTestInvatationLinkButton.Enabled = true;
                                        msgTitle = PSPESConstants.TestRejection;
                                        msgTmplt = MessageTemplateRepository.Getby(msgTitle, 2).Result;
                                        sendmessage = "yes";

                                    }


                                    if (sendmessage == "yes")
                                    {
                                        #region Create the message


                                        string smsMsg = "";


                                        if ((uint)CultureInfo.CurrentUICulture.LCID == 1025)
                                        {
                                            smsMsg = msgTmplt.ArabicMessage;
                                        }
                                        else { smsMsg = msgTmplt.EnglishMessage; }

                                        if (msgTitle == PSPESConstants.TestInvitation)
                                        {

                                            smsMsg = smsMsg.Replace("%StudentName% ", TxtStudentNm.Text);
                                            smsMsg = smsMsg.Replace("%StudentQID%", TxtQID.Text);
                                            smsMsg = smsMsg.Replace("%reference number%", TxtAppRef.Text);
                                            try
                                            {

                                                MOE_SCHOOL_Model schoolModel = MOE_SCHOOL_Repository.GetSchoolInfo((DateTime.Now.Year + 1).ToString(), TxtPreEnScl.Text, "false").Result;

                                                if ((uint)CultureInfo.CurrentUICulture.LCID == 1025)
                                                {
                                                    smsMsg = smsMsg.Replace("%school name%", schoolModel.MOE_SCHOOL_NAME_ARA);
                                                }
                                                else { smsMsg = smsMsg.Replace("%school name%", schoolModel.MOE_SCHOOL_NAME_ENG); }

                                            }
                                            catch (Exception ex)
                                            { }
                                            smsMsg = smsMsg.Replace("%Academic year% ", (appDTModel.MOE_TERM - 1).ToString() + "-" + appDTModel.MOE_TERM.ToString());
                                            smsMsg = smsMsg.Replace("%Grade%", TxtPreEnGrade.Text);
                                            smsMsg = smsMsg.Replace("%date%", TestDate);
                                            smsMsg = smsMsg.Replace("%time%", TestTime);
                                        }
                                        else if (msgTitle == PSPESConstants.TestRejection)
                                        {
                                            smsMsg = smsMsg.Replace("%StudentName% ", TxtStudentNm.Text);
                                            smsMsg = smsMsg.Replace("%StudentQID%", TxtQID.Text);
                                            smsMsg = smsMsg.Replace("%reference number%", TxtAppRef.Text);
                                            smsMsg = smsMsg.Replace("%Academic year% ", (appDTModel.MOE_TERM - 1).ToString() + "-" + appDTModel.MOE_TERM.ToString());
                                            smsMsg = smsMsg.Replace("%Grade%", TxtPreEnGrade.Text);
                                            // smsMsg = smsMsg.Replace("%parentName%", Guardian[0].MOE_ENGLISH_NAME);
                                            smsMsg = smsMsg.Replace("%Rejection Reason%", ReasonOfRejection);
                                            MOE_SCHOOL_Model schoolModel = MOE_SCHOOL_Repository.GetSchoolInfo((DateTime.Now.Year + 1).ToString(), TxtPreEnScl.Text, "false").Result;
                                            if ((uint)CultureInfo.CurrentUICulture.LCID == 1025)
                                            {
                                                smsMsg = smsMsg.Replace("%school name%", schoolModel.MOE_SCHOOL_NAME_ARA);
                                            }
                                            else { smsMsg = smsMsg.Replace("%school name%", schoolModel.MOE_SCHOOL_NAME_ENG); }

                                        }


                                        #endregion

                                        #region sending message 

                                        string smsStatus = SendSMS.SendMessage(msgTitle, Guardian[0].MOE_MOBILE_CONTACT_NBR, smsMsg);
                                        if (smsStatus == "1000")
                                        {
                                            SMSHistoryModel smsHist = new SMSHistoryModel()
                                            {
                                                MOE_APPL_REF_NBR = TxtAppRef.Text,
                                                MsgStatus = "SENT",
                                                MsgText = smsMsg,
                                                MsgTime = DateTime.Now.ToString(),
                                                MsgType = "Test",
                                                MsgTitle = msgTitle,
                                                MobileNumber = Guardian[0].MOE_MOBILE_CONTACT_NBR,
                                                MsgSender = SPContext.Current.Site.RootWeb.CurrentUser.Name


                                            };

                                            var insertSMSHistory = SMSHistoryRepository.Insert(smsHist).Result;
                                            // GrdVTest.DataBind();
                                            BindingUtility.LoadMessageHistory(TestResultSMSGridView, TxtAppRef.Text, "Test", Guardian[0].MOE_MOBILE_CONTACT_NBR, TxtQID.Text);
                                            if ((uint)CultureInfo.CurrentUICulture.LCID == 1033)
                                            { CustomMessageBoxHelper.Show(this.Page, "Message sent successfully"); }
                                            else { CustomMessageBoxHelper.Show(this.Page, "تم إرسال الرسالة بنجاح"); }

                                        }

                                        #endregion


                                    }


                                }


                            }
                            else
                            {

                                if ((uint)CultureInfo.CurrentUICulture.LCID == 1025)
                                { CustomMessageBoxHelper.Show(this.Page, "الرجاء تحديد الإجراء"); }
                                else { CustomMessageBoxHelper.Show(this.Page, "Please Select the action"); }


                            }
                        }
                        else
                        {

                            if ((uint)CultureInfo.CurrentUICulture.LCID == 1025)
                            { CustomMessageBoxHelper.Show(this.Page, "الرجاء تحديد وقت الاختبار"); }
                            else { CustomMessageBoxHelper.Show(this.Page, "Please Select the Test Time "); }



                        }

                    }
                    else
                    {

                        if ((uint)CultureInfo.CurrentUICulture.LCID == 1025)
                        { CustomMessageBoxHelper.Show(this.Page, "يرجى تحديد تاريخ الاختبار"); }
                        else { CustomMessageBoxHelper.Show(this.Page, "Please Select the Test Date "); }


                    }

                }
                else
                {

                    //if ((uint)CultureInfo.CurrentUICulture.LCID == 1025)
                    //{ CustomMessageBoxHelper.Show(this.Page, "لا توجد مقاعد متاحة لهذا الصف"); }
                    //else { CustomMessageBoxHelper.Show(this.Page, "No Seats Available for this grade "); }
                    if ((uint)CultureInfo.CurrentUICulture.LCID == 1025)
                    { CustomMessageBoxHelper.Show(this.Page, "قد وصلت إلى الحد الأقصى لقدرة المقاعد المخصصة للصف. لا يمكنك استكمال هذا الطلب ما لم تتم زيادة المقاعد في هذا الصف"); }
                    else { CustomMessageBoxHelper.Show(this.Page, "You have reached the maximum capacity of the seats that is allocated for the grade. You cannot finalize this application unless seats are increased in this grade"); }

                    //no seats avialble
                }
            }
            catch { }


        }

        protected void SendTestResultLinkButton_Click(object sender, EventArgs e)
        {
            // List<TestResultModel> TestResult = TestResultRepository.GetTestResult(TxtAppRef.Text).Result;


            // FileUpload RequiredFileUpload = (FileUpload)item.FindControl("fuRequiredDocument");
            // HiddenField DocumentTypeIDHiddenField = (HiddenField)item.FindControl("DocumentTypeIDHiddenField");
            try
            {
                MOE_APPLICATION_DATA_Model appDTModel = MOE_APPLICATION_DATA_Repository.GetByRefAndID(TxtAppRef.Text, TxtQID.Text).Result;
                List<SeatCapacityModel> seatCapacityModel = SeatCapacityRepository.CheckExistsSeatCapacity(DateTime.Now.Year + 1, appDTModel.MOE_SCHOOL_CODE, appDTModel.MOE_APPLIED_GRADE).Result;
                int AvailableSeats = 0;
                if (seatCapacityModel.Count > 0)
                {
                    if (seatCapacityModel[0].MOE_SEAT_DISTRIBUTION > 0)
                    {
                        AvailableSeats = seatCapacityModel[0].MOE_SEAT_DISTRIBUTION;
                    }
                }

                if (AvailableSeats > 0)
                {
                    if (DDLTestResult.SelectedItem.Text != "--Select--" && DDLTestResult.SelectedItem.Text != "--اختر--")
                    {

                        if (FuploadTestReslt.PostedFile != null)
                        {


                            if (FuploadTestReslt.PostedFile.ContentLength > 0)
                            {
                                System.IO.Stream strm = FuploadTestReslt.PostedFile.InputStream;

                                byte[] ApplicantByte = new byte[
                                        Convert.ToInt32(FuploadTestReslt.PostedFile.ContentLength)];

                                strm.Read(ApplicantByte, 0, Convert.ToInt32
                                                                   (FuploadTestReslt.PostedFile.ContentLength));
                                string ApplicantFileExtension = System.IO.Path.GetExtension(FuploadTestReslt.FileName);
                                string ApplicantReference = string.Format("{0}-{1}{2}", DateTime.Now.Year.ToString(), DateTime.Now.Minute, DateTime.Now.Second);

                                string FilePath = SaveToDocumentLibrary(ApplicantByte, ApplicantFileExtension, ApplicantReference);


                                var Task = TestResultRepository.Insert(new TestResultModel { MOE_APPL_NBR = TxtAppRef.Text, NATIONAL_ID = TxtQID.Text, ResultDocLocation = FilePath, TestResult1 = DDLTestResult.SelectedItem.Text, USERID = SPContext.Current.Site.RootWeb.CurrentUser.Name, DTTM = DateTime.Now }).Result;



                                #region get Guardian info: Mobile number

                                List<MOE_GUARDIAN_DATA_Model> Guardian = new List<MOE_GUARDIAN_DATA_Model>();
                                Guardian = MOE_GUARDIAN_DATA_Repository.GetStudentContactsInfo(TxtQID.Text).Result;
                                #endregion

                                MessageTemplate msgTmplt = new MessageTemplate();
                                string msgTitle = "";

                                string sendmessage = "no";

                                if (DDLTestResult.SelectedItem.Text == "Pass")
                                {

                                    msgTitle = PSPESConstants.TestResultpass;
                                    msgTmplt = MessageTemplateRepository.Getby(msgTitle, 3).Result;
                                    sendmessage = "yes";
                                }
                                else if (DDLTestResult.SelectedItem.Text == "ناجح")
                                {

                                    msgTitle = PSPESConstants.TestResultpass;
                                    msgTmplt = MessageTemplateRepository.Getby(msgTitle, 3).Result;
                                    sendmessage = "yes";
                                }
                                else if (DDLTestResult.SelectedItem.Text == "Fail")
                                {

                                    msgTitle = PSPESConstants.TestResultFail;
                                    msgTmplt = MessageTemplateRepository.Getby(msgTitle, 10).Result;
                                    sendmessage = "yes";
                                }
                                else if (DDLTestResult.SelectedItem.Text == "راسب")
                                {

                                    msgTitle = PSPESConstants.TestResultFail;
                                    msgTmplt = MessageTemplateRepository.Getby(msgTitle, 10).Result;
                                    sendmessage = "yes";
                                }

                                if (sendmessage == "yes")
                                {
                                    #region Create the message
                                    string smsMsg = "";
                                    if ((uint)CultureInfo.CurrentUICulture.LCID == 1025)
                                    {
                                        smsMsg = msgTmplt.ArabicMessage;
                                    }
                                    else { smsMsg = msgTmplt.EnglishMessage; }

                                    // string smsMsg = msgTmplt.EnglishMessage;

                                    smsMsg = smsMsg.Replace("%StudentName% ", TxtStudentNm.Text);
                                    smsMsg = smsMsg.Replace("%StudentQID%", TxtQID.Text);
                                    smsMsg = smsMsg.Replace("%reference number%", TxtAppRef.Text);

                                    smsMsg = smsMsg.Replace("%Grade%", TxtPreEnGrade.Text);
                                    // smsMsg = smsMsg.Replace("%parentName%", Guardian[0].MOE_ENGLISH_NAME);
                                    smsMsg = smsMsg.Replace("%Academic year% ", (appDTModel.MOE_TERM - 1).ToString() + "-" + appDTModel.MOE_TERM.ToString());

                                    MOE_SCHOOL_Model schoolModel = MOE_SCHOOL_Repository.GetSchoolInfo((DateTime.Now.Year + 1).ToString(), TxtPreEnScl.Text, "false").Result;

                                    if ((uint)CultureInfo.CurrentUICulture.LCID == 1025)
                                    {
                                        smsMsg = smsMsg.Replace("%school name%", schoolModel.MOE_SCHOOL_NAME_ARA);
                                    }
                                    else { smsMsg = smsMsg.Replace("%school name%", schoolModel.MOE_SCHOOL_NAME_ENG); }

                                    #endregion

                                    #region sending message 

                                    string smsStatus = SendSMS.SendMessage(msgTitle, Guardian[0].MOE_MOBILE_CONTACT_NBR, smsMsg);
                                    if (smsStatus == "1000")
                                    {
                                        SMSHistoryModel smsHist = new SMSHistoryModel()
                                        {
                                            MOE_APPL_REF_NBR = TxtAppRef.Text,
                                            MsgStatus = "SENT",
                                            MsgText = smsMsg,
                                            MsgTime = DateTime.Now.ToString(),
                                            MsgType = "Test",
                                            MsgTitle = msgTitle,
                                            MobileNumber = Guardian[0].MOE_MOBILE_CONTACT_NBR,
                                            MsgSender = SPContext.Current.Site.RootWeb.CurrentUser.Name


                                        };

                                        var insertSMSHistory = SMSHistoryRepository.Insert(smsHist).Result;
                                        // GrdVTest.DataBind();
                                        BindingUtility.LoadMessageHistory(TestResultSMSGridView, TxtAppRef.Text, "Test", Guardian[0].MOE_MOBILE_CONTACT_NBR, TxtQID.Text);
                                        if ((uint)CultureInfo.CurrentUICulture.LCID == 1033)
                                        { CustomMessageBoxHelper.Show(this.Page, "Message sent successfully"); }
                                        else { CustomMessageBoxHelper.Show(this.Page, "تم إرسال الرسالة بنجاح"); }

                                    }

                                    #endregion




                                }
                            }
                        }
                        else
                        {
                            if ((uint)CultureInfo.CurrentUICulture.LCID == 1025)
                            { CustomMessageBoxHelper.Show(this.Page, "يرجى تحميل مستند نتيجة الاختبار"); }
                            else { CustomMessageBoxHelper.Show(this.Page, "Please upload the Test Result document "); }


                            return;
                        }
                    }
                    else
                    {

                        if ((uint)CultureInfo.CurrentUICulture.LCID == 1025)
                        { CustomMessageBoxHelper.Show(this.Page, "يرجى تحديد نتيجة الاختبار"); }
                        else { CustomMessageBoxHelper.Show(this.Page, "Please select a Test Result "); }


                        return;
                    }
                }
                else
                {

                    //if ((uint)CultureInfo.CurrentUICulture.LCID == 1025)
                    //{ CustomMessageBoxHelper.Show(this.Page, "لا توجد مقاعد متاحة لهذا الصف"); }
                    //else { CustomMessageBoxHelper.Show(this.Page, "No Seats Available for this grade "); }
                    if ((uint)CultureInfo.CurrentUICulture.LCID == 1025)
                    { CustomMessageBoxHelper.Show(this.Page, "قد وصلت إلى الحد الأقصى لقدرة المقاعد المخصصة للصف. لا يمكنك استكمال هذا الطلب ما لم تتم زيادة المقاعد في هذا الصف"); }
                    else { CustomMessageBoxHelper.Show(this.Page, "You have reached the maximum capacity of the seats that is allocated for the grade. You cannot finalize this application unless seats are increased in this grade"); }

                    //no seats avialble
                }
            }


            catch { }

        }

        protected void ShowHideTestSMSHistoryLinkButton_Click(object sender, EventArgs e)
        {

            if (TestResultSMSGridView.Rows.Count > 0)
            {
                TestResultSMSGridView.Visible = !TestResultSMSGridView.Visible;
            }
            else
            {
                if ((uint)CultureInfo.CurrentUICulture.LCID == 1025)
                { CustomMessageBoxHelper.Show(this.Page, "لم يتم إرسال أي رسالة من قبل "); }
                else { CustomMessageBoxHelper.Show(this.Page, "No Message Sent Before "); }


            }

            if (TestResultSMSGridView.Visible)
            {
                if ((uint)CultureInfo.CurrentUICulture.LCID == 1025)
                { ShowHideTestSMSHistoryLinkButton.Text = "إخفاء تاريخ الرسائل القصيرة"; }
                else { ShowHideTestSMSHistoryLinkButton.Text = "Hide SMS History"; }

            }
            else
            {
                if ((uint)CultureInfo.CurrentUICulture.LCID == 1025)
                { ShowHideTestSMSHistoryLinkButton.Text = "تظهر تاريخ الرسائل القصيرة"; }
                else { ShowHideTestSMSHistoryLinkButton.Text = "Show SMS History"; }


            }
        }




        protected void SendInterviewSMSLinkButton1_Click(object sender, EventArgs e)
        {
            try
            {
                MOE_APPLICATION_DATA_Model appDTModel = MOE_APPLICATION_DATA_Repository.GetByRefAndID(TxtAppRef.Text, TxtQID.Text).Result;
                List<SeatCapacityModel> seatCapacityModel = SeatCapacityRepository.CheckExistsSeatCapacity(DateTime.Now.Year + 1, appDTModel.MOE_SCHOOL_CODE, appDTModel.MOE_APPLIED_GRADE).Result;
                int AvailableSeats = 0;
                if (seatCapacityModel.Count > 0)
                {
                    if (seatCapacityModel[0].MOE_SEAT_DISTRIBUTION > 0)
                    {
                        AvailableSeats = seatCapacityModel[0].MOE_SEAT_DISTRIBUTION;
                    }
                }

                if (AvailableSeats > 0)
                {
                    string IntDate = Request.Form["InterviewDateText"].ToString();
                    string IntTime = Request.Form["InterviewTimeText"].ToString();

                    string NoAction = InterviewNoactionRadioHiddenField.Value;
                    string Invitaion = InterviewInvitationRadioHiddenField.Value;
                    string Rejection = InterviewRejectionRadioHiddenField.Value;


                    #region get Guardian info: Mobile number

                    List<MOE_GUARDIAN_DATA_Model> Guardian = new List<MOE_GUARDIAN_DATA_Model>();
                    Guardian = MOE_GUARDIAN_DATA_Repository.GetStudentContactsInfo(TxtQID.Text).Result;
                    #endregion

                    MessageTemplate msgTmplt = new MessageTemplate();
                    string msgTitle = "";
                    string ReasonOfRejection = "";




                    if (IntDate.Length > 0)
                    {
                        if (IntTime.Length > 0)
                        {



                            if (NoAction != "" || Invitaion != "" || Rejection != "")
                            {

                                ReasonOfRejection = Request.Form["InterviewRejectionReasonText"].ToString();
                                if (Rejection == "true" && (ReasonOfRejection == null || ReasonOfRejection == ""))
                                {
                                    if ((uint)CultureInfo.CurrentUICulture.LCID == 1025)
                                    { CustomMessageBoxHelper.Show(this.Page, "يرجى إدخال سبب الرفض"); }
                                    else { CustomMessageBoxHelper.Show(this.Page, "Please enter the Rejection Reason "); }


                                }
                                else
                                {

                                    if (NoAction == "true")
                                    {
                                        InterviewRejectionPanel.Visible = false;
                                        SendInterviewSMSLinkButton1.Enabled = false;
                                        //here noaction code
                                    }

                                    if (Invitaion == "true")
                                    {
                                        InterviewRejectionPanel.Visible = false;
                                        SendInterviewSMSLinkButton1.Enabled = true;
                                        msgTitle = PSPESConstants.InterviewInvitation;
                                        msgTmplt = MessageTemplateRepository.Getby(msgTitle, 4).Result;
                                        //here inivtation code
                                    }
                                    if (Rejection == "true")
                                    {
                                        InterviewRejectionPanel.Visible = true;
                                        SendInterviewSMSLinkButton1.Enabled = true;

                                        msgTitle = PSPESConstants.InterviewRejection;
                                        msgTmplt = MessageTemplateRepository.Getby(msgTitle, 5).Result;
                                        //here rejection ciode
                                    }


                                    string smsMsg = "";
                                    #region Create the message

                                    if ((uint)CultureInfo.CurrentUICulture.LCID == 1025)
                                    {
                                        smsMsg = msgTmplt.ArabicMessage;
                                    }
                                    else { smsMsg = msgTmplt.EnglishMessage; }




                                    if (msgTitle == PSPESConstants.InterviewInvitation)
                                    {

                                        smsMsg = smsMsg.Replace("%StudentName% ", TxtStudentNm.Text);
                                        smsMsg = smsMsg.Replace("%StudentQID%", TxtQID.Text);
                                        smsMsg = smsMsg.Replace("%reference number%", TxtAppRef.Text);
                                        smsMsg = smsMsg.Replace("%Academic year% ", (appDTModel.MOE_TERM - 1).ToString() + "-" + appDTModel.MOE_TERM.ToString());
                                        smsMsg = smsMsg.Replace("%Grade%", TxtPreEnGrade.Text);
                                        smsMsg = smsMsg.Replace("%date%", IntDate);
                                        smsMsg = smsMsg.Replace("%time%", IntTime);
                                        MOE_SCHOOL_Model schoolModel = MOE_SCHOOL_Repository.GetSchoolInfo((DateTime.Now.Year + 1).ToString(), TxtPreEnScl.Text, "false").Result;

                                        if ((uint)CultureInfo.CurrentUICulture.LCID == 1025)
                                        {
                                            smsMsg = smsMsg.Replace("%school name%", schoolModel.MOE_SCHOOL_NAME_ARA);
                                        }
                                        else { smsMsg = smsMsg.Replace("%school name%", schoolModel.MOE_SCHOOL_NAME_ENG); }
                                    }
                                    else
                                    {
                                        smsMsg = smsMsg.Replace("%StudentName% ", TxtStudentNm.Text);
                                        smsMsg = smsMsg.Replace("%StudentQID%", TxtQID.Text);
                                        smsMsg = smsMsg.Replace("%reference number%", TxtAppRef.Text);
                                        smsMsg = smsMsg.Replace("%Academic year% ", (appDTModel.MOE_TERM - 1).ToString() + "-" + appDTModel.MOE_TERM.ToString());
                                        smsMsg = smsMsg.Replace("%Grade%", TxtPreEnGrade.Text);
                                        // smsMsg = smsMsg.Replace("%parentName%", Guardian[0].MOE_ENGLISH_NAME);
                                        smsMsg = smsMsg.Replace("%Rejection Reason%", ReasonOfRejection);
                                        MOE_SCHOOL_Model schoolModel = MOE_SCHOOL_Repository.GetSchoolInfo((DateTime.Now.Year + 1).ToString(), TxtPreEnScl.Text, "false").Result;

                                        if ((uint)CultureInfo.CurrentUICulture.LCID == 1025)
                                        {
                                            smsMsg = smsMsg.Replace("%school name%", schoolModel.MOE_SCHOOL_NAME_ARA);
                                        }
                                        else { smsMsg = smsMsg.Replace("%school name%", schoolModel.MOE_SCHOOL_NAME_ENG); }

                                    }
                                    #endregion

                                    #region sending message 

                                    string smsStatus = SendSMS.SendMessage(msgTitle, Guardian[0].MOE_MOBILE_CONTACT_NBR, smsMsg);
                                    if (smsStatus == "1000")
                                    {
                                        SMSHistoryModel smsHist = new SMSHistoryModel()
                                        {
                                            MOE_APPL_REF_NBR = TxtAppRef.Text,
                                            MsgStatus = "SENT",
                                            MsgText = smsMsg,
                                            MsgTime = DateTime.Now.ToString(),
                                            MsgType = "Interview",
                                            MsgTitle = msgTitle,
                                            MobileNumber = Guardian[0].MOE_MOBILE_CONTACT_NBR,
                                            MsgSender = SPContext.Current.Site.RootWeb.CurrentUser.Name


                                        };

                                        var insertSMSHistory = SMSHistoryRepository.Insert(smsHist).Result;
                                        // GrdVTest.DataBind();
                                        BindingUtility.LoadMessageHistory(InterviewSMSHistoryGridView, TxtAppRef.Text, "Interview", Guardian[0].MOE_MOBILE_CONTACT_NBR, TxtQID.Text);
                                        if ((uint)CultureInfo.CurrentUICulture.LCID == 1033)
                                        { CustomMessageBoxHelper.Show(this.Page, "Message sent successfully"); }
                                        else { CustomMessageBoxHelper.Show(this.Page, "تم إرسال الرسالة بنجاح"); }


                                    }

                                    #endregion


                                }



                            }
                            else
                            {

                                if ((uint)CultureInfo.CurrentUICulture.LCID == 1025)
                                { CustomMessageBoxHelper.Show(this.Page, "الرجاء تحديد الإجراء"); }
                                else { CustomMessageBoxHelper.Show(this.Page, "Please Select the action"); }


                            }
                        }
                        else
                        {

                            if ((uint)CultureInfo.CurrentUICulture.LCID == 1025)
                            { CustomMessageBoxHelper.Show(this.Page, "الرجاء تحديد الوقت"); }
                            else { CustomMessageBoxHelper.Show(this.Page, "Please Select the Time"); }


                        }

                    }
                    else
                    {

                        if ((uint)CultureInfo.CurrentUICulture.LCID == 1025)
                        {
                            CustomMessageBoxHelper.Show(this.Page, "الرجاء تحديد التاريخ");
                        }
                        else
                        {
                            CustomMessageBoxHelper.Show(this.Page, "Please Select the Date");
                        }

                    }
                }
                else
                {

                    //if ((uint)CultureInfo.CurrentUICulture.LCID == 1025)
                    //{ CustomMessageBoxHelper.Show(this.Page, "لا توجد مقاعد متاحة لهذا الصف"); }
                    //else { CustomMessageBoxHelper.Show(this.Page, "No Seats Available for this grade "); }
                    if ((uint)CultureInfo.CurrentUICulture.LCID == 1025)
                    { CustomMessageBoxHelper.Show(this.Page, "قد وصلت إلى الحد الأقصى لقدرة المقاعد المخصصة للصف. لا يمكنك استكمال هذا الطلب ما لم تتم زيادة المقاعد في هذا الصف"); }
                    else { CustomMessageBoxHelper.Show(this.Page, "You have reached the maximum capacity of the seats that is allocated for the grade. You cannot finalize this application unless seats are increased in this grade"); }

                    //no seats avialble
                }

            }
            catch { }
        }

        protected void SendInterviewResultLinkButton1_Click(object sender, EventArgs e)
        {
            try
            {
                MOE_APPLICATION_DATA_Model appDTModel = MOE_APPLICATION_DATA_Repository.GetByRefAndID(TxtAppRef.Text, TxtQID.Text).Result;
                List<SeatCapacityModel> seatCapacityModel = SeatCapacityRepository.CheckExistsSeatCapacity(DateTime.Now.Year + 1, appDTModel.MOE_SCHOOL_CODE, appDTModel.MOE_APPLIED_GRADE).Result;
                int AvailableSeats = 0;
                if (seatCapacityModel.Count > 0)
                {
                    if (seatCapacityModel[0].MOE_SEAT_DISTRIBUTION > 0)
                    {
                        AvailableSeats = seatCapacityModel[0].MOE_SEAT_DISTRIBUTION;
                    }
                }

                if (AvailableSeats > 0)
                {
                    if (DDLinterviewReslt.SelectedItem.Text == "--Select--" || DDLinterviewReslt.SelectedItem.Text == "--اختر--")
                    {
                        if ((uint)CultureInfo.CurrentUICulture.LCID == 1025)
                        {
                            CustomMessageBoxHelper.Show(this.Page, "الرجاء تحديد نتيجة المقابلة");
                        }
                        else
                        {
                            CustomMessageBoxHelper.Show(this.Page, "Please Select the Interview result ");
                        }


                        return;
                    }
                    else
                    {

                        var Task = InterviewResultRepository.Insert(new InterviewResultModel { MOE_APPL_NBR = TxtAppRef.Text, NATIONAL_ID = TxtQID.Text, InterviewResult1 = DDLinterviewReslt.SelectedItem.Text, USERID = SPContext.Current.Site.RootWeb.CurrentUser.Name, DTTM = DateTime.Now }).Result;


                        #region get Guardian info: Mobile number

                        List<MOE_GUARDIAN_DATA_Model> Guardian = new List<MOE_GUARDIAN_DATA_Model>();
                        Guardian = MOE_GUARDIAN_DATA_Repository.GetStudentContactsInfo(TxtQID.Text).Result;
                        #endregion

                        MessageTemplate msgTmplt = new MessageTemplate();
                        string msgTitle = "";

                        string sendmessage = "no";

                        if (DDLinterviewReslt.SelectedItem.Text == "Pass")
                        {

                            msgTitle = PSPESConstants.InterviewResultpass;
                            msgTmplt = MessageTemplateRepository.Getby(msgTitle, 6).Result;
                            sendmessage = "yes";
                        }
                        else if (DDLTestResult.SelectedItem.Text == "ناجح")
                        {

                            msgTitle = PSPESConstants.InterviewResultpass;
                            msgTmplt = MessageTemplateRepository.Getby(msgTitle, 6).Result;
                            sendmessage = "yes";
                        }
                        else if (DDLTestResult.SelectedItem.Text == "Fail")
                        {

                            msgTitle = PSPESConstants.InterviewResultFailed;
                            msgTmplt = MessageTemplateRepository.Getby(msgTitle, 7).Result;
                            sendmessage = "yes";
                        }
                        else if (DDLTestResult.SelectedItem.Text == "راسب")
                        {

                            msgTitle = PSPESConstants.InterviewResultFailed;
                            msgTmplt = MessageTemplateRepository.Getby(msgTitle, 7).Result;
                            sendmessage = "yes";
                        }

                        if (sendmessage == "yes")
                        {
                            #region Create the message


                            string smsMsg = msgTmplt.EnglishMessage;

                            smsMsg = smsMsg.Replace("%StudentName% ", TxtStudentNm.Text);
                            smsMsg = smsMsg.Replace("%StudentQID%", TxtQID.Text);
                            smsMsg = smsMsg.Replace("%reference number%", TxtAppRef.Text);
                            smsMsg = smsMsg.Replace("%Academic year% ", (appDTModel.MOE_TERM - 1).ToString() + "-" + appDTModel.MOE_TERM.ToString());
                            smsMsg = smsMsg.Replace("%Grade%", TxtPreEnGrade.Text);
                            MOE_SCHOOL_Model schoolModel = MOE_SCHOOL_Repository.GetSchoolInfo((DateTime.Now.Year + 1).ToString(), TxtPreEnScl.Text, "false").Result;

                            if ((uint)CultureInfo.CurrentUICulture.LCID == 1025)
                            {
                                smsMsg = smsMsg.Replace("%school name%", schoolModel.MOE_SCHOOL_NAME_ARA);
                            }
                            else { smsMsg = smsMsg.Replace("%school name%", schoolModel.MOE_SCHOOL_NAME_ENG); }
                            // smsMsg = smsMsg.Replace("%parentName%", Guardian[0].MOE_ENGLISH_NAME);
                            // smsMsg = smsMsg.Replace("%time%", TestTime);
                            #endregion

                            #region sending message 

                            string smsStatus = SendSMS.SendMessage(msgTitle, Guardian[0].MOE_MOBILE_CONTACT_NBR, smsMsg);
                            if (smsStatus == "1000")
                            {
                                SMSHistoryModel smsHist = new SMSHistoryModel()
                                {
                                    MOE_APPL_REF_NBR = TxtAppRef.Text,
                                    MsgStatus = "SENT",
                                    MsgText = smsMsg,
                                    MsgTime = DateTime.Now.ToString(),
                                    MsgType = "Interview",
                                    MsgTitle = msgTitle,
                                    MobileNumber = Guardian[0].MOE_MOBILE_CONTACT_NBR,
                                    MsgSender = SPContext.Current.Site.RootWeb.CurrentUser.Name


                                };

                                var insertSMSHistory = SMSHistoryRepository.Insert(smsHist).Result;
                                // GrdVTest.DataBind();
                                BindingUtility.LoadMessageHistory(InterviewSMSHistoryGridView, TxtAppRef.Text, "Interview", Guardian[0].MOE_MOBILE_CONTACT_NBR, TxtQID.Text);
                                if ((uint)CultureInfo.CurrentUICulture.LCID == 1033)
                                { CustomMessageBoxHelper.Show(this.Page, "Message sent successfully"); }
                                else { CustomMessageBoxHelper.Show(this.Page, "تم إرسال الرسالة بنجاح"); }
                            }

                            #endregion


                        }




                    }
                }
                else
                {

                    //if ((uint)CultureInfo.CurrentUICulture.LCID == 1025)
                    //{ CustomMessageBoxHelper.Show(this.Page, "لا توجد مقاعد متاحة لهذا الصف"); }
                    //else { CustomMessageBoxHelper.Show(this.Page, "No Seats Available for this grade "); }
                    if ((uint)CultureInfo.CurrentUICulture.LCID == 1025)
                    { CustomMessageBoxHelper.Show(this.Page, "قد وصلت إلى الحد الأقصى لقدرة المقاعد المخصصة للصف. لا يمكنك استكمال هذا الطلب ما لم تتم زيادة المقاعد في هذا الصف"); }
                    else { CustomMessageBoxHelper.Show(this.Page, "You have reached the maximum capacity of the seats that is allocated for the grade. You cannot finalize this application unless seats are increased in this grade"); }

                    //no seats avialble
                }
            }
            catch { }


        }

        protected void ShowHideInterviewSMSHistoryLinkButton_Click(object sender, EventArgs e)
        {
            if (InterviewSMSHistoryGridView.Rows.Count > 0)
            {
                InterviewSMSHistoryGridView.Visible = !InterviewSMSHistoryGridView.Visible;
            }
            else
            {
                if ((uint)CultureInfo.CurrentUICulture.LCID == 1025)
                { CustomMessageBoxHelper.Show(this.Page, "لم يتم إرسال أي رسالة من قبل "); }
                else { CustomMessageBoxHelper.Show(this.Page, "No Message Sent Before "); }

            }

            if (InterviewSMSHistoryGridView.Visible)
            {

                if ((uint)CultureInfo.CurrentUICulture.LCID == 1025)
                { ShowHideInterviewSMSHistoryLinkButton.Text = "إخفاء تاريخ الرسائل القصيرة"; }
                else { ShowHideInterviewSMSHistoryLinkButton.Text = "Hide SMS History"; }
            }
            else
            {
                if ((uint)CultureInfo.CurrentUICulture.LCID == 1025)
                { ShowHideInterviewSMSHistoryLinkButton.Text = "تظهر تاريخ الرسائل القصيرة"; }
                else { ShowHideInterviewSMSHistoryLinkButton.Text = "Show SMS History"; }


            }
        }

        protected void EditApplicationLinkButton_Click(object sender, EventArgs e)
        {
            RepeaterItem item = (sender as LinkButton).Parent as RepeaterItem;
            Label ApplicationRefNumberLabel = (Label)item.FindControl("ApplicationRefNoLabel");
            Label QIDLabel = (Label)item.FindControl("QIDLabel");

            try
            {
                string serviceURL = ConfigurationManager.AppSettings["AddApplicationPageLink"];
                CustomMessageBoxHelper.Show(this.Page, string.Format("Please Redirect This to Tareq page with {0}", ApplicationRefNumberLabel.Text));
                string AddApplicationPageLink = string.Format("{0}?ApplicationRefNumber={1}&&QID={2}", serviceURL, ApplicationRefNumberLabel.Text, QIDLabel.Text);
                Response.Redirect(AddApplicationPageLink);
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
                EditClicked(ApplicationRefNumberLabel.Text, QIDLabel.Text);
                //  string URL = string.Format("{0}?ApplicationRefNumber={1}","", ApplicationRefNumberLabel.Text);
                //Response.Redirect("");
                //
            }
            catch { }

        }
    }
}
