using ITWORX.MOEHE.Utilities;
using ITWORX.MOEHE.Utilities.Logging;
using ITWORX.MOEHEWF.Common.Entities;
using ITWORX.MOEHEWF.UCE.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;

namespace ITWORX.MOEHEWF.UCE.CONTROLTEMPLATES.ITWORX.MOEHEWF.UCE
{
    public partial class HistoricalRecords : UserControlBase
    {
        public bool isApplicant { get { return HelperMethods.InGroup(Common.Utilities.Constants.ApplicanstGroupName); } }


        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                if (Page.Session["DisplayRequestId"] != null)
                {
                    BindGrid(Page.Session["DisplayRequestId"].ToString());
                    BindControls(Page.Session["DisplayRequestId"].ToString(), LCID);
                }
                else if (Page.Session["EditRequestId"]!=null)
                {
                    BindGrid(Page.Session["EditRequestId"].ToString());
                    BindControls(Page.Session["EditRequestId"].ToString(), LCID);
                }
            }
        }
        private void BindControls(string reqID,int LCID)
        {
            try
            {
                Logging.GetInstance().Debug("Entering method HistoricalRecords.BindControls");
                List<Entities.SimilarRequest> Record = BL.HistoricalRecords.GetRequestDetailsbyID(reqID, LCID);
                lbl_DateVal.Text = Record[0].SubmitDate == null ? string.Empty : Record[0].SubmitDate.ToShortDateString();
                lbl_degreeVal.Text = Record[0].AcademicDegree == null ? string.Empty : Record[0].AcademicDegree.ToString();
                lbl_QatariIDVal.Text = Record[0].QatariID == null ? string.Empty : Record[0].QatariID.ToString();
                lbl_RequestIDVal.Text = Record[0].RequestID == null ? string.Empty : Record[0].RequestID.ToString();
                lbl_ApplicantNameVal.Text = Record[0].ApplicantName == null ? string.Empty : Record[0].ApplicantName.ToString();
            }
            catch (Exception ex)
            {
                Logging.GetInstance().LogException(ex);
            }
            finally
            {
                Logging.GetInstance().Debug("Exiting method HistoricalRecords.BindControls");
            }
        }
        private void BindGrid(string reqID)
        {
            try
            {
                Logging.GetInstance().Debug("Entering method HistoricalRecords.BindGrid");
                var historicalRecords = Common.BL.HistoricalRecords.GetReqHistoricalRecords(LCID, reqID, Utilities.Constants.RequestHistoricalRecords);
                if (isApplicant)
                {
                    var historicalRecordsIitems = historicalRecords.GroupBy(r => r.ExecutedAction)
                                                           .Select(hr => new { HistoricalRecords = hr.Key, Items = hr.ToList() })
                                                           .ToList();
                    historicalRecords = new List<HistoricalRecord>();
                    foreach (var item in historicalRecordsIitems)
                    {
                        historicalRecords.Add(item.Items[0]);
                    }

                    grd_HistoricalRecords.Columns[2].Visible = false;
                    grd_HistoricalRecords.Columns[3].Visible = false;


                }
                HelperMethods.BindGridView(grd_HistoricalRecords, historicalRecords);

            }
            catch (Exception ex)
            {
                Logging.GetInstance().LogException(ex);
            }
            finally
            {
                Logging.GetInstance().Debug("Exiting method HistoricalRecords.BindGrid");
            }
        }

        protected void grd_HistoricalRecords_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            Logging.GetInstance().Debug("Enter HistoricalRecords.grd_HistoricalRecords_PageIndexChanging");
            try
            {
                grd_HistoricalRecords.PageIndex = e.NewPageIndex;
                BindGrid(Page.Session["DisplayRequestId"].ToString());
            }
            catch (Exception ex)
            {
                Logging.GetInstance().LogException(ex);
            }
            finally
            {

                Logging.GetInstance().Debug("Exit HistoricalRecords.grd_HistoricalRecords_PageIndexChanging");
            }
        }
    }
}

