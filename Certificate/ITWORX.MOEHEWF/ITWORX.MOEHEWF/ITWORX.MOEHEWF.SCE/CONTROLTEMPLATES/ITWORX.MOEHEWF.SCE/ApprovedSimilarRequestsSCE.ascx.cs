using ITWORX.MOEHE.Utilities.Logging;
using Microsoft.SharePoint;
using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Linq;
using ITWORX.MOEHEWF.SCE.Entities;
using ITWORX.MOEHEWF.Common.Utilities;
using ITWORX.MOEHE.Utilities;
using System.Collections.Generic;
using ITWORX.MOEHEWF.SCE.BL;

namespace ITWORX.MOEHEWF.SCE.CONTROLTEMPLATES.ITWORX.MOEHEWF.SCE
{
    public partial class ApprovedSimilarRequestsSCE : UserControlBase
    {
        private int RequestId { get { return Convert.ToInt32(Request.QueryString["RequestId"]); } }

        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                Logging.GetInstance().Debug("Entering method ApprovedSimilarRequestsSCE.Page_Load");
                var hdnCtl = Page.Request.Form.Get("__EventTriggerApprovedControl");
                if (Request.Form["__EVENTTARGET"] == null)
                {
                    BindData();
                }
                else if (Request.Form["__EVENTTARGET"].ToString().Contains("Menu_Links"))
                {
                    BindData();
                }
                else if (string.IsNullOrEmpty(hdnCtl) && !Request.Form["__EVENTTARGET"].ToString().Contains(grd_ApprovedResults.ID))
                {
                    BindData();
                }
            }
            catch (Exception ex)
            {
                Logging.GetInstance().LogException(ex);
            }
            finally
            {
                Logging.GetInstance().Debug("Exiting method ApprovedSimilarRequestsSCE.Page_Load");
            }
        }

        private void BindData()
        {
            try
            {
                Logging.GetInstance().Debug("Entering method ApprovedSimilarRequestsSCE.Page_Load");
                string rootWebUrl = SPContext.Current.Site.RootWeb.Url;
                List<SearchSimilarRequest> similarRequests = new List<SearchSimilarRequest>();
                SPSecurity.RunWithElevatedPrivileges(delegate ()
                {
                    using (SCEContextDataContext ctx = new SCEContextDataContext(rootWebUrl))
                    {
                        SCERequestsListFieldsContentType currentRequest = ctx.SCERequestsList.ScopeToFolder("", true).Where(r => r.Id == RequestId).FirstOrDefault();
                        if (currentRequest !=null &&currentRequest.RelatedRequests != null)
                        {
                            string[] relatedIds = currentRequest.RelatedRequests.Split(';');
                            for (int i = 0; i < relatedIds.Length; i++)
                            {
                                if (!string.IsNullOrEmpty(relatedIds[i]))
                                {
                                    SCERequestsListFieldsContentType item = ctx.SCERequestsList.ScopeToFolder("", true).Where(r => r.Id == Convert.ToInt32(relatedIds[i])).FirstOrDefault();
                                    SearchSimilarRequest req = new SearchSimilarRequest();
                                    req.RequestStatusId = item.RequestStatus.Id.Value;
                                    req.RegisteredSchoolId = item.RegisteredScholasticLevel.Id.Value;
                                    req.Id = item.Id;
                                    req.RequestNumber = item.RequestNumber;
                                    req.RequestDate = item.DecisionDate.ToString();
                                    req.RequestUrl = SPContext.Current.Web.Url + "/Pages/ViewRequest.aspx?RequestId=" + req.Id;
                                    if (item.StdNationality != null)
                                    {
                                        var natObj = Common.BL.Nationality.GetAll();// ctx.NationalityList.ToList();
                                        req.Nationality = LCID == (int)Language.English ? natObj.Where(n => n.ID == item.StdNationality.Id).FirstOrDefault().EnglishTitle : natObj.Where(n => n.ID == item.StdNationality.Id).FirstOrDefault().ArabicTitle;
                                    }
                                    else
                                        req.Nationality = "";
                                    req.ApplicantName = item.StdPrintedName;
                                    req.ApplicantNumber = item.StdQatarID;


                                    if (string.IsNullOrEmpty(item.OtherCertificateResource))
                                    {
                                        if (item.CertificateResourceId != null)
                                        {
                                            var certificateResourceItem = Common.BL.CountryOfStudy.GetAll(LCID);//ctx.CountryOfStudyList.Where(c => c.Id == Convert.ToInt32(item.CertificateResourceId)).FirstOrDefault();
                                            req.CertificateOrigin = LCID == (int)Language.English ? certificateResourceItem.Where(c => c.ID == Convert.ToInt32(item.CertificateResourceId)).FirstOrDefault().EnglishTitle : certificateResourceItem.Where(c => c.ID == Convert.ToInt32(item.CertificateResourceId)).FirstOrDefault().ArabicTitle;
                                        }
                                        else
                                            req.CertificateOrigin = "";
                                    }
                                    else
                                    {
                                        req.CertificateOrigin = item.OtherCertificateResource;
                                    }

                                    if (string.IsNullOrEmpty(item.OtherSchoolType))
                                    {
                                        if (item.SchoolType != null)
                                            req.SchoolType = LCID == (int)Language.English ? ((SchoolTypeLookupsCT)item.SchoolType).Title : ((SchoolTypeLookupsCT)item.SchoolType).TitleAr;
                                    }
                                    else
                                    {
                                        req.SchoolType = item.OtherSchoolType;
                                    }


                                    req.CertificateType = item.CertificateType == null ? "" : item.CertificateType.Title;
                                    similarRequests.Add(req);



                                }

                            }
                        }
                        HelperMethods.BindGridView(grd_ApprovedResults, similarRequests);

                        if (similarRequests.Count > 0)
                        {
                            lbl_NoOfRequests.Text = HelperMethods.LocalizedText("ITWORX_MOEHEWF_SCE", "NoOfRequests", (uint)LCID) + similarRequests.Count;
                            lbl_NoOfRequests.Visible = true;
                            if (HelperMethods.InGroup(Utilities.Constants.SCEEquivalenceEmployees))
                                btn_RemoveAttached.Visible = true;
                            else
                                btn_RemoveAttached.Visible = false;
                        }
                        else
                        {
                            lbl_NoOfRequests.Visible = false;
                            btn_RemoveAttached.Visible = false;

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
                Logging.GetInstance().Debug("Exiting method ApprovedSimilarRequestsSCE.Page_Load");
            }
        }

        protected void lnk_View_Click(object sender, EventArgs e)
        {
            try
            {
                Logging.GetInstance().Debug("Entering method ApprovedSimilarRequestsSCE.lnk_View_Click");
                SPSecurity.RunWithElevatedPrivileges(delegate ()
                {
                    LinkButton lnkButton = (LinkButton)sender;
                    GridViewRow gvr = (GridViewRow)lnkButton.NamingContainer;
                    HiddenField hdn_RequestId = (HiddenField)gvr.FindControl("hdn_AppID");
                    SCERequestAttachments.ViewAttachments(Utilities.Constants.SCERequests, false, Convert.ToInt32(hdn_RequestId.Value), Response);
                });
            }
            catch (Exception ex)
            {
                Logging.GetInstance().LogException(ex);
            }
            finally
            {
                Logging.GetInstance().Debug("Exiting method ApprovedSimilarRequestsSCE.lnk_View_Click");
            }
        }



        protected void btn_RemoveAttached_Click(object sender, EventArgs e)
        {
            try
            {
                Logging.GetInstance().Debug("Entering method ApprovedSimilarRequestsSCE.btn_Save_Click");
                string requestId = Request.QueryString["RequestId"];
                string removedReqs = "";
                using (SCEContextDataContext ctx = new SCEContextDataContext(SPContext.Current.Site.RootWeb.Url))
                {
                    for (int i = 0; i < grd_ApprovedResults.Rows.Count; i++)
                    {
                        CheckBox chkStat = grd_ApprovedResults.Rows[i].FindControl("chk_AppReqs") as CheckBox;

                        if (chkStat.Checked)
                        {
                            HiddenField reqId = grd_ApprovedResults.Rows[i].FindControl("hdn_AppID") as HiddenField;
                            removedReqs += reqId.Value + ";";

                        }
                    }
                    SCERequestsListFieldsContentType request = ctx.SCERequestsList.ScopeToFolder("", true).Where(c => c.Id.Value == Convert.ToInt32(requestId)).FirstOrDefault();
                    request.RelatedRequests = request.RelatedRequests.Replace(removedReqs, "");

                    ctx.SubmitChanges();

                }
                BindData();
            }
            catch (Exception ex)
            {
                Logging.GetInstance().LogException(ex);
            }
            finally
            {
                Logging.GetInstance().Debug("Exiting method ApprovedSimilarRequestsSCE.btn_Save_Click");
            }
        }

        protected void grd_ApprovedResults_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            try
            {
                Logging.GetInstance().Debug("Entering method ApprovedSimilarRequestsSCE.grd_ApprovedResults_RowDataBound");
                if (HelperMethods.InGroup(Utilities.Constants.SCEEquivalenceEmployees))
                {
                    e.Row.Cells[11].Visible = true;
                }
                else
                {
                    e.Row.Cells[11].Visible = false;
                }

                if (e.Row.RowType == DataControlRowType.DataRow)
                {
                    LinkButton viewLink = (LinkButton)e.Row.FindControl("lnk_View");

                    HiddenField hdnRequestStatus = (HiddenField)e.Row.FindControl("hdnRequestStatus");
                    HiddenField hdnRegisteredLevel = (HiddenField)e.Row.FindControl("hdnRegisteredLevel");


                    int requestStatusId = int.Parse(hdnRequestStatus.Value);
                    if (requestStatusId == (int)RequestStatus.SCESectionManagerAccepted
                        || (!string.IsNullOrEmpty(hdnRegisteredLevel.Value) && int.Parse(hdnRegisteredLevel.Value) == 14
                        && requestStatusId == (int)RequestStatus.SCESectionManagerRejected))

                    {
                        viewLink.Visible = true;
                    }
                    else
                    {
                        viewLink.Visible = false;
                    }
                }
                }
            catch (Exception ex)
            {
                Logging.GetInstance().LogException(ex);
            }
            finally
            {
                Logging.GetInstance().Debug("Exiting method ApprovedSimilarRequestsSCE.grd_ApprovedResults_RowDataBound");
            }
        }

        protected void grd_ApprovedResults_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            try
            {
                Logging.GetInstance().Debug("Entering ApprovedSimilarRequestsSCE.grd_ApprovedResults_PageIndexChanging");
                grd_ApprovedResults.PageIndex = e.NewPageIndex;
                BindData();
            }
            catch (Exception ex)
            {
                Logging.GetInstance().LogException(ex);
            }
            finally
            {

                Logging.GetInstance().Debug("Exit ApprovedSimilarRequestsSCE.grd_ApprovedResults_PageIndexChanging");
            }
        }
    }
}
