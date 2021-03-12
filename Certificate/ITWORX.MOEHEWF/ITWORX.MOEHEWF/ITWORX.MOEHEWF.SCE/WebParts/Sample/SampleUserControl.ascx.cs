using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;

namespace ITWORX.MOEHEWF.SCE.WebParts.Sample
{
    public partial class SampleUserControl : UserControl
    {

       
        protected global::ITWORX.MOEHEWF.Common.CONTROLTEMPLATES.ITWORX.MOEHEWF.Common.ClientSideFileUpload FileUp1;
        protected global::ITWORX.MOEHEWF.Common.CONTROLTEMPLATES.ITWORX.MOEHEWF.Common.ClientSideFileUpload FileUp2;
        protected global::ITWORX.MOEHEWF.Common.CONTROLTEMPLATES.ITWORX.MOEHEWF.Common.ClientSideFileUpload FileUp3;
        protected global::ITWORX.MOEHEWF.Common.CONTROLTEMPLATES.ITWORX.MOEHEWF.Common.ClientSideFileUpload FileUp4;
        protected void Page_Load(object sender, EventArgs e)
        {

            FileUp1.Group = "1";
            FileUp1.HasOptions = true;
            FileUp1.DocumentLibraryName = "SCEBookAttachments";
            FileUp1.IsRequired = true;
            FileUp1.MaxFileNumber = 1;
            FileUp1.MaxSize = 10240000;
            FileUp1.SupportedExtensions = "jpg,pdf,png";
            FileUp1.DocLibWebUrl = "http://moehe-dev-02/sites/SC";
            FileUp1.Title = "File Upload";
            FileUp1.RequiredValidationMessage = "File upload is required";
            FileUp1.LookupFieldName = "RequestID";
            FileUp1.LookupFieldValue = 1;
            FileUp1.FileExtensionValidation = "Supported file extensions are jpg,pdf,png";
            FileUp1.FileSizeValidationMsg = "File size must not be greater than " + FileUp1.MaxSize + " MB";
            FileUp1.FileNumbersValidationMsg = "You can't upload more than " + FileUp1.MaxFileNumber + " files";
            FileUp1.FileExistsValidationMsg = "File exists with the same name";
            FileUp1.DropClientID = dropFileUpload.ClientID;
            FileUp1.ReqDropClientID = lblRequiredDrop.ClientID;
            FileUp1.ValidqationGroup = "Submit";
            FileUp1.LabelRequiredDrop = lbldropFileUpload.ClientID;

            FileUp2.Group = "2";                
            FileUp2.HasOptions = true;
            FileUp2.DocumentLibraryName = "SCEBookAttachments";
            FileUp2.IsRequired = true;
            FileUp2.MaxFileNumber = 2;
            FileUp2.MaxSize = 10240000;
            FileUp2.SupportedExtensions = "jpg,pdf,png";
            FileUp2.DocLibWebUrl = "http://moehe-dev-02/sites/SC";
            FileUp2.Title = "File Upload";
            FileUp2.RequiredValidationMessage = "File upload is required";
            FileUp2.LookupFieldName = "RequestID";
            FileUp2.LookupFieldValue = 1;
            FileUp2.FileExtensionValidation = "Supported file extensions are jpg,pdf,png";
            FileUp2.FileSizeValidationMsg = "File size must not be greater than " + FileUp2.MaxSize + " MB";
            FileUp2.FileNumbersValidationMsg = "You can't upload more than " + FileUp2.MaxFileNumber + " files";
            FileUp2.FileExistsValidationMsg = "File exists with the same name";
            FileUp2.DropClientID = dropFileUpload2.ClientID;
            FileUp2.ReqDropClientID = lblRequiredDrop2.ClientID;
            FileUp2.ValidqationGroup = "Submit";
            FileUp2.LabelRequiredDrop = lbldropFileUpload2.ClientID;

            FileUp3.Group = "3";
            FileUp3.HasOptions = false;
            FileUp3.DocumentLibraryName = "SCEBookAttachments";
            FileUp3.IsRequired = true;
            FileUp3.MaxFileNumber = 2;
            FileUp3.MaxSize = 10240000;
            FileUp3.SupportedExtensions = "jpg,pdf,png";
            FileUp3.DocLibWebUrl = "http://moehe-dev-02/sites/SC";
            FileUp3.Title = "File Upload";
            FileUp3.RequiredValidationMessage = "File upload is required";
            FileUp3.LookupFieldName = "RequestID";
            FileUp3.LookupFieldValue = 1;
            FileUp3.FileExtensionValidation = "Supported file extensions are jpg,pdf,png";
            FileUp3.FileSizeValidationMsg = "File size must not be greater than " + FileUp3.MaxSize + " MB";
            FileUp3.FileNumbersValidationMsg = "You can't upload more than " + FileUp3.MaxFileNumber + " files";
            FileUp3.FileExistsValidationMsg = "File exists with the same name";
            FileUp3.ValidqationGroup = "Submit";
         
        }

        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            FileUp1.SaveAttachments();
            FileUp2.SaveAttachments();
            FileUp3.SaveAttachments();
            FileUp4.SaveAttachments();
        }
    }
}
