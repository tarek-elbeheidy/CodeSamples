using ITWORX.MOEHE.Utilities;
using ITWORX.MOEHEWF.SCE.Entities;
using Microsoft.SharePoint;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;

namespace ITWORX.MOEHEWF.SCE.CONTROLTEMPLATES.ITWORX.MOEHEWF.SCE
{
    public partial class DisplayRequestDetails : UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            //if (!Page.IsPostBack)
            //{
            BindRequest();

            //}
        }
        public bool isEmpAsApplicant { get { return HelperMethods.InGroup(Common.Utilities.Constants.EmployeeAsApplicant); } }

        public string LoginName { get { return SPContext.Current.Web.CurrentUser.LoginName; } }

        public int? requestID
        {
            get
            {
                if (Request.QueryString["requestID"] != null)
                {
                    return Convert.ToInt32(Request.QueryString["requestID"]);
                }
                else if (ViewState["requestID"] != null)
                {
                    return Convert.ToInt32(ViewState["requestID"]);
                }
                return null;
            }
            set
            {
                ViewState["requestID"] = value;
            }
        }

        void BindRequest()
        {
            SPSecurity.RunWithElevatedPrivileges(delegate ()
            {
                int lcid = SPContext.Current.Web.UICulture.LCID;
                SCEContextDataContext ctx = new SCEContextDataContext(SPContext.Current.Site.RootWeb.Url);
                requestID = Convert.ToInt32(ViewState["requestID"]);
                if (requestID != null)
                {
                    SCERequestsListFieldsContentType request = ctx.SCERequestsList.ScopeToFolder("", true).Where(r => r.Id == requestID && r.LoginName == LoginName).FirstOrDefault();
                    if (request != null)
                    {
                        if (isEmpAsApplicant)
                        {
                            lbl_PassPort.Visible = true; 
                            lblPassPortVal.Visible = true;
                            lblPassPortVal.Text = request.StdPassportNum;
                        }
                        lblQatarIDVal.Text = request.StdQatarID;
                        lblbirthDateVal.Text = request.StdBirthDate;
                        lblNameVal.Text = request.StdName;
                        if (request.StdNationality != null)
                        {
                            //lblNationalityVal.Text = lcid == 1025 ? ctx.NationalityList.Where(n => n.Id == request.StdNationality.Id).FirstOrDefault().TitleAr
                            //: ctx.NationalityList.Where(n => n.Id == request.StdNationality.Id).FirstOrDefault().Title;
                        }
                        lblNatCatVal.Text = request.StdNationalityCatTitle;
                        lblGenderVal.Text= request.StdGender == "M" ? lcid == 1025 ? "ذكر" : "Male" : lcid == 1025 ? "انثى" : "Female";
                        txt_PrintedName.Text = request.StdPrintedName;
                        if (string.IsNullOrEmpty(request.OtherCertificateResource))
                        {
                            certificateResource.Text = request.CertificateResourceTitle;
                        }
                        else
                        {
                            certificateResource.Text = request.OtherCertificateResource;
                        }
                        if (request.SchoolType == null)
                        {
                            schooleType.Text = request.OtherSchoolType;
                        }
                        else
                        {
                            schooleType.Text = lcid == 1025 ? ctx.SchoolTypeList.Where(s => s.Id == request.SchoolType.Id).FirstOrDefault().TitleAr : request.SchoolType.Title;
                        }
                        txt_PrevSchool.Text = request.PrevSchool;
                        lblSchoolSystemVal.Text = request.SchoolSystem != null ? lcid == 1025 ? ctx.SchoolSystemList.Where(s=>s.Id== request.SchoolType.Id).FirstOrDefault().TitleAr 
                            : request.SchoolSystem.Title : "";
                        ddl_ScholasticLevel.Text = request.LastScholasticLevel != null ? lcid == 1025? ctx.ScholasticLevelList.Where(s=>s.Id == request.LastScholasticLevel.Id).FirstOrDefault().TitleAr: request.LastScholasticLevel.Title : "";
                        ddl_LastAcademicYear.Text = string.IsNullOrEmpty(request.LastAcademicYear) ? "" : request.LastAcademicYear;
                        lblEquiPurposeVal.Text = request.EquivalencyPurpose != null ? lcid == 1025 ? ctx.EquivalencyPurposeList.Where(e=>e.Id== request.EquivalencyPurposeAr.Id).FirstOrDefault().TitleAr 
                            : request.EquivalencyPurpose.Title : "";
                        ddl_GoingToClass.Text = request.RegisteredScholasticLevel != null ? lcid == 1025? ctx.ScholasticLevelList.Where(g => g.Id == request.RegisteredScholasticLevel.Id).FirstOrDefault().TitleAr: request.RegisteredScholasticLevel.Title : "";
                        
                        if (request.CertificateType == null)
                        {
                            certificateType.Text = request.OtherCertificateType;
                        }
                        //else if (request.CertificateType == null && !string.IsNullOrEmpty(request.OtherCertificateType))
                        //{
                        //    certificateType.SelectedValue = "-2";
                        //    certificateType.OtherValue = request.OtherCertificateType;
                        //}
                        else if (request.CertificateType != null)
                        {
                            certificateType.Text = lcid == 1025 ? ctx.CertificateTypeCT.Where(c => c.Id == request.CertificateType.Id).FirstOrDefault().TitleAr : request.CertificateType.Title;
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
                                        Level=item.Level.Title,
                                        //LevelTitle = item.Level.Title,
                                        Points = item.PointCount
                                    });
                                }
                                IBList_HF.Value = JsonConvert.SerializeObject(SCEIBList);
                            }
                        }
                    }

                }

            });
        }
    }
}
