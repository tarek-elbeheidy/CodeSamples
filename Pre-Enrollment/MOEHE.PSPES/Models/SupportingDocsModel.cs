using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOEHE.PSPES.Models
{
   public class SupportingDocsModel
    {
        public int ID { get; set; }
        public string Term { get; set; }
        public string SchoolCode { get; set; }
        public string SchoolName { get; set; }
        public string ArabicSchoolName { get; set; }
        public string DoumentLocation { get; set; }
        public string Curriculum { get; set; }
        public string CurriculumID { get; set; }
        public string ArabicCurriculum { get; set; }
        public bool IsUploadedArabic { set; get; }
        public bool IsUploadedEnglish { set; get; }
        public bool IsUpdateAllowed { set; get; }
       

        public string Grade { get; set; }
        public Nullable<int> DocumentTypeID { get; set; }
        public string ArabicDocumentType { get; set; }
        public string EnglishDocumentType { get; set; }
        public bool ShowEnglish { get; set; }
        public bool ShowArabic { get; set; }
        public bool EnableSchoolRequired { get; set; }
        public bool EnableMinistryRequired { get; set; }

        public Nullable<bool> IsRequiredForPSO { get; set; }
        public Nullable<bool> IsRequiredForSchool { get; set; }
        public string CreatedBy { get; set; }
        public Nullable<System.DateTime> CreateDate { get; set; }
        public string ModifiedBy { get; set; }
        public Nullable<System.DateTime> ModifiedDate { get; set; }
       public bool SeacrhByTermAndSchoolCodeAndGrade { set; get; }
        public bool SeacrhByTermAndSchoolCode { set; get; }
        public bool SeacrhByAll { set; get; }
        public bool SeacrhByTermAndSchoolCodeAndGradeAndDocumentType { set; get; }
        public bool SeacrhByTermAndSchoolCodeAndDocumentType { set; get; }
        public Nullable<bool> IsActive { get; set; }


    }
    [Serializable]
    public class SupportingDocsModelSerializable
    {
        public int ID { get; set; }
        public string Term { get; set; }
        public string SchoolCode { get; set; }
        public string SchoolName { get; set; }
        public string ArabicSchoolName { get; set; }
        public string DoumentLocation { get; set; }
        public string Curriculum { get; set; }
        public string CurriculumID { get; set; }
        public string ArabicCurriculum { get; set; }
        public bool IsUploadedArabic { set; get; }
        public bool IsUploadedEnglish { set; get; }
        public bool IsUpdateAllowed { set; get; }


        public string Grade { get; set; }
        public Nullable<int> DocumentTypeID { get; set; }
        public string ArabicDocumentType { get; set; }
        public string EnglishDocumentType { get; set; }
        public bool ShowEnglish { get; set; }
        public bool ShowArabic { get; set; }
        public bool EnableSchoolRequired { get; set; }
        public bool EnableMinistryRequired { get; set; }

        public Nullable<bool> IsRequiredForPSO { get; set; }
        public Nullable<bool> IsRequiredForSchool { get; set; }
        public string CreatedBy { get; set; }
        public Nullable<System.DateTime> CreateDate { get; set; }
        public string ModifiedBy { get; set; }
        public Nullable<System.DateTime> ModifiedDate { get; set; }
        public bool SeacrhByTermAndSchoolCodeAndGrade { set; get; }
        public bool SeacrhByTermAndSchoolCode { set; get; }
        public bool SeacrhByAll { set; get; }
        public bool SeacrhByTermAndSchoolCodeAndGradeAndDocumentType { set; get; }
        public bool SeacrhByTermAndSchoolCodeAndDocumentType { set; get; }
        public Nullable<bool> IsActive { get; set; }


    }
}
