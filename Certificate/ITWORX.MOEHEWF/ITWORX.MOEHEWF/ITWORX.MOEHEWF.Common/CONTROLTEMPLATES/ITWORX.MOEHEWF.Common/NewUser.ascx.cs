using ITWORX.MOEHE.Utilities;
using ITWORX.MOEHE.Utilities.Logging;
using ITWORX.MOEHEWF.Common.Entities;
using ITWORX.MOEHEWF.Common.Utilities;
using System;
using System.Collections.Generic;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;

namespace ITWORX.MOEHEWF.Common.CONTROLTEMPLATES.ITWORX.MOEHEWF.Common
{
    public partial class NewUser : UserControlBase
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                PopulateDropdowns();
            }
        }
        private void PopulateDropdowns()
        {
            Logging.GetInstance().Debug("Enter NewUser.PopulateDropdowns");
            try
            {
                List<Nationality> nationalityItems = BL.Nationality.GetAll();
                List<NationalityCategory> nationalityCategoryItems = BL.NationalityCategory.GetAll();
                if (nationalityItems != null || nationalityItems.Count > 0)
                {

                    dropNationailty.Items.Insert(0, new ListItem(HelperMethods.LocalizedText("ITWORX.MOEHEWF.Common", "ChooseValue", (uint)LCID), string.Empty));
                    dropNationailty.AppendDataBoundItems = true;
                    HelperMethods.BindDropDownList(ref dropNationailty, nationalityItems, "ID", "ArabicTitle", "EnglishTitle", LCID);

                }
                if (nationalityCategoryItems != null || nationalityCategoryItems.Count > 0)
                {

                    dropNationalityCategory.Items.Insert(0, new ListItem(HelperMethods.LocalizedText("ITWORX.MOEHEWF.Common", "ChooseValue", (uint)LCID), string.Empty));
                    dropNationalityCategory.AppendDataBoundItems = true;
                    HelperMethods.BindDropDownList(ref dropNationalityCategory, nationalityCategoryItems, "ID", "ArabicTitle", "EnglishTitle", LCID);

                }

            }
            catch (Exception ex)
            {
                Logging.GetInstance().LogException(ex);
            }
            finally
            {

                Logging.GetInstance().Debug("Exit NewUser.PopulateDropdowns");
            }
        }
    }
}