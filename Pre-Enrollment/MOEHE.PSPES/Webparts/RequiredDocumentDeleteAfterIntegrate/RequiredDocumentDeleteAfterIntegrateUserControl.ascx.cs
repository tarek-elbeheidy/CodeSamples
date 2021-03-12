using Microsoft.SharePoint;
using MOEHE.PSPES.Models;
using MOEHE.PSPES.Repository;
using System;
using System.Globalization;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;

namespace MOEHE.PSPES.Webparts.RequiredDocumentDeleteAfterIntegrate
{
    public partial class RequiredDocumentDeleteAfterIntegrateUserControl : UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                string SchoolCode = "10097";
                string Term = "2019";
                string Grade = "Grarde";
                bool isMinistryUser = true;
                MOE_SCHOOL_Model SchoolInfo = MOE_SCHOOL_Repository.GetSchoolInfo(Term, SchoolCode, "false").Result;

                //Then View  All schools Document type for specific grade
                if ((uint)CultureInfo.CurrentUICulture.LCID == 1025)
                {
                    BindingUtility.LoadRequiredSupportingDocs(gvRequiredDocuments, new SupportingDocsModel { SchoolCode = SchoolCode, Term = Term, Grade = Grade, SeacrhByTermAndSchoolCodeAndGrade = true }, SchoolInfo, false, isMinistryUser, SPContext.Current.Site.Url);
                }
                else
                {
                    BindingUtility.LoadRequiredSupportingDocs(gvRequiredDocuments, new SupportingDocsModel { SchoolCode = SchoolCode, Term = Term, Grade = Grade, SeacrhByTermAndSchoolCodeAndGrade = true }, SchoolInfo, true, isMinistryUser, SPContext.Current.Site.Url);


                }

            }


            //foreach (GridViewRow item in gvRequiredDocuments.Rows)
            //{
            //    FileUpload RequiredFileUpload = (FileUpload)item.FindControl("lblName");

            //    if (RequiredFileUpload.PostedFile != null)
            //    {


            //        if (RequiredFileUpload.PostedFile.ContentLength > 0)
            //        {
            //            System.IO.Stream strm = RequiredFileUpload.PostedFile.InputStream;

            //            byte[] ApplicantByte = new byte[
            //                    Convert.ToInt32(RequiredFileUpload.PostedFile.ContentLength)];

            //            strm.Read(ApplicantByte, 0, Convert.ToInt32
            //                                               (RequiredFileUpload.PostedFile.ContentLength));
            //            string ApplicantFileExtension = System.IO.Path.GetExtension(RequiredFileUpload.FileName);
            //            string ApplicantReference = string.Format("{0}-{1}{2}", DateTime.Now.Year.ToString(), DateTime.Now.Minute, DateTime.Now.Second);

            //          string FilePath=  SaveToDocumentLibrary(ApplicantByte, ApplicantFileExtension, ApplicantReference);


            //        }
            //    }
            //}
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

                            SPFile ApplicantFile = mylibrary.Files.Add(ApplicantFileName, ApplicantByte);
                            int ApplicantFileID = ApplicantFile.Item.ID;
                            ApplicantFilePath = SPContext.Current.Site.Url + "/" + ApplicantFile.Item.Url.ToString();

                            objWeb.AllowUnsafeUpdates = false;

                        }
                    }

                }
            }));
            return ApplicantFilePath;
        }

    }
}

     
