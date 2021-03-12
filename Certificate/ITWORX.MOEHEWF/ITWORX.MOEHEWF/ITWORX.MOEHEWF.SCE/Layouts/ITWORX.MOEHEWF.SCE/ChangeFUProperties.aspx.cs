using System;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;
using ITWORX.MOEHEWF.SCE.WebParts.EditRequest;
using Microsoft.SharePoint;
using Microsoft.SharePoint.WebControls;

namespace ITWORX.MOEHEWF.SCE.Layouts.ITWORX.MOEHEWF.SCE
{
    public partial class ChangeFUProperties : UnsecuredLayoutsPageBase
    {
        protected override bool AllowAnonymousAccess { get { return true; } }
      protected override void OnInit(EventArgs e)
        {

            base.OnInit(e);
            LCID = System.Threading.Thread.CurrentThread.CurrentUICulture.LCID;
        }
     
        protected void Page_Load(object sender, EventArgs e)
        {
        

          
        }
   

       
        [WebMethod]
        
        public static void ChangeProperties(string certificateTypeId,string natCatId,string countryId,string goingClassId
            ,int requestId, string dropClientID, string textBoxClientID ,string reqDropClientID ,string labelRequiredDrop , int language)
        {
           
              string ascxPath = @"~/_CONTROLTEMPLATES/15/ITWORX.MOEHEWF.SCE.WebParts/EditRequest/EditRequestUserControl.ascx";

            using (Page page = new Page())
            {
                
                EditRequestUserControl userControl = (EditRequestUserControl)page.LoadControl(ascxPath);

                page.Controls.Add(userControl);

                Entities.FileUploadProperties uploadProperties = new Entities.FileUploadProperties();
                uploadProperties.certificateTypeId = certificateTypeId;
                uploadProperties.goingClassId = goingClassId;
                uploadProperties.natCatId = natCatId;
                uploadProperties.countryId = countryId;
                uploadProperties.requestId = requestId;
                uploadProperties.dropClientID = dropClientID;
                uploadProperties.textBoxClientID = textBoxClientID;
                uploadProperties.reqDropClientID = reqDropClientID;
                uploadProperties.labelRequiredDrop = labelRequiredDrop;
                uploadProperties.language = language;
                //userControl.BindAttachmentsProperties(uploadProperties);
               
            }

          
           
        }
    }
}
