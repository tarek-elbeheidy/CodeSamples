using ITWORX.MOEHE.Utilities;
using ITWORX.MOEHE.Utilities.Logging;
using ITWORX.MOEHEWF.UCE.Utilities;
using Microsoft.SharePoint;
using Microsoft.SharePoint.Utilities;
using System;
using System.Collections.Generic;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;

namespace ITWORX.MOEHEWF.UCE.WebParts.Sample
{
    public partial class SampleUserControl : UserControlBase
    {
        #region Protected Variables

        /// <summary>
        /// fileUpload control.
        /// </summary>
        /// <remarks>
        /// Auto-generated field.
        /// To modify move field declaration from designer file to code-behind file.
        /// </remarks>
        protected global::ITWORX.MOEHEWF.Common.CONTROLTEMPLATES.ITWORX.MOEHEWF.Common.FileUpload fileUpload;
        #endregion



        //Caching of method return value
        //PARequest requestItem = null;
        //public PARequest RequestItem
        //{ get
        //    {
        //        int requestId = int.Parse(Convert.ToString(Page.Session["PADisplayRequestId"]));
        //        if (requestId != 0 && requestItem==null)
        //        {
        //            requestItem = BL.Request.Reviewer_GetRequestByNumber(requestId, LCID);
        //        }


        //        return requestItem;
        //    }
        //}
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                Logging.GetInstance().Debug("Entering method SampleUserControl.Page_Load");
                using (SPWeb web = new SPSite("http://moehe-dev-01/").OpenWeb())
                {
                    string joins =
                        "<Join Type='INNER' ListAlias='Applicants'>" +
                                    "<Eq>" +
                                        "<FieldRef Name='ApplicantID' RefType='Id'/>" +
                                        "<FieldRef List='Applicants' Name='ID'/>" +
                                    "</Eq>" +
                                    "</Join>"


                   + "<Join Type='INNER' ListAlias='Nationality'>" +
                                    "<Eq>" +
                                        "<FieldRef List='Applicants' Name='Nationality' RefType='Id'/>" +
                                        "<FieldRef List='Nationality' Name='ID'/>" +
                                    "</Eq>" +
                                    "</Join>";

                    string projectedFields =
                        "<Field Name='Applicants_ApplicantName' Type='Lookup' "
                                + "List='Applicants' ShowField='ApplicantName'/>"
                    + "<Field Name='Applicants_QatarID' Type='Lookup' " +
                            "List='Applicants' ShowField='PersonalID'/>"
                   + "<Field Name='Nationality_Title' Type='Lookup' " +
                            "List='Nationality' ShowField='Title'/>"

                            + "<Field Name='Nationality_TitleAr' Type='Lookup' " +
                            "List='Nationality' ShowField='TitleAr'/>";



                    string viewFields = "<FieldRef Name='RequestNumber'/>"
                                        + "<FieldRef Name='Applicants_QatarID'/>"
                                        + "<FieldRef Name='Applicants_ApplicantName'/>"
                                        + "<FieldRef Name='SubmitDate'/>"
                                        + "<FieldRef Name='Nationality_Title'/>"
                                        + "<FieldRef Name='Nationality_TitleAr'/>"
                                        + "<FieldRef Name='Certificates'/>"
                                        + "<FieldRef Name='CountryOfStudy'/>"
                                        + "<FieldRef Name='University'/>"
                                        + "<FieldRef Name='Faculty'/>"
                                        + "<FieldRef Name='Specialization'/>"
                                        + "<FieldRef Name='EntityNeedsEquivalency'/>";

                    SPList customerList = web.Lists["Requests"];
                    SPQuery query = Common.Utilities.BusinessHelper.GetQueryObject(string.Empty, joins, projectedFields, viewFields);
                    SPListItemCollection items = customerList.GetItems(query);
                    foreach (SPListItem item in items)
                    {
                        Label1.Text = "Request Number: " + item["RequestNumber"].ToString();

                        SPFieldLookupValue ApplicantsQatarID = new SPFieldLookupValue(item["Applicants_QatarID"].ToString());
                        Label2.Text = "Qatar ID: " + ApplicantsQatarID.LookupValue;

                        SPFieldLookupValue ApplicantsApplicantName = new SPFieldLookupValue(item["Applicants_ApplicantName"].ToString());
                        Label3.Text = "Applicant Name: " + ApplicantsApplicantName.LookupValue;

                        Label4.Text = "Submission Date: " + item["SubmitDate"].ToString();

                        SPFieldLookupValue NationalityTitle = new SPFieldLookupValue(item["Nationality_Title"].ToString());
                        Label5.Text = "Nationality: " + NationalityTitle.LookupValue;

                        SPFieldLookupValue NationalityTitleAr = new SPFieldLookupValue(item["Nationality_TitleAr"].ToString());
                        Label6.Text = "NationalityAr: " + NationalityTitleAr.LookupValue;

                        SPFieldLookupValue Certificates = new SPFieldLookupValue(item["Certificates"].ToString());
                        Label7.Text = "Certificates: " + Certificates.LookupValue;

                        SPFieldLookupValue CountryOfStudy = new SPFieldLookupValue(item["CountryOfStudy"].ToString());
                        Label8.Text = "Country: " + CountryOfStudy.LookupValue;

                        SPFieldLookupValue University = new SPFieldLookupValue(item["University"].ToString());
                        Label9.Text = "University: " + University.LookupValue;

                        SPFieldLookupValue Faculty = new SPFieldLookupValue(item["Faculty"].ToString());
                        Label10.Text = "Faculty: " + Faculty.LookupValue;

                        SPFieldLookupValue Specialization = new SPFieldLookupValue(item["Specialization"].ToString());
                        Label11.Text = "Specialization: " + Specialization.LookupValue;


                        SPFieldLookupValue EntityNeedsEquivalency = new SPFieldLookupValue(item["EntityNeedsEquivalency"].ToString());
                        Label12.Text = "EntityNeedsEquivalency: " + EntityNeedsEquivalency.LookupValue;

                    }
                }


                if (LCID == (int)Language.English)
                {

                }
                else
                {

                }

                Literal1.Text = HelperMethods.LocalizedText("ITWORX.MOEHEWF.UCE", "LocalSample", (uint)LCID);

                SPHttpUtility.HtmlEncode(txtTitle.Text);

            }
            catch (Exception ex)
            {
                Logging.GetInstance().LogException(ex);
            }
            finally
            {
                Logging.GetInstance().Debug("Exiting method SampleUserControl.Page_Load");
            }

        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            try
            {
                Logging.GetInstance().Debug("Entering method SampleUserControl.Button1_Click");
                if (!IsRefresh)
                {
                   }
            }
            catch (Exception ex)
            {
                Logging.GetInstance().LogException(ex);
                //handle exception as the business require.
            }
            finally
            {
                //dispose any objects that require dispose.
                Logging.GetInstance().Debug("Exiting method SampleUserControl.Button1_Click");
            }

        }
        private void Method()
        {
            #region Prerequiestes
            /// add colum "RequestID" lookup from Requests
            /// add column "DocumentStatus" choice field, values: Saved,Uploaded,Deleted
            /// add column Group, single line of text
            /// it is better to bind the below properties in the ASCX file (in HTML itself). but for LabelDisplayName it must be in the ASCX file (in HTML itself)
            #endregion
            #region Edit Mode
            fileUpload.AttachmentCategoryDataValueField = "ID";
            fileUpload.AttachmentCategoryDataTextEnField = "EnglishTitle";
            fileUpload.AttachmentCategoryDataTextArField = "ArabicTitle";
            List<ITWORX.MOEHEWF.UCE.Entities.AcademicDegree> academics = ITWORX.MOEHEWF.UCE.BL.AcademicDegree.GetAll();
            fileUpload.AttachmentCategories = academics;


            fileUpload.DocumentLibraryName = "Documents";
            fileUpload.DocLibWebUrl = "http://moehe-tst-dc:8080/en";

            fileUpload.LabelDisplayName = "fileUpload";//it must be in the ASCX file (in HTML itself)
            fileUpload.MaxFileNumber = 1;
            fileUpload.MaxSize = 2048000;//2MB
            fileUpload.Group = "fileUpload";// field name for example, shouldn't be used for more than one field per each control.
            fileUpload.RequestID = 1;
            fileUpload.SupportedExtensions = "PNG,PDF,JPG";
            fileUpload.IsRequired = true;
            fileUpload.DeleteImageUrl = SPContext.Current.Site.Url + "/_catalogs/masterpage/MOEHE/common/img/DELETE.png";
            fileUpload.Enabled = true;

            fileUpload.Bind();
            #endregion

            #region Save 
            // fileUpload.SaveAttachments();
            #endregion
            #region Display Mode
            //fileUpload.DocumentLibraryName = "Documents";
            //fileUpload.DocLibWebUrl = "http://moehe-tst-dc:8080/en";

            //fileUpload.LabelDisplayName = "fileUpload";
            //fileUpload.Group = "fileUpload";
            //fileUpload.RequestID = 1;
            //fileUpload.Enabled = false;

            //fileUpload.Bind();
            #endregion

        }

        private void JoinedClarifications()
        {
            using (SPWeb web = new SPSite("http://moehe-dev-03:90/").OpenWeb())
            {
                string joins = @"<Join Type='INNER' ListAlias='Requests'>
  <Eq>
    <FieldRef Name='RequestID' RefType='Id' />
    <FieldRef List='Requests' Name='Id' />
  </Eq>
</Join><Join Type='LEFT' ListAlias='AcademicDegree'>
  <Eq>
    <FieldRef List='Requests' Name='AcademicDegreeID' RefType='Id' />
    <FieldRef List='AcademicDegree' Name='Id' />
  </Eq>
</Join><Join Type='LEFT' ListAlias='CountryOfStudy'>
  <Eq>
    <FieldRef List='Requests' Name='CountryOfStudy' RefType='Id' />
    <FieldRef List='CountryOfStudy' Name='Id' />
  </Eq>
</Join><Join Type='LEFT' ListAlias='University'>
  <Eq>
    <FieldRef List='Requests' Name='UniversityID' RefType='Id' />
    <FieldRef List='University' Name='Id' />
  </Eq>
</Join><Join Type='LEFT' ListAlias='Faculty'>
  <Eq>
    <FieldRef List='Requests' Name='FacultyID' RefType='Id' />
    <FieldRef List='Faculty' Name='Id' />
  </Eq>
</Join><Join Type='LEFT' ListAlias='EntityNeedsEquivalency'>
  <Eq>
    <FieldRef List='Requests' Name='EntityNeedsEquivalency' RefType='Id' />
    <FieldRef List='EntityNeedsEquivalency' Name='Id' />
  </Eq>
</Join><Join Type='INNER' ListAlias='Applicants'>
  <Eq>
    <FieldRef List='Requests' Name='ApplicantID' RefType='Id' />
    <FieldRef List='Applicants' Name='Id' />
  </Eq>
</Join><Join Type='INNER' ListAlias='Nationality'>
  <Eq>
    <FieldRef List='Applicants' Name='Nationality' RefType='Id' />
    <FieldRef List='Nationality' Name='Id' />
  </Eq>
</Join>";

                string projectedFields =
                                      "<Field Name='Requests_RequestNumber' Type='Lookup' "
                + "List='Requests' ShowField='RequestNumber'/>"
                  + "<Field Name='AcademicDegree_Title' Type='Lookup' "
                + "List='AcademicDegree' ShowField='Title'/>"
                + "<Field Name='CountryOfStudy_Title' Type='Lookup' "
                + "List='CountryOfStudy' ShowField='Title'/>"
                 + "<Field Name='University_Title' Type='Lookup' "
                + "List='University' ShowField='Title'/>"
                + "<Field Name='Faculty_Title' Type='Lookup' "
                + "List='Faculty' ShowField='Title'/>"
                  + "<Field Name='EntityNeedsEquivalency_Title' Type='Lookup'"
                + "List='EntityNeedsEquivalency' ShowField='Title'/>"
                  + "<Field Name='Applicants_ApplicantName' Type='Lookup' "
                                                + "List='Applicants' ShowField='ApplicantName'/>"

                                                + "<Field Name='Applicants_QatarID' Type='Lookup' "
                                                + "List='Applicants' ShowField='PersonalID'/>"
                + "<Field Name='Nationality_Title' Type='Lookup' "
                + "List='Nationality' ShowField='Title'/>"
               ;



                string viewFields = "<FieldRef Name='Requests_RequestNumber'/>"
                + "<FieldRef Name='AcademicDegree_Title'/>"
                + "<FieldRef Name='CountryOfStudy_Title'/>"
                + "<FieldRef Name='University_Title'/>"
                + "<FieldRef Name='Faculty_Title'/>"
                + "<FieldRef Name='EntityNeedsEquivalency_Title'/>"
                + "<FieldRef Name='Applicants_ApplicantName'/>"
                + "<FieldRef Name='Applicants_QatarID'/>"
                + "<FieldRef Name='Nationality_Title'/>"
                + "<FieldRef Name='RequestedClarification'/>"
                + "<FieldRef Name='ClarificationDate'/>"
                ;


                SPList list = web.Lists["ClarificationsRequests"];
                SPQuery query = Common.Utilities.BusinessHelper.GetQueryObject(string.Empty, joins, projectedFields, viewFields);
                SPListItemCollection items = list.GetItems(query);
                foreach (SPListItem item in items)
                {
                    //SPFieldLookupValue RequestedClarification = new SPFieldLookupValue(item["RequestedClarification"].ToString());
                    //Label1.Text += " RequestedClarification: " + RequestedClarification.LookupValue;

                    //SPFieldLookupValue ClarificationDate = new SPFieldLookupValue(item["ClarificationDate"].ToString());
                    //Label2.Text = " ClarificationDate: " + ClarificationDate.LookupValue;

                    //SPFieldLookupValue ApplicantsQatarID = new SPFieldLookupValue(item["Applicants_QatarID"].ToString());
                    //Label3.Text = "Qatar ID: " + ApplicantsQatarID.LookupValue;

                    //SPFieldLookupValue ApplicantName = new SPFieldLookupValue(item["Applicants_ApplicantName"].ToString());
                    //Label4.Text = "Applicant Name: " + ApplicantName.LookupValue;


                    //SPFieldLookupValue NationalityTitle = new SPFieldLookupValue(item["Nationality_Title"].ToString());
                    //Label5.Text = "Nationality: " + NationalityTitle.LookupValue;


                    //SPFieldLookupValue RequestNumber = new SPFieldLookupValue(item["Requests_RequestNumber"].ToString());
                    //Label6.Text = "RequestNumber: " + RequestNumber.LookupValue;

                    //SPFieldLookupValue AcademicDegree = new SPFieldLookupValue(item["AcademicDegree_Title"].ToString());
                    //Label7.Text = "AcademicDegree: " + AcademicDegree.LookupValue;

                    //SPFieldLookupValue CountryOfStudy = new SPFieldLookupValue(item["CountryOfStudy_Title"].ToString());
                    //Label8.Text = "Country: " + CountryOfStudy.LookupValue;

                    //SPFieldLookupValue University = new SPFieldLookupValue(item["University_Title"].ToString());
                    //Label9.Text = "University: " + University.LookupValue;

                    //SPFieldLookupValue Faculty = new SPFieldLookupValue(item["Faculty_Title"].ToString());
                    //Label10.Text = "Faculty: " + Faculty.LookupValue;

                    //SPFieldLookupValue EntityNeedsEquivalency = new SPFieldLookupValue(item["EntityNeedsEquivalency_Title"].ToString());
                    //Label11.Text = "EntityNeedsEquivalency: " + EntityNeedsEquivalency.LookupValue;

                }
            }
        }
    }
}
