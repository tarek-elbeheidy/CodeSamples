using Microsoft.SharePoint;
using MOEHE.PSPES.Models;
using MOEHE.PSPES.WebAPI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.UI.WebControls;

namespace MOEHE.PSPES.Repository
{


    public class BindingUtility
    {






        public static  int getGradeMapping(string gradeCode)
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
        public static void LoadApplicationsData(Repeater repeater, SearchApplicationPageFilters   searchApplicationPageFilters)
        {

            try
            {
                List<ApplicationDataModel> applicationsModels = MOE_APPLICATION_DATA_Repository.Get(searchApplicationPageFilters).Result;
                string sss = "";
                List<ApplicationDataModel> sortedApplications = applicationsModels.OrderBy(s => getGradeMapping(s.Grade)).ThenBy(x => x.MOE_WAITLIST_NUMBER).ToList();
                repeater.DataSource = sortedApplications;
                repeater.DataBind();
            }
            catch (Exception ex)
            { string message = ex.Message; }

        }


        public static void LoadSupportingDocs(Repeater repeater,SupportingDocsModel supportingDocsModel, MOE_SCHOOL_Model SchoolInfo,bool ShowEnglish,bool IsMinistryUser,bool IsSchoolUser,string CurrentSiteURL)
        {

            try
            {
                List<SupportingDocsModel> SupportingDocs= SupportingDocsRepository.GetSupportingDocs(SchoolInfo,supportingDocsModel, ShowEnglish, IsMinistryUser,IsSchoolUser, CurrentSiteURL).Result;
                repeater.DataSource = SupportingDocs;
                repeater.DataBind();
            }
            catch (Exception ex)
            { string message = ex.Message; }

        }

        public static void LoadRequiredSupportingDocs(GridView gridView, SupportingDocsModel supportingDocsModel, MOE_SCHOOL_Model SchoolInfo, bool ShowEnglish, bool IsMinistryUser,bool IsSchoolUser, string CurrentSiteURL)
        {

            try
            {
                List<SupportingDocsModel> SupportingDocs = SupportingDocsRepository.GetSupportingDocs(SchoolInfo, supportingDocsModel, ShowEnglish, IsMinistryUser,IsSchoolUser, CurrentSiteURL).Result;
                List<SupportingDocsModel> SupportingDocsForBind = SupportingDocs.Where(D => D.IsRequiredForSchool == true).Select(D => D).ToList();
                gridView.DataSource = SupportingDocsForBind;
                gridView.DataBind();
            }
            catch (Exception ex)
            { string message = ex.Message; }

        }

        public static void LoadRequiredSupportingDocs(GridView gridView,string ApplicantReferenceNumber,decimal QID, SupportingDocsModel supportingDocsModel, bool ShowEnglish, bool IsMinistryUser, string CurrentSiteURL)
        {

            try
            {
                List<SupportingDocsModel> SupportingDocs = SupportingDocsRepository.GetSupportingDocs( supportingDocsModel, ShowEnglish, IsMinistryUser, CurrentSiteURL).Result;

               
                List<SupportingDocsModel> SupportingDocsForBind = SupportingDocs.Where(D => D.IsRequiredForSchool == true).Select(D => new SupportingDocsModel{ DocumentTypeID=D.DocumentTypeID,DoumentLocation=GetDocumentLocatoin(D.DocumentTypeID,ApplicantReferenceNumber,QID).DocumentLocationPath, ArabicCurriculum=D.ArabicCurriculum, ArabicDocumentType=D.ArabicDocumentType, ArabicSchoolName=D.ArabicSchoolName, CreateDate=D.CreateDate, CreatedBy=D.CreatedBy,
                 Curriculum=D.Curriculum, CurriculumID=D.CurriculumID, EnableSchoolRequired=D.EnableSchoolRequired, EnglishDocumentType=D.EnglishDocumentType, Grade=D.Grade, ID=D.ID, IsRequiredForPSO=D.IsRequiredForPSO, IsRequiredForSchool=D.IsRequiredForSchool, ModifiedBy=D.ModifiedBy, ModifiedDate=D.ModifiedDate, SchoolCode=D.SchoolCode,
                 SchoolName=D.SchoolName, SeacrhByAll=D.SeacrhByAll, IsUploadedArabic=(D.ShowArabic && GetDocumentLocatoin(D.DocumentTypeID, ApplicantReferenceNumber, QID).IsUploaded), IsUploadedEnglish=(D.ShowEnglish && GetDocumentLocatoin(D.DocumentTypeID, ApplicantReferenceNumber, QID).IsUploaded), ShowArabic=D.ShowArabic,ShowEnglish=D.ShowEnglish , Term=D.Term}).ToList();
                gridView.DataSource = SupportingDocsForBind;
                gridView.DataBind();
            }
            catch (Exception ex)
            { string message = ex.Message; }

        }

        public static void LoadMessageHistory(GridView gridView, string ApplicantReferenceNumber,string MessageType,string MobileNumber,string QID)
        {

            try
            {
                List<SMSHistoryModel> SMSMessageHistoryList = SMSHistoryRepository.GetSMSHistory(ApplicantReferenceNumber,MessageType,MobileNumber,QID).Result;


               
                gridView.DataSource = SMSMessageHistoryList;
                gridView.DataBind();
            }
            catch (Exception ex)
            { string message = ex.Message; }

        }

        public static void LoadMessageHistory(GridView gridView, string ApplicantReferenceNumber, string MessageType,string messageTitle,string MobileNumber, string QID)
        {

            try
            {
                List<SMSHistoryModel> SMSMessageHistoryList = SMSHistoryRepository.GetSMSHistory(ApplicantReferenceNumber, MessageType, messageTitle,MobileNumber,QID).Result;



                gridView.DataSource = SMSMessageHistoryList;
                gridView.DataBind();
            }
            catch (Exception ex)
            { string message = ex.Message; }

        }

        public static DocumentLocation GetDocumentLocatoin(int? DocumentTypeID,string ApplicantReferenceNumber,decimal QID)
        {
            string DocumentLocationPath = "";
            DocumentLocation documentLocation = new DocumentLocation();
            bool IsUploaded = false;
            try
            {
                List<MOE_APPL_SUPPORT_DOCS_Model> CurrentSupportingDocs = MOE_APPL_SUPPORT_DOCS_Repository.GetBy(ApplicantReferenceNumber, QID.ToString()).Result;

                DocumentLocationPath = CurrentSupportingDocs.Where(D => D.MOE_DOCUMENT_TYPE_ID == DocumentTypeID.ToString()).Select(D => D).FirstOrDefault().MOE_DOCUMENT_LOCATION;
            }
            catch { }
            if (DocumentLocationPath != "")
            {
                IsUploaded = true;
            }
            documentLocation.DocumentLocationPath = DocumentLocationPath;
            documentLocation.IsUploaded = IsUploaded;

            return documentLocation;
        }

        public static void LoadGrades(List<schoolGrade> Grades,DropDownList GradeDropDownList,string Lang)
        {

            GradeDropDownList.Items.Clear();
            if (Lang == PSPESConstants.ArabicLanguage)
            {
                GradeDropDownList.Items.Add(new ListItem { Text = "اختر", Value = "All" });

            }
            else
            {
                GradeDropDownList.Items.Add(new ListItem { Text = "Select", Value = "All" });

            }
            GradeDropDownList.AppendDataBoundItems = true;
            GradeDropDownList.DataSource = Grades;
            GradeDropDownList.DataTextField = "Grade";
            GradeDropDownList.DataValueField = "Grade";
            GradeDropDownList.DataBind();


        }

        public static void LoadGrades(List<schoolGrade> Grades, CheckBoxList chkLstGrades, string Lang)
        {

            chkLstGrades.Items.Clear();

            //chkLstGrades.AppendDataBoundItems = true;
            chkLstGrades.DataSource = Grades;
            chkLstGrades.DataTextField = "Grade";
            chkLstGrades.DataValueField = "Grade";
            chkLstGrades.DataBind();


        }

        public static void LoadGradesFromSeatCapacity(List<PSPES.Models.V_Schools_Grades_Ages> Grades, DropDownList GradeDropDownList, string Lang)
        {

            GradeDropDownList.Items.Clear();
            if (Lang == PSPESConstants.ArabicLanguage)
            {
                GradeDropDownList.Items.Add(new ListItem { Text = "اختر", Value = "All" });
                GradeDropDownList.DataTextField = "MOE_GRADE_DESC_ARA";

            }
            else
            {
                GradeDropDownList.Items.Add(new ListItem { Text = "Select", Value = "All" });
                GradeDropDownList.DataTextField = "MOE_GRADE_DESC_ENG";

            }
            GradeDropDownList.AppendDataBoundItems = true;
            GradeDropDownList.DataSource = Grades;
           
            GradeDropDownList.DataValueField = "MOE_SCHOOL_GRADE";
            GradeDropDownList.DataBind();


        }
        public static void LoadNationalities(List<MOI_COUNTRY_MAP> Countries, ListBox lstNationalities, string Lang)
        {
            string textField = "";
            if (Lang == PSPESConstants.ArabicLanguage)
            {
                textField = "COUNTRY_ARA";

            }
            else
            {
                textField = "COUNTRY";

            }
            lstNationalities.Items.Clear();

            //chkLstGrades.AppendDataBoundItems = true;
            lstNationalities.DataSource = Countries;
            lstNationalities.DataTextField = textField;
            lstNationalities.DataValueField = "MOI_COUNTRY_CODE";
            lstNationalities.DataBind();


        }


        public static void LoadTerms(DropDownList TermDropDownList ,string Lang)
        {
            // List<TermModel> AllTerms = TermRepository.GetTerms().Result;
            List<TermModel> AllTerms = TermRepository.GetTerms().Result.Where(s => s.ACADEMIC_YEAR_DESC == "PREENROLLMENT").Select(d => d).ToList();
            TermDropDownList.Items.Clear();
            //if (Lang == PSPESConstants.ArabicLanguage)
            //{
            //    TermDropDownList.Items.Add(new ListItem { Text = "الكل", Value = "0" });

            //}
            //else
            //{
            //    TermDropDownList.Items.Add(new ListItem { Text = "All", Value = "0" });

            //}
            TermDropDownList.AppendDataBoundItems = true;
            TermDropDownList.DataSource = AllTerms;
            TermDropDownList.DataTextField = "TermName";
            TermDropDownList.DataValueField = "TermCode";
            TermDropDownList.DataBind();


        }



        public static void LoadApplicationStatus(DropDownList ApplicationStatusDropDownList, string Lang)
        {
            
            ApplicationStatusDropDownList.Items.Clear();
            if (Lang == PSPESConstants.ArabicLanguage)
            {
                ApplicationStatusDropDownList.Items.Add(new ListItem { Text = "قائمة الانتظار", Value = "0" });
                ApplicationStatusDropDownList.Items.Add(new ListItem { Text = "تم استكمال الطلب", Value = "1" });
            }
            else
            {
                ApplicationStatusDropDownList.Items.Add(new ListItem { Text = "Waiting List", Value = "0" });
                ApplicationStatusDropDownList.Items.Add(new ListItem { Text = "Finalized Application", Value = "1" });
            }
        }




        public static void LoadDocumentType(DropDownList DocumentTypeDropDownList,string Lang)
        {
            using (SPSite CurrentSite=new SPSite(SPContext.Current.Site.Url))
            {
                using (SPWeb web=CurrentSite.OpenWeb())
                {
                    DocumentTypeDropDownList.Items.Clear();
                    DocumentTypeDropDownList.Items.Add(new ListItem { Text = "All", Value = "0" });
                    DocumentTypeDropDownList.AppendDataBoundItems = true;
                    SPList list = web.Lists["documentTypeList"];
                    List<DocumentTypeList> AllDocumentTypes = new List<DocumentTypeList>();
                    foreach (SPListItem item in list.Items)
                    {
                        if (Convert.ToBoolean(item["DocumentTypeStatus"]))
                        {
                            if (Lang==PSPESConstants.ArabicLanguage)
                            {
                                AllDocumentTypes.Add( new DocumentTypeList { DocumentTypeName = item["ArabicNameDocumentType"].ToString(), DocumentTypeID = int.Parse(item["ID"].ToString()) });
                            }
                            else
                            {
                                AllDocumentTypes.Add(new DocumentTypeList { DocumentTypeName = item["EnglishNameDocumentType"].ToString(), DocumentTypeID =int.Parse( item["ID"].ToString()) });

     

                            }
                        }
                    }
                    DocumentTypeDropDownList.DataSource = AllDocumentTypes;
                    DocumentTypeDropDownList.DataTextField = "DocumentTypeName";
                    DocumentTypeDropDownList.DataValueField = "DocumentTypeID";

                    DocumentTypeDropDownList.DataBind();
                }

            }
            
           


        }

        public static void LoadSchools(List<MOE_SCHOOL_Model> Schools, DropDownList SchoolCodesDropDownList,string Lang)
        {
            SchoolCodesDropDownList.Items.Clear();
         

            List<SchoolModel> SchoolsForBind = new List<SchoolModel>();
            if (Lang == PSPESConstants.EnglishLanguage)
            {
                SchoolCodesDropDownList.Items.Add(new ListItem {Text = "Select", Value = "All" });
                SchoolCodesDropDownList.AppendDataBoundItems = true;
                SchoolsForBind = Schools.Select(p => new SchoolModel { ShcoolCode = p.MOE_SCHOOL_CODE, EnglishShcoolName = string.Format("{0} - {1}", p.MOE_SCHOOL_NAME_ENG, p.MOE_SCHOOL_CODE) })
                                   .Distinct().ToList();
                SchoolCodesDropDownList.DataTextField = "EnglishShcoolName";
                SchoolCodesDropDownList.DataValueField = "ShcoolCode";
            }
            else
            {
                SchoolCodesDropDownList.Items.Add(new ListItem { Text = "اختر", Value = "All" });
                SchoolCodesDropDownList.AppendDataBoundItems = true;
                SchoolsForBind = Schools.Select(p => new SchoolModel { ShcoolCode = p.MOE_SCHOOL_CODE, ArabicShcoolName = string.Format("{0} - {1}", p.MOE_SCHOOL_CODE, p.MOE_SCHOOL_NAME_ARA) })
                                .Distinct().ToList();
                SchoolCodesDropDownList.DataTextField = "ArabicShcoolName";
                SchoolCodesDropDownList.DataValueField = "ShcoolCode";
            }

            SchoolCodesDropDownList.DataSource = SchoolsForBind;
     
            SchoolCodesDropDownList.DataBind();
        }
        public static void LoadSchools(List<MOE_SCHOOL_Model> Schools, DropDownList SchoolCodesDropDownList,string Curriculum,string Lang)
        {
            SchoolCodesDropDownList.Items.Clear();
         
            List<SchoolModel> uniqueSchools = new List<SchoolModel>();
            if (Lang==PSPESConstants.EnglishLanguage)
            {
                SchoolCodesDropDownList.Items.Add(new ListItem { Text = "All", Value = "All" });
                SchoolCodesDropDownList.AppendDataBoundItems = true;
                uniqueSchools = Schools.Where(P => P.MOE_SCHOOL_CURRICULUM_ID == Curriculum).Select(p => new SchoolModel { ShcoolCode = p.MOE_SCHOOL_CODE, EnglishShcoolName = string.Format("{0} - {1}", p.MOE_SCHOOL_NAME_ENG, p.MOE_SCHOOL_CODE) })
                                .Distinct().ToList();
                SchoolCodesDropDownList.DataTextField = "EnglishShcoolName";
                SchoolCodesDropDownList.DataValueField = "ShcoolCode";
            }
            else
            {
                SchoolCodesDropDownList.Items.Add(new ListItem { Text = "الكل", Value = "All" });
                SchoolCodesDropDownList.AppendDataBoundItems = true;
                uniqueSchools = Schools.Where(P => P.MOE_SCHOOL_CURRICULUM_ID == Curriculum).Select(p => new SchoolModel { ShcoolCode = p.MOE_SCHOOL_CODE, ArabicShcoolName = string.Format("{0} - {1}", p.MOE_SCHOOL_CODE, p.MOE_SCHOOL_NAME_ARA) })
                                .Distinct().ToList();
                SchoolCodesDropDownList.DataTextField = "ArabicShcoolName";
                SchoolCodesDropDownList.DataValueField = "ShcoolCode";
            }
           
            SchoolCodesDropDownList.DataSource = uniqueSchools;

          
            SchoolCodesDropDownList.DataBind();
        }
        public static void LoadCurriculums(DropDownList CurriculumsDropDownList,string Lang)
        {
            List<ListOfValues_Model> AllCurricullms = ListOfValues_Repository.GetListOfValues(PSPESConstants.CurriculumsCodesetID).Result;
            CurriculumsDropDownList.Items.Clear();
         
            if (Lang==PSPESConstants.ArabicLanguage)
            {

                CurriculumsDropDownList.Items.Add(new ListItem { Text = "الكل", Value = "All" });
                CurriculumsDropDownList.AppendDataBoundItems = true;
                CurriculumsDropDownList.DataTextField = "DescriptionArabic";
                CurriculumsDropDownList.DataValueField = "ID";

            }
            else
            {
                CurriculumsDropDownList.Items.Add(new ListItem { Text = "All", Value = "All" });
                CurriculumsDropDownList.AppendDataBoundItems = true;
                CurriculumsDropDownList.DataTextField = "DescriptionEnglish";
                CurriculumsDropDownList.DataValueField = "ID";
            }
           
            CurriculumsDropDownList.DataSource = AllCurricullms;

            //CurriculumsDropDownList.DataTextField = "MOE_SCHOOL_CURRICULUM";
            //CurriculumsDropDownList.DataValueField = "MOE_SCHOOL_CURRICULUM";
            CurriculumsDropDownList.DataBind();
        }
    }
    public class DocumentLocation
    {
        public string DocumentLocationPath { get; set; }
        public bool IsUploaded { get; set; }

    }

    public class SchoolModel
    {
        public string EnglishShcoolName { get; set; }
        public string ArabicShcoolName{ get; set; }

        public string ShcoolCode { get; set; }
    }

    public class CurriculumModel
    {
        public string CurriculumID { get; set; }

        public string EnglishCurriculumName { get; set; }
        public string ArabicCurriculumName { get; set; }

    }

}
