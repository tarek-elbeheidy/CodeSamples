using ITWORX.MOEHE.Utilities;
using ITWORX.MOEHE.Utilities.Logging;
using ITWORX.MOEHEWF.Common.Utilities;
using Microsoft.SharePoint;
using Microsoft.SharePoint.Utilities;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Mail;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;

namespace ITWORX.MOEHEWF.Common.CONTROLTEMPLATES.ITWORX.MOEHEWF.Common
{
    public partial class ClientSideFileUpload : UserControlBase
    {
       
   
        private string title { set; get; }
        public string Title
        {
            get
            {
                if (!string.IsNullOrEmpty(Convert.ToString(ViewState["Title"])))
                {
                    title = ViewState["Title"].ToString();
                }
                return title;
            }
            set
            {
                title = value;
                lblFileName.Text = value;
                ViewState["Title"] = value;
            }
        }

        public string UploadedFilesIDS
        {
            get
            {
                return hdn_UploadedFileIDs.Value;
            }
        }
        public string TitleAr { set; get; }
        public AttachmentStatus Choice { set; get; }
        private string documentLibraryName { set; get; }
        public string DocumentLibraryName
        {
            get
            {
                if (!string.IsNullOrEmpty(Convert.ToString(ViewState["DocumentLibraryName"])))
                {
                    documentLibraryName = ViewState["DocumentLibraryName"].ToString();
                }
                return documentLibraryName;
            }
            set
            {
                documentLibraryName = value;
                hdnDocLibrary.Value = value;
                ViewState["DocumentLibraryName"] = value;
            }
        }
        private string docLibWebUrl { set; get; }
        public string DocLibWebUrl
        {
            get
            {
                if (!string.IsNullOrEmpty(Convert.ToString(ViewState["DocLibWebUrl"])))
                {
                    docLibWebUrl = ViewState["DocLibWebUrl"].ToString();
                }
                return docLibWebUrl;
            }
            set
            {
                docLibWebUrl = value;
                hdnDocLibWebUrl.Value = value;
                ViewState["DocLibWebUrl"] = value;
            }
        }

        private string lookupDocumentLibraryName { set; get; }
        public string LookupDocumentLibraryName
        {
            get
            {
                if (!string.IsNullOrEmpty(Convert.ToString(ViewState["LookupDocumentLibraryName"])))
                {
                    lookupDocumentLibraryName = ViewState["LookupDocumentLibraryName"].ToString();
                }
                return lookupDocumentLibraryName;
            }
            set
            {
                lookupDocumentLibraryName = value;
                hdnLookupDocLibrary.Value = value;
                ViewState["LookupDocumentLibraryName"] = value;
            }
        }
        private int maxFileNumber { set; get; }
        public int MaxFileNumber
        {
            get
            {

                if (/*!string.IsNullOrEmpty(Convert.ToString(ViewState["MaxFileNumber"]))*/
                    !string.IsNullOrEmpty(hdnMaxFileNo.Value))
                {
                    maxFileNumber = int.Parse(hdnMaxFileNo.Value);//ViewState["MaxFileNumber"].ToString());
                }
                return maxFileNumber;
            }
            set
            {
                maxFileNumber = value;
                hdnMaxFileNo.Value = value.ToString();
                //ViewState["MaxFileNumber"] = value;
            }
        }

        private string group { set; get; }
        public string Group
        {
            get
            {
                if (!string.IsNullOrEmpty(hdnGrp.Value) /*!string.IsNullOrEmpty(Convert.ToString(ViewState["Group"]))*/)
                {
                    group = hdnGrp.Value;// ViewState["Group"].ToString();
                }
                return group;
            }
            set
            {
                group = value;
                hdnGrp.Value = value;
                //ViewState["Group"] = value;
            }
        }

        private string lookupFieldName { set; get; }
        public string LookupFieldName
        {
            get
            {
                if (!string.IsNullOrEmpty(Convert.ToString(ViewState["LookupFieldName"])))
                {
                    lookupFieldName = ViewState["LookupFieldName"].ToString();
                }
                return lookupFieldName;
            }
            set
            {
                lookupFieldName = value;
                hdnLookupFieldName.Value = value;
                ViewState["LookupFieldName"] = value;

            }
        }


        private bool hasOptions;
        public bool HasOptions
        {
            get
            {
                if (!string.IsNullOrEmpty(Convert.ToString(ViewState["HasOptions"])))
                {

                    hasOptions = bool.Parse(ViewState["HasOptions"].ToString());
                }
                return hasOptions;
            }
            set
            {
                hasOptions = value;
                hdnHasOptions.Value = value.ToString();
                ViewState["HasOptions"] = value;
            }
        }
        private string labelRequiredDrop;
        public string LabelRequiredDrop
        {
            get
            {
                if (!string.IsNullOrEmpty(Convert.ToString(ViewState["LabelRequiredDrop"])))
                {
                    labelRequiredDrop = ViewState["LabelRequiredDrop"].ToString();
                }
                return labelRequiredDrop;
            }
            set
            {
                labelRequiredDrop = value;
                ViewState["LabelRequiredDrop"] = value;
            }
        }
        private bool isRequired { set; get; }
        public bool IsRequired
        {
            get
            {
                if (!string.IsNullOrEmpty(Convert.ToString(ViewState["IsRequired"])))
                {

                    isRequired = bool.Parse(ViewState["IsRequired"].ToString());
                }
                return isRequired;
            }
            set
            {
                isRequired = value;
                if (!hasOptions)
                {
                    lblFileUpload.Visible = value;

                }

                hdnRequired.Value = value.ToString();
                custRequiredFile.Enabled = value;
                ViewState["IsRequired"] = value;
            }
        }
        private string requiredValidationMessage { set; get; }
        public string RequiredValidationMessage
        {
            get
            {
                if (!string.IsNullOrEmpty(Convert.ToString(ViewState["RequiredValidationMessage"])))
                {
                    requiredValidationMessage = ViewState["RequiredValidationMessage"].ToString();
                }
                return requiredValidationMessage;
            }
            set
            {
                requiredValidationMessage = value;
                custRequiredFile.ErrorMessage = value;
                ViewState["RequiredValidationMessage"] = value;
            }
        }
        private int maxSize { set; get; }
        public int MaxSize
        {
            get
            {
                if (!string.IsNullOrEmpty(Convert.ToString(ViewState["MaxSize"])))
                {
                    maxSize = int.Parse(ViewState["MaxSize"].ToString());
                }
                return maxSize;
            }
            set
            {
                maxSize = value;
                hdnMaxSize.Value = value.ToString();
                ViewState["MaxSize"] = value;
            }
        }
        private string supportedExtensions { set; get; }
        public string SupportedExtensions
        {
            get
            {
                if (!string.IsNullOrEmpty(Convert.ToString(ViewState["SupportedExtensions"])))
                {
                    supportedExtensions = ViewState["SupportedExtensions"].ToString();
                }
                return supportedExtensions;
            }
            set
            {
                supportedExtensions = value;
                regFileExt.ValidationExpression = GetFileExtensions(value);
                supportedExtensions = value;
                ViewState["SupportedExtensions"] = value;
            }
        }
        private int lookupFieldValue { set; get; }
        public int LookupFieldValue
        {
            get
            {
                if (!string.IsNullOrEmpty(Convert.ToString(ViewState["LookupFieldValue"])))
                {
                    lookupFieldValue = int.Parse(ViewState["LookupFieldValue"].ToString());
                }
                return lookupFieldValue;
            }
            set
            {
                lookupFieldValue = value;
                hdnLookupFieldValue.Value = value.ToString();
                ViewState["LookupFieldValue"] = value;
            }
        }
        private string fileExtensionValidation { set; get; }
        public string FileExtensionValidation
        {
            get
            {
                if (!string.IsNullOrEmpty(Convert.ToString(ViewState["FileExtensionValidation"])))
                {
                    fileExtensionValidation = ViewState["FileExtensionValidation"].ToString();
                }
                return fileExtensionValidation;
            }
            set
            {
                fileExtensionValidation = value;
                regFileExt.ErrorMessage = value;
                ViewState["FileExtensionValidation"] = value;
            }
        }
        private string fileSizeValidationMsg { set; get; }
        public string FileSizeValidationMsg
        {
            get
            {
                if (!string.IsNullOrEmpty(Convert.ToString(ViewState["FileSizeValidationMsg"])))
                {
                    fileSizeValidationMsg = ViewState["FileSizeValidationMsg"].ToString();
                }
                return fileSizeValidationMsg;
            }
            set
            {
                fileSizeValidationMsg = value;
                ViewState["FileSizeValidationMsg"] = value;
                //custSize.ErrorMessage = value;
            }
            //   / (1000 * 1024)
        }
        private string fileNumbersValidationMsg { set; get; }
        public string FileNumbersValidationMsg
        {
            get
            {
                if (!string.IsNullOrEmpty(Convert.ToString(ViewState["FileNumbersValidationMsg"])))
                {
                    fileNumbersValidationMsg = ViewState["FileNumbersValidationMsg"].ToString();
                }
                return fileNumbersValidationMsg;
            }
            set
            {
                fileNumbersValidationMsg = value;
                ViewState["FileNumbersValidationMsg"] = value;
                //custFileNumbers.ErrorMessage = value;
            }
        }
        private string fileExistsValidationMsg { set; get; }
        public string FileExistsValidationMsg
        {
            get
            {
                if (!string.IsNullOrEmpty(Convert.ToString(ViewState["FileExistsValidationMsg"])))
                {
                    fileExistsValidationMsg = ViewState["FileExistsValidationMsg"].ToString();
                }
                return fileExistsValidationMsg;
            }
            set
            {
                fileExistsValidationMsg = value;
                ViewState["FileExistsValidationMsg"] = value;
                //custFileExists.ErrorMessage = value;
            }
        }

        private string dropClientID;
        public string DropClientID
        {
            get
            {
                if (!string.IsNullOrEmpty(Convert.ToString(ViewState["DropClientID"])))
                {
                    dropClientID = ViewState["DropClientID"].ToString();
                }
                return dropClientID;
            }
            set
            {
                dropClientID = value;
                ViewState["DropClientID"] = value;
            }
        }

        private string textBoxClientID;
        public string TextBoxClientID
        {
            get
            {
                if (!string.IsNullOrEmpty(Convert.ToString(ViewState["TextBoxClientID"])))
                {

                    textBoxClientID = ViewState["TextBoxClientID"].ToString();
                }
                return textBoxClientID;
            }
            set
            {
                textBoxClientID = value;
                ViewState["TextBoxClientID"] = value;
            }
        }


        private string lblTextBoxClientID;
        public string LblTextBoxClientID
        {
            get
            {
                if (!string.IsNullOrEmpty(Convert.ToString(ViewState["LBblTextBoxClientID"])))
                {

                    lblTextBoxClientID = ViewState["LBblTextBoxClientID"].ToString();
                }
                return lblTextBoxClientID;
            }
            set
            {
                lblTextBoxClientID = value;
                ViewState["LBblTextBoxClientID"] = value;
            }
        }
        private string reqDropClientID;
        public string ReqDropClientID
        {
            get
            {
                if (!string.IsNullOrEmpty(Convert.ToString(ViewState["ReqDropClientID"])))
                {

                    reqDropClientID = ViewState["ReqDropClientID"].ToString();
                }
                return reqDropClientID;
            }
            set
            {
                reqDropClientID = value;
                ViewState["ReqDropClientID"] = value;
            }
        }

        private string validqationGroup;
        public string ValidqationGroup
        {
            get
            {
                if (!string.IsNullOrEmpty(Convert.ToString(ViewState["ValidqationGroup"])))
                {

                    validqationGroup = ViewState["ValidqationGroup"].ToString();
                }
                return validqationGroup;
            }
            set
            {
                validqationGroup = value;
                custRequiredFile.ValidationGroup = value;
                ViewState["ValidqationGroup"] = value;
            }
        }

        private bool isDownloadable;
        public bool IsDownloadable
        {
            get
            {
                if (!string.IsNullOrEmpty(Convert.ToString(ViewState["IsDownloadable"])))
                {

                    isDownloadable = bool.Parse(ViewState["IsDownloadable"].ToString());
                }
                return isDownloadable;
            }
            set
            {
                isDownloadable = value;
                ViewState["IsDownloadable"] = value;

            }
        }
        private string confirmationDeleteMsg;
        public string ConfirmationDeleteMsg
        {
            get
            {
                if (!string.IsNullOrEmpty(Convert.ToString(ViewState["ConfirmationDeleteMsg"])))
                {

                    confirmationDeleteMsg = ViewState["ConfirmationDeleteMsg"].ToString();
                }
                return confirmationDeleteMsg;
            }
            set
            {
                confirmationDeleteMsg = value;
                ViewState["ConfirmationDeleteMsg"] = value;
            }
        }

        private bool displayMode;
        public bool DisplayMode
        {
            get
            {
                if (!string.IsNullOrEmpty(Convert.ToString(ViewState["DisplayMode"])))
                {
                    displayMode = bool.Parse(ViewState["DisplayMode"].ToString());
                }
                return displayMode;
            }
            set
            {
                displayMode = value;
                fileUpload.Visible = value;
                btnUpload.Visible = value;
                hdnDisplayMode.Value = value.ToString();
                lblChoose.Visible = value;
                ViewState["DisplayMode"] = value;
            }

        }
        public string LocalizedText(string resourceKey)
        {
            return HelperMethods.LocalizedText("ITWORX.MOEHEWF.Common", resourceKey, (uint)System.Threading.Thread.CurrentThread.CurrentUICulture.LCID);
        }

        protected override void OnInit(EventArgs e)
        {


            IsDownloadable = false;
            //lnkbtnView.OnClientClick = "window.document.forms[0].target='_blank'; setTimeout(function(){window.document.forms[0].target='';}, 500);setFormSubmitToFalse()";

            //set default values for validation msg here
            //if (string.IsNullOrEmpty(RequiredValidationMessage))
            //{
            //    RequiredValidationMessage = LocalizedText("FU_FileEmptyMessage");
            //}



            if (string.IsNullOrEmpty(FileNumbersValidationMsg))
            {
                FileNumbersValidationMsg = LocalizedText("FU_MaxFileNumberErrorMessage");
            }

            if (string.IsNullOrEmpty(ConfirmationDeleteMsg))
            {
                ConfirmationDeleteMsg = LocalizedText("FU_ConfirmDeleteMessage");
            }

            if (string.IsNullOrEmpty(FileExistsValidationMsg))
            {
                FileExistsValidationMsg = LocalizedText("FU_FileExistsMessage");
            }

            if (string.IsNullOrEmpty(RequiredValidationMessage))
            {
                RequiredValidationMessage = string.Format(LocalizedText("FU_RequiredValidationMessage"),
                  lblFileName);
            }
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            //if (string.IsNullOrEmpty(FileSizeValidationMsg))
            //{
            //    float size = (float)MaxSize / (1000 * 1024);
            //    FileSizeValidationMsg = string.Format(LocalizedText("FU_FileSizeMessage"),Math.Round( size));
            //}
        }

        public string GetFileExtensions(string fileExt)
        {
            Logging.GetInstance().Debug("Entering method  ClientSideFileUpload.GetFileExtensions");
            string fileExtensions = string.Empty;
            try
            {
                //^.+(.doc|.DOC|.docx|.DOCX|.pdf|.PDF)$
                fileExtensions = "^.+(";

                if (!string.IsNullOrEmpty(fileExt) && fileExt.Contains(","))
                {
                    var allSupportedExt = fileExt.Split(',');
                    for (int i = 0; i < allSupportedExt.Length; i++)
                    {
                        fileExtensions += "." + allSupportedExt[i].ToLower() + "|." + allSupportedExt[i].ToUpper() + "|";
                    }

                }
                fileExtensions = fileExtensions.TrimEnd('|');
                fileExtensions += ")$";



            }
            catch (Exception ex)
            {
                Logging.GetInstance().LogException(ex);
            }
            finally
            {

                Logging.GetInstance().Debug("Exiting method  ClientSideFileUpload.GetFileExtensions");
            }
            return fileExtensions;
        }

        protected void custRequiredFile_ServerValidate(object source, ServerValidateEventArgs args)
        {
            if (MaxFileNumber != 0 && int.Parse(hdnUploadCount.Value) < MaxFileNumber)
            {
                args.IsValid = false;
            }
            else
            {
                args.IsValid = true;
            }



        }
        protected void lnkbtnView_Click(object sender, EventArgs e)
        {
            Logging.GetInstance().Debug("Entering method  ClientSideFileUpload.lnkbtnView_Click");
            try
            {
                LinkButton lnkFile = sender as LinkButton;
                string[] commandArgument = lnkFile.CommandArgument.Split(';');
                DisplayedFile displayFile = new DisplayedFile()
                {
                    //DocLibWebUrl = this.DocLibWebUrl,
                    //ItemID = int.Parse(commandArgument[0]),
                    //DocumentLibraryName = this.DocumentLibraryName,
                    //DownloadableName = commandArgument[1],
                    //IsDownloadable = false

                    DocLibWebUrl = DocLibWebUrl,
                    ItemID = 96,//85,//int.Parse(commandArgument[0]),
                    DocumentLibraryName = DocumentLibraryName,
                    DownloadableName = "General",// commandArgument[1],
                    IsDownloadable = IsDownloadable
                };

                Session["DisplayFile"] = displayFile;
                SPUtility.Redirect("/_layouts/15/ITWORX.MOEHEWF.Common/DisplayFile.aspx", SPRedirectFlags.DoNotEndResponse, System.Web.HttpContext.Current);
            }
            catch (Exception ex)
            {
                Logging.GetInstance().LogException(ex);
            }
            finally
            {

                Logging.GetInstance().Debug("Exiting method  ClientSideFileUpload.lnkbtnView_Click");
            }
        }

        public List<Attachment> SaveAttachments(int id = 0, List<string> que = null)
        {
            Logging.GetInstance().Debug("Entering method ClientSideFileUpload.SaveAttachments");
            SPWeb web = null;

            List<Attachment> attachments = new List<Attachment>();
            try
            {

                SPSecurity.RunWithElevatedPrivileges(delegate ()
                {
                    using (SPSite site = new SPSite(DocLibWebUrl))
                    {
                        using (web = site.OpenWeb())
                        {
                            SPList list = web.Lists.TryGetList(DocumentLibraryName);
                            if (list != null)
                            {
                                web.AllowUnsafeUpdates = true;
                                string query = string.Empty;
                                if (que == null)
                                {
                                    query = @"<Where><And><Eq><FieldRef Name='" + LookupFieldName + "'/><Value Type='Lookup'>" + LookupFieldValue + @"</Value></Eq>
                                            <And><Eq><FieldRef Name='MOEHEDocumentGroup' /><Value Type='Text'>" + Group + @"</Value></Eq>
                                             <Or><Eq><FieldRef Name='MOEHEDocumentStatus' /><Value Type='Choice'>Uploaded</Value></Eq>
                                             <Eq><FieldRef Name='MOEHEDocumentStatus' /><Value Type='Choice'>Deleted</Value>
                                             </Eq></Or></And></And></Where>";
                                }
                                else
                                {
                                    query = BusinessHelper.CreateCAMLQuery(que, "Or", true);
                                }

                                SPQuery sPQuery = new SPQuery();
                                sPQuery.Query = query;
                                var items = list.GetItems(sPQuery);
                                foreach (SPListItem item in items)
                                {
                                    if (Convert.ToString(item["MOEHEDocumentStatus"]) == AttachmentStatus.Uploaded.ToString())
                                    {
                                        if (id != 0)
                                        {
                                            item[LookupFieldName] = new SPFieldLookupValue(id, id.ToString());
                                        }
                                        item["MOEHEDocumentStatus"] = AttachmentStatus.Saved;
                                        item.SystemUpdate();
                                        SPFile spfile = item.File;
                                        if (spfile.CheckOutType != SPFile.SPCheckOutType.None)
                                            spfile.CheckIn("File saved Successfully");

                                        Stream ms = new MemoryStream(item.File.OpenBinary());
                                        attachments.Add(new Attachment(ms, spfile.Title + "." + item["File_x0020_Type"].ToString()));

                                      
                                    }
                                    else
                                    {

                                        list.Items.DeleteItemById(item.ID);

                                        // this gives error Collection was modified; enumeration operation may not execute.
                                        // item.Delete();

                                    }

                                }
                            }
                        }
                    }
                });
            }

            catch (Exception ex)
            {
                Logging.GetInstance().LogException(ex);
            }
            finally
            {
                if
                    (web != null)
                {
                    web.AllowUnsafeUpdates = false;
                }
                Logging.GetInstance().Debug("Exiting method ClientSideFileUpload.SaveAttachments");
            }
            return attachments;


        }
        public void DeleteAttachments()
        {
            Logging.GetInstance().Debug("Entering method ClientSideFileUpload.DeleteAttachments");
            SPWeb web = null;

            try
            {

                SPSecurity.RunWithElevatedPrivileges(delegate ()
                {
                    using (SPSite site = new SPSite(DocLibWebUrl))
                    {
                        using (web = site.OpenWeb())
                        {
                            SPList list = web.Lists.TryGetList(DocumentLibraryName);
                            if (list != null)
                            {
                                web.AllowUnsafeUpdates = true;
                               //string query = @"<Where><And><Eq><FieldRef Name='" + LookupFieldName + "'/><Value Type='Lookup'>" + LookupFieldValue + @"</Value></Eq>
                               //             <And><Neq><FieldRef Name='MOEHEDocumentGroup' /><Value Type='Text'>" + Group + @"</Value></Neq>
                               //              <Or><Eq><FieldRef Name='MOEHEDocumentStatus' /><Value Type='Choice'>Uploaded</Value></Eq>
                               //              <Eq><FieldRef Name='MOEHEDocumentStatus' /><Value Type='Choice'>Saved</Value>
                               //              </Eq></Or></And></And></Where>";


                               //string query = @"<Where><And><Eq><FieldRef Name='" + LookupFieldName + "' /><Value Type='Lookup'>"+ LookupFieldValue +
                               // "</Value></Eq><Neq><FieldRef Name='MOEHEDocumentGroup' /><Value Type='Text'>" + Group+ "</Value></Neq></And></Where>";

                                string query = @"<Where><And><Eq><FieldRef Name='" + LookupFieldName + "'/><Value Type='Lookup'>"+ LookupFieldValue +
                                "</Value></Eq><And><Neq><FieldRef Name='MOEHEDocumentGroup' /><Value Type='Text'>" + Group+
                                "</Value></Neq><Neq><FieldRef Name='MOEHEDocumentGroup' /><Value Type='Text'>"+ Group+"Other" + "</Value></Neq></And></And></Where>";
                              
                                SPQuery sPQuery = new SPQuery();
                                sPQuery.Query = query;
                                var items = list.GetItems(sPQuery);
                                foreach (SPListItem item in items)
                                {
                                    list.Items.DeleteItemById(item.ID);
                                    //item["MOEHEDocumentStatus"] = AttachmentStatus.Deleted;
                                    //item.SystemUpdate();

                                }
                            }
                        }
                    }
                });

            }
            catch (Exception ex)
            {
                Logging.GetInstance().LogException(ex);
            }
            finally
            {
                if
                    (web != null)
                {
                    web.AllowUnsafeUpdates = false;
                }
                Logging.GetInstance().Debug("Exiting method ClientSideFileUpload.SaveAttachments");
            }


        }


     
    }
}
