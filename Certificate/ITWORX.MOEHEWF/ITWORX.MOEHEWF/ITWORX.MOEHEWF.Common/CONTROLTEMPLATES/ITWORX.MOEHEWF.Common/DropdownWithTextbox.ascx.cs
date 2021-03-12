using ITWORX.MOEHE.Utilities;
using ITWORX.MOEHE.Utilities.Logging;
using ITWORX.MOEHEWF.Common.Utilities;
using Microsoft.SharePoint;
using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;

namespace ITWORX.MOEHEWF.Common.CONTROLTEMPLATES.ITWORX.MOEHEWF.Common
{
    public partial class DropdownWithTextbox : UserControlBase
    {
        public SPListItemCollection SpCollection { set; get; }

        public bool IsRequired { set; get; }
        public string NewItemText { set; get; }
        public string RequiredDropText { set; get; }
        public string RequiredTextboxText { set; get; }
        public string LblText { set; get; }

        public string ValidationGroup { set; get; }

        

        public string NewOptionText
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
        public DropDownList DropWithNewOption
        {
            get { return dropWithNewOption; }
        }
        public bool Enabled { set; get; }
        public object DropListItems
        {
            get;
            set;
        }

        public string DataValueField
        {
            get;
            set;
        }


        public string DataTextEnField
        {
            get;
            set;
        }

        public string DataTextArField
        {
            get;
            set;
        }
        //public bool DropEnabled { get; set; }
        //public bool TxtEnabled { get; set; }
        public RequiredFieldValidator ReqNewOptionText { get => reqNewOptionText; }
        public RequiredFieldValidator ReqDropWithNewOption { get => reqDropWithNewOption; }
        public TextBox OtherTextBox { get => txtNewOption; }
        public bool ReqErrorAstrik { set; get; }
        protected void Page_Load(object sender, EventArgs e)
        {
            Logging.GetInstance().Debug("Entering method DropdownWithTextbox.Page_Load");
            try
            {

                //if (DropEnabled)
                //{
                //    reqDropWithNewOption.Enabled = true;
                //}
                //else
                //{
                //    reqDropWithNewOption.Enabled = false;
                //}
                //if (TxtEnabled)
                //{
                //    reqNewOptionText.Enabled = true;
                //}
                //else
                //{
                //    reqNewOptionText.Enabled = false;
                //}
                if (!Page.IsPostBack)
                {

                    //hdnTextNewItem.Value = newItemText;
                    lblDropdown.Text = LblText;
                    reqDropWithNewOption.ErrorMessage = RequiredDropText;
                    reqNewOptionText.ErrorMessage = RequiredTextboxText;
                    reqDropWithNewOption.ValidationGroup = ValidationGroup;
                    reqNewOptionText.ValidationGroup = ValidationGroup;

                    if (IsRequired == true)
                    {
                        reqDropWithNewOption.Enabled = true;
                        
                    }
                    else
                    {
                        reqDropWithNewOption.Enabled = false;
                        spandrop.Visible = false;
                    }

                    if (ReqErrorAstrik)
                    {
                        spandrop.Visible = true;
                    }
                    else
                    {
                        spandrop.Visible = false;
                    }
                }


            }
            catch (Exception ex)
            {
                Logging.GetInstance().LogException(ex);
            }
            finally
            {
                Logging.GetInstance().Debug("Exiting method DropdownWithTextbox.Page_Load");
            }
        }
        public void BindDataSource()
        {
            Logging.GetInstance().Debug("Entering method DropdownWithTextbox.BindDropDown");
            try
            {
                dropWithNewOption.Items.Insert(0, new ListItem(HelperMethods.LocalizedText("ITWORX_MOEHEWF_UCE", "ChooseValue", (uint)LCID), string.Empty));
                dropWithNewOption.AppendDataBoundItems = true;
                if (DropListItems != null)
                {
                    if (DropListItems.ToString() != "New")
                    {
                        HelperMethods.BindDropDownList(ref dropWithNewOption, DropListItems, DataValueField, DataTextArField, DataTextEnField, LCID);

                    }

                    dropWithNewOption.Items.Add(new ListItem(HelperMethods.LocalizedText("ITWORX_MOEHEWF_UCE", "Other", (uint)LCID), "New"));
                }
                //else
                //{
                //    dropWithNewOption.Items.Clear();
                //}
            }
            catch (Exception ex)
            {
                Logging.GetInstance().LogException(ex);
            }
            finally
            {
                Logging.GetInstance().Debug("Exiting method DropdownWithTextbox.BindDropDown");
            }
        }

        public void SetVisibility(bool visibilty)
        {
            Logging.GetInstance().Debug("Entering method DropdownWithTextbox.SetVisibility");
            try

            {
                if (visibilty == false)
                {
                    dropWithNewOption.ClearSelection();
                    dropWithNewOption.Visible = false;
                    reqDropWithNewOption.Enabled = false;
                    txtNewOption.Visible = false;
                    reqDropWithNewOption.Enabled = false;
                }
                else
                {
                    dropWithNewOption.Visible = true;
                    reqDropWithNewOption.Enabled = true;
                    if (dropWithNewOption.SelectedValue == "New")
                    {
                        txtNewOption.Visible = true;
                        reqDropWithNewOption.Enabled = true;
                    }

                }
            }
            catch (Exception ex)
            {
                Logging.GetInstance().LogException(ex);
            }
            finally
            {
                Logging.GetInstance().Debug("Exiting method DropdownWithTextbox.SetVisibility");
            }


        }
        public void EnableDropdown(bool enabled)
        {
            Logging.GetInstance().Debug("Entering method DropdownWithTextbox.EnableDropdown");
            try
            {
                if (enabled == false)
                {
                    dropWithNewOption.Enabled = false;
                }
                else
                {
                    dropWithNewOption.Enabled = true;
                }
            }
            catch (Exception ex)
            {
                Logging.GetInstance().LogException(ex);
            }
            finally
            {
                Logging.GetInstance().Debug("Exiting method DropdownWithTextbox.EnableDropdown");
            }

        }
        //public void dropWithNewOption_SelectedIndexChanged(object sender, EventArgs e)
        //{
        //    Logging.GetInstance().Debug("Enter DropdownWithTextbox.dropWithNewOption_SelectedIndexChanged");
        //    try
        //    {
        //        dropWithNewOption.SelectedIndexChanged
        //    }
        //    catch (Exception ex)
        //    {
        //        Logging.GetInstance().LogException(ex);
        //    }
        //    finally
        //    {

        //        Logging.GetInstance().Debug("Exit DropdownWithTextbox.dropWithNewOption_SelectedIndexChanged");
        //    }
        //}
    }
}
