using Microsoft.SharePoint;
using MOEHE.PSPES.Models;
using MOEHE.PSPES.Repository;

using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;

namespace MOEHE.PSPES.Webparts.EnrollmentControlling
{
    public partial class EnrollmentControllingUserControl : UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                string NextYear  = string.Format("{0}", DateTime.Now.Year + 1);
                List<MOE_SCHOOL_Model> schools = MOE_SCHOOL_Repository.GetSchools(NextYear, "false").Result;

                if (schools.Count > 0)
                {
                    if ((uint)CultureInfo.CurrentUICulture.LCID == 1025)
                    {

                        BindingUtility.LoadSchools(schools, SchoolCodesDropDownList, PSPESConstants.ArabicLanguage);

                    }
                    else
                    {
                        BindingUtility.LoadSchools(schools, SchoolCodesDropDownList, PSPESConstants.EnglishLanguage);

                    }
                }

            }
        }

        protected void SchoolCodesDropDownList_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (SchoolCodesDropDownList.SelectedValue != "All")
            {


                pnlData.Visible = true;
                
                EnrollmentControllingModel enrollment_controlling_model = EnorllmentControllingRepository.GetEnrollmentControllingData(SchoolCodesDropDownList.SelectedValue).Result;
                #region Bind Dates
                lblItemID.Text = enrollment_controlling_model.ID.ToString();
                string enrollmentFrom;
                string enrollmentTo;
                string NextYear = string.Format("{0}", DateTime.Now.Year + 1);
                enrollmentFrom = DateTime.ParseExact(((DateTime)enrollment_controlling_model.FromDate).Date.ToString("dd/MM/yyyy", CultureInfo.GetCultureInfo(2057)), "dd/MM/yyyy", CultureInfo.GetCultureInfo(2057)).ToString("dd/MM/yyyy", CultureInfo.GetCultureInfo(2057));
                enrollmentTo = DateTime.ParseExact(((DateTime)enrollment_controlling_model.ToDate).Date.ToString("dd/MM/yyyy", CultureInfo.GetCultureInfo(2057)), "dd/MM/yyyy", CultureInfo.GetCultureInfo(2057)).ToString("dd/MM/yyyy", CultureInfo.GetCultureInfo(2057));
                dtEnrollmentDateFrom.Value = enrollmentFrom;
                dtEnrollmentDateTo.Value = enrollmentTo;
                #endregion
                #region  Bind Grades
                chkLstGrades.Items.Clear();
                List<schoolGrade> schoolGrades = MOE_SCHOOL_Repository.GetSchoolGrade(NextYear, SchoolCodesDropDownList.SelectedValue, "false").Result;
                List<schoolGrade> selectedGrades= new List<schoolGrade>();
                if ((uint)CultureInfo.CurrentUICulture.LCID == 1025)
                {
                    BindingUtility.LoadGrades(schoolGrades, chkLstGrades, PSPESConstants.ArabicLanguage);




                }
                else
                {
                    BindingUtility.LoadGrades(schoolGrades, chkLstGrades, PSPESConstants.EnglishLanguage);



                }

                if (enrollment_controlling_model.GradeCodes != "ALL")
                {
                    selectedGrades = schoolGrades.Where(c => enrollment_controlling_model.GradeCodes.Split(',').Contains(c.Grade)).Select(co => co).ToList();
                    selectedGrades.ForEach(item => chkLstGrades.Items.FindByValue(item.Grade).Selected=true);
                    rblGrades.SelectedValue = "select";
                    rblGradesSelectionChange(rblGrades.SelectedValue);
                }
                else
                {
                    rblGrades.SelectedValue = "ALL";
                    rblGradesSelectionChange(rblGrades.SelectedValue);
                }

                #endregion
                #region Bind Countries
                //Load Conuntries
                lstNationalities.Items.Clear();
                lstSelectedNationalities.Items.Clear();
                List<MOI_COUNTRY_MAP> Countries = MOI_COUNTRY_MAP_Repository.GetCountries().Result;
                List<MOI_COUNTRY_MAP> selectedCountries = new List<MOI_COUNTRY_MAP>();

                if (enrollment_controlling_model.CountryCodes != "ALL")
                {
                    selectedCountries = Countries.Where(c => enrollment_controlling_model.CountryCodes.Split(',').Contains(c.MOI_COUNTRY_CODE)).Select(co => co).ToList();
                    selectedCountries.ForEach(item => Countries.Remove(item));
                    rblNationalities.SelectedValue = "select";
                    rblNationalitiesSelectionChange(rblNationalities.SelectedValue);
                }
                else
                {
                    rblNationalities.SelectedValue = "ALL";
                    rblNationalitiesSelectionChange(rblNationalities.SelectedValue);
                }


                if ((uint)CultureInfo.CurrentUICulture.LCID == 1025)
                {
                    BindingUtility.LoadNationalities(Countries, lstNationalities, PSPESConstants.ArabicLanguage);
                    if (selectedCountries.Count>0)
                    {
                        BindingUtility.LoadNationalities(selectedCountries, lstSelectedNationalities, PSPESConstants.ArabicLanguage);
                    }




                }
                else
                {
                    BindingUtility.LoadNationalities(Countries, lstNationalities, PSPESConstants.EnglishLanguage);
                    if (selectedCountries.Count > 0)
                    {
                        BindingUtility.LoadNationalities(selectedCountries, lstSelectedNationalities, PSPESConstants.EnglishLanguage);
                    }



                }
                #endregion

            }
            else
            {
                pnlData.Visible = false;
            }


        }

        protected void rblNationalities_SelectedIndexChanged(object sender, EventArgs e)
        {
            rblNationalitiesSelectionChange(rblNationalities.SelectedValue);
        }

        private void rblNationalitiesSelectionChange(string selectedValue)
        {
            if (selectedValue == "select")
            {
                //lstNationalities.Enabled = true;
                //lstSelectedNationalities.Enabled = true;
                //lnkSelectNationalities.Enabled = true;
                //lnkDeSelectNationalities.Enabled = true;
                dvNationalities.Visible = true;
            }
            else
            {
                //lstNationalities.Enabled = false;
                //lstSelectedNationalities.Enabled = false;
                //lnkSelectNationalities.Enabled = false;
                //lnkDeSelectNationalities.Enabled = false;
                dvNationalities.Visible = false;

            }
        }

        protected void rblGrades_SelectedIndexChanged(object sender, EventArgs e)
        {
            rblGradesSelectionChange(rblGrades.SelectedValue);
        }

        private void rblGradesSelectionChange(string selectedValue)
        {
            if (selectedValue == "select")
            {
                
                dvGrades.Visible = true;
            }
            else
            {
               

                dvGrades.Visible = false;

            }
        }

        protected void lnkSelectNationalities_Click(object sender, EventArgs e)
        {
            List<ListItem>  selectedNationalities = lstNationalities.Items.Cast<ListItem>()
                                                                          .Where(i => i.Selected)
                                                                           .Select(i => i).ToList();
            selectedNationalities.ForEach(item => lstNationalities.Items.Remove(item));
            selectedNationalities.ForEach(item => lstSelectedNationalities.Items.Add(item));
        }

        protected void lnkDeSelectNationalities_Click(object sender, EventArgs e)
        {
            List<ListItem> deSelectedNationalities = lstSelectedNationalities.Items.Cast<ListItem>()
                                                                        .Where(i => i.Selected)
                                                                         .Select(i => i).ToList();
            deSelectedNationalities.ForEach(item => lstSelectedNationalities.Items.Remove(item));
            deSelectedNationalities.ForEach(item => lstNationalities.Items.Add(item));
        }

        protected void SaveLinkButton_Click(object sender, EventArgs e)
        {
            try
            {
                EnrollmentControllingModel enrollmentControllingModel = new EnrollmentControllingModel();
                enrollmentControllingModel.FromDate = DateTime.ParseExact(dtEnrollmentDateFrom.Value, "dd/MM/yyyy", CultureInfo.GetCultureInfo(2057));
                enrollmentControllingModel.ToDate = DateTime.ParseExact(dtEnrollmentDateTo.Value, "dd/MM/yyyy", CultureInfo.GetCultureInfo(2057));
                enrollmentControllingModel.ID = int.Parse(lblItemID.Text);
                enrollmentControllingModel.SchoolCode = SchoolCodesDropDownList.SelectedValue;

                #region collect the selected countries
                string selectedCountryCodes = "";
                if (rblNationalities.SelectedValue == "select")
                {


                    List<ListItem> selectedNationalities = lstSelectedNationalities.Items.Cast<ListItem>()
                                                                                 .Select(i => i).ToList();


                    foreach (ListItem item in selectedNationalities)
                    {
                        selectedCountryCodes += item.Value + ",";
                    }

                    if (selectedCountryCodes.Length > 0)
                    {
                        selectedCountryCodes = selectedCountryCodes.Substring(0, selectedCountryCodes.Length - 1);
                    }
                }

                else
                {
                    selectedCountryCodes = "ALL";
                }
                enrollmentControllingModel.CountryCodes = selectedCountryCodes;
                #endregion

                #region collect selected grades
                string selectedGrades = "";

                if (rblGrades.SelectedValue == "select")
                {
                    List<ListItem> selectedGradesList = chkLstGrades.Items.Cast<ListItem>()
                                                                             .Where(i => i.Selected)
                                                                              .Select(i => i).ToList();
                    foreach (ListItem item in selectedGradesList)
                    {
                        selectedGrades += item.Value + ",";
                    }

                    if (selectedGrades.Length > 0)
                    {
                        selectedGrades = selectedGrades.Substring(0, selectedGrades.Length - 1);
                    }

                }
                else
                {
                    selectedGrades = "ALL";
                }
                enrollmentControllingModel.GradeCodes = selectedGrades;
                #endregion

                DBOperationResult result = EnorllmentControllingRepository.UpdateEnrollmentControllingData(enrollmentControllingModel).Result;
                if (result.EnglishResult == PSPESConstants.InsertionDone)
                {
                    if ((uint)CultureInfo.CurrentUICulture.LCID == 1033)
                    {
                        CustomMessageBoxHelper.Show(this.Page, "Data Saved Successfully");
                       

                    }
                    else
                    {
                        // CustomMessageBoxHelper.Show(this.Page, "من فضلك ت");
                        CustomMessageBoxHelper.Show(this.Page, "تم الحفظ بنجاح");

                    }
                }
            }
            catch(Exception ex)
            {
                DBOperationResult ReturnedResult2 = TransactionLogsRepository.Insert(new TransactionsLog { CreatedDate = DateTime.Now, ShortDescription = "Enrollment Controlling Control", Description = "Enrollment Controlling Exception, ITEMID: " + lblItemID.Text + ", Exception:" + ex.Message, UserID = SPContext.Current.Site.RootWeb.CurrentUser.LoginName }).Result;
                if ((uint)CultureInfo.CurrentUICulture.LCID == 1033)
                {
                    CustomMessageBoxHelper.Show(this.Page, PSPESConstants.EnglishSaveError);

                }
                else
                {
                    // CustomMessageBoxHelper.Show(this.Page, "من فضلك ت");
                    CustomMessageBoxHelper.Show(this.Page, PSPESConstants.ArabicSaveError);

                }
            }


        }
    }
}
