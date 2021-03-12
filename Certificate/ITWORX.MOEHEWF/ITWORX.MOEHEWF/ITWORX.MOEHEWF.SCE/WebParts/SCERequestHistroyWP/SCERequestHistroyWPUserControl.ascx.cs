using ITWORX.MOEHE.Utilities;
using ITWORX.MOEHE.Utilities.Logging;
using ITWORX.MOEHEWF.Common.Entities;
using ITWORX.MOEHEWF.Common.Utilities;
using Microsoft.SharePoint;
using System;
using System.Linq;
using System.Collections.Generic;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;

namespace ITWORX.MOEHEWF.SCE.WebParts.SCERequestHistroyWP
{
    public partial class SCERequestHistroyWPUserControl : UserControlBase
    {

        public int? RequestID
        {
            get
            {
                if (Request.QueryString["requestID"] != null)
                {
                    if (!string.IsNullOrEmpty(Request.QueryString["requestID"]))
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
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                GetRequestDetails();
                BindGrid(RequestID.ToString());
            }
        }


        private void GetRequestDetails()
        {
            try
            {


                using (SCEContextDataContext ctx = new SCEContextDataContext(SPContext.Current.Site.RootWeb.Url))
                {
                    SCERequestsListFieldsContentType request = ctx.SCERequestsList.ScopeToFolder("", true).Where(r => r.Id == RequestID).FirstOrDefault();

                    if (request != null)
                    {
                        lblRequestDate.Text = ((DateTime)request.SubmitDate).ToString("yyyy/MM/dd");
                        lblRequestNo.Text = request.RequestNumber != null ? request.RequestNumber : string.Empty;
                        lblCertEquivalance.Text = request.RegisteredScholasticLevel != null ? request.RegisteredScholasticLevel.Title.ToString() : string.Empty;
                        lblStudentName.Text = request.StdName != null ? request.StdName : string.Empty;
                        lblStudentNo.Text = request.StdQatarID != null ? request.StdQatarID : string.Empty;
                        lblApplicantName.Text = request.ApplicantOfficialName != null ? request.ApplicantOfficialName : string.Empty;
                    }
                }
            }
            catch (Exception ex)
            {

                Logging.GetInstance().LogException(ex);


            }

        }

        private void BindGrid(string reqID)
        {
            try
            {
                Logging.GetInstance().Debug("Entering method SCERequestHistroyWPUserControl.BindGrid");
                if (!string.IsNullOrEmpty(reqID))
                {
                    var historicalRecords = Common.BL.HistoricalRecords.GetReqHistoricalRecords(LCID, reqID, Utilities.Constants.SCERequestHistory);
                    //var historicalRecordsIitems = historicalRecords.GroupBy(r => r.ExecutedAction)
                    //                                           .Select(hr => new { HistoricalRecords = hr.Key, Items = hr.ToList() })
                    //                                           .ToList();
                    //historicalRecords = new List<HistoricalRecord>();
                    //foreach (var item in historicalRecordsIitems)
                    //{
                    //    historicalRecords.Add(item.Items[0]);
                    //}

                    //grd_HistoricalRecords.Columns[2].Visible = false;
                    //grd_HistoricalRecords.Columns[3].Visible = false;



                    HelperMethods.BindGridView(grd_HistoricalRecords, historicalRecords);
                }

            }
            catch (Exception ex)
            {
                Logging.GetInstance().LogException(ex);
            }
            finally
            {
                Logging.GetInstance().Debug("Exiting method SCERequestHistroyWPUserControl.BindGrid");
            }
        }


        protected void grd_HistoricalRecords_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {

            Logging.GetInstance().Debug("Enter SCERequestHistroyWPUserControl.grd_HistoricalRecords_PageIndexChanging");
            try
            {
                grd_HistoricalRecords.PageIndex = e.NewPageIndex;
                BindGrid(RequestID.ToString());
            }
            catch (Exception ex)
            {
                Logging.GetInstance().LogException(ex);
            }
            finally
            {

                Logging.GetInstance().Debug("Exit SCERequestHistroyWPUserControl.grd_HistoricalRecords_PageIndexChanging");
            }

        }
    }
}
