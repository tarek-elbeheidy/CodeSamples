using ITWORX.MOEHE.Utilities;
using ITWORX.MOEHEWF.Common.BL;
using ITWORX.MOEHEWF.Common.CONTROLTEMPLATES.ITWORX.MOEHEWF.Common;
using ITWORX.MOEHEWF.SCE.Entities;
using Microsoft.SharePoint;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.UI;
using System.Web.UI.WebControls;
using ITWORX.MOEHEWF.SCE.Utilities;
using System.Web.UI.WebControls.WebParts;
using System.Globalization;
using Microsoft.SharePoint.Utilities;
using System.Web;
using ITWORX.MOEHE.Utilities.Logging;


namespace ITWORX.MOEHEWF.SCE.WebParts.EditRequest
{
    public partial class EditRequestUserControl : UserControlBase
    {
        protected DDLWithTXTWithNoPostback certificateResource;
        protected DDLWithTXTWithNoPostback schooleType;
        protected DDLWithTXTWithNoPostback certificateType;
        protected DDLWithTXTWithNoPostback dropFileUpload;
        protected DDLWithTXTWithNoPostback ddlSchoolingSystem;

        protected global::ITWORX.MOEHEWF.Common.CONTROLTEMPLATES.ITWORX.MOEHEWF.Common.ClientSideFileUpload FileUp1;


        protected int RequestId = 0;
        protected string DocumentLibrary = string.Empty;
        protected string WebUrl = string.Empty;
        protected string LookupFieldName = string.Empty;
        /* protected string DropClientId = string.Empty;
         protected string TextBoxClientID = string.Empty;
         protected string ReqDropClientID = string.Empty;
         protected string LabelRequiredDrop = string.Empty;
         protected string Group = string.Empty;*/
        public string CerTypeClientID { get { return certificateType.Client_ID; } }
        public string AssApplicantGroupName { get { return Common.Utilities.Constants.EmployeeAsApplicant; } }
        public bool isEmpAsApplicant { get { return HelperMethods.InGroup(Common.Utilities.Constants.EmployeeAsApplicant); } }

        //public bool isEmpAsApplicant { get { return Applicants.inApplicantGroup(AssApplicantGroupName); } }

        public string LoginName { get { return SPContext.Current.Web.CurrentUser.LoginName; } }

        public int? requestID
        {
            get
            {
                if (Request.QueryString["RequestId"] != null)
                {
                    return Convert.ToInt32(Request.QueryString["RequestId"]);
                }
                else if (ViewState["RequestId"] != null)
                {
                    return Convert.ToInt32(ViewState["RequestId"]);
                }
                return null;
            }
            set
            {
                ViewState["RequestId"] = value;
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
           
            if (!IsPostBack)
            {
                RequestId = Convert.ToInt32(requestID);
                bindDDLS();
                BindApplicant();
                MOIAddress_hdf.Value = new Uri(HelperMethods.GetWebAppConfigValue(SPContext.Current.Site.Url,
                             "IdentityServiceURLAjaxCall") + "?QatarId=").ToString();

                if (requestID != null)
                {
                    BindRequest();
                }
            }
        }

        void BindRequest()
        {
            SCEContextDataContext ctx = new SCEContextDataContext(SPContext.Current.Site.RootWeb.Url);
            SCERequestsListFieldsContentType request = ctx.SCERequestsList.ScopeToFolder("", true).Where(r => r.Id == requestID && r.LoginName == LoginName).FirstOrDefault();
            if (request != null)
            {
                if (isEmpAsApplicant)
                {
                    txtTempQatarID.Text = request.StdQatarID;
                    txt_PassPort.Text = request.StdPassportNum;
                    txt_birthDate.Text = request.StdBirthDate;
                    txt_Name.Text = request.StdName;
                }
 
                txt_QatarID.Text = request.StdQatarID;
                txt_PrintedName.Text = request.StdPrintedName; 
                ddl_Nationality.SelectedValue = request.StdNationality != null ? request.StdNationality.Id.ToString() : "-1";
                ddl_NatCat.SelectedValue = request.StdNationalityCatId.ToString();
 
                txt_QatarID.Text = request.StdQatarID;
                txt_birthDate.Text = request.StdBirthDate;
                txt_Name.Text = request.StdName;
                ddl_Gender.SelectedValue = request.StdGender != null ? request.StdGender : "-1";

                if (request.CertificateResourceId == null && string.IsNullOrEmpty(request.OtherCertificateResource))
                {
                    certificateResource.SelectedValue = "-1";
                }
                else if (request.CertificateResourceId == null && !string.IsNullOrEmpty(request.OtherCertificateResource))
                {
                    certificateResource.SelectedValue = "-2";
                    certificateResource.OtherValue = request.OtherCertificateResource;
                }
                else if (request.CertificateResourceId != null)
                {
                    certificateResource.SelectedValue = request.CertificateResourceId.ToString();
                }

                if (request.SchoolType == null && string.IsNullOrEmpty(request.OtherSchoolType))
                {
                    schooleType.SelectedValue = "-1";
                }
                else if (request.SchoolType == null && !string.IsNullOrEmpty(request.OtherSchoolType))
                {
                    schooleType.SelectedValue = "-2";
                    schooleType.OtherValue = request.OtherSchoolType;
                }
                else if (request.SchoolType != null)
                {
                    schooleType.SelectedValue = request.SchoolType.Id.ToString();
                }
                if (request.SchoolSystem == null && string.IsNullOrEmpty(request.OtherSchoolSystem))
                {
                    ddlSchoolingSystem.SelectedValue = "-1";
                }
                else if (request.SchoolSystem == null && !string.IsNullOrEmpty(request.OtherSchoolSystem))
                {
                    ddlSchoolingSystem.SelectedValue = "-2";
                    ddlSchoolingSystem.OtherValue = request.OtherSchoolSystem;
                }
                else if (request.SchoolSystem != null)
                {
                    ddlSchoolingSystem.SelectedValue = request.SchoolSystem.Id.ToString();
                } 

                if (request.EquivalencyPurpose != null)
                {
                    ddlEquiPurpose.SelectedValue = request.EquivalencyPurpose.Id.ToString();
                }
                txt_PrevSchool.Text = request.PrevSchool;
                ddl_ScholasticLevel.SelectedValue = request.LastScholasticLevel != null ? request.LastScholasticLevel.Id.ToString() : "-1";
                ddl_LastAcademicYear.SelectedValue = string.IsNullOrEmpty(request.LastAcademicYear) ? "-1" : request.LastAcademicYear;
                ddl_GoingToClass.SelectedValue = request.RegisteredScholasticLevel != null ? request.RegisteredScholasticLevel.Id.ToString() : "-1";
                txtPassedYears.Text = request.TotalPassedYears;
                if (request.CertificateType == null && string.IsNullOrEmpty(request.OtherCertificateType))
                {
                    certificateType.SelectedValue = "-1";
                }
                else if (request.CertificateType == null && !string.IsNullOrEmpty(request.OtherCertificateType))
                {
                    certificateType.SelectedValue = "-2";
                    certificateType.OtherValue = request.OtherCertificateType;
                }
                else if (request.CertificateType != null)
                {
                    certificateType.SelectedValue = request.CertificateType.Id.ToString();
                    if (request.CertificateType.Id == 1)
                    {
                        var oLevels = ctx.SCEIGCSEList.ScopeToFolder("", true).ToList().Where(x => x.RequestID == ctx.SCERequests.ScopeToFolder("", true).Where(r => r.Id == requestID).FirstOrDefault() && x.Type == Type.Olevel).ToList();
                        //var oLevels = ctx.SCEIGCSEList.ScopeToFolder("", true).Where(x=>x.Type == Type.Olevel);

                        List<SCEIGCSE> OlevelListList = new List<SCEIGCSE>();
                        int counter = 1;
                        foreach (var item in oLevels)
                        {
                            OlevelListList.Add(new SCEIGCSE()
                            {
                                //ID = (int)item.Id,
                                ID = counter,
                                Avrage = item.Average,
                                Code = item.Code,
                                Title = item.Title
                            });
                            counter++;

                        }
                        OLevel_HF.Value = JsonConvert.SerializeObject(OlevelListList);
                        var aLevels = ctx.SCEIGCSEList.ScopeToFolder("", true).Where(x => x.RequestID.Id == requestID && x.Type == Type.ALevel);
                        List<SCEIGCSE> AlevelListList = new List<SCEIGCSE>();
                        counter = 1;
                        foreach (var item in aLevels)
                        {
                            AlevelListList.Add(new SCEIGCSE()
                            {
                                //ID = (int)item.Id,
                                ID = counter,
                                Avrage = item.Average,
                                Code = item.Code,
                                Title = item.Title
                            });
                            counter++;
                        }
                        ALevel_HF.Value = JsonConvert.SerializeObject(AlevelListList);

                    }
                    else if (request.CertificateType.Id == 2)
                    {
                        var SCEIBS = ctx.SCEIBList.ScopeToFolder("", true).Where(x => x.RequestID.Id == requestID);
                        List<SCEIB> SCEIBList = new List<SCEIB>();
                        foreach (var item in SCEIBS)
                        {
                            SCEIBList.Add(new SCEIB()
                            {
                                ID = (int)item.Id,
                                Title = item.Title,
                                Level = item.Level.Title,
                                //LevelTitle = item.Level.Title,
                                Points = item.PointCount,
                                Code = item.Code
                            });
                        }
                        IBList_HF.Value = JsonConvert.SerializeObject(SCEIBList);
                    }
                }
                dropFileUpload.OtherText = HelperMethods.LocalizedText("ITWORX_MOEHEWF_SCE", "OtherDocuments", (uint)LCID);
                dropFileUpload.LblOtherTextBoxVisibility = true;
                dropFileUpload.LblOtherTextBoxText = HelperMethods.LocalizedText("ITWORX_MOEHEWF_SCE", "FileDescription", (uint)LCID);
                //if ((ddl_GoingToClass.SelectedValue == "14" || string.IsNullOrEmpty(ddl_GoingToClass.SelectedValue))
                //                 && (!string.IsNullOrEmpty(certificateType.SelectedValue) || certificateType.SelectedValue!="-1"))
                //ddl_ScholasticLevel.SelectedValue == "13")
                if (ddl_GoingToClass.SelectedValue == "14" && certificateType.SelectedValue != "-1")
                {
                    var selectedCountry = ctx.CountryOfStudyList.Where(c => c.Id == int.Parse(certificateResource.SelectedValue)).FirstOrDefault();
                    //var fileNames = ctx.FileNameList.Where(f => f.CertificateTypeId.Contains(Convert.ToInt32(certificateType.SelectedValue)));
                    var fileNames = ctx.FileNameList.Where(f => f.CertificateTypeId.Contains(Convert.ToInt32(certificateType.SelectedValue)) && f.MOEHECountryType.Contains(selectedCountry.MOEHECountryType.ToString())).ToList();
                    var selectedCertType = ctx.CertificateType.Where(c => c.Id == int.Parse(certificateType.SelectedValue)).FirstOrDefault();

                    //check  if certificate type is high school, country is arab and school type is not private 
                    //in order to remove the certificate from country option
                    string groupName = string.Empty;
                  

                    if (certificateType.SelectedText.Equals(HelperMethods.LocalizedText("ITWORX_MOEHEWF_SCE", "HighSchool", (uint)LCID))
                 && selectedCountry.MOEHECountryType.ToString().Equals("Arab"))

                    {
                        if (schooleType.SelectedValue != "-2" && schooleType.SelectedText.Equals(HelperMethods.LocalizedText("ITWORX_MOEHEWF_SCE", "Private", (uint)LCID)))
                        {
                            groupName = selectedCertType.Title + selectedCountry.MOEHECountryType.ToString() + "Private";
                        }
                        else
                        {
                            // remove here
                            groupName = selectedCertType.Title + selectedCountry.MOEHECountryType.ToString();
                            string certText = HelperMethods.LocalizedText("ITWORX_MOEHEWF_SCE", "CertificateCopyFromCountry", (uint)LCID);
                            var file = fileNames.Find(f => f.Title.Equals(certText) || f.TitleAr.Equals(certText));
                            fileNames.Remove(file);
                        }



                    }
                    else
                    {
                        groupName = selectedCertType.Title + selectedCountry.MOEHECountryType.ToString();
                    }


                    dropFileUpload.DataSource = fileNames;
                    dropFileUpload.DataValueField = "ID";
                    dropFileUpload.DataENTextField = "Title";
                    dropFileUpload.DataARTextField = "TitleAr";
                    dropFileUpload.ValidationGroup = "Submit";
                   
                    dropFileUpload.BingDDL();
                    
                    BindAttachments(fileNames.Count(), groupName);
                    ViewState["filesCount"] = fileNames.Count().ToString();
                }
                else
                {
                    var fileNames = ctx.FileNameList.Where(f => f.NationalityCategoryId.Contains(Convert.ToInt32(ddl_NatCat.SelectedValue)));
                    //HelperMethods.BindDropDownList(ref dropFileUpload, fileNames, "ID", "TitleAr", "Title", LCID);
                    //dropFileUpload.Items.Insert(0, new ListItem(HelperMethods.LocalizedText("ITWORX_MOEHEWF_UCE", "ChooseValue", (uint)LCID), "-1"));
                    //dropFileUpload.IsRequired = true;
                    dropFileUpload.DataSource = fileNames;
                    dropFileUpload.DataValueField = "ID";
                    dropFileUpload.DataENTextField = "Title";
                    dropFileUpload.DataARTextField = "TitleAr";
                    dropFileUpload.ValidationGroup = "Submit";
                    //dropFileUpload.Title = HelperMethods.LocalizedText("ITWORX_MOEHEWF_SCE", "SchoolTypeTitle", (uint)LCID);
                    //dropFileUpload.ValidationMSG = HelperMethods.LocalizedText("ITWORX_MOEHEWF_SCE", "SchoolTypeValidation", (uint)LCID);
                   
                    dropFileUpload.BingDDL();
                    //if (ddl_NatCat.SelectedValue == "6" && ctx.FileNameList.Where(f => f.NationalityCategoryId.Contains(Convert.ToInt32(ddl_NatCat.SelectedValue)) && f.ScholasticLevelId.Contains(Convert.ToInt32(ddl_ScholasticLevel.SelectedValue))).Count() == 0)
                    //{
                    //    //dropFileUpload.Items.RemoveAt(dropFileUpload.Items.Count - 1);
                    //    DropDownList myddl = (DropDownList)dropFileUpload.FindControl("dropWithNewOption");
                    //    myddl.Items.RemoveAt(myddl.Items.Count - 2);
                    //    BindAttachments(fileNames.Count());
                    //    //ViewState["filesCount"] = (fileNames.Count() - 1).ToString();
                    //}
                    //else
                    //{
                    var nationalityCat = ctx.NationalityCategory.Where(f => f.Id == int.Parse(ddl_NatCat.SelectedValue)).FirstOrDefault();
                    BindAttachments(fileNames.Count(), nationalityCat.Title);// + 1);
                                                                             //ViewState["filesCount"] = fileNames.Count().ToString();
                                                                             //}
                }

            }
        }


        void bindDDLS()
        {
            Logging.GetInstance().Debug("Entering EditRequestUserControl.bindDDLS");
            try
            {
                SPSecurity.RunWithElevatedPrivileges(delegate ()
                {
                    certificateResource.IsRequired = true;
                    certificateResource.DataSource = ITWORX.MOEHEWF.Common.BL.CountryOfStudy.GetAll(LCID);
                    certificateResource.DataValueField = "ID";
                    certificateResource.DataENTextField = "EnglishTitle";
                    certificateResource.DataARTextField = "ArabicTitle";
                    certificateResource.ValidationGroup = "Submit";
                    certificateResource.Title = HelperMethods.LocalizedText("ITWORX_MOEHEWF_SCE", "CertificateResourceTitle", (uint)LCID);
                    certificateResource.ValidationMSG = HelperMethods.LocalizedText("ITWORX_MOEHEWF_SCE", "CertificateResourceValidation", (uint)LCID);
                    certificateResource.BingDDL();

                    schooleType.IsRequired = true;
                    schooleType.DataSource = BL.SchoolType.GetAll();
                    schooleType.DataValueField = "ID";
                    schooleType.DataENTextField = "Title";
                    schooleType.DataARTextField = "TitleAr";
                    schooleType.ValidationGroup = "Submit";
                    schooleType.Title = HelperMethods.LocalizedText("ITWORX_MOEHEWF_SCE", "SchoolTypeTitle", (uint)LCID);
                    schooleType.ValidationMSG = HelperMethods.LocalizedText("ITWORX_MOEHEWF_SCE", "SchoolTypeValidation", (uint)LCID);
                    schooleType.BingDDL();

                    using (SCEContextDataContext ctx = new SCEContextDataContext(SPContext.Current.Site.RootWeb.Url))
                    {
                        var schooSystem = ctx.SchoolSystemList;
                        //HelperMethods.BindDropDownList(ref ddlSchoolingSystem, schooSystem, "ID", "TitleAr", "Title", LCID);
                        //ddlSchoolingSystem.Items.Insert(0, new ListItem(HelperMethods.LocalizedText("ITWORX_MOEHEWF_UCE", "ChooseValue", (uint)LCID), "-1"));
                        ddlSchoolingSystem.IsRequired = false;
                        ddlSchoolingSystem.DataSource = schooSystem;
                        ddlSchoolingSystem.DataValueField = "ID";
                        ddlSchoolingSystem.DataENTextField = "Title";
                        ddlSchoolingSystem.DataARTextField = "TitleAr";
                        ddlSchoolingSystem.ValidationGroup = "Submit";
                        ddlSchoolingSystem.Title = HelperMethods.LocalizedText("ITWORX_MOEHEWF_SCE", "SchoolingSystem", (uint)LCID);
                        //ddlSchoolingSystem.ValidationMSG = HelperMethods.LocalizedText("ITWORX_MOEHEWF_SCE", "SchoolTypeValidation", (uint)LCID);
                        ddlSchoolingSystem.BingDDL();
                    }

                    using (SCEContextDataContext ctx = new SCEContextDataContext(SPContext.Current.Site.RootWeb.Url))
                    {
                        var equiPurpose = ctx.EquivalencyPurposeList;
                        HelperMethods.BindDropDownList(ref ddlEquiPurpose, equiPurpose, "ID", "TitleAr", "Title", LCID);
                        ddlEquiPurpose.Items.Insert(0, new ListItem(HelperMethods.LocalizedText("ITWORX_MOEHEWF_UCE", "ChooseValue", (uint)LCID), "-1"));

                        var igDegree = ctx.IGDegreeList;
                        HelperMethods.BindDropDownList(ref ddlOlevelAverage, igDegree, "ID", "TitleAr", "Title", LCID);
                        ddlOlevelAverage.Items.Insert(0, new ListItem(HelperMethods.LocalizedText("ITWORX_MOEHEWF_UCE", "ChooseValue", (uint)LCID), "-1"));
                        HelperMethods.BindDropDownList(ref ddlAlevelAverage, igDegree, "ID", "TitleAr", "Title", LCID);
                        ddlAlevelAverage.Items.Insert(0, new ListItem(HelperMethods.LocalizedText("ITWORX_MOEHEWF_UCE", "ChooseValue", (uint)LCID), "-1"));



                        var lastScholasticLevel = ctx.ScholasticLevelList.ToList();
                        string notAvailableLevel = HelperMethods.LocalizedText("ITWORX_MOEHEWF_SCE", "NotAvailable", (uint)LCID);
                        var level = lastScholasticLevel.Find(f => f.Title.Equals(notAvailableLevel) || f.TitleAr.Equals(notAvailableLevel));
                        lastScholasticLevel.Remove(level);

                        HelperMethods.BindDropDownList(ref ddl_ScholasticLevel, lastScholasticLevel, "ID", "TitleAr", "Title", LCID);
                        ddl_ScholasticLevel.Items.Insert(0, new ListItem(HelperMethods.LocalizedText("ITWORX_MOEHEWF_UCE", "ChooseValue", (uint)LCID), "-1"));

                    }

             
                    
                    var IBLevel = BL.Levels.GetAll();
                    HelperMethods.BindDropDownList(ref ddl_IBLevel, IBLevel, "ID", "TitleAr", "Title", LCID);
                    ddl_IBLevel.Items.Insert(0, new ListItem(HelperMethods.LocalizedText("ITWORX_MOEHEWF_UCE", "ChooseValue", (uint)LCID), "-1"));

                    var scholasticLevel = BL.ScholasticLevel.GetAll();
                    HelperMethods.BindDropDownList(ref ddl_GoingToClass, scholasticLevel, "ID", "TitleAr", "Title", LCID);
                    ddl_GoingToClass.Items.Insert(0, new ListItem(HelperMethods.LocalizedText("ITWORX_MOEHEWF_UCE", "ChooseValue", (uint)LCID), "-1"));
                    ddl_GoingToClass.Items.RemoveAt(1);

                    certificateType.DataSource = BL.CertificateType.GetEquivalenceCertificateType();
                    certificateType.DataValueField = "ID";
                    certificateType.DataENTextField = "Title";
                    certificateType.DataARTextField = "TitleAr";
                    // certificateType.ValidationGroup = "Submit";
                    certificateType.IsRequired = false;
                    certificateType.Title = HelperMethods.LocalizedText("ITWORX_MOEHEWF_SCE", "CertificateTypeTitle", (uint)LCID);
                    certificateType.BingDDL();

                    ddl_NatCat.Items.Insert(0, new ListItem(HelperMethods.LocalizedText("ITWORX_MOEHEWF_UCE", "ChooseValue", (uint)LCID), "-1"));
                    ddl_NatCat.AppendDataBoundItems = true;
                    var nationalCategories = NationalityCategory.GetAll().Where(x => x.ID == 1 || x.ID == 6).ToList();

                    HelperMethods.BindDropDownList(ref ddl_NatCat, nationalCategories, "ID", "ArabicTitle", "EnglishTitle", (int)SPContext.Current.Web.Language);

                    ddl_Nationality.Items.Insert(0, new ListItem(HelperMethods.LocalizedText("ITWORX_MOEHEWF_UCE", "ChooseValue", (uint)LCID), "-1"));
                    ddl_Nationality.AppendDataBoundItems = true;
                    if (LCID == (int)Language.English)
                    {
                        HelperMethods.BindDropDownList(ref ddl_Nationality, Nationality.GetAll().OrderBy(n => n.EnglishTitle), "ID", "ArabicTitle", "EnglishTitle", (int)SPContext.Current.Web.Language);
                    }
                    else
                    {
                        HelperMethods.BindDropDownList(ref ddl_Nationality, Nationality.GetAll().OrderBy(n => n.ArabicTitle), "ID", "ArabicTitle", "EnglishTitle", (int)SPContext.Current.Web.Language);
                    }

                    ddl_Gender.Items.Insert(0, new ListItem(HelperMethods.LocalizedText("ITWORX_MOEHEWF_UCE", "ChooseValue", (uint)LCID), "-1"));
                    ddl_Gender.Items.Insert(1, new ListItem(HelperMethods.LocalizedText("ITWORX_MOEHEWF_SCE", "Male", (uint)LCID), "M"));
                    ddl_Gender.Items.Insert(2, new ListItem(HelperMethods.LocalizedText("ITWORX_MOEHEWF_SCE", "Female", (uint)LCID), "F"));

                    ddl_LastAcademicYear.Items.Insert(0, new ListItem(HelperMethods.LocalizedText("ITWORX_MOEHEWF_UCE", "ChooseValue", (uint)LCID), "-1"));
                    ddl_LastAcademicYear.AppendDataBoundItems = true;
                    HelperMethods.BindDropDownList(ref ddl_LastAcademicYear, getCertificateYearsReverse(), "Key", "Value", "Value", (int)SPContext.Current.Web.Language);

                    //using (SCEContextDataContext ctx = new SCEContextDataContext(SPContext.Current.Site.RootWeb.Url))
                    //{
                    //    var fileNames = ctx.FileNameList.Where(f => f.NationalityCategoryId.Contains(Convert.ToInt32(ddl_NatCat.SelectedValue)));
                    //    HelperMethods.BindDropDownList(ref dropFileUpload, fileNames, "ID", "TitleAr", "Title", LCID);
                    //    dropFileUpload.Items.Insert(0, new ListItem(HelperMethods.LocalizedText("ITWORX_MOEHEWF_UCE", "ChooseValue", (uint)LCID), "-1"));
                    //}

                });
            }
            catch (Exception ex)
            {
                Logging.GetInstance().LogException(ex);
            }
            finally
            {
                Logging.GetInstance().Debug("Exit EditRequestUserControl.bindDDLS");
            }

        }

        void BindAttachments(int maxFiles, string groupName)
        {
            Logging.GetInstance().Debug("Entering NewRequestUserControl.BindAttachments");
            try
            {
                using (SCEContextDataContext ctx = new SCEContextDataContext(SPContext.Current.Site.RootWeb.Url))
                {
                    AttachmentsLookupListFieldsContentType attachmentInfo = ctx.AttachmentsLookupList.Where(a => a.Group == ITWORX.MOEHEWF.SCE.Utilities.Constants.NewSCEAttachments).FirstOrDefault();
                    if (attachmentInfo != null)
                    {
                        FileUp1.Group = groupName;//attachmentInfo.Group;
                        FileUp1.HasOptions = true;
                        FileUp1.DocumentLibraryName = attachmentInfo.DocumentLibraryName;
                        FileUp1.IsRequired = (bool)attachmentInfo.IsRequired;
                        //FileUp1.MaxFileNumber = (int)attachmentInfo.MaxFileNumber;
                        FileUp1.MaxFileNumber = maxFiles;
                        FileUp1.MaxSize = (int)attachmentInfo.MaxSize;
                        FileUp1.SupportedExtensions = attachmentInfo.SupportedExtensions;
                        FileUp1.DocLibWebUrl = attachmentInfo.DocLibWebUrl;
                        //FileUp1.Title = LCID == (int)Language.English ? attachmentInfo.Title : attachmentInfo.TitleAr;
                        FileUp1.RequiredValidationMessage = LCID == (int)Language.English ? attachmentInfo.RequiredValidationMessage : attachmentInfo.RequiredValidationMessageAr;
                        FileUp1.LookupFieldName = attachmentInfo.LookupFieldName;
                        FileUp1.LookupFieldValue = Convert.ToInt32(requestID);
                        FileUp1.FileExtensionValidation = HelperMethods.LocalizedText("ITWORX_MOEHEWF_SCE", "FileExtensionValidation", (uint)LCID); //"Supported file extensions are jpg,pdf,png";
                        FileUp1.FileNumbersValidationMsg = string.Format(HelperMethods.LocalizedText("ITWORX_MOEHEWF_SCE", "FileNumbersValidationMsg", (uint)LCID), FileUp1.MaxFileNumber); //"You can't upload more than " + FileUp1.MaxFileNumber + " files";
                        FileUp1.FileExistsValidationMsg = HelperMethods.LocalizedText("ITWORX_MOEHEWF_SCE", "FileExistsValidationMsg", (uint)LCID); //"File exists with the same name";
                        FileUp1.DropClientID = dropFileUpload.Client_ID;
                        FileUp1.TextBoxClientID = dropFileUpload.TextBoxClient_ID;
                        FileUp1.LblTextBoxClientID = dropFileUpload.LblTxtNewOption_ID;
                        FileUp1.ReqDropClientID = lblRequiredDrop.ClientID;
                        FileUp1.ValidqationGroup = "Submit";
                        FileUp1.LabelRequiredDrop = lbldropFileUpload.ClientID;
                        FileUp1.DisplayMode = true;
                        FileUp1.LookupDocumentLibraryName = Utilities.Constants.FileName;
                        DocumentLibrary = FileUp1.DocumentLibraryName;
                        WebUrl = FileUp1.DocLibWebUrl;
                        LookupFieldName = FileUp1.LookupFieldName;

                        //RequestId =Convert.ToInt32(requestID);
                        //DropClientId = dropFileUpload.Client_ID;
                        //TextBoxClientID = dropFileUpload.TextBoxClient_ID;
                        //ReqDropClientID = lblRequiredDrop.ClientID;
                        //LabelRequiredDrop = lbldropFileUpload.ClientID;


                    }
                }
            }
            catch (Exception ex)
            {
                Logging.GetInstance().LogException(ex);
            }
            finally
            {
                Logging.GetInstance().Debug("Exit NewRequestUserControl.BindAttachments");
            }
        }

        void BindApplicant()
        {
            if (isEmpAsApplicant)
            {
                divQatarID.Visible = false;
                lbl_QatarID.Visible = false;
                txt_QatarID.Visible = false;
                QatarIDValidator.Enabled = false;
                QatarIDValidator.Visible = false;
                //lbl_QatarIDValidat.Visible = false;
                lblTempQatarID.Visible = true;
                txtTempQatarID.Visible = true;
                passportContainer.Visible = true;
                lbl_PassPort.Visible = true;
                txt_PassPort.Visible = true;
                valPassport.Visible = true;
                valPassport.Enabled = true;
                ddl_Nationality.Enabled = true;
                ddl_Gender.Enabled = true;
                //txtApplicantName.Enabled = true;
                txt_birthDate.Enabled = true;
                valBirthDate.Enabled = true;
                valBirthDate.Visible = true;
                valGender.Enabled = true;
                valGender.Visible = true;
                valName.Enabled = true;
                valName.Visible = true;
                txt_Name.Enabled = true;
            }
        }

        protected void valCertificateType_ServerValidate(object source, ServerValidateEventArgs args)
        {
            if (certificateType.SelectedValue == "-1" && ddl_ScholasticLevel.SelectedValue == "13")
            {
                args.IsValid = false;
            }
            else
            {
                args.IsValid = true;
            }
        }

        protected void serverValidateAlevel(object sender, ServerValidateEventArgs e)
        {
            string crtTypeVal = certificateType.SelectedValue;
            List<SCEIB> ibList = JsonConvert.DeserializeObject<List<SCEIB>>(IBList_HF.Value);
            List<SCEIGCSE> AlevelListList = JsonConvert.DeserializeObject<List<SCEIGCSE>>(ALevel_HF.Value);
            if (crtTypeVal == "1" && AlevelListList.Count < 2)
            {
                e.IsValid = false;
            }
        }

        protected void serverValidateOlevel(object sender, ServerValidateEventArgs e)
        {
            string crtTypeVal = certificateType.SelectedValue;
            List<SCEIGCSE> OlevelListList = JsonConvert.DeserializeObject<List<SCEIGCSE>>(OLevel_HF.Value);
            if (crtTypeVal == "1" && OlevelListList.Count < 5)
            {
                e.IsValid = false;
            }
        }

        protected void serverValidateIB(object sender, ServerValidateEventArgs e)
        {
            string crtTypeVal = certificateType.SelectedValue;
            if (crtTypeVal == "2")
            {
                int points = 0;
                List<SCEIB> IBList = JsonConvert.DeserializeObject<List<SCEIB>>(IBList_HF.Value);
                foreach (var item in IBList)
                {
                    points = points + Convert.ToInt32(item.Points);
                }
                if (points < 24)
                    e.IsValid = false;
            }
        }

        private Dictionary<string, string> getCertificateYears()
        {
            string sYear = HelperMethods.GetConfigurationValue(SPContext.Current.Site.Url + Common.Utilities.Constants.HEWebUrl, Common.Utilities.Constants.Configuration, "SchoolingAcademicStudyStart");
            string eYear = HelperMethods.GetConfigurationValue(SPContext.Current.Site.Url + Common.Utilities.Constants.HEWebUrl, Common.Utilities.Constants.Configuration, "SchoolingAcademicStudyEnd");
            Dictionary<string, string> yearsList = new Dictionary<string, string>();
            if (!string.IsNullOrEmpty(sYear) && !string.IsNullOrEmpty(eYear))
            {
                int StartYear = int.Parse(sYear);
                int EndYear = int.Parse(eYear);
                int nextYear = StartYear + 1;
                for (int i = StartYear; i < EndYear; i++)
                {
                    nextYear = i + 1;
                    yearsList.Add(i + " - " + nextYear, i + " - " + nextYear);
                }
            }
            return yearsList;
        }

        private Dictionary<string, string> getCertificateYearsReverse()
        {
            string sYear = HelperMethods.GetConfigurationValue(SPContext.Current.Site.Url + Common.Utilities.Constants.HEWebUrl, Common.Utilities.Constants.Configuration, "SchoolingAcademicStudyStart");
            string eYear = HelperMethods.GetConfigurationValue(SPContext.Current.Site.Url + Common.Utilities.Constants.HEWebUrl, Common.Utilities.Constants.Configuration, "SchoolingAcademicStudyEnd");
            Dictionary<string, string> yearsList = new Dictionary<string, string>();
            if (!string.IsNullOrEmpty(sYear) && !string.IsNullOrEmpty(eYear))
            {
                int StartYear = int.Parse(sYear);
                int EndYear = int.Parse(eYear);
                int prevYear = EndYear - 1;
                for (int i = EndYear; i > StartYear; i--)
                {
                    prevYear = i - 1;
                    yearsList.Add(i + " - " + prevYear, i + " - " + prevYear);
                }
            }
            return yearsList;
        }

        protected void btnFinishButton_Click(object sender, EventArgs e)
        {
            if (Page.IsValid)
            {
                SCEContextDataContext ctx = new SCEContextDataContext(SPContext.Current.Site.RootWeb.Url);
                SCERequestsListFieldsContentType request = ctx.SCERequestsList.ScopeToFolder("", true).Where(r => r.Id == requestID && r.LoginName == LoginName).FirstOrDefault();
                if (request != null)
                {
                    bool isEmp = isEmpAsApplicant;
                    // request.MobileNumber = txtMobileNumber.Text;
                    //request.Email = txtEmail.Text;
                    // request.ApplicantOfficialName = txtApplicantName.Text;
                    if (isEmp)
                    {
                        request.StdQatarID = txtTempQatarID.Text;
                        request.StdPassportNum = txt_PassPort.Text;
                        request.StdBirthDate = txt_birthDate.Text;
                        request.StdName = txt_Name.Text;
                        request.StdNationality = ctx.NationalityList.Where(n => n.Id == int.Parse(ddl_Nationality.SelectedValue)).FirstOrDefault();
                        request.StdNationalityCatId = Convert.ToInt32(ddl_NatCat.SelectedValue);
                        request.StdGender = ddl_Gender.SelectedValue;
                        request.StdPrintedName = txt_PrintedName.Text;
                    }
                    else
                    {
                        request.StdQatarID = txt_QatarID.Text;
                        request.StdPassportNum = txt_PassPort.Text;
                        if (!string.IsNullOrEmpty(hdnBirthDate.Value))
                            request.StdBirthDate = hdnBirthDate.Value;
                        if (!string.IsNullOrEmpty(hdnStudentName.Value))
                            request.StdName = hdnStudentName.Value;
                        if (!string.IsNullOrEmpty(stdNationality_hf.Value))
                        {
                            request.StdNationality = ctx.NationalityList.Where(n => n.ISOCode == stdNationality_hf.Value).FirstOrDefault();
                        }
                        request.StdNationalityCatId = Convert.ToInt32(ddl_NatCat.SelectedValue);
                        if (!string.IsNullOrEmpty(stdGender_hf.Value))
                            request.StdGender = stdGender_hf.Value;
                        request.StdPrintedName = txt_PrintedName.Text;
                    }
                    request.CertificateResourceId = (certificateResource.SelectedValue == "-2") ? null : (int?)Convert.ToInt32((certificateResource.SelectedValue));
                    request.OtherCertificateResource = (certificateResource.SelectedValue == "-2") ? certificateResource.OtherValue : string.Empty;
                    request.SchoolType = (schooleType.SelectedValue == "-2") ? null : ctx.SchoolType.Where(n => n.Id == Convert.ToInt32(schooleType.SelectedValue)).FirstOrDefault();
                    request.OtherSchoolType = (schooleType.SelectedValue == "-2") ? schooleType.OtherValue : string.Empty;
                    request.PrevSchool = txt_PrevSchool.Text;
                    request.LastScholasticLevel = (ddl_ScholasticLevel.SelectedValue == "-1") ? null : ctx.ScholasticLevel.Where(n => n.Id == Convert.ToInt32(ddl_ScholasticLevel.SelectedValue)).FirstOrDefault();
                    request.LastAcademicYear = ddl_LastAcademicYear.SelectedValue != "-1" ? ddl_LastAcademicYear.SelectedValue : string.Empty;
                    request.RegisteredScholasticLevel = (ddl_GoingToClass.SelectedValue == "-1") ? null : ctx.ScholasticLevel.Where(n => n.Id == Convert.ToInt32(ddl_GoingToClass.SelectedValue)).FirstOrDefault();
                    request.CertificateType = (certificateType.SelectedValue == "-2") ? null : ctx.CertificateType.Where(n => n.Id == Convert.ToInt32(certificateType.SelectedValue)).FirstOrDefault();
                    request.OtherCertificateType = (certificateType.SelectedValue == "-2") ? certificateType.OtherValue : string.Empty;
                    request.SchoolSystem = (ddlSchoolingSystem.SelectedValue == "-1") ? null : ctx.SchoolSystem.Where(n => n.Id == Convert.ToInt32(ddlSchoolingSystem.SelectedValue)).FirstOrDefault();
                    request.SchoolSystemAr = (ddlSchoolingSystem.SelectedValue == "-1") ? null : ctx.SchoolSystem.Where(n => n.Id == Convert.ToInt32(ddlSchoolingSystem.SelectedValue)).FirstOrDefault();
                    request.OtherSchoolSystem = ddlSchoolingSystem.SelectedValue == "-2" ? ddlSchoolingSystem.OtherValue : string.Empty;
                    request.EquivalencyPurpose = (ddlEquiPurpose.SelectedValue == "-1") ? null : ctx.EquivalencyPurpose.Where(n => n.Id == Convert.ToInt32(ddlEquiPurpose.SelectedValue)).FirstOrDefault();
                    request.EquivalencyPurposeAr = (ddlEquiPurpose.SelectedValue == "-1") ? null : ctx.EquivalencyPurpose.Where(n => n.Id == Convert.ToInt32(ddlEquiPurpose.SelectedValue)).FirstOrDefault();
                    request.TotalPassedYears = txtPassedYears.Text;
                    string folderUrl = DateTime.Now.ToString("yyyy/MM/dd");
                    if (certificateType.SelectedValue == "1")
                    {
                        var oldSCEIGCSE = ctx.SCEIGCSEList.ScopeToFolder("", true).ToList().Where(x => x.RequestID == ctx.SCERequests.ScopeToFolder("", true).Where(r => r.Id == requestID).FirstOrDefault()).ToList();
                        if (oldSCEIGCSE.Count > 0)
                        {
                            ctx.SCEIGCSE.DeleteAllOnSubmit(oldSCEIGCSE);
                        }
                        SPList sPSCEIGCSEList = SPContext.Current.Site.RootWeb.Lists.TryGetList("SCEIGCSE");
                        SPFolder SCEIGCSEFolder = ITWORX.MOEHEWF.Common.Utilities.BusinessHelper.CreateFolderInternal(sPSCEIGCSEList, sPSCEIGCSEList.RootFolder, folderUrl);
                        List<SCEIGCSE> AlevelListList = JsonConvert.DeserializeObject<List<SCEIGCSE>>(ALevel_HF.Value);
                        List<SCEIGCSE> OlevelListList = JsonConvert.DeserializeObject<List<SCEIGCSE>>(OLevel_HF.Value);
                        foreach (var item in AlevelListList)
                        {
                            ctx.SCEIGCSE.InsertOnSubmit(new SCEIGCSEListFieldsContentType()
                            {
                                RequestID = ctx.SCERequests.ScopeToFolder("", true).Where(r => r.Id == requestID).FirstOrDefault(),
                                Code = item.Code,
                                Title = item.Title,
                                Average = item.Avrage,
                                Type = Type.ALevel,
                                Path = SCEIGCSEFolder.Url
                            });
                        }

                        foreach (var item in OlevelListList)
                        {
                            ctx.SCEIGCSE.InsertOnSubmit(new SCEIGCSEListFieldsContentType()
                            {
                                RequestID = ctx.SCERequests.ScopeToFolder("", true).Where(r => r.Id == requestID).FirstOrDefault(),
                                Code = item.Code,
                                Title = item.Title,
                                Average = item.Avrage,
                                Type = Type.Olevel,
                                Path = SCEIGCSEFolder.Url
                            });
                        }
                    }
                    else if (certificateType.SelectedValue == "2")
                    {
                        var oldSCEIB = ctx.SCEIBList.ScopeToFolder("", true).ToList().Where(ib => ib.RequestID == ctx.SCERequests.ScopeToFolder("", true).Where(r => r.Id == requestID).FirstOrDefault()).ToList();
                        if (oldSCEIB.Count > 0)
                        {
                            ctx.SCEIB.DeleteAllOnSubmit(oldSCEIB);
                        }
                        SPList sPIbList = SPContext.Current.Site.RootWeb.Lists.TryGetList("SCEIB");
                        SPFolder ibFolder = ITWORX.MOEHEWF.Common.Utilities.BusinessHelper.CreateFolderInternal(sPIbList, sPIbList.RootFolder, folderUrl);
                        List<SCEIB> ibList = JsonConvert.DeserializeObject<List<SCEIB>>(IBList_HF.Value);
                        foreach (var ib in ibList)
                        {
                            ctx.SCEIB.InsertOnSubmit(new SCEIBListFieldsContentType()
                            {
                                RequestID = ctx.SCERequests.ScopeToFolder("", true).Where(r => r.Id == requestID).FirstOrDefault(),
                                Title = ib.Title,
                                Level = ctx.Levels.Where(n => n.Title == ib.Level).FirstOrDefault(),
                                PointCount = ib.Points,
                                Path = ibFolder.Url,
                                Code = ib.Code

                            });
                        }
                    }
                    // request.RequestStatus = ctx.RequestStatus.Where(x => x.Id == Convert.ToInt32(ITWORX.MOEHEWF.Common.Utilities.RequestStatus.SCESubmitted)).FirstOrDefault();
                    //CultureInfo english = CultureInfo.GetCultureInfo("en-US");
                    //request.SubmitDate = DateTime.Parse(DateTime.Now.ToString(english), CultureInfo.InvariantCulture);
                    ctx.SubmitChanges();
                    FileUp1.SaveAttachments();
                }
                if (Session["ClarificationId"] != null)
                {
                    SPUtility.Redirect(SPContext.Current.Web.Url + "/Pages/SCEClarificationRequestDetails.aspx?ClarId=" + Session["ClarificationId"].ToString() + "&RequestId=" + requestID + "&Edit=2", SPRedirectFlags.DoNotEndResponse, HttpContext.Current);
                }
                else
                {
                   int? clarId = ctx.SCEClarificationsRequestsList.ScopeToFolder("", true).Where(c => c.RequestIDId.Value == requestID && c.ClarificationReply==null).FirstOrDefault().Id;
                    SPUtility.Redirect(SPContext.Current.Web.Url + "/Pages/SCEClarificationRequestDetails.aspx?ClarId=" +clarId + "&RequestId=" + requestID + "&Edit=2", SPRedirectFlags.DoNotEndResponse, HttpContext.Current);
                }
            }
        
        }

        protected void btnBack_Click(object sender, EventArgs e)
        {
            if (Session["ClarificationId"] != null)
            {
                SPUtility.Redirect(SPContext.Current.Web.Url + "/Pages/SCEClarificationRequestDetails.aspx?ClarId=" + Session["ClarificationId"].ToString() + "&RequestId=" + requestID + "&Edit=1", SPRedirectFlags.DoNotEndResponse, HttpContext.Current);
            }
            else
            {
                SPUtility.Redirect(SPContext.Current.Web.Url + "/Pages/SCEClarificationsListing.aspx?RequestId=" + requestID, SPRedirectFlags.DoNotEndResponse, HttpContext.Current);
            }
        }

        protected void custIBSubjects_ServerValidate(object source, ServerValidateEventArgs args)
        {
            string crtTypeVal = certificateType.SelectedValue;
            if (crtTypeVal == "2")
            {
                int SLCount = 0;
                int HLCount = 0;
                List<SCEIB> IBList = JsonConvert.DeserializeObject<List<SCEIB>>(IBList_HF.Value);
                foreach (var item in IBList)
                {
                    if (item.Level == "HL")
                        HLCount++;
                    else
                        SLCount++;

                }
                if ((SLCount == 4 && HLCount == 2) || (SLCount == 3 && HLCount == 3))
                    args.IsValid = true;
                else
                    args.IsValid = false;
            }
        }
        //On dropdown changes 

        //public void BindAttachmentsProperties(Entities.FileUploadProperties fileUploadProperties)
        //{
        //    Logging.GetInstance().Debug("Entering EditRequestUserControl.BindAttachmentsProperties");
        //    try
        //    {
        //        using (SCEContextDataContext ctx = new SCEContextDataContext(SPContext.Current.Site.RootWeb.Url))
        //        {

        //            AttachmentsLookupListFieldsContentType attachmentInfo = ctx.AttachmentsLookupList.Where(a => a.Group == ITWORX.MOEHEWF.SCE.Utilities.Constants.NewSCEAttachments).FirstOrDefault();
        //            if (attachmentInfo != null)
        //            {

        //                FileUp1.HasOptions = true;
        //                FileUp1.DocumentLibraryName = attachmentInfo.DocumentLibraryName;
        //                FileUp1.IsRequired = (bool)attachmentInfo.IsRequired;
        //                //FileUp1.MaxFileNumber = (int)attachmentInfo.MaxFileNumber;

        //                FileUp1.MaxSize = (int)attachmentInfo.MaxSize;
        //                FileUp1.SupportedExtensions = attachmentInfo.SupportedExtensions;
        //                FileUp1.DocLibWebUrl = attachmentInfo.DocLibWebUrl;
        //                //FileUp1.Title = LCID == (int)Language.English ? attachmentInfo.Title : attachmentInfo.TitleAr;
        //                FileUp1.RequiredValidationMessage = fileUploadProperties.language == (int)Language.English ? attachmentInfo.RequiredValidationMessage : attachmentInfo.RequiredValidationMessageAr;
        //                FileUp1.LookupFieldName = attachmentInfo.LookupFieldName;
        //                FileUp1.LookupFieldValue = fileUploadProperties.requestId;
        //                FileUp1.FileExtensionValidation = HelperMethods.LocalizedText("ITWORX_MOEHEWF_SCE", "FileExtensionValidation", (uint)fileUploadProperties.language); //"Supported file extensions are jpg,pdf,png";

        //                FileUp1.FileExistsValidationMsg = HelperMethods.LocalizedText("ITWORX_MOEHEWF_SCE", "FileExistsValidationMsg", (uint)fileUploadProperties.language); //"File exists with the same name";
        //                FileUp1.DropClientID = fileUploadProperties.dropClientID;
        //                FileUp1.TextBoxClientID = fileUploadProperties.textBoxClientID;
        //                FileUp1.ReqDropClientID = fileUploadProperties.reqDropClientID;
        //                FileUp1.ValidqationGroup = "Submit";
        //                FileUp1.LabelRequiredDrop = fileUploadProperties.labelRequiredDrop;
        //                FileUp1.DisplayMode = true;
        //            }

        //            if ((fileUploadProperties.goingClassId == "14" || string.IsNullOrEmpty(fileUploadProperties.goingClassId))
        //                         && !string.IsNullOrEmpty(fileUploadProperties.certificateTypeId))
        //            //ddl_ScholasticLevel.SelectedValue == "13")
        //            {
        //                var selectedCountry = ctx.CountryOfStudyList.Where(c => c.Id == int.Parse(fileUploadProperties.countryId)).FirstOrDefault();

        //                var fileNames = ctx.FileNameList.Where(f => f.CertificateTypeId.Contains(Convert.ToInt32(fileUploadProperties.certificateTypeId)) && f.MOEHECountryType.Contains(selectedCountry.MOEHECountryType.ToString()));
        //               var certificateTypeItem= ctx.CertificateTypeList.Where(f => f.Id == Convert.ToInt32(fileUploadProperties.certificateTypeId)).SingleOrDefault();

        //                FileUp1.Group = certificateTypeItem.Title+ selectedCountry.MOEHECountryType.ToString();
        //                FileUp1.MaxFileNumber = fileNames.Count();
        //                FileUp1.FileNumbersValidationMsg = string.Format(HelperMethods.LocalizedText("ITWORX_MOEHEWF_SCE", "FileNumbersValidationMsg", (uint)fileUploadProperties.language), FileUp1.MaxFileNumber);
        //               // Group = FileUp1.Group;
        //            }
        //            else
        //            {
        //                var fileNames = ctx.FileNameList.Where(f => f.NationalityCategoryId.Contains(Convert.ToInt32(fileUploadProperties.natCatId)));
        //                var catItem = ctx.NationalityCategoryList.Where(c => c.Id == int.Parse(fileUploadProperties.natCatId)).SingleOrDefault();
        //                FileUp1.Group =  catItem.Title ;
        //                FileUp1.MaxFileNumber = fileNames.Count();
        //                FileUp1.FileNumbersValidationMsg = string.Format(HelperMethods.LocalizedText("ITWORX_MOEHEWF_SCE", "FileNumbersValidationMsg", (uint)fileUploadProperties.language), FileUp1.MaxFileNumber);
        //               // Group = FileUp1.Group;
        //            }

        //            FileUp1.DeleteAttachments();
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        Logging.GetInstance().LogException(ex);
        //    }
        //    finally
        //    {
        //        Logging.GetInstance().Debug("Exit EditRequestUserControl.BindAttachmentsProperties");
        //    }
        //}


    }
}
