using System;
using System.Web;

using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Linq;
using Microsoft.SharePoint;
using ITWORX.MOEHEWF.Common.Utilities;
using ITWORX.MOEHE.Utilities;
using ITWORX.MOEHEWF.Common.CONTROLTEMPLATES.ITWORX.MOEHEWF.Common;
using System.Web;
using ITWORX.MOEHE.Utilities.Logging;
using ITWorx.MOEHEWF.Nintex.Actions;
using ITWORX.MOEHEWF.Common.BL;
using ITWORX.MOEHEWF.SCE.BL;
using System.IO;
using Microsoft.SharePoint.Utilities;
using iTextSharp.text.pdf;
using System.Net;
using ITWORX.MOEHEWF.SCE.Entities;
using System.Text;

namespace ITWORX.MOEHEWF.SCE.WebParts.PMDecisionSCE
{
    public partial class PMDecisionSCEUserControl : UserControlBase
    {
        protected DDLWithTXTWithNoPostback drpFinalDecisionRejectReason;
        protected DDLWithTXTWithNoPostback drpFinalRejectDecisonReasonssReadOnly;

        protected DDLWithTXTWithNoPostback drpManagerDecision;
        protected DDLWithTXTWithNoPostback drpViewOnlyRejectionReason;



        public string LoginName
        {
            get {
                if (ViewState["LoginName"] == null)
                {
                    return SPContext.Current.Web.CurrentUser.LoginName;
                }
                return (string)ViewState["LoginName"];
            }
            set { ViewState["LoginName"] = value; }

        }


        public string RootWebURL
        {

            get
            {
                if (ViewState["RootWebURL"] == null)
                {
                    return SPContext.Current.Site.RootWeb.Url;
                }
                return (string)ViewState["RootWebURL"];
            }
            set { ViewState["RootWebURL"] = value; }

        }

        public int? RequestID
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

        public string requestNumber
        {
            get
            {
                if (ViewState["requestNumber"] != null)
                {
                    return ViewState["requestNumber"].ToString();
                }
                return null;
            }
            set
            {
                ViewState["requestNumber"] = value;
            }
        }






        protected void Page_Load(object sender, EventArgs e)
        {

            SPSecurity.RunWithElevatedPrivileges(() =>
            {


                if (!Page.IsPostBack)
                {
                    
                    PermissionsAndControls();

                }
            });
            

        }

        private void BindFinalDecisionRejectionReasonsDDL()
        {

            try
            {
                Logging.GetInstance().Debug("Enter PMDecisionSCEUserControl.BindFinalDecisionRejectionReasonsDDL");
                using (SCEContextDataContext ctx = new SCEContextDataContext(RootWebURL))
                {

                    var rejectionReasons = ctx.RejectionReasonsList.ScopeToFolder("", true).Where(c => c.Status == Status.Active).ToList();

                    drpFinalDecisionRejectReason.IsRequired = true;
                    drpFinalDecisionRejectReason.DataSource = rejectionReasons;
                    drpFinalDecisionRejectReason.DataValueField = "ID";
                    drpFinalDecisionRejectReason.DataENTextField = "Title";
                    drpFinalDecisionRejectReason.DataARTextField = "TitleAr";
                    drpFinalDecisionRejectReason.ValidationGroup = "Submit";
                    drpFinalDecisionRejectReason.Title =HelperMethods.LocalizedText("ITWORX_MOEHEWF_SCE", "SCERejectionReason", (uint)LCID);
                    drpFinalDecisionRejectReason.ValidationMSG = HelperMethods.LocalizedText("ITWORX_MOEHEWF_SCE", "RequiredRejectionReason", (uint)LCID);
                    drpFinalDecisionRejectReason.ValidationGroup = "Submit";
                    drpFinalDecisionRejectReason.BingDDL();
                }


            }

            catch (Exception ex)
            {
                Logging.GetInstance().LogException(ex);
            }
            finally
            {
                // resetting the SPContext

                Logging.GetInstance().Debug("Exit PMDecisionSCEUserControl.BindFinalDecisionRejectionReasonsDDL");
            }

        }


        private void BindViewOnlyRejectionReasonsDDL()
        {

            try
            {
                Logging.GetInstance().Debug("Enter PMDecisionSCEUserControl.BindFinalDecisionRejectionReasonsDDL");
                using (SCEContextDataContext ctx = new SCEContextDataContext(RootWebURL))
                {

                    var rejectionReasons = ctx.RejectionReasonsList.ScopeToFolder("", true).Where(c => c.Status == Status.Active).ToList();

                    drpViewOnlyRejectionReason.IsRequired = false;
                    drpViewOnlyRejectionReason.DataSource = rejectionReasons;
                    drpViewOnlyRejectionReason.DataValueField = "ID";
                    drpViewOnlyRejectionReason.DataENTextField = "Title";
                    drpViewOnlyRejectionReason.DataARTextField = "TitleAr";
                    drpViewOnlyRejectionReason.ValidationGroup = "Submit";
                    drpViewOnlyRejectionReason.Title = "";
                    drpViewOnlyRejectionReason.ValidationMSG = "يرجى إدخال الحقل";
                    drpViewOnlyRejectionReason.ValidationGroup = "Submit";
                    drpViewOnlyRejectionReason.Enabled = false;
                    drpViewOnlyRejectionReason.BingDDL();
                }


            }

            catch (Exception ex)
            {
                Logging.GetInstance().LogException(ex);
            }
            finally
            {
                // resetting the SPContext

                Logging.GetInstance().Debug("Exit PMDecisionSCEUserControl.BindFinalDecisionRejectionReasonsDDL");
            }

        }



        private void BindFinalRejectionReasonsReadOnlyDDL()
        {

            try
            {
                Logging.GetInstance().Debug("Enter PMDecisionSCEUserControl.BindFinalDecisionRejectionReasonsDDL");
                using (SCEContextDataContext ctx = new SCEContextDataContext(RootWebURL))
                {

                    var rejectionReasons = ctx.RejectionReasonsList.ScopeToFolder("", true).Where(c => c.Status == Status.Active).ToList();

                    drpFinalRejectDecisonReasonssReadOnly.IsRequired = false;
                    drpFinalRejectDecisonReasonssReadOnly.DataSource = rejectionReasons;
                    drpFinalRejectDecisonReasonssReadOnly.DataValueField = "ID";
                    drpFinalRejectDecisonReasonssReadOnly.DataENTextField = "TitleAr";
                    drpFinalRejectDecisonReasonssReadOnly.DataARTextField = "TitleAr";
                    drpFinalRejectDecisonReasonssReadOnly.ValidationGroup = "Submit";
                    drpFinalRejectDecisonReasonssReadOnly.Title = "";
                    drpFinalRejectDecisonReasonssReadOnly.ValidationMSG = "يرجى إدخال الحقل";
                    drpFinalRejectDecisonReasonssReadOnly.ValidationGroup = "Submit";
                    drpFinalRejectDecisonReasonssReadOnly.Enabled = false;
                    drpFinalRejectDecisonReasonssReadOnly.BingDDL();
                }


            }

            catch (Exception ex)
            {
                Logging.GetInstance().LogException(ex);
            }
            finally
            {
                // resetting the SPContext

                Logging.GetInstance().Debug("Exit PMDecisionSCEUserControl.BindFinalDecisionRejectionReasonsDDL");
            }

        }

        private void BindManagerDecisionRejectionReasonsDDL()
        {
            //drpManagerDecision


            try
            {
                Logging.GetInstance().Debug("Enter PMDecisionSCEUserControl.BindManagerDecisionRejectionReasonsDDL");
                using (SCEContextDataContext ctx = new SCEContextDataContext(RootWebURL))
                {

                    var rejectionReasons = ctx.RejectionReasonsList.ScopeToFolder("", true).Where(c => c.Status == Status.Active).ToList();

                    drpManagerDecision.IsRequired = true;
                    drpManagerDecision.DataSource = rejectionReasons;
                    drpManagerDecision.DataValueField = "ID";
                    drpManagerDecision.DataENTextField = "Title";
                    drpManagerDecision.DataARTextField = "TitleAr";
                    drpManagerDecision.ValidationGroup = "Submit";
                    drpManagerDecision.Title = HelperMethods.LocalizedText("ITWORX_MOEHEWF_SCE", "SCERejectionReason", (uint)LCID);
                    drpManagerDecision.ValidationMSG = HelperMethods.LocalizedText("ITWORX_MOEHEWF_SCE", "SCERejectionReasonRequired", (uint)LCID);
                    drpManagerDecision.ValidationGroup = "Submit";

                    drpManagerDecision.Enabled = true;

                    drpManagerDecision.BingDDL();

                }

            }

            catch (Exception ex)
            {
                Logging.GetInstance().LogException(ex);
            }
            finally
            {
                // resetting the SPContext

                Logging.GetInstance().Debug("Exit PMDecisionSCEUserControl.BindManagerDecisionRejectionReasonsDDL");
            }

        }

       


        private void PermissionsAndControls()
        {
            try
            {
                Logging.GetInstance().Debug("Enter PMDecisionSCEUserControl.PermissionsAndControls");


                using (SCEContextDataContext ctx = new SCEContextDataContext(RootWebURL))
                {
                    SCERequestsListFieldsContentType currentRequest = ctx.SCERequestsList.ScopeToFolder("", true).Where(a => a.Id == RequestID).SingleOrDefault();
                    
                    if (currentRequest != null)
                    {

                        requestNumber = currentRequest.RequestNumber;

                        if (currentRequest.RequestStatus.Id == (int)Common.Utilities.RequestStatus.SCEAcceptedRecommendation
                            || currentRequest.RequestStatus.Id == (int)Common.Utilities.RequestStatus.SCERejectedRecommendationSectionManager
                            ) // Approved from Employee and Sent to Section Manager  or rejected from Employedd and sent to Section Manager to take decision
                        {
                            // Final Confirm  Tab// will be the Section Manager Decision
                            finalconfirm.Style.Add("display", "block");
                            GetFinalConformContorls();

                            //Get CurrentRequest Details 

                            SCEPMDecisionListFieldsContentType currentDecision = ctx.SCEPMDecisionList.ScopeToFolder("", true).Where(x => x.RequestIDId == RequestID).SingleOrDefault();
                            if (currentDecision != null)
                            {
                                txtFinalDecisionComments.Text = currentDecision.Comments;
                                drpFianlDecisonRecommendation.SelectedValue = currentDecision.DecisionId.ToString();
                                if (drpFianlDecisonRecommendation.SelectedValue == "2")
                                {
                                    BindFinalDecisionRejectionReasonsDDL();
                                    divFinalDecisionRejectionReasons.Visible = true;

                                    drpFinalDecisionRejectReason.SelectedValue = currentDecision.RejectionReasonId == 0 ? "-2" : currentDecision.RejectionReasonId.ToString();
                                    drpFinalDecisionRejectReason.OtherValue = currentDecision.OtherRejectionReason;
                                }


                            }
                            //Noha
                            SPSecurity.RunWithElevatedPrivileges(() =>
                            {
                                using (SPSite site = new SPSite(SPContext.Current.Site.Url))
                                {
                                    using (SPWeb web = site.OpenWeb())
                                    {
                                        SPList list = web.Lists[Utilities.Constants.SCERequests];
                                        SPListItemCollection collListItems = null;
                                        var camelQuery = Common.Utilities.BusinessHelper.GetQueryObject("<Where><Eq><FieldRef Name ='ID'/><Value Type = 'Number'>" +
                                            RequestID.ToString() + "</Value></Eq></Where>");

                                        collListItems = list.GetItems(camelQuery);

                                        if (collListItems != null)
                                        {
                                            int attachmentsCount = collListItems[0].Attachments.Count;

                                            if (attachmentsCount == 0)
                                            {
                                                btnViewFinalDecision.Enabled = false;
                                            }
                                        }
                                    }
                                }
                            });

                            //Noha



                        }
                        else if (currentRequest.RequestStatus.Id == (int)Common.Utilities.RequestStatus.SCEDepartmentManagerAcceptDecision
                            || currentRequest.RequestStatus.Id == (int)Common.Utilities.RequestStatus.SCEDepartmentManagerRejectDecision)
                        {
                            //    Department Manger took the decision and send the request to Section Manager to close the request. 

                            finalReject.Style.Add("display", "block");
                            GetFinalRejectControls();



                            SCEHMDecisionListFieldsContentType currentDecision = ctx.SCEHMDecisionList.ScopeToFolder("", true).Where(x => x.RequestIDId == RequestID).SingleOrDefault();
                            if (currentDecision != null)
                            {


                                txtFinalRejectCommentsReadOnly.Text = currentDecision.Comments;
                                drpFinalRejectRecommendReadOnly.SelectedValue = currentDecision.DecisionId.ToString();
                                if (drpFinalRejectRecommendReadOnly.SelectedValue == "2")
                                {
                                    BindFinalRejectionReasonsReadOnlyDDL();
                                    dvFinalRejectReasonReadOnly.Visible = true;

                                    drpFinalRejectDecisonReasonssReadOnly.SelectedValue = currentDecision.RejectionReasonId == 0 ? "-2" : currentDecision.RejectionReasonId.ToString();
                                    drpFinalRejectDecisonReasonssReadOnly.OtherValue = currentDecision.OtherRejectionReason;
                                }
                                else
                                {
                                    dvFinalRejectReasonReadOnly.Visible = false;
                                }




                            }





                            if (currentRequest.RequestStatus.Id == (int)Common.Utilities.RequestStatus.SCEDepartmentManagerRejectDecision)
                            {
                                SPSecurity.RunWithElevatedPrivileges(() =>
                                {
                                    using (SPSite site = new SPSite(SPContext.Current.Site.Url))
                                    {
                                        using (SPWeb web = site.OpenWeb())
                                        {
                                            SPList list = web.Lists[Utilities.Constants.SCERequests];
                                            SPListItemCollection collListItems = null;
                                            var camelQuery = Common.Utilities.BusinessHelper.GetQueryObject("<Where><Eq><FieldRef Name ='ID'/><Value Type = 'Number'>" +
                                                RequestID.ToString() + "</Value></Eq></Where>");

                                            collListItems = list.GetItems(camelQuery);

                                            if (collListItems != null)
                                            {
                                                int attachmentsCount = collListItems[0].Attachments.Count;

                                                if (attachmentsCount == 0)
                                                {
                                                    btnFinalRejectView.Enabled = false;
                                                }
                                            }
                                        }
                                    }
                                });

                            }



                        }
                        else if (currentRequest.RequestStatus.Id == (int)Common.Utilities.RequestStatus.SCERejectedRecommendationDepartmentManager)
                        {
                            // managerDecision


                            //Request status will be eithrt SCEDepartmentManagerAcceptDecision  OR   SCEDepartmentManagerRejectDecision to be sent to the Section Manager 

                            managerDecision.Style.Add("display", "block");
                            GetManagerDecisionControls();
                            //Noha
                            SPSecurity.RunWithElevatedPrivileges(() =>
                            {
                                using (SPSite site = new SPSite(SPContext.Current.Site.Url))
                                {
                                    using (SPWeb web = site.OpenWeb())
                                    {
                                        SPList list = web.Lists[Utilities.Constants.SCERequests];
                                        SPListItemCollection collListItems = null;
                                        var camelQuery = Common.Utilities.BusinessHelper.GetQueryObject("<Where><Eq><FieldRef Name ='ID'/><Value Type = 'Number'>" +
                                            RequestID.ToString() + "</Value></Eq></Where>");

                                        collListItems = list.GetItems(camelQuery);

                                        if (collListItems != null)
                                        {
                                            int attachmentsCount = collListItems[0].Attachments.Count;

                                        if (attachmentsCount == 0)
                                            {
                                                btnManagerDecisionView.Enabled = false;
                                            }
                                        }
                                    }
                                }
                            });
                                  
                                    //Noha


                                    SCEHMDecisionListFieldsContentType currentDecision = ctx.SCEHMDecisionList.ScopeToFolder("", true).Where(x => x.RequestIDId == RequestID).SingleOrDefault();
                            if (currentDecision != null)
                            {


                                txtManagerDecisionComments.Text = currentDecision.Comments;
                                drpHMDecisionRecommend.SelectedValue = currentDecision.DecisionId.ToString();
                                if (drpHMDecisionRecommend.SelectedValue == "2")
                                {
                                    BindManagerDecisionRejectionReasonsDDL();
                                    dvFinalDecisionRejection.Visible = true;

                                    drpManagerDecision.SelectedValue = currentDecision.RejectionReasonId == 0 ? "-2" : currentDecision.RejectionReasonId.ToString();
                                    drpManagerDecision.OtherValue = currentDecision.OtherRejectionReason;
                                }
                                else
                                {
                                    dvFinalDecisionRejection.Visible = false;
                                }




                            }



                        }
                       
                        else
                        {
                            //read Only View 

                            readOnLyView.Style.Add("display", "block");

                            GetViewOnlyContorls();


                            SCEPMDecisionListFieldsContentType currentDecision = ctx.SCEPMDecisionList.ScopeToFolder("", true).Where(x => x.RequestIDId == RequestID).SingleOrDefault();
                            drpRecommendationViewOnly.SelectedValue = currentDecision.DecisionId.ToString();
                            if (currentDecision != null)
                            {
                                txtCommentsViewOnly.Text = currentDecision.Comments;
                                txtCommentsViewOnly.ReadOnly = true;


                                if (drpRecommendationViewOnly.SelectedValue == "2")
                                {
                                    BindViewOnlyRejectionReasonsDDL();
                                    dvviewOnlyRejectionReason.Visible = true;
                                    if (currentDecision.RejectionReasonId == null && !string.IsNullOrEmpty(currentDecision.OtherRejectionReason))
                                    {
                                        drpViewOnlyRejectionReason.SelectedValue = "-2";
                                        drpViewOnlyRejectionReason.OtherValue = currentDecision.OtherRejectionReason;
                                    }
                                    else

                                    {
                                        drpViewOnlyRejectionReason.SelectedValue = currentDecision.RejectionReasonId.ToString();

                                    }


                                }
                            }





                        }





                    }



                }



            }
            catch(Exception ex)
             {
                Logging.GetInstance().LogException(ex);
            }
            finally
            {
                // resetting the SPContext

                Logging.GetInstance().Debug("Exit PMDecisionSCEUserControl.PermissionsAndControls");
            }




        }



        private void GetFinalConformContorls()
        {


            try
            {
                Logging.GetInstance().Debug("Enter PMDecisionSCEUserControl.BindRecommendationsDDLs");

                using (SCEContextDataContext ctx = new SCEContextDataContext(RootWebURL))
                {
                    ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////
                    var recommandations = ctx.RecommendationsList.ScopeToFolder("", true).Where(c => c.Status == Status.Active).ToList();
                    HelperMethods.BindDropDownList(ref drpFianlDecisonRecommendation, recommandations, "ID", "TitleAr", "Title", LCID);
                    drpFianlDecisonRecommendation.Items.Insert(0, new ListItem(HelperMethods.LocalizedText("ITWORX_MOEHEWF_SCE", "ChooseValue", (uint)LCID), string.Empty));
                    drpFianlDecisonRecommendation.AppendDataBoundItems = true;

                    
                }

                requiredDecisionRecommend.Enabled = false;
                drpFinalDecisionRejectReason.IsRequired = true;

            }
            catch (Exception ex)
            {
                Logging.GetInstance().LogException(ex);
            }
            finally
            {
                // resetting the SPContext

                Logging.GetInstance().Debug("Exit PMDecisionSCEUserControl.BindRecommendationsDDLs");
            }

            




        }



        private void GetViewOnlyContorls()
        {


            try
            {
                Logging.GetInstance().Debug("Enter PMDecisionSCEUserControl.GetViewOnlyContorls");

                using (SCEContextDataContext ctx = new SCEContextDataContext(RootWebURL))
                {
                    ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////
                    var recommandations = ctx.RecommendationsList.ScopeToFolder("", true).Where(c => c.Status == Status.Active).ToList();
                    HelperMethods.BindDropDownList(ref drpRecommendationViewOnly, recommandations, "ID", "TitleAr", "Title", LCID);
                    drpRecommendationViewOnly.Items.Insert(0, new ListItem(HelperMethods.LocalizedText("ITWORX_MOEHEWF_SCE", "ChooseValue", (uint)LCID), string.Empty));
                    drpRecommendationViewOnly.AppendDataBoundItems = true;

                 
                }


            }
            catch (Exception ex)
            {
                Logging.GetInstance().LogException(ex);
            }
            finally
            {
                // resetting the SPContext

                Logging.GetInstance().Debug("Exit PMDecisionSCEUserControl.GetViewOnlyContorls");
            }






        }




        private void GetFinalRejectControls()
        {
            try
            {
                Logging.GetInstance().Debug("Enter PMDecisionSCEUserControl.GetDepartmentManagerRejectControls");

                using (SCEContextDataContext ctx = new SCEContextDataContext(RootWebURL))
                {
                    ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////
                    var recommandations = ctx.RecommendationsList.ScopeToFolder("", true).Where(c => c.Status == Status.Active).ToList();
                    HelperMethods.BindDropDownList(ref drpFinalRejectRecommendReadOnly, recommandations, "ID", "TitleAr", "Title", LCID);
                    drpFianlDecisonRecommendation.Items.Insert(0, new ListItem(HelperMethods.LocalizedText("ITWORX_MOEHEWF_SCE", "ChooseValue", (uint)LCID), string.Empty));
                    drpFianlDecisonRecommendation.AppendDataBoundItems = true;
                }


            }
            catch (Exception ex)
            {
                Logging.GetInstance().LogException(ex);
            }
            finally
            {
                // resetting the SPContext

                Logging.GetInstance().Debug("Exit PMDecisionSCEUserControl.GetDepartmentManagerRejectControls");
            }


        }



        private void GetManagerDecisionControls()
        {

            
                try
            {
                Logging.GetInstance().Debug("Enter PMDecisionSCEUserControl.GetDepartmentManagerRejectControls");

                using (SCEContextDataContext ctx = new SCEContextDataContext(RootWebURL))
                {
                    ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////
                    var recommandations = ctx.RecommendationsList.ScopeToFolder("", true).Where(c => c.Status == Status.Active).ToList();
                    HelperMethods.BindDropDownList(ref drpHMDecisionRecommend, recommandations, "ID", "TitleAr", "Title", LCID);
                    drpHMDecisionRecommend.Items.Insert(0, new ListItem(HelperMethods.LocalizedText("ITWORX_MOEHEWF_SCE", "ChooseValue", (uint)LCID), string.Empty));
                    drpHMDecisionRecommend.AppendDataBoundItems = true;
                }

                reqFinalDecisioRecomm.Enabled = false;
                drpManagerDecision.IsRequired = true;
              
               
            }
            catch (Exception ex)
            {
                Logging.GetInstance().LogException(ex);
            }
            finally
            {
                // resetting the SPContext

                Logging.GetInstance().Debug("Exit PMDecisionSCEUserControl.GetDepartmentManagerRejectControls");
            }



        }

        private void SaveSectionManagerDecision( bool SubmitChanges)
        {
            SPSecurity.RunWithElevatedPrivileges(() =>
            {
                using (SPSite site = new SPSite(SPContext.Current.Site.Url))
                {
                    using (SPWeb web = site.OpenWeb())
                    {
                        using (SCEContextDataContext ctx = new SCEContextDataContext(RootWebURL))
                        {
                            try
                            {

                                web.AllowUnsafeUpdates = true;
                                string folderUrl = DateTime.Now.ToString("yyyy/MM/dd");
                                SPList list = web.Lists[Utilities.Constants.SCEPMDecision];
                                //Get Current RequestID By Request Number
                                SCEPMDecisionListFieldsContentType currentRequest = ctx.SCEPMDecisionList.ScopeToFolder("", true).Where(x => x.RequestIDId == RequestID).SingleOrDefault();
                                if (currentRequest == null)
                                {
                                    //create new item 
                                    SCEPMDecisionListFieldsContentType newDecision = new SCEPMDecisionListFieldsContentType
                                    {
                                        Comments = txtFinalDecisionComments.Text,
                                        RequestIDId = RequestID,


                                    };

                                    if (!string.IsNullOrEmpty(drpFinalDecisionRejectReason.SelectedValue))
                                    {
                                        LookupsCT reasonDetails = ctx.RejectionReasonsList.ScopeToFolder("", true).Where(r => r.Id == Convert.ToInt32(drpFinalDecisionRejectReason.SelectedValue) && r.Status == Status.Active).SingleOrDefault();

                                        if (drpFinalDecisionRejectReason.SelectedValue == "-2")
                                        {


                                            newDecision.RejectionReasonId = 0;
                                            newDecision.DecisionTitle = "";
                                            newDecision.OtherRejectionReason = drpFinalDecisionRejectReason.OtherValue;

                                        }
                                        else
                                        {
                                            if (reasonDetails != null)
                                            {
                                                newDecision.RejectionReasonId = reasonDetails.Id;
                                                newDecision.RejectionReasonTitle = reasonDetails.TitleAr;
                                                newDecision.OtherRejectionReason = "";

                                            }
                                        }
                                    }


                                    LookupsCT decisionDetails = ctx.RecommendationsList.ScopeToFolder("", true).Where(r => r.Id == Convert.ToInt32(drpFianlDecisonRecommendation.SelectedValue) && r.Status == Status.Active).SingleOrDefault();

                                    if (decisionDetails != null)
                                    {
                                        newDecision.DecisionId = decisionDetails.Id;
                                        newDecision.DecisionTitle = decisionDetails.TitleAr;
                                        newDecision.Title = decisionDetails.TitleAr;

                                    }



                                    SPFolder folder = Common.Utilities.BusinessHelper.CreateFolderInternal(list, list.RootFolder, folderUrl);
                                    newDecision.Path = folder.Url;
                                    ctx.SCEPMDecision.InsertOnSubmit(newDecision);


                                }
                                else
                                {
                                    currentRequest.RequestIDId = RequestID;
                                    currentRequest.Comments = txtFinalDecisionComments.Text;

                                    LookupsCT decisionDetails = ctx.RecommendationsList.ScopeToFolder("", true).Where(r => r.Id == Convert.ToInt32(drpFianlDecisonRecommendation.SelectedValue) && r.Status == Status.Active).SingleOrDefault();

                                    if (decisionDetails != null)
                                    {
                                        currentRequest.DecisionId = decisionDetails.Id;
                                        currentRequest.DecisionTitle = decisionDetails.TitleAr;
                                        currentRequest.Title = decisionDetails.TitleAr;

                                    }
                                    if (!string.IsNullOrEmpty(drpFinalDecisionRejectReason.SelectedValue))
                                    {
                                        LookupsCT reasonDetails = ctx.RejectionReasonsList.ScopeToFolder("", true).Where(r => r.Id == Convert.ToInt32(drpFinalDecisionRejectReason.SelectedValue) && r.Status == Status.Active).SingleOrDefault();

                                        if (drpFinalDecisionRejectReason.SelectedValue == "-2")
                                        {


                                            currentRequest.RejectionReasonId = 0;
                                            currentRequest.DecisionTitle = "";
                                            currentRequest.OtherRejectionReason = drpFinalDecisionRejectReason.OtherValue;

                                        }
                                        else
                                        {
                                            if (reasonDetails != null)
                                            {
                                                currentRequest.RejectionReasonId = reasonDetails.Id;
                                                currentRequest.RejectionReasonTitle = reasonDetails.TitleAr;
                                                currentRequest.OtherRejectionReason = "";


                                            }
                                        }
                                    }



                                }


                                


                            }
                            catch (Exception ex)
                            {

                                Logging.GetInstance().LogException(ex);


                            }
                            if (SubmitChanges)
                            {
                                lblSaveSuccess.Text = string.Format(HelperMethods.LocalizedText("ITWORX_MOEHEWF_SCE", "SaveSucceed", (uint)LCID), requestNumber);
                                modalSavePopup.Show();
                            }

                            SCERequestsListFieldsContentType finalRequest = ctx.SCERequestsList.ScopeToFolder("", true).FirstOrDefault();
                            if (finalRequest != null)
                            {
                                finalRequest.DecisionDate = DateTime.Now;

                            }


                            ctx.SubmitChanges();
                        }
                    }
                }

            });
            
        }

        private void SaveFinalRejectDecision(bool SubmitChanges)
        {

            SPSecurity.RunWithElevatedPrivileges(() =>
            {
                using (SPSite site = new SPSite(SPContext.Current.Site.Url))
                {
                    using (SPWeb web = site.OpenWeb())
                    {
                        using (SCEContextDataContext ctx = new SCEContextDataContext(RootWebURL))
                        {
                            try
                            {

                                web.AllowUnsafeUpdates = true;
                                string folderUrl = DateTime.Now.ToString("yyyy/MM/dd");
                                SPList list = web.Lists[Utilities.Constants.SCEPMDecision];
                                //Get Current RequestID By Request Number
                                SCEPMDecisionListFieldsContentType currentRequest = ctx.SCEPMDecisionList.ScopeToFolder("", true).Where(x => x.RequestIDId == RequestID).SingleOrDefault();
                                if (currentRequest == null)
                                {
                                    //create new item 
                                    SCEPMDecisionListFieldsContentType newDecision = new SCEPMDecisionListFieldsContentType
                                    {
                                        Comments = txtFinalRejectCommentsReadOnly.Text,
                                        RequestIDId = RequestID,


                                    };

                                    if (!string.IsNullOrEmpty(drpFinalDecisionRejectReason.SelectedValue))
                                    {
                                        LookupsCT reasonDetails = ctx.RejectionReasonsList.ScopeToFolder("", true).Where(r => r.Id == Convert.ToInt32(drpFinalRejectDecisonReasonssReadOnly.SelectedValue) && r.Status == Status.Active).SingleOrDefault();

                                        if (drpFinalDecisionRejectReason.SelectedValue == "-2")
                                        {


                                            newDecision.RejectionReasonId = 0;
                                            newDecision.DecisionTitle = "";
                                            newDecision.OtherRejectionReason = drpFinalRejectDecisonReasonssReadOnly.OtherValue;

                                        }
                                        else
                                        {
                                            if (reasonDetails != null)
                                            {
                                                newDecision.RejectionReasonId = reasonDetails.Id;
                                                newDecision.RejectionReasonTitle = reasonDetails.TitleAr;
                                                newDecision.OtherRejectionReason = "";

                                            }
                                        }
                                    }


                                    LookupsCT decisionDetails = ctx.RecommendationsList.ScopeToFolder("", true).Where(r => r.Id == Convert.ToInt32(drpFinalRejectRecommendReadOnly.SelectedValue) && r.Status == Status.Active).SingleOrDefault();

                                    if (decisionDetails != null)
                                    {
                                        newDecision.DecisionId = decisionDetails.Id;
                                        newDecision.DecisionTitle = decisionDetails.TitleAr;
                                        newDecision.Title = decisionDetails.TitleAr;

                                    }



                                    SPFolder folder = Common.Utilities.BusinessHelper.CreateFolderInternal(list, list.RootFolder, folderUrl);
                                    newDecision.Path = folder.Url;
                                    ctx.SCEPMDecision.InsertOnSubmit(newDecision);


                                }
                                else
                                {
                                    currentRequest.RequestIDId = RequestID;
                                    currentRequest.Comments = txtFinalDecisionComments.Text;

                                    LookupsCT decisionDetails = ctx.RecommendationsList.ScopeToFolder("", true).Where(r => r.Id == Convert.ToInt32(drpFinalRejectRecommendReadOnly.SelectedValue) && r.Status == Status.Active).SingleOrDefault();

                                    if (decisionDetails != null)
                                    {
                                        currentRequest.DecisionId = decisionDetails.Id;
                                        currentRequest.DecisionTitle = decisionDetails.TitleAr;
                                        currentRequest.Title = decisionDetails.TitleAr;

                                    }
                                    if (!string.IsNullOrEmpty(drpFinalDecisionRejectReason.SelectedValue))
                                    {
                                        LookupsCT reasonDetails = ctx.RejectionReasonsList.ScopeToFolder("", true).Where(r => r.Id == Convert.ToInt32(drpFinalRejectDecisonReasonssReadOnly.SelectedValue) && r.Status == Status.Active).SingleOrDefault();

                                        if (drpFinalDecisionRejectReason.SelectedValue == "-2")
                                        {


                                            currentRequest.RejectionReasonId = 0;
                                            currentRequest.DecisionTitle = "";
                                            currentRequest.OtherRejectionReason = drpFinalRejectDecisonReasonssReadOnly.OtherValue;

                                        }
                                        else
                                        {
                                            if (reasonDetails != null)
                                            {
                                                currentRequest.RejectionReasonId = reasonDetails.Id;
                                                currentRequest.RejectionReasonTitle = reasonDetails.TitleAr;
                                                currentRequest.OtherRejectionReason = "";


                                            }
                                        }
                                    }



                                }


                              


                            }
                            catch (Exception ex)
                            {

                                Logging.GetInstance().LogException(ex);


                            }
                            if (SubmitChanges)
                            {
                                lblSaveSuccess.Text = string.Format(HelperMethods.LocalizedText("ITWORX_MOEHEWF_SCE", "SaveSucceed", (uint)LCID), requestNumber);
                                modalSavePopup.Show();

                            }
                            ctx.SubmitChanges();
                        }
                    }
                }

            });




        }

        private void SubmitSectionManagerDecision()
        {

            
                using (SCEContextDataContext ctx = new SCEContextDataContext(RootWebURL))
                {
                try
                {
                    if (drpFianlDecisonRecommendation.SelectedValue == "1")// Close by Approval
                    {
                        //SCESectionManagerAccepted

                        SaveSectionManagerDecision(false);
                        savedecisionPDF(drpFianlDecisonRecommendation.SelectedValue);
                        NintexHelper.ContinueTask(ITWorx.MOEHEWF.Nintex.Utilities.Constants.SCESectionManagerAccepted, txtFinalDecisionComments.Text, Utilities.Constants.SCERequests, RequestID.ToString());
                        
                        Common.BL.HistoricalRecords.AddHistoricalRecords(Utilities.Constants.SCERequests, Utilities.Constants.SCERequestHistory, LCID, (int)Common.Utilities.RequestStatus.SCESectionManagerAccepted, SPContext.Current.Web.CurrentUser.Name, string.Empty, RequestID.ToString(), "Yes");
                        SendEmail();
                        SendSMSForSubmition((int)Common.Utilities.RequestStatus.SCESectionManagerAccepted);



                    }
                    else if (drpFianlDecisonRecommendation.SelectedValue == "2") // Close by Rejection
                    {
                        //SCESectionManagerRejected
                        SaveSectionManagerDecision(false);
                        savedecisionPDF(drpFianlDecisonRecommendation.SelectedValue);
                        NintexHelper.ContinueTask(ITWorx.MOEHEWF.Nintex.Utilities.Constants.SCESectionManagerRejected, txtFinalDecisionComments.Text, Utilities.Constants.SCERequests, RequestID.ToString());
                        ctx.SubmitChanges();
                        Common.BL.HistoricalRecords.AddHistoricalRecords(Utilities.Constants.SCERequests, Utilities.Constants.SCERequestHistory, LCID, (int)Common.Utilities.RequestStatus.SCESectionManagerRejected, SPContext.Current.Web.CurrentUser.Name, string.Empty, RequestID.ToString(), "Yes");
                        SendEmail();
                        SendSMSForSubmition((int)Common.Utilities.RequestStatus.SCESectionManagerRejected);

                    }

                   
                

                        lblSuccess.Text = string.Format(HelperMethods.LocalizedText("ITWORX_MOEHEWF_SCE", "SubmitSucceed", (uint)LCID), requestNumber);
                    modalPopUpConfirmation.Show();
                }
                catch (Exception ex)
                {
                    Logging.GetInstance().LogException(ex);
                }
                
                }
            
        }

        private void SubmitFinalRejectDecision() // Rejected from Depatemrnt, i will close the workflow
        {
            //Rejected Request 

            using (SCEContextDataContext ctx = new SCEContextDataContext(RootWebURL))
            {
                try
                {
                  // the input of the statu already dimmed because the department manger had the decision

                    if (drpFinalRejectRecommendReadOnly.SelectedValue == "1")// Close by Approval
                    {
                        //SCESectionManagerAccepted

                        SaveFinalRejectDecision(false);
                        savedecisionPDF(drpFinalRejectRecommendReadOnly.SelectedValue);
                        NintexHelper.ContinueTask(ITWorx.MOEHEWF.Nintex.Utilities.Constants.SCESectionManagerAccepted, txtFinalDecisionComments.Text, Utilities.Constants.SCERequests, RequestID.ToString());
                        Common.BL.HistoricalRecords.AddHistoricalRecords(Utilities.Constants.SCERequests, Utilities.Constants.SCERequestHistory, LCID, (int)Common.Utilities.RequestStatus.SCESectionManagerAccepted, SPContext.Current.Web.CurrentUser.Name, string.Empty, RequestID.ToString(), "Yes");


                    }
                    else if (drpFinalRejectRecommendReadOnly.SelectedValue == "2") // Close by Rejection
                    {
                        //SCESectionManagerRejected
                        SaveFinalRejectDecision(false);
                        savedecisionPDF(drpFinalRejectRecommendReadOnly.SelectedValue);
                        NintexHelper.ContinueTask(ITWorx.MOEHEWF.Nintex.Utilities.Constants.SCESectionManagerRejected, txtFinalDecisionComments.Text, Utilities.Constants.SCERequests, RequestID.ToString());
                        ctx.SubmitChanges();
                        Common.BL.HistoricalRecords.AddHistoricalRecords(Utilities.Constants.SCERequests, Utilities.Constants.SCERequestHistory, LCID, (int)Common.Utilities.RequestStatus.SCESectionManagerRejected, SPContext.Current.Web.CurrentUser.Name, string.Empty, RequestID.ToString(), "Yes");
                    }

                    lblSuccess.Text = string.Format(HelperMethods.LocalizedText("ITWORX_MOEHEWF_SCE", "SubmitSucceed", (uint)LCID), requestNumber);
                    modalPopUpConfirmation.Show();


                }
                catch (Exception ex)
                {
                    Logging.GetInstance().LogException(ex);
                }
                
            }



        }


        private void SaveManagerDecision( bool SubmitChanges)
        {
            HttpContext backupContext = HttpContext.Current;

            SPSecurity.RunWithElevatedPrivileges(() =>
            {
                using (SPSite site = new SPSite(SPContext.Current.Site.Url))
                {
                    using (SPWeb web = site.OpenWeb())
                    {
                        using (SCEContextDataContext ctx = new SCEContextDataContext(RootWebURL))
                        {
                            
                            HttpContext.Current = null;
                            try
                            {

                                web.AllowUnsafeUpdates = true;
                                string folderUrl = DateTime.Now.ToString("yyyy/MM/dd");
                                SPList list = web.Lists[Utilities.Constants.SCEHMDecision];
                                //Get Current RequestID By Request Number
                                SCEHMDecisionListFieldsContentType currentRequest = ctx.SCEHMDecisionList.ScopeToFolder("", true).Where(x => x.RequestIDId == RequestID).SingleOrDefault();
                                if (currentRequest == null)
                                {
                                    //create new item 
                                    SCEHMDecisionListFieldsContentType newDecision = new SCEHMDecisionListFieldsContentType
                                    {
                                        Comments = txtManagerDecisionComments.Text,
                                        RequestIDId = RequestID,


                                    };

                                    if (!string.IsNullOrEmpty(drpHMDecisionRecommend.SelectedValue))
                                    {
                                        LookupsCT reasonDetails = ctx.RejectionReasonsList.ScopeToFolder("", true).Where(r => r.Id == Convert.ToInt32(drpManagerDecision.SelectedValue) && r.Status == Status.Active).SingleOrDefault();

                                        if (drpManagerDecision.SelectedValue == "-2")
                                        {


                                            newDecision.RejectionReasonId = 0;
                                            newDecision.DecisionTitle = "";
                                            newDecision.OtherRejectionReason = drpManagerDecision.OtherValue;

                                        }
                                        else
                                        {
                                            if (reasonDetails != null)
                                            {
                                                newDecision.RejectionReasonId = reasonDetails.Id;
                                                newDecision.RejectionReasonTitle = reasonDetails.TitleAr;
                                                newDecision.OtherRejectionReason = "";

                                            }
                                        }
                                    }


                                    LookupsCT decisionDetails = ctx.RecommendationsList.ScopeToFolder("", true).Where(r => r.Id == Convert.ToInt32(drpHMDecisionRecommend.SelectedValue) && r.Status == Status.Active).SingleOrDefault();

                                    if (decisionDetails != null)
                                    {
                                        newDecision.DecisionId = decisionDetails.Id;
                                        newDecision.DecisionTitle = decisionDetails.TitleAr;
                                        newDecision.Title = decisionDetails.TitleAr;

                                    }



                                    SPFolder folder = Common.Utilities.BusinessHelper.CreateFolderInternal(list, list.RootFolder, folderUrl);
                                    newDecision.Path = folder.Url;
                                    ctx.SCEHMDecision.InsertOnSubmit(newDecision);


                                }
                                else
                                {
                                    currentRequest.RequestIDId = RequestID;
                                    currentRequest.Comments = txtManagerDecisionComments.Text;

                                    LookupsCT decisionDetails = ctx.RecommendationsList.ScopeToFolder("", true).Where(r => r.Id == Convert.ToInt32(drpHMDecisionRecommend.SelectedValue) && r.Status == Status.Active).SingleOrDefault();

                                    if (decisionDetails != null)
                                    {
                                        currentRequest.DecisionId = decisionDetails.Id;
                                        currentRequest.DecisionTitle = decisionDetails.TitleAr;
                                        currentRequest.Title = decisionDetails.TitleAr;

                                    }
                                    if (!string.IsNullOrEmpty(drpManagerDecision.SelectedValue))
                                    {
                                        LookupsCT reasonDetails = ctx.RejectionReasonsList.ScopeToFolder("", true).Where(r => r.Id == Convert.ToInt32(drpManagerDecision.SelectedValue) && r.Status == Status.Active).SingleOrDefault();

                                        if (drpManagerDecision.SelectedValue == "-2")
                                        {


                                            currentRequest.RejectionReasonId = 0;
                                            currentRequest.DecisionTitle = "";
                                            currentRequest.OtherRejectionReason = drpManagerDecision.OtherValue;

                                        }
                                        else
                                        {
                                            if (reasonDetails != null)
                                            {
                                                currentRequest.RejectionReasonId = reasonDetails.Id;
                                                currentRequest.RejectionReasonTitle = reasonDetails.TitleAr;
                                                currentRequest.OtherRejectionReason = "";


                                            }
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
                                // resetting the SPContext
                                if (HttpContext.Current == null)
                                {
                                    HttpContext.Current = backupContext;
                                }

                            }
                            if (SubmitChanges)
                            {
                                lblSaveSuccess.Text = string.Format(HelperMethods.LocalizedText("ITWORX_MOEHEWF_SCE", "SaveSucceed", (uint)LCID), requestNumber);
                                modalSavePopup.Show();
                            }
                            ctx.SubmitChanges();
                        }
                    }
                }

            });



        }

        private void SubmitManagerDecisio()
        {

            //Rejected Request 

            using (SCEContextDataContext ctx = new SCEContextDataContext(RootWebURL))
            {
                try
                {
                    // the input of the statu already dimmed because the department manger had the decision

                    if (drpHMDecisionRecommend.SelectedValue == "1")// Close by Approval
                    {


                        SaveManagerDecision(false);

                        
                        NintexHelper.ContinueTask(ITWorx.MOEHEWF.Nintex.Utilities.Constants.SCEDepartmentManagerAcceptDecision, txtFinalDecisionComments.Text, Utilities.Constants.SCERequests, RequestID.ToString());
                        
                        Common.BL.HistoricalRecords.AddHistoricalRecords(Utilities.Constants.SCERequests, Utilities.Constants.SCERequestHistory, LCID, (int)Common.Utilities.RequestStatus.SCEDepartmentManagerAcceptDecision, SPContext.Current.Web.CurrentUser.Name, string.Empty, RequestID.ToString(), "Yes");


                    }
                    else if (drpHMDecisionRecommend.SelectedValue == "2") // Close by Rejection
                    {

                        SaveManagerDecision(false);
                       
                        NintexHelper.ContinueTask(ITWorx.MOEHEWF.Nintex.Utilities.Constants.SCEDepartmentManagerRejectDecision, txtFinalDecisionComments.Text, Utilities.Constants.SCERequests, RequestID.ToString());
                        ctx.SubmitChanges();
                        Common.BL.HistoricalRecords.AddHistoricalRecords(Utilities.Constants.SCERequests, Utilities.Constants.SCERequestHistory, LCID, (int)Common.Utilities.RequestStatus.SCEDepartmentManagerRejectDecision, SPContext.Current.Web.CurrentUser.Name, string.Empty, RequestID.ToString(), "Yes");
                    }
                    lblSuccess.Text = string.Format(HelperMethods.LocalizedText("ITWORX_MOEHEWF_SCE", "SubmitSucceed", (uint)LCID), requestNumber);
                    modalPopUpConfirmation.Show();


                }
                catch (Exception ex)
                {
                    Logging.GetInstance().LogException(ex);
                }
              

               
            }
        }



        private void SendSMS()
        {

            try
            {
                Logging.GetInstance().Debug("Entering method PMDecisionSCEUserControl.SendSMS");
                using (SCEContextDataContext ctx = new SCEContextDataContext(RootWebURL))
                {

                    SCERequestsListFieldsContentType currentRequest = ctx.SCERequestsList.ScopeToFolder("", true).Where(a => a.Id == RequestID).SingleOrDefault();

                     SCENotificationsListFieldsContentType notfications = ctx.SCENotificationsList.ScopeToFolder("", true).Where(y => y.RequestStatusIdId == (int)Common.Utilities.RequestStatus.SCEAttendApplicant && y.Type==NotifcationType.SMS).SingleOrDefault();


                    if (currentRequest != null && notfications != null)
                    {


                         SCENotifcations.SendSMS(currentRequest.MobileNumber, string.Format (notfications.Body, currentRequest.RegisteredScholasticLevel.Title));

                    }

                }
            }
            catch (Exception ex)
            {
                Logging.GetInstance().LogException(ex);
            }
            finally
            {
                Logging.GetInstance().Debug("exit method PMDecisionSCEUserControl.SendSMS");
            }

        }

        private void SendSMSForSubmition(int status)
        {
            try
            {
                Logging.GetInstance().Debug("Entering method PMDecisionSCEUserControl.SendSMS");
                using (SCEContextDataContext ctx = new SCEContextDataContext(RootWebURL))
                {

                    SCERequestsListFieldsContentType currentRequest = ctx.SCERequestsList.ScopeToFolder("", true).Where(a => a.Id == RequestID).SingleOrDefault();

                    SCENotificationsListFieldsContentType notfications = ctx.SCENotificationsList.ScopeToFolder("", true).Where(y => y.RequestStatusIdId == status && y.Type == NotifcationType.SMS).SingleOrDefault();


                    if (currentRequest != null && notfications != null)
                    {
                        string smsBody = string.Format(notfications.Body, currentRequest.CertificateResourceTitle,currentRequest.RequestNumber, currentRequest.RequestNumber);


                        SCENotifcations.SendSMS(currentRequest.MobileNumber,smsBody );

                    }

                }
            }
            catch (Exception ex)
            {
                Logging.GetInstance().LogException(ex);
            }
            finally
            {
                Logging.GetInstance().Debug("exit method PMDecisionSCEUserControl.SendSMS");
            }

        }


        private void SendEmail()
        {
            try
            {
                Logging.GetInstance().Debug("Entering method PMDecisionSCEUserControl.SendEmail");
                using (SCEContextDataContext ctx = new SCEContextDataContext(RootWebURL))
                {

                    SCERequestsListFieldsContentType currentRequest = ctx.SCERequestsList.ScopeToFolder("", true).Where(a => a.Id == RequestID).SingleOrDefault();

                    SCENotificationsListFieldsContentType notfications = ctx.SCENotificationsList.ScopeToFolder("", true).Where(y => y.RequestStatusIdId == currentRequest.RequestStatus.Id && y.Type == NotifcationType.Email).SingleOrDefault();

                    string emailbody = string.Format(notfications.Body, requestNumber, currentRequest.CertificateResourceTitle, requestNumber, currentRequest.CertificateResourceTitle);

                    SCENotifcations.SendEmail(emailbody,notfications.Subject,currentRequest.Email,true, (int)currentRequest.Id);

                }
            }
            catch (Exception ex)
            {
                Logging.GetInstance().LogException(ex);
            }
            finally
            {
                Logging.GetInstance().Debug("exit method PMDecisionSCEUserControl.SendEmail");
            }

          

        }

        private void ViewCertificate( )
        {

            //string htmlPath = Server.MapPath("/Certificates/ApplicantSchoolingEquivilance.html");
            //string html = File.ReadAllText(htmlPath);


            //string htmlFormatted = string.Format(html, RequestID.ToString(), "Accepted");
            // PDFConverter.ConvertHTMLToPDF(htmlFormatted);

            using (SCEContextDataContext ctx = new SCEContextDataContext(RootWebURL))
            {

                SCERequestsListFieldsContentType currentRequest = ctx.SCERequestsList.ScopeToFolder("", true).Where(a => a.Id == RequestID).SingleOrDefault();
                if (currentRequest != null && currentRequest.RequestStatus.Id == (int)Common.Utilities.RequestStatus.SCESectionManagerAccepted
                    || currentRequest.RequestStatus.Id == (int)Common.Utilities.RequestStatus.SCESectionManagerRejected)
                {
                    SCERequestAttachments.ViewAttachments(Utilities.Constants.SCERequests, false, (int)RequestID, Response);
                }
                else
                {
                    SCERequestAttachments.ViewAttachments(Utilities.Constants.SCERequests, true, (int)RequestID, Response);

                }
            }


        }





       

        protected void drpRecommendations_SelectedIndexChanged(object sender, EventArgs e)
        {
            //DropDownList newobject = (DropDownList)sender;
            //if (newobject != null)
            //{
            //    if (newobject.SelectedValue == "2")
            //    {
            //        rejectionReasons.Visible = true;
            //        CommentsRequired.Visible = true;
            //        BindRejectionReasonsDDL();
            //        rqrrequiredComments.Enabled = true;

            //    }
            //    else
            //    {
            //        rejectionReasons.Visible = false;
            //        CommentsRequired.Visible = false;
            //        rqrrequiredComments.Enabled = false;



            //    }
            //}

        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
        }

        protected void btnSaveFinalDecision_Click(object sender, EventArgs e)
        {
            if (Page.IsValid)
            {
                SaveSectionManagerDecision(true);
            }
        }

        protected void btnSubmitFinalDecision_Click(object sender, EventArgs e)
        {
            if (Page.IsValid)
            {
                SubmitSectionManagerDecision();
            }
        }

        protected void btnViewFinalDecision_Click(object sender, EventArgs e)
        {
            ViewCertificate();
        }

        protected void drpFianlDecisonRecommendation_SelectedIndexChanged(object sender, EventArgs e)
        {

            DropDownList newobject = (DropDownList)sender;
            if (newobject != null)
            {
                var PEAcc = PEAcceptance();

                if ((PEAcc && newobject.SelectedValue == "1") || (!PEAcc && newobject.SelectedValue == "2"))
                {
                    if (newobject.SelectedValue == "2")
                    {
                        drpFinalDecisionRejectReason.Visible = true;
                        FinalDecisionCommentsRequired.Visible = true;
                        BindFinalDecisionRejectionReasonsDDL();
                        rqrFinalDecisonCommnets.Enabled = true;
                        divFinalDecisionRejectionReasons.Visible = true;
                    }
                    else
                    {
                        drpFinalDecisionRejectReason.Visible = false;
                        FinalDecisionCommentsRequired.Visible = false;
                        rqrFinalDecisonCommnets.Enabled = false;
                        divFinalDecisionRejectionReasons.Visible = false;
                        drpFinalDecisionRejectReason.IsRequired = false;
                    }
                }
                else
                {
                    string decision = PEAcc ? HelperMethods.LocalizedText("ITWORX_MOEHEWF_SCE", "Acceptance", (uint)LCID) : HelperMethods.LocalizedText("ITWORX_MOEHEWF_SCE", "Rejection", (uint)LCID);
                    lbl_ConfirmDecision.Text = string.Format(HelperMethods.LocalizedText("ITWORX_MOEHEWF_SCE", "ConfirmDecisionMsg", (uint)LCID), decision);
                    ModalPopupConfirmDecision.Show();
                }

            }

        }

        protected void btnFinalRejectView_Click(object sender, EventArgs e)
        {
            ViewCertificate();



        }

        protected void btnFinalRejectSubmit_Click(object sender, EventArgs e)
        {
           
                SubmitFinalRejectDecision();
               



        }

        protected void btnManagerDecisionSave_Click(object sender, EventArgs e)
        {
            if (Page.IsValid)
            {
                SaveManagerDecision(true);
            }
        }

        protected void btnManagerDecisionView_Click(object sender, EventArgs e)
        {

            ViewCertificate();
        }

        protected void btnManagerDecisionSubmit_Click(object sender, EventArgs e)
        {
            if (Page.IsValid)
            {
                SubmitManagerDecisio();
                
            }
        }

        protected void drpHMDecisionRecommend_SelectedIndexChanged(object sender, EventArgs e)
        {
            DropDownList newobject = (DropDownList)sender;
            if (newobject != null)
            {
                var PEAcc = PEAcceptance();

                if ((PEAcc && newobject.SelectedValue == "1") || (!PEAcc && newobject.SelectedValue == "2"))
                {
                    if (newobject.SelectedValue == "2")
                    {
                        dvFinalDecisionRejection.Visible = true;
                        rqrtxtManagerDecisionComments.Enabled = true;
                        dvFinalDecisionSpan.Visible = true;
                        BindManagerDecisionRejectionReasonsDDL();
                    }
                    else
                    {
                        drpManagerDecision.IsRequired = false;
                        dvFinalDecisionRejection.Visible = false;
                        rqrtxtManagerDecisionComments.Enabled = false;
                        dvFinalDecisionSpan.Visible = false;
                    }
                }
                else
                {
                    string decision = PEAcc ? HelperMethods.LocalizedText("ITWORX_MOEHEWF_SCE", "Acceptance", (uint)LCID) : HelperMethods.LocalizedText("ITWORX_MOEHEWF_SCE", "Rejection", (uint)LCID);
                    lbl_ConfirmDecision.Text = string.Format(HelperMethods.LocalizedText("ITWORX_MOEHEWF_SCE", "ConfirmDecisionMsg", (uint)LCID), decision);
                    ModalPopupConfirmDecision.Show();
                }
            }

        }
        bool PEAcceptance()
        {
            using (SCEContextDataContext ctx = new SCEContextDataContext(SPContext.Current.Site.RootWeb.Url))
            {
                var recommendation = ctx.SCESearchStatusRecommendationList.ScopeToFolder("", true).Where(x => x.RequestID.Id == RequestID).LastOrDefault();
                return recommendation.SendRequestToId == null;
            }
        }
        protected void btnSendSMSFinalConfirm_Click(object sender, EventArgs e)
        {

            SendSMS();



        }

        protected void btnSendSMSFinalReject_Click(object sender, EventArgs e)
        {

            SendSMS();

        }

        protected void btnDownload_Click(object sender, EventArgs e)
        {
            ViewCertificate();

        }

        protected void btnSaveOk_Click(object sender, EventArgs e)
        {
            Logging.GetInstance().Debug("Entering method PMDecisionSCEUserControl.btnSaveOk_Click");

            try
            { 
                modalSavePopup.Hide();
            }
            catch (Exception ex)
            {
                Logging.GetInstance().LogException(ex);
            }
            finally
            {
                Logging.GetInstance().Debug("Exiting method PMDecisionSCEUserControl.btnSaveOk_Click");
            }
        }

        protected void btnModalOK_Click(object sender, EventArgs e)
        {
            Logging.GetInstance().Debug("Entering method PMDecisionSCEUserControl.btnModalOK_Click");
            try
            {
                if (HelperMethods.InGroup(Common.Utilities.Constants.SCESectionManagers))
                {
                    SPUtility.Redirect(SPContext.Current.Web.Url + "/Pages/SCESectionManagerDashboard.aspx", SPRedirectFlags.DoNotEndResponse, HttpContext.Current);
                }
                else
                {
                    SPUtility.Redirect(SPContext.Current.Web.Url + "/Pages/SCEDepartmentManagerDashboard.aspx", SPRedirectFlags.DoNotEndResponse, HttpContext.Current);
                }
            }
            catch (Exception ex)
            {
                Logging.GetInstance().LogException(ex);
            }
            finally
            {
                Logging.GetInstance().Debug("Exiting method PMDecisionSCEUserControl.btnModalOK_Click");
            }
        }

        protected void btnConfirmDecisionOK_Click(object sender, EventArgs e)
        {
            Logging.GetInstance().Debug("Entering method PMDecisionSCEUserControl.btnConfirmDecisionOK_Click");
            try
            {
                SPUtility.Redirect(SPContext.Current.Web.Url + "/Pages/SCERejectRequestPM.aspx?RequestId="+RequestID, SPRedirectFlags.DoNotEndResponse, HttpContext.Current);

            }
            catch (Exception ex)
            {
                Logging.GetInstance().LogException(ex);
            }
            finally
            {
                Logging.GetInstance().Debug("Exiting method PMDecisionSCEUserControl.btnConfirmDecisionOK_Click");
            }
        }
        protected void btnConfirmDecisionCancel_Click(object sender, EventArgs e)
        {
            Logging.GetInstance().Debug("Entering method PMDecisionSCEUserControl.btnConfirmDecisionCancel_Click");
            try
            {
                if (HelperMethods.InGroup(Common.Utilities.Constants.SCESectionManagers))
                {
                    drpFianlDecisonRecommendation.SelectedIndex = 0;
                }
                else
                {
                    drpHMDecisionRecommend.SelectedIndex = 0;
                }
                ModalPopupConfirmDecision.Hide();
            }
            catch (Exception ex)
            {
                Logging.GetInstance().LogException(ex);
            }
            finally
            {
                Logging.GetInstance().Debug("Exiting method PMDecisionSCEUserControl.btnConfirmDecisionCancel_Click");
            }
        }
       
        private string  GetDecision(string selectedValue)
        {
            string decisionText = string.Empty;
           
          

                //GetCurrent Request 
                using (SCEContextDataContext ctx = new SCEContextDataContext(SPContext.Current.Site.RootWeb.Url))
            {
                int requestID = Convert.ToInt32(Request.QueryString["RequestId"]);

                SCERequestsListFieldsContentType currentRequest = ctx.SCERequestsList.ScopeToFolder("", true).Where(a => a.Id == requestID).SingleOrDefault();
                if (currentRequest != null)

                {
                    requestNumber = currentRequest.RequestNumber;
                   
                    int currentgrade = (int)currentRequest.RegisteredScholasticLevel.Id;

                   
                        
                        string certTypeAr = string.Empty;
                    string certTypeEn = string.Empty;

                    if (currentRequest.CertificateType != null)
                    {
                        CertificateTypeLookupsCT certifiateType = ctx.CertificateTypeCT.ScopeToFolder("", true).Where(x => x.Id == currentRequest.CertificateType.Id).FirstOrDefault();
                        if (certifiateType != null)
                        {
                            certTypeAr = certifiateType.TitleAr;
                            certTypeEn = certifiateType.Title;
                        }
                    }
                    NationalityCategoryListFieldsContentType nationalityCat = ctx.NationalityCategoryList.ScopeToFolder("", true).Where(y => y.Id == currentRequest.StdNationalityCatId).FirstOrDefault();

                   
                    NationalityItem nationalityItem = NationalityList.GetNationalityById(Convert.ToInt32(currentRequest.StdNationality.Id));
                    
                    string currentNatEn = nationalityItem.Title;
                    string currentNatAr = nationalityItem.TitleAr;
                    string signImg = SCERequestAttachments.GenerateImage(Server.MapPath("/Certificates/Signature.jpg"));
                    string warningImg = SCERequestAttachments.GenerateImage(Server.MapPath("/Certificates/warning.png"));
                    
                    if (currentgrade == 14 && selectedValue == "1")
                    {

                       

                        string htmlPath = Server.MapPath("/Certificates/SecondryApproval.html");
                        string localPath = new Uri(htmlPath).LocalPath;
                        string html = File.ReadAllText(htmlPath, UTF8Encoding.UTF8);




                        string formattedtext = string.Format(html, currentRequest.StdPrintedName, currentRequest.StdQatarID, currentNatAr, currentRequest.PrevSchool, currentNatAr, currentRequest.SubmitDate.ToDate().ToShortDateString(), certTypeAr, "percentage", currentRequest.StdPrintedName, currentRequest.StdQatarID, currentNatEn, currentRequest.PrevSchool, currentNatEn, currentRequest.SubmitDate.ToDate().ToShortDateString(), certTypeEn, "percentage");
                        formattedtext = formattedtext.Replace("signImg", signImg);
                        formattedtext = formattedtext.Replace("warningImg", warningImg);
                        decisionText = formattedtext;
                    }

                    if (currentgrade == 14 && selectedValue == "2")
                    {

                     

                        string htmlPath = Server.MapPath("/Certificates/SecondaryRefusal.html");
                        string localPath = new Uri(htmlPath).LocalPath;
                        string html = File.ReadAllText(htmlPath, UTF8Encoding.UTF8);

                        string formattedtext = string.Format(html, currentRequest.StdPrintedName, currentRequest.StdQatarID, currentNatAr, currentRequest.PrevSchool, currentNatAr, currentRequest.SubmitDate.ToDate().ToShortDateString(), certTypeAr, currentRequest.StdPrintedName, currentRequest.StdQatarID, currentNatEn, currentRequest.PrevSchool, currentNatEn, currentRequest.SubmitDate.ToDate().ToShortDateString(), certTypeEn);
                        formattedtext = formattedtext.Replace("signImg", signImg);
                        formattedtext = formattedtext.Replace("warningImg", warningImg);
                        decisionText = formattedtext;
                    }

                    else if (currentgrade < 14 && selectedValue == "1")
                    {
                        string htmlPath = Server.MapPath("/Certificates/ElementryTemplate.html");
                        string localPath = new Uri(htmlPath).LocalPath;
                        string html = File.ReadAllText(htmlPath, UTF8Encoding.UTF8);
                        CertificateResourceItem certificateResourceItem = CertificateResourceList.GetCertificateResourceById(Convert.ToInt32(currentRequest.CertificateResourceId));

                        string certificateSourceEn = string.Empty;
                        string certificateSourceAr = string.Empty;

                        if (currentRequest.CertificateResourceId != null && !string.IsNullOrEmpty(currentRequest.PrevSchool))
                        {

                            certificateSourceEn = currentRequest.PrevSchool + "/" + certificateResourceItem.Title;
                            certificateSourceAr = currentRequest.PrevSchool + "/" + certificateResourceItem.TitleAr;
                        }
                        else if (currentRequest.CertificateResourceId == null && !string.IsNullOrEmpty(currentRequest.OtherCertificateResource) && !string.IsNullOrEmpty(currentRequest.PrevSchool))
                        {

                            certificateSourceEn = currentRequest.PrevSchool + "/" + currentRequest.OtherCertificateResource;
                            certificateSourceAr = currentRequest.PrevSchool + "/" + currentRequest.OtherCertificateResource;
                        }
                        else if (currentRequest.CertificateResourceId != null && string.IsNullOrEmpty(currentRequest.PrevSchool))
                        {

                            certificateSourceEn = certificateResourceItem.Title;
                            certificateSourceAr = certificateResourceItem.TitleAr;
                        }
                        else if (currentRequest.CertificateResourceId == null && !string.IsNullOrEmpty(currentRequest.OtherCertificateResource) && string.IsNullOrEmpty(currentRequest.PrevSchool))
                        {

                            certificateSourceEn = currentRequest.OtherCertificateResource;
                            certificateSourceAr = currentRequest.OtherCertificateResource;
                        }


                        var lastScholasticLevel = ctx.ScholasticLevelList.ScopeToFolder("", true).Where(a => a.Id == currentRequest.LastScholasticLevel.Id).SingleOrDefault();
                        var regScholasticLevel = ctx.ScholasticLevelList.ScopeToFolder("", true).Where(a => a.Id == currentRequest.RegisteredScholasticLevel.Id).SingleOrDefault();

                       

                        string formattedtext = string.Format(html, currentRequest.StdPrintedName, currentNatAr, currentRequest.StdQatarID, lastScholasticLevel.TitleAr, currentRequest.LastAcademicYear, certificateSourceAr, regScholasticLevel.TitleAr, HelperMethods.LocalizedText("ITWORX_MOEHEWF_SCE", "ProgramManagerSignature", (uint)LCID));//, currentRequest.StdPrintedName, currentNatEn, currentRequest.StdQatarID, lastScholasticLevel.Title, currentRequest.LastAcademicYear, certificateSourceEn, regScholasticLevel.Title);
                        formattedtext = formattedtext.Replace("signImg", signImg);
                        formattedtext = formattedtext.Replace("warningImg", warningImg);
                        decisionText = formattedtext; 
                    }


                }
            }
            return decisionText;
        }

        private void savedecisionPDF(string selectedValue)
        {
            Logging.GetInstance().Debug("Entering method PMDecisionSCEUserControl.savedecisionPDF");
            try
            {
                string htmlPath = Server.MapPath("/Certificates/DefaultWithHeader.html");
                string localPath = new Uri(htmlPath).LocalPath;
                string html = File.ReadAllText(htmlPath, UTF8Encoding.UTF8);

                int RequestID = Convert.ToInt32(Request.QueryString["RequestId"].ToString());
                MemoryStream currentOutPut = new MemoryStream();
                using (SCEContextDataContext ctx = new SCEContextDataContext(SPContext.Current.Site.RootWeb.Url))
                {
                    SCERequestsListFieldsContentType currentRequest = ctx.SCERequestsList.ScopeToFolder("", true).Where(a => a.Id == RequestID).SingleOrDefault();
                    if (currentRequest != null)
                    {
                        string generatedImage = SCERequestAttachments.GenerateBarcodeImage(requestNumber);

                        string minImage = SCERequestAttachments.GenerateImage(Server.MapPath("/Certificates/MinistryLogo.JPG"));

                        string footerImage1 = SCERequestAttachments.GenerateImage(Server.MapPath("/Certificates/footerone.png"));

                        string footerImage2 = SCERequestAttachments.GenerateImage(Server.MapPath("/Certificates/footertwo.png"));

                        string footerNoteImage = SCERequestAttachments.GenerateImage(Server.MapPath("/Certificates/footernote.png"));

                        string htmlFormatted = string.Format(html, requestNumber, currentRequest.CreateDate.Value.ToShortDateString(), GetDecision(selectedValue));
                        htmlFormatted = htmlFormatted.Replace("ministryImg", minImage);
                        htmlFormatted = htmlFormatted.Replace("barCodeImg", generatedImage);
                        htmlFormatted = htmlFormatted.Replace("footerImg1", footerImage1);
                        htmlFormatted = htmlFormatted.Replace("footerImg2", footerImage2);
                        htmlFormatted = htmlFormatted.Replace("footerNoteImg", footerNoteImage);

                        currentOutPut = SCERequestAttachments.GetPDFFile(htmlFormatted, currentRequest.RequestNumber, (Common.Utilities.RequestStatus)currentRequest.RequestStatus.Id);

                    }
                }





                SPSecurity.RunWithElevatedPrivileges(() =>
                {
                    using (SPSite site = new SPSite(SPContext.Current.Site.RootWeb.Url))
                    {
                        using (SPWeb web = site.RootWeb)
                        {
                            SPList list = web.Lists["SCERequests"];
                            SPListItemCollection collListItems = null;
                            var camelQuery = Common.Utilities.BusinessHelper.GetQueryObject("<Where><Eq><FieldRef Name ='ID'/><Value Type = 'Number'>" +
                               RequestID.ToString() + "</Value></Eq></Where>");

                            collListItems = list.GetItems(camelQuery);
                            if (collListItems != null)
                            {
                                SPListItem item = collListItems[0];
                                if (item.Attachments.Count > 0)
                                {
                                    item.Attachments.Delete("FinalDecision.pdf");
                                }
                                item.Attachments.Add("FinalDecision.pdf", currentOutPut.ToArray());
                                item.Update();

                            }


                        }
                    }

                }

                );


            }
            catch (Exception ex)
            {
                Logging.GetInstance().LogException(ex);
            }
            finally
            {
                Logging.GetInstance().Debug("Exiting method PMDecisionSCEUserControl.savedecisionPDF");
            }
        }
       


    }
}
