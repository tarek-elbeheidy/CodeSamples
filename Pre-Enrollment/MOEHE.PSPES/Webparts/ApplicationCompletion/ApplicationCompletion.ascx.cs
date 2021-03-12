using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Configuration;
using MOEHE.PSPES.Models;
using MOEHE.PSPES.Repository;
using System.Collections.Generic;
using System.Data;
using System.Data.Linq;
using System.Linq;
using Microsoft.SharePoint;
using System.Globalization;

namespace MOEHE.PSPES.Webparts.ApplicationCompletion
{

    public partial class ApplicationCompletion : WebPart
    {
        // Uncomment the following SecurityPermission attribute only when doing Performance Profiling on a farm solution
        // using the Instrumentation method, and then remove the SecurityPermission attribute when the code is ready
        // for production. Because the SecurityPermission attribute bypasses the security check for callers of
        // your constructor, it's not recommended for production purposes.
        // [System.Security.Permissions.SecurityPermission(System.Security.Permissions.SecurityAction.Assert, UnmanagedCode = true)]
        public ApplicationCompletion()
        {
        }

        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);
            InitializeControl();
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {

                FillTestResultDDL();
                FillInterviewResultDDL();


              }
        }

        protected void BtnSrch_Click(object sender, EventArgs e)
        {
            LblappRefNum.Text = "";

            MOE_APPLICATION_DATA_Model appDTModel = MOE_APPLICATION_DATA_Repository.GetBy(TxtAppRef.Text).Result;

            if (appDTModel == null)  //|| appDTModel.Count==0
            {
                LblappRefNum.Text = "This Application Reference Number does not exist.";
                LblappRefNum.Visible = true;
                return;
            }
            else {
                LblappRefNum.Visible = false;
                TxtWListNum.Text = appDTModel.MOE_WAITLIST_NUMBER.ToString();
                TxtPreEnGrade.Text = appDTModel.MOE_APPLIED_GRADE.ToString();
                TxtAppDT.Text = appDTModel.MOE_APPL_DATE.ToString();
                TxtPreEnScl.Text = appDTModel.MOE_SCHOOL_CODE;


                #region loading TestResult Document if exists in the db
                List<TestResultModel> StudentTestResult = TestResultRepository.GetTestResult(TxtAppRef.Text).Result;
                if (StudentTestResult.Count > 0)
                {
                    HlinkTestResult.NavigateUrl = StudentTestResult[0].ResultDocLocation;
                    HlinkTestResult.Visible = true;
                    btnTestRSTUpload.Enabled = false;

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
                    GrdVTest.DataSource = dt;
                    GrdVTest.DataBind();
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
                    GrdVInterview.DataSource = dt;
                    GrdVInterview.DataBind();
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

        }

        
        protected void RBtnListTest_SelectedIndexChanged(object sender, EventArgs e)
        {
            TxtRejctRsn.Text = "";
            DTControlTest.ClearSelection();
            TxtTime.Text = "";

            if (RBtnListTest.SelectedValue == "1")
            {
                TxtRejctRsn.Visible = false;
                DTControlTest.Enabled = true;
                TxtTime.Enabled = true;
                btnSendSMStst.Enabled = true;

            }
            else if (RBtnListTest.SelectedValue == "2")
            {
                TxtRejctRsn.Visible = true;
                DTControlTest.Enabled = true;
                TxtTime.Enabled = true;
                btnSendSMStst.Enabled = true;

            }
            else if (RBtnListTest.SelectedValue == "3")
            {
                TxtRejctRsn.Visible = false;
                DTControlTest.Enabled = false;
                TxtTime.Enabled = false;
                btnSendSMStst.Enabled = false;
            }

        }

        protected string SendSMSAppCompletion(string title, string mobileNumber, string txtMessageBody)
        {
            string MessageStatus = "";
            try
            {
                // txtPreEnrollmentSchool.Text += "start";

                int BulkID = 0;
                var BulkReturnedValue = Repository.SendSMS.InsertBulk(new MessageBulkModel { UserID = long.Parse("28135610324"), CreateDate = DateTime.Now, IsBulkSet = false, IsCompleted = false }).Result;
               

                BulkID = BulkReturnedValue[0];

                //}

                string textBody = string.Format("{0}  ", "test message");
                // txtPreEnrollmentSchool.Text += "before insert message";
                var task = Repository.SendSMS.Insert(new MessageDetailsModel
                {
                    UserID = long.Parse("28135610324"),
                    Title = title,
                    MobileNumber = mobileNumber,
                    PriorityID = 1,

                    MessageID = "",
                    IsBulk = true,
                    SenderCode = "11500",
                    BulkID = BulkID,
                    ContactSourceID = 1,
                    TextBody = txtMessageBody
                });

                string MessageID = task.Result;
                //txtPreEnrollmentSchool.Text += MessageID;
                MessageDetailsModel messageDetails = new MessageDetailsModel
                {
                    UserID = long.Parse("28135610324"),
                    Title = title,
                    MobileNumber = mobileNumber,
                    PriorityID = 1,

                    MessageID = MessageID,
                    IsBulk = true,
                    SenderCode = "11500",
                    IsBulkSet = false,
                    BulkID = BulkID,
                    ContactSourceID = 1,
                    TextBody = txtMessageBody
                };


                var messagetask = Repository.SendSMS.Send(messageDetails).Result;
                MessageStatus = messagetask.ToString();
                //txtPreEnrollmentSchool.Text += "after send";
            }
            catch (Exception ex)
            {
                //txtPreEnrollmentSchool.Text += ex.Message;
            }
            return MessageStatus;
        }

        protected void btnSendSMStst_Click(object sender, EventArgs e)
        {
            #region get Guardian info: Mobile number

            List<MOE_GUARDIAN_DATA_Model> Guardian = new List<MOE_GUARDIAN_DATA_Model>();
            Guardian = MOE_GUARDIAN_DATA_Repository.GetStudentContactsInfo(TxtQID.Text).Result;
            #endregion

            MessageTemplate msgTmplt = new MessageTemplate();
            string msgTitle = "";
            if (RBtnListTest.SelectedValue == "1")
            {
                msgTitle = PSPESConstants.TestInvitation;
                msgTmplt = MessageTemplateRepository.Getby(msgTitle, 1).Result;
            }
            else if (RBtnListTest.SelectedValue == "2")
            {
                msgTitle = PSPESConstants.TestRejection;
                msgTmplt = MessageTemplateRepository.Getby(msgTitle, 2).Result;
            }
            //else if (RBtnListTest.SelectedValue == "3")
            //{

            //}

            #region Create the message
            string smsMsg = msgTmplt.EnglishMessage;

            smsMsg = smsMsg.Replace("%StudentName% ", TxtStudentNm.Text);
            smsMsg = smsMsg.Replace("%StudentQID%", TxtQID.Text);
            smsMsg = smsMsg.Replace("%reference number%", TxtAppRef.Text);
            //smsMsg = smsMsg.Replace("%school name%", TxtAppRef.Text);
            smsMsg = smsMsg.Replace("%Grade%", TxtPreEnGrade.Text);
            smsMsg = smsMsg.Replace("%date%", DTControlTest.SelectedDate.Date.ToString());
            smsMsg = smsMsg.Replace("%time%", TxtTime.Text);
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
                    MobileNumber= Guardian[0].MOE_MOBILE_CONTACT_NBR,
                    MsgSender = SPContext.Current.Site.RootWeb.CurrentUser.Name


                };

                var insertSMSHistory = SMSHistoryRepository.Insert(smsHist).Result;
                GrdVTest.DataBind();
            }

            #endregion



        }

        protected void btnTestResultSMS_Click(object sender, EventArgs e)
        {



        }

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

        void FillInterviewResultDDL()
        {

            ListItem ItemTemp1 = new ListItem();
            ItemTemp1.Value = "0";
            // if ((uint)CultureInfo.CurrentUICulture.LCID == 1025)
            //{ 
            //ItemTemp1.Text = "--اختر--"; }
            //else {
            ItemTemp1.Text = "--Select--"; //}

            // ItemTemp1.Selected = true;
            DDLinterviewReslt.Items.Add(ItemTemp1);

            ListItem ItemTemp2 = new ListItem();
            ItemTemp2.Value = "1";
            //if ((uint)CultureInfo.CurrentUICulture.LCID == 1025)
            //{
            // ItemTemp2.Text = "موظف الوزارة"; }
            // else {
            ItemTemp2.Text = "Pass"; //}


            DDLinterviewReslt.Items.Add(ItemTemp2);

            ListItem ItemTemp3 = new ListItem();
            ItemTemp3.Value = "2";
            //if ((uint)CultureInfo.CurrentUICulture.LCID == 1025)
            //{
            // ItemTemp3.Text = "موظف المدرسة"; }
            // else { 
            ItemTemp3.Text = "Fail"; //}

            DDLinterviewReslt.Items.Add(ItemTemp3);


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

        protected void btnTestRSTUpload_Click(object sender, EventArgs e)
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


                    var Task = TestResultRepository.Insert(new TestResultModel { MOE_APPL_NBR = TxtAppRef.Text,NATIONAL_ID = TxtQID.Text, ResultDocLocation = FilePath, TestResult1 = DDLTestResult.SelectedItem.Text, USERID = SPContext.Current.Site.RootWeb.CurrentUser.Name , DTTM= DateTime.Now}).Result;


                }
            }

        }

        protected void btnSendSMSInterview_Click(object sender, EventArgs e)
        {
            #region get Guardian info: Mobile number

            List<MOE_GUARDIAN_DATA_Model> Guardian = new List<MOE_GUARDIAN_DATA_Model>();
            Guardian = MOE_GUARDIAN_DATA_Repository.GetStudentContactsInfo(TxtQID.Text).Result;
            #endregion

            MessageTemplate msgTmplt = new MessageTemplate();
            string msgTitle = "";
            if (RBtnListInterview.SelectedValue == "1")
            {
                msgTitle = PSPESConstants.InterviewInvitation;
                msgTmplt = MessageTemplateRepository.Getby(msgTitle, 4).Result;
            }
            else if (RBtnListInterview.SelectedValue == "2")
            {
                msgTitle = PSPESConstants.InterviewRejection;
                msgTmplt = MessageTemplateRepository.Getby(msgTitle, 5).Result;
            }
            //else if (RBtnListTest.SelectedValue == "3")
            //{

            //}

            #region Create the message
            string smsMsg = msgTmplt.EnglishMessage;

            smsMsg = smsMsg.Replace("%StudentName% ", TxtStudentNm.Text);
            smsMsg = smsMsg.Replace("%StudentQID%", TxtQID.Text);
            smsMsg = smsMsg.Replace("%reference number%", TxtAppRef.Text);
            //smsMsg = smsMsg.Replace("%school name%", TxtAppRef.Text);
            smsMsg = smsMsg.Replace("%Grade%", TxtPreEnGrade.Text);
            smsMsg = smsMsg.Replace("%date%", DTControlTest.SelectedDate.Date.ToString());
            smsMsg = smsMsg.Replace("%time%", TxtTime.Text);
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
                GrdVInterview.DataBind();
            }

            #endregion





        }

        protected void RBtnListInterview_SelectedIndexChanged(object sender, EventArgs e)
        {
            TxtIntRejReason.Text = "";
            DTControlInterview.ClearSelection();
            TxtInterTime.Text = "";


            if (RBtnListInterview.SelectedValue == "1")
            {
                TxtIntRejReason.Visible = false;
                DTControlInterview.Enabled = true;
                TxtInterTime.Enabled = true;
                btnSendSMSInterview.Enabled = true;

            }
            else if (RBtnListInterview.SelectedValue == "2")
            {
                TxtIntRejReason.Visible = true;
                DTControlInterview.Enabled = true;
                TxtInterTime.Enabled = true;
                btnSendSMSInterview.Enabled = true;

            }
            else if (RBtnListInterview.SelectedValue == "3")
            {
                TxtIntRejReason.Visible = false;
                DTControlInterview.Enabled = false;
                TxtInterTime.Enabled = false;
                btnSendSMSInterview.Enabled = false;
            }
        }

        protected void btnViewTestSMSHistory_Click(object sender, EventArgs e)
        {
            if (GrdVTest.Visible == false)
            {
                GrdVTest.Visible = true;
                btnViewTestSMSHistory.Text = "Hide SMS History";
            }
            else { GrdVTest.Visible = false;
                   btnViewTestSMSHistory.Text = "View SMS History";
            }
            
        }

        protected void btnViewInterviewSMSHistory_Click(object sender, EventArgs e)
        {
            if (GrdVInterview.Visible == false)
            {
                GrdVInterview.Visible = true;
                btnViewInterviewSMSHistory.Text = "Hide SMS History";
            }
            else
            {
                GrdVInterview.Visible = false;
                btnViewInterviewSMSHistory.Text = "View SMS History";
            }
        }

        protected void BtnSendIntResultSMS_Click(object sender, EventArgs e)
        {

        }
    }
}
