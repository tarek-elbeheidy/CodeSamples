using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Configuration;
using MOEHE.PSPES.Models;
using MOEHE.PSPES.Repository;
using System.Collections.Generic;
using System.Data;
using System.Data.Linq;
using System.Linq;
using Microsoft.SharePoint;
using System.Globalization;

namespace MOEHE.PSPES.Webparts.SeatCapacityAllocate
{
    public partial class SeatCapacityAllocateUserControl : UserControl
    {
        //string nextTerm = (DateTime.Now.Year + 1).ToString();btn_AddGrades

        #region Variables

        //string nextTerm = DateTime.Now.Year.ToString() ;
        //string sclYearID = DateTime.Now.Year.ToString();
        //string sclCode = ConfigurationManager.AppSettings["SchoolCode"];



        #endregion



        protected override void OnInit(EventArgs e)
        {
            if (!IsPostBack)
            {

                bool isMinistryUser = false;
                bool isSchoolUser = false;
                isMinistryUser = SharePointUtilityRepository.IsUserInGroup(PSPESConstants.PrivateSchoolOfficeSharePointGroup, SPContext.Current.Site.RootWeb.CurrentUser.LoginName);
                isSchoolUser = SharePointUtilityRepository.IsUserInGroup(PSPESConstants.PrivateSchoolUsersSharePointGroup, SPContext.Current.Site.RootWeb.CurrentUser.LoginName);




                UserHelper userhelper = ADutility.GetUserDetails(SPContext.Current.Site.RootWeb.CurrentUser);
                string schoolCode = userhelper.DepartmentID;
                ViewState["SclCode"] = schoolCode;
                utype.Items.Clear();
                DdlSclCode.Items.Clear();

                if (schoolCode == "-1")
                {
                    string errorMessage = "";
                    if ((uint)CultureInfo.CurrentUICulture.LCID == 1025)
                    {
                        errorMessage = PSPESConstants.ArabicNoSchoolError;
                    }
                    else
                    {
                        errorMessage = PSPESConstants.EnglishNoSchoolError;
                    }

                    CustomMessageBoxHelper.Show(this.Page, errorMessage);
                }
                else
                {
                    if (isMinistryUser)
                    {
                        if (ViewState["IsMinisteryUser"] != null)
                        {
                            ViewState["IsMinisteryUser"] = "true";
                        }
                        else
                        {
                            ViewState.Add("IsMinisteryUser", "true");
                        }

                        //if ((uint)CultureInfo.CurrentUICulture.LCID == 1025)
                        //{
                        //    utype.SelectedItem.Text = "موظف الوزارة";
                        //}
                        //else
                        //{
                        //    utype.SelectedItem.Text = "Ministry user";
                        //}

                        //utype.Enabled = false;
                        //DdlSclCode.Enabled = true;

                    }

                    else if (isSchoolUser)
                    {


                        if (ViewState["IsMinisteryUser"] != null)
                        {
                            ViewState["IsMinisteryUser"] = "false";
                        }
                        else
                        {
                            ViewState.Add("IsMinisteryUser", "false");
                        }
                        //if ((uint)CultureInfo.CurrentUICulture.LCID == 1025)
                        //{
                        //    utype.SelectedItem.Text = "موظف المدرسة";
                        //}
                        //else
                        //{
                        //    utype.SelectedItem.Text = "School user";
                        //}

                        //utype.Enabled = false;
                        //DdlSclCode.Enabled = true;



                        //try
                        //{





                        //}
                        //catch { }
                    }

                    else
                    {
                        if ((uint)CultureInfo.CurrentUICulture.LCID == 1025)
                        {
                            CustomMessageBoxHelper.Show(this.Page, "You do not have permission to access this page");
                        }
                        else
                        {
                            // CustomMessageBoxHelper.Show(this.Page, "من فضلك ت");
                            CustomMessageBoxHelper.Show(this.Page, "ليس لديك صلاحية الدخول لهذه الصفحه ");

                        }
                    }
                }



            }
        }



        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                CapNumTxt.Enabled = false;
                DDLnewGrades.Enabled = false;
                if (ViewState["IsMinisteryUser"] != null)
                {
                    try
                    {

                        string nextTerm = TermRepository.GetTerms().Result.Where(s => s.ACADEMIC_YEAR_DESC == "PREENROLLMENT").Select(d => d).FirstOrDefault().TermCode; //(DateTime.Now.Year).ToString() + " - " +
                        string sclYearID = TermRepository.GetTerms().Result.Where(s => s.ACADEMIC_YEAR_DESC == "CURRENT").Select(d => d).FirstOrDefault().TermCode;
                        FilluserDDL();
                        FillscCodeDDL();
                        FillnewGradesDDL();
                        Pnl.Visible = true;

                        string isMinistry = ViewState["IsMinisteryUser"].ToString();

                        if (isMinistry == "true")
                        {
                            if ((uint)CultureInfo.CurrentUICulture.LCID == 1025)
                            {
                                utype.SelectedItem.Text = "موظف الوزارة";
                            }
                            else
                            {

                                utype.SelectedItem.Text = "Ministry user";
                            }

                            utype.Enabled = false;
                            DdlSclCode.Enabled = true;

                            DDLnewGrades.Enabled = true;

                            CapNumTxt.Enabled = true;
                            // AddCapBtn.Enabled = true;
                            foreach (GridViewRow r in GridView1.Rows)
                            {

                                DropDownList ddGr = (DropDownList)r.FindControl("DDLGrades");
                                TextBox txtbox = (TextBox)r.FindControl("DistTxt");
                                txtbox.Enabled = false;
                                ddGr.Enabled = true;

                            }
                            dvLastModifiedDate.Visible = true;
                            dvLastModifiedUser.Visible = true;

                            //tarek el beheidy 25.4.2018 hide the seat capacity percentage panel for school user and show it for ministry user as per Mr. Tarek al Abdallah Request
                            fieldsetSeatCapacityPercentage.Visible = true;
                        }
                        else
                        {
                            if ((uint)CultureInfo.CurrentUICulture.LCID == 1025)
                            {
                                utype.SelectedItem.Text = "موظف المدرسة";
                            }
                            else
                            {

                                utype.SelectedItem.Text = "School user";
                            }

                            utype.Enabled = false;
                            DdlSclCode.Enabled = false;
                            DdlSclCode.SelectedItem.Value = ViewState["SclCode"].ToString();
                            PanelSclCodeDDL.Visible = false;

                            CapNumTxt.Enabled = false;
                           
                            //AddCapBtn.Enabled = false;

                            Pnl_AddedGrades.Visible = false;
                            btn_AddGrades.Visible = false;
                            dvLastModifiedDate.Visible = false;
                            dvLastModifiedUser.Visible = false;

                            LoadData(sclYearID, ViewState["SclCode"].ToString());
                            // LoadData(nextTerm, "10020");
                            string BlockedSchoolsFromDistribution = ConfigurationManager.AppSettings["BlockedSchoolsFromDistribution"];
                            if (BlockedSchoolsFromDistribution.Contains(ViewState["SclCode"].ToString()))
                            {
                                foreach (GridViewRow r in GridView1.Rows)
                                {
                                    DropDownList ddGr = (DropDownList)r.FindControl("DDLGrades");
                                    TextBox txtbox = (TextBox)r.FindControl("DistTxt");
                                    txtbox.Enabled = false;
                                    ddGr.Enabled = false;
                                }
                            }
                            else
                            {
                                foreach (GridViewRow r in GridView1.Rows)
                                {
                                    DropDownList ddGr = (DropDownList)r.FindControl("DDLGrades");
                                    TextBox txtbox = (TextBox)r.FindControl("DistTxt");
                                    txtbox.Enabled = true;
                                    ddGr.Enabled = true;
                                }
                            }
                            //tarek el beheidy 25.4.2018 hide the seat capacity percentage panel for school user as per Mr. Tarek al Abdallah Request
                            fieldsetSeatCapacityPercentage.Visible = false;
                        }
                    }
                    catch(Exception ex)
                    {
                        DBOperationResult ReturnedResult2 = TransactionLogsRepository.Insert(new TransactionsLog { CreatedDate = DateTime.Now, ShortDescription = "Seat Capacity Control", Description = "Seat Capacity Page Load Exception: " + ex.Message, UserID = SPContext.Current.Site.RootWeb.CurrentUser.LoginName }).Result;
                        if ((uint)CultureInfo.CurrentUICulture.LCID == 1033)
                        {
                            CustomMessageBoxHelper.Show(this.Page, "Service is currently unavaliable, please contact helpdesk");
                        }
                        else
                        {
                            // CustomMessageBoxHelper.Show(this.Page, "من فضلك ت");
                            CustomMessageBoxHelper.Show(this.Page, " الخدمة غير متاحة حاليا، برجاء التواصل مع ادارة الدعم الفني");

                        }
                    }
                }

                else
                {
                    if ((uint)CultureInfo.CurrentUICulture.LCID == 1025)
                    {
                        CustomMessageBoxHelper.Show(this.Page, "You do not have permission to access this page");
                    }
                    else
                    {
                        // CustomMessageBoxHelper.Show(this.Page, "من فضلك ت");
                        CustomMessageBoxHelper.Show(this.Page, "ليس لديك صلاحية الدخول لهذه الصفحه ");

                    }
                }
            }

        }

        private void LoadData(string sclYearID, string sclCode1)
        {

            GridView1.DataSource = null;
            GridView1.DataBind();
            DDLnewGrades.ClearSelection();
            DDLNumbrOfGrades.ClearSelection();

            string GradesFromNCIS = "";

            DDLNumbrOfGrades.Enabled = false;
            //btn_AddGrades.Enabled = false;
            LinkBtnSubmit.Enabled = true;



            string nextTerm = TermRepository.GetTerms().Result.Where(s => s.ACADEMIC_YEAR_DESC == "PREENROLLMENT").Select(d => d).FirstOrDefault().TermCode;
            //string sclYearID = DateTime.Now.Year.ToString();
            string sclCode = sclCode1;// DdlSclCode.SelectedValue.ToString();


            Pnl.Visible = true;

            MOE_SCHOOL_Model schoolModel = MOE_SCHOOL_Repository.GetSchoolInfo(sclYearID, sclCode, "false").Result;
            string currentTerm = (DateTime.Now.Year).ToString();
            nxtTermTxt.Text = currentTerm + "-" + nextTerm;

            sclCodeTxt.Text = sclCode;

            if ((uint)CultureInfo.CurrentUICulture.LCID == 1025)
            {
                scNameTxt.Text = schoolModel.MOE_SCHOOL_NAME_ARA.ToString();
            }
            else
            {

                scNameTxt.Text = schoolModel.MOE_SCHOOL_NAME_ENG.ToString();
            }
           
            // scCurriculumTxt.Text = schoolModel.MOE_SCHOOL_CURRICULUM.ToString();
            if (schoolModel.MOE_SCHOOL_CURRENT_CAPACITY != 0)
            {


                int currYrCapacity = schoolModel.MOE_SCHOOL_CURRENT_CAPACITY;

                int currEnrolled = schoolModel.MOE_SCHOOL_CURRENT_ENROLLMENTS;

                currEnrolledtxt.Text = currEnrolled.ToString();
                currYearCaptxt.Text = currYrCapacity.ToString();
                AvailSeatstxt.Text = (currYrCapacity - currEnrolled).ToString();

                //reading data from Ncis
                List<schoolGrade> SchoolDataList = new List<schoolGrade>();
                SchoolDataList = schoolModel.schoolGrades;

                //ordering the grades
                var newSortedList = SchoolDataList.Select(x => new
                {
                    x.CurrentCapacity,
                    x.CurrentEnrollments,
                    x.Grade,
                    GradeMapping = getGradeMapping(x.Grade)
                });


                var SortedSchoolDataList = newSortedList.OrderBy(s => s.GradeMapping).Select(p => new schoolGrade { CurrentCapacity = p.CurrentCapacity, Grade = p.Grade, CurrentEnrollments = p.CurrentEnrollments });


                SchoolDataList = SortedSchoolDataList.ToList<schoolGrade>();



                DataTable dt = new DataTable();

                dt.Columns.Add("Preenrollment seats for grade", typeof(int));
                dt.Columns.Add("Grade", typeof(string));
                dt.Columns.Add("CurrentEnrollments", typeof(int));
                dt.Columns.Add("CurrentCapacity", typeof(int));
                dt.Columns.Add("Distribution of newly allocated seats", typeof(int));
                dt.Columns.Add("CurrentYearAvailableSeats", typeof(int));
                dt.Columns.Add("SeatsafterPromotion", typeof(int));
                dt.Columns.Add("AvailableseatsAfterPromotion", typeof(int));
                dt.Columns.Add("GradeMapping", typeof(int));
                //dt.Columns.Add("IsEnabled", typeof(bool));

                int t = 0;



                foreach (schoolGrade sc in SchoolDataList)
                {
                    DataRow dr = dt.NewRow();

                    string s = (sc.CurrentEnrollments.ToString());

                    dr["Preenrollment seats for grade"] = Convert.ToInt32(s);
                    dr["Grade"] = sc.Grade.ToString();
                    dr["CurrentEnrollments"] = Convert.ToInt32(sc.CurrentEnrollments);
                    dr["CurrentCapacity"] = Convert.ToInt32(sc.CurrentCapacity);
                    dr["CurrentYearAvailableSeats"] = (Convert.ToInt32(sc.CurrentCapacity) - Convert.ToInt32(sc.CurrentEnrollments));

                    GradesFromNCIS = GradesFromNCIS + "," + sc.Grade.ToString();

                    //  dr["IsEnabled"] = false; ;
                    // currYrCapacity = currYrCapacity + Convert.ToInt32(dr["CurrentCapacity"]);
                    // currEnrolled = currEnrolled + Convert.ToInt32(dr["CurrentEnrollments"]);

                    if (SchoolDataList.IndexOf(sc) == 0)
                    {
                        dr["SeatsafterPromotion"] = 0;
                        dr["AvailableseatsAfterPromotion"] = Convert.ToInt32(sc.CurrentCapacity) - 0;
                    }
                    else
                    {
                        dr["SeatsafterPromotion"] = Convert.ToInt32(SchoolDataList[t].CurrentEnrollments);
                        dr["AvailableseatsAfterPromotion"] = Convert.ToInt32(sc.CurrentCapacity) - (Convert.ToInt32(SchoolDataList[t].CurrentEnrollments));
                        t++;
                    }

                    dr["GradeMapping"] = getGradeMapping(sc.Grade.ToString());
                    dt.Rows.Add(dr);


                }

                //getting the additional grades added newly

                List<SeatCapacityModel> SeatCap_models = SeatCapacityRepository.CheckNumberofGrades(Convert.ToInt32(nextTerm), sclCode).Result;

                List<SeatCapacityModel> SeatCap_additionalGrades = SeatCap_models.Where(D => !GradesFromNCIS.Contains(D.MOE_SCHOOL_GRADE)).Select(P => P).ToList();
                //DataTable dt = (DataTable)ViewState["CurrentTable"]
                if (SeatCap_additionalGrades.Count > 0)
                {
                    foreach (SeatCapacityModel model in SeatCap_additionalGrades)
                    {
                        DataRow dr = dt.NewRow();
                        dr["Preenrollment seats for grade"] = model.MOE_PREENROL_SEATS;
                        dr["Grade"] = model.MOE_SCHOOL_GRADE;
                        dr["CurrentEnrollments"] = model.MOE_CURRENT_ENROLLED;
                        dr["CurrentCapacity"] = "0";
                        dr["CurrentYearAvailableSeats"] = "0";
                        dr["SeatsafterPromotion"] = "0";
                        dr["AvailableseatsAfterPromotion"] = "0";
                        dr["Distribution of newly allocated seats"] = model.MOE_SEAT_DISTRIBUTION;
                        dr["GradeMapping"] = getGradeMapping(model.MOE_SCHOOL_GRADE);

                        dt.Rows.Add(dr);

                    }

                }

                //tarek el beheidy 24.4.2018=> sorting the datatable before binding to get the correct last grade passing out
                DataView DV = dt.DefaultView;
                DV.Sort = "GradeMapping ASC";
                DataTable sortedDT = DV.ToTable();

                // currEnrolledtxt.Text = currEnrolled.ToString();
                //currYearCaptxt.Text = currYrCapacity.ToString();
                AvailSeatstxt.Text = (currYrCapacity - currEnrolled).ToString();
                //lastGrdSeatstxt.Text = (SchoolDataList[SchoolDataList.Count - 1].CurrentEnrollments).ToString();
                lastGrdSeatstxt.Text = sortedDT.Rows[sortedDT.Rows.Count - 1]["CurrentEnrollments"].ToString();// (SchoolDataList[SchoolDataList.Count - 1].CurrentEnrollments).ToString();


                if ((uint)CultureInfo.CurrentUICulture.LCID == 1025)
                { GridView1.Columns[0].FooterText = "المجموع"; }
                else { GridView1.Columns[0].FooterText = "Totals:"; }


                GridView1.Columns[1].FooterText = dt.AsEnumerable().Select(x => x.Field<int>("CurrentEnrollments")).Sum().ToString();
                GridView1.Columns[2].FooterText = dt.AsEnumerable().Select(x => x.Field<int>("CurrentCapacity")).Sum().ToString();
                GridView1.Columns[3].FooterText = dt.AsEnumerable().Select(x => x.Field<int>("CurrentYearAvailableSeats")).Sum().ToString();
                GridView1.Columns[4].FooterText = dt.AsEnumerable().Select(x => x.Field<int>("SeatsafterPromotion")).Sum().ToString();
                GridView1.Columns[5].FooterText = dt.AsEnumerable().Select(x => x.Field<int>("AvailableseatsAfterPromotion")).Sum().ToString();
                //GridView1.Columns[5].FooterText = dt.AsEnumerable().Select(x => x.Field<int>("CurrentEnrollments")).Sum().ToString();
                //GridView1.Columns[5].FooterText = dt.AsEnumerable().Select(x => x.Field<int>("CurrentEnrollments")).Sum().ToString();

                ViewState["CurrentTable"] = sortedDT; //saving the datatable for addrow

                GridView1.DataSource = sortedDT;
                GridView1.DataBind();

                FillGradesDDLinGridView();
                int n = 0;
                foreach (GridViewRow gvr in GridView1.Rows)
                {
                    DropDownList Ddltemp = (DropDownList)gvr.FindControl("DDLGrades");

                    //Ddltemp.Items.FindByText(b).Selected = true;
                    Label GrdLevelLBL = (Label)gvr.FindControl("GrdLevelLBL");
                    // TextBox DistText1 = (TextBox)gvr.FindControl("DistTxt");
                    if (GrdLevelLBL != null && GrdLevelLBL.Text != "")
                    {
                        Ddltemp.SelectedItem.Text = GrdLevelLBL.Text;//SchoolDataList[n].Grade;
                        n++;
                    }


                }


                bool isMinistryUser = false;
                isMinistryUser = SharePointUtilityRepository.IsUserInGroup(PSPESConstants.PrivateSchoolOfficeSharePointGroup, SPContext.Current.Site.RootWeb.CurrentUser.LoginName);


                if (isMinistryUser == true)
                {
                    if ((uint)CultureInfo.CurrentUICulture.LCID == 1025)
                    {
                        utype.SelectedItem.Text = "موظف الوزارة";
                    }
                    else
                    {

                        utype.SelectedItem.Text = "Ministry user";
                    }



                    DDLnewGrades.Enabled = true;
                    CapNumTxt.Enabled = true;
                    foreach (GridViewRow r in GridView1.Rows)
                    {
                        TextBox txtbox = (TextBox)r.FindControl("DistTxt");
                        txtbox.Enabled = false;
                    }

                }
                else
                {
                    if ((uint)CultureInfo.CurrentUICulture.LCID == 1025)
                    {
                        utype.SelectedItem.Text = "موظف المدرسة";
                    }
                    else
                    {

                        utype.SelectedItem.Text = "School user";
                    }




                    CapNumTxt.Enabled = false;
                    foreach (GridViewRow r in GridView1.Rows)
                    {
                        TextBox txtbox = (TextBox)r.FindControl("DistTxt");
                        txtbox.Enabled = true;
                    }

                    Pnl_AddedGrades.Visible = false;
                    btn_AddSeatCapacity.Visible = false;

                }


                GridView1.HeaderRow.TableSection = TableRowSection.TableHeader;

                if (CapNumTxt.Text == string.Empty)
                {
                    CapNumTxt.Text = "0";
                    HiddenCapNumTxt.Text = "0";
                }





                if ((MOEHE.PSPES.Repository.AddedCapacityRepository.GetAddedCapacity(Convert.ToInt32(nextTerm), sclCode)).Result.Count > 0)  // != null)
                {
                    List<AddedCapacityModel> AddCapMd = AddedCapacityRepository.GetAddedCapacity(Convert.ToInt32(nextTerm), sclCode).Result;
                    CapNumTxt.Text = (AddCapMd[0].MOE_ADDED_CAPACITY1).ToString();
                    HiddenCapNumTxt.Text = (AddCapMd[0].MOE_ADDED_CAPACITY1).ToString();
                    //CapNumTxt.Enabled = false;
                }

                else
                {
                    CapNumTxt.Text = "0";
                    HiddenCapNumTxt.Text = "0";

                }

                if (CapNumTxt.Text != null && CapNumTxt.Text != string.Empty)
                    percentTxt.Text = ((Convert.ToInt32(CapNumTxt.Text) * 100) / Convert.ToInt32(currYearCaptxt.Text)).ToString() + " %";

                scCapNxtYrTxt.Text = (currYrCapacity + Convert.ToInt32(CapNumTxt.Text)).ToString();

                #region Old Calculation of Preenrollemtn seats for next year before tarek el beheidy change in 24.4.208

                /* tarek el beheidy 24.4.208=>moved preenrollment seats for next year calculations after setting the correct data ( either from NSIS or from DB) this is the old calculations*/
                /*
                                 if (CapNumTxt.Text != "0")
                {

                    if ((AddedGradeRepository.GetAddedGrades(Convert.ToInt32(nextTerm), sclCode)).Result.Count > 0)
                    {
                        PreenrollnxtYrTxt.Text = ((currYrCapacity - currEnrolled) + Convert.ToInt32(CapNumTxt.Text)).ToString();
                    }
                    else
                    {
                        PreenrollnxtYrTxt.Text = ((currYrCapacity - currEnrolled) + Convert.ToInt32(lastGrdSeatstxt.Text) + Convert.ToInt32(CapNumTxt.Text)).ToString();
                    }

                    List<AddedCapacityModel> AddCapMd2 = AddedCapacityRepository.GetAddedCapacity(Convert.ToInt32(nextTerm), sclCode).Result;
                    if (int.Parse(PreenrollnxtYrTxt.Text) < 0 && (AddCapMd2.Count > 0))
                    {
                        PreenrollnxtYrTxt.Text = AddCapMd2[0].MOE_ADDED_CAPACITY1.ToString();
                    }

                }
                else
                {

                    if ((AddedGradeRepository.GetAddedGrades(Convert.ToInt32((DateTime.Now.Year + 1).ToString()), sclCodeTxt.Text)).Result.Count > 0)
                    {
                        PreenrollnxtYrTxt.Text = ((currYrCapacity - currEnrolled) + Convert.ToInt32(CapNumTxt.Text)).ToString();
                    }
                    else
                    {
                        PreenrollnxtYrTxt.Text = ((currYrCapacity - currEnrolled) + Convert.ToInt32(lastGrdSeatstxt.Text) + Convert.ToInt32(CapNumTxt.Text)).ToString();
                    }

                }
                 */
                #endregion
                try
                {
                    //tarek.elbeheidy 24.4.2018=>setting the total of the preenrolled students after filling the data in grid and setting the total preenrolled students in top of the page
                    int CurrentEnrolledStudentsSum = 0;
                    foreach (GridViewRow r in GridView1.Rows)
                    {
                        DropDownList grade1 = (DropDownList)r.FindControl("DDLGrades");      //("GrdLevelLBL");
                        Label preEnrolLbl = (Label)r.FindControl("PreenLbl");
                        TextBox DistributeTxt = (TextBox)r.FindControl("DistTxt");
                        Label CurrEnrollmnts = (Label)r.FindControl("CrEnCLBL");

                        Label HiddnDist = (Label)r.FindControl("lblHiddenDistNum");

                        //if data exists in the DB , load from db 

                        if (SeatCapacityRepository.CheckExistsSeatCapacity(Convert.ToInt32(nextTerm), sclCode, grade1.SelectedItem.Text).Result.Count > 0)// != null)
                        {
                            List<SeatCapacityModel> SeatCapMd = SeatCapacityRepository.CheckExistsSeatCapacity(Convert.ToInt32(nextTerm), sclCode, grade1.SelectedItem.Text).Result;


                            DistributeTxt.Text = SeatCapMd[0].MOE_SEAT_DISTRIBUTION.ToString();
                            HiddnDist.Text = SeatCapMd[0].MOE_SEAT_DISTRIBUTION.ToString();
                            //added for geting previous value of distribution 

                            preEnrolLbl.Text = SeatCapMd[0].MOE_PREENROL_SEATS.ToString();
                            lstMdDtTxt.Text = SeatCapMd[0].MOE_TRANSACTION_DTMM.ToString();
                            LstUpDTUsrTxt.Text = SeatCapMd[0].MOE_USERID;
                            CurrEnrollmnts.Text = SeatCapMd[0].MOE_CURRENT_ENROLLED.ToString();
                            //tarek.elbeheidy 24.4.2018=>setting the total of the preenrolled students after filling the data in grid and setting the total preenrolled students in top of the page

                            CurrentEnrolledStudentsSum += SeatCapMd[0].MOE_CURRENT_ENROLLED;

                        }
                        else
                        {
                            //tarek.elbeheidy 24.4.2018=>setting the total of the preenrolled students after filling the data in grid and setting the total preenrolled students in top of the page
                            CurrentEnrolledStudentsSum += int.Parse(CurrEnrollmnts.Text);
                            if (CapNumTxt.Text == "" || CapNumTxt.Text == string.Empty || CapNumTxt.Text == "0")
                            {
                                TextBox txtboxt4 = (TextBox)r.FindControl("DistTxt");
                                txtboxt4.Enabled = false;

                            }

                            TextBox txtbox1 = (TextBox)r.FindControl("DistTxt");
                            if (txtbox1.Text != null && txtbox1.Text != "")  //&& txtbox1.Text != "0"
                            {
                                Label t2 = (Label)r.FindControl("AvaiSeatAfterPrLBL");
                                string b = t2.Text;
                                string c = txtbox1.Text;
                                if (c == "") { c = "0"; }
                                int temp;

                                temp = Convert.ToInt32(b) + Convert.ToInt32(c);
                                preEnrolLbl.Text = temp.ToString();

                            }
                            DistributeTxt.Text = "0";
                            HiddnDist.Text = "0";



                            // preEnrolLbl.Text = "0";
                            lstMdDtTxt.Text = "-";
                            LstUpDTUsrTxt.Text = "-";
                        }


                    }
                    //tarek.elbeheidy 24.4.2018=>setting the total of the preenrolled students after filling the data in grid and setting the total preenrolled students in top of the page

                    currEnrolledtxt.Text = CurrentEnrolledStudentsSum.ToString();

                    //change the last grade passing out after updating the data from DB

                    Label lastPassingGrade = (Label)GridView1.Rows[GridView1.Rows.Count - 1].FindControl("CrEnCLBL");
                    lastGrdSeatstxt.Text = lastPassingGrade.Text;// (SchoolDataList[SchoolDataList.Count - 1].CurrentEnrollments).ToString();


                    Label CurrentEnrollmentsTotal = (Label)GridView1.FooterRow.FindControl("CurrentEnrollmentsTotal");
                    CurrentEnrollmentsTotal.Text = CurrentEnrolledStudentsSum.ToString();
                    GridView1.FooterRow.DataBind();
                    AvailSeatstxt.Text = (currYrCapacity - CurrentEnrolledStudentsSum).ToString();

                    #region preenrollment seat for next year New Calculations after tarek el beheidy change in 24.4.2018

                    if (CapNumTxt.Text != "0")
                    {
                        //Tarek El Beheidy 30.4.2018 => fix the negative case to have only the increased seat capacity number
                        int PreEnrollmentSeatsForNexYearWithoutAddedCapacity = ((currYrCapacity - int.Parse(currEnrolledtxt.Text)) + Convert.ToInt32(lastGrdSeatstxt.Text));
                        List<AddedCapacityModel> AddCapMd2 = AddedCapacityRepository.GetAddedCapacity(Convert.ToInt32(nextTerm), sclCode).Result;
                        if ((PreEnrollmentSeatsForNexYearWithoutAddedCapacity < 0) && (AddCapMd2.Count > 0))
                        {
                            PreenrollnxtYrTxt.Text = AddCapMd2[0].MOE_ADDED_CAPACITY1.ToString();
                        }
                        else
                        {
                            //TAREK EL BEHEIDY 13.5.2018, REMOVE THE IF CONDITION AS WE ALREADY SORTED THE GRADES CORRECTLY ABOVE, SO WE ARE GETTING THE LAST PASSING GRADE CORRECTLY AFTER SORTING
                            //if ((AddedGradeRepository.GetAddedGrades(Convert.ToInt32(nextTerm), sclCode)).Result.Count > 0)
                            //{
                            //    PreenrollnxtYrTxt.Text = ((currYrCapacity - int.Parse(currEnrolledtxt.Text)) + Convert.ToInt32(CapNumTxt.Text)).ToString();
                            //}
                            //else
                            //{
                            PreenrollnxtYrTxt.Text = ((currYrCapacity - int.Parse(currEnrolledtxt.Text)) + Convert.ToInt32(lastGrdSeatstxt.Text) + Convert.ToInt32(CapNumTxt.Text)).ToString();
                            //}
                        }


                    }
                    else
                    {
                        //TAREK EL BEHEIDY 13.5.2018, REMOVE THE IF CONDITION AS WE ALREADY SORTED THE GRADES CORRECTLY ABOVE, SO WE ARE GETTING THE LAST PASSING GRADE CORRECTLY AFTER SORTING
                        //if ((AddedGradeRepository.GetAddedGrades(Convert.ToInt32((DateTime.Now.Year + 1).ToString()), sclCodeTxt.Text)).Result.Count > 0)
                        //{
                        //    PreenrollnxtYrTxt.Text = ((currYrCapacity - int.Parse(currEnrolledtxt.Text)) + Convert.ToInt32(CapNumTxt.Text)).ToString();
                        //}
                        //else
                        //{
                        PreenrollnxtYrTxt.Text = ((currYrCapacity - int.Parse(currEnrolledtxt.Text)) + Convert.ToInt32(lastGrdSeatstxt.Text)).ToString();// + Convert.ToInt32(CapNumTxt.Text)).ToString();
                        //}

                    }

                    #endregion




                    //if (int.Parse(PreenrollnxtYrTxt.Text) < 0)
                    //{
                    //    PreenrollnxtYrTxt.Text = CapNumTxt.Text;
                    //}
                }
                catch (Exception ex)
                { }
            }
            else
            {
                string errorMessage = "";
                if ((uint)CultureInfo.CurrentUICulture.LCID == 1025)
                {
                    errorMessage = "لم يتم تحديد الطاقة الاستيعابية الحالية للمدرسة، برجاء التواصل مع الموظف المختص";
                }
                else
                {
                    errorMessage = "No Capacity defined for the school, please contact system administrator";
                }

                CustomMessageBoxHelper.Show(this.Page, errorMessage);
            }

        }

        protected void DistTxt_TextChanged(object sender, EventArgs e)
        {

            TextBox txx = (TextBox)sender;
            int sum = 0;

            foreach (GridViewRow r in GridView1.Rows)
            {

                TextBox txtbox = (TextBox)r.FindControl("DistTxt");
                int val = 0;
                if (txtbox.Text == "")
                {
                    val = 0;
                }
                else
                {
                    val = Convert.ToInt32(txtbox.Text);
                }
                sum += Convert.ToInt32(val);
            }
            //TextBox txx = (TextBox)sender;
            if (sum > Convert.ToInt32(PreenrollnxtYrTxt.Text))
            {
                //ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('value should be within range');", true);
                //if ((uint)CultureInfo.CurrentUICulture.LCID == 1025)
                //{ MsgLbl.Text = CapNumTxt.Text + "عذرا، لا يمكنك إدخال قيمة تجعل الإجمالي أكثر من السعة المحددة  "; }
                //else { MsgLbl.Text = "Sorry, You can not enter a value that brings the total to more than the assigned capacity of : " + CapNumTxt.Text; }

                //MsgLbl.Visible = true;
                string msg1 = "";
                if ((uint)CultureInfo.CurrentUICulture.LCID == 1025)
                { msg1 = CapNumTxt.Text + "عذرا، لا يمكنك إدخال قيمة تجعل الإجمالي أكثر من السعة المحددة  "; }
                else { msg1 = "You can not enter a value that brings the total to more than  : " + PreenrollnxtYrTxt.Text; }
                CustomMessageBoxHelper.Show(this.Page, msg1);

                txx.Text = "0";
                txx.Focus();
            }




            String Term = (DateTime.Now.Year + 1).ToString();
            String schoolCode = sclCodeTxt.Text;
            //DropDownList grdDdl = (DropDownList)GridView1.FindControl("DDLGrades");

            TextBox thisDistTextBox = (TextBox)sender;
            GridViewRow currentRow = (GridViewRow)thisDistTextBox.Parent.Parent;
            int rowindex = 0;
            rowindex = currentRow.RowIndex;
            DropDownList grdDdl = (DropDownList)currentRow.FindControl("DDLGrades");
            Label distHiddenLbl = (Label)currentRow.FindControl("lblHiddenDistNum");

            //if( int.Parse(distHiddenLbl.Text) > int.Parse(thisDistTextBox.Text))
            //{
            // SeatReservationFeeModel SRFModel = new SeatReservationFeeModel();
            int CountofConfirmedAppsbyGrade = SeatReservationFeeRepository.GetCountOfApplientApplications(int.Parse(Term), schoolCode, grdDdl.SelectedItem.Text).Result;
            if (CountofConfirmedAppsbyGrade > 0)
            {
                if (int.Parse(thisDistTextBox.Text) < CountofConfirmedAppsbyGrade)
                {
                    string msg1 = "";
                    if ((uint)CultureInfo.CurrentUICulture.LCID == 1025)
                    { msg1 = CountofConfirmedAppsbyGrade + " :لا يمكن للعدد أن يقل عن عدد الطلبات المؤكدة لهذا الصف "; }
                    else { msg1 = "Can not decrease to below the number of confirmed applications for this grade " + CountofConfirmedAppsbyGrade; }
                    CustomMessageBoxHelper.Show(this.Page, msg1);
                    thisDistTextBox.Text = distHiddenLbl.Text;

                    return;
                }
            }

            // }
            distHiddenLbl.Text = thisDistTextBox.Text;

            foreach (GridViewRow rr in GridView1.Rows)
            {
                TextBox txtbox1 = (TextBox)rr.FindControl("DistTxt");
                //if (txtbox1.Text != null && txtbox1.Text != "")  //&& txtbox1.Text != "0"
                //{
                // string a = rr.Cells[7].Text;
                TextBox t1 = (TextBox)rr.FindControl("DistTxt");
                string a = t1.Text;
                //  string b = rr.Cells[5].Text;
                Label t2 = (Label)rr.FindControl("AvaiSeatAfterPrLBL");
                string b = t2.Text;
                string c = txtbox1.Text;
                if (c == "" || c == null) { c = "0"; }
                if (b == "" || b == null) { b = "0"; }
                int temp;

                temp = Convert.ToInt32(b) + Convert.ToInt32(c);
                Label preEnrolSeats = (Label)rr.FindControl("PreenLbl");
                preEnrolSeats.Text = temp.ToString();
                //}

                //if (txtbox1.Text == "") {txtbox1.Text= "0"; }
                //DistSum += Convert.ToInt32(txtbox1.Text);
                //GridView1.Columns[6].FooterText = DistSum.ToString();
                //PreEnSum += Convert.ToInt32(preEnrolSeats.Text);

                //var insertSeats = SeatCapacityRepository.Insert(new SeatCapacityModel {MOE_TERM = Convert.ToInt32(nextTerm), MOE_SCHOOL_CODE = sclCodeTxt.Text, MOE_SCHOOL_GRADE = rr.Cells[0].Text, MOE_SEAT_DISTRIBUTION = Convert.ToInt32(rr.Cells[6].Text), MOE_PREENROL_SEATS = Convert.ToInt32(rr.Cells[7].Text) }).Result;

            }

            //DistSum += Convert.ToInt32(a);

            //PreEnSum += Convert.ToInt32(preEnrolSeats.Text);

            //var insertSeats = SeatCapacityRepository.Insert(new SeatCapacityModel {MOE_TERM = Convert.ToInt32(nextTerm), MOE_SCHOOL_CODE = sclCodeTxt.Text, MOE_SCHOOL_GRADE = rr.Cells[0].Text, MOE_SEAT_DISTRIBUTION = Convert.ToInt32(rr.Cells[6].Text), MOE_PREENROL_SEATS = Convert.ToInt32(rr.Cells[7].Text) }).Result;
            //GridView1.Columns[6].FooterText = DistSum.ToString();
            //GridView1.Columns[7].FooterText = PreEnSum.ToString();
        }

        protected void utype_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (utype.SelectedValue == "1")
            {
                CapNumTxt.Enabled = true;
                // AddCapBtn.Enabled = true;
                foreach (GridViewRow r in GridView1.Rows)
                {
                    TextBox txtbox = (TextBox)r.FindControl("DistTxt");
                    txtbox.Enabled = false;
                }
            }
            else if (utype.SelectedValue == "2")
            {
                CapNumTxt.Enabled = false;
                //AddCapBtn.Enabled = false;
                foreach (GridViewRow r in GridView1.Rows)
                {
                    TextBox txtbox = (TextBox)r.FindControl("DistTxt");
                    txtbox.Enabled = true;
                }

            }
            else { }
        }



        protected void LinkBtnSubmit_Click(object sender, EventArgs e)
        {
            //try
            //{
            foreach (GridViewRow gr in GridView1.Rows)
            {

                TextBox L = (TextBox)gr.FindControl("DistTxt");
                if (L.Text == string.Empty || L.Text == "")

                {
                    L.Text = "0";
                    //MsgLbl.Text = "Sorry, All rows must be filled before submitting";
                    //MsgLbl.Visible = true;
                    //return;
                }
            }

            foreach (GridViewRow rr in GridView1.Rows)
            {
                //Label grade = (Label)rr.FindControl("GrdLevelLBL");
                DropDownList grade = (DropDownList)rr.FindControl("DDLGrades");
                TextBox seat_distribution = (TextBox)rr.FindControl("DistTxt");
                Label prEn = (Label)rr.FindControl("PreenLbl");
                Label CurrrEnrolll = (Label)rr.FindControl("CrEnCLBL");

                //String CheckLastGrade = "PreviousGrade";
                if (grade.SelectedItem.Text == "--Select--")
                {
                    string gradeSelectError = "";
                    if ((uint)CultureInfo.CurrentUICulture.LCID == 1025)
                    { gradeSelectError = "الرجاء التأكد من تحديد مستوى الصف لجميع الصفوف المضافة"; }
                    else { gradeSelectError = "Please make sure to select the Grade Level for all  added rows"; }
                    CustomMessageBoxHelper.Show(this.Page, gradeSelectError);
                    return;
                }


                if (grade.SelectedItem.Text != null && seat_distribution.Text != null)
                {

                    if (seat_distribution.Text == "")
                    { seat_distribution.Text = "0"; }

                    SeatCapacityModel SeatCapM = new SeatCapacityModel()
                    {
                        MOE_TERM = Convert.ToInt32((DateTime.Now.Year + 1)),
                        MOE_SCHOOL_CODE = sclCodeTxt.Text,
                        MOE_SCHOOL_GRADE = grade.SelectedItem.Text,
                        MOE_SEAT_DISTRIBUTION = int.Parse(seat_distribution.Text),
                        MOE_PREENROL_SEATS = Convert.ToInt32(prEn.Text),
                        MOE_USERID = SPContext.Current.Site.RootWeb.CurrentUser.Name,
                        MOE_TRANSACTION_DTMM = DateTime.Now,
                        MOE_CURRENT_ENROLLED = int.Parse(CurrrEnrolll.Text)


                    };


                    var insertSeats = SeatCapacityRepository.Insert(SeatCapM).Result;



                }
            }
            string msg = "";
            if ((uint)CultureInfo.CurrentUICulture.LCID == 1025)
            { msg = "تم الحفظ بنجاح"; }
            else { msg = "Successfully saved."; }
            CustomMessageBoxHelper.Show(this.Page, msg);

            //LinkBtnSubmit.Enabled = false;
            //}
            //catch (Exception ex)
            //{
            //}
        }

        protected void DdlSclCode_SelectedIndexChanged(object sender, EventArgs e)
        {
            bool schoolNotExistinCapacityTable = false;

            GridView1.DataSource = null;
            GridView1.DataBind();
            DDLnewGrades.Enabled = true;
            btn_AddSeatCapacity.Enabled = true;
            CapNumTxt.Enabled = true;

            DDLnewGrades.ClearSelection();
            DDLNumbrOfGrades.ClearSelection();

            string GradesFromNCIS = "";

            DDLNumbrOfGrades.Enabled = false;
            //btn_AddGrades.Enabled = false;
            LinkBtnSubmit.Enabled = true;



            string nextTerm = /*DateTime.Now.Year.ToString()*/TermRepository.GetTerms().Result.Where(s => s.ACADEMIC_YEAR_DESC == "PREENROLLMENT").Select(d => d).FirstOrDefault().TermCode;
            string sclYearID = /*DateTime.Now.Year.ToString()*/TermRepository.GetTerms().Result.Where(s => s.ACADEMIC_YEAR_DESC == "CURRENT").Select(d => d).FirstOrDefault().TermCode;
            string sclCode = DdlSclCode.SelectedValue.ToString();


            Pnl.Visible = true;

            MOE_SCHOOL_Model schoolModel = MOE_SCHOOL_Repository.GetSchoolInfo(sclYearID, sclCode, "false").Result;

            if (schoolModel.MOE_SCHOOL_CURRENT_CAPACITY != 0)
            {



                string currentTerm = (DateTime.Now.Year).ToString();
                nxtTermTxt.Text = currentTerm + "-" + nextTerm;

                sclCodeTxt.Text = sclCode;
                scNameTxt.Text = schoolModel.MOE_SCHOOL_NAME_ENG.ToString();
                // scCurriculumTxt.Text = schoolModel.MOE_SCHOOL_CURRICULUM.ToString();
                int currYrCapacity = schoolModel.MOE_SCHOOL_CURRENT_CAPACITY;

                int currEnrolled = schoolModel.MOE_SCHOOL_CURRENT_ENROLLMENTS;

                currEnrolledtxt.Text = currEnrolled.ToString();
                currYearCaptxt.Text = currYrCapacity.ToString();
                AvailSeatstxt.Text = (currYrCapacity - currEnrolled).ToString();

                List<schoolGrade> SchoolDataList = new List<schoolGrade>();
                SchoolDataList = schoolModel.schoolGrades;

                //ordering the Grades
                var newSortedList = SchoolDataList.Select(x => new
                {
                    x.CurrentCapacity,
                    x.CurrentEnrollments,
                    x.Grade,
                    GradeMapping = getGradeMapping(x.Grade)
                });


                var SortedSchoolDataList = newSortedList.OrderBy(s => s.GradeMapping).Select(p => new schoolGrade { CurrentCapacity = p.CurrentCapacity, Grade = p.Grade, CurrentEnrollments = p.CurrentEnrollments });


                SchoolDataList = SortedSchoolDataList.ToList<schoolGrade>();





                DataTable dt = new DataTable();

                dt.Columns.Add("Preenrollment seats for grade", typeof(int));
                dt.Columns.Add("Grade", typeof(string));
                dt.Columns.Add("CurrentEnrollments", typeof(int));
                dt.Columns.Add("CurrentCapacity", typeof(int));
                dt.Columns.Add("Distribution of newly allocated seats", typeof(int));
                dt.Columns.Add("CurrentYearAvailableSeats", typeof(int));
                dt.Columns.Add("SeatsafterPromotion", typeof(int));
                dt.Columns.Add("AvailableseatsAfterPromotion", typeof(int));
                dt.Columns.Add("GradeMapping", typeof(int));
                //dt.Columns.Add("IsEnabled", typeof(bool));

                int t = 0;



                //foreach (schoolGrade sc in SchoolDataList)

                foreach (schoolGrade sc in SchoolDataList)
                {
                    DataRow dr = dt.NewRow();

                    string s = (sc.CurrentEnrollments.ToString());

                    dr["Preenrollment seats for grade"] = Convert.ToInt32(s);
                    dr["Grade"] = sc.Grade.ToString();
                    dr["CurrentEnrollments"] = Convert.ToInt32(sc.CurrentEnrollments);
                    dr["CurrentCapacity"] = Convert.ToInt32(sc.CurrentCapacity);
                    dr["CurrentYearAvailableSeats"] = (Convert.ToInt32(sc.CurrentCapacity) - Convert.ToInt32(sc.CurrentEnrollments));

                    GradesFromNCIS = GradesFromNCIS + "," + sc.Grade.ToString();



                    if (SchoolDataList.IndexOf(sc) == 0)
                    {
                        dr["SeatsafterPromotion"] = 0;
                        dr["AvailableseatsAfterPromotion"] = Convert.ToInt32(sc.CurrentCapacity) - 0;
                    }
                    else
                    {
                        dr["SeatsafterPromotion"] = Convert.ToInt32(SchoolDataList[t].CurrentEnrollments);
                        dr["AvailableseatsAfterPromotion"] = Convert.ToInt32(sc.CurrentCapacity) - (Convert.ToInt32(SchoolDataList[t].CurrentEnrollments));
                        t++;
                    }
                    dr["GradeMapping"] = getGradeMapping(sc.Grade.ToString());


                    dt.Rows.Add(dr);


                }

                //getting the additional grades added newly

                List<SeatCapacityModel> SeatCap_models = SeatCapacityRepository.CheckNumberofGrades(Convert.ToInt32(nextTerm), sclCode).Result;

                List<SeatCapacityModel> SeatCap_additionalGrades = SeatCap_models.Where(D => !GradesFromNCIS.Contains(D.MOE_SCHOOL_GRADE)).Select(P => P).ToList();
                //DataTable dt = (DataTable)ViewState["CurrentTable"]
                if (SeatCap_additionalGrades.Count > 0)
                {
                    foreach (SeatCapacityModel model in SeatCap_additionalGrades)
                    {
                        DataRow dr = dt.NewRow();
                        dr["Preenrollment seats for grade"] = model.MOE_PREENROL_SEATS;
                        dr["Grade"] = model.MOE_SCHOOL_GRADE;
                        dr["CurrentEnrollments"] = model.MOE_CURRENT_ENROLLED;
                        dr["CurrentCapacity"] = "0";
                        dr["CurrentYearAvailableSeats"] = "0";
                        dr["SeatsafterPromotion"] = "0";
                        dr["AvailableseatsAfterPromotion"] = "0";
                        dr["Distribution of newly allocated seats"] = model.MOE_SEAT_DISTRIBUTION;
                        dr["GradeMapping"] = getGradeMapping(model.MOE_SCHOOL_GRADE);

                        dt.Rows.Add(dr);

                    }

                }




                //tarek el beheidy 24.4.2018=> sorting the datatable before binding to get the correct last grade passing out
                DataView DV = dt.DefaultView;
                DV.Sort = "GradeMapping ASC";
                DataTable sortedDT = DV.ToTable();

                // currEnrolledtxt.Text = currEnrolled.ToString();
                //currYearCaptxt.Text = currYrCapacity.ToString();
                AvailSeatstxt.Text = (currYrCapacity - currEnrolled).ToString();
                //lastGrdSeatstxt.Text = (SchoolDataList[SchoolDataList.Count - 1].CurrentEnrollments).ToString();
                lastGrdSeatstxt.Text = sortedDT.Rows[sortedDT.Rows.Count - 1]["CurrentEnrollments"].ToString();// (SchoolDataList[SchoolDataList.Count - 1].CurrentEnrollments).ToString();





                if ((uint)CultureInfo.CurrentUICulture.LCID == 1025)
                { GridView1.Columns[0].FooterText = "المجموع"; }
                else { GridView1.Columns[0].FooterText = "Totals:"; }

                //tarek.elbeheidy 24.4.2018 => changing this to be lable to reflect the number when changed
                GridView1.Columns[1].FooterText = dt.AsEnumerable().Select(x => x.Field<int>("CurrentEnrollments")).Sum().ToString();
                //Label CurrentEnrollmentsTotalLabel = (Label)GridView1.FooterRow.FindControl("CurrentEnrollmentsTotal");
               // CurrentEnrollmentsTotalLabel.Text = dt.AsEnumerable().Select(x => x.Field<int>("CurrentEnrollments")).Sum().ToString(); 
                GridView1.Columns[2].FooterText = dt.AsEnumerable().Select(x => x.Field<int>("CurrentCapacity")).Sum().ToString();
                GridView1.Columns[3].FooterText = dt.AsEnumerable().Select(x => x.Field<int>("CurrentYearAvailableSeats")).Sum().ToString();
                GridView1.Columns[4].FooterText = dt.AsEnumerable().Select(x => x.Field<int>("SeatsafterPromotion")).Sum().ToString();
                GridView1.Columns[5].FooterText = dt.AsEnumerable().Select(x => x.Field<int>("AvailableseatsAfterPromotion")).Sum().ToString();
                //GridView1.Columns[5].FooterText = dt.AsEnumerable().Select(x => x.Field<int>("CurrentEnrollments")).Sum().ToString();
                //GridView1.Columns[5].FooterText = dt.AsEnumerable().Select(x => x.Field<int>("CurrentEnrollments")).Sum().ToString();

                ViewState["CurrentTable"] = sortedDT; //saving the datatable for addrow

                GridView1.DataSource = sortedDT;
                GridView1.DataBind();

                FillGradesDDLinGridView();
                int n = 0;
                foreach (GridViewRow gvr in GridView1.Rows)
                {
                    DropDownList Ddltemp = (DropDownList)gvr.FindControl("DDLGrades");

                    //Ddltemp.Items.FindByText(b).Selected = true;
                    Label GrdLevelLBL = (Label)gvr.FindControl("GrdLevelLBL");
                    // TextBox DistText1 = (TextBox)gvr.FindControl("DistTxt");
                    if (GrdLevelLBL != null && GrdLevelLBL.Text != "")
                    {
                        Ddltemp.SelectedItem.Text = GrdLevelLBL.Text;//SchoolDataList[n].Grade;
                        n++;
                    }


                }

                bool isMinistryUser = false;
                isMinistryUser = SharePointUtilityRepository.IsUserInGroup(PSPESConstants.PrivateSchoolOfficeSharePointGroup, SPContext.Current.Site.RootWeb.CurrentUser.LoginName);


                if (isMinistryUser == true)


                {
                    if ((uint)CultureInfo.CurrentUICulture.LCID == 1025)
                    {
                        utype.SelectedItem.Text = "موظف الوزارة";
                    }
                    else
                    {

                        utype.SelectedItem.Text = "Ministry user";
                    }



                    DDLnewGrades.Enabled = true;

                    CapNumTxt.Enabled = true;

                    //tarek.elbeheidy 1/8/2018 add check to open distribution for PSO for the blocked schools only


                    string BlockedSchoolsFromDistribution = ConfigurationManager.AppSettings["BlockedSchoolsFromDistribution"];
                    if (BlockedSchoolsFromDistribution.Contains(sclCode))
                    {
                        foreach (GridViewRow r in GridView1.Rows)
                        {
                            //DropDownList ddGr = (DropDownList)r.FindControl("DDLGrades");
                            TextBox txtbox = (TextBox)r.FindControl("DistTxt");
                            txtbox.Enabled = true;
                            // ddGr.Enabled = false;
                        }
                    }


                    else
                    {

                        foreach (GridViewRow r in GridView1.Rows)
                        {
                            TextBox txtbox = (TextBox)r.FindControl("DistTxt");
                            txtbox.Enabled = false;
                        }
                    }
                    // LinkBtnSubmit.Visible = false;

                }
                else
                {
                    if ((uint)CultureInfo.CurrentUICulture.LCID == 1025)
                    {
                        utype.SelectedItem.Text = "موظف المدرسة";
                    }
                    else
                    {

                        utype.SelectedItem.Text = "School user";
                    }




                    CapNumTxt.Enabled = false;
                    foreach (GridViewRow r in GridView1.Rows)
                    {
                        TextBox txtbox = (TextBox)r.FindControl("DistTxt");
                        txtbox.Enabled = true;
                    }
                    Pnl_AddedGrades.Visible = false;
                    btn_AddSeatCapacity.Visible = false;

                }

                GridView1.HeaderRow.TableSection = TableRowSection.TableHeader;

                if (CapNumTxt.Text == string.Empty)
                {
                    CapNumTxt.Text = "0";
                    HiddenCapNumTxt.Text = "0";
                }


                //removed


                if ((MOEHE.PSPES.Repository.AddedCapacityRepository.GetAddedCapacity(Convert.ToInt32(nextTerm), sclCode)).Result.Count > 0)  // != null)
                {
                    List<AddedCapacityModel> AddCapMd = AddedCapacityRepository.GetAddedCapacity(Convert.ToInt32(nextTerm), sclCode).Result;
                    CapNumTxt.Text = (AddCapMd[0].MOE_ADDED_CAPACITY1).ToString();
                    HiddenCapNumTxt.Text = (AddCapMd[0].MOE_ADDED_CAPACITY1).ToString();
                    //CapNumTxt.Enabled = false;
                }

                else
                {
                    CapNumTxt.Text = "0";
                    HiddenCapNumTxt.Text = "0";
                }

                if (CapNumTxt.Text != null && CapNumTxt.Text != string.Empty)
                    percentTxt.Text = ((Convert.ToInt32(CapNumTxt.Text) * 100) / Convert.ToInt32(currYearCaptxt.Text)).ToString() + " %";

                scCapNxtYrTxt.Text = (currYrCapacity + Convert.ToInt32(CapNumTxt.Text)).ToString();

                #region Old Calculation of Preenrollemtn seats for next year before tarek el beheidy change in 24.4.208

                /* tarek el beheidy 24.4.208=>moved preenrollment seats for next year calculations after setting the correct data ( either from NSIS or from DB) this is the old calculations*/
                /*
                if (CapNumTxt.Text != "0")
                {

                    if ((AddedGradeRepository.GetAddedGrades(Convert.ToInt32(nextTerm), sclCode)).Result.Count > 0)
                    {
                        PreenrollnxtYrTxt.Text = ((currYrCapacity - currEnrolled) + Convert.ToInt32(CapNumTxt.Text)).ToString();
                    }
                    else
                    {
                        PreenrollnxtYrTxt.Text = ((currYrCapacity - currEnrolled) + Convert.ToInt32(lastGrdSeatstxt.Text) + Convert.ToInt32(CapNumTxt.Text)).ToString();
                    }
                    List<AddedCapacityModel> AddCapMd2 = AddedCapacityRepository.GetAddedCapacity(Convert.ToInt32(nextTerm), sclCode).Result;

                    if (int.Parse(PreenrollnxtYrTxt.Text) < 0 && (AddCapMd2.Count > 0))
                    {
                        PreenrollnxtYrTxt.Text = AddCapMd2[0].MOE_ADDED_CAPACITY1.ToString();
                    }
                    // int DistSum = 0;
                    //int PreEnSum = 0;
                }
                else
                {

                    if ((AddedGradeRepository.GetAddedGrades(Convert.ToInt32((DateTime.Now.Year + 1).ToString()), sclCodeTxt.Text)).Result.Count > 0)
                    {
                        PreenrollnxtYrTxt.Text = ((currYrCapacity - currEnrolled) + Convert.ToInt32(CapNumTxt.Text)).ToString();
                    }
                    else
                    {
                        PreenrollnxtYrTxt.Text = ((currYrCapacity - currEnrolled) + Convert.ToInt32(lastGrdSeatstxt.Text) + Convert.ToInt32(CapNumTxt.Text)).ToString();
                    }
                }
                */
                #endregion
                try
                {
                    //tarek.elbeheidy 24.4.2018=>setting the total of the preenrolled students after filling the data in grid and setting the total preenrolled students in top of the page
                    int CurrentEnrolledStudentsSum = 0;

                   
                    foreach (GridViewRow r in GridView1.Rows)
                    {
                        DropDownList grade1 = (DropDownList)r.FindControl("DDLGrades");      //("GrdLevelLBL");
                        Label preEnrolLbl = (Label)r.FindControl("PreenLbl");
                        TextBox DistributeTxt = (TextBox)r.FindControl("DistTxt");
                        Label CurrEnrollmnts = (Label)r.FindControl("CrEnCLBL");
                        Label HiddnDist = (Label)r.FindControl("lblHiddenDistNum");

                        //if data exists in the DB , load from db 

                        if (SeatCapacityRepository.CheckExistsSeatCapacity(Convert.ToInt32(nextTerm), sclCode, grade1.SelectedItem.Text).Result.Count > 0)// != null)
                        {
                            List<SeatCapacityModel> SeatCapMd = SeatCapacityRepository.CheckExistsSeatCapacity(Convert.ToInt32(nextTerm), sclCode, grade1.SelectedItem.Text).Result;


                            DistributeTxt.Text = SeatCapMd[0].MOE_SEAT_DISTRIBUTION.ToString();
                            HiddnDist.Text = SeatCapMd[0].MOE_SEAT_DISTRIBUTION.ToString();
                            preEnrolLbl.Text = SeatCapMd[0].MOE_PREENROL_SEATS.ToString();
                            lstMdDtTxt.Text = SeatCapMd[0].MOE_TRANSACTION_DTMM.ToString();
                            LstUpDTUsrTxt.Text = SeatCapMd[0].MOE_USERID;
                            CurrEnrollmnts.Text = SeatCapMd[0].MOE_CURRENT_ENROLLED.ToString();
                            //tarek.elbeheidy 24.4.2018=>setting the total of the preenrolled students after filling the data in grid and setting the total preenrolled students in top of the page

                            CurrentEnrolledStudentsSum += SeatCapMd[0].MOE_CURRENT_ENROLLED;

                        }
                        else
                        {
                            //tarek.elbeheidy 24.4.2018=>setting the total of the preenrolled students after filling the data in grid and setting the total preenrolled students in top of the page
                            CurrentEnrolledStudentsSum += int.Parse(CurrEnrollmnts.Text);
                            if (CapNumTxt.Text == "" || CapNumTxt.Text == string.Empty || CapNumTxt.Text == "0")
                            {
                                TextBox txtboxt4 = (TextBox)r.FindControl("DistTxt");
                                txtboxt4.Enabled = false;

                            }

                            TextBox txtbox1 = (TextBox)r.FindControl("DistTxt");
                            if (txtbox1.Text != null && txtbox1.Text != "")  //&& txtbox1.Text != "0"
                            {
                                Label t2 = (Label)r.FindControl("AvaiSeatAfterPrLBL");
                                string b = t2.Text;
                                string c = txtbox1.Text;
                                if (c == "") { c = "0"; }
                                int temp;

                                temp = Convert.ToInt32(b) + Convert.ToInt32(c);
                                preEnrolLbl.Text = temp.ToString();

                            }
                            DistributeTxt.Text = "0";
                            HiddnDist.Text = "0";



                            // preEnrolLbl.Text = "0";
                            lstMdDtTxt.Text = "-";
                            LstUpDTUsrTxt.Text = "-";
                        }


                    }
                    //tarek.elbeheidy 24.4.2018=>setting the total of the preenrolled students after filling the data in grid and setting the total preenrolled students in top of the page

                    currEnrolledtxt.Text = CurrentEnrolledStudentsSum.ToString();
                    //change the last grade passing out after updating the data from DB

                    Label lastPassingGrade = (Label)GridView1.Rows[GridView1.Rows.Count - 1].FindControl("CrEnCLBL");
                    lastGrdSeatstxt.Text = lastPassingGrade.Text;// (SchoolDataList[SchoolDataList.Count - 1].CurrentEnrollments).ToString();

                    Label CurrentEnrollmentsTotal = (Label)GridView1.FooterRow.FindControl("CurrentEnrollmentsTotal");
                    CurrentEnrollmentsTotal.Text = CurrentEnrolledStudentsSum.ToString();
                    GridView1.FooterRow.DataBind();
                   // GridView1.DataBind();

                    //int currYrCapacity = int.Parse(currYearCaptxt.Text);

                    //int currEnrolled = int.Parse(schoolModel.MOE_SCHOOL_CURRENT_ENROLLMENTS;

                    //currEnrolledtxt.Text = currEnrolled.ToString();
                    //currYearCaptxt.Text = currYrCapacity.ToString();
                    AvailSeatstxt.Text = (currYrCapacity - CurrentEnrolledStudentsSum).ToString();



                    //if (int.Parse(PreenrollnxtYrTxt.Text) < 0)
                    //{
                    //    PreenrollnxtYrTxt.Text = CapNumTxt.Text;
                    //}






                    #region preenrollment seat for next year New Calculations after tarek el beheidy change in 24.4.2018

                    if (CapNumTxt.Text != "0")
                    {
                        //Tarek El Beheidy 30.4.2018 => fix the negative case to have only the increased seat capacity number
                        int PreEnrollmentSeatsForNexYearWithoutAddedCapacity = ((currYrCapacity - int.Parse(currEnrolledtxt.Text)) + Convert.ToInt32(lastGrdSeatstxt.Text));
                        List<AddedCapacityModel> AddCapMd2 = AddedCapacityRepository.GetAddedCapacity(Convert.ToInt32(nextTerm), sclCode).Result;
                        if ((PreEnrollmentSeatsForNexYearWithoutAddedCapacity < 0) && (AddCapMd2.Count > 0))
                        {
                            PreenrollnxtYrTxt.Text = AddCapMd2[0].MOE_ADDED_CAPACITY1.ToString();
                        }
                        else
                        {
                            //TAREK EL BEHEIDY 13.5.2018, REMOVE THE IF CONDITION AS WE ALREADY SORTED THE GRADES CORRECTLY ABOVE, SO WE ARE GETTING THE LAST PASSING GRADE CORRECTLY AFTER SORTING
                            //if ((AddedGradeRepository.GetAddedGrades(Convert.ToInt32(nextTerm), sclCode)).Result.Count > 0)
                            //{
                            //    PreenrollnxtYrTxt.Text = ((currYrCapacity - int.Parse(currEnrolledtxt.Text)) + Convert.ToInt32(CapNumTxt.Text)).ToString();
                            //}
                            //else
                            //{
                                PreenrollnxtYrTxt.Text = ((currYrCapacity - int.Parse(currEnrolledtxt.Text)) + Convert.ToInt32(lastGrdSeatstxt.Text) + Convert.ToInt32(CapNumTxt.Text)).ToString();
                            //}
                        }
                       

                    }
                    else
                    {
                        //TAREK EL BEHEIDY 13.5.2018, REMOVE THE IF CONDITION AS WE ALREADY SORTED THE GRADES CORRECTLY ABOVE, SO WE ARE GETTING THE LAST PASSING GRADE CORRECTLY AFTER SORTING
                        //if ((AddedGradeRepository.GetAddedGrades(Convert.ToInt32((DateTime.Now.Year + 1).ToString()), sclCodeTxt.Text)).Result.Count > 0)
                        //{
                        //    PreenrollnxtYrTxt.Text = ((currYrCapacity - int.Parse(currEnrolledtxt.Text)) + Convert.ToInt32(CapNumTxt.Text)).ToString();
                        //}
                        //else
                        //{
                        PreenrollnxtYrTxt.Text = ((currYrCapacity - int.Parse(currEnrolledtxt.Text)) + Convert.ToInt32(lastGrdSeatstxt.Text)).ToString();// + Convert.ToInt32(CapNumTxt.Text)).ToString();
                        //}

                    }

                    #endregion

                }
                catch (Exception ex)
                { }
            }

            else
            {
                string errorMessage = "";
                if ((uint)CultureInfo.CurrentUICulture.LCID == 1025)
                {
                    errorMessage = "لم يتم تحديد الطاقة الاستيعابية الحالية للمدرسة، برجاء التواصل مع الموظف المختص";
                }
                else
                {
                    errorMessage = "No Capacity defined for the school, please contact system administrator";
                }

                CustomMessageBoxHelper.Show(this.Page, errorMessage);
            }

        }


        void FillscCodeDDL()
        {
            string sclYearID = TermRepository.GetTerms().Result.Where(s => s.ACADEMIC_YEAR_DESC == "PREENROLLMENT").Select(d => d).FirstOrDefault().TermCode;
            ListItem ItemTemp1 = new ListItem();
            ItemTemp1.Value = "0";
            ItemTemp1.Text = "--Select--";
            ItemTemp1.Selected = true;
            DdlSclCode.Items.Add(ItemTemp1);

            List<MOE_SCHOOL_Model> schoolMdl = MOE_SCHOOL_Repository.GetSchools(sclYearID, "false").Result;

            foreach (var itm in schoolMdl)
            {
                ListItem ItemTemp2 = new ListItem();
                ItemTemp2.Value = itm.MOE_SCHOOL_CODE;
                if ((uint)CultureInfo.CurrentUICulture.LCID == 1025)
                { ItemTemp2.Text = itm.MOE_SCHOOL_CODE + " - " + itm.MOE_SCHOOL_NAME_ARA; }
                else { ItemTemp2.Text = itm.MOE_SCHOOL_CODE + " - " + itm.MOE_SCHOOL_NAME_ENG; }

                ItemTemp2.Selected = false;
                DdlSclCode.Items.Add(ItemTemp2);
            }

        }

        void FilluserDDL()
        {

            ListItem ItemTemp1 = new ListItem();
            ItemTemp1.Value = "0";
            if ((uint)CultureInfo.CurrentUICulture.LCID == 1025)
            { ItemTemp1.Text = "--اختر--"; }
            else { ItemTemp1.Text = "--Select--"; }

            // ItemTemp1.Selected = true;
            utype.Items.Add(ItemTemp1);

            ListItem ItemTemp2 = new ListItem();
            ItemTemp2.Value = "1";
            if ((uint)CultureInfo.CurrentUICulture.LCID == 1025)
            { ItemTemp2.Text = "موظف الوزارة"; }
            else { ItemTemp2.Text = "Ministry user"; }


            utype.Items.Add(ItemTemp2);

            ListItem ItemTemp3 = new ListItem();
            ItemTemp3.Value = "2";
            if ((uint)CultureInfo.CurrentUICulture.LCID == 1025)
            { ItemTemp3.Text = "موظف المدرسة"; }
            else { ItemTemp3.Text = "School user"; }

            utype.Items.Add(ItemTemp3);


        }

        void FillnewGradesDDL()
        {

            ListItem ItemTemp1 = new ListItem();
            ItemTemp1.Value = "0";
            if ((uint)CultureInfo.CurrentUICulture.LCID == 1025)
            { ItemTemp1.Text = "اختر"; }
            else { ItemTemp1.Text = "Select"; }

            // ItemTemp1.Selected = true;
            DDLnewGrades.Items.Add(ItemTemp1);

            ListItem ItemTemp2 = new ListItem();
            ItemTemp2.Value = "1";
            if ((uint)CultureInfo.CurrentUICulture.LCID == 1025)
            { ItemTemp2.Text = "نعم"; }
            else { ItemTemp2.Text = "Yes"; }


            DDLnewGrades.Items.Add(ItemTemp2);

            //ListItem ItemTemp3 = new ListItem();
            //ItemTemp3.Value = "2";
            //if ((uint)CultureInfo.CurrentUICulture.LCID == 1025)
            //{ ItemTemp3.Text = "لا"; }
            //else { ItemTemp3.Text = "No"; }

            //DDLnewGrades.Items.Add(ItemTemp3);


        }


        protected void btn_AddGrades_Click(object sender, EventArgs e)
        {



            if (DDLnewGrades.SelectedValue == "1")
            {
                if (DDLNumbrOfGrades.SelectedValue != "select")
                {
                    //if ((uint)CultureInfo.CurrentUICulture.LCID == 1025)
                    //{ CustomMessageBoxHelper.Show(this.Page, "هذا الاجراء سيقوم بإضافة صفوف جديدة لهذه المدرسة"); }
                    //else { CustomMessageBoxHelper.Show(this.Page, "This action will add new grades to this school "); }

                    GridView1.DataSource = null;
                    GridView1.DataBind();


                    int rowIndex = 0;
                    if (ViewState["CurrentTable"] != null)
                    {

                        DataTable dt = (DataTable)ViewState["CurrentTable"];

                        int SelectedNum = int.Parse(DDLNumbrOfGrades.SelectedValue);
                        int z = dt.Rows.Count;
                        DataRow dr;

                        for (int i = z; i < z + SelectedNum; i++)
                        {
                            dr = dt.NewRow();
                            dt.Rows.Add(dr);
                        }
                        dt.AcceptChanges();
                        GridView1.DataSource = dt;
                        GridView1.DataBind();
                        int n = 0;

                        foreach (GridViewRow r in GridView1.Rows)
                        {
                            DropDownList Ddltemp = (DropDownList)r.FindControl("DDLGrades");
                            TextBox distributionTextBox = (TextBox)r.FindControl("DistTxt");

                            distributionTextBox.Enabled = false;
                            // Ddltemp.SelectedItem.Text = dt.
                            if (r.RowIndex <= z - 1)
                            {
                                Ddltemp.SelectedItem.Text = dt.Rows[n]["Grade"].ToString();
                                n++;
                            }
                            else
                            {
                                Label lblCREnC = (Label)r.FindControl("CrEnCLBL");
                                Ddltemp.Enabled = true;
                                lblCREnC.Text = "0";
                            }


                        }


                    }
                    AddedGradeModel AddedGRDM = new AddedGradeModel()
                    {
                        MOE_SCHOOL_YEAR = Convert.ToInt32((DateTime.Now.Year + 1)),

                        MOE_SCHOOL_CODE = sclCodeTxt.Text,
                        ADDED_NUMBER_OF_GRADES = int.Parse(DDLNumbrOfGrades.SelectedValue),
                        USERID = SPContext.Current.Site.RootWeb.CurrentUser.Name,
                        DTTM = DateTime.Now,


                    };

                    var insertSeats = AddedGradeRepository.Insert(AddedGRDM).Result;
                    PreenrollnxtYrTxt.Text = (int.Parse(PreenrollnxtYrTxt.Text) - Convert.ToInt32(lastGrdSeatstxt.Text)).ToString();

                }
                else
                {

                    if ((uint)CultureInfo.CurrentUICulture.LCID == 1025)
                    { CustomMessageBoxHelper.Show(this.Page, "يرجى تحديد عدد الصفوف المراد إضافتها أدناه"); }
                    else { CustomMessageBoxHelper.Show(this.Page, "Please select the number of grades to be added below "); }
                    return;
                }

            }

        }


        void FillGradesDDLinGridView()
        {

            List<string> gradesAvailable = new List<string>() { "01", "02", "03", "04", "05", "06", "07", "08", "09", "10", "11", "12", "13", "PK", "KG", "N" };
            foreach (GridViewRow r in GridView1.Rows)
            {
                DropDownList DDL1 = (DropDownList)r.FindControl("DDLGrades");
                DDL1.DataSource = gradesAvailable;
                DDL1.DataBind();

            }

        }

        protected void DDLNumbrOfGrades_SelectedIndexChanged(object sender, EventArgs e)
        {


        }

        protected void DDLnewGrades_SelectedIndexChanged1(object sender, EventArgs e)
        {
            //if (DDLnewGrades.SelectedItem.Text == "Yes")
            if (DDLnewGrades.SelectedValue == "1")
            {
                DDLNumbrOfGrades.Enabled = true;
                btn_AddGrades.Enabled = true;
            }

            else if (DDLnewGrades.SelectedValue == "0")
            {
                DDLNumbrOfGrades.ClearSelection();
                DDLNumbrOfGrades.Enabled = false;
                btn_AddGrades.Enabled = false;
            }

        }

        protected void DDLGrades_SelectedIndexChanged(object sender, EventArgs e)
        {
            //GridViewRow CurrentRow = ((DropDownList)sender).Parent as GridViewRow;

            DropDownList dd = (DropDownList)sender;
            GridViewRow CurrentRow = (GridViewRow)dd.Parent.Parent;
            string selectedGrade = "";
            selectedGrade = dd.SelectedValue;

            foreach (GridViewRow r in GridView1.Rows)
            {
                DropDownList Ddltemp = (DropDownList)r.FindControl("DDLGrades");


                if (Ddltemp.SelectedValue == selectedGrade && r.RowIndex != CurrentRow.RowIndex)
                {
                    if ((uint)CultureInfo.CurrentUICulture.LCID == 1025)
                    { CustomMessageBoxHelper.Show(this.Page, "يرجى تحديد صف أخر، تم تحديد هذا الخيار بالفعل"); }
                    else { CustomMessageBoxHelper.Show(this.Page, "Please select another grade , this one is already selected "); }

                    dd.ClearSelection();
                    return;

                }





            }
        }

        protected void btn_AddSeatCapacity_Click(object sender, EventArgs e)
        {
            try
            {
                if ((CapNumTxt.Text == "") || !(CapNumTxt.Text.All(char.IsNumber)))
                {
                    if ((uint)CultureInfo.CurrentUICulture.LCID == 1025)
                    {
                        CustomMessageBoxHelper.Show(this.Page, "من فضلك أدخل رقما صحيحا");
                    }
                    else { CustomMessageBoxHelper.Show(this.Page, "Please enter a Valid number."); }

                    CapNumTxt.Text = HiddenCapNumTxt.Text;
                }
                else
                {

                    if (int.Parse(CapNumTxt.Text) < 0)
                    {
                        if ((uint)CultureInfo.CurrentUICulture.LCID == 1025)
                        {
                            CustomMessageBoxHelper.Show(this.Page, "لا يمكن إدخال رقم أقل من الصفر");
                        }
                        else { CustomMessageBoxHelper.Show(this.Page, "Cannot enter a number less than zero."); }
                        CapNumTxt.Text = HiddenCapNumTxt.Text;

                    }
                    else
                    {

                        //checking the sum of distributions.

                        int sum = 0;
                        foreach (GridViewRow r in GridView1.Rows)
                        {
                            TextBox txtbox = (TextBox)r.FindControl("DistTxt");
                            int val = 0;
                            if (txtbox.Text == "")
                            {
                                val = 0;
                            }
                            else
                            {
                                val = Convert.ToInt32(txtbox.Text);
                            }
                            sum += Convert.ToInt32(val);
                        }

                        if (int.Parse(CapNumTxt.Text) < sum)
                        {
                            if ((uint)CultureInfo.CurrentUICulture.LCID == 1025)
                            {
                                CustomMessageBoxHelper.Show(this.Page, sum + " :لا يمكنك إدخال رقم أقل من العدد الإجمالي للمقاعد الموزعة");
                            }
                            else { CustomMessageBoxHelper.Show(this.Page, "Cannot enter a number less than the total number of distributed seats: " + sum); }
                            CapNumTxt.Text = HiddenCapNumTxt.Text;
                            return;
                        }
                        //chcking the number of finalized apps
                        String Term = (DateTime.Now.Year + 1).ToString();
                        String schoolCode = sclCodeTxt.Text;
                        int totalFinalizedApps = SeatReservationFeeRepository.GetAllConfirmedAppCount(int.Parse(Term), schoolCode).Result;
                        if (int.Parse(CapNumTxt.Text) < totalFinalizedApps)
                        {
                            if ((uint)CultureInfo.CurrentUICulture.LCID == 1025)
                            {
                                CustomMessageBoxHelper.Show(this.Page, totalFinalizedApps + " :لا يمكنك إدخال رقم أقل من عدد الطلبات المستكملة.");
                            }
                            else { CustomMessageBoxHelper.Show(this.Page, "Cannot enter a number less than the number of Finalized Applications: " + totalFinalizedApps); }
                            CapNumTxt.Text = HiddenCapNumTxt.Text;
                            return;
                        }


                        percentTxt.Text = ((Convert.ToInt32(CapNumTxt.Text) * 100) / Convert.ToInt32(currYearCaptxt.Text)).ToString() + " %";

                        AddedCapacityModel AddedModel = new AddedCapacityModel();
                        AddedModel.MOE_ADDED_CAPACITY1 = Convert.ToInt32(CapNumTxt.Text);
                        AddedModel.MOE_SCHOOL_CODE = sclCodeTxt.Text;
                        AddedModel.MOE_SCHOOL_YEAR = Convert.ToInt32((DateTime.Now.Year + 1));
                        var insertNew = AddedCapacityRepository.Insert(AddedModel).Result;
                        HiddenCapNumTxt.Text = CapNumTxt.Text;

                        string msg = "";
                        if ((uint)CultureInfo.CurrentUICulture.LCID == 1025)
                        { msg = "تم الحفظ بنجاح"; }
                        else { msg = "Successfully saved."; }
                        CustomMessageBoxHelper.Show(this.Page, msg);

                        //Handling the preenrolforNextYear field which is then used for Distribution OF SEATS
                        if (CapNumTxt.Text != "0")
                        {

                            if (int.Parse(PreenrollnxtYrTxt.Text) < 0)
                            {
                                PreenrollnxtYrTxt.Text = CapNumTxt.Text;

                            }
                            else
                            {

                                if ((AddedGradeRepository.GetAddedGrades(Convert.ToInt32((DateTime.Now.Year + 1).ToString()), sclCodeTxt.Text)).Result.Count > 0)
                                {
                                    PreenrollnxtYrTxt.Text = ((int.Parse(currYearCaptxt.Text) - int.Parse(currEnrolledtxt.Text)) + Convert.ToInt32(CapNumTxt.Text)).ToString();
                                }
                                else
                                {
                                    PreenrollnxtYrTxt.Text = ((int.Parse(currYearCaptxt.Text) - int.Parse(currEnrolledtxt.Text)) + Convert.ToInt32(lastGrdSeatstxt.Text) + Convert.ToInt32(CapNumTxt.Text)).ToString();
                                }
                            }

                            // tarek el beheidy 15.03.2018  moved the following if before the above calculation and inside if(capnumtxt !=0)
                            // because in case the added seats was larger than the negative number it was calculating wrong

                            //if (int.Parse(PreenrollnxtYrTxt.Text) < 0)
                            //{
                            //    PreenrollnxtYrTxt.Text = CapNumTxt.Text;

                            //}

                        }
                        else
                        {

                            if ((AddedGradeRepository.GetAddedGrades(Convert.ToInt32((DateTime.Now.Year + 1).ToString()), sclCodeTxt.Text)).Result.Count > 0)
                            {
                                PreenrollnxtYrTxt.Text = ((int.Parse(currYearCaptxt.Text) - int.Parse(currEnrolledtxt.Text)) + Convert.ToInt32(CapNumTxt.Text)).ToString();
                            }
                            else
                            {
                                PreenrollnxtYrTxt.Text = ((int.Parse(currYearCaptxt.Text) - int.Parse(currEnrolledtxt.Text)) + Convert.ToInt32(lastGrdSeatstxt.Text) + Convert.ToInt32(CapNumTxt.Text)).ToString();
                            }

                        }

                    }

                }
            }
            catch (Exception ex)
            {

            }
            finally
            {

            }
        }


        private int getGradeMapping(string gradeCode)
        {
            int gradeMapping = -20;
            switch (gradeCode)
            {
                case "N":
                    gradeMapping = -2; break;
                case "PK":
                    gradeMapping = -1; break;
                case "KG":
                    gradeMapping = 0; break;
                case "01":
                    gradeMapping = 1; break;
                case "02":
                    gradeMapping = 2; break;
                case "03":
                    gradeMapping = 3; break;
                case "04":
                    gradeMapping = 4; break;
                case "05":
                    gradeMapping = 5; break;
                case "06":
                    gradeMapping = 6; break;
                case "07":
                    gradeMapping = 7; break;
                case "08":
                    gradeMapping = 8; break;
                case "09":
                    gradeMapping = 9; break;
                case "10":
                    gradeMapping = 10; break;
                case "11":
                    gradeMapping = 11; break;
                case "12":
                    gradeMapping = 12; break;
                case "13":
                    gradeMapping = 13; break;

                default:
                    break;
            }

            return gradeMapping;

        }






    }

}


