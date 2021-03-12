using Microsoft.SharePoint;
using System;
using System.Collections.Generic;
using System.Web.UI;

namespace ITWORX.MOEHEWF.PA.CONTROLTEMPLATES.ITWORX.MOEHEWF.PA
{
    public partial class Procedure_LegalAffairsViewOpinion : UserControl
    {
        protected global::ITWORX.MOEHEWF.Common.CONTROLTEMPLATES.ITWORX.MOEHEWF.Common.FileUpload legalAffairsAttachements;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Page.Session["PADisplayRequestId"] != null)
            {
                List<Entities.Procedures> InitialOpinion = BL.AllProcedures.GetLegalAffairsOpinionbyReqID(Page.Session["PADisplayRequestId"].ToString());
                if (InitialOpinion.Count > 0)
                {
                    lbl_InitialOpinionVal.Text = InitialOpinion[0].Opinion;
                    lnk_AddNewOpinionPopUp.Visible = false;
                    lbl_InitialOpinion.Visible = true;
                    lbl_InitialOpinionVal.Visible = true;
                    BindAttachements();
                }
            }
        }

        protected void lnk_AddNewOpinionPopUp_Click(object sender, EventArgs e)
        {
            Response.Redirect(SPContext.Current.Web.Url + "/_layouts/15/ITWORX.MOEHEWF.PA/LegalAffairs.aspx");
        }

        private void BindAttachements()
        {
            #region Prerequiestes

            /// add colum "RequestID" lookup from Requests
            /// add column "DocumentStatus" choice field, values: Saved,Uploaded,Deleted
            /// add column Group, single line of text

            #endregion Prerequiestes

            #region Display Mode

            legalAffairsAttachements.DocumentLibraryName = Utilities.Constants.PALegalAffairsAttachements;
            legalAffairsAttachements.DocLibWebUrl = SPContext.Current.Site.Url;

            legalAffairsAttachements.LabelDisplayName = "المذكرات التي تم إعدادها";
            legalAffairsAttachements.Group = "المذكرات التي تم إعدادها";
            legalAffairsAttachements.RequestID = Convert.ToInt32(Page.Session["PADisplayRequestId"].ToString());
            legalAffairsAttachements.Enabled = false;

            legalAffairsAttachements.Bind();
            if (legalAffairsAttachements.AttachmentsCount == 0)
                legalAffairsAttachements.Visible = false;

            #endregion Display Mode
        }
    }
}