using ITWORX.MOEHE.Utilities;
using ITWORX.MOEHE.Utilities.Logging;
using ITWORX.MOEHEWF.SCE.BL;
using ITWORX.MOEHEWF.SCE.Entities;
using ITWORX.MOEHEWF.SCE.Utilities;
using Microsoft.SharePoint;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;

namespace ITWORX.MOEHEWF.SCE.WebParts.ViewRequest
{
    public partial class ViewRequestUserControl : UserControlBase
    {

        protected global::ITWORX.MOEHEWF.Common.CONTROLTEMPLATES.ITWORX.MOEHEWF.Common.ClientSideFileUpload FileUploadDisplay;

        protected global::ITWORX.MOEHEWF.Common.CONTROLTEMPLATES.ITWORX.MOEHEWF.Common.ClientSideFileUpload ClientSideFileUpload1;

        protected void Page_Load(object sender, EventArgs e)
        {
            BindDisplayRequest();
        }

        void BindDisplayRequest()
        {
            HttpContext backupContext = HttpContext.Current;
            try
            {
                Logging.GetInstance().Debug("Enter ViewRequestUserControl.BindDisplayRequest");
                //    SPSecurity.RunWithElevatedPrivileges(delegate ()
                //{
                //SCEContextDataContext ctx = new SCEContextDataContext(SPContext.Current.Site.RootWeb.Url);



                string loginName = SPContext.Current.Web.CurrentUser.LoginName; // if you need it in your code
                string rootWebUrl = SPContext.Current.Site.RootWeb.Url;
                string currentUser = SPContext.Current.Web.CurrentUser.LoginName.ToLower();
                bool isEmpAsApplicant = HelperMethods.InGroup(Common.Utilities.Constants.EmployeeAsApplicant);
                bool isApplicant = HelperMethods.InGroup(Common.Utilities.Constants.Applicants);
                HttpContext.Current = null;

                SPSecurity.RunWithElevatedPrivileges(() =>
                {
                    using (SCEContextDataContext ctx = new SCEContextDataContext(rootWebUrl))// the url of the web is outside the Datcontext
                        {


                        int requestID =0;
                        if (Page.Request.QueryString["RequestId"] != null)
                        {
                            requestID = Convert.ToInt32(Page.Request.QueryString["RequestId"]);
                        }
                      

                        if (requestID != 0)
                        {
                            SCERequestsListFieldsContentType request = ctx.SCERequestsList.ScopeToFolder("", true).Where(r => r.Id == requestID /*&& r.LoginName == loginName*/).FirstOrDefault();
                            if (request != null)
                            {
                                NationalityCategoryItem nationalityCategoryItem = null;
                                //if (isEmpAsApplicant)
                                //{
                                //    divPassportContainer.Style.Add("display", "block");
                                //    lblPassPortDisplay.Visible = true;
                                //    lblPassPortVal.Visible = true;
                                //    lblPassPortVal.Text = request.StdPassportNum;
                                //     Label10.Text= HelperMethods.LocalizedText("ITWORX_MOEHEWF_SCE", "TempQatarID", (uint)LCID); 
                                //}
                                //else
                                //{
                                //    Label10.Text = HelperMethods.LocalizedText("ITWORX_MOEHEWF_SCE", "QatarID", (uint)LCID);
                                //}
                                //Noha
                                if(isApplicant)
                                {
                                    Label10.Text = HelperMethods.LocalizedText("ITWORX_MOEHEWF_SCE", "QatarID", (uint)LCID);
                                }
                                else
                                {
                                   
                                   //Employee created request
                                   if (request.LoginName.ToLower() == currentUser && request.IsEmployee == IsEmployee.Yes)
                                    {
                                        divPassportContainer.Style.Add("display", "block");
                                        lblPassPortDisplay.Visible = true;
                                        lblPassPortVal.Visible = true;
                                        lblPassPortVal.Text = request.StdPassportNum;
                                        Label10.Text = HelperMethods.LocalizedText("ITWORX_MOEHEWF_SCE", "TempQatarID", (uint)LCID);
                                    }
                                   else
                                    {
                                        Label10.Text = HelperMethods.LocalizedText("ITWORX_MOEHEWF_SCE", "QatarID", (uint)LCID);
                                    }


                                }
                                //Noha
                                lblQatarIDVal.Text = request.StdQatarID;
                                //lblbirthDateVal.Text = ExtensionMethods.QatarFormatedDate(DateTime.Parse(request.StdBirthDate));
                                lblbirthDateVal.Text = request.StdBirthDate;
                                lblNameVal.Text = request.StdName;
                                if (request.StdNationality != null)
                                {
                                    HttpContext.Current = backupContext;
                                    NationalityItem nationalityItem = NationalityList.GetNationalityById(Convert.ToInt32(request.StdNationality.Id));
                                    // NationalityListFieldsContentType nationalityItem = ctx.NationalityList.ScopeToFolder("", true).Where(n=>n.Id == request.StdNationality.Id.Value).FirstOrDefault();
                                    lblNationalityVal.Text = LCID == (int)Language.English ? nationalityItem.Title : nationalityItem.TitleAr;
                                }
                                if (request.StdNationalityCatId != null)
                                {
                                     nationalityCategoryItem = NationalityCategoryList.GetNationalityCategoryById(Convert.ToInt32(request.StdNationalityCatId));
                                    lblNatCatVal.Text = LCID == (int)Language.English ? nationalityCategoryItem.Title : nationalityCategoryItem.TitleAr;
                                }
                                if (request.StdGender != null)
                                {
                                    lblGenderVal.Text = request.StdGender == "M" ? LCID == (int)Language.English ? "Male" : "ذكر" : LCID == (int)Language.English ? "Female" : "انثى";
                                }
                                lblPrintedNameDisplay.Text = request.StdPrintedName;
                                if (string.IsNullOrEmpty(request.OtherCertificateResource))
                                {
                                    CertificateResourceItem certificateResourceItem = CertificateResourceList.GetCertificateResourceById(Convert.ToInt32(request.CertificateResourceId));
                                    lblcertificateResource.Text = LCID == (int)Language.English ? certificateResourceItem.Title : certificateResourceItem.TitleAr;
                                   
                                }
                                else
                                {
                                    lblcertificateResource.Text = request.OtherCertificateResource;
                                }

                                HttpContext.Current = null;
                                if (request.SchoolType == null)
                                {
                                    lblSchoolTypeVal.Text = request.OtherSchoolType;
                                }
                                else
                                {
                                        //lblSchoolTypeVal.Text = lcid == 1025 ? ctx.SchoolTypeList.Where(s => s.Id == request.SchoolType.Id).FirstOrDefault().TitleAr : request.SchoolType.Title;
                                        lblSchoolTypeVal.Text = LCID == (int)Language.English ? request.SchoolType.Title : ctx.SchoolTypeList.Where(s => s.Id == request.SchoolType.Id).FirstOrDefault().TitleAr;
                                }
                                lblPrevSchool.Text = request.PrevSchool;
                                //lblSchoolSystemVal.Text = request.SchoolSystem != null ? lcid == 1025 ? ctx.SchoolSystemList.Where(s => s.Id == request.SchoolType.Id).FirstOrDefault().TitleAr
                                //    : request.SchoolSystem.Title : "";

                                if (request.SchoolSystem == null && !string.IsNullOrEmpty(request.OtherSchoolSystem))
                                {

                                    lblSchoolSystemVal.Text = request.OtherSchoolSystem;
                                }
                               
                                else
                                {
                                    lblSchoolSystemVal.Text = request.SchoolSystem != null ? LCID == (int)Language.English ? request.SchoolSystem.Title
                                        : ctx.SchoolSystemList.Where(s => s.Id == request.SchoolSystem.Id).FirstOrDefault().TitleAr : "";
                                }
                                    //lblScholasticLevel.Text = request.LastScholasticLevel != null ? lcid == 1025 ? ctx.ScholasticLevelList.Where(s => s.Id == request.LastScholasticLevel.Id).FirstOrDefault().TitleAr : request.LastScholasticLevel.Title : "";
                                    lblScholasticLevel.Text = request.LastScholasticLevel != null ? LCID == (int)Language.English ? request.LastScholasticLevel.Title : ctx.ScholasticLevelList.Where(s => s.Id == request.LastScholasticLevel.Id).FirstOrDefault().TitleAr : "";

                                lblLastAcademicYear.Text = string.IsNullOrEmpty(request.LastAcademicYear) ? "" : request.LastAcademicYear;

                                    //lblEquiPurposeVal.Text = request.EquivalencyPurpose != null ? lcid == 1025 ? ctx.EquivalencyPurposeList.Where(e => e.Id == request.EquivalencyPurposeAr.Id).FirstOrDefault().TitleAr
                                    //    : request.EquivalencyPurpose.Title : "";
                                    lblEquiPurposeVal.Text = request.EquivalencyPurpose != null ? LCID == (int)Language.English ? request.EquivalencyPurpose.Title
                                    : ctx.EquivalencyPurposeList.Where(e => e.Id == request.EquivalencyPurposeAr.Id).FirstOrDefault().TitleAr : "";

                                    //lblGoingToClass.Text = request.RegisteredScholasticLevel != null ? lcid == 1025 ? ctx.ScholasticLevelList.Where(g => g.Id == request.RegisteredScholasticLevel.Id).FirstOrDefault().TitleAr : request.RegisteredScholasticLevel.Title : "";
                                    lblGoingToClass.Text = request.RegisteredScholasticLevel != null ? LCID == (int)Language.English ? request.RegisteredScholasticLevel.Title : ctx.ScholasticLevelList.Where(g => g.Id == request.RegisteredScholasticLevel.Id).FirstOrDefault().TitleAr : "";
                                lblTotalPassedYears.Text = request.TotalPassedYears;
                                if (request.CertificateType == null)
                                {
                                    lblCertificateTypeVal.Text = request.OtherCertificateType;
                                }
                                    //else if (request.CertificateType == null && !string.IsNullOrEmpty(request.OtherCertificateType))
                                    //{
                                    //    certificateType.SelectedValue = "-2";
                                    //    certificateType.OtherValue = request.OtherCertificateType;
                                    //}
                                    else if (request.CertificateType != null)
                                {
                                        //lblCertificateTypeVal.Text = lcid == 1025 ? ctx.CertificateTypeCT.Where(c => c.Id == request.CertificateType.Id).FirstOrDefault().TitleAr : request.CertificateType.Title;
                                        lblCertificateTypeVal.Text = LCID == (int)Language.English ? request.CertificateType.Title : ctx.CertificateTypeCT.Where(c => c.Id == request.CertificateType.Id).FirstOrDefault().TitleAr;
                                    if (request.CertificateType.Id == 1)
                                    {
                                        var oLevels = ctx.SCEIGCSEList.ScopeToFolder("", true).Where(x => x.RequestID.Id == requestID && x.Type == Type.Olevel);

                                        List<SCEIGCSE> OlevelListList = new List<SCEIGCSE>();
                                        foreach (var item in oLevels)
                                        {
                                            OlevelListList.Add(new SCEIGCSE()
                                            {
                                                ID = (int)item.Id,
                                                Avrage = item.Average,
                                                Code = item.Code,
                                                Title = item.Title
                                            });

                                        }
                                        OLevel_HF.Value = JsonConvert.SerializeObject(OlevelListList);
                                        var aLevels = ctx.SCEIGCSEList.ScopeToFolder("", true).Where(x => x.RequestID.Id == requestID && x.Type == Type.ALevel);
                                        List<SCEIGCSE> AlevelListList = new List<SCEIGCSE>();
                                        foreach (var item in aLevels)
                                        {
                                            AlevelListList.Add(new SCEIGCSE()
                                            {
                                                ID = (int)item.Id,
                                                Avrage = item.Average,
                                                Code = item.Code,
                                                Title = item.Title
                                            });
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
                                                Code = item.Code,
                                                Level = item.Level.Title,
                                                //LevelTitle = item.Level.Title,
                                                Points = item.PointCount
                                            });
                                        }
                                        IBList_HF.Value = JsonConvert.SerializeObject(SCEIBList);
                                    }
                                }


                                //Get attachment group
                                if ((request.RegisteredScholasticLevel==null || request.RegisteredScholasticLevel.Id == 14 )
                                && request.CertificateType!=null )
                                {
                                   // string certificateType = string.Empty;
                                    string selectedCountryType = string.Empty;
                                    
                                   // certificateType = LCID == (int)Language.English ? request.CertificateType.Title : ctx.CertificateTypeCT.Where(c => c.Id == request.CertificateType.Id).FirstOrDefault().TitleAr;
                                    
                                    if(request.CertificateResourceId!=null)
                                    {
                                        selectedCountryType = ctx.CountryOfStudyList.Where(c => c.Id == request.CertificateResourceId).FirstOrDefault().MOEHECountryType.ToString();
                                    }
                                  


                                    if (lblCertificateTypeVal.Text.Equals(HelperMethods.LocalizedText("ITWORX_MOEHEWF_SCE", "HighSchool", (uint)LCID))
                                         && selectedCountryType.Equals("Arab"))

                                    {
                                        if (request.SchoolType != null && lblSchoolTypeVal.Text.Equals(HelperMethods.LocalizedText("ITWORX_MOEHEWF_SCE", "Private", (uint)LCID)))
                                        {
                                            FileUploadDisplay.Group = request.CertificateType.Title + selectedCountryType + "Private";
                                        }
                                        else
                                        {
                                          
                                            FileUploadDisplay.Group = request.CertificateType.Title + selectedCountryType;
                                            
                                        }



                                    }
                                    else
                                    {
                                        FileUploadDisplay.Group = request.CertificateType.Title + selectedCountryType;

                                    }


                                }




                                else
                                {
                                   
                                    FileUploadDisplay.Group = nationalityCategoryItem.Title;
                                }


                              
                        AttachmentsLookupListFieldsContentType attachmentInfo = ctx.AttachmentsLookupList.Where(a => a.Group == ITWORX.MOEHEWF.SCE.Utilities.Constants.NewSCEAttachments).FirstOrDefault();
                        if (attachmentInfo != null)
                        {
                            FileUploadDisplay.DisplayMode = false;
                            
                            FileUploadDisplay.HasOptions = false;
                            FileUploadDisplay.DocumentLibraryName = attachmentInfo.DocumentLibraryName;
                           // FileUploadDisplay.MaxFileNumber = (int)attachmentInfo.MaxFileNumber;
                            //FileUploadDisplay.MaxSize = (int)attachmentInfo.MaxSize;
                            //FileUploadDisplay.SupportedExtensions = attachmentInfo.SupportedExtensions;
                            FileUploadDisplay.DocLibWebUrl = attachmentInfo.DocLibWebUrl;
                           // FileUploadDisplay.Title = LCID == (int)Language.English ? attachmentInfo.Title : attachmentInfo.TitleAr;
                            FileUploadDisplay.LookupFieldName = attachmentInfo.LookupFieldName;
                            FileUploadDisplay.LookupFieldValue = Convert.ToInt32(requestID);
                            FileUploadDisplay.LookupDocumentLibraryName = Utilities.Constants.FileName;




                                }
                           }

                        }
                    }

                });
                //HttpContext.Current = backupContext;


            }

            catch (Exception ex)
            {
                Logging.GetInstance().LogException(ex);
            }
            finally
            {
                // resetting the SPContext
                if (HttpContext.Current == null)
                {
                    HttpContext.Current = backupContext;
                }

                Logging.GetInstance().Debug("Exit ViewRequestUserControl.BindDisplayRequest");
            }
        }

    }
}
