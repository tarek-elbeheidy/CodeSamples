using ITWORX.MOEHE.Utilities;
using ITWORX.MOEHEWF.Common.Utilities;
using System; 
using System.Web.UI;
using System.Web.UI.WebControls; 

namespace ITWORX.MOEHEWF.Common.CONTROLTEMPLATES.ITWORX.MOEHEWF.Common
{
    public partial class DDLWithTXTWithNoPostback : UserControlBase
    {
        public bool IsRequired
        {
            get
            {
                return string.IsNullOrEmpty(isRequired_HF.Value)?false : Convert.ToBoolean(isRequired_HF.Value);
            }
            set
            {
                customValidator.Enabled = value;
                isRequired_HF.Value = value.ToString();
            }
        }
        public string ValidationMSG
        {
            set
            {
                customValidator.ErrorMessage = value;
            }

        }

        public string ValidationGroup
        {
            set
            {
                customValidator.ValidationGroup = value;
            }
        }

        //===================================================
        public string Title
        {
            set
            {
                lblDropdown.Text = value;
            }
        }

        //==================================================

        public string Client_ID
        {
            get
            {
                return dropWithNewOption.ClientID;
            }
        }

        public string TextBoxClient_ID
        {
            get
            {
                return txtNewOption.ClientID;
            }
        }
        public string LblTxtNewOption_ID
        {
            get
            {
                return lblTxtNewOption.ClientID;
            }
        }
        
        public string SelectedValue
        {
            get
            {
                return dropWithNewOption.SelectedValue;
            }
            set
            {
                dropWithNewOption.SelectedValue = value;
            }
        }

        public string SelectedText
        {
            get
            {
                return dropWithNewOption.SelectedItem.Text;
            }
            set
            {
                dropWithNewOption.SelectedItem.Text = value;
            }
        }


        public string OtherValue
        {
            get
            {
                return txtNewOption.Text;
            }
            set
            {
                txtNewOption.Text = value;
            }
        }
        //===================================================
        public bool IsDepending
        {
            get
            {
                return Convert.ToBoolean(isDepending_HF.Value);
            }
            set
            {
                customValidator.Enabled = value;
                isDepending_HF.Value = value.ToString();
            }
        }
        public string ParentDDL
        {
            set
            {
                parentDDL_HF.Value = value;
            }
        }

        public bool Enabled
        {
            get
            {
                return dropWithNewOption.Enabled;
            }
            set
            {
                dropWithNewOption.Enabled = value;
            }

        }
        public string lkpARTextField { set { lkpARText_HF.Value = value; } }
        public string lkpENTextField { set { lkpENText_HF.Value = value; } }
        public string lkpValField { set { lkpVal_HF.Value = value; } }
        public string lkpList { set { lkpList_HF.Value = value; } }
        //=======================================================
        public string DataENTextField{ get; set; }
        public string DataARTextField { get; set; }
        public string DataValueField { get; set; }
        public object DataSource { get; set;}


        private string otherText;
        public string OtherText
        {
            get
            {
               
                   
                
                return otherText;
            }
            set
            {
                otherText = value;
               
            }
        }

         private string lblOtherTextBoxText;
        public string LblOtherTextBoxText
        {
            get
            {
                return lblOtherTextBoxText;
            }
            set
            {
                lblOtherTextBoxText = value;
                lblTxtNewOption.Text = value;
               
            }
        }

        private bool hideOtherOption;
        public bool HideOtherOption
        {
            get
            {
                return hideOtherOption;
            }
            set
            {
                hideOtherOption = value;
            }
        }

        private bool lblOtherTextBoxVisibility;
        public bool LblOtherTextBoxVisibility
        {
            get
            {
                return lblOtherTextBoxVisibility;
            }
            set
            {
                lblOtherTextBoxVisibility = value;
                //lblTxtNewOption.Visible = value;
                hdnTxtWithLbl.Value =value.ToString();
            }
        }
        protected override void OnInit(EventArgs e)
        {
            if (string.IsNullOrEmpty(OtherText))
            {
                OtherText = HelperMethods.LocalizedText("ITWORX_MOEHEWF_UCE", "Other", (uint)System.Threading.Thread.CurrentThread.CurrentUICulture.LCID);
            }
        }
            public void BingDDL()
        { 
            HelperMethods.BindDropDownList(ref dropWithNewOption, DataSource, DataValueField, DataARTextField, DataENTextField, System.Threading.Thread.CurrentThread.CurrentUICulture.LCID);
            if(!hideOtherOption)
            dropWithNewOption.Items.Add(new ListItem(/*HelperMethods.LocalizedText("ITWORX_MOEHEWF_UCE", "Other", (uint)LCID)*/OtherText, "-2"));
            dropWithNewOption.Items.Insert(0, new ListItem(HelperMethods.LocalizedText("ITWORX_MOEHEWF_UCE", "ChooseValue", (uint)System.Threading.Thread.CurrentThread.CurrentUICulture.LCID), "-1"));   
        }
        //=====================================================
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (IsRequired)
                { 
                    customValidator.ClientValidationFunction = @"if (($('#" + dropWithNewOption.ClientID + "').val() == '-1') || ($('#" + dropWithNewOption.ClientID + "').val() == '-2' && $('#" + txtNewOption.ClientID + @"').val() == ''))
                                                                        {    
                                                                            args.IsValid = false; 
                                                                        } ";
                }
            }
        }

        protected void cusCustom_ServerValidate(object sender, ServerValidateEventArgs e)
        {
            if (IsRequired)
            {
                if ((dropWithNewOption.SelectedValue == "-1") || (dropWithNewOption.SelectedValue == "-2" && string.IsNullOrEmpty(txtNewOption.Text)))
                e.IsValid = false; 
            }
        }
    }
}
