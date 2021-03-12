using ITWORX.MOEHE.Utilities;
using ITWORX.MOEHE.Utilities.Logging;
using ITWORX.MOEHEWF.Common.Entities;
using ITWORX.MOEHEWF.PA.Utilities;
using Microsoft.SharePoint;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ITWORX.MOEHEWF.PA.CONTROLTEMPLATES.ITWORX.MOEHEWF.PA
{
    public partial class PAHistoricalRecords : UserControlBase
    {
        public bool isApplicant { get { return HelperMethods.InGroup(Common.Utilities.Constants.ApplicanstGroupName); } }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                if (Page.Session["PADisplayRequestId"] != null)
                {
                    BindGrid(Page.Session["PADisplayRequestId"].ToString());
                    BindControls(Page.Session["PADisplayRequestId"].ToString(), LCID);
                }
                else if (Page.Session["PAEditRequestId"] != null)
                {
                    BindGrid(Page.Session["PAEditRequestId"].ToString());
                    BindControls(Page.Session["PAEditRequestId"].ToString(), LCID);
                }
            }
        } 
        private void BindControls(string reqID, int LCID)
        {
            try
            {
                Logging.GetInstance().Debug("Entering method PAHistoricalRecords.BindControls");
                List<Entities.SimilarRequest> Record = BL.PAHistoricalRecords.GetPARequestDetailsbyID(reqID, LCID);
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
                Logging.GetInstance().Debug("Exiting method PAHistoricalRecords.BindControls");
            }
        }

        private void BindGrid(string reqID)
        {
            try
            {
                
                Logging.GetInstance().Debug("Entering method PAHistoricalRecords.BindGrid");
                var historicalRecords = Common.BL.HistoricalRecords.GetReqHistoricalRecords(LCID, reqID, Utilities.Constants.RequestPAHistoricalRecords);
                if(isApplicant)
                {
                    var historicalRecordsIitems = historicalRecords.GroupBy(r => r.ExecutedAction)
                                                           .Select(hr => new { HistoricalRecords = hr.Key, Items =hr.ToList()})
                                                           .ToList(); 
                    historicalRecords = new List<HistoricalRecord>();
                    foreach(var item in historicalRecordsIitems)
                    {
                        historicalRecords.Add(item.Items[0]);
                    }

                    grd_PAHistoricalRecords.Columns[2].Visible = false;
                    grd_PAHistoricalRecords.Columns[3].Visible = false;


                }
                HelperMethods.BindGridView(grd_PAHistoricalRecords, historicalRecords);
            }
            catch (Exception ex)
            {
                Logging.GetInstance().LogException(ex);
            }
            finally
            {
                Logging.GetInstance().Debug("Exiting method PAHistoricalRecords.BindGrid");
            }
        }

        protected void grd_PAHistoricalRecords_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            Logging.GetInstance().Debug("Enter PAHistoricalRecords.grd_PAHistoricalRecords_PageIndexChanging");
            try
            {
                grd_PAHistoricalRecords.PageIndex = e.NewPageIndex;
                BindGrid(Page.Session["PADisplayRequestId"].ToString());
            }
            catch (Exception ex)
            {
                Logging.GetInstance().LogException(ex);
            }
            finally
            {
                Logging.GetInstance().Debug("Exit PAHistoricalRecords.grd_PAHistoricalRecords_PageIndexChanging");
            }
        }
    }
}