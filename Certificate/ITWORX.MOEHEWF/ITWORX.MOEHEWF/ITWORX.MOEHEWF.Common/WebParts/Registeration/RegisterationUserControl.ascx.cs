using ITWORX.MOEHE.Utilities;
using ITWORX.MOEHE.Utilities.Logging;
using ITWORX.MOEHEWF.Common.BL;
using System;
using System.Collections.Generic;
using System.Web.UI;
using ITWORX.MOEHEWF.Common.Entities;
using ITWORX.MOEHEWF.Common.Utilities;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.Security;
using Microsoft.SharePoint;

namespace ITWORX.MOEHEWF.Common.WebParts.Registeration
{
    public partial class RegisterationUserControl : UserControlBase
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                PopulateDropdowns();
            }
        }
        private void PopulateDropdowns()
        {
            Logging.GetInstance().Debug("Enter RegisterationUserControl.PopulateDropdowns");
            try
            {
                List<Common.Entities.Nationality> nationalityItems = BL.Nationality.GetAll();//.Nationality.GetAll();
                List<Common.Entities.NationalityCategory> nationalityCategoryItems = BL.NationalityCategory.GetAll();
               // List<Common.Entities.AttachmentType> attachmentsTypes = BL.AttachmentType.GetAll();
                if (nationalityItems != null || nationalityItems.Count > 0)
                {

                    dropNationailty.Items.Insert(0, new ListItem(HelperMethods.LocalizedText("ITWORX.MOEHEWF.Common", "ChooseValue", (uint)LCID), string.Empty));
                    dropNationailty.AppendDataBoundItems = true;
                    HelperMethods.BindDropDownList(ref dropNationailty, nationalityItems, "ID", "ArabicTitle", "EnglishTitle", LCID);

                }
                if (nationalityCategoryItems != null || nationalityCategoryItems.Count > 0)
                {

                    dropNationalityCategory.Items.Insert(0, new ListItem(HelperMethods.LocalizedText("ITWORX.MOEHEWF.Common", "ChooseValue", (uint)LCID), string.Empty));
                    dropNationalityCategory.AppendDataBoundItems = true;
                    HelperMethods.BindDropDownList(ref dropNationalityCategory, nationalityCategoryItems, "ID",
                        "ArabicTitle", "EnglishTitle", LCID);



                }
                //if (attachmentsTypes != null || attachmentsTypes.Count > 0)
                //{

                //    dropAttachments.Items.Insert(0, new ListItem(HelperMethods.LocalizedText("ITWORX.MOEHEWF.Common", "ChooseValue", (uint)LCID), string.Empty));
                //    dropAttachments.AppendDataBoundItems = true;
                //    HelperMethods.BindDropDownList(ref dropAttachments, attachmentsTypes, "ID", "ArabicTitle", "EnglishTitle", LCID);

                //}
                List<ListItem> items = new List<ListItem>() { new ListItem() { Text = HelperMethods.LocalizedText("ITWORX.MOEHEWF.Common", "Male", (uint)LCID), Value = "0" },
                    new ListItem() {  Text= HelperMethods.LocalizedText("ITWORX.MOEHEWF.Common", "Female", (uint)LCID),  Value ="1"} };
                rblGender.Items.AddRange(items.ToArray());


            }
            catch (Exception ex)
            {
                Logging.GetInstance().LogException(ex);
            }
            finally
            {

                Logging.GetInstance().Debug("Exit RegisterationUserControl.PopulateDropdowns");
            }
        }



        protected void btnLogout_Click(object sender, EventArgs e)
        {

        }

        protected void btnCreateUser_Click(object sender, EventArgs e)
        {
            Logging.GetInstance().Debug("Enter RegisterationUserControl.btnCreateUser");
            try
            {
              
                // need to know username and password
                MembershipUser membershipUser = Membership.CreateUser(txtEmail.Text, txtPersonalID.Text);
                Microsoft.SharePoint.SPSecurity.RunWithElevatedPrivileges(delegate
                {

                    using (SPSite spSite = new SPSite(SPContext.Current.Site.ID))
                    {
                        using (SPWeb spWeb = spSite.OpenWeb())
                        {
                            spWeb.AllowUnsafeUpdates = true;
                            SPUser spUser = spWeb.EnsureUser(txtEmail.Text);
                            SPGroup spGroup = spWeb.SiteGroups[Common.Utilities.Constants.ApplicantsGroup];
                            spGroup.AddUser(spUser);
                            spWeb.AllowUnsafeUpdates = false;
                        }
                    }
                });
                if (membershipUser != null)
                {
                    SaveUserRegisterationInfo();
                }
            }
            catch (Exception ex)
            {
                Logging.GetInstance().LogException(ex);
            }
            finally
            {

                Logging.GetInstance().Debug("Exit RegisterationUserControl.btnCreateUser");
            }
        }
       
        private void SaveUserRegisterationInfo()
        {
            Entities.Applicants applicant = new Entities.Applicants();
            applicant.ID = 0;
            applicant.PersonalID = txtPersonalID.Text;
            applicant.Nationality = new Entities.Nationality();
            applicant.Nationality.SelectedID = BL.Nationality.GetNationalityID(dropNationailty.SelectedValue.ToString()).ToString();
            applicant.Nationality.SelectedTitle = dropNationailty.SelectedValue.ToString();
            applicant.NationalityCategory = new Entities.NationalityCategory();
            applicant.NationalityCategory.SelectedID = BL.NationalityCategory.GetNationalityCategoryID(
                dropNationalityCategory.SelectedValue.ToString()).ToString();
            applicant.NationalityCategory.SelectedTitle = dropNationalityCategory.SelectedValue.ToString();
            applicant.MobileNumber = txtMobileNumber.Text;
            applicant.ApplicantEmail = txtEmail.Text;
            applicant.ApplicantGender = Convert.ToBoolean(rblGender.SelectedItem.Value);
            applicant.BirthDate = dtBirthDate.SelectedDate.Date;
            BL.Applicants.AddApplicant(applicant, 0);
        }

        //List<DocumentInfo> UploadedDocuments { get; set; }
        //private void SaveUploadedDocument()
        //{
        //    if (UploadedDocuments == null)
        //    {
        //        UploadedDocuments = new List<DocumentInfo>();
        //    }
        //    UploadedDocuments.Add(new DocumentInfo()
        //    {
        //        FileName = uploadAttachments.FileName,
        //        FileContent = uploadAttachments.FileContent,
        //        Overwrite = true,
        //        DocumentType = dropAttachments.SelectedValue.ToString()
        //    });
        //}
    //    private void UploadFileToDocumentLibrary()
    //    {
    //        BL.Applicants
    //        String documentLibraryName = "StudentDocument";
    //        SPSecurity.RunWithElevatedPrivileges(delegate ()
    //        {
    //            using (SPSite oSite = new SPSite(SPContext.Current.Web.Url))
    //            {
    //                using (SPWeb oWeb = oSite.OpenWeb())
    //                {

    //                    oWeb.AllowUnsafeUpdates = true;
    //                    SPFolder myLibrary = oWeb.Folders[documentLibraryName];

    //                    // Upload document

    //                    foreach (DocumentInfo docInfo in UploadedDocuments)
    //                    {

    //                        SPFile spfile = myLibrary.Files.Add(docInfo.FileName,
    //                            docInfo.FileContent, docInfo.Overwrite);

    //                        int gDLookupID = GetLookupIDFromList(oWeb, "DocumentType", "Title",
    //                                    docInfo.DocumentType);
    //                        spfile.Item["Document Type"] = new SPFieldLookupValue(gDLookupID,
    //                           docInfo.DocumentType);
    //                        // spfile.Item["Student Email"]=
    //                        spfile.Item.Update();


    //                    }
    //                    myLibrary.Update();
    //                }
    //            }
    //        });
    //    }

    //    protected void BtnUpload_Click(object sender, EventArgs e)
    //    {
    //        if (FileUpload1.HasFile)
    //        {
    //            SaveUploadedDocument();

    //        }
    //    }
    //}

    //public class DocumentInfo
    //{
    //    public string FileName { set; get; }
    //    public System.IO.Stream FileContent { set; get; }
    //    public bool Overwrite { set; get; }
    //    public string DocumentType { get; internal set; }
    //}

}
    }
