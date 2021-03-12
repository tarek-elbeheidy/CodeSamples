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

namespace MOEHE.PSPES.Webparts.SetupSupport.SetupSupportingDocs
{
    public partial class SetupSupportingDocsUserControl : UserControl
    {
        protected override void OnInit(EventArgs e)
        {
            if (!IsPostBack)
            {

                bool isMinistryUser = false;
                bool isSchoolUser = false;
                isMinistryUser = SharePointUtilityRepository.IsUserInGroup(PSPESConstants.PrivateSchoolOfficeSharePointGroup,SPContext.Current.Site.RootWeb.CurrentUser.LoginName);
                isSchoolUser= SharePointUtilityRepository.IsUserInGroup(PSPESConstants.PrivateSchoolUsersSharePointGroup, SPContext.Current.Site.RootWeb.CurrentUser.LoginName);

                
              

                UserHelper userhelper = ADutility.GetUserDetails(SPContext.Current.Site.RootWeb.CurrentUser);
                string schoolCode = userhelper.DepartmentID;

                string NextYear = TermNameTextBox.Text;
                if (TermNameTextBox.Text == "")
                {
                    NextYear = string.Format("{0}", DateTime.Now.Year + 1);
                }
                if (SPContext.Current.Site.RootWeb.CurrentUser.LoginName.Contains("t-m.elhana"))
                {
                    schoolCode = "30120";
                }
              
                TermNameTextBox.Text = NextYear;
                TermNameTextBox.Visible = false;
                FullTermNameTextBox.Text = string.Format("{0}-{1}",DateTime.Now.Year,NextYear);


                if (schoolCode == "-1" )
                {
                    if ((uint)CultureInfo.CurrentUICulture.LCID == 1025)
                    {
                        CustomMessageBoxHelper.Show(this.Page, PSPESConstants.ArabicNoSchoolError);
                    }
                    else
                    {
                        CustomMessageBoxHelper.Show(this.Page, PSPESConstants.EnglishNoSchoolError);

                    }
                }
                else
                {
                    if (isMinistryUser)
                    {

                        ViewState["IsMinisteryUser"] = "true";
                        SchoolCodesDropDownList.Enabled = true;
                        CurriculumsDropDownList.Enabled = true;
                        PSORequiredCheckBox.Enabled = true;

                        SchoolRequiredCheckBox.Enabled = false;
                        TermNameTextBox.Text = NextYear;
                        List<MOE_SCHOOL_Model> schools = MOE_SCHOOL_Repository.GetSchools(NextYear, "false").Result;

                        if ((uint)CultureInfo.CurrentUICulture.LCID == 1025)
                        {

                            BindingUtility.LoadSchools(schools, SchoolCodesDropDownList, PSPESConstants.ArabicLanguage);
                            BindingUtility.LoadCurriculums(CurriculumsDropDownList, PSPESConstants.ArabicLanguage);

                        }
                        else
                        {
                            BindingUtility.LoadSchools(schools, SchoolCodesDropDownList, PSPESConstants.EnglishLanguage);
                            BindingUtility.LoadCurriculums(CurriculumsDropDownList, PSPESConstants.EnglishLanguage);

                        }
                    }

                    else if (isSchoolUser)
                    {


                        ViewState["IsMinisteryUser"] = "false";

                        PSORequiredCheckBox.Enabled = false;
                        SchoolRequiredCheckBox.Enabled = true;
                        try
                        {
                            string CurrentSchoolCode = schoolCode;
                            MOE_SCHOOL_Model SchoolInfo = MOE_SCHOOL_Repository.GetSchoolInfo(NextYear, CurrentSchoolCode, "false").Result;
                            List<ListOfValues_Model> AllCurricullms = ListOfValues_Repository.GetListOfValues(PSPESConstants.CurriculumsCodesetID).Result;

                            List<SchoolModel> CurrentSchool = new List<SchoolModel>();
                            SchoolModel schoolModel = new SchoolModel();
                            //List<schoolGrade> schoolGrades = MOE_SCHOOL_Repository.GetSchoolGrade(NextYear, CurrentSchoolCode, "false").Result;
                            CurriculumsDropDownList.Items.Clear();
                            List<CurriculumModel> Curriculums = new List<CurriculumModel>();
                            if ((uint)CultureInfo.CurrentUICulture.LCID == 1025)
                            {
                                schoolModel = new SchoolModel { ShcoolCode = CurrentSchoolCode, ArabicShcoolName = string.Format("{0} - {1}", SchoolInfo.MOE_SCHOOL_NAME_ARA, CurrentSchoolCode) };
                                SchoolCodesDropDownList.DataTextField = "ArabicShcoolName";

                                SchoolCodesDropDownList.DataValueField = "ShcoolCode";
                                //BindingUtility.LoadGrades(schoolGrades, SchoolGradesDropDownList, PSPESConstants.ArabicLanguage);

                                Curriculums.Add(new CurriculumModel { CurriculumID = SchoolInfo.MOE_SCHOOL_CURRICULUM_ID, ArabicCurriculumName = SupportingDocsRepository.GetCurrilculmName(AllCurricullms, SchoolInfo.MOE_SCHOOL_CURRICULUM_ID).ArabicCurriculumName });

                                CurriculumsDropDownList.DataTextField = "ArabicCurriculumName";
                                CurriculumsDropDownList.DataValueField = "CurriculumID";


                            }
                            else
                            {
                                schoolModel = new SchoolModel { ShcoolCode = CurrentSchoolCode, EnglishShcoolName = string.Format("{0} - {1}", CurrentSchoolCode, SchoolInfo.MOE_SCHOOL_NAME_ENG) };
                                SchoolCodesDropDownList.DataTextField = "EnglishShcoolName";

                                SchoolCodesDropDownList.DataValueField = "ShcoolCode";
                                //BindingUtility.LoadGrades(schoolGrades, SchoolGradesDropDownList, PSPESConstants.EnglishLanguage);


                                Curriculums.Add(new CurriculumModel { CurriculumID = SchoolInfo.MOE_SCHOOL_CURRICULUM_ID, EnglishCurriculumName = SupportingDocsRepository.GetCurrilculmName(AllCurricullms, SchoolInfo.MOE_SCHOOL_CURRICULUM_ID).EnglishCurriculumName });
                                CurriculumsDropDownList.DataTextField = "EnglishCurriculumName";
                                CurriculumsDropDownList.DataValueField = "CurriculumID";

                            }

                            CurriculumsDropDownList.DataSource = Curriculums;

                            CurriculumsDropDownList.DataBind();


                            CurrentSchool.Add(schoolModel);

                            SchoolCodesDropDownList.DataSource = CurrentSchool;
                            SchoolCodesDropDownList.DataBind();

                            #region New Grades from SeatCapacity Table to include Newly added Grades
                            //tarek.elbeheidy 18.4.2018 => changed the way grades are loaded to show the newly added grades
                            List<V_Schools_Grades_Ages> SchoolGradesAndAges = V_Schools_Grades_Ages_Repository.GetSchoolGradesAndAges(int.Parse(TermNameTextBox.Text), SchoolCodesDropDownList.SelectedValue).Result;

                            if (SchoolGradesAndAges != null && SchoolGradesAndAges.Count > 0)
                            {
                                List<V_Schools_Grades_Ages> SortedSchoolDataList = SchoolGradesAndAges.OrderBy(s => s.Weight).ToList();
                                if ((uint)CultureInfo.CurrentUICulture.LCID == 1025)
                                {
                                    BindingUtility.LoadGradesFromSeatCapacity(SortedSchoolDataList, SchoolGradesDropDownList, PSPESConstants.ArabicLanguage);
                                }
                                else
                                {
                                    BindingUtility.LoadGradesFromSeatCapacity(SortedSchoolDataList, SchoolGradesDropDownList, PSPESConstants.EnglishLanguage);
                                }
                            }
                            #endregion

                            //Check if this school code added before
                            //Check if the shcool FirstTime Login the insert it
                            List<SupportingDocsModel> supportDocsForCurrentSchool = SupportingDocsRepository.GetSupportingDocs(SchoolInfo, new SupportingDocsModel { SchoolCode = SchoolCodesDropDownList.SelectedValue, Term = TermNameTextBox.Text, SeacrhByTermAndSchoolCode = true }, true,isMinistryUser,isSchoolUser, SPContext.Current.Site.Url).Result;
                            int SupportingDocumentsCount = (SchoolGradesDropDownList.Items.Count - 1) * (DocumentTypesDropDownList.Items.Count - 1);

                            if (supportDocsForCurrentSchool.Count == 0 || supportDocsForCurrentSchool.Count != SupportingDocumentsCount)
                            {
                                List<DocumentTypeList> AllDocumentTypes = SupportingDocsRepository.GetDocumentTypeList(PSPESConstants.ArabicLanguage);
                                //Then Add all schools Document type for all grades
                                foreach (var DocumentType in AllDocumentTypes)
                                {
                                    foreach (var schoolGrade in SchoolGradesAndAges)
                                    {
                                        var InsretNew = SupportingDocsRepository.Insert(new SupportingDocsModel { IsUpdateAllowed = false, CreateDate = DateTime.Now, CreatedBy = SPContext.Current.Site.RootWeb.CurrentUser.LoginName, CurriculumID = SchoolInfo.MOE_SCHOOL_CURRICULUM_ID, DocumentTypeID = DocumentType.DocumentTypeID, Grade = schoolGrade.MOE_SCHOOL_GRADE, IsRequiredForPSO = false, IsRequiredForSchool = false, ModifiedBy = SPContext.Current.Site.RootWeb.CurrentUser.LoginName, ModifiedDate = DateTime.Now, SchoolCode = SchoolInfo.MOE_SCHOOL_CODE, SchoolName = SchoolInfo.MOE_SCHOOL_NAME_ENG, Term = TermNameTextBox.Text, ArabicSchoolName = SchoolInfo.MOE_SCHOOL_NAME_ARA }).Result;
                                    }
                                }
                            }
                        }
                        catch (Exception ex)
                        {
                            DBOperationResult ReturnedResult2 = TransactionLogsRepository.Insert(new TransactionsLog { CreatedDate = DateTime.Now, ShortDescription = "Setup supporting Documents Control", Description = "Setup supporting Documents on Init Exception: " + ex.Message, UserID = SPContext.Current.Site.RootWeb.CurrentUser.LoginName }).Result;
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

                if ((uint)CultureInfo.CurrentUICulture.LCID == 1025)
                {
                    BindingUtility.LoadDocumentType(DocumentTypesDropDownList, PSPESConstants.ArabicLanguage);

                }
                else
                {
                    BindingUtility.LoadDocumentType(DocumentTypesDropDownList, PSPESConstants.EnglishLanguage);

                }

            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {

            if (!IsPostBack)
            {
                ViewState["SupportingDocsModelList"] = null;

            }
        }



        protected void SearchButton_Click(object sender, EventArgs e)
        {

            PSORequiredCheckBox.Checked = false;
            SchoolRequiredCheckBox.Checked = false;
            try
            {
                List<SupportingDocsModel> SupportingDocs = new List<SupportingDocsModel>();

                LoadSupportingDocument();
                // From Here we will get the date from the DB based in the selection

            }
            catch (Exception ex)
            { string message = ex.Message; }
        }

        public void LoadSupportingDocument()
        {
            bool isMinistryUser = false; bool isShcoolUser = false;
            if (ViewState["IsMinisteryUser"]!=null)
            {
                isMinistryUser = bool.Parse(ViewState["IsMinisteryUser"].ToString());
                isShcoolUser = !isMinistryUser;
            }
            string NextYear = TermNameTextBox.Text;
            if (TermNameTextBox.Text == "")
            {
                NextYear = string.Format("{0}", DateTime.Now.Year + 1);
            }
            MOE_SCHOOL_Model SchoolInfo = MOE_SCHOOL_Repository.GetSchoolInfo(NextYear, SchoolCodesDropDownList.SelectedValue, "false").Result;

     
           
            isMinistryUser = SharePointUtilityRepository.IsUserInGroup(PSPESConstants.PrivateSchoolOfficeSharePointGroup, SPContext.Current.Site.RootWeb.CurrentUser.LoginName);
            isShcoolUser = SharePointUtilityRepository.IsUserInGroup(PSPESConstants.PrivateSchoolUsersSharePointGroup, SPContext.Current.Site.RootWeb.CurrentUser.LoginName);


            if (SchoolGradesDropDownList.SelectedValue == "All" && DocumentTypesDropDownList.SelectedValue == "0")
            {
                //View  school bases on all Document type for all grades
                if ((uint)CultureInfo.CurrentUICulture.LCID == 1025)
                {
                    BindingUtility.LoadSupportingDocs(SupportingDocsRepeater, new SupportingDocsModel { SchoolCode = SchoolCodesDropDownList.SelectedValue, Term = TermNameTextBox.Text,CurriculumID=CurriculumsDropDownList.SelectedValue, SeacrhByTermAndSchoolCode = true }, SchoolInfo, false, isMinistryUser,!isMinistryUser, SPContext.Current.Site.Url);
                }
                else
                {
                    BindingUtility.LoadSupportingDocs(SupportingDocsRepeater, new SupportingDocsModel { SchoolCode = SchoolCodesDropDownList.SelectedValue, Term = TermNameTextBox.Text, CurriculumID = CurriculumsDropDownList.SelectedValue, SeacrhByTermAndSchoolCode = true }, SchoolInfo, true, isMinistryUser,!isMinistryUser, SPContext.Current.Site.Url);

                }

            }
            else if (SchoolGradesDropDownList.SelectedValue != "All" && DocumentTypesDropDownList.SelectedValue != "0")
            {
                //Then view by specific schools Document type for specific grades


                if ((uint)CultureInfo.CurrentUICulture.LCID == 1025)
                {
                    BindingUtility.LoadSupportingDocs(SupportingDocsRepeater, new SupportingDocsModel { SchoolCode = SchoolCodesDropDownList.SelectedValue, Term = TermNameTextBox.Text, CurriculumID = CurriculumsDropDownList.SelectedValue, Grade = SchoolGradesDropDownList.SelectedValue, DocumentTypeID = int.Parse(DocumentTypesDropDownList.SelectedValue), SeacrhByTermAndSchoolCodeAndGradeAndDocumentType = true }, SchoolInfo, false, isMinistryUser,!isMinistryUser, SPContext.Current.Site.Url);
                }
                else
                {
                    BindingUtility.LoadSupportingDocs(SupportingDocsRepeater, new SupportingDocsModel { SchoolCode = SchoolCodesDropDownList.SelectedValue, Term = TermNameTextBox.Text, CurriculumID = CurriculumsDropDownList.SelectedValue, Grade = SchoolGradesDropDownList.SelectedValue, DocumentTypeID = int.Parse(DocumentTypesDropDownList.SelectedValue), SeacrhByTermAndSchoolCodeAndGradeAndDocumentType = true }, SchoolInfo, true, isMinistryUser,!isMinistryUser, SPContext.Current.Site.Url);

                }


            }
            else if (SchoolGradesDropDownList.SelectedValue == "All")
            {
                //Then View specific schools Document type for all grades
                if ((uint)CultureInfo.CurrentUICulture.LCID == 1025)
                {
                    BindingUtility.LoadSupportingDocs(SupportingDocsRepeater, new SupportingDocsModel { SchoolCode = SchoolCodesDropDownList.SelectedValue, CurriculumID = CurriculumsDropDownList.SelectedValue, DocumentTypeID = int.Parse(DocumentTypesDropDownList.SelectedValue), Term = TermNameTextBox.Text, SeacrhByTermAndSchoolCodeAndDocumentType = true }, SchoolInfo, false, isMinistryUser,!isMinistryUser, SPContext.Current.Site.Url);
                }
                else
                {
                    BindingUtility.LoadSupportingDocs(SupportingDocsRepeater, new SupportingDocsModel { SchoolCode = SchoolCodesDropDownList.SelectedValue, CurriculumID = CurriculumsDropDownList.SelectedValue, Term = TermNameTextBox.Text, DocumentTypeID = int.Parse(DocumentTypesDropDownList.SelectedValue), SeacrhByTermAndSchoolCodeAndDocumentType = true }, SchoolInfo, true, isMinistryUser,!isMinistryUser, SPContext.Current.Site.Url);

                }



            }
            else
            {
                //Then View  All schools Document type for specific grade
                if ((uint)CultureInfo.CurrentUICulture.LCID == 1025)
                {
                    BindingUtility.LoadSupportingDocs(SupportingDocsRepeater, new SupportingDocsModel { SchoolCode = SchoolCodesDropDownList.SelectedValue, CurriculumID = CurriculumsDropDownList.SelectedValue, Term = TermNameTextBox.Text, Grade = SchoolGradesDropDownList.SelectedValue, SeacrhByTermAndSchoolCodeAndGrade = true }, SchoolInfo, false, isMinistryUser,!isMinistryUser, SPContext.Current.Site.Url);
                }
                else
                {
                    BindingUtility.LoadSupportingDocs(SupportingDocsRepeater, new SupportingDocsModel { SchoolCode = SchoolCodesDropDownList.SelectedValue, CurriculumID = CurriculumsDropDownList.SelectedValue, Term = TermNameTextBox.Text, Grade = SchoolGradesDropDownList.SelectedValue, SeacrhByTermAndSchoolCodeAndGrade = true }, SchoolInfo, true, isMinistryUser,!isMinistryUser, SPContext.Current.Site.Url);

                }

            }

        }

        protected void SchoolCodesDropDownList_SelectedIndexChanged(object sender, EventArgs e)
        {
            List<ListOfValues_Model> AllCurricullms = ListOfValues_Repository.GetListOfValues(PSPESConstants.CurriculumsCodesetID).Result;
            List<SupportingDocsModel> SchoolsWithDocumentName = new List<SupportingDocsModel>();
            bool isMinistryUser = true;
            string NextYear = TermNameTextBox.Text;
            if (TermNameTextBox.Text == "")
            {
                NextYear = string.Format("{0}", DateTime.Now.Year + 1);
            }
            MOE_SCHOOL_Model SchoolInfo = MOE_SCHOOL_Repository.GetSchoolInfo(NextYear, SchoolCodesDropDownList.SelectedValue, "false").Result;

            //tarek el beheidy 21.03.2018 trying to handle the curriculum with , (schools with more than one curriculum)
            if (SchoolInfo!=null && SchoolInfo.MOE_SCHOOL_CURRICULUM_ID !=null && SchoolInfo.MOE_SCHOOL_CURRICULUM_ID.Contains(","))
            {
                SchoolInfo.MOE_SCHOOL_CURRICULUM_ID = SchoolInfo.MOE_SCHOOL_CURRICULUM_ID.Split(',')[1];
            }

            if (SchoolCodesDropDownList.SelectedValue == "All")
            {
               
                CurriculumsDropDownList.Enabled = true;
                SchoolGradesDropDownList.Items.Clear();
              
                List<MOE_SCHOOL_Model> schools = MOE_SCHOOL_Repository.GetSchools(NextYear, "false").Result;

                if ((uint)CultureInfo.CurrentUICulture.LCID == 1025)
                {
                    BindingUtility.LoadCurriculums( CurriculumsDropDownList, PSPESConstants.ArabicLanguage);
                BindingUtility.LoadSchools(schools, SchoolCodesDropDownList,PSPESConstants.ArabicLanguage);

                }
                else
                {
                    BindingUtility.LoadCurriculums( CurriculumsDropDownList, PSPESConstants.EnglishLanguage);
                BindingUtility.LoadSchools(schools, SchoolCodesDropDownList,PSPESConstants.EnglishLanguage);

                }
                //MOE_SCHOOL_Model SchoolInfo = MOE_SCHOOL_Repository.GetSchoolInfo(NextYear, SchoolCodesDropDownList.SelectedValue, "false").Result;


            }
            else
            {
                // List<schoolGrade> schoolGrades = MOE_SCHOOL_Repository.GetSchoolGrade(NextYear, SchoolCodesDropDownList.SelectedValue, "false").Result;
                #region New Grades from SeatCapacity Table to include Newly added Grades
                //tarek.elbeheidy 18.4.2018 => changed the way grades are loaded to show the newly added grades
                List<V_Schools_Grades_Ages> SchoolGradesAndAges = V_Schools_Grades_Ages_Repository.GetSchoolGradesAndAges(int.Parse(TermNameTextBox.Text), SchoolCodesDropDownList.SelectedValue).Result;
                if (SchoolGradesAndAges != null && SchoolGradesAndAges.Count > 0)
                {
                    List<V_Schools_Grades_Ages> SortedSchoolDataList = SchoolGradesAndAges.OrderBy(s => s.Weight).ToList();
                    if ((uint)CultureInfo.CurrentUICulture.LCID == 1025)
                    {
                        BindingUtility.LoadGradesFromSeatCapacity(SortedSchoolDataList, SchoolGradesDropDownList, PSPESConstants.ArabicLanguage);
                    }
                    else
                    {
                        BindingUtility.LoadGradesFromSeatCapacity(SortedSchoolDataList, SchoolGradesDropDownList, PSPESConstants.EnglishLanguage);
                    }
                }
                #endregion
                CurriculumsDropDownList.Items.Clear();
                List<CurriculumModel> Curriculums = new List<CurriculumModel>();
                if ((uint)CultureInfo.CurrentUICulture.LCID == 1025)
                {
                    //BindingUtility.LoadGrades(schoolGrades, SchoolGradesDropDownList,PSPESConstants.ArabicLanguage);
                   
                    Curriculums.Add(new CurriculumModel { CurriculumID = SchoolInfo.MOE_SCHOOL_CURRICULUM_ID, ArabicCurriculumName =SupportingDocsRepository.GetCurrilculmName(AllCurricullms,SchoolInfo.MOE_SCHOOL_CURRICULUM_ID).ArabicCurriculumName });

                    CurriculumsDropDownList.DataTextField = "ArabicCurriculumName";
                    CurriculumsDropDownList.DataValueField = "CurriculumID";


                }
                else
                {
                    //BindingUtility.LoadGrades(schoolGrades, SchoolGradesDropDownList, PSPESConstants.EnglishLanguage);
                   

                    Curriculums.Add(new CurriculumModel { CurriculumID = SchoolInfo.MOE_SCHOOL_CURRICULUM_ID, EnglishCurriculumName =SupportingDocsRepository.GetCurrilculmName(AllCurricullms,SchoolInfo.MOE_SCHOOL_CURRICULUM_ID).EnglishCurriculumName });
                    CurriculumsDropDownList.DataTextField = "EnglishCurriculumName";
                    CurriculumsDropDownList.DataValueField = "CurriculumID";

                }
                
                CurriculumsDropDownList.DataSource = Curriculums;

                CurriculumsDropDownList.DataBind();

                //Check if this school code added before
                //Check if the shcool FirstTime Login the insert it
                List<SupportingDocsModel> supportDocsForCurrentSchool = SupportingDocsRepository.GetSupportingDocs(SchoolInfo,new SupportingDocsModel { SchoolCode = SchoolCodesDropDownList.SelectedValue, Term = TermNameTextBox.Text, SeacrhByTermAndSchoolCode = true },true, isMinistryUser,!isMinistryUser, SPContext.Current.Site.Url).Result;
                int SupportingDocumentsCount = (SchoolGradesDropDownList.Items.Count - 1) * (DocumentTypesDropDownList.Items.Count-1);
                
                if (supportDocsForCurrentSchool.Count == 0 || supportDocsForCurrentSchool .Count!= SupportingDocumentsCount)
                {
                    List<DocumentTypeList> AllDocumentTypes = SupportingDocsRepository.GetDocumentTypeList(PSPESConstants.ArabicLanguage);
                    //Then Add all schools Document type for all grades
                    foreach (var DocumentType in AllDocumentTypes)
                    {
                        foreach (var schoolGrade in SchoolGradesAndAges)
                        {
                            var InsretNew = SupportingDocsRepository.Insert(new SupportingDocsModel { IsUpdateAllowed = false, CreateDate = DateTime.Now, CreatedBy = SPContext.Current.Site.RootWeb.CurrentUser.LoginName, CurriculumID = SchoolInfo.MOE_SCHOOL_CURRICULUM_ID, DocumentTypeID = DocumentType.DocumentTypeID, Grade = schoolGrade.MOE_SCHOOL_GRADE, IsRequiredForPSO = false, IsRequiredForSchool = false, ModifiedBy = SPContext.Current.Site.RootWeb.CurrentUser.LoginName, ModifiedDate = DateTime.Now, SchoolCode = SchoolInfo.MOE_SCHOOL_CODE, SchoolName = SchoolInfo.MOE_SCHOOL_NAME_ENG, Term = TermNameTextBox.Text, ArabicSchoolName = SchoolInfo.MOE_SCHOOL_NAME_ARA }).Result;
                        }
                    }
                }

            }
        }

        protected void TermNameTextBox_TextChanged(object sender, EventArgs e)
        {

            string NextYear = TermNameTextBox.Text;
            List<MOE_SCHOOL_Model> Schools = MOE_SCHOOL_Repository.GetSchools(NextYear, "false").Result;
            //MOE_SCHOOL_Model SchoolInfo = MOE_SCHOOL_Repository.GetSchoolInfo(NextYear, SchoolCodesDropDownList.SelectedValue, "false").Result;
            if ((uint)CultureInfo.CurrentUICulture.LCID == 1025)
            {
                BindingUtility.LoadSchools(Schools, SchoolCodesDropDownList,PSPESConstants.ArabicLanguage);

                BindingUtility.LoadCurriculums( CurriculumsDropDownList, PSPESConstants.ArabicLanguage);
            }
            else
            {
                BindingUtility.LoadCurriculums( CurriculumsDropDownList, PSPESConstants.EnglishLanguage);
                BindingUtility.LoadSchools(Schools, SchoolCodesDropDownList, PSPESConstants.EnglishLanguage);

            }

            // LoadGrades(SchoolInfo.schoolGrades);
        }

        protected void CurriculumsDropDownList_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (CurriculumsDropDownList.SelectedValue != "All")
            {


                string NextYear = TermNameTextBox.Text;

                List<MOE_SCHOOL_Model> Schools = MOE_SCHOOL_Repository.GetSchools(NextYear, "false").Result;
                if ((uint)CultureInfo.CurrentUICulture.LCID == 1025)
                {
                    BindingUtility.LoadSchools(Schools, SchoolCodesDropDownList, CurriculumsDropDownList.SelectedValue,PSPESConstants.ArabicLanguage);
                }
                else
                {
                    BindingUtility.LoadSchools(Schools, SchoolCodesDropDownList, CurriculumsDropDownList.SelectedValue, PSPESConstants.EnglishLanguage);

                }
            }
            else
            {
                string NextYear = TermNameTextBox.Text;
                List<MOE_SCHOOL_Model> Schools = MOE_SCHOOL_Repository.GetSchools(NextYear, "false").Result;
                if ((uint)CultureInfo.CurrentUICulture.LCID == 1025)
                {
                    BindingUtility.LoadSchools(Schools, SchoolCodesDropDownList, PSPESConstants.ArabicLanguage);

                }
                else
                {
                    BindingUtility.LoadSchools(Schools, SchoolCodesDropDownList, PSPESConstants.EnglishLanguage);

                }
            }
            
        }

        protected void PSORequiredCheckBox_CheckedChanged(object sender, EventArgs e)
        {

            foreach (RepeaterItem item in SupportingDocsRepeater.Items)
            {
                CheckBox pSORequiredCheckBox = (CheckBox)item.FindControl("PSORequiredRepeaterCheckBox");
                CheckBox schoolRequiredCheckBox = (CheckBox)item.FindControl("PSORequiredRepeaterCheckBox");

                TextBox termNameTextBox = (TextBox)item.FindControl("TermNameTextBox");
                TextBox schoolCode = (TextBox)item.FindControl("SchoolCode");
                HiddenField curriculumIDHiddenField = (HiddenField)item.FindControl("CurriculumIDHiddenField");
                TextBox gradeTextBox = (TextBox)item.FindControl("gradeTextBox");
                TextBox DocumentTypeIDTextBox = (TextBox)item.FindControl("DocumentTypeIDTextBox");
                TextBox CurriculumIDTextBox = (TextBox)item.FindControl("CurriculumIDTextBox");

                pSORequiredCheckBox.Checked = PSORequiredCheckBox.Checked;
                schoolRequiredCheckBox.Checked= PSORequiredCheckBox.Checked;
                HiddenField documentTypeIDHiddenField = (HiddenField)item.FindControl("DocumentTypeIDHiddenField");
                SupportingDocsModelSerializable supportingDocsModel = new SupportingDocsModelSerializable
                {
                    CreateDate = DateTime.Now,
                    CreatedBy = SPContext.Current.Site.RootWeb.CurrentUser.LoginName,
                    CurriculumID = CurriculumIDTextBox.Text,
                    DocumentTypeID = int.Parse(DocumentTypeIDTextBox.Text),
                    Grade = gradeTextBox.Text,
                    IsRequiredForPSO = PSORequiredCheckBox.Checked,
                    IsRequiredForSchool = PSORequiredCheckBox.Checked,
                    ModifiedBy = SPContext.Current.Site.RootWeb.CurrentUser.LoginName,
                    ModifiedDate = DateTime.Now,
                    SchoolCode = schoolCode.Text,
                    Term = termNameTextBox.Text,
                     IsUpdateAllowed=true
                };
                List<SupportingDocsModelSerializable> newSupportingDocsModelList = new List<SupportingDocsModelSerializable>();

                if (ViewState["SupportingDocsModelList"] == null)
                {

                    newSupportingDocsModelList.Add(supportingDocsModel);
                }
                else
                {
                    bool IsAlreadyAdded = false;
                    List<SupportingDocsModelSerializable> supportingDocsModelList = ViewState["SupportingDocsModelList"] as List<SupportingDocsModelSerializable>;
                    foreach (var itemSupportingDocsModel in supportingDocsModelList)
                    {
                        if (itemSupportingDocsModel.Term == supportingDocsModel.Term && itemSupportingDocsModel.CurriculumID == supportingDocsModel.CurriculumID && itemSupportingDocsModel.DocumentTypeID == supportingDocsModel.DocumentTypeID && itemSupportingDocsModel.Grade == supportingDocsModel.Grade && itemSupportingDocsModel.SchoolCode == supportingDocsModel.SchoolCode)
                        {

                            newSupportingDocsModelList.Add(supportingDocsModel);
                            IsAlreadyAdded = true;
                        }
                        else
                        {
                            newSupportingDocsModelList.Add(itemSupportingDocsModel);
                            if (!IsAlreadyAdded)
                            {
                                newSupportingDocsModelList.Add(supportingDocsModel);
                                IsAlreadyAdded = true;
                            }

                        }
                    }
                }

                ViewState["SupportingDocsModelList"] = newSupportingDocsModelList;

            }
        }

        protected void SchoolRequiredCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            foreach (RepeaterItem item in SupportingDocsRepeater.Items)
            {

                CheckBox pSORequiredCheckBox = (CheckBox)item.FindControl("PSORequiredRepeaterCheckBox");
                CheckBox schoolRequiredCheckBox = (CheckBox)item.FindControl("SchoolRequiredRepeaterCheckBox");

                TextBox termNameTextBox = (TextBox)item.FindControl("TermNameTextBox");
                TextBox schoolCode = (TextBox)item.FindControl("SchoolCode");
                HiddenField curriculumIDHiddenField = (HiddenField)item.FindControl("CurriculumIDHiddenField");
                TextBox gradeTextBox = (TextBox)item.FindControl("gradeTextBox");
                TextBox DocumentTypeIDTextBox = (TextBox)item.FindControl("DocumentTypeIDTextBox");
                TextBox CurriculumIDTextBox = (TextBox)item.FindControl("CurriculumIDTextBox");

                //pSORequiredCheckBox.Checked = PSORequiredCheckBox.Checked;
                schoolRequiredCheckBox.Checked = SchoolRequiredCheckBox.Checked;

                HiddenField documentTypeIDHiddenField = (HiddenField)item.FindControl("DocumentTypeIDHiddenField");
                SupportingDocsModelSerializable supportingDocsModel = new SupportingDocsModelSerializable { CreateDate = DateTime.Now, CreatedBy = SPContext.Current.Site.RootWeb.CurrentUser.LoginName, CurriculumID = CurriculumIDTextBox.Text, DocumentTypeID = int.Parse(DocumentTypeIDTextBox.Text), Grade = gradeTextBox.Text ,  IsRequiredForSchool = SchoolRequiredCheckBox.Checked,IsRequiredForPSO=pSORequiredCheckBox.Checked, IsUpdateAllowed=true, ModifiedBy = SPContext.Current.Site.RootWeb.CurrentUser.LoginName, ModifiedDate = DateTime.Now, SchoolCode = schoolCode.Text, Term = termNameTextBox.Text };
                List<SupportingDocsModelSerializable> newSupportingDocsModelList = new List<SupportingDocsModelSerializable>();

                if (ViewState["SupportingDocsModelList"] == null)
                {

                    newSupportingDocsModelList.Add(supportingDocsModel);
                }
                else
                {
                    bool IsAlreadyAdded = false;
                    List<SupportingDocsModelSerializable> supportingDocsModelList = ViewState["SupportingDocsModelList"] as List<SupportingDocsModelSerializable>;
                    foreach (var itemSupportingDocsModel in supportingDocsModelList)
                    {
                        if (itemSupportingDocsModel.Term == supportingDocsModel.Term && itemSupportingDocsModel.CurriculumID == supportingDocsModel.CurriculumID && itemSupportingDocsModel.DocumentTypeID == supportingDocsModel.DocumentTypeID && itemSupportingDocsModel.Grade == supportingDocsModel.Grade && itemSupportingDocsModel.SchoolCode == supportingDocsModel.SchoolCode)
                        {

                            newSupportingDocsModelList.Add(supportingDocsModel);
                            IsAlreadyAdded = true;
                        }
                        else
                        {
                            newSupportingDocsModelList.Add(itemSupportingDocsModel);
                            if (!IsAlreadyAdded)
                            {
                                newSupportingDocsModelList.Add(supportingDocsModel);
                                IsAlreadyAdded = true;
                            }

                        }
                    }
                }

                ViewState["SupportingDocsModelList"] = newSupportingDocsModelList;


            }
        }

        protected void SaveButton_Click(object sender, EventArgs e)
        {
            bool isMinistryUser = false;
            bool isSchoolUser = false;
            isMinistryUser = SharePointUtilityRepository.IsUserInGroup(PSPESConstants.PrivateSchoolOfficeSharePointGroup, SPContext.Current.Site.RootWeb.CurrentUser.LoginName);
            isSchoolUser = SharePointUtilityRepository.IsUserInGroup(PSPESConstants.PrivateSchoolUsersSharePointGroup, SPContext.Current.Site.RootWeb.CurrentUser.LoginName);

            #region NewCode Alhanafi 3-3-2018 to get the value from the grid directly 
            int Counter = 0;
            bool Checker = false;
            string confirmationMessage = "";


            foreach (RepeaterItem item in SupportingDocsRepeater.Items)
            {
               
                CheckBox pSORequiredCheckBox = (CheckBox)item.FindControl("PSORequiredRepeaterCheckBox");
                CheckBox schoolRequiredCheckBox = (CheckBox)item.FindControl("SchoolRequiredRepeaterCheckBox");

                TextBox termNameTextBox = (TextBox)item.FindControl("TermNameTextBox");
                TextBox schoolCode = (TextBox)item.FindControl("SchoolCode");
                HiddenField curriculumIDHiddenField = (HiddenField)item.FindControl("CurriculumIDHiddenField");
                TextBox gradeTextBox = (TextBox)item.FindControl("gradeTextBox");
                TextBox DocumentTypeIDTextBox = (TextBox)item.FindControl("DocumentTypeIDTextBox");
                TextBox CurriculumIDTextBox = (TextBox)item.FindControl("CurriculumIDTextBox");


                HiddenField documentTypeIDHiddenField = (HiddenField)item.FindControl("DocumentTypeIDHiddenField");

                
                SupportingDocsModelSerializable supportingDocsModel = new SupportingDocsModelSerializable { CreateDate = DateTime.Now, CreatedBy = SPContext.Current.Site.RootWeb.CurrentUser.LoginName, CurriculumID = CurriculumIDTextBox.Text, DocumentTypeID = int.Parse(DocumentTypeIDTextBox.Text), Grade = gradeTextBox.Text, IsRequiredForPSO = pSORequiredCheckBox.Checked, IsUpdateAllowed = true, IsRequiredForSchool = schoolRequiredCheckBox.Checked, ModifiedBy = SPContext.Current.Site.RootWeb.CurrentUser.LoginName, ModifiedDate = DateTime.Now, SchoolCode = schoolCode.Text, Term = termNameTextBox.Text };
                if (isMinistryUser)
                {
                    
                        supportingDocsModel.IsRequiredForSchool = pSORequiredCheckBox.Checked;
                    
                }
               


                try
                {
                    var InsretNew = SupportingDocsRepository.Insert(new SupportingDocsModel { IsUpdateAllowed = true, CreateDate = DateTime.Now, CreatedBy = SPContext.Current.Site.RootWeb.CurrentUser.LoginName, CurriculumID = supportingDocsModel.CurriculumID, DocumentTypeID = supportingDocsModel.DocumentTypeID, Grade = supportingDocsModel.Grade, IsRequiredForPSO = supportingDocsModel.IsRequiredForPSO, IsRequiredForSchool = supportingDocsModel.IsRequiredForSchool, ModifiedBy = SPContext.Current.Site.RootWeb.CurrentUser.LoginName, ModifiedDate = DateTime.Now, SchoolCode = supportingDocsModel.SchoolCode, SchoolName = supportingDocsModel.SchoolName, Term = supportingDocsModel.Term, ArabicSchoolName = supportingDocsModel.ArabicSchoolName }).Result;
                    Counter++;
                    Checker = true;
                  
                }
                catch (Exception ex)
                {


                    string message = ex.Message;
                }


            }
            if (Checker)
            {
                LoadSupportingDocument();

                if ((uint)CultureInfo.CurrentUICulture.LCID == 1025)
                {

                    confirmationMessage = string.Format("{0} ", PSPESConstants.ArabicSupportingDocConfirmation);
                }
                else
                {

                    confirmationMessage = string.Format("{0} ", PSPESConstants.EnglishSupportingDocConfirmation);

                }


            }
            else
            {
                if ((uint)CultureInfo.CurrentUICulture.LCID == 1025)
                {

                    confirmationMessage = string.Format("{0}", PSPESConstants.ArabicSupportingDocError);
                }
                else
                {

                    confirmationMessage = string.Format("{0}", PSPESConstants.EnglishSupportingDocError);

                }
            }
           

            CustomMessageBoxHelper.Show(this.Page, confirmationMessage);

            #endregion


            #region OldCodeWith Postback

            //if (ViewState["SupportingDocsModelList"] == null)
            //{
            //}
            //else
            //{
            //    List<SupportingDocsModelSerializable> SupportingDocsModelList = ViewState["SupportingDocsModelList"] as List<SupportingDocsModelSerializable>;



            //    if (SupportingDocsModelList.Count > 0)
            //    {

            //       // bool Checker = false;

            //        try
            //        {
            //            foreach (SupportingDocsModelSerializable item in SupportingDocsModelList)
            //            {

            //                try
            //                {
            //                    var InsretNew = SupportingDocsRepository.Insert(new SupportingDocsModel { IsUpdateAllowed=true, CreateDate = DateTime.Now, CreatedBy = SPContext.Current.Site.RootWeb.CurrentUser.LoginName, CurriculumID = item.CurriculumID, DocumentTypeID = item.DocumentTypeID, Grade = item.Grade, IsRequiredForPSO = item.IsRequiredForPSO, IsRequiredForSchool = item.IsRequiredForSchool, ModifiedBy = SPContext.Current.Site.RootWeb.CurrentUser.LoginName, ModifiedDate = DateTime.Now, SchoolCode = item.SchoolCode, SchoolName = item.SchoolName, Term = item.Term, ArabicSchoolName = item.ArabicSchoolName}).Result;
            //                    Counter++;
            //                    Checker = true;
            //                }
            //                catch (Exception ex)
            //                {
                                

            //                    string message = ex.Message;
            //                }
            //            }
            //        }



            //        catch (Exception ex) { string message = ex.Message; }
            //        //string confirmationMessage = "";
            //        if (Checker)
            //        {
            //            LoadSupportingDocument();

            //            if ((uint)CultureInfo.CurrentUICulture.LCID == 1025)
            //            {

            //                confirmationMessage = string.Format("{0} ", PSPESConstants.ArabicSupportingDocConfirmation);
            //            }
            //            else
            //            {

            //                confirmationMessage = string.Format("{0} ", PSPESConstants.EnglishSupportingDocConfirmation);

            //            }


            //        }
            //        else
            //        {
            //            if ((uint)CultureInfo.CurrentUICulture.LCID == 1025)
            //            {

            //                confirmationMessage = string.Format("{0}", PSPESConstants.ArabicSupportingDocError);
            //            }
            //            else
            //            {

            //                confirmationMessage = string.Format("{0}", PSPESConstants.EnglishSupportingDocError);

            //            }
            //        }
            //        ViewState["SupportingDocsModelList"] = null;

            //        CustomMessageBoxHelper.Show(this.Page, confirmationMessage);


            //    }
            //}

            #endregion

        }

        protected void SchoolRequiredRepeaterCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            RepeaterItem item = (sender as CheckBox).Parent as RepeaterItem;
            CheckBox pSORequiredCheckBox = (CheckBox)item.FindControl("PSORequiredRepeaterCheckBox");
            CheckBox schoolRequiredCheckBox = (CheckBox)item.FindControl("SchoolRequiredRepeaterCheckBox");

            TextBox termNameTextBox = (TextBox)item.FindControl("TermNameTextBox");
            TextBox schoolCode = (TextBox)item.FindControl("SchoolCode");
            HiddenField curriculumIDHiddenField = (HiddenField)item.FindControl("CurriculumIDHiddenField");
            TextBox gradeTextBox = (TextBox)item.FindControl("gradeTextBox");
            TextBox DocumentTypeIDTextBox = (TextBox)item.FindControl("DocumentTypeIDTextBox");
            TextBox CurriculumIDTextBox = (TextBox)item.FindControl("CurriculumIDTextBox");


            HiddenField documentTypeIDHiddenField = (HiddenField)item.FindControl("DocumentTypeIDHiddenField");
            SupportingDocsModelSerializable supportingDocsModel =   new SupportingDocsModelSerializable { CreateDate = DateTime.Now, CreatedBy = SPContext.Current.Site.RootWeb.CurrentUser.LoginName, CurriculumID = CurriculumIDTextBox.Text, DocumentTypeID = int.Parse(DocumentTypeIDTextBox.Text), Grade = gradeTextBox.Text,  IsRequiredForPSO= pSORequiredCheckBox.Checked, IsUpdateAllowed=true, IsRequiredForSchool = schoolRequiredCheckBox.Checked, ModifiedBy = SPContext.Current.Site.RootWeb.CurrentUser.LoginName, ModifiedDate = DateTime.Now, SchoolCode = schoolCode.Text, Term = termNameTextBox.Text };
            List<SupportingDocsModelSerializable> newSupportingDocsModelList = new List<SupportingDocsModelSerializable>();

            if (ViewState["SupportingDocsModelList"] == null)
            {

                newSupportingDocsModelList.Add(supportingDocsModel);
            }
            else
            {
                bool IsAlreadyAdded = false;
                List<SupportingDocsModelSerializable> supportingDocsModelList = ViewState["SupportingDocsModelList"] as List<SupportingDocsModelSerializable>;
                foreach (var itemSupportingDocsModel in supportingDocsModelList)
                {
                    if (itemSupportingDocsModel.Term == supportingDocsModel.Term && itemSupportingDocsModel.CurriculumID == supportingDocsModel.CurriculumID && itemSupportingDocsModel.DocumentTypeID == supportingDocsModel.DocumentTypeID && itemSupportingDocsModel.Grade == supportingDocsModel.Grade && itemSupportingDocsModel.SchoolCode == supportingDocsModel.SchoolCode)
                    {

                        newSupportingDocsModelList.Add(supportingDocsModel);
                        IsAlreadyAdded = true;
                    }
                    else
                    {
                        newSupportingDocsModelList.Add(itemSupportingDocsModel);
                        if (!IsAlreadyAdded)
                        {
                            newSupportingDocsModelList.Add(supportingDocsModel);
                            IsAlreadyAdded = true;
                        }

                    }
                }
            }

            ViewState["SupportingDocsModelList"] = newSupportingDocsModelList;


        }

        protected void PSORequiredRepeaterCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            RepeaterItem item = (sender as CheckBox).Parent as RepeaterItem;
            CheckBox pSORequiredCheckBox = (CheckBox)item.FindControl("PSORequiredRepeaterCheckBox");
            CheckBox schoolRequiredCheckBox = (CheckBox)item.FindControl("PSORequiredRepeaterCheckBox");

            TextBox termNameTextBox = (TextBox)item.FindControl("TermNameTextBox");
            TextBox schoolCode = (TextBox)item.FindControl("SchoolCode");
            HiddenField curriculumIDHiddenField = (HiddenField)item.FindControl("CurriculumIDHiddenField");
            TextBox gradeTextBox = (TextBox)item.FindControl("gradeTextBox");
            TextBox DocumentTypeIDTextBox = (TextBox)item.FindControl("DocumentTypeIDTextBox");
            TextBox CurriculumIDTextBox = (TextBox)item.FindControl("CurriculumIDTextBox");
            schoolRequiredCheckBox.Checked = pSORequiredCheckBox.Checked;

            HiddenField documentTypeIDHiddenField = (HiddenField)item.FindControl("DocumentTypeIDHiddenField");
            SupportingDocsModelSerializable supportingDocsModel = new SupportingDocsModelSerializable
            { CreateDate = DateTime.Now,
                CreatedBy = SPContext.Current.Site.RootWeb.CurrentUser.LoginName,
                CurriculumID = CurriculumIDTextBox.Text,
                DocumentTypeID = int.Parse(DocumentTypeIDTextBox.Text),
                Grade = gradeTextBox.Text,
                IsRequiredForPSO = pSORequiredCheckBox.Checked,
                IsRequiredForSchool = pSORequiredCheckBox.Checked,
                ModifiedBy = SPContext.Current.Site.RootWeb.CurrentUser.LoginName,
                ModifiedDate = DateTime.Now,
                SchoolCode = schoolCode.Text,
                Term = termNameTextBox.Text,
                 IsUpdateAllowed=true
            };
            List<SupportingDocsModelSerializable> newSupportingDocsModelList = new List<SupportingDocsModelSerializable>();
           
            if (ViewState["SupportingDocsModelList"]==null)
            {

                newSupportingDocsModelList.Add(supportingDocsModel);
            }
            else
            {
                bool IsAlreadyAdded = false;
                List<SupportingDocsModelSerializable> supportingDocsModelList = ViewState["SupportingDocsModelList"] as List<SupportingDocsModelSerializable>;
                foreach (var itemSupportingDocsModel in supportingDocsModelList)
                {
                    if (itemSupportingDocsModel.Term== supportingDocsModel.Term &&itemSupportingDocsModel.CurriculumID==supportingDocsModel.CurriculumID&& itemSupportingDocsModel.DocumentTypeID==supportingDocsModel.DocumentTypeID&&itemSupportingDocsModel.Grade==supportingDocsModel.Grade &&itemSupportingDocsModel.SchoolCode==supportingDocsModel.SchoolCode)
                    {

                        newSupportingDocsModelList.Add(supportingDocsModel);
                        IsAlreadyAdded = true;
                    }
                    else
                    {
                        newSupportingDocsModelList.Add(itemSupportingDocsModel);
                        if (!IsAlreadyAdded)
                        {
                            newSupportingDocsModelList.Add(supportingDocsModel);
                            IsAlreadyAdded = true;
                        }

                    }
                }
            }

            ViewState["SupportingDocsModelList"] = newSupportingDocsModelList;



        }

        protected void FullTermNameTextBox_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
