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

namespace MOEHE.PSPES.Webparts.CompletionScreen.CompletionProcess
{
    public partial class CompletionProcessUserControl : UserControl
    {
        string ApplicantReferenceNumber = "2019-30120-10-1";
        decimal NationalID = 3323232323;
        public string QID = "";
        public string StudentName = "";
        public string PreviousSchools = "";
        void FillTestResultDDL()
        {

            ListItem ItemTemp1 = new ListItem();
            ItemTemp1.Value = "0";
            // if ((uint)CultureInfo.CurrentUICulture.LCID == 1025)
            //{ 
            //ItemTemp1.Text = "--اختر--"; }
            //else {
            ItemTemp1.Text = "--Select--"; //}

            // ItemTemp1.Selected = true;
            DDLTestResult.Items.Add(ItemTemp1);

            ListItem ItemTemp2 = new ListItem();
            ItemTemp2.Value = "1";
            //if ((uint)CultureInfo.CurrentUICulture.LCID == 1025)
            //{
            // ItemTemp2.Text = "موظف الوزارة"; }
            // else {
            ItemTemp2.Text = "Pass"; //}


            DDLTestResult.Items.Add(ItemTemp2);

            ListItem ItemTemp3 = new ListItem();
            ItemTemp3.Value = "2";
            //if ((uint)CultureInfo.CurrentUICulture.LCID == 1025)
            //{
            // ItemTemp3.Text = "موظف المدرسة"; }
            // else { 
            ItemTemp3.Text = "Fail"; //}

            DDLTestResult.Items.Add(ItemTemp3);


        }
        protected void Page_Load(object sender, EventArgs e)
        {

            if (!Page.IsPostBack)
            {

                FillTestResultDDL();
               // FillInterviewResultDDL();


            }

            bool isMinistryUser = true;
            //string Date= PayByDateTime.Value;
            // bool PayFeesRE = PayFeesNoActionRadio.Checked;
            MOE_APPLICATION_DATA_Model mOE_APPLICATION_DATA_Model = MOE_APPLICATION_DATA_Repository.GetBy(ApplicantReferenceNumber).Result;
            //Then View  All schools Document type for specific grade


         
            BindingUtility.LoadMessageHistory(PayFessMessageHistoryGridView, ApplicantReferenceNumber, "", PSPESConstants.SeatreservationFeesInvite);


            BindingUtility.LoadMessageHistory(TestResultSMSGridView, ApplicantReferenceNumber, "Test");
            BindingUtility.LoadMessageHistory(InterviewSMSHistoryGridView, ApplicantReferenceNumber, "Interview");


            BindingUtility.LoadMessageHistory(PayFessConfirmationMessageHistoryGridView, ApplicantReferenceNumber, "", PSPESConstants.Seatreservationconfirmation);

            if ((uint)CultureInfo.CurrentUICulture.LCID == 1025)
            {
                BindingUtility.LoadRequiredSupportingDocs(gvRequiredDocuments, ApplicantReferenceNumber, NationalID, new SupportingDocsModel { SchoolCode = mOE_APPLICATION_DATA_Model.MOE_SCHOOL_CODE, Term = mOE_APPLICATION_DATA_Model.MOE_TERM.ToString(), Grade = mOE_APPLICATION_DATA_Model.MOE_APPLIED_GRADE, SeacrhByTermAndSchoolCodeAndGrade = true }, false, isMinistryUser, SPContext.Current.Site.Url);

            }
            else
            {
                BindingUtility.LoadRequiredSupportingDocs(gvRequiredDocuments, ApplicantReferenceNumber, NationalID, new SupportingDocsModel { SchoolCode = mOE_APPLICATION_DATA_Model.MOE_SCHOOL_CODE, Term = mOE_APPLICATION_DATA_Model.MOE_TERM.ToString(), Grade = mOE_APPLICATION_DATA_Model.MOE_APPLIED_GRADE, SeacrhByTermAndSchoolCodeAndGrade = true }, true, isMinistryUser, SPContext.Current.Site.Url);


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
            MOE_APPLICATION_DATA_Model mOE_APPLICATION_DATA_Model = MOE_APPLICATION_DATA_Repository.GetBy(ApplicantReferenceNumber).Result;

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


                        var Task = MOE_APPL_SUPPORT_DOCS_Repository.Insert(new MOE_APPL_SUPPORT_DOCS_Model_Not_Serializable { DeactiveOtherApplications = false, MOE_APPL_NBR = ApplicantReferenceNumber, MOE_DOCUMENT_LOCATION = FilePath, MOE_DOCUMENT_TYPE_ID = DocumentTypeIDHiddenField.Value, MOE_TRANSACTION_DTTM = DateTime.Now, MOE_USERID = SPContext.Current.Site.RootWeb.CurrentUser.LoginName, NATIONAL_ID = NationalID }).Result;


                    }
                }
            }
        }

        public string GetQID(string ApplicantReferenceNumber)
        {
            string StudentQID = "";
           StudentQID = MOE_APPLICATION_DATA_Repository.GetBy(ApplicantReferenceNumber).Result.NATIONAL_ID.ToString();
            return StudentQID;
        }
        protected void FeesPaidConfirmationLinkButton_Click(object sender, EventArgs e)
        {

            string PaidReservationFeesText = Request.Form["PaidFeesTextBox"].ToString();
            string PayFeesOnDateTime = Request.Form["PaidFeesDateTime"].ToString();
            MessageTemplate messageTemplate = new MessageTemplate();
            #region get Guardian info: Mobile number
            MOE_APPLICATION_DATA_Model appDTModel = MOE_APPLICATION_DATA_Repository.GetBy(ApplicantReferenceNumber).Result;

            List<SeatCapacityModel> seatCapacityModel = SeatCapacityRepository.CheckExistsSeatCapacity(DateTime.Now.Year + 1, appDTModel.MOE_SCHOOL_CODE, appDTModel.MOE_APPLIED_GRADE).Result;
            int AvailableSeats = 0;
            if (seatCapacityModel.Count > 0)
            {
                if (seatCapacityModel[0].MOE_PREENROL_SEATS > 0)
                {
                    AvailableSeats = seatCapacityModel[0].MOE_PREENROL_SEATS;
                }
            }
            List<SeatReservationFee> CheckIFReservationDone = SeatReservationFeeRepository.Getby(ApplicantReferenceNumber).Result;

            if (CheckIFReservationDone.Count <= 0)
            {

                if (AvailableSeats > 0)
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

                            var InsretSeatReservationTaskResult = SeatReservationFeeRepository.Insert(new SeatReservationFeeModel { ApplicantReferenceNumber = ApplicantReferenceNumber, CreatedBy = SPContext.Current.Site.RootWeb.CurrentUser.LoginName, CreatedDate = DateTime.Now, FeesPaidDate = PayFeesOnDateTimeParsed, NationalID = NationalID, ReservationFeesAmount = long.Parse(PaidReservationFeesText) }).Result;
                            string messageTitle = PSPESConstants.Seatreservationconfirmation;
                            messageTemplate = MessageTemplateRepository.Getby(messageTitle, 9).Result;
                            #region Create the message
                            string smsMessage = messageTemplate.EnglishMessage;
                            string SchoolName = MOE_SCHOOL_Repository.GetSchoolInfo(DateTime.Now.Year.ToString(), appDTModel.MOE_SCHOOL_CODE, "false").Result.MOE_SCHOOL_NAME_ARA;
                            smsMessage = smsMessage.Replace("%parentName%", mOE_GUARDIAN_DATA_Model.bio_data.MOE_ARABIC_NAME);
                            smsMessage = smsMessage.Replace("%StudentName%", mOE_GUARDIAN_DATA_Model.MOE_ARABIC_NAME);

                            smsMessage = smsMessage.Replace("%StudentQID%", appDTModel.NATIONAL_ID.ToString());
                            smsMessage = smsMessage.Replace("%reference number%", appDTModel.MOE_APPL_REF_NBR);
                            //smsMsg = smsMsg.Replace("%school name%", TxtAppRef.Text);
                            smsMessage = smsMessage.Replace("%Grade%", appDTModel.MOE_APPLIED_GRADE);
                            smsMessage = smsMessage.Replace("%xxxx%", PaidReservationFeesText);
                            smsMessage = smsMessage.Replace("%Academic year%", appDTModel.MOE_APPL_YEAR.ToString());
                            smsMessage = smsMessage.Replace("%School Name%", SchoolName);

                            #endregion
                            string smsStatus = SendSMS.SendMessage(messageTitle, mOE_GUARDIAN_DATA_Model.MOE_MOBILE_CONTACT_NBR, smsMessage);
                            smsStatus = SendSMS.SendMessage(messageTitle, "55531606", smsMessage);

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
                                //update seats
                                DBOperationResult dBOperationResult = SeatCapacityRepository.Insert(new SeatCapacityModel
                                {
                                    MOE_SCHOOL_CODE = seatCapacityModel[0].MOE_SCHOOL_CODE,
                                    ID = seatCapacityModel[0].ID,
                                    MOE_PREENROL_SEATS = AvailableSeats - 1,
                                    MOE_SCHOOL_GRADE = seatCapacityModel[0].MOE_SCHOOL_GRADE,
                                    MOE_SEAT_DISTRIBUTION = seatCapacityModel[0].MOE_SEAT_DISTRIBUTION
                                         ,
                                    MOE_TERM = seatCapacityModel[0].MOE_TERM,
                                    MOE_TRANSACTION_DTMM = DateTime.Now,
                                    MOE_USERID = SPContext.Current.Site.RootWeb.CurrentUser.LoginName
                                }).Result;



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
                                MOE_APPL_REF_NBR = ApplicantReferenceNumber,
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
                        CustomMessageBoxHelper.Show(this.Page, "Please enter the amount");

                    }

                }
                else
                {
                    CustomMessageBoxHelper.Show(this.Page, "No Seats Available for this grade");
                    //no seats avialble
                }
            }
            else
            {
                CustomMessageBoxHelper.Show(this.Page, "This Applcation Already Paid");

            }
        }

        protected void PayFeesSMSLinkButton_Click(object sender, EventArgs e)
        {
            //we need to check also if he already pay the fees
            if (PayFeesRequestRadioHiddenField.Value == "true")
            {
                #region get Guardian info: Mobile number
                MOE_APPLICATION_DATA_Model appDTModel = MOE_APPLICATION_DATA_Repository.GetBy(ApplicantReferenceNumber).Result;

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
                    if (seatCapacityModel[0].MOE_PREENROL_SEATS > 0)
                    {
                        AvailableSeats = seatCapacityModel[0].MOE_PREENROL_SEATS;
                    }
                }
                List<SeatReservationFee> CheckIFReservationDone = SeatReservationFeeRepository.Getby(ApplicantReferenceNumber).Result;

                if (CheckIFReservationDone.Count <= 0)
                {
                    if (AvailableSeats > 0)
                    {


                        if (ReservationFeesTextBoxForSMS.Length > 0)
                        {
                            if (PayFeesOnDateTime.Length > 0)
                            {


                                string messageTitle = PSPESConstants.SeatreservationFeesInvite;
                                messageTemplate = MessageTemplateRepository.Getby(messageTitle, 8).Result;
                                #region Create the message
                                string smsMessage = messageTemplate.EnglishMessage;

                                smsMessage = smsMessage.Replace("%parentName%", mOE_GUARDIAN_DATA_Model.bio_data.MOE_ENGLISH_NAME);
                                smsMessage = smsMessage.Replace("%StudentName%", mOE_GUARDIAN_DATA_Model.MOE_ARABIC_NAME);

                                smsMessage = smsMessage.Replace("%StudentQID%", appDTModel.NATIONAL_ID.ToString());
                                smsMessage = smsMessage.Replace("%reference number%", appDTModel.MOE_APPL_REF_NBR);
                                //smsMsg = smsMsg.Replace("%school name%", TxtAppRef.Text);
                                smsMessage = smsMessage.Replace("%Grade%", appDTModel.MOE_APPLIED_GRADE);
                                smsMessage = smsMessage.Replace("%xxxx%", ReservationFeesTextBoxForSMS);
                                smsMessage = smsMessage.Replace("%Paybydate%", PayFeesOnDateTime);
                                #endregion

                                string smsStatus = SendSMS.SendMessage(messageTitle, "55531606", smsMessage);

                                smsStatus = SendSMS.SendMessage(messageTitle, mOE_GUARDIAN_DATA_Model.MOE_MOBILE_CONTACT_NBR, smsMessage);
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

                                }
                            }
                            else
                            {
                                CustomMessageBoxHelper.Show(this.Page, "Please enter the date");

                            }
                        }
                        else
                        {
                            CustomMessageBoxHelper.Show(this.Page, "Please enter the amount");

                        }
                    }
                    else
                    {
                        CustomMessageBoxHelper.Show(this.Page, "No Seats Available for this grade");
                        //no seats avialble
                    }
                }
                else
                {
                    CustomMessageBoxHelper.Show(this.Page, "This Applcation Already Paid");

                }
            }
            else
            {
                CustomMessageBoxHelper.Show(this.Page, "Please Select The Action ");

            }
        }

        protected void ViewFeesPaidConfirmationLLinkButton_Click(object sender, EventArgs e)
        {
            if (PayFessConfirmationMessageHistoryGridView.Rows.Count > 0)
            {
                PayFessConfirmationMessageHistoryGridView.Visible = !PayFessConfirmationMessageHistoryGridView.Visible;
            }
            else
            {
                CustomMessageBoxHelper.Show(this.Page, "No Message Sent Before ");

            }

            if (PayFessConfirmationMessageHistoryGridView.Visible)
            {
                ViewFeesPaidConfirmationLLinkButton.Text = "hide";
            }
            else
            {
                ViewFeesPaidConfirmationLLinkButton.Text = "show";

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
                CustomMessageBoxHelper.Show(this.Page, "No Message Sent Before ");

            }

            if (PayFessMessageHistoryGridView.Visible)
            {
                ViewPayFeesSMSLinkButton1.Text = "hide";
            }
            else
            {
                ViewPayFeesSMSLinkButton1.Text = "show";

            }
        }

        protected void SearchLinkButton_Click(object sender, EventArgs e)
        {
            //search

            BindingUtility.LoadMessageHistory(TestResultSMSGridView, ApplicantReferenceNumber, "test");

            if (TxtAppRef.Text.Length > 0)
            {
                MOE_APPLICATION_DATA_Model appDTModel = MOE_APPLICATION_DATA_Repository.GetBy(TxtAppRef.Text).Result;


                TxtWListNum.Text = appDTModel.MOE_WAITLIST_NUMBER.ToString();
                TxtPreEnGrade.Text = appDTModel.MOE_APPLIED_GRADE.ToString();
                TxtAppDT.Text = appDTModel.MOE_APPL_DATE.ToString();
                TxtPreEnScl.Text = appDTModel.MOE_SCHOOL_CODE;


                #region loading TestResult Document if exists in the db
                List<TestResultModel> StudentTestResult = TestResultRepository.GetTestResult(TxtAppRef.Text).Result;
                if (StudentTestResult.Count > 0)
                {
                    //view Test Result Document Load
                    //HlinkTestResult.NavigateUrl = StudentTestResult[0].ResultDocLocation;
                    //HlinkTestResult.Visible = true;
                    //btnTestRSTUpload.Enabled = false;

                    List<TestResultModel> tr = TestResultRepository.GetTestResult(TxtAppRef.Text).Result;
                    //  List<TestResultModel> TRForBind = tr.Where(D => D.IsRequiredForSchool == true).Select(D => D).ToList();
                    //gvTestRst.DataSource = tr;
                    //gvTestRst.DataBind();


                }

                #endregion

                #region If exists, Load sms history into Test- Grid


                if ((SMSHistoryRepository.GetSMSHistory(TxtAppRef.Text, "Test")).Result.Count > 0)  // != null)
                {
                    List<SMSHistoryModel> SMSHistModl = SMSHistoryRepository.GetSMSHistory(TxtAppRef.Text, "Test").Result;

                    DataTable dt = new DataTable();

                    dt.Columns.Add("Message Type", typeof(string));
                    dt.Columns.Add("Message Text", typeof(string));
                    dt.Columns.Add("Sent Time", typeof(string));
                    dt.Columns.Add("Status", typeof(string));


                    //foreach (schoolGrade sc in SchoolDataList)
                    foreach (var item in SMSHistModl)
                    {
                        DataRow dr = dt.NewRow();



                        dr["Message Type"] = item.MsgType;
                        dr["Message Text"] = item.MsgText;
                        dr["Sent Time"] = item.MsgTime;
                        dr["Status"] = item.MsgStatus;

                        dt.Rows.Add(dr);
                    }
            BindingUtility.LoadMessageHistory(TestResultSMSGridView, ApplicantReferenceNumber, "test");

                }
                else
                {
                }

                #endregion

                #region If exists, Load sms history into Interview- Grid
                if ((SMSHistoryRepository.GetSMSHistory(TxtAppRef.Text, "Interview")).Result.Count > 0)  // != null)
                {
                    List<SMSHistoryModel> SMSHistModl = SMSHistoryRepository.GetSMSHistory(TxtAppRef.Text, "Interview").Result;

                    DataTable dt = new DataTable();

                    dt.Columns.Add("Message Type", typeof(string));
                    dt.Columns.Add("Message Text", typeof(string));
                    dt.Columns.Add("Sent Time", typeof(string));
                    dt.Columns.Add("Status", typeof(string));


                    //foreach (schoolGrade sc in SchoolDataList)
                    foreach (var item in SMSHistModl)
                    {
                        DataRow dr = dt.NewRow();



                        dr["Message Type"] = item.MsgType;
                        dr["Message Text"] = item.MsgText;
                        dr["Sent Time"] = item.MsgTime;
                        dr["Status"] = item.MsgStatus;

                        dt.Rows.Add(dr);
                    }
                    //GrdVInterview.DataSource = dt;
                    //GrdVInterview.DataBind();
                }
                else
                {
                }

                #endregion

                #region GetAdditional Information from MOE_biodata

                MOE_BIO_DATA_Model BioDataMdl = MOE_BIO_DATA_Repository.GetBioDataByQID(TxtQID.Text).Result;


                #endregion

                //add arabic /english
                TxtGender.Text = BioDataMdl.MOE_GENDER;
                TxtNationality.Text = BioDataMdl.MOE_COUNTRY_ENGLISH_NAME;
                TxtStudentNm.Text = BioDataMdl.MOE_ENGLISH_NAME;

            }

            else
            {
                CustomMessageBoxHelper.Show(this.Page, "Please Enter Application Reference Number ");
            }
        }
    

        protected void SendTestInvatationLinkButton_Click(object sender, EventArgs e)
        {
            string TestDate = Request.Form["TestDateTextBox"].ToString();
            string TestTime = Request.Form["TestTimeText"].ToString();
            string ReasonOfRejection = Request.Form["TestRejectionReasonText"].ToString();
            string NoAction = NoactionTestRadioHiddenField.Value;
            string Invitaion = InviationTestaRadioHiddenField.Value;
            string Rejection = RejecttionTestRadioHiddenField.Value;


            #region get Guardian info: Mobile number

            List<MOE_GUARDIAN_DATA_Model> Guardian = new List<MOE_GUARDIAN_DATA_Model>();
            Guardian = MOE_GUARDIAN_DATA_Repository.GetStudentContactsInfo(TxtQID.Text).Result;
            #endregion

            MessageTemplate msgTmplt = new MessageTemplate();
            string msgTitle = "";




            if (TestDate.Length > 0)
            {
                if (TestTime.Length > 0)
                {

                   

                    if (NoAction == "" || Invitaion == "" || Rejection == "")
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
                            //here inivtation code
                        }
                        if (Rejection == "true")
                        {
                            TestRejectionReasonPanel.Visible = true;
                            SendTestInvatationLinkButton.Enabled = true;
                            msgTitle = PSPESConstants.TestRejection;
                            msgTmplt = MessageTemplateRepository.Getby(msgTitle, 2).Result;
                            //here rejection ciode
                        }
                    }
                    else
                    {
                        CustomMessageBoxHelper.Show(this.Page, "Please Select the action ");

                    }
                }
                else
                {
                    CustomMessageBoxHelper.Show(this.Page, "Please Select the Time ");

                }

            }
            else
            {
                CustomMessageBoxHelper.Show(this.Page, "Please Select the Date ");

            }

            #region Create the message
            string smsMsg = msgTmplt.EnglishMessage;

            smsMsg = smsMsg.Replace("%StudentName% ", TxtStudentNm.Text);
            smsMsg = smsMsg.Replace("%StudentQID%", TxtQID.Text);
            smsMsg = smsMsg.Replace("%reference number%", TxtAppRef.Text);
            //smsMsg = smsMsg.Replace("%school name%", TxtAppRef.Text);
            smsMsg = smsMsg.Replace("%Grade%", TxtPreEnGrade.Text);
            smsMsg = smsMsg.Replace("%date%", TestDate);
            smsMsg = smsMsg.Replace("%time%", TestTime);
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
                BindingUtility.LoadMessageHistory(TestResultSMSGridView, ApplicantReferenceNumber, "test");


            }

            #endregion




        }

        protected void SendTestResultLinkButton_Click(object sender, EventArgs e)
        {
            List<TestResultModel> TestResult = TestResultRepository.GetTestResult(TxtAppRef.Text).Result;


            // FileUpload RequiredFileUpload = (FileUpload)item.FindControl("fuRequiredDocument");
            // HiddenField DocumentTypeIDHiddenField = (HiddenField)item.FindControl("DocumentTypeIDHiddenField");

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


                }
            }
        }

        protected void ShowHideTestSMSHistoryLinkButton_Click(object sender, EventArgs e)
        {

            if (TestResultSMSGridView.Rows.Count > 0)
            {
                TestResultSMSGridView.Visible = !TestResultSMSGridView.Visible;
            }
            else
            {
                CustomMessageBoxHelper.Show(this.Page, "No Message Sent Before ");

            }

            if (TestResultSMSGridView.Visible)
            {
                ShowHideTestSMSHistoryLinkButton.Text = "hide";
            }
            else
            {
                ShowHideTestSMSHistoryLinkButton.Text = "show";

            }
        }

        protected void SendInterviewSMSLinkButton1_Click(object sender, EventArgs e)
        {
            string TestDate = Request.Form["InterviewDateText"].ToString();
            string TestTime = Request.Form["InterviewTimeText"].ToString();
            string ReasonOfRejection = Request.Form["InterviewRejectionReasonText"].ToString();
            string NoAction = InterviewNoactionRadioHiddenField.Value;
            string Invitaion = InterviewInvitationRadioHiddenField.Value;
            string Rejection = InterviewRejectionRadioHiddenField.Value;


            #region get Guardian info: Mobile number

            List<MOE_GUARDIAN_DATA_Model> Guardian = new List<MOE_GUARDIAN_DATA_Model>();
            Guardian = MOE_GUARDIAN_DATA_Repository.GetStudentContactsInfo(GetQID(ApplicantReferenceNumber)).Result;
            #endregion

            MessageTemplate msgTmplt = new MessageTemplate();
            string msgTitle = "";




            if (TestDate.Length > 0)
            {
                if (TestTime.Length > 0)
                {



                    if (NoAction == "" || Invitaion == "" || Rejection == "")
                    {
                        if (NoAction == "true")
                        {
                            InterviewRejectionPanel.Visible = false;
                            SendTestInvatationLinkButton.Enabled = false;
                            //here noaction code
                        }

                        if (Invitaion == "true")
                        {
                            InterviewRejectionPanel.Visible = false;
                            SendTestInvatationLinkButton.Enabled = true;
                            msgTitle = PSPESConstants.InterviewInvitation;
                            msgTmplt = MessageTemplateRepository.Getby(msgTitle, 1).Result;
                            //here inivtation code
                        }
                        if (Rejection == "true")
                        {
                            InterviewRejectionPanel.Visible = true;
                            SendTestInvatationLinkButton.Enabled = true;
                            msgTitle = PSPESConstants.InterviewRejection;
                            msgTmplt = MessageTemplateRepository.Getby(msgTitle, 2).Result;
                            //here rejection ciode
                        }
                    }
                    else
                    {
                        CustomMessageBoxHelper.Show(this.Page, "Please Select the action ");

                    }
                }
                else
                {
                    CustomMessageBoxHelper.Show(this.Page, "Please Select the Time ");

                }

            }
            else
            {
                CustomMessageBoxHelper.Show(this.Page, "Please Select the Date ");

            }

            #region Create the message
            string smsMsg = msgTmplt.EnglishMessage;

            smsMsg = smsMsg.Replace("%StudentName% ", TxtStudentNm.Text);
            smsMsg = smsMsg.Replace("%StudentQID%", TxtQID.Text);
            smsMsg = smsMsg.Replace("%reference number%", TxtAppRef.Text);
            //smsMsg = smsMsg.Replace("%school name%", TxtAppRef.Text);
            smsMsg = smsMsg.Replace("%Grade%", TxtPreEnGrade.Text);
            smsMsg = smsMsg.Replace("%date%", TestDate);
            smsMsg = smsMsg.Replace("%time%", TestTime);
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
                BindingUtility.LoadMessageHistory(TestResultSMSGridView, ApplicantReferenceNumber, "test");


            }

            #endregion
        }

        protected void SendInterviewResultLinkButton1_Click(object sender, EventArgs e)
        {
            //var Task = inter.Insert(new TestResultModel { MOE_APPL_NBR = TxtAppRef.Text, NATIONAL_ID = TxtQID.Text, ResultDocLocation = FilePath, TestResult1 = DDLTestResult.SelectedItem.Text, USERID = SPContext.Current.Site.RootWeb.CurrentUser.Name, DTTM = DateTime.Now }).Result;

        }

        protected void ShowHideInterviewSMSHistoryLinkButton_Click(object sender, EventArgs e)
        {
            if (InterviewSMSHistoryGridView.Rows.Count > 0)
            {
                InterviewSMSHistoryGridView.Visible = !InterviewSMSHistoryGridView.Visible;
            }
            else
            {
                CustomMessageBoxHelper.Show(this.Page, "No Message Sent Before ");

            }

            if (InterviewSMSHistoryGridView.Visible)
            {
                SendInterviewResultLinkButton1.Text = "hide";
            }
            else
            {
                SendInterviewResultLinkButton1.Text = "show";

            }
        }
    }
}
