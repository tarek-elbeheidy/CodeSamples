using ITWORX.MOEHE.Utilities;
using ITWORX.MOEHE.Utilities.Logging;
using ITWORX.MOEHEWF.Common.Entities;
using Microsoft.SharePoint;
using Microsoft.SharePoint.DirectorySoap;
using Microsoft.SharePoint.Utilities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ITWORX.MOEHEWF.UCE.Entities
{
    public class Request
    {
        public int ID { get; set; }
        public string RequestNumber { get; set; }

        public DateTime RequestCreationDate { get; set; }
        public DateTime SubmitDate { get; set; }
        public AcademicDegree AcademicDegree { get; set; }

        public bool CertificateThroughScholarship { get; set; }

        public string EntityProvidingStudy { get; set; }
      //  public string OtherEntity { get; set; }
        public bool CampusStudy { get; set; }
       public string PlaceOfStudy { get; set; }
        public bool CampusExam { get; set; }
        public string PlaceOfExam { get; set; }
        public AcademicDegree AcademicDegreeForEquivalence { get; set; }

        public CountryOfStudy CountryOfStudy { get; set; }
        public University University { get; set; }
        public string UniversityNotFoundInList { get; set; }
        public string Faculty { get; set; }
        public string WizardActiveStep { get; set; }

        public string FacultyNotFoundInList { get; set; }
        public Specialization Specialization { get; set; }
        public string SpecializationNotFoundInList { get; set; }
        public StudyLanguage StudyLanguage { get; set; }
        //public StudyType StudyType { get; set; }
        public StudySystem StudySystem { get; set; }
        public EntityNeedsEquivalency EntityNeedsEquivalency { get; set; }
        public string OtherEntityNeedsEquivalency { get; set; }
        public Certificates Certificates { get; set; }
        public Common.Entities.RequestStatus RequestStatus { get; set; }
        public DateTime StudyStartDate { get; set; }
        public DateTime StudyGraduationDate { get; set; }
        public int AcademicProgramPeriod { get; set; }
        public int ActualStudingPeriod { get; set; }
        public int NumberOfHoursGained { get; set; }
        public string GPA { get; set; }
        public int NumberOfOnlineHours { get; set; }
        public string PercentageOfOnlineHours { get; set; }
       // public bool IsThereComprehensiveExam { get; set; }
        //public bool IsThereAcceptanceExam { get; set; }
        public UniversityMainCountry UniversityMainHeadquarter { get; set; }
        public string NewUniversityHeadquarter { get; set; }
        public string RejectionReason { get; set; }
        public string RejectedFrom { get; set; }
        public DateTime RejectionDate { get; set; }
        public string UniversityAddress { get; set; }
        public string UniversityEmail { get; set; }
        public bool WorkingOrNot { get; set; }
       
        public EntityWorkingFor EntityWorkingFor { get; set; }
        public string OtherEntityWorkingFor { get; set; }
        //public DateTime HiringDate { get; set; }
       // public Occupation Occupation { get; set; }
        public string Occupation { get; set; }
       // public string OtherOccupation { get; set; }
        //public string OccupationPhone { get; set; }
        public int IncomingNumber { get; set; }
        public DateTime BookDate { get; set; }
        public string BarCode { get; set; }
        public string LoginName { get; set; }
        public Applicants ApplicantID { get; set; }
        public Request RelatedRequests {get;set;}
        public RequestTypes RequestType { get; set; }
        public string FinalDecision { get; set; }
        public string FinalDecisionAr { get; set; }
        public string UniversityList { get; set; }
        public int Year { get; set; }
        public DateTime ActionDate { get; set; }
        public bool HavePAOrNot { get; set; }
        public string UniversityType { get; set; }
        public string Note { get; set; }
    }

}
