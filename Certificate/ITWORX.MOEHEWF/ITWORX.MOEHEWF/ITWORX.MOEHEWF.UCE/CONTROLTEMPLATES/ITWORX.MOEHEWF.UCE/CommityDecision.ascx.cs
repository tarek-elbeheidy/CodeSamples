using ITWORX.MOEHE.Utilities;
using ITWORX.MOEHEWF.UCE.Utilities;
using Microsoft.SharePoint;
using System;
using System.Linq;
using System.Collections.Generic;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;

namespace ITWORX.MOEHEWF.UCE.CONTROLTEMPLATES.ITWORX.MOEHEWF.UCE
{
    public partial class CommityDecision : UserControlBase
    {
        protected global::ITWORX.MOEHEWF.Common.CONTROLTEMPLATES.ITWORX.MOEHEWF.Common.FileUpload fileUploadCertificates;

        protected void Page_Load(object sender, EventArgs e)
        {

        }



        public void LoadData(int requestId, string siteURL)
        {
            SPSecurity.RunWithElevatedPrivileges(() =>
            {
                using (SPSite site = new SPSite(siteURL))
                {
                    SPWeb web = site.OpenWeb();

                    // Commity Decision lookup
                    SPList CommityDecOptionsList = web.Lists.TryGetList(Utilities.Constants.CommityDecOptions);
                    if (CommityDecOptionsList != null)
                    {
                        var options = CommityDecOptionsList.GetItems().Cast<SPListItem>();
                        if (options != null && options.Count() > 0)
                        {
                            var dataSource = (from option in options
                                              select new
                                              {
                                                  ID = option["ID"],
                                                  TitleAr = option["TitleAr"],
                                                  Title = option["Title"]
                                              }).ToList();

                            HelperMethods.BindDropDownList(ref drp_RejectionReason, dataSource, "ID", "TitleAr", "Title", LCID);
                            drp_RejectionReason.Items.Insert(0, new ListItem(HelperMethods.LocalizedText("ITWORX_MOEHEWF_UCE", "ChooseValue", (uint)LCID), "-1"));

                        }
                    }

                    //CommityDecision

                    SPList CommityDeclist = web.Lists.TryGetList(Utilities.Constants.CommityDecision);
                    SPQuery query = new SPQuery();
                    query.Query = @"<Where>
      <Eq>
         <FieldRef Name='RequestID' />
         <Value Type='Lookup'>" + requestId + @"</Value>
      </Eq>
   </Where>";
                    var dec = CommityDeclist.GetItems(query);
                    if (dec != null && dec.Count > 0)
                    {
                        if (drp_RejectionReason.SelectedIndex >= 0)
                            drp_RejectionReason.SelectedValue = new SPFieldLookupValue(dec[0]["CommityDecision"].ToString()).LookupValue;
                        var bookDate = dec[0]["CommityDate"].ToString();
                        DateTime parsedDate = DateTime.Parse(bookDate);
                        txt_CommityDate.Text = parsedDate.ToShortDateString();
                    }


                }



                fileUploadCertificates.DocumentLibraryName = Utilities.Constants.CertificatesAttachments;
                fileUploadCertificates.DocLibWebUrl = SPContext.Current.Site.Url;
                fileUploadCertificates.LabelDisplayName = HelperMethods.LocalizedText("ITWORX_MOEHEWF_UCE", "CertificateToBeEquivalent", (uint)LCID);
                fileUploadCertificates.MaxSize = 7168000;
                fileUploadCertificates.Group = "CommityDecAttachments";
                fileUploadCertificates.RequestID = requestId;
                fileUploadCertificates.SupportedExtensions = "PNG,PDF,JPG";
                fileUploadCertificates.IsRequired = true;
                fileUploadCertificates.RequiredValidationMessage = HelperMethods.LocalizedText("ITWORX_MOEHEWF_UCE", "RequiredCertificateToBeEquivalent", (uint)LCID);
                fileUploadCertificates.ValidationGroup = "Submit";
                fileUploadCertificates.DeleteImageUrl = SPContext.Current.Site.Url + "/_catalogs/masterpage/MOEHE/common/img/DELETE.png";
                fileUploadCertificates.Enabled = true;
                fileUploadCertificates.Bind();
            });
        }
        public void Savedata(int requestId, string siteURL)
        {
            if(drp_RejectionReason.SelectedIndex>0)
            SPSecurity.RunWithElevatedPrivileges(() =>
            {
                using (SPSite site = new SPSite(siteURL))
                {
                    SPWeb web = site.OpenWeb();

                    // Commity Decision lookup
                    SPList CommityDecOptionsList = web.Lists.TryGetList(Utilities.Constants.CommityDecOptions);


                    //CommityDecision

                    SPList CommityDeclist = web.Lists.TryGetList(Utilities.Constants.CommityDecision);
                    SPQuery query = new SPQuery();
                    query.Query = @"<Where>
      <Eq>
         <FieldRef Name='RequestID' />
         <Value Type='Lookup'>" + requestId + @"</Value>
      </Eq>
   </Where>";
                    var dec = CommityDeclist.GetItems(query);
                    if (dec != null && dec.Count > 0)
                    {
                        if (drp_RejectionReason.SelectedIndex >= 0)
                            dec[0]["CommityDecision"] = new SPFieldLookupValue(int.Parse(drp_RejectionReason.SelectedValue), drp_RejectionReason.SelectedValue); ;
                        dec[0]["CommityDate"] = DateTime.Parse(txt_CommityDate.Text);
                        dec[0].Update();
                    }
                    else
                    {
                        var item = CommityDeclist.AddItem();
                        item["CommityDate"] = DateTime.Parse(txt_CommityDate.Text);
                        item["CommityDecision"] = new SPFieldLookupValue(int.Parse(drp_RejectionReason.SelectedValue), drp_RejectionReason.SelectedValue);
                        item["RequestID"] = new SPFieldLookupValue(requestId, requestId.ToString());
                        item.Update();
                    }

                    fileUploadCertificates.SaveAttachments();

                }
            });
        }
        protected void custValidateFileUploadCertificates_ServerValidate(object source, ServerValidateEventArgs args)
        {

            if (fileUploadCertificates.AttachmentsCount > 0)
            {
                args.IsValid = true;
            }
            else
            {
                args.IsValid = false;
            }
        }

    }
}
