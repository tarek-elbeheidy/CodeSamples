using ITWORX.MOEHE.Utilities.Logging;
using ITWORX.MOEHEWF.Common.Entities;
using ITWORX.MOEHEWF.Common.Utilities;
using Microsoft.SharePoint;
using System;
using System.Collections.Generic;
using System.IO;
using System.Web.UI;

namespace ITWORX.MOEHEWF.Common.CONTROLTEMPLATES.ITWORX.MOEHEWF.Common
{
    public partial class TermsAndConditions : UserControl
    {
        
        public bool isVisible { set; get; }
     
       
        public bool CheckVisibility
        {
            get
            {
                return chkTermsAndConditions.Visible;
            }
            set
            {
                chkTermsAndConditions.Visible = value;
            }
        }
        //public bool LabelVisibility
        //{
        //    get
        //    {
        //        return lblTermsAndConditionsText.Visible;
        //    }
        //    set
        //    {
        //        lblTermsAndConditionsText.Visible = value;
        //    }
        //}

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (isVisible == false)
                {
                    btnAgree.Visible = false;
                }
                else
                {
                    btnAgree.Visible = true;
                }
                HETemplates templates = BL.HETemplates.GetAttachmentByType(TemplateType.TermsAndConditions.ToString());
                if (templates != null)
                {
                    hypTerms.Text =Path.GetFileNameWithoutExtension( templates.FileName);
                    hypTerms.NavigateUrl = templates.FileUrl;

                }
            }
          //  bind();
        }

        private void bind()
        {
            SPSecurity.RunWithElevatedPrivileges(delegate ()
            {
            using (SPSite site = new SPSite(SPContext.Current.Site.Url))
            {
                using (SPWeb web = site.OpenWeb())
                {
                    SPList list = web.Lists["Documents"];
                    SPFolder folder = list.RootFolder;
                    foreach (SPFolder SubRootFolder in folder.SubFolders)
                    {
                        if (SubRootFolder.Name == "Terms and Conditions")
                        {
                            if (SubRootFolder.Files.Count > 0)
                            {
                                List<fileitems> fItems = new List<fileitems>();
                                SPFileCollection fileitems = SubRootFolder.Files;

                                foreach (SPFile file in fileitems)
                                {
                                    fItems.Add(new fileitems{
                                            Name= Path.GetFileNameWithoutExtension(file.Name),
                                            fileUrl= SPContext.Current.Site.Url +"/"+ file.Url
                                    }); 
                                        
                                    }
                                    
                                    if (fItems.Count > 0)
                                    {
                                       
                                        //rpt_Links.DataSource = fItems;
                                        //rpt_Links.DataBind();
                                    }
                                }
                            }
                        }
                    }
                }
            });
        }
     
     

      public  class fileitems
        {
            public string Name { get; set; }

            public string fileUrl { get; set; }
        }

        //protected void custTerms_ServerValidate(object source, System.Web.UI.WebControls.ServerValidateEventArgs args)
        //{
        //    if (chkTermsAndConditions.Checked)
        //    {
        //        args.IsValid = true;
        //    }
        //    else
        //    {
        //        args.IsValid = false;
        //    }
        //}
    }
}