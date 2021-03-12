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
using System.IO;

namespace MOEHE.PSPES.Webparts.WithdrawApplication
{
    public partial class WithdrawApplicationUserControl : UserControl
    {
        public bool isSchoolUser { get; set; }

        #region Page Events
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
                isSchoolUser = SharePointUtilityRepository.IsUserInGroup(PSPESConstants.PrivateSchoolUsersSharePointGroup, SPContext.Current.Site.RootWeb.CurrentUser.LoginName);
                
                if (isSchoolUser)
                {
                    ResetControls();
                    BindControls();
                }
                else
                {
                    Response.Redirect(PSPESConstants.RedirectPage, true);
                }
            }
        }

        protected void lbCancel_Click(object sender, EventArgs e)
        {
            ResetControls();
        }
        protected void lbSubmit_Click(object sender, EventArgs e)
        {
            try
            {
                string errMsg = string.Empty;
                DateTime rDate = DateTime.ParseExact(txtwithdrawalRequestDate.Value, "dd/MM/yyyy", CultureInfo.InvariantCulture);
                DateTime wDate = DateTime.ParseExact(txtwithdrawalDate.Value, "dd/MM/yyyy", CultureInfo.InvariantCulture);

                if (string.IsNullOrEmpty(txtwithdrawalDate.Value))
                {
                    if (string.IsNullOrEmpty(errMsg))
                    {
                        errMsg = GetGlobalResourceObject("MOEHE.PSPES", "ERROR_SELECT_WITHDRAWALDATE").ToString();
                    }
                    else
                    {
                        errMsg = errMsg + "\\n" + GetGlobalResourceObject("MOEHE.PSPES", "ERROR_SELECT_WITHDRAWALDATE").ToString();
                    }
                }
                if (string.IsNullOrEmpty(txtwithdrawalRequestDate.Value))
                {
                    if (string.IsNullOrEmpty(errMsg))
                    {
                        errMsg = GetGlobalResourceObject("MOEHE.PSPES", "ERROR_SELECT_WITHDRAWALREQUESTDATE").ToString();
                    }
                    else
                    {
                        errMsg = errMsg + "\\n" + GetGlobalResourceObject("MOEHE.PSPES", "ERROR_SELECT_WITHDRAWALREQUESTDATE").ToString();
                    }
                }
                if (string.IsNullOrEmpty(ddlWithdrawalReason.SelectedValue))
                {
                    if (string.IsNullOrEmpty(errMsg))
                    {
                        errMsg = GetGlobalResourceObject("MOEHE.PSPES", "ERROR_SELECT_WITHDRAWALREASON").ToString();
                    }
                    else
                    {
                        errMsg = errMsg + "\\n" + GetGlobalResourceObject("MOEHE.PSPES", "ERROR_SELECT_WITHDRAWALREASON").ToString();
                    }
                }
                if ((fUploadForm.PostedFile == null) || (fUploadForm.PostedFile.ContentLength <= 0))
                {
                    if (string.IsNullOrEmpty(errMsg))
                    {
                        errMsg = GetGlobalResourceObject("MOEHE.PSPES", "ERROR_UPLOAD_DOCUMENT").ToString();
                    }
                    else
                    {
                        errMsg = errMsg + "\\n" + GetGlobalResourceObject("MOEHE.PSPES", "ERROR_UPLOAD_DOCUMENT").ToString();
                    }
                }
                if (string.IsNullOrEmpty(errMsg))
                { 
                    string filePath = string.Empty;
                    string loginName = GetDomainLoginName();                                       

                    filePath = SaveToLibrary();

                    //Adding new record into withdrawal application for cancellation.
                    MOE_WITHDRAWAL_DATA_Model oModel = new MOE_WITHDRAWAL_DATA_Model
                    {
                        MOE_SCHOOL_CODE = hdnSchoolCode.Value,
                        MOE_NATIONAL_ID = txtQID.Text.Trim(),
                        MOE_REFERENCE_ID = txtApplicationRefNo.Text.Trim(),
                        MOE_WITHDRAWAL_REQUESTDATE = rDate,
                        MOE_WITHDRAWAL_DATE = wDate,
                        MOE_WITHDRAWAL_REASONID = ddlWithdrawalReason.SelectedIndex,
                        MOE_ATTACHMENT = filePath,
                        MOE_LOGIN_ID = loginName
                    };
                    var task = MOE_WITHDRAWAL_DATA_Repository.Insert(oModel).Result;

                    if (task != null)
                    {
                        SendOTP();
                        CustomMessageBoxHelper.Show(this.Page, GetGlobalResourceObject("MOEHE.PSPES", "SUCCESS_WITHDRAWAL_ONSUBMIT").ToString());
                        ResetControls();
                    }
                }
                else
                {
                    CustomMessageBoxHelper.Show(this.Page, errMsg);
                }
            }
            catch (Exception ex)
            {
                CustomMessageBoxHelper.Show(this.Page, ex.Message);
            }
        }
        public string SaveToLibrary()
        {
            string filePath = string.Empty;

            try
            {
                if (fUploadForm.PostedFile != null)
                {
                    if (fUploadForm.PostedFile.ContentLength > 0)
                    {
                        Stream strm = fUploadForm.PostedFile.InputStream;
                        byte[] bytes = new byte[Convert.ToInt32(fUploadForm.PostedFile.ContentLength)];

                        strm.Read(bytes, 0, Convert.ToInt32(fUploadForm.PostedFile.ContentLength));
                        string fileExtension = Path.GetExtension(fUploadForm.FileName);
                        string fileRef = string.Format("{0}-{1}{2}", DateTime.Now.Year.ToString(), DateTime.Now.Minute, DateTime.Now.Second);

                        SPSecurity.RunWithElevatedPrivileges(delegate ()
                        {
                            using (SPSite oSite = new SPSite(SPContext.Current.Site.Url))
                            {
                                using (SPWeb oWeb = oSite.OpenWeb())
                                {
                                    SPFolder mylibrary = oWeb.Folders["ApplicantAttachedDocuments"];
                                    if (mylibrary != null)
                                    {
                                        Random rd = new Random();
                                        oWeb.AllowUnsafeUpdates = true;
                                        string fileName = string.Format("{0}{1}", fileRef, fileExtension);
                                        SPFile ApplicantFile = mylibrary.Files.Add(fileName, bytes, true);
                                        int itemID = ApplicantFile.Item.ID;
                                        filePath = SPContext.Current.Site.Url + "/" + ApplicantFile.Item.Url.ToString();
                                        oWeb.AllowUnsafeUpdates = false;
                                    }
                                }
                            }
                        });
                    }
                }
            }
            catch (Exception ex)
            {
                CustomMessageBoxHelper.Show(this.Page, ex.Message);
            }
            return filePath;
        }

        public static string GetDomainLoginName()
        {
            SPUser user = SPContext.Current.Web.CurrentUser;
            if (user.LoginName.Contains("|"))
                return user.LoginName.Split('|')[1];
            else
                return user.LoginName;
        }

        #region Removed OTP on 30 July 2018 By Veer

        //protected void lnkSendOtp_Click(object sender, EventArgs e)
        //{
        //    if (SendOTP())
        //    {
        //        if (CultureInfo.CurrentUICulture.Name.ToLower() == "ar-sa" || CultureInfo.CurrentCulture.Name.ToLower() == "ar-sa")
        //            CustomMessageBoxHelper.Show(this.Page, "تم إرسال الرسالة بنجاح");
        //        else
        //            CustomMessageBoxHelper.Show(this.Page, "OTP sent successfully");
        //    }
        //    else
        //    {
        //        if (CultureInfo.CurrentUICulture.Name.ToLower() == "ar-sa" || CultureInfo.CurrentCulture.Name.ToLower() == "ar-sa")
        //            CustomMessageBoxHelper.Show(this.Page, "Unable to send OTP");
        //        else
        //            CustomMessageBoxHelper.Show(this.Page, "Unable to send OTP");
        //    }
        //}

        //protected void lnkReSendOTP_Click(object sender, EventArgs e)
        //{
        //    if (SendOTP())
        //    {
        //        if ((uint)CultureInfo.CurrentUICulture.LCID == 1025)
        //            CustomMessageBoxHelper.Show(this.Page, "تم إرسال الرسالة بنجاح");
        //        else
        //            CustomMessageBoxHelper.Show(this.Page, "OTP sent successfully");
        //    }
        //    else
        //    {
        //        if ((uint)CultureInfo.CurrentUICulture.LCID == 1025)
        //            CustomMessageBoxHelper.Show(this.Page, "Unable to send OTP");
        //        else
        //            CustomMessageBoxHelper.Show(this.Page, "Unable to send OTP");
        //    }
        //}
        #endregion

        #endregion

        #region On QID Changed
        protected void txtQID_TextChanged(object sender, EventArgs e)
        {
            string errMsg = string.Empty;
            string QID = txtQID.Text.Trim();
            string RefID = string.Empty;
            List<TermModel> oTerm = TermRepository.GetTerms().Result;
            string TERMCode = oTerm.Where(D => D.IsActive).Select(D => D.TermCode).FirstOrDefault();
            UserHelper userhelper = ADutility.GetUserDetails(SPContext.Current.Site.RootWeb.CurrentUser);
            string schoolCode = userhelper.DepartmentID;
            MOE_APPLICATION_DATA_Model oApp = MOE_APPLICATION_DATA_Repository.FinalizedBySchool(QID, schoolCode, Convert.ToInt32(TERMCode)).Result;
            #region Validation
            if (QID.Length != 11)
            {
                if (string.IsNullOrEmpty(errMsg))
                {
                    errMsg = GetGlobalResourceObject("MOEHE.PSPES", "ERROR_VALID_QID").ToString();
                }
                else
                {
                    errMsg = errMsg + "\\n" + GetGlobalResourceObject("MOEHE.PSPES", "ERROR_VALID_QID").ToString();
                }
            }
            if ((oApp == null))
            {
                if (string.IsNullOrEmpty(errMsg))
                {
                    errMsg = GetGlobalResourceObject("MOEHE.PSPES", "ERROR_STUDENTDATA_NOTAVAILABLE").ToString();
                }
                else
                {
                    errMsg = errMsg + "\\n" + GetGlobalResourceObject("MOEHE.PSPES", "ERROR_STUDENTDATA_NOTAVAILABLE").ToString();
                }
            }
            #endregion

            if (string.IsNullOrEmpty(errMsg))
            {
                RefID = oApp.MOE_APPL_REF_NBR;
                FillStudentInfo(QID, RefID, oTerm);
            }
            else
            {
                CustomMessageBoxHelper.Show(this.Page, errMsg);
                ResetControls();
            }
        }
        #endregion

        #region Fill Student Information
        public void FillStudentInfo(string QID, string RefId, List<TermModel> oTerm)
        {
            try
            {
                MOE_BIO_DATA_Model BioData = MOE_BIO_DATA_Repository.GetBioDataByQID(QID).Result;
                decimal qID = decimal.Parse(QID);
                List<MOE_GUARDIAN_DATA_Model> GuardianData = new List<MOE_GUARDIAN_DATA_Model>();
                string guardianQID = "";
                string relationID = "";
                string mobileNo = "";
                string homeNo = "";
                string emailId = "";
                GuardianData = MOE_GUARDIAN_DATA_Repository.GetStudentContactsInfo(QID).Result;
                if ((BioData != null) && (GuardianData != null))
                {
                    if (GuardianData.Select(W => W.MOE_RELATED_QID) !=null)
                    {
                        guardianQID = GuardianData.Select(W => W.MOE_RELATED_QID).FirstOrDefault().ToString();
                    }
                    if (GuardianData.Select(W => W.MOE_MOBILE_CONTACT_NBR) != null)
                    {
                        mobileNo = GuardianData.Select(W => W.MOE_MOBILE_CONTACT_NBR).FirstOrDefault().ToString();
                    }
                    if (GuardianData.Select(W => W.MOE_HOME_CONTACT_NBR) != null)
                    {
                        homeNo = GuardianData.Select(W => W.MOE_HOME_CONTACT_NBR).FirstOrDefault().ToString();
                    }
                    if (GuardianData.Select(W => W.MOE_EMAIL) != null)
                    {
                        emailId = GuardianData.Select(W => W.MOE_EMAIL).FirstOrDefault().ToString();
                    }
                   
                   
                    
                   
                    
                   
                    List<MOE_SCHOOL_Model> oSchoolModel = MOE_SCHOOL_Repository.GetSchoolsDB().Result;
                    MOE_APPLICATION_DATA_Model oApp = MOE_APPLICATION_DATA_Repository.GetByRefAndID(RefId, QID).Result;
                    hdnSchoolCode.Value = oApp.MOE_SCHOOL_CODE;
                    string TERMCode = oTerm.Where(D => D.IsActive).Select(D => D.TermCode).FirstOrDefault();
                    string TERMName = oTerm.Where(D => D.IsActive).Select(D => D.TermName).FirstOrDefault();

                    string gID = guardianQID.Split('.')[0];
                    MOE_BIO_DATA_Model GuardianBioData = MOE_BIO_DATA_Repository.GetBioDataByQID(gID).Result;

                    List<ListOfValues_Model> Curriculums = ListOfValues_Repository.GetListOfValues(PSPESConstants.CurriculumsCodesetID).Result;
                    string curriculumID = MOE_AGE_VALIDATE_Repository.GetCurriculumIDBySchoolCode(TERMCode, oApp.MOE_SCHOOL_CODE).Result;
                    ListOfValues_Model oCurriculum = Curriculums.Where(R => R.ID == curriculumID).FirstOrDefault();

                    List<ListOfValues_Model> RelationShip = ListOfValues_Repository.GetListOfValues(PSPESConstants.RelationshipsCodesetID).Result;
                    ListOfValues_Model oRelation = RelationShip.Where(R => R.ID == relationID).FirstOrDefault();

                    if (CultureInfo.CurrentUICulture.Name.ToLower() == "ar-sa" || CultureInfo.CurrentCulture.Name.ToLower() == "ar-sa")
                    {
                        //BIO DATA
                        txtName.Text = BioData.MOE_ARABIC_NAME; 
                        txtGender.Text = BioData.MOE_GENDER; 
                        txtDOB.Text = BioData.MOE_DOB; 
                        txtNationality.Text = BioData.MOE_COUNTRY_ARABIC_NAME; 

                        //ACADAMIC INFO
                        txtTerm.Text = TERMName;

                        txtPreEnrollmentSchool.Text = oSchoolModel.Where(D => D.MOE_SCHOOL_CODE == oApp.MOE_SCHOOL_CODE).Select(D => D.MOE_SCHOOL_NAME_ARA).FirstOrDefault();
                        txtCurrentCurriculum.Text = oCurriculum.DescriptionArabic.ToString(); 

                        txtApplicationRefNo.Text = RefId;
                        dtApplicationDate.Value = oApp.MOE_APPL_DATE.ToString();
                        txtPreEnrolmentGrade.Text = oApp.MOE_APPLIED_GRADE;

                        //Guardian Data
                        txtGuardianQID.Text = gID; 
                        txtGuardianName.Text = GuardianBioData.MOE_ARABIC_NAME; 
                        txtGuardianGender.Text = GuardianBioData.MOE_GENDER; 
                        txtGuardianCountry.Text = GuardianBioData.MOE_COUNTRY_ARABIC_NAME;
                        if (oRelation != null) txtRelationship.Text = oRelation.DescriptionArabic;
                        txtGuardianMobile.Text = mobileNo; 
                        txtGuardianLandLine.Text = homeNo; 
                        txtGuardianEmail.Text = emailId; 
                    }
                    else
                    {
                        //BIO DATA
                        txtName.Text = BioData.MOE_ENGLISH_NAME; 
                        txtGender.Text = BioData.MOE_GENDER; 
                        txtDOB.Text = BioData.MOE_DOB; 
                        txtNationality.Text = BioData.MOE_COUNTRY_ENGLISH_NAME; 

                        //ACADAMIC INFO
                        txtTerm.Text = TERMName;
                        txtPreEnrollmentSchool.Text = oSchoolModel.Where(D => D.MOE_SCHOOL_CODE == oApp.MOE_SCHOOL_CODE).Select(D => D.MOE_SCHOOL_NAME_ENG).FirstOrDefault();
                        txtCurrentCurriculum.Text = oCurriculum.DescriptionEnglish.ToString();
                        txtApplicationRefNo.Text = RefId; 
                        dtApplicationDate.Value = oApp.MOE_APPL_DATE.ToString();
                        txtPreEnrolmentGrade.Text = oApp.MOE_APPLIED_GRADE;
                        //Guardian Data
                        txtGuardianQID.Text = gID; 
                        txtGuardianName.Text = GuardianBioData.MOE_ENGLISH_NAME; 
                        txtGuardianGender.Text = GuardianBioData.MOE_GENDER; 
                        txtGuardianCountry.Text = GuardianBioData.MOE_COUNTRY_ENGLISH_NAME; 
                        if(oRelation != null) txtRelationship.Text = oRelation.DescriptionEnglish; 
                        txtGuardianMobile.Text = mobileNo; 
                        txtGuardianLandLine.Text = homeNo; 
                        txtGuardianEmail.Text = emailId; 
                    }
                    divSubmit.Visible = true;
                }
                else
                {
                    CustomMessageBoxHelper.Show(this.Page, GetGlobalResourceObject("MOEHE.PSPES", "ERROR_NOTFINALIZED_APPLICATION").ToString());
                    ResetControls();
                }
            }
            catch (Exception ex)
            {
                CustomMessageBoxHelper.Show(this.Page, ex.Message);
            }
        }
        #endregion

        #region BindControls
        public void BindControls()
        {
            List<MOE_WITHDRAWAL_REASON_Model> oData = MOE_WITHDRAWAL_REASON_Repository.GetWithdrawalReasons().Result;
            if (oData != null)
            {
                if (CultureInfo.CurrentUICulture.Name.ToLower() == "ar-sa" || CultureInfo.CurrentCulture.Name.ToLower() == "ar-sa")
                {
                    ddlWithdrawalReason.DataSource = oData;
                    ddlWithdrawalReason.DataTextField = "MOE_NAME_ARABIC";
                    ddlWithdrawalReason.DataValueField = "ID";
                    ddlWithdrawalReason.DataBind();
                    ddlWithdrawalReason.Items.Insert(0, new ListItem(this.GetGlobalResourceObject("MOEHE.PSPES", "PleaseSelect").ToString(), ""));
                }
                else
                {
                    ddlWithdrawalReason.DataSource = oData;
                    ddlWithdrawalReason.DataTextField = "MOE_NAME_ENGLISH";
                    ddlWithdrawalReason.DataValueField = "ID";
                    ddlWithdrawalReason.DataBind();
                    ddlWithdrawalReason.Items.Insert(0, new ListItem(this.GetGlobalResourceObject("MOEHE.PSPES", "PleaseSelect").ToString(), ""));
                }
            }
        }
        #endregion

        #region Reset Controls
        public void ResetControls()
        {
            txtQID.Enabled = true;
            txtApplicationRefNo.Text = "";
            txtCurrentCurriculum.Text = "";
            txtDOB.Text = "";
            txtGender.Text = "";
            txtApplicationRefNo.Text = "";
            txtGuardianName.Text = "";
            txtGuardianGender.Text = "";
            txtGuardianCountry.Text = "";
            txtGuardianEmail.Text = "";
            txtGuardianLandLine.Text = "";
            txtGuardianMobile.Text = "";
            txtGuardianQID.Text = "";
            txtName.Text = "";
            txtNationality.Text = "";
            txtPreEnrollmentSchool.Text = "";
            txtPreEnrolmentGrade.Text = "";
            txtQID.Text = "";
            txtRelationship.Text = "";
            txtTerm.Text = "";
            txtwithdrawalDate.Value = "";
            txtwithdrawalRequestDate.Value = "";
            divSubmit.Visible = false;
        }
        #endregion

        #region Send OTP on Withdrawal Application
        public bool SendOTP()
        {
            bool isSent = false;
            try
            {
                #region Removed OTP on 30 July 2018 by Veer
                //string errMsg = string.Empty;
                //if (string.IsNullOrEmpty(txtQID.Text))
                //{
                //    if (string.IsNullOrEmpty(errMsg))
                //    {
                //        if (CultureInfo.CurrentUICulture.Name.ToLower() == "ar-sa" || CultureInfo.CurrentCulture.Name.ToLower() == "ar-sa")
                //        {
                //            errMsg = "Please provide QID";
                //        }
                //        else
                //        {
                //            errMsg = "Please provide QID";
                //        }
                //    }
                //}

                //if (string.IsNullOrEmpty(errMsg))
                //{
                //    string msgTitle = PSPESConstants.WithdrawApplication;
                //    MessageTemplate msgTmplt = MessageTemplateRepository.Getby(msgTitle, 12).Result;
                //    hdnOTP.Value = Utility.GenerateOTP();

                //    string smsMsg = string.Empty;

                //    if (CultureInfo.CurrentUICulture.Name.ToLower() == "ar-sa" || CultureInfo.CurrentCulture.Name.ToLower() == "ar-sa")
                //        smsMsg = msgTmplt.ArabicMessage;
                //    else
                //        smsMsg = msgTmplt.EnglishMessage;

                //    if (msgTitle == PSPESConstants.WithdrawApplication)
                //    {
                //        smsMsg = smsMsg.Replace("%studentname% ", txtName.Text);
                //        smsMsg = smsMsg.Replace("%withdrawreason%", ddlWithdrawalReason.SelectedItem.Text);
                //        smsMsg = smsMsg.Replace("%studentQID%", txtQID.Text);
                //        smsMsg = smsMsg.Replace("%referencenumber%", txtApplicationRefNo.Text);
                //        smsMsg = smsMsg.Replace("%otpno%", hdnOTP.Value);
                //    }
                //    string loginName = GetDomainLoginName();
                //    string smsStatus = SendSMS.SendMessage(msgTitle, txtGuardianMobile.Text, smsMsg);
                //    if (smsStatus == "1000")
                //    {
                //        SMSHistoryModel smsHist = new SMSHistoryModel()
                //        {
                //            MOE_APPL_REF_NBR = txtApplicationRefNo.Text,
                //            MsgStatus = "SENT",
                //            MsgText = smsMsg,
                //            MsgTime = DateTime.Now.ToString(),
                //            MsgType = "Withdrawal",
                //            MsgTitle = msgTitle,
                //            MobileNumber = txtGuardianMobile.Text,
                //            MsgSender = loginName
                //        };
                //        var insertSMSHistory = SMSHistoryRepository.Insert(smsHist).Result;
                //        if (insertSMSHistory != null)
                //        {
                //            isSent = true;
                //        }
                //    }
                //}
                //else
                //{
                //    CustomMessageBoxHelper.Show(this.Page, errMsg);
                //}
                #endregion
                string msgTitle = PSPESConstants.WithdrawApplication;
                MessageTemplate msgTmplt = MessageTemplateRepository.Getby(msgTitle, 12).Result;
                //hdnOTP.Value = Utility.GenerateOTP();

                string smsMsg = string.Empty;

                if (CultureInfo.CurrentUICulture.Name.ToLower() == "ar-sa" || CultureInfo.CurrentCulture.Name.ToLower() == "ar-sa")
                    smsMsg = msgTmplt.ArabicMessage;
                else
                    smsMsg = msgTmplt.EnglishMessage;

                if (msgTitle == PSPESConstants.WithdrawApplication)
                {
                    smsMsg = smsMsg.Replace("%studentname% ", txtName.Text);
                    smsMsg = smsMsg.Replace("%withdrawreason%", ddlWithdrawalReason.SelectedItem.Text);
                    smsMsg = smsMsg.Replace("%studentQID%", txtQID.Text);
                    smsMsg = smsMsg.Replace("%referencenumber%", txtApplicationRefNo.Text);
                    smsMsg = smsMsg.Replace("%grade%", txtPreEnrolmentGrade.Text);
                    smsMsg = smsMsg.Replace("%schoolname%", txtPreEnrollmentSchool.Text);
                    smsMsg = smsMsg.Replace("%academicyear%", txtTerm.Text);
                }
                string loginName = GetDomainLoginName();
                string smsStatus = SendSMS.SendMessage(msgTitle, txtGuardianMobile.Text, smsMsg);
                if (smsStatus == "1000")
                {
                    SMSHistoryModel smsHist = new SMSHistoryModel()
                    {
                        MOE_APPL_REF_NBR = txtApplicationRefNo.Text,
                        MsgStatus = "SENT",
                        MsgText = smsMsg,
                        MsgTime = DateTime.Now.ToString(),
                        MsgType = "Withdrawal",
                        MsgTitle = msgTitle,
                        MobileNumber = txtGuardianMobile.Text,
                        MsgSender = loginName
                    };
                    var insertSMSHistory = SMSHistoryRepository.Insert(smsHist).Result;
                    if (insertSMSHistory != null)
                    {
                        isSent = true;
                    }
                }
            }
            catch (Exception ex)
            {
                isSent = false;
            }
            return isSent;
        }
        #endregion
    }
}

