using Microsoft.SharePoint;
using Microsoft.SharePoint.Utilities;
using System;
using System.Collections.Generic;
using System.IO;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Linq;
using ITWORX.MOEHE.Utilities;
using ITWORX.MOEHE.Utilities.Logging;
using ITWORX.MOEHEWF.Common.Utilities;
using System.Web.Services;
using System.Text;

namespace ITWORX.MOEHEWF.Common.CONTROLTEMPLATES.ITWORX.MOEHEWF.Common
{
    public partial class FileUpload : UserControlBase
    {
        #region Protected Variables
        //protected global::ITWORX.MOEHEWF.Common.CONTROLTEMPLATES.ITWORX.MOEHEWF.Common.DropdownWithTextbox ddlAttachmentCategory;
        #endregion
        #region Public Properties

        /// <summary>
        /// Gets or sets the request ID of the file.
        /// </summary>
        /// <value>
        /// The file request ID.
        /// </value>
        /// 
        public Boolean AfterDawnLoad { get; set; }

        public int RequestID
        {
            get
            {
                int requestID = 0;
                if (!string.IsNullOrEmpty(Convert.ToString(ViewState["RequestID"])))
                {
                    requestID = Convert.ToInt32(ViewState["RequestID"]);
                }

                return requestID;
            }
            set
            {
                ViewState["RequestID"] = value;
            }
        }

        /// <summary>
        /// Gets or sets the group name of the file.
        /// </summary>
        /// <value>
        /// The file group name .
        /// </value>
        public string Group
        {
            get
            {
                var group = string.Empty;
                if (!string.IsNullOrEmpty(Convert.ToString(Session[string.Format("{0}_{1}", "Group", this.ClientID)])))
                {
                    group = Session[string.Format("{0}_{1}", "Group", this.ClientID)].ToString();
                }

                return group;
            }
            set
            {
                Session[string.Format("{0}_{1}", "Group", this.ClientID)] = value;
            }
        }

        public string LookupFieldName
        {
            get
            {
                var lookupName = string.Empty;
                if (!string.IsNullOrEmpty(Convert.ToString(Session[string.Format("{0}_{1}", "LookupFieldName", this.ClientID)])))
                {
                    lookupName = Session[string.Format("{0}_{1}", "LookupFieldName", this.ClientID)].ToString();
                }
                else
                {
                    lookupName = "RequestID";
                }

                return lookupName;
            }
            set
            {
                Session[string.Format("{0}_{1}", "LookupFieldName", this.ClientID)] = value;
            }
        }


        /// <summary>
        /// Gets or sets the name of the file.
        /// </summary>
        /// <value>
        /// The file  name .
        /// </value>
        //public string FileName
        //{
        //    get
        //    {
        //        var fileName = string.Empty;
        //        if (!string.IsNullOrEmpty(Convert.ToString(Session[string.Format("{0}_{1}", "FileName", this.ClientID)])))
        //        {
        //            fileName = Session[string.Format("{0}_{1}", "FileName", this.ClientID)].ToString();
        //        }

        //        return fileName;
        //    }
        //    set
        //    {
        //        Session[string.Format("{0}_{1}", "Group", this.ClientID)] = value;
        //    }
        //}
        /// <summary>
        /// Gets or sets the document library name.
        /// </summary>
        /// <value>
        /// The document library name.
        /// </value>
        public string DocumentLibraryName
        {
            get
            {
                var documentLibraryName = string.Empty;
                if (!string.IsNullOrEmpty(Convert.ToString(ViewState["DocumentLibraryName"])))
                {
                    documentLibraryName = ViewState["DocumentLibraryName"].ToString();
                }

                return documentLibraryName;
            }
            set
            {
                ViewState["DocumentLibraryName"] = value;
            }
        }

        /// <summary>
        /// Gets or sets the document library web Url.
        /// </summary>
        /// <value>
        /// The document library web Url.
        /// </value>
        public string DocLibWebUrl
        {
            get
            {
                var docLibWebUrl = string.Empty;
                if (!string.IsNullOrEmpty(Convert.ToString(ViewState["DocLibWebUrl"])))
                {
                    docLibWebUrl = ViewState["DocLibWebUrl"].ToString();
                }

                return docLibWebUrl;
            }
            set
            {
                ViewState["DocLibWebUrl"] = value;
            }
        }

        /// <summary>
        /// Gets or sets the document template ID.
        /// </summary>
        /// <value>
        /// The document template ID.
        /// </value>
        public int TemplateID
        {
            get
            {
                int templateID = 0;
                if (!string.IsNullOrEmpty(Convert.ToString(ViewState["TemplateID"])))
                {
                    templateID = Convert.ToInt32(ViewState["TemplateID"]);
                }

                return templateID;
            }
            set
            {
                ViewState["TemplateID"] = value;
            }
        }

        /// <summary>
        /// Gets or sets the name of the lookup field of TemplateID.
        /// </summary>
        /// <value>
        /// The name of the lookup field of TemplateID.
        /// </value>
        public string TemplateIDInternalFieldName
        {
            get;
            set;
        }

        private object attachmentCategoryList;
        /// <summary>
        /// Gets or sets the attachment categories.
        /// </summary>
        /// <value>
        /// The attachment categories.
        /// </value>
        public object AttachmentCategories
        {
            get
            {
                return attachmentCategoryList;
            }
            set
            {
                attachmentCategoryList = value;
            }
        }

        /// <summary>
        /// Gets or sets the attachment categories dropdownlist data value field, ex: ID.
        /// </summary>
        /// <value>
        /// The attachment categories categories dropdownlist data value field, ex: ID.
        /// </value>
        public string AttachmentCategoryDataValueField
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the attachment categories dropdownlist data English text field, ex: EnglishTitle.
        /// </summary>
        /// <value>
        /// The attachment categories categories dropdownlist data English text field, ex: EnglishTitle.
        /// </value>
        public string AttachmentCategoryDataTextEnField
        {
            get;
            set;
        }
        /// <summary>
        /// Gets or sets the attachment categories dropdownlist data Arabic text field, ex: ArabicTitle.
        /// </summary>
        /// <value>
        /// The attachment categories categories dropdownlist data Arabic text field, ex: ArabicTitle.
        /// </value>
        public string AttachmentCategoryDataTextArField
        {
            get;
            set;
        }
        /// <summary>
        /// Gets  the attachments.
        /// </summary>
        /// <value>
        /// The attachments.
        /// </value>
        public List<FileUploaded> Attachments { get; set; } = new List<FileUploaded>();


        /// <summary>
        /// Gets or sets the attachments.
        /// </summary>
        /// <value>
        /// The attachments.
        /// </value>
        public int AttachmentsCount
        {
            get
            {
                int attachmentCount = 0;
                if (!string.IsNullOrEmpty(Convert.ToString(Session[string.Format("{0}_{1}", "AttachmentsCount", this.ClientID)])))
                {
                    attachmentCount = int.Parse(Session[string.Format("{0}_{1}", "AttachmentsCount", this.ClientID)].ToString());
                }

                return attachmentCount;
            }
            set
            {
                Session[string.Format("{0}_{1}", "AttachmentsCount", this.ClientID)] = value;
            }
        }

        /// <summary>
        /// Gets or sets the header label.
        /// </summary>
        /// <value>
        /// The header label.
        /// </value>
        public string HeaderLabel { get { return lblHeader.Text; } set { lblHeader.Text = value; } }

        /// <summary>
        /// Gets or sets the required validation message.
        /// </summary>
        /// <value>
        /// The required validation message.
        /// </value>
        public string RequiredValidationMessage
        {
            get { return cvFileRequired.ErrorMessage; }
            set { cvFileRequired.ErrorMessage = value; }
        }

        /// <summary>
        /// Gets or sets the required validation message.
        /// </summary>
        /// <value>
        /// The required validation message.
        /// </value>
        //public string AttachmentRequiredValidationMessage
        //{
        //    get { return rfvAttachmentCategory.ErrorMessage; }
        //    set { rfvAttachmentCategory.ErrorMessage = value; }
        //    //get { return ddlAttachmentCategory.ErrorMessage; }
        //    //set { ddlAttachmentCategory.ErrorMessage = value; }
        //    //get; set;
        //}
        /// <summary>
        ///   Sets the supported extensions. (coma separated)
        /// </summary>
        /// <value> The supported extensions. </value>
        public string SupportedExtensions
        {
            set
            {
                revFileExtension.ValidationExpression = GetExtensionsRegeExpression(value);
                if (string.IsNullOrEmpty(FileExtenionValidationMessage))
                {
                    FileExtenionValidationMessage = string.Format(LocalizedText("FU_FileExtenionValidationMessage"),
                                                                  value);
                }
            }
        }

        /// <summary>
        /// Gets or sets the required field flag.
        /// </summary>
        /// <value>
        /// The required field flag.
        /// </value>
        public string RequiredFieldFlag
        {
            get
            {
                //    if (divAttachmentCategory.Visible)
                //    {
                //        return lblRequiredCatrgoryflag.Text;
                //    }
                //    else
                //    {
                return lblRequiredflag.Text;
                //   }
            }
            set
            {
                //if (divAttachmentCategory.Visible)
                //{
                //    lblRequiredCatrgoryflag.Text = value;
                //}
                //else
                //{
                lblRequiredflag.Text = value;
                //  }
            }
        }

        /// <summary>
        /// Gets or sets the delete image URL.
        /// </summary>
        /// <value>
        /// The delete image URL.
        /// </value>
        public string DeleteImageUrl
        {
            get;
            set;
        }


        /// <summary>
        /// Gets or sets the validation group.
        /// </summary>
        /// <value>
        /// The validation group.
        /// </value>
        public string ValidationGroup
        {
            get { return cvFileRequired.ValidationGroup; }
            set
            {
                //btnUpload.ValidationGroup = value;
                cvFileRequired.ValidationGroup = value;
                //ddlAttachmentCategory.ValidationGroup = value;
                //revFileExtension.ValidationGroup = value;
                //cvFileSize.ValidationGroup = value;
                //cvFileExists.ValidationGroup = value;
            }
        }

        /// <summary>
        /// Gets or sets the file empty message.
        /// </summary>
        /// <value>
        /// The file empty message.
        /// </value>
        public string FileEmptyMessage
        {
            get
            {
                var fileEmptyMessage = string.Empty;
                if (!string.IsNullOrEmpty(Convert.ToString(ViewState["FileEmptyMessage"])))
                {
                    fileEmptyMessage = ViewState["FileEmptyMessage"].ToString();
                }

                return fileEmptyMessage;
            }

            set
            {
                ViewState["FileEmptyMessage"] = value;
            }
        }


        /// <summary>
        /// Gets or sets a value indicating whether [enabled].
        /// </summary>
        /// <value>
        ///   <c>true</c> if [enabled]; otherwise, <c>false</c>.
        /// </value>
        public bool Enabled
        {
            get
            {
                bool enabled = false;
                if (!string.IsNullOrEmpty(Convert.ToString(Session[string.Format("{0}_{1}", "Enabled", this.ClientID)])))
                {
                    enabled = bool.Parse(Session[string.Format("{0}_{1}", "Enabled", this.ClientID)].ToString());
                }

                return enabled;
            }

            set
            {
                Session[string.Format("{0}_{1}", "Enabled", this.ClientID)] = value;
                EnableControl(value);
            }
        }

        /// <summary>
        /// Gets or sets the file size message.
        /// </summary>
        /// <value>
        /// The file size message.
        /// </value>
        public string FileSizeMessage
        {
            get
            {
                var fileSizeMessage = string.Empty;
                if (!string.IsNullOrEmpty(Convert.ToString(ViewState["FileSizeMessage"])))
                {
                    fileSizeMessage = Convert.ToString(ViewState["FileSizeMessage"]);
                }

                return fileSizeMessage;
            }
            set
            {
                ViewState["FileSizeMessage"] = value;
            }
        }

        /// <summary>
        /// Gets or sets the file exist ID.
        /// </summary>
        /// <value>
        /// The file exist ID.
        /// </value>
        public int FileExistID
        {
            get
            {
                int fileExistID = 0;
                if (Convert.ToInt32(ViewState["FileExistID"]) != 0)
                {
                    fileExistID = Convert.ToInt32(ViewState["FileExistID"]);
                }

                return fileExistID;
            }
            set
            {
                ViewState["FileExistID"] = value;
            }
        }

        /// <summary>
        /// Gets or sets the maximum file number.
        /// </summary>
        /// <value>
        /// The maximum file number.
        /// </value>
        public int MaxFileNumber
        {
            get
            {
                int maxFileNumber = 0;
                if (!string.IsNullOrEmpty(Convert.ToString(Session[string.Format("{0}_{1}", "MaxFileNumber", this.ClientID)])))
                {
                    maxFileNumber = int.Parse(Session[string.Format("{0}_{1}", "MaxFileNumber", this.ClientID)].ToString());
                }

                return maxFileNumber;
            }
            set
            {
                Session[string.Format("{0}_{1}", "MaxFileNumber", this.ClientID)] = value;
            }
        }

        /// <summary>
        /// Gets or sets the maximum file number error message.
        /// </summary>
        /// <value>
        /// The maximum file number error message.
        /// </value>
        public string MaxFileNumberErrorMessage
        {
            get
            {
                var maxFileNumberErrorMessage = string.Empty;
                if (!string.IsNullOrEmpty(Convert.ToString(ViewState["MaxFileNumberErrorMessage"])))
                {
                    maxFileNumberErrorMessage = Convert.ToString(ViewState["MaxFileNumberErrorMessage"]);
                }

                return maxFileNumberErrorMessage;
            }
            set
            {
                ViewState["MaxFileNumberErrorMessage"] = value;
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether [is required].
        /// </summary>
        /// <value>
        ///   <c>true</c> if [is required]; otherwise, <c>false</c>.
        /// </value>
        public bool IsRequired
        {
            get
            {
                var isRequired = false;
                if (Session[string.Format("{0}_{1}", "IsRequired", this.ClientID)] != null)
                {
                    isRequired = Convert.ToBoolean(Session[string.Format("{0}_{1}", "IsRequired", this.ClientID)]);
                }

                return isRequired;
            }
            set
            {
                Session[string.Format("{0}_{1}", "IsRequired", this.ClientID)] = value;
                lblRequiredflag.Visible = value;
            }
        }



        /// <summary>
        /// Gets or sets the display name of the attachment category label.
        /// </summary>
        /// <value>
        /// The display name of the attachment category label.
        /// </value>
        //   public string LabelAttachmentCategory { get { return lblAttachmentCategory.Text; } set { lblAttachmentCategory.Text = value; } }

        /// <summary>
        /// Gets or sets the display name of the attachment name header label in the repeater.
        /// </summary>
        /// <value>
        /// The display name of the attachment name header label in the repeater.
        /// </value>
        //public string LabelAttachmentNameHeader { get { return lblAttachmentNameHeader.Text; } set { lblAttachmentNameHeader.Text = value; } }

        /// <summary>
        /// Gets or sets the display name of the modified date header label in the repeater.
        /// </summary>
        /// <value>
        /// The display name of the modified date header label in the repeater.
        /// </value>
        //public string LabelModifiedHeader { get { return lblModifiedHeader.Text; } set { lblModifiedHeader.Text = value; } }

        /// <summary>
        /// Gets or sets the display name of the action header label in the repeater.
        /// </summary>
        /// <value>
        /// The display name of the action header label in the repeater.
        /// </value>
        //public string LabelActionHeader { get { return lblAction.Text; } set { lblAction.Text = value; } }

        /// <summary>
        /// Gets or sets the display name of the label.
        /// </summary>
        /// <value>
        /// The display name of the label.
        /// </value>

        public string LabelDisplayName { get { return lblUploadLabel.Text; } set { lblUploadLabel.Text = value; } }

        /// <summary>
        /// Gets or sets the display name of the upload button.
        /// </summary>
        /// <value>
        /// The display name of the upload button.
        /// </value>
        public string UploadButtonDisplayName { get { return btnUpload.Text; } set { btnUpload.Text = value; } }

        /// <summary>
        /// Gets or sets the no files message.
        /// </summary>
        /// <value>
        /// The display no files message.
        /// </value>
        public string LabelNoFilesUploaded { get { return lblNoFiles.Text; } set { lblNoFiles.Text = value; } }

        /// <summary>
        /// Gets or sets the confirm delete message.
        /// </summary>
        /// <value>
        /// The confirm delete message.
        /// </value>
        public string ConfirmDeleteMessage
        {
            get
            {
                var confirmDeleteMessage = string.Empty;
                if (!string.IsNullOrEmpty(Convert.ToString(ViewState["ConfirmDeleteMessage"])))
                {
                    confirmDeleteMessage = Convert.ToString(ViewState["ConfirmDeleteMessage"]);
                }

                return confirmDeleteMessage;
            }
            set
            {
                ViewState["ConfirmDeleteMessage"] = value;
            }
        }

        /// <summary>
        /// Gets or sets the confirm replace file message.
        /// </summary>
        /// <value>
        /// The confirm replace file message.
        /// </value>
        public string FileExistsMessage
        {
            get
            {
                var fileExistsMessage = string.Empty;
                if (!string.IsNullOrEmpty(Convert.ToString(ViewState["FileExistsMessage"])))
                {
                    fileExistsMessage = Convert.ToString(ViewState["FileExistsMessage"]);
                }

                return fileExistsMessage;
            }
            set
            {
                ViewState["FileExistsMessage"] = value;
            }
        }

        /// <summary>
        /// Gets or sets the maximum size.
        /// </summary>
        /// <value>
        /// The maximum size.
        /// </value>
        public int MaxSize
        {
            get
            {
                var maxSize = 0;
                if (!string.IsNullOrEmpty(Convert.ToString(Session[string.Format("{0}_{1}", "MaxSize", this.ClientID)])))
                {
                    maxSize = int.Parse(Session[string.Format("{0}_{1}", "MaxSize", this.ClientID)].ToString());
                }

                return maxSize;
            }

            set
            {
                Session[string.Format("{0}_{1}", "MaxSize", this.ClientID)] = value;
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether [has template].
        /// </summary>
        /// <value>
        ///   <c>true</c> if [has template]; otherwise, <c>false</c>.
        /// </value>
        public bool HasTemplate
        {
            get { return hylnkDownloadTemplate.Visible; }
            set
            {
                hylnkDownloadTemplate.Visible = value;
                divUploadTemplate.Visible = value;
            }
        }

        /// <summary>
        /// Gets or sets the template URL.
        /// </summary>
        /// <value>
        /// The template URL.
        /// </value>
        public string TemplateUrl
        {
            get { return hylnkDownloadTemplate.NavigateUrl; }
            set { hylnkDownloadTemplate.NavigateUrl = value; }
        }

        /// <summary>
        ///   Gets or sets the file extenion validation message.
        /// </summary>
        /// <value> The file extenion validation message. </value>
        public string FileExtenionValidationMessage
        {
            get { return revFileExtension.ErrorMessage; }
            set { revFileExtension.ErrorMessage = value; }
        }

        /// <summary>
        /// Gets or sets the download icon CSS class.
        /// </summary>
        /// <value>
        /// The download icon CSS class.
        /// </value>
        public string DownloadIconCssClass
        {
            get { return hylnkDownloadTemplate.CssClass; }
            set
            {
                hylnkDownloadTemplate.CssClass = value;
            }
        }

        /// <summary>
        /// Gets or sets the fancy box CSS class.
        /// </summary>
        /// <value>
        /// The fancy box CSS class.
        /// </value>
        public string FancyBoxCssClass
        {
            get
            {
                var fancyBoxCssClass = string.Empty;
                if (!string.IsNullOrEmpty(Convert.ToString(ViewState["FancyBoxCssClass"])))
                {
                    fancyBoxCssClass = Convert.ToString(ViewState["FancyBoxCssClass"]);
                }

                return fancyBoxCssClass;
            }
            set
            {
                ViewState["FancyBoxCssClass"] = value;
            }
        }

        /// <summary>
        /// Gets or sets the document icon CSS class.
        /// </summary>
        /// <value>
        /// The document icon CSS class.
        /// </value>
        public string DocumentIconCssClass
        {
            get
            {
                var documentIconCssClass = string.Empty;
                if (!string.IsNullOrEmpty(Convert.ToString(ViewState["DocumentIconCssClass"])))
                {
                    documentIconCssClass = Convert.ToString(ViewState["DocumentIconCssClass"]);
                }

                return documentIconCssClass;
            }
            set
            {
                ViewState["DocumentIconCssClass"] = value;
            }
        }


        /// <summary>
        /// Gets or sets the name of the template (will be displayed as download link tooltip)
        /// </summary>
        /// <value>
        /// The name of the template.
        /// </value>
        public string TemplateName
        {
            get
            {
                return hylnkDownloadTemplate.ToolTip;
            }
            set
            {
                hylnkDownloadTemplate.ToolTip = value;
                lblTemplateName.Text = value;
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether [view template name]. (default value = false)
        /// </summary>
        /// <value>
        ///   <c>true</c> if [view template name]; otherwise, <c>false</c>.
        /// </value>
        public bool ViewTemplateName { get { return lblTemplateName.Visible; } set { lblTemplateName.Visible = value; } }

        /// <summary>
        /// Gets or sets the uploaded repeater CSS class.
        /// </summary>
        /// <value>
        /// The uploaded repeater CSS class.
        /// </value>        
        public string UploadedRepeaterCssClass
        {
            get
            {
                var uploadedRepeaterCssClass = string.Empty;
                if (!string.IsNullOrEmpty(Convert.ToString(ViewState["UploadedRepeaterCssClass"])))
                {
                    uploadedRepeaterCssClass = Convert.ToString(ViewState["UploadedRepeaterCssClass"]);
                }

                return uploadedRepeaterCssClass;
            }
            set
            {
                ViewState["UploadedRepeaterCssClass"] = value;
            }
        }

        //public string SelectedValue
        //{
        //    get
        //    {
        //        return ddlAttachmentCategory.SelectedValue;
        //    }
        //    set
        //    {
        //        ddlAttachmentCategory.SelectedValue = value;

        //    }
        //}
        //public string SelectedText
        //{
        //    get
        //    {
        //        return ddlAttachmentCategory.SelectedItem.Text;
        //    }
        //    set
        //    {
        //        ddlAttachmentCategory.SelectedItem.Text = value;

        //    }
        //}
        public string AttachmentCategorySelectedText { set; get; }
        #endregion

        #region Events

        /// <summary>
        /// Handles the Load event of the Page control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        protected void Page_Load(object sender, EventArgs e)
        {
            lblCustomErrorMessage.Text = string.Empty;

            if (string.IsNullOrEmpty(LabelDisplayName))
            {
                divUploadLabel.Visible = false;
            }
            else
            {
                divUploadLabel.Visible = true;
            }
        }

        /// <summary>
        /// Raises the <see cref="E:System.Web.UI.Control.Init" /> event.
        /// </summary>
        /// <param name="e">An <see cref="T:System.EventArgs" /> object that contains the event data.</param>
        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);
            #region Set Local Validators
            var validationGroup = this.ClientID;

            //  rfvAttachmentCategory.ValidationGroup = validationGroup;
            revFileExtension.ValidationGroup = validationGroup;
            cvFileSize.ValidationGroup = validationGroup;
            cvFileExists.ValidationGroup = validationGroup;
            btnUpload.ValidationGroup = validationGroup;
            #endregion
            //if (string.IsNullOrEmpty(LabelAttachmentNameHeader))
            //{
            //    LabelAttachmentNameHeader = LocalizedText("FU_lblAttachmentNameHeader");
            //}
            //if (string.IsNullOrEmpty(LabelModifiedHeader))
            //{
            //    LabelModifiedHeader = LocalizedText("FU_lblModifiedHeader");
            //}

            //if (string.IsNullOrEmpty(LabelActionHeader))
            //{
            //    LabelActionHeader = LocalizedText("FU_lblActionHeader");
            //}

            if (string.IsNullOrEmpty(RequiredFieldFlag))
            {
                RequiredFieldFlag = "*";
            }

            if (string.IsNullOrEmpty(DownloadIconCssClass))
            {
                DownloadIconCssClass = "downloadIcon";
            }

            if (string.IsNullOrEmpty(FancyBoxCssClass))
            {
                FancyBoxCssClass = "FancyBox";
            }

            if (string.IsNullOrEmpty(DocumentIconCssClass))
            {
                DocumentIconCssClass = "docIcon";
            }

            if (string.IsNullOrEmpty(UploadedRepeaterCssClass))
            {
                UploadedRepeaterCssClass = "uploadedReapeater";
            }

            if (string.IsNullOrEmpty(FileEmptyMessage))
            {
                FileEmptyMessage = LocalizedText("FU_FileEmptyMessage");
            }

            if (string.IsNullOrEmpty(FileSizeMessage))
            {
                FileSizeMessage = LocalizedText("FU_FileSizeMessage");
            }

            if (string.IsNullOrEmpty(MaxFileNumberErrorMessage))
            {
                MaxFileNumberErrorMessage = LocalizedText("FU_MaxFileNumberErrorMessage");
            }

            if (string.IsNullOrEmpty(ConfirmDeleteMessage))
            {
                ConfirmDeleteMessage = LocalizedText("FU_ConfirmDeleteMessage");
            }

            if (string.IsNullOrEmpty(FileExistsMessage))
            {
                FileExistsMessage = LocalizedText("FU_FileExistsMessage");
            }

            //if (string.IsNullOrEmpty(LabelAttachmentCategory))
            //{
            //    LabelAttachmentCategory = LocalizedText("FU_lblAttachmentNameHeader");
            //}

            if (string.IsNullOrEmpty(RequiredValidationMessage))
            {
                RequiredValidationMessage = string.Format(LocalizedText("FU_RequiredValidationMessage"),
                                                          LabelDisplayName);
            }

            //if (string.IsNullOrEmpty(AttachmentRequiredValidationMessage))
            //{
            //    AttachmentRequiredValidationMessage = string.Format(LocalizedText("FU_AttachmentRequiredValidationMessage"),
            //                                              LabelAttachmentCategory);
            //}

            if (string.IsNullOrEmpty(UploadButtonDisplayName))
            {
                btnUpload.Text = LocalizedText("FU_Upload");
            }

            if (string.IsNullOrEmpty(LabelNoFilesUploaded))
            {
                lblNoFiles.Text = LocalizedText("FU_NoFilesUploaded");
            }

            upfFile.Attributes["onChange"] = "javascript:ValidateValue(this,'" + btnUpload.ClientID + "');";
        }

        /// <summary>
        /// Handles the Click event of the btnUpload control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        protected void btnUpload_Click(object sender, EventArgs e)
        {
            try
            {
                Logging.GetInstance().Debug("Entering method FileUpload.btnUpload_Click");
                
                    
                if (!IsRefresh || AfterDawnLoad)
                {


                    if (MaxFileNumber != 0)
                    {
                        if (AttachmentsCount == MaxFileNumber)
                        {
                            lblCustomErrorMessage.Visible = true;
                            lblCustomErrorMessage.Text = string.Format(MaxFileNumberErrorMessage, MaxFileNumber);
                            return;
                        }

                        lblCustomErrorMessage.Text = string.Empty;
                        lblCustomErrorMessage.Visible = false;
                    }

                    if (upfFile.HasFile || (!upfFile.HasFile && !string.IsNullOrEmpty(upfFile.FileName)))
                    {
                        cvFileSize_ServerValidate(cvFileSize, new ServerValidateEventArgs(cvFileSize.ClientID, false));
                        cvFileExists_ServerValidate(cvFileExists, new ServerValidateEventArgs(cvFileExists.ClientID, false));
                        if (Page.IsValid && cvFileSize.IsValid && cvFileExists.IsValid)
                        {
                            FileUploaded file = new FileUploaded
                            {
                                RequestID = RequestID,
                                NameGuid = Guid.NewGuid().ToString(),
                                Title = (!string.IsNullOrEmpty(AttachmentCategorySelectedText) ? AttachmentCategorySelectedText + Path.GetExtension(upfFile.FileName) : upfFile.FileName),
                                MIMEType = upfFile.PostedFile.ContentType,
                                FileStatus = AttachmentStatus.Uploaded,
                                FileBytes = upfFile.FileBytes,
                                Group = Group,
                                TemplateID = (HasTemplate && TemplateID != 0) ? this.TemplateID : 0
                            };
                            UploadAttachment(file, 0);
                            GetAttachments();
                            SetUploadedAttachmentsLinks();
 
                        }
                        Page.Session.Remove("FileCategory");
                    }
                }
            }
            catch (Exception ex)
            {
                Logging.GetInstance().LogException(ex);
            }
            finally
            {
                Logging.GetInstance().Debug("Exiting method FileUpload.btnUpload_Click");
            }
        }
        /// <summary>
        /// Handles the ServerValidate event of the cvFileRequired control.
        /// </summary>
        /// <param name="source">The source of the event.</param>
        /// <param name="args">The <see cref="ServerValidateEventArgs"/> instance containing the event data.</param>
        protected void cvFileRequired_ServerValidate(object source, ServerValidateEventArgs args)
        {
            try
            {
                Logging.GetInstance().Debug("Entering method FileUpload.cvFileRequired_ServerValidate");
                if (IsRequired)
                {
                    if (AttachmentsCount > 0)
                    {
                        cvFileRequired.IsValid = true;
                        args.IsValid = true;
                    }
                    else
                    {
                        cvFileRequired.ErrorMessage = RequiredValidationMessage;
                        cvFileRequired.Visible = true;
                        cvFileRequired.IsValid = false;
                        args.IsValid = false;
                    }
                }
                else
                {
                    args.IsValid = true;
                }
            }
            catch (Exception ex)
            {
                Logging.GetInstance().LogException(ex);
            }
            finally
            {
                Logging.GetInstance().Debug("Exiting method FileUpload.cvFileRequired_ServerValidate");
            }
        }


        /// <summary>
        /// Handles the ServerValidate event of the cvFileSize control.
        /// </summary>
        /// <param name="source">The source of the event.</param>
        /// <param name="args">The <see cref="ServerValidateEventArgs"/> instance containing the event data.</param>
        protected void cvFileSize_ServerValidate(object source, ServerValidateEventArgs args)
        {
            try
            {
                Logging.GetInstance().Debug("Entering method FileUpload.cvFileSize_ServerValidate");
                if (cvFileSize != null)
                {
                    cvFileSize.Visible = false;

                    if (upfFile.HasFile)
                    {
                        if (upfFile.PostedFile.ContentLength == 0)
                        {
                            cvFileSize.ErrorMessage = FileEmptyMessage;
                            cvFileSize.Visible = true;
                            cvFileSize.IsValid = false;
                            args.IsValid = false;
                        }
                        else if (upfFile.PostedFile.ContentLength > MaxSize)
                        {
                            cvFileSize.ErrorMessage = string.Format(
                                FileSizeMessage, (MaxSize / (1000 * 1024)).ToString());
                            cvFileSize.Visible = true;
                            cvFileSize.IsValid = false;
                            args.IsValid = false;
                        }
                        else
                        {
                            args.IsValid = true;
                            cvFileSize.IsValid = true;
                        }
                    }
                    else if (!upfFile.HasFile && !string.IsNullOrEmpty(upfFile.FileName))
                    {
                        cvFileSize.ErrorMessage = FileEmptyMessage;
                        cvFileSize.Visible = true;
                        cvFileSize.IsValid = false;
                        args.IsValid = false;
                    }
                }
            }
            catch (Exception ex)
            {
                Logging.GetInstance().LogException(ex);
            }
            finally
            {
                Logging.GetInstance().Debug("Exiting method FileUpload.cvFileSize_ServerValidate");
            }
        }
        /// <summary>
        /// Handles the ServerValidate event of the cvFileExists control.
        /// </summary>
        /// <param name="source">The source of the event.</param>
        /// <param name="args">The <see cref="ServerValidateEventArgs"/> instance containing the event data.</param>
        protected void cvFileExists_ServerValidate(object source, ServerValidateEventArgs args)
        {
            try
            {
                Logging.GetInstance().Debug("Entering method FileUpload.cvFileExists_ServerValidate");
                if (cvFileExists != null)
                {
                    cvFileExists.Visible = false;

                    if (upfFile.HasFile)
                    {
                        //string fileName = ((!string.IsNullOrEmpty(FileName)) ? FileName + Path.GetExtension(upfFile.FileName) : upfFile.FileName);
                        //  string fileName = ((ddlAttachmentCategory.Visible) ? ddlAttachmentCategory.SelectedItem.Text + Path.GetExtension(upfFile.FileName) : upfFile.FileName);
                        string fileName = (!string.IsNullOrEmpty(AttachmentCategorySelectedText) ? AttachmentCategorySelectedText + Path.GetExtension(upfFile.FileName) : upfFile.FileName);// (Page.Session["FileCategory"] != null ? Page.Session["FileCategory"].ToString() + Path.GetExtension(upfFile.FileName) : upfFile.FileName);
                        if (FileExists(fileName))
                        {
                            cvFileExists.ErrorMessage = FileExistsMessage;
                            cvFileExists.Visible = true;
                            cvFileExists.IsValid = false;
                            args.IsValid = false;
                        }
                        else
                        {
                            args.IsValid = true;
                            cvFileExists.IsValid = true;
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
                Logging.GetInstance().Debug("Exiting method FileUpload.cvFileExists_ServerValidate");
            }
        }

        /// <summary>
        /// Handles the Click event of the lnbTemplate control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        protected void lnbTemplate_Click(object sender, EventArgs e)
        {
            try
            {
                Logging.GetInstance().Debug("Entering method FileUpload.lnbTemplate_Click");
                SPSecurity.RunWithElevatedPrivileges(
                    () =>
                    {
                        byte[] fileBytes = new System.Net.WebClient().DownloadData(TemplateUrl);
                        var context = HttpContext.Current;
                        context.Response.Buffer = false;
                        // Clear all content output from the buffer stream 
                        context.Response.Clear();
                        // Add a HTTP header to the output stream that specifies the default filename 
                        // for the browser's download dialog 
                        var fileName = Path.GetFileName(TemplateUrl);

                        context.Response.AddHeader("Content-Disposition", "attachment; filename=" + fileName);
                        // Add a HTTP header to the output stream that contains the 
                        // content length(File Size). This lets the browser know how much data is being transfered 
                        context.Response.AddHeader("Content-Length", fileBytes.Length.ToString());
                        context.Response.AddHeader("X-Download-Options", "noopen");
                        // Set the HTTP MIME type of the output stream 
                        //context.Response.ContentType = fileContentType;
                        // Write the data out to the client. 
                        context.Response.BinaryWrite(fileBytes);
                        context.Response.Flush();
                        context.Response.End();
                    });
            }
            catch (Exception ex)
            {
                Logging.GetInstance().LogException(ex);
            }
            finally
            {
                Logging.GetInstance().Debug("Exiting method FileUpload.lnbTemplate_Click");
            }
        }

        /// <summary>
        /// Handles the Click event of the lnkFile control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        protected void lnkFile_Click(object sender, EventArgs e)
        {
            try
            {
                Logging.GetInstance().Debug("Entering method FileUpload.lnkFile_Click");
                LinkButton lnkFile = sender as LinkButton;
                string[] commandArgument = lnkFile.CommandArgument.Split(';');
                DisplayedFile displayFile = new DisplayedFile()
                {
                    DocLibWebUrl = this.DocLibWebUrl,
                    ItemID = int.Parse(commandArgument[0]),
                    DocumentLibraryName = this.DocumentLibraryName,
                    DownloadableName = commandArgument[1],
                    IsDownloadable = false
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
                Logging.GetInstance().Debug("Exiting method FileUpload.lnkFile_Click");
            }
        }

        /// <summary>
        /// Handles the Click event of the btnDelete control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="ImageClickEventArgs"/> instance containing the event data.</param>
        protected void btnDelete_Click(object sender, ImageClickEventArgs e)
        {
            try
            {
                Logging.GetInstance().Debug("Entering method FileUpload.btnDelete_Click");
                ImageButton deleteBtn = (ImageButton)sender;
                DeleteAttachmentByID(int.Parse(deleteBtn.CommandArgument));
                lblCustomErrorMessage.Text = string.Empty;
                lblCustomErrorMessage.Visible = false;

                GetAttachments();
                SetUploadedAttachmentsLinks();
            }
            catch (Exception ex)
            {
                Logging.GetInstance().LogException(ex);
            }
            finally
            {
                Logging.GetInstance().Debug("Exiting method FileUpload.btnDelete_Click");
            }
        }
        #endregion

        #region Methods

        /// <summary>
        /// Enables the control.
        /// </summary>
        /// <param name="enable">if set to <c>true</c> [enable].</param>
        private void EnableControl(bool enable)
        {
            // upfFile.Visible = enable;
            if (IsRequired)
            {
                lblRequiredflag.Visible = enable;
            }
            //  lblRequiredCatrgoryflag.Visible = enable;
            //rfvFile.Visible = enable;
            //btnUpload.Visible = enable;

            // cvFileSize.Visible = enable;
            // cvFileExists.Visible = enable;
            // revFileExtension.Visible = enable;

            //hylnkDownloadTemplate.Visible = enable;
            divUploadControl.Visible = enable;

            divUploadButton.Visible = enable;
            divUploadValidations.Visible = enable;
            if (HasTemplate)
            {
                divUploadTemplate.Visible = enable;
            }
            //if (attachmentCategoryList != null)
            //{
            //    divAttachmentCategory.Visible = enable;
            //}
            //else
            //{
            //    divAttachmentCategory.Visible = false;
            //}
        }

        /// <summary>
        /// Gets the extensions rege expression.
        /// </summary>
        /// <param name="supportedExtensions">The supported extensions.</param>
        /// <returns></returns>
        public string GetExtensionsRegeExpression(string supportedExtensions)
        {
            var regExp = "^.+(";
            try
            {
                Logging.GetInstance().Debug("Entering method FileUpload.GetExtensionsRegeExpression");
                if (!supportedExtensions.Contains(","))
                {
                    regExp = string.Format(
                        "{0}.{1}|.{2})$", regExp, supportedExtensions.Trim().ToLower(),
                        supportedExtensions.Trim().ToUpper());
                }
                else
                {
                    //^.+(.doc|.DOC|.docx|.DOCX|.pdf|.PDF)$
                    var extensions = supportedExtensions.Split(',');
                    for (int i = 0; i < extensions.Length; i++)
                    {
                        var extension = extensions[i];
                        if (i == extensions.Length - 1)
                        {
                            regExp = string.Format("{0}.{1}|", regExp, extension.Trim().ToLower());
                            regExp = string.Format("{0}.{1}", regExp, extension.Trim().ToUpper());
                        }
                        else
                        {
                            regExp = string.Format("{0}.{1}|", regExp, extension.Trim().ToLower());
                            regExp = string.Format("{0}.{1}|", regExp, extension.Trim().ToUpper());
                        }
                    }

                    regExp = regExp + ")$";

                }
            }
            catch (Exception ex)
            {
                Logging.GetInstance().LogException(ex);
            }
            finally
            {
                Logging.GetInstance().Debug("Exiting method FileUpload.GetExtensionsRegeExpression");
            }

            return regExp;
        }

        /// <summary>
        /// Upload attachments to dcument library.
        /// </summary>
        /// <param name="file">File to upload in the document library.</param>
        /// <param name="ItemID">ItemID if it is 0, then add new item, else update the existing one.</param>
        private void UploadAttachment(FileUploaded file, int ItemID)
        {
            Logging.GetInstance().Debug("Entering method FileUpload.UploadAttachment");
            SPWeb web = null;
            SPListItem fileListItem = null;
            SPFile spfile = null;
            try
            {
                SPSecurity.RunWithElevatedPrivileges(() =>
                {
                    using (SPSite site = new SPSite(DocLibWebUrl))
                    {
                        using (web = site.OpenWeb())
                        {
                            SPFolder library = web.Lists.TryGetList(DocumentLibraryName).RootFolder;
                            web.AllowUnsafeUpdates = true;
                            if (ItemID == 0)
                            {
                                spfile = library.Files.Add(file.NameGuid, file.FileBytes);
                                fileListItem = spfile.Item;
                                fileListItem["Title"] = file.Title;
                                fileListItem[LookupFieldName] = file.RequestID;
                                fileListItem["DocumentStatus"] = file.FileStatus.ToString();
                                fileListItem["Group"] = file.Group;
                                fileListItem["File_x0020_Type"] = file.MIMEType;
                                if (HasTemplate && TemplateID != 0 && !string.IsNullOrEmpty(TemplateIDInternalFieldName))
                                {
                                    fileListItem[TemplateIDInternalFieldName.Trim()] = TemplateID;
                                }
                            }
                            else
                            {
                                SPList list = web.Lists[DocumentLibraryName];
                                fileListItem = list.GetItemById(ItemID);
                                if (fileListItem != null)
                                {
                                    fileListItem["Title"] = file.Title;
                                    fileListItem[LookupFieldName] = file.RequestID;
                                    fileListItem["DocumentStatus"] = file.FileStatus.ToString();
                                    fileListItem["Group"] = file.Group;
                                    fileListItem["File_x0020_Type"] = file.MIMEType;
                                    spfile = fileListItem.File;
                                    if (HasTemplate && TemplateID != 0 && !string.IsNullOrEmpty(TemplateIDInternalFieldName))
                                    {
                                        fileListItem[TemplateIDInternalFieldName.Trim()] = TemplateID;
                                    }
                                }
                            }
                            fileListItem.SystemUpdate();
                            if (spfile.CheckOutType != SPFile.SPCheckOutType.None)
                                spfile.CheckIn("File uploaded Successfully");
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
                web.AllowUnsafeUpdates = false;
                Logging.GetInstance().Debug("Exiting method FileUpload.UploadAttachment");
            }
        }
        public void DeleteAttachmentByID(int ID)
        {
            Logging.GetInstance().Debug("Entering method FileUpload.DeleteAttachmentByID");
            SPWeb web = null;
            try
            {
                SPSecurity.RunWithElevatedPrivileges(() =>
                {
                    using (SPSite site = new SPSite(DocLibWebUrl.Trim()))
                    {
                        using (web = site.OpenWeb())
                        {
                            SPList library = web.Lists.TryGetList(DocumentLibraryName.Trim());
                            web.AllowUnsafeUpdates = true;
                            SPListItem fileListItem = library.GetItemById(ID);
                            if (fileListItem != null)
                            {
                                fileListItem["DocumentStatus"] = AttachmentStatus.Deleted.ToString();
                                SPFile spfile = fileListItem.File;
                                fileListItem.SystemUpdate();
                                if (spfile.CheckOutType != SPFile.SPCheckOutType.None)
                                    spfile.CheckIn("Document Status updated by deleted Successfully");
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
                web.AllowUnsafeUpdates = false;
                Logging.GetInstance().Debug("Exiting method FileUpload.DeleteAttachmentByID");
            }
        }

        private void SetUploadedAttachmentsLinks()
        {
            Logging.GetInstance().Debug("Entering method FileUpload.SetUploadedAttachmentsLinks");
            HtmlGenericControl div = null;
            try
            {
                pnlUploadedDocuments.Controls.Clear();

                if (AttachmentsCount > 0)
                {
                    cvFileRequired.Enabled = false;
                }
                else if (IsRequired)
                {
                    cvFileRequired.Enabled = true;
                }
                else
                {
                    cvFileRequired.Enabled = false;
                }
                divUploadedRepeater.Visible = (AttachmentsCount > 0);
                foreach (FileUploaded attachment in Attachments)
                {
                    div = new HtmlGenericControl();

                    HtmlGenericControl spanFile = new HtmlGenericControl("SPAN");

                    Label lblFile = new Label();
                    lblFile.CssClass = DocumentIconCssClass;
                    lblFile.Text = Path.GetFileNameWithoutExtension(attachment.Title);
                    SetLinkFileCSSClass(lblFile, attachment.Title);
                    lblFile.Visible = true;
                    spanFile.Controls.Add(lblFile);
                    div.Controls.Add(spanFile);

                    //HtmlGenericControl spanModified = new HtmlGenericControl("SPAN");

                    //Label lblModified = new Label();
                    //               lblModified.Text = attachment.Modified.QatarFormatedDate();
                    //spanModified.Controls.Add(lblModified);
                    //pnlUploadedDocuments.Controls.Add(spanModified);


                    //HtmlGenericControl spanModifiedBy = new HtmlGenericControl("SPAN");

                    //Label lblModifiedBy = new Label();
                    //lblModified.Text = attachment.ModifiedBy;
                    //spanModifiedBy.Controls.Add(lblModifiedBy);
                    //pnlUploadedDocuments.Controls.Add(spanModifiedBy);



                    HtmlGenericControl spanView = new HtmlGenericControl("SPAN");

                    LinkButton lnkFile = new LinkButton();
                    lnkFile.OnClientClick = "window.document.forms[0].target='_blank'; setTimeout(function(){window.document.forms[0].target='';}, 500);setFormSubmitToFalse()";
                    lnkFile.CommandArgument = attachment.ID.ToString() + ";" + attachment.Title;
                    lnkFile.Click += new EventHandler(lnkFile_Click);


                    spanView.Controls.Add(lnkFile);
                    div.Controls.Add(spanView);


                    HtmlGenericControl spanDelete = new HtmlGenericControl("SPAN");
                    ImageButton btn = new ImageButton();
                    btn.CommandArgument = attachment.ID.ToString();
                    btn.CommandName = "DeleteFile";
                    btn.ImageUrl = DeleteImageUrl;
                    btn.Attributes.Add("onclick", string.Format("return confirm('{0}');", ConfirmDeleteMessage));
                    btn.Visible = Enabled;
                    btn.CausesValidation = false;
                    btn.ID = "btnDelete" + attachment.ID.ToString();
                    btn.Click += btnDelete_Click;

                    spanDelete.Controls.Add(btn);

                    div.Controls.Add(spanDelete);
                    pnlUploadedDocuments.Controls.Add(div);

                }
            }
            catch (Exception ex)
            {
                Logging.GetInstance().LogException(ex);
            }
            finally
            {
                Logging.GetInstance().Debug("Exiting method FileUpload.SetUploadedAttachmentsLinks");
            }
        }

        /// <summary>
        /// Sets the link file CSS class.
        /// </summary>
        /// <param name="lnkFile">The linkbutton of file.</param>
        /// <param name="fileName">Name of the file.</param>
        private void SetLinkFileCSSClass(Label lblFile, string fileName)
        {
            string fileExtension = Path.GetExtension(fileName);

            if (fileExtension.ToLower() == ".pdf" || fileExtension.ToLower() == ".jpg" ||
                fileExtension.ToLower() == ".jpeg" || fileExtension.ToLower() == ".tif" ||
                fileExtension.ToLower() == ".png" || fileExtension.ToLower() == ".gif" ||
                fileExtension.ToLower() == ".tiff")
            {
                lblFile.CssClass = string.Format("{0} {1}", FancyBoxCssClass, DocumentIconCssClass);
            }
        }
        /// <summary>
        /// Get the document library attachments with status uploaded and saved only.
        /// </summary>
        public List<FileUploaded> GetAttachments()
        {
            try
            {
                Logging.GetInstance().Debug("Entering method FileUpload.GetAttachments");
                SPSecurity.RunWithElevatedPrivileges(() =>
                {
                    using (SPSite site = new SPSite(DocLibWebUrl.Trim()))
                    {
                        using (SPWeb web = site.OpenWeb())
                        {
                            SPList list = web.Lists.TryGetList(DocumentLibraryName.Trim());
                            var q = Utilities.BusinessHelper.GetQueryObject("<Where><And><Eq><FieldRef Name='" +
                                LookupFieldName + "' /><Value Type='Lookup'>" + RequestID.ToString() +
                                "</Value></Eq><And><Eq><FieldRef Name='Group' /><Value Type='Text'>" + Group.Trim() +
                                "</Value></Eq><Or><Eq><FieldRef Name='DocumentStatus' /><Value Type='Choice'>" + AttachmentStatus.Saved.ToString() +
                                "</Value></Eq><Eq><FieldRef Name='DocumentStatus' /><Value Type='Choice'>" + AttachmentStatus.Uploaded.ToString() +
                                "</Value></Eq></Or></And></And></Where>");

                            var items = list.GetItems(q);
                            var listItems = from item in items.Cast<SPListItem>()
                                            select new FileUploaded
                                            {
                                                ID = item.ID,
                                                RequestID = RequestID,
                                                Title = item["Title"] != null ? item["Title"].ToString() : string.Empty,
                                                NameGuid = item["Name"] != null ? item["Name"].ToString() : string.Empty,
                                                MIMEType = item["File_x0020_Type"] != null ? item["File_x0020_Type"].ToString() : string.Empty,
                                                Size = item["File_x0020_Size"] != null ? Convert.ToInt64(item["File_x0020_Size"]) : 0,
                                                //Use this with delegation
                                                Url = item["FileRef"] != null ? (site.Url + item["FileRef"].ToString()) : string.Empty,
                                                Modified = DateTime.Parse((item["Modified"] != null) ? item["Modified"].ToString() : string.Empty),
                                                ModifiedBy = item["Editor"] != null ? item["Editor"].ToString().Split('#')[1].ToString() : string.Empty,
                                                Created = DateTime.Parse((item["Modified"] != null) ? item["Modified"].ToString() : string.Empty),
                                                CreatedBy = item["Author"] != null ? item["Author"].ToString().Split('#')[1].ToString() : string.Empty,
                                                FileStatus = (item["DocumentStatus"] != null) ? (AttachmentStatus)Enum.Parse(typeof(AttachmentStatus), item["DocumentStatus"].ToString()) : new AttachmentStatus()
                                            };

                            Attachments = listItems.ToList<FileUploaded>();
                            AttachmentsCount = Attachments.Count;
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
                Logging.GetInstance().Debug("Exiting method FileUpload.GetAttachments");
            }
            return Attachments;
        }
        /// <summary>
        /// Get the document library attachments with status saved only to be displayed in the read-only mode.
        /// </summary>
        private List<FileUploaded> GetSavedAttachments()
        {
            try
            {
                Logging.GetInstance().Debug("Entering method FileUpload.GetSavedAttachments");
                SPSecurity.RunWithElevatedPrivileges(() =>
                {
                    using (SPSite site = new SPSite(DocLibWebUrl.Trim()))
                    {
                        using (SPWeb web = site.OpenWeb())
                        {
                            SPList list = web.Lists.TryGetList(DocumentLibraryName.Trim());
                            var q = BusinessHelper.GetQueryObject("<Where><And><Eq><FieldRef Name='" + LookupFieldName + "' /><Value Type='Lookup'>" +
                                RequestID.ToString() + "</Value></Eq><And><Eq><FieldRef Name='Group' /><Value Type='Text'>" + Group.Trim() +
                                "</Value></Eq><Eq><FieldRef Name='DocumentStatus' /><Value Type='Choice'>" + AttachmentStatus.Saved.ToString() +
                                "</Value></Eq></And></And></Where>");

                            var items = list.GetItems(q);
                            var listItems = from item in items.Cast<SPListItem>()
                                            select new FileUploaded
                                            {
                                                ID = item.ID,
                                                RequestID = RequestID,
                                                Title = item["Title"] != null ? item["Title"].ToString() : string.Empty,
                                                NameGuid = item["Name"] != null ? item["Name"].ToString() : string.Empty,
                                                MIMEType = item["File_x0020_Type"] != null ? item["File_x0020_Type"].ToString() : string.Empty,
                                                Size = item["File_x0020_Size"] != null ? Convert.ToInt64(item["File_x0020_Size"]) : 0,
                                                Url = item["FileRef"] != null ? (site.Url + item["FileRef"].ToString()) : string.Empty,
                                                Modified = DateTime.Parse((item["Modified"] != null) ? item["Modified"].ToString() : string.Empty),
                                                ModifiedBy = item["Editor"] != null ? item["Editor"].ToString().Split('#')[1].ToString() : string.Empty,
                                                Created = DateTime.Parse((item["Modified"] != null) ? item["Modified"].ToString() : string.Empty),
                                                CreatedBy = item["Author"] != null ? item["Author"].ToString().Split('#')[1].ToString() : string.Empty,
                                                FileStatus = (item["DocumentStatus"] != null) ? (AttachmentStatus)Enum.Parse(typeof(AttachmentStatus), item["DocumentStatus"].ToString()) : new AttachmentStatus()
                                            };

                            Attachments = listItems.ToList<FileUploaded>();
                            AttachmentsCount = Attachments.Count;
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
                Logging.GetInstance().Debug("Exiting method FileUpload.GetSavedAttachments");
            }
            return Attachments;
        }

        /// <summary>
        /// Bind the attachments.
        /// </summary>
        public void Bind()
        {
            //if (attachmentCategoryList != null)
            //{
            //    AttachmentCategoriesBind();
            //}
            if (Enabled)
            {
                GetAttachments();
                divNoFiles.Visible = false;
            }
            else
            {
                GetSavedAttachments();
                if (AttachmentsCount == 0)
                {


                    divNoFiles.Visible = true;
                }
                else
                {
                    divNoFiles.Visible = false;
                }
            }
            SetUploadedAttachmentsLinks();
        }
        /// <summary>
        /// Bind the dropdownlist attachments categories.
        /// </summary>
        //public void AttachmentCategoriesBind()
        //{
        //    if (attachmentCategoryList != null)
        //    {
        //        divUploadLabel.Visible = false;
        //        HelperMethods.BindDropDownList(ref ddlAttachmentCategory, attachmentCategoryList, AttachmentCategoryDataValueField, AttachmentCategoryDataTextArField, AttachmentCategoryDataTextEnField, LCID);
        //        ddlAttachmentCategory.Items.Insert(0, new ListItem(LocalizedText("FU_SelectDefaultValue"), "-1"));
        //        //ddlAttachmentCategory.DataSource = attachmentCategoryList;
        //        //ddlAttachmentCategory.BindDataSource();
        //    }
        //}

        /// <summary>
        /// Clears the attached documents.
        /// </summary>
        public void ClearAttachments()
        {
            Attachments = new List<FileUploaded>();
            SetUploadedAttachmentsLinks();
        }
        public bool SaveAttachments()
        {
            Logging.GetInstance().Debug("Entering method FileUpload.SaveAttachment");
            SPWeb web = null;
            bool saved = false;
            try
            {
                if (AttachmentsCount > 0)
                {
                    SPSecurity.RunWithElevatedPrivileges(() =>
                    {
                        using (SPSite site = new SPSite(DocLibWebUrl))
                        {
                            using (web = site.OpenWeb())
                            {
                                SPList list = web.Lists.TryGetList(DocumentLibraryName);
                                if (list != null)
                                {
                                    web.AllowUnsafeUpdates = true;
                                    List<string> columns = new List<string>();
                                    foreach (var attach in Attachments)
                                    {
                                        columns.Add("ID;Counter;Eq;" + attach.ID);
                                    }
                                    var q = Utilities.BusinessHelper.GetQueryObject(BusinessHelper.CreateCAMLQuery(columns, "Or", true));

                                    var items = list.GetItems(q);
                                    foreach (SPListItem item in items)
                                    {
                                        item["DocumentStatus"] = AttachmentStatus.Saved;
                                        item.SystemUpdate();
                                        SPFile spfile = item.File;
                                        if (spfile.CheckOutType != SPFile.SPCheckOutType.None)
                                            spfile.CheckIn("File saved Successfully");

                                        saved = true;
                                    }
                                }
                            }
                        }
                    });
                }
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
                Logging.GetInstance().Debug("Exiting method FileUpload.SaveAttachment");
            }
            return saved;
        }

        /// <summary>
        /// Localizeds the text.
        /// </summary>
        /// <param name="resourceKey">The resource key.</param>
        /// <returns></returns>
        public string LocalizedText(string resourceKey)
        {
            return HelperMethods.LocalizedText("ITWORX.MOEHEWF.Common", resourceKey, (uint)LCID);
        }

        private bool FileExists(string fileName)
        {
            Logging.GetInstance().Debug("Entering method FileUpload.FileExists");
            bool exist = false;

            try
            {
                SPSecurity.RunWithElevatedPrivileges(() =>
                {
                    using (SPSite site = new SPSite(DocLibWebUrl))
                    {
                        using (SPWeb web = site.OpenWeb())
                        {
                            SPList library = web.Lists.TryGetList(DocumentLibraryName);
                            var query = Utilities.BusinessHelper.GetQueryObject("<Where><And><Eq><FieldRef Name='Title' /><Value Type='Text'>" +
                                fileName.Trim() + "</Value></Eq><And><Eq><FieldRef Name='Group' /><Value Type='Text'>" + Group.Trim() +
                                "</Value></Eq><And><Eq><FieldRef Name='" + LookupFieldName + "' /><Value Type='Lookup'>" + RequestID +
                                "</Value></Eq><Neq><FieldRef Name='DocumentStatus' /><Value Type='Choice'>" + AttachmentStatus.Deleted.ToString() +
                                "</Value></Neq></And></And></And></Where>");

                            var items = library.GetItems(query);
                            if (items != null && items.Count > 0)
                            {
                                exist = true;
                                FileExistID = items[0].ID;
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
                Logging.GetInstance().Debug("Exiting method FileUpload.FileExists");
            }
            return exist;
        }
        #endregion

    }

}

