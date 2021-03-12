using ITWORX.MOEHE.Utilities;
using ITWORX.MOEHE.Utilities.Logging;
using ITWORX.MOEHEWF.Common.Utilities;
using Microsoft.SharePoint;
using Microsoft.SharePoint.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

namespace ITWORX.MOEHEWF.Common.WebParts.HomeDashboard
{
    public partial class HomeDashboardUserControl : UserControlBase
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Logging.GetInstance().Debug("Enter HomeDashboard.Page_Load");
            try
            {
                if (SPContext.Current.Web.CurrentUser == null)
                {
                    SPUtility.Redirect(SPContext.Current.Web.Url + "/Pages/HomeRegister.aspx", SPRedirectFlags.DoNotEndResponse, HttpContext.Current);

                }
                if (!Page.IsPostBack)
                {
                    if (HelperMethods.InGroup(Utilities.Constants.ApplicanstGroupName))
                    {
                        SPUtility.Redirect(SPContext.Current.Web.Url + "/applicant/Pages/TrackRequest.aspx", SPRedirectFlags.DoNotEndResponse, HttpContext.Current);
                    }
                    else
                        BindRepeater();
                }
            }
            catch (Exception ex)
            {
                Logging.GetInstance().LogException(ex);
            }
            finally
            {
                Logging.GetInstance().Debug("Exit HomeDashboard.Page_Load");
            }
        }

        private void BindRepeater()
        {
            try
            {
                Logging.GetInstance().Debug("Entering method HomeDashboard.BindRepeater");
                BL.ServiceDashboard NL = new BL.ServiceDashboard();

                rptDashBoard.DataSource = NL.GetServiceLinks(LCID);
                rptDashBoard.DataBind();
            }
            catch (Exception ex)
            {
                Logging.GetInstance().LogException(ex);
                //handle exception as the business require.
            }
            finally
            {
                //dispose any objects that require dispose.
                Logging.GetInstance().Debug("Exiting method HomeDashboard.BindRepeater");
            }
        }

        private string GetGroup()
        {
            string groupObject = null;
            using (SPSite site = new SPSite(SPContext.Current.Site.Url))
            {
                using (SPWeb web = site.OpenWeb())
                {
                    SPUser user = web.CurrentUser;
                    SPGroupCollection groups = user.Groups;
                    foreach (SPGroup group in groups)
                    {
                        groupObject = group.Name;
                    }
                }
            }
            return groupObject;
        }

        protected void rptDashBoard_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            Logging.GetInstance().Debug("Entering method HomeDashboard.rptDashBoard_ItemDataBound");

            try
            {
                if (e.Item.ItemType == ListItemType.AlternatingItem || e.Item.ItemType == ListItemType.Item)

                {
                    HiddenField hdn_SPG = e.Item.FindControl("hdnSPGroup") as HiddenField;
                    HtmlGenericControl dvLink = e.Item.FindControl("divLink") as HtmlGenericControl;
                    if (!string.IsNullOrEmpty(hdn_SPG.Value))
                    {
                        var lst = hdn_SPG.Value.Split(',').ToList();
                        var knownGrps = loggedInUserGroups();
                        lst = lst.Where(x => knownGrps.Contains(x)).ToList();
                        foreach (var item in lst)
                        {
                            dvLink.Visible = true;
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
                Logging.GetInstance().Debug("Exiting method HomeDashboard.rptDashBoard_ItemDataBound");
            }
        }

        protected void hylnk_links_Click(object sender, EventArgs e)
        {
            Logging.GetInstance().Debug("Enter HomeDashboard.hylnk_links_Click");
            try
            {
                LinkButton lnkButton = (LinkButton)sender;
                RepeaterItem gvr = (RepeaterItem)lnkButton.NamingContainer;
                HiddenField hdnGroup = (HiddenField)gvr.FindControl("hdnSPGroup");
                HiddenField hdnID = (HiddenField)gvr.FindControl("hdn_ID");
                if (!string.IsNullOrEmpty(hdnGroup.Value))
                {
                    //Certificate Equivalence Service
                    if (Convert.ToInt32(hdnID.Value) == 1)
                    {
                        var lst = hdnGroup.Value.Split(',').ToList();
                        var knownGrps = loggedInUserGroups();
                        lst = lst.Where(x => knownGrps.Contains(x)).ToList();

                        foreach (var item in lst)
                        {
                            switch (item)
                            {
                                case "Arabian and Asian Program Employees":
                                    SPUtility.Redirect(SPContext.Current.Web.Url + "/reviewer/Pages/ArabicAsianProgramEmployeeDashboard.aspx", SPRedirectFlags.DoNotEndResponse, HttpContext.Current);
                                    break;

                                case "Head Managers":
                                    SPUtility.Redirect(SPContext.Current.Web.Url + "/reviewer/Pages/HeadManagerDashboard.aspx", SPRedirectFlags.DoNotEndResponse, HttpContext.Current);
                                    break;

                                case "Program Managers":
                                    SPUtility.Redirect(SPContext.Current.Web.Url + "/reviewer/Pages/ProgramManagerDashboard.aspx", SPRedirectFlags.DoNotEndResponse, HttpContext.Current);
                                    break;

                                case "Higher Educational Institutions":
                                    SPUtility.Redirect(SPContext.Current.Web.Url + "/reviewer/Pages/HigherEducationalInstitutionsDashboard.aspx", SPRedirectFlags.DoNotEndResponse, HttpContext.Current);
                                    break;

                                case "Cultural Mission Britain":
                                    SPUtility.Redirect(SPContext.Current.Web.Url + "/reviewer/Pages/CulturalMissionEmployeeDashboard.aspx", SPRedirectFlags.DoNotEndResponse, HttpContext.Current);
                                    break;

                                case "Cultural Mission Australia":
                                    SPUtility.Redirect(SPContext.Current.Web.Url + "/reviewer/Pages/CulturalMissionEmployeeDashboard.aspx", SPRedirectFlags.DoNotEndResponse, HttpContext.Current);
                                    break;

                                case "Cultural Mission France":
                                    SPUtility.Redirect(SPContext.Current.Web.Url + "/reviewer/Pages/CulturalMissionEmployeeDashboard.aspx", SPRedirectFlags.DoNotEndResponse, HttpContext.Current);
                                    break;

                                case "Cultural Mission USA":
                                    SPUtility.Redirect(SPContext.Current.Web.Url + "/reviewer/Pages/CulturalMissionEmployeeDashboard.aspx", SPRedirectFlags.DoNotEndResponse, HttpContext.Current);
                                    break;

                                case "Cultural Mission Canada":
                                    SPUtility.Redirect(SPContext.Current.Web.Url + "/reviewer/Pages/CulturalMissionEmployeeDashboard.aspx", SPRedirectFlags.DoNotEndResponse, HttpContext.Current);
                                    break;

                                case "Cultural Mission Jordan":
                                    SPUtility.Redirect(SPContext.Current.Web.Url + "/reviewer/Pages/CulturalMissionEmployeeDashboard.aspx", SPRedirectFlags.DoNotEndResponse, HttpContext.Current);
                                    break;
                               
                                case "Financial Management":
                                    SPUtility.Redirect(SPContext.Current.Web.Url + "/reviewer/Pages/PaymentRequests.aspx", SPRedirectFlags.DoNotEndResponse, HttpContext.Current);
                                    break;
                            }
                        }
                    }
                    //Prior Approval Service
                    else if (Convert.ToInt32(hdnID.Value) == 2)
                    {
                        var lst = hdnGroup.Value.Split(',').ToList();
                        var knownGrps = loggedInUserGroups();
                        lst = lst.Where(x => knownGrps.Contains(x)).ToList();
                        foreach (var item in lst)
                        {
                            switch (item)
                            {
                                case "Arabian and Asian Program Employees":
                                    SPUtility.Redirect(SPContext.Current.Web.Url + "/reviewer/Pages/PAArabicAsianProgramEmployeeDashboard.aspx", SPRedirectFlags.DoNotEndResponse, HttpContext.Current);
                                    break;

                                case "Head Managers":
                                    SPUtility.Redirect(SPContext.Current.Web.Url + "/reviewer/Pages/PAHeadManagerDashboard.aspx", SPRedirectFlags.DoNotEndResponse, HttpContext.Current);
                                    break;

                                case "Program Managers":
                                    SPUtility.Redirect(SPContext.Current.Web.Url + "/reviewer/Pages/PAProgramManagerDashboard.aspx", SPRedirectFlags.DoNotEndResponse, HttpContext.Current);
                                    break;

                                case "Higher Educational Institutions":
                                    SPUtility.Redirect(SPContext.Current.Web.Url + "/reviewer/Pages/PAHigherEducationalInstitutionsDashboard.aspx", SPRedirectFlags.DoNotEndResponse, HttpContext.Current);
                                    break;

                                case "Cultural Mission Britain":
                                    SPUtility.Redirect(SPContext.Current.Web.Url + "/reviewer/Pages/PACulturalMissionEmployeeDashboard.aspx", SPRedirectFlags.DoNotEndResponse, HttpContext.Current);
                                    break;

                                case "Cultural Mission Australia":
                                    SPUtility.Redirect(SPContext.Current.Web.Url + "/reviewer/Pages/PACulturalMissionEmployeeDashboard.aspx", SPRedirectFlags.DoNotEndResponse, HttpContext.Current);
                                    break;

                                case "Cultural Mission France":
                                    SPUtility.Redirect(SPContext.Current.Web.Url + "/reviewer/Pages/PACulturalMissionEmployeeDashboard.aspx", SPRedirectFlags.DoNotEndResponse, HttpContext.Current);
                                    break;

                                case "Cultural Mission USA":
                                    SPUtility.Redirect(SPContext.Current.Web.Url + "/reviewer/Pages/PACulturalMissionEmployeeDashboard.aspx", SPRedirectFlags.DoNotEndResponse, HttpContext.Current);
                                    break;

                                case "Cultural Mission Canada":
                                    SPUtility.Redirect(SPContext.Current.Web.Url + "/reviewer/Pages/PACulturalMissionEmployeeDashboard.aspx", SPRedirectFlags.DoNotEndResponse, HttpContext.Current);
                                    break;

                                case "Cultural Mission Jordan":
                                    SPUtility.Redirect(SPContext.Current.Web.Url + "/reviewer/Pages/PACulturalMissionEmployeeDashboard.aspx", SPRedirectFlags.DoNotEndResponse, HttpContext.Current);
                                    break;
                            }
                        }
                    }
                    //Schooling Service
                    else 
                    {
                        var lst = hdnGroup.Value.Split(',').ToList();
                        var knownGrps = loggedInUserGroups();
                        lst = lst.Where(x => knownGrps.Contains(x)).ToList();
                        foreach (var item in lst)
                        {
                            switch (item)
                            {
                                case "SCE Equivalence Employees":
                                    SPUtility.Redirect(SPContext.Current.Web.Url + "/reviewer/Pages/SCEEquivalenceEmployeesDashboard.aspx", SPRedirectFlags.DoNotEndResponse, HttpContext.Current);
                                    break;

                                case "SCE Department Managers":
                                    SPUtility.Redirect(SPContext.Current.Web.Url + "/reviewer/Pages/SCEDepartmentManagerDashboard.aspx", SPRedirectFlags.DoNotEndResponse, HttpContext.Current);
                                    break;

                                case "SCE Section Managers":
                                    SPUtility.Redirect(SPContext.Current.Web.Url + "/reviewer/Pages/SCESectionManagerDashboard.aspx", SPRedirectFlags.DoNotEndResponse, HttpContext.Current);
                                    break;

                              
                                case "SCE Higher Educational Institutions":
                                    SPUtility.Redirect(SPContext.Current.Web.Url + "/reviewer/Pages/SCEHigherEducationalInstitutionsDashboard.aspx", SPRedirectFlags.DoNotEndResponse, HttpContext.Current);
                                    break;

                                case "SCE Cultural Mission Australia":
                                    SPUtility.Redirect(SPContext.Current.Web.Url + "/reviewer/Pages/SCECulturalMissionDashboard.aspx", SPRedirectFlags.DoNotEndResponse, HttpContext.Current);
                                    break;

                                
                                case "SCE Cultural Mission Britain":
                                    SPUtility.Redirect(SPContext.Current.Web.Url + "/reviewer/Pages/SCECulturalMissionDashboard.aspx", SPRedirectFlags.DoNotEndResponse, HttpContext.Current);
                                    break;

                                case "SCE Cultural Mission Canada":
                                    SPUtility.Redirect(SPContext.Current.Web.Url + "/reviewer/Pages/SCECulturalMissionDashboard.aspx", SPRedirectFlags.DoNotEndResponse, HttpContext.Current);
                                    break;

                                case "SCE Cultural Mission France":
                                    SPUtility.Redirect(SPContext.Current.Web.Url + "/reviewer/Pages/SCECulturalMissionDashboard.aspx", SPRedirectFlags.DoNotEndResponse, HttpContext.Current);
                                    break;

                                case "SCE Cultural Mission Jordan":
                                    SPUtility.Redirect(SPContext.Current.Web.Url + "/reviewer/Pages/SCECulturalMissionDashboard.aspx", SPRedirectFlags.DoNotEndResponse, HttpContext.Current);
                                    break;

                                case "SCE Cultural Mission USA":
                                    SPUtility.Redirect(SPContext.Current.Web.Url + "/reviewer/Pages/SCECulturalMissionDashboard.aspx", SPRedirectFlags.DoNotEndResponse, HttpContext.Current);
                                    break;
                                case "SCE Cultural Mission Egypt":
                                    SPUtility.Redirect(SPContext.Current.Web.Url + "/reviewer/Pages/SCECulturalMissionDashboard.aspx", SPRedirectFlags.DoNotEndResponse, HttpContext.Current);
                                    break;

                                


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
                Logging.GetInstance().Debug("Exit HomeDashboard.hylnk_links_Click");
            }
        }

        private List<string> loggedInUserGroups()
        {
            List<string> result = new List<string>();
            SPGroupCollection _SPGroupCollection = SPContext.Current.Web.CurrentUser.Groups;
            foreach (SPGroup group in _SPGroupCollection)
            {
                result.Add(group.Name);
            }
            return result;
            //bool groupExist = false;

            ////return _SPGroupCollection.GetByName(GroupName) != null;
            //foreach (SPGroup group in _SPGroupCollection)
            //{
            //    if (GroupName.Equals(group.Name))
            //    {
            //        groupExist = true;
            //        //return true;
            //    }

            //}
            //return groupExist;
        }
    }
}