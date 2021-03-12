using ITWORX.MOEHE.Utilities;
using ITWORX.MOEHE.Utilities.Logging;
using ITWORX.MOEHEWF.UCE.Utilities;
using Microsoft.SharePoint;
using Microsoft.SharePoint.Utilities;
using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using common = ITWORX.MOEHEWF.Common;


namespace ITWORX.MOEHEWF.UCE.CONTROLTEMPLATES.ITWORX.MOEHEWF.UCE
{
    public partial class ViewStatusandRecommendation : UserControlBase
    {
        protected global::ITWORX.MOEHEWF.Common.CONTROLTEMPLATES.ITWORX.MOEHEWF.Common.FileUpload ViewStatusRecommendAttachements;

        protected void Page_Load(object sender, EventArgs e)
        {

            if (Page.Session["DisplayRequestId"] != null)
            {
                BindLabelsData(Page.Session["DisplayRequestId"].ToString());
                BindViewAttachements();
            }

        }

        private void BindLabelsData(string ReqID)
        {
            try
            {
                Logging.GetInstance().Debug("Enter ViewStatusandRecommendation.BindLabelsData");
                List<Entities.Procedures> RecommendProc = BL.AllProcedures.GetApprovedRecommendationStatus(Page.Session["DisplayRequestId"].ToString(), "Approved");
                if (RecommendProc.Count > 0)
                {
                    lbl_EmpOpinion.Text = RecommendProc[0].Opinion;

                    if (RecommendProc[0].Procedure == "Approved" || RecommendProc[0].Procedure == "بالموافقة")
                    {
                        lbl_DecisiontxtVal.Text = HelperMethods.LocalizedText("ITWORX_MOEHEWF_UCE", "approveDecisionText", (uint)LCID);
                    }
                    else
                    {
                        lbl_DecisiontxtVal.Text = HelperMethods.LocalizedText("ITWORX_MOEHEWF_UCE", "rejectDecisionText", (uint)LCID);
                    }

                    lbl_DecisiontxtVal.Text = lbl_DecisiontxtVal.Text + " " + SPHttpUtility.HtmlDecode(RecommendProc[0].DecisionForPrint);

                    lbl_decicionBobyView.Text = string.Format(lbl_decicionBobyView.Text, RecommendProc[0].BookNum, ExtensionMethods.QatarFormatedDate(Convert.ToDateTime(RecommendProc[0].BookDate)), "{0}","{1}","{2}");
                    lbl_SirValue.Text = !string.IsNullOrEmpty(RecommendProc[0].SirValue) ? RecommendProc[0].SirValue : HttpContext.GetGlobalResourceObject("ITWORX_MOEHEWF_UCE", "sir").ToString();
                    lbl_RespectedValue.Text = !string.IsNullOrEmpty(RecommendProc[0].RespectedValue) ? RecommendProc[0].RespectedValue : HttpContext.GetGlobalResourceObject("ITWORX_MOEHEWF_UCE", "recpected").ToString();
                    lbl_OccupationName.Text = RecommendProc[0].OccupationName;

                    lbl_EmpRecommendation.Text = RecommendProc[0].Procedure;
                    lbl_EmpRecommend.Visible = true;
                    lbl_Decisiontxt.Visible = true;
                    lbl_Opinion.Visible = true;
                    _bindDecisionTemplate();
                    if(!string.IsNullOrEmpty( RecommendProc[0].NumberOfHoursGained))
                       lbltxtGainedHours.Text = RecommendProc[0].NumberOfHoursGained;
                    if (!string.IsNullOrEmpty(RecommendProc[0].NumberOfOnlineHours))
                        lbltxtOnlineHours.Text = RecommendProc[0].NumberOfOnlineHours;
                    if (!string.IsNullOrEmpty(RecommendProc[0].PercentageOfOnlineHours))
                        lbltxtOnlinePercentage.Text = RecommendProc[0].PercentageOfOnlineHours;
                    if (!string.IsNullOrEmpty(RecommendProc[0].OrdinaryOrOwners))
                        lblrblHonoraryDegree.Text = RecommendProc[0].OrdinaryOrOwners;
                    //if (RecommendProc[0].HavePA == "True")
                    //    valckbHavePA.Text = HelperMethods.LocalizedText("ITWORX_MOEHEWF_UCE", "Yes", (uint)LCID);
                    //else
                    //    valckbHavePA.Text = HelperMethods.LocalizedText("ITWORX_MOEHEWF_UCE", "No", (uint)LCID);

                    #region Getting have PA value from request 
                    SPListItem requestItem = BL.Request.Reviewer_GetRequestItemByID(int.Parse(ReqID));

                    if (requestItem != null)
                    {
                        string havePA = requestItem["HavePAOrNot"] == null ? string.Empty : requestItem["HavePAOrNot"].ToString();
                        if (havePA == "True")
                            valckbHavePA.Text = HelperMethods.LocalizedText("ITWORX_MOEHEWF_UCE", "Yes", (uint)LCID);
                        else
                            valckbHavePA.Text = HelperMethods.LocalizedText("ITWORX_MOEHEWF_UCE", "No", (uint)LCID);


                        //get universty type

                        if (requestItem["UniversityType"] != null)
                        {
                            string universityType = Convert.ToString(requestItem["UniversityType"]);
                            lblrblUniversity.Text = universityType.ToLower() == "government" ? HelperMethods.LocalizedText("ITWORX_MOEHEWF_UCE", "universityGovernment", (uint)LCID) : HelperMethods.LocalizedText("ITWORX_MOEHEWF_UCE", "universityPrivate", (uint)LCID);
                        }


                    }
                    #endregion
                    //if (!string.IsNullOrEmpty(RecommendProc[0].TypeUniversity))
                    //    lblrblUniversity.Text = RecommendProc[0].TypeUniversity;

                    if (!string.IsNullOrEmpty(RecommendProc[0].HasException) && RecommendProc[0].HasException == "True")
                    {
                        lbl_HaveException.Text = HelperMethods.LocalizedText("ITWORX_MOEHEWF_UCE", "Yes", (uint)LCID);
                        if (!string.IsNullOrEmpty(RecommendProc[0].ExceptionFrom))
                            lblExceptionFromValue.Text = RecommendProc[0].ExceptionFrom;
                    }
                    else
                        lbl_HaveException.Text = HelperMethods.LocalizedText("ITWORX_MOEHEWF_UCE", "No", (uint)LCID);
                    
                }
                else
                {
                    lbl_NoResults.Visible = true;
                }

                
                    lbl_headManagerName.Text = HelperMethods.GetConfigurationValue(SPContext.Current.Site.RootWeb.Url, Utilities.Constants.ConfigurationList, "HeadManagerName");

            }
            catch (Exception ex)
            {
                Logging.GetInstance().LogException(ex);
            }
            finally
            {
                Logging.GetInstance().Debug("Exit ViewStatusandRecommendation.BindLabelsData");
            }
        }
        private void _bindDecisionTemplate()
        {
            try
            {
                if (Page.Session["DisplayRequestId"] != null)
                {
                    SPListItem request = common.BL.Request.GetRequestItemByID(int.Parse(Page.Session["DisplayRequestId"].ToString()));
                    {
                        string EntityNeedsEquivalency = string.Empty;
                        string ApplicantName = string.Empty;
                        string PersonalID = string.Empty; 

                        if (request["EntityNeedsEquivalencyAr"] != null)
                        {
                            SPFieldLookupValue EntityNeedsEquivalencylkp = new SPFieldLookupValue(request["EntityNeedsEquivalencyAr"].ToString());
                            EntityNeedsEquivalency = EntityNeedsEquivalencylkp.LookupValue;
                        }
                        else if (request["OtherEntityNeedsEquivalency"] != null)
                        {
                            EntityNeedsEquivalency = request["OtherEntityNeedsEquivalency"].ToString();
                        }
                        else if (request["EntityWorkingForAr"] != null)
                        {
                            SPFieldLookupValue EntityWorkingForlkp = new SPFieldLookupValue(request["EntityWorkingForAr"].ToString());
                            EntityNeedsEquivalency = EntityWorkingForlkp.LookupValue;
                        }
                        else if (request["OtherEntityWorkingFor"] != null)
                        {
                            EntityNeedsEquivalency = request["OtherEntityWorkingFor"].ToString();
                        }

                        if (request["Applicants_PersonalID"] != null)
                        {
                            SPFieldLookupValue Applicants_PersonalID = new SPFieldLookupValue(request["Applicants_PersonalID"].ToString());
                            PersonalID = Applicants_PersonalID.LookupValue;
                        }
                        if (request["Applicants_ApplicantAraicName"] != null)
                        {
                            SPFieldLookupValue Applicants_ApplicantName = new SPFieldLookupValue(request["Applicants_ApplicantAraicName"].ToString());
                            ApplicantName = Applicants_ApplicantName.LookupValue;
                        }

                        string applicantGender = common.BL.Applicants.IsFemale(PersonalID) ? HelperMethods.LocalizedText("ITWORX_MOEHEWF_UCE", "studentF", (uint)LCID) : HelperMethods.LocalizedText("ITWORX_MOEHEWF_UCE", "studentM", (uint)LCID);


                        lbl_EntityNeedsEquivalencyView.Text = EntityNeedsEquivalency;
                        lbl_decicionBobyView.Text = string.Format(lbl_decicionBobyView.Text,applicantGender,ApplicantName, PersonalID);
                    }
                }
            }
            catch (Exception ex)
            {
                Logging.GetInstance().LogException(ex);
            }
        }
        private void BindViewAttachements()
        {
            Logging.GetInstance().Debug("Entering method ViewStatusandRecommendation.BindViewAttachements");
            try
            {

                #region Prerequiestes
                /// add colum "RequestID" lookup from Requests
                /// add column "DocumentStatus" choice field, values: Saved,Uploaded,Deleted
                /// add column Group, single line of text
                #endregion
                #region Display Mode

                ViewStatusRecommendAttachements.DocumentLibraryName = Utilities.Constants.ProgEmpStatusandRecomAttachements;
                ViewStatusRecommendAttachements.DocLibWebUrl = SPContext.Current.Site.Url;
                ViewStatusRecommendAttachements.LabelDisplayName = HelperMethods.LocalizedText("ITWORX_MOEHEWF_UCE", "NotesPrepared", (uint)LCID);
                ViewStatusRecommendAttachements.Group = "StatusRecommendAttachements";
                ViewStatusRecommendAttachements.RequestID = Convert.ToInt32(Page.Session["DisplayRequestId"].ToString());
                ViewStatusRecommendAttachements.Enabled = false;
                ViewStatusRecommendAttachements.Bind();
                if (ViewStatusRecommendAttachements.AttachmentsCount == 0)
                    ViewStatusRecommendAttachements.Visible = false;
                #endregion
            }
            catch (Exception ex)
            {
                Logging.GetInstance().LogException(ex);
                //handle exception as the business require.
            }
            finally
            {
                //dispose any objects that require dispose.
                Logging.GetInstance().Debug("Exiting method ViewStatusandRecommendation.BindViewAttachements");
            }
        }
    }
}
