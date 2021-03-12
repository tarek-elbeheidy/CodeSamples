using Microsoft.SharePoint;
using System;
using System.Data;
using Microsoft.SharePoint.Publishing;
using System.Web;
using System.Web.UI;
using ITWORX.MOEHEWF.Common.Utilities;
using System.Collections;
using ITWORX.MOEHE.Utilities.Logging;

namespace ITWORX.MOEHEWF.Common.CONTROLTEMPLATES.ITWORX.MOEHEWF.Common
{
    public partial class LanguageSwitcher : UserControlBase
    {
        #region Private Variables
        SPSite site;
        SPWeb Web;
        SPList VariationsList;
        DataTable AllLabels;
        string CurrentLabel;
        ArrayList otherLabel;
        #endregion

        #region Event Hndlers
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                Logging.GetInstance().Debug("Entering method LanguageSwitcher.Page_Load");
                SPSecurity.RunWithElevatedPrivileges(() =>
                    {
                        FillVariationLabelsTable();
                        CurrentLabel = GetCurrentVariationLabel();
                        otherLabel = GetVariationLabel();
                        if (!Page.IsPostBack)
                        {
                            if (otherLabel != null && otherLabel.Count > 1)
                            {
                                lnkbtn_Language.Text = otherLabel[0].ToString();
                                hdnVarLabel.Value = otherLabel[1].ToString();
                            }
                        }
                    });
            }
            catch (Exception ex)
            {
                Logging.GetInstance().LogException(ex);
            }
            finally
            {
                Logging.GetInstance().Debug("Exiting Method LanguageSwitcher.Page_Load");
            }

        }

        protected void lnkbtn_Language_OnClick(object sender, EventArgs e)
        {
            try
            {
                Logging.GetInstance().Debug("Entering method LanguageSwitcher.lnkbtn_Language_OnClick");
                string VarLabel = hdnVarLabel.Value;//lnkbtn_Language.CommandArgument;
                string CurrentRequestUrl = HttpContext.Current.Request.Url.ToString();
                string CurrentWebUrl = string.Format("{0}/", SPContext.Current.Web.Url);
                string TargetUrl = CurrentWebUrl;

                if (PublishingWeb.IsPublishingWeb(Web))
                {
                    if (CurrentWebUrl.ToLower().Contains(string.Format("/{0}/", CurrentLabel.ToLower())))
                    {
                        if (string.IsNullOrEmpty(CurrentLabel))
                        {
                        }
                        else
                        {
                            if (Request.Url.ToString().ToLower().Contains("dispform.aspx"))
                            {
                                TargetUrl = Request.Url.ToString().ToLower().Replace(string.Format("/{0}/", CurrentLabel).ToLower(), string.Format("/{0}/", VarLabel).ToLower());
                                string ItemId = TargetUrl.Substring(TargetUrl.LastIndexOf('=') + 1);
                                int num1 = TargetUrl.IndexOf("lists/") + 6;
                                string ListName = TargetUrl.Substring(num1);
                                string[] str = ListName.Split('/');
                                ListName = str[0];
                                using (site = new SPSite((SPContext.Current.Web.Url).ToString() + "/" + CurrentLabel + "/"))
                                using (Web = site.OpenWeb())
                                {
                                    SPList CurrList = Web.Lists[ListName];
                                    SPQuery CurrQuery = Utilities.BusinessHelper.GetQueryObject("<Where><Eq><FieldRef Name='ID'/><Value Type='Counter'>" + ItemId +
                                        "</Value></Eq></Where></Query><ViewFields><FieldRef Name='GUID_Link'/></ViewFields>");

                                    SPListItemCollection Curritems = CurrList.GetItems(CurrQuery);
                                    SPListItem item = Curritems[0];
                                    Guid CurrItemGuid = new Guid(item["GUID_Link"].ToString());
                                    using (site = new SPSite((SPContext.Current.Web.Url).ToString() + "/" + VarLabel + "/"))
                                    using (Web = site.OpenWeb())
                                    {
                                        SPList VarList = Web.Lists[ListName];
                                        SPQuery VarQuery = Utilities.BusinessHelper.GetQueryObject("<Where><Eq><FieldRef Name='GUID_Link'/><Value Type='Guid'>" + CurrItemGuid + "</Value></Eq></Where></Query><ViewFields><FieldRef Name='ID'/></ViewFields>");

                                        SPListItemCollection Varitems = VarList.GetItems(VarQuery);
                                        string VarItemId = Varitems[0]["ID"].ToString();
                                        TargetUrl = TargetUrl.Replace(ItemId, VarItemId);
                                    }
                                }

                            }
                            else if (Request.Url.ToString().ToLower().Contains(CurrentLabel.ToLower() + "/pages/"))
                            {

                                TargetUrl = Request.Url.ToString().ToLower().Replace(string.Format("/{0}/", CurrentLabel).ToLower(), string.Format("/{0}/", VarLabel).ToLower());
                            }
                            else if (Request.Url.ToString().ToLower().Contains("/_layouts/"))
                            {
                                TargetUrl = CurrentWebUrl.ToLower().Replace(string.Format("/{0}/", CurrentLabel).ToLower(), string.Format("/{0}/", VarLabel).ToLower()) + "_" + Request.Url.ToString().Split('_')[1];
                            }
                            else
                            {
                                TargetUrl = CurrentRequestUrl.ToLower().Replace(string.Format("/{0}/", CurrentLabel).ToLower(), string.Format("/{0}/", VarLabel).ToLower());
                            }
                        }
                    }

                    Response.Redirect(TargetUrl, false);
                }
            }
            catch (Exception ex)
            {
                Logging.GetInstance().LogException(ex);
            }
            finally
            {
                Logging.GetInstance().Debug("Exiting Method LanguageSwitcher.lnkbtn_Language_OnClick");
            }
        }
        #endregion

        #region Private Methods
        private void FillVariationLabelsTable()
        {
            try
            {
                Logging.GetInstance().Debug("Entering method LanguageSwitcher.FillVariationLabelsTable");
                using (site = new SPSite(SPContext.Current.Site.Url))
                {
                    using (Web = site.OpenWeb())
                    {
                        if (PublishingWeb.IsPublishingWeb(Web))
                        {
                            string listIdString = Web.AllProperties["_VarLabelsListId"].ToString();
                            if (!string.IsNullOrEmpty(listIdString))
                            {
                                Guid listId = new Guid(listIdString);
                                VariationsList = Web.Lists[listId];
                                if (VariationsList != null)
                                {
                                    AllLabels = VariationsList.Items.GetDataTable();
                                }

                            }

                        }
                    }

                }
            }
            catch (Exception ex)
            {
                Logging.GetInstance().LogException(ex);
            }
            finally
            {
                Logging.GetInstance().Debug("Exiting Method LanguageSwitcher.FillVariationLabelsTable");
            }
        }

        protected string GetCurrentVariationLabel()
        {
            Logging.GetInstance().Debug("Entering method LanguageSwitcher.GetCurrentVariationLabel");
            string currentVariationLabel = string.Empty;
            try
            {
                string CurrentWeb = string.Format("{0}/", SPContext.Current.Web.Url);
                foreach (DataRow dr in AllLabels.Rows)
                {
                    if (CurrentWeb.ToLower().Contains(string.Format("/{0}/", dr["Title"].ToString().ToLower())))
                        currentVariationLabel = dr["Title"].ToString();
                }
            }
            catch (Exception ex)
            {
                Logging.GetInstance().LogException(ex);
            }
            finally
            {
                Logging.GetInstance().Debug("Exiting Method LanguageSwitcher.GetCurrentVariationLabel");
            }
            return currentVariationLabel;
        }

        protected ArrayList GetVariationLabel()
        {
            Logging.GetInstance().Debug("Entering method LanguageSwitcher.GetVariationLabel");
            ArrayList arrList = new ArrayList();
            try
            {
                string CurrentWeb = string.Format("{0}/", SPContext.Current.Web.Url);
                foreach (DataRow dr in AllLabels.Rows)
                {
                    if (!CurrentWeb.ToLower().Contains(string.Format("/{0}/", dr["Title"].ToString().ToLower())))
                    {
                        arrList.Add(dr.ItemArray[3]);
                        arrList.Add(dr["Title"]);
                    }
                }
            }
            catch (Exception ex)
            {
                Logging.GetInstance().LogException(ex);
            }
            finally
            {
                Logging.GetInstance().Debug("Exiting Method LanguageSwitcher.GetVariationLabel");
            }
            return arrList;
        }
        #endregion
    }
}
