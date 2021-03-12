using ITWorx.MOEHEWF.Nintex.Actions;
using ITWORX.MOEHE.Utilities;
using ITWORX.MOEHE.Utilities.Logging;
using ITWORX.MOEHEWF.Common.Utilities;
using ITWORX.MOEHEWF.SCE.BL;
using Microsoft.SharePoint;
using Microsoft.SharePoint.Utilities;
using System;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using BarcodeLib;
using ITWORX.MOEHEWF.SCE.Entities;
using System.Text.RegularExpressions;

namespace ITWORX.MOEHEWF.SCE.WebParts.PERecommendation
{
    public partial class PERecommendationUserControl : UserControlBase
    {

        protected global::ITWORX.MOEHEWF.Common.CONTROLTEMPLATES.ITWORX.MOEHEWF.Common.ClientSideFileUpload AddFile;
        protected global::ITWORX.MOEHEWF.Common.CONTROLTEMPLATES.ITWORX.MOEHEWF.Common.ClientSideFileUpload ViewFile;


        public string requestNumber
        {
            get
            {
                if (ViewState["requestNumber"] != null)
                {
                    return ViewState["requestNumber"].ToString();
                }
                return null;
            }
            set
            {
                ViewState["requestNumber"] = value;
            }
        }

        public string creationDate
        {
            get
            {
                if (ViewState["creationDate"] != null)
                {
                    return ViewState["creationDate"].ToString();
                }
                return null;
            }
            set
            {
                ViewState["creationDate"] = value;
            }
        }




        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                bindPage();
            }
        }

        void bindPage()
        {
            Logging.GetInstance().Debug("Entering method PERecommendationUserControl.bindPage");
            try
            {
                if (Request.QueryString["RequestId"] != null)
                {
                    int requestID = Convert.ToInt32(Request.QueryString["RequestId"]);
                    using (SCEContextDataContext ctx = new SCEContextDataContext(SPContext.Current.Site.RootWeb.Url))
                    {
                        var currentRequest = ctx.SCERequestsList.ScopeToFolder("", true).Where(x => x.Id == requestID).FirstOrDefault();
                        if (currentRequest != null)
                        {
                            var recommendation = getLastRecommendation();

                            int currentStatus = (int)currentRequest.RequestStatus.Id;
                            if ((currentStatus == (int)RequestStatus.SCESubmitted 
                                || currentStatus == (int)RequestStatus.SCECulturalMissionStatementReply
                                || currentStatus == (int)RequestStatus.SCEEmployeeClarificationReply 
                                || currentStatus == (int)RequestStatus.SCESectionManagerMissingInformation
                                || currentStatus == (int)RequestStatus.SCEDepartmentManagerMissingRecommendation
                                || currentStatus == (int)RequestStatus.SCEEquivalenceEmployeeReassign) && currentRequest.EmployeeAssignedTo.ToLower() == SPContext.Current.Web.CurrentUser.LoginName.ToLower())
                            {
                                div_Edit.Visible = true;
                                div_View.Visible = false;
                                bindDDL();

                                int currentgrade = (int)currentRequest.RegisteredScholasticLevel.Id;
                                if (recommendation != null)
                                    bindAddEditDiv(recommendation, requestID, currentgrade);
                                else
                                    fileUpload_init(false, requestID);

                                // LoadDecisionTextContro();
                            }
                            else
                            {
                                div_View.Visible = true;
                                div_Edit.Visible = false;

                                if (recommendation != null)
                                    bindViewDiv(recommendation, requestID);
                                else
                                    fileUpload_init(true, requestID);
                            }
                        }


                    }
                }

            }
            catch (Exception ex)
            {
                Logging.GetInstance().LogException(ex);
            }
            finally
            {
                Logging.GetInstance().Debug("Exiting method PERecommendationUserControl.bindPage");
            }
        }

        void fileUpload_init(bool isViewMode, int requestID)
        {
            Logging.GetInstance().Debug("Entering method PERecommendationUserControl.fileUpload_init");
            try
            {
                using (SCEContextDataContext ctx = new SCEContextDataContext(SPContext.Current.Site.RootWeb.Url))
                {
                    AttachmentsLookupListFieldsContentType attachmentInfo = ctx.AttachmentsLookupList.Where(a => a.Group == ITWORX.MOEHEWF.SCE.Utilities.Constants.SCESearchStatusAttachments).FirstOrDefault();
                    if (isViewMode)
                    {
                        if (attachmentInfo != null)
                        {
                            ViewFile.Group = attachmentInfo.Group;
                            ViewFile.DocumentLibraryName = attachmentInfo.DocumentLibraryName;
                            ViewFile.DocLibWebUrl = attachmentInfo.DocLibWebUrl;
                            ViewFile.Title = LCID == (int)Language.English ? attachmentInfo.Title : attachmentInfo.TitleAr;
                            ViewFile.LookupFieldName = attachmentInfo.LookupFieldName;
                            ViewFile.LookupFieldValue = Convert.ToInt32(requestID);
                            ViewFile.DisplayMode = false;
                        }
                    }
                    else
                    {
                        if (attachmentInfo != null)
                        {
                            AddFile.Group = attachmentInfo.Group;
                            AddFile.HasOptions = false;
                            AddFile.DocumentLibraryName = attachmentInfo.DocumentLibraryName;
                            AddFile.IsRequired = (bool)attachmentInfo.IsRequired;
                            AddFile.MaxFileNumber = (int)attachmentInfo.MaxFileNumber;
                            AddFile.MaxSize = (int)attachmentInfo.MaxSize;
                            AddFile.SupportedExtensions = attachmentInfo.SupportedExtensions;
                            AddFile.DocLibWebUrl = attachmentInfo.DocLibWebUrl;
                            AddFile.Title = LCID == (int)Language.English ? attachmentInfo.Title : attachmentInfo.TitleAr;
                            AddFile.RequiredValidationMessage = LCID == (int)Language.English ? attachmentInfo.RequiredValidationMessage : attachmentInfo.RequiredValidationMessageAr;
                            AddFile.LookupFieldName = attachmentInfo.LookupFieldName;
                            AddFile.LookupFieldValue = Convert.ToInt32(requestID);
                            AddFile.FileExtensionValidation = HelperMethods.LocalizedText("ITWORX_MOEHEWF_SCE", "FileExtensionValidation", (uint)LCID); //"Supported file extensions are jpg,pdf,png";
                            AddFile.FileSizeValidationMsg = string.Format(HelperMethods.LocalizedText("ITWORX_MOEHEWF_SCE", "FileMaxValidationMsg", (uint)LCID), AddFile.MaxSize); //"File size must not be greater than " + FileUp1.MaxSize + " MB";
                            AddFile.FileNumbersValidationMsg = string.Format(HelperMethods.LocalizedText("ITWORX_MOEHEWF_SCE", "FileNumbersValidationMsg", (uint)LCID), AddFile.MaxFileNumber); //"You can't upload more than " + FileUp1.MaxFileNumber + " files";
                            AddFile.FileExistsValidationMsg = HelperMethods.LocalizedText("ITWORX_MOEHEWF_SCE", "FileExistsValidationMsg", (uint)LCID); //"File exists with the same name";
                            AddFile.ValidqationGroup = "RecommendationGroup1";
                            AddFile.DisplayMode = true;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Logging.GetInstance().LogException(ex);
            }
            finally
            {
                Logging.GetInstance().Debug("Exiting method PERecommendationUserControl.fileUpload_init");
            }
        }

        SCESearchStatusRecommendationListFieldsContentType getLastRecommendation()
        {
            using (SCEContextDataContext ctx = new SCEContextDataContext(SPContext.Current.Site.RootWeb.Url))
            {
                int requestID = Convert.ToInt32(Request.QueryString["RequestId"]);
                var recommendation = ctx.SCESearchStatusRecommendationList.ScopeToFolder("", true).Where(x => x.RequestID.Id == requestID).LastOrDefault();
                return recommendation;
            }
        }
        void bindAddEditDiv(SCESearchStatusRecommendationListFieldsContentType recommendation, int requestID,int currentGrade)
        {
            Logging.GetInstance().Debug("Entering method PERecommendationUserControl.bindAddEditDiv");
            try
            {
                txt_SearchStatus.Text = recommendation.SearchStatus;
                txt_decision.Text = recommendation.DecisionText;
                if (recommendation.SendRequestToId != null)
                {
                    ddl_Recommendation.SelectedValue = "2";
                    ddl_assignRejected.SelectedValue = recommendation.SendRequestToId.ToString();
                }
                else
                    ddl_Recommendation.SelectedValue = "1";

              
             
                if (ddl_Recommendation.SelectedValue == "1")// Approved
                {
                    
                    btn_Preview.Enabled = true;
                   
                }
                else
                {
                    if (currentGrade < 14)
                    {
                       
                        btn_Preview.Enabled = false;
                    }
                    else
                    {
                        btn_Preview.Enabled = true;

                    }
                }

                if (recommendation.ClassRoom != null)
                    ddl_ScholasticLevel.SelectedValue = recommendation.ClassRoom.Id.ToString();


                fileUpload_init(false, requestID);
            }
            catch (Exception ex)
            {
                Logging.GetInstance().LogException(ex);
            }
            finally
            {
                Logging.GetInstance().Debug("Exiting method PERecommendationUserControl.bindAddEditDiv");
            }
        }
      
        void bindViewDiv(SCESearchStatusRecommendationListFieldsContentType recommendation, int requestID)
        {
            Logging.GetInstance().Debug("Entering method PERecommendationUserControl.bindViewDiv");
            try
            {
                int recommendationID = 0;
                int assignRejected = 0;
                int scLevel = 0;
                lbl_SearchStatus.Text = recommendation.SearchStatus;
                lbl_decision.Text = recommendation.DecisionText;




                if (recommendation.SendRequestToId != null)
                {
                    recommendationID = 2;
                    assignRejected = (int)recommendation.SendRequestToId;
                }
                else
                    recommendationID = 1;


                if (recommendation.ClassRoom != null)
                    scLevel = (int)recommendation.ClassRoom.Id;

                using (SCEContextDataContext ctx = new SCEContextDataContext(SPContext.Current.Site.RootWeb.Url))
                {
                    if (recommendationID != 0)
                    {
                        var recommen = ctx.RecommendationsList.Where(x => x.Id == recommendationID).FirstOrDefault();
                        lbl_Recommendation.Text = LCID == 1033 ? recommen.Title : recommen.TitleAr;
                    }
                    if (scLevel != 0)
                    {
                        var classRoom = ctx.ScholasticLevelList.Where(x => x.Id == scLevel).FirstOrDefault();
                        lbl_ScholasticLevel.Text = LCID == 1033 ? classRoom.Title : classRoom.TitleAr;
                    }
                    if (assignRejected != 0)
                    {
                        var rej = ctx.SCEEmployeesList.Where(x => x.Id == assignRejected).FirstOrDefault();
                        lbl_assignRejected.Text = LCID == 1033 ? rej.Title : rej.TitleAr;
                        assignRejectedDiv.Visible = true;
                    }
                    fileUpload_init(true, requestID);
                }
            }
            catch (Exception ex)
            {
                Logging.GetInstance().LogException(ex);
            }
            finally
            {
                Logging.GetInstance().Debug("Exiting method PERecommendationUserControl.bindViewDiv");
            }
        }
        void bindDDL()
        {
            using (SCEContextDataContext ctx = new SCEContextDataContext(SPContext.Current.Site.RootWeb.Url))
            {
                var recommendations = ctx.Recommendations;
                HelperMethods.BindDropDownList(ref ddl_Recommendation, recommendations, "ID", "TitleAr", "Title", LCID);
                ddl_Recommendation.Items.Insert(0, new ListItem(HelperMethods.LocalizedText("ITWORX_MOEHEWF_UCE", "ChooseValue", (uint)LCID), "-1"));

                var scholasticLevels = ctx.ScholasticLevel;
                HelperMethods.BindDropDownList(ref ddl_ScholasticLevel, scholasticLevels, "ID", "TitleAr", "Title", LCID);
                ddl_ScholasticLevel.Items.Insert(0, new ListItem(HelperMethods.LocalizedText("ITWORX_MOEHEWF_UCE", "ChooseValue", (uint)LCID), "-1"));

                var assignRejected = ctx.SCEEmployees;
                HelperMethods.BindDropDownList(ref ddl_assignRejected, assignRejected, "ID", "TitleAr", "Title", LCID);
                ddl_assignRejected.Items.Insert(0, new ListItem(HelperMethods.LocalizedText("ITWORX_MOEHEWF_UCE", "ChooseValue", (uint)LCID), "-1"));

            }
        }

        void saveRecommendation()
        {
            using (SCEContextDataContext ctx = new SCEContextDataContext(SPContext.Current.Site.RootWeb.Url))
            {
                int requestID = Convert.ToInt32(Request.QueryString["RequestId"].ToString());

                string folderUrl = DateTime.Now.ToString("yyyy/MM/dd");
                SPList recommendationList = SPContext.Current.Site.RootWeb.Lists.TryGetList("SCESearchStatusRecommendation");
                SPFolder recommendationFolder = ITWORX.MOEHEWF.Common.Utilities.BusinessHelper.CreateFolderInternal(recommendationList, recommendationList.RootFolder, folderUrl);

                var recommendation = new SCESearchStatusRecommendationListFieldsContentType()
                {
                    RequestID = ctx.SCERequests.ScopeToFolder("", true).Where(x => x.Id == requestID).FirstOrDefault(),
                    SearchStatus = txt_SearchStatus.Text,
                    DecisionText = txt_decision.Text,
                    ClassRoom = ddl_ScholasticLevel.SelectedValue != "-1" ? ctx.ScholasticLevel.ScopeToFolder("", true).Where(x => x.Id == Convert.ToInt32(ddl_ScholasticLevel.SelectedValue)).FirstOrDefault() : null,
                    Path = recommendationFolder.Url
                };

                if (ddl_Recommendation.SelectedValue == "2" && ddl_assignRejected.SelectedValue != "-1")
                    recommendation.SendRequestToId = Convert.ToInt32(ddl_assignRejected.SelectedValue);
                else
                    recommendation.SendRequestToId = null;


                ctx.SCESearchStatusRecommendationList.InsertOnSubmit(recommendation);
                ctx.SubmitChanges();
            }
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            Logging.GetInstance().Debug("Entering method PERecommendationUserControl.btnSave_Click");
            try
            {
                txt_decision.Text = hdnTextDecision.Value;
                Page.Validate();
                if (Page.IsValid)
                {
                    if (Request.QueryString["RequestId"] != null)
                    {
                        using (SCEContextDataContext ctx = new SCEContextDataContext(SPContext.Current.Site.RootWeb.Url))
                        {
                            int requestID = Convert.ToInt32(Request.QueryString["RequestId"]);

                            var recommendation = ctx.SCESearchStatusRecommendationList.ScopeToFolder("", true).Where(x => x.RequestID.Id == requestID).LastOrDefault();

                            if (recommendation != null)
                            {
                                recommendation.SearchStatus = txt_SearchStatus.Text;
                                recommendation.DecisionText = txt_decision.Text;
                                recommendation.ClassRoom = ddl_ScholasticLevel.SelectedValue != "-1" ? ctx.ScholasticLevel.ScopeToFolder("", true).Where(x => x.Id == Convert.ToInt32(ddl_ScholasticLevel.SelectedValue)).FirstOrDefault() : null;
                             
                                if (ddl_Recommendation.SelectedValue == "2" && ddl_assignRejected.SelectedValue != "-1")
                                {
                                    recommendation.SendRequestToId = Convert.ToInt32(ddl_assignRejected.SelectedValue);
                                }
                                else
                                {
                                    recommendation.SendRequestToId = null;
                                }
                                ctx.SubmitChanges();
                            }
                            else
                                saveRecommendation();


                            AddFile.SaveAttachments();
                        }
                        savedecisionPDF();
                    }
                }
            }
            catch (Exception ex)
            {
                Logging.GetInstance().LogException(ex);
            }
            finally
            {
                Logging.GetInstance().Debug("Exiting method PERecommendationUserControl.btnSave_Click");
            }
        }

        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            Logging.GetInstance().Debug("Entering method PERecommendationUserControl.btnSubmit_Click");
            try
            {
                txt_decision.Text = hdnTextDecision.Value;
                Page.Validate("RecommendationGroup");
                if (Page.IsValid)
                {
                    if (Request.QueryString["RequestId"] != null)
                    {
                        saveRecommendation();

                        string outcome = string.Empty;
                        if (ddl_Recommendation.SelectedValue == "1")
                            outcome = ITWorx.MOEHEWF.Nintex.Utilities.Constants.SCEAcceptedRecommendation;
                        else if (ddl_Recommendation.SelectedValue == "2")
                        {
                            if (ddl_assignRejected.SelectedValue == "2")
                                outcome = ITWorx.MOEHEWF.Nintex.Utilities.Constants.SCERejectedRecommendationDepartmentManager;
                            else if (ddl_assignRejected.SelectedValue == "1")
                                outcome = ITWorx.MOEHEWF.Nintex.Utilities.Constants.SCERejectedRecommendationSectionManager;
                        }

                        if (!string.IsNullOrEmpty(outcome))
                        {
                            NintexHelper.ContinueTask(outcome, txt_SearchStatus.Text, Utilities.Constants.SCERequests, Request.QueryString["RequestId"].ToString());
                            Common.Utilities.RequestStatus requestStatus;
                            Enum.TryParse(outcome, out requestStatus);
                            Common.BL.HistoricalRecords.AddHistoricalRecords(Utilities.Constants.SCERequests, Utilities.Constants.SCERequestHistory, LCID, (int)requestStatus, SPContext.Current.Web.CurrentUser.Name, string.Empty, Request.QueryString["RequestId"].ToString(), "Yes");
                        }

                        AddFile.SaveAttachments();
                        //Noha
                        using (SCEContextDataContext ctx = new SCEContextDataContext(SPContext.Current.Site.RootWeb.Url))
                        {
                            int requestID = Convert.ToInt32(Request.QueryString["RequestId"].ToString());


                            var req = ctx.SCERequestsList.ScopeToFolder("", true).Where(x => x.Id == requestID).FirstOrDefault();
                            if (ddl_Recommendation.SelectedValue == "1" || (ddl_Recommendation.SelectedValue == "2" && req.RegisteredScholasticLevel == ctx.ScholasticLevel.Where(x => x.Title.ToLower() == "Not available".ToLower()).FirstOrDefault()))
                             {
                                savedecisionPDF();
                            }
                        }

                    }
                }
            }
            catch (Exception ex)
            {
                Logging.GetInstance().LogException(ex);
            }
            finally
            {
                Logging.GetInstance().Debug("Exiting method PERecommendationUserControl.btnSubmit_Click");
                SPUtility.Redirect(SPContext.Current.Web.Url + "/Pages/SCEEquivalenceEmployeesDashboard.aspx", SPRedirectFlags.DoNotEndResponse, HttpContext.Current);
            }
        }

        protected void Custom_ServerValidate(object sender, ServerValidateEventArgs e)
        {
            if (ddl_Recommendation.SelectedValue == "2" && ddl_assignRejected.SelectedValue == "-1")
            {
                e.IsValid = false;
            }
        }

        protected void btn_Preview_Click(object sender, EventArgs e)
        {
            string htmlPath = Server.MapPath("/Certificates/DefaultWithHeader.html");
            string localPath = new Uri(htmlPath).LocalPath;
            string html = File.ReadAllText(htmlPath, UTF8Encoding.UTF8);

            int RequestID = Convert.ToInt32(Request.QueryString["RequestId"].ToString());
            using (SCEContextDataContext ctx = new SCEContextDataContext(SPContext.Current.Site.RootWeb.Url))
            {
                SCERequestsListFieldsContentType currentRequest = ctx.SCERequestsList.ScopeToFolder("", true).Where(a => a.Id == RequestID).SingleOrDefault();
                if (currentRequest != null)
                {
                    requestNumber = currentRequest.RequestNumber;
                    creationDate = currentRequest.CreateDate.Value.ToShortDateString();
                    string generatedImage = SCERequestAttachments.GenerateBarcodeImage(requestNumber);
                   // string barcodeBase = "<img alt='' style='margin:auto;width:30%;' src='data:image/png;base64," + generatedImage + "'" + "/>";

                    string minImage = SCERequestAttachments.GenerateImage(Server.MapPath("/Certificates/MinistryLogo.JPG"));
                    //string minImgTag = "<img alt='ministry logo' style='margin: auto;width: 100%;'  src='data:image/JPG;base64," + minImage + "'" + "/>";

                    string footerImage1 = SCERequestAttachments.GenerateImage(Server.MapPath("/Certificates/footerone.png"));
                    //string footerImgTag= "<img  alt='ministry footer' style='margin:auto; width:100 %;' src='data:image/png;base64," + footerImage + "'" + "/>";

                    string footerImage2 = SCERequestAttachments.GenerateImage(Server.MapPath("/Certificates/footertwo.png"));

                    string footerNoteImage = SCERequestAttachments.GenerateImage(Server.MapPath("/Certificates/footernote.png"));
                    //string footerNoteImgTag = "<img  alt='ministry footer' style='margin: 5px 10%;width:60%;' src='data:image/png;base64," + footerNoteImage + "'" + "/>";



                    string htmlFormatted = string.Format(html, requestNumber, creationDate, txt_decision.Text);
                    htmlFormatted = htmlFormatted.Replace("ministryImg", minImage);
                    htmlFormatted = htmlFormatted.Replace("barCodeImg", generatedImage);
                    htmlFormatted = htmlFormatted.Replace("footerImg1", footerImage1);
                    htmlFormatted = htmlFormatted.Replace("footerImg2", footerImage2);
                    htmlFormatted = htmlFormatted.Replace("footerNoteImg", footerNoteImage);
                    SCERequestAttachments.ConvertHTMLToPDF(htmlFormatted, requestNumber, (RequestStatus)currentRequest.RequestStatus.Id);

                }
            }

           



        }


        private void LoadDecisionTextContro()
        {
            //GetCurrent Request 
            using (SCEContextDataContext ctx = new SCEContextDataContext(SPContext.Current.Site.RootWeb.Url))
            {
                int requestID = Convert.ToInt32(Request.QueryString["RequestId"]);

                SCERequestsListFieldsContentType currentRequest = ctx.SCERequestsList.ScopeToFolder("", true).Where(a => a.Id == requestID).SingleOrDefault();
                if (currentRequest != null)

                {
                    requestNumber = currentRequest.RequestNumber;
                    creationDate = currentRequest.CreateDate.Value.ToShortDateString();
                    int currentgrade = (int)currentRequest.RegisteredScholasticLevel.Id;

                    // select current template 
                    if (ddl_Recommendation.SelectedValue == "1")// Approved
                    {
                        txt_decision.Enabled = true;
                        btn_Preview.Enabled = true;
                        rfv_decision.Enabled = true;
                    }
                    else
                    {
                        if (currentgrade < 14)
                        {
                            txt_decision.Enabled = false;
                            btn_Preview.Enabled = false;
                            rfv_decision.Enabled = true;
                           txt_decision.Text = string.Empty;
                            txt_decision.Enabled = false;

                        }
                        else
                        {
                            txt_decision.Enabled = true;
                            btn_Preview.Enabled = true;
                            rfv_decision.Enabled = true;


                        }
                    }

                    var recommendation = getLastRecommendation();
                    string certTypeAr = string.Empty;
                    string certTypeEn = string.Empty;

                    if (currentRequest.CertificateType != null)
                    {
                        CertificateTypeLookupsCT certifiateType = ctx.CertificateTypeCT.ScopeToFolder("", true).Where(x => x.Id == currentRequest.CertificateType.Id).FirstOrDefault();
                        if (certifiateType != null)
                        {
                            certTypeAr = certifiateType.TitleAr;
                            certTypeEn = certifiateType.Title;
                        }
                    }
                    NationalityCategoryListFieldsContentType nationalityCat = ctx.NationalityCategoryList.ScopeToFolder("", true).Where(y => y.Id == currentRequest.StdNationalityCatId).FirstOrDefault();

                    //NationalityListFieldsContentType nationality = ctx.NationalityList.ScopeToFolder("", true).Where(z => z.Id == ((int)currentRequest.StdNationality.Id)).FirstOrDefault<NationalityListFieldsContentType>();

                    NationalityItem nationalityItem = NationalityList.GetNationalityById(Convert.ToInt32(currentRequest.StdNationality.Id));
                    // NationalityListFieldsContentType nationalityItem = ctx.NationalityList.ScopeToFolder("", true).Where(n=>n.Id == request.StdNationality.Id.Value).FirstOrDefault();
                    string currentNatEn = nationalityItem.Title;
                    string currentNatAr = nationalityItem.TitleAr;
                    string signImg = SCERequestAttachments.GenerateImage(Server.MapPath("/Certificates/signature2.jpg"));
                    string warningImg = SCERequestAttachments.GenerateImage(Server.MapPath("/Certificates/warning.png"));
                    //string warningImgTag = "<img  alt = 'warning' style ='margin: auto;width: auto;float: right;' src='data:image/png;base64," + warningImg + "'" + "/>";

                    if (currentgrade == 14 && ddl_Recommendation.SelectedValue == "1")
                    {

                        //string formattedtext =

                        string htmlPath = Server.MapPath("/Certificates/SecondryApproval.html");
                        string localPath = new Uri(htmlPath).LocalPath;
                        string html = File.ReadAllText(htmlPath, UTF8Encoding.UTF8);



                      
                        string formattedtext = string.Format(html, currentRequest.StdPrintedName, currentRequest.StdQatarID, currentNatAr, currentRequest.PrevSchool, currentNatAr, currentRequest.SubmitDate.ToDate().ToShortDateString(), certTypeAr, "percentage", currentRequest.StdPrintedName, currentRequest.StdQatarID, currentNatEn, currentRequest.PrevSchool, currentNatEn, currentRequest.SubmitDate.ToDate().ToShortDateString(), certTypeEn, "percentage");
                        formattedtext = formattedtext.Replace("signImg", signImg);
                        formattedtext = formattedtext.Replace("warningImg", warningImg);
                        txt_decision.Text = formattedtext;
                    }

                    if (currentgrade == 14 && ddl_Recommendation.SelectedValue == "2")
                    {

                        //string formattedtext =

                        string htmlPath = Server.MapPath("/Certificates/SecondaryRefusal.html");
                        string localPath = new Uri(htmlPath).LocalPath;
                        string html = File.ReadAllText(htmlPath, UTF8Encoding.UTF8);

                        string formattedtext = string.Format(html, currentRequest.StdPrintedName, currentRequest.StdQatarID, currentNatAr, currentRequest.PrevSchool, currentNatAr, currentRequest.SubmitDate.ToDate().ToShortDateString(), certTypeAr, currentRequest.StdPrintedName, currentRequest.StdQatarID, currentNatEn, currentRequest.PrevSchool, currentNatEn, currentRequest.SubmitDate.ToDate().ToShortDateString(), certTypeEn);
                        formattedtext = formattedtext.Replace("signImg", signImg);
                        formattedtext = formattedtext.Replace("warningImg", warningImg);
                        txt_decision.Text = formattedtext; 
                    }

                    else if (currentgrade < 14 && ddl_Recommendation.SelectedValue == "1")
                    {
                        string htmlPath = Server.MapPath("/Certificates/ElementryTemplate.html");
                        string localPath = new Uri(htmlPath).LocalPath;
                        string html = File.ReadAllText(htmlPath, UTF8Encoding.UTF8);
                        CertificateResourceItem certificateResourceItem = CertificateResourceList.GetCertificateResourceById(Convert.ToInt32(currentRequest.CertificateResourceId));

                        string certificateSourceEn = string.Empty;
                        string certificateSourceAr = string.Empty;

                        if (currentRequest.CertificateResourceId != null && !string.IsNullOrEmpty(currentRequest.PrevSchool))
                        {
                           
                            certificateSourceEn = currentRequest.PrevSchool + "/" + certificateResourceItem.Title;
                            certificateSourceAr = currentRequest.PrevSchool + "/" + certificateResourceItem.TitleAr;
                        }
                        else if (currentRequest.CertificateResourceId == null && !string.IsNullOrEmpty(currentRequest.OtherCertificateResource) && !string.IsNullOrEmpty(currentRequest.PrevSchool))
                        {

                            certificateSourceEn = currentRequest.PrevSchool + "/" + currentRequest.OtherCertificateResource;
                            certificateSourceAr = currentRequest.PrevSchool + "/" + currentRequest.OtherCertificateResource;
                        }
                        else if (currentRequest.CertificateResourceId != null && string.IsNullOrEmpty(currentRequest.PrevSchool))
                        {
                            
                            certificateSourceEn = certificateResourceItem.Title;
                            certificateSourceAr = certificateResourceItem.TitleAr;
                        }
                        else if (currentRequest.CertificateResourceId == null && !string.IsNullOrEmpty(currentRequest.OtherCertificateResource) && string.IsNullOrEmpty(currentRequest.PrevSchool))
                        {

                            certificateSourceEn = currentRequest.OtherCertificateResource;
                            certificateSourceAr = currentRequest.OtherCertificateResource;
                        }

                  
                        var lastScholasticLevel= ctx.ScholasticLevelList.ScopeToFolder("", true).Where(a => a.Id == currentRequest.LastScholasticLevel.Id).SingleOrDefault();
                        var regScholasticLevel = ctx.ScholasticLevelList.ScopeToFolder("", true).Where(a => a.Id == currentRequest.RegisteredScholasticLevel.Id).SingleOrDefault();

                        //string signImg = SCERequestAttachments.GenerateImage(Server.MapPath("/Certificates/Signature.jpg"));
                        //string signatureImg = "<img  alt='signature'  src='data:image/jpg;base64," + signImg + "'" + "/>";


                        string formattedtext = string.Format(html, currentRequest.StdPrintedName, currentNatAr, currentRequest.StdQatarID, lastScholasticLevel.TitleAr, currentRequest.LastAcademicYear, certificateSourceAr, regScholasticLevel.TitleAr, HelperMethods.LocalizedText("ITWORX_MOEHEWF_SCE", "ProgramManagerSignature", (uint)LCID));//, currentRequest.StdPrintedName, currentNatEn, currentRequest.StdQatarID, lastScholasticLevel.Title, currentRequest.LastAcademicYear, certificateSourceEn, regScholasticLevel.Title);
                        formattedtext = formattedtext.Replace("signImg", signImg);
                        formattedtext = formattedtext.Replace("warningImg", warningImg);
                        txt_decision.Text = formattedtext; // StripHTML(formattedtext);
                    }


                }
            }
        }

        private void savedecisionPDF()
        {
            string htmlPath = Server.MapPath("/Certificates/DefaultWithHeader.html");
            string localPath = new Uri(htmlPath).LocalPath;
            string html = File.ReadAllText(htmlPath, UTF8Encoding.UTF8);

            int RequestID = Convert.ToInt32(Request.QueryString["RequestId"].ToString());
            MemoryStream currentOutPut = new MemoryStream();
            using (SCEContextDataContext ctx = new SCEContextDataContext(SPContext.Current.Site.RootWeb.Url))
            {
                SCERequestsListFieldsContentType currentRequest = ctx.SCERequestsList.ScopeToFolder("", true).Where(a => a.Id == RequestID).SingleOrDefault();
                if (currentRequest != null)
                {
                    string generatedImage = SCERequestAttachments.GenerateBarcodeImage(requestNumber);
                    // string barcodeBase = "<img alt='' style='margin:auto;width:30%;' src='data:image/png;base64," + generatedImage + "'" + "/>";

                    string minImage = SCERequestAttachments.GenerateImage(Server.MapPath("/Certificates/MinistryLogo.JPG"));
                    //string minImgTag = "<img alt='ministry logo' style='margin: auto;width: 100%;'  src='data:image/JPG;base64," + minImage + "'" + "/>";

                    string footerImage1 = SCERequestAttachments.GenerateImage(Server.MapPath("/Certificates/footerone.png"));
                    //string footerImgTag= "<img  alt='ministry footer' style='margin:auto; width:100 %;' src='data:image/png;base64," + footerImage + "'" + "/>";

                    string footerImage2 = SCERequestAttachments.GenerateImage(Server.MapPath("/Certificates/footertwo.png"));

                    string footerNoteImage = SCERequestAttachments.GenerateImage(Server.MapPath("/Certificates/footernote.png"));
                    //string footerNoteImgTag = "<img  alt='ministry footer' style='margin: 5px 10%;width:60%;' src='data:image/png;base64," + footerNoteImage + "'" + "/>";



                    string htmlFormatted = string.Format(html, requestNumber, creationDate, txt_decision.Text);
                    htmlFormatted = htmlFormatted.Replace("ministryImg", minImage);
                    htmlFormatted = htmlFormatted.Replace("barCodeImg", generatedImage);
                    htmlFormatted = htmlFormatted.Replace("footerImg1", footerImage1);
                    htmlFormatted = htmlFormatted.Replace("footerImg2", footerImage2);
                    htmlFormatted = htmlFormatted.Replace("footerNoteImg", footerNoteImage);

                    currentOutPut = SCERequestAttachments.GetPDFFile(htmlFormatted, currentRequest.RequestNumber, (RequestStatus)currentRequest.RequestStatus.Id);

                }
            }





            SPSecurity.RunWithElevatedPrivileges(() =>
            {
                using (SPSite site = new SPSite(SPContext.Current.Site.RootWeb.Url))
                {
                    using (SPWeb web = site.RootWeb)
                    {
                        SPList list = web.Lists["SCERequests"];
                        SPListItemCollection collListItems = null;
                        var camelQuery = Common.Utilities.BusinessHelper.GetQueryObject("<Where><Eq><FieldRef Name ='ID'/><Value Type = 'Number'>" +
                           RequestID.ToString() + "</Value></Eq></Where>");

                        collListItems = list.GetItems(camelQuery);
                        if (collListItems != null)
                        {
                            SPListItem item = collListItems[0];
                            if (item.Attachments.Count > 0)
                            {
                                item.Attachments.Delete("FinalDecision.pdf");
                            }
                            item.Attachments.Add("FinalDecision.pdf", currentOutPut.ToArray());
                            item.Update();

                        }


                    }
                }

            }

            );


        }


        protected void ddl_Recommendation_SelectedIndexChanged(object sender, EventArgs e)
        {
            LoadDecisionTextContro();
        }
    }
}
