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

namespace ITWORX.MOEHEWF.PA.Entities
{
    public class PARequest
    {
        public int ID { get; set; }
        public string RequestNumber { get; set; }

        public DateTime RequestCreationDate { get; set; }
        public DateTime SubmitDate { get; set; }
        public Certificates HighestCertificate { get; set; }

        public CountryOfStudy CountryOfStudy { get; set; }
        public University University { get; set; }
        public string UniversityNotFoundInList { get; set; }
        public string Faculty { get; set; }
        public string FacultyNotFoundInList { get; set; }
        public Specialization Specialization { get; set; }

        public Specialization ProgramSpecialization { get; set; }

        public string SpecializationNotFoundInList { get; set; }
        public string ProgramSpecializationNotFoundInList { get; set; }
        
        public Certificates Certificates { get; set; }
        public Common.Entities.RequestStatus RequestStatus { get; set; }
        public DateTime AcademicStartDate { get; set; }
        public DateTime AcademicEndDate { get; set; }
        public int AcademicNumberOfYears { get; set; }
        //public UniversityMainCountry UniversityMainHeadquarter { get; set; }
        //public string NewUniversityHeadquarter { get; set; }
        public string RejectionReason { get; set; }
        public string RejectedFrom { get; set; }
        public DateTime RejectionDate { get; set; }
        public bool WorkingOrNot { get; set; }
        public EntityWorkingFor EntityWorkingFor { get; set; }
        public string OtherEntityWorkingFor { get; set; }
        public DateTime HiringDate { get; set; }
        public string Occupation { get; set; }
        public string OtherOccupation { get; set; }
        public string OccupationPhone { get; set; }
        public string LoginName { get; set; }
        public Applicants ApplicantID { get; set; }
        public PARequest RelatedRequests { get; set; }
        public RequestTypes RequestType { get; set; }
        public string FinalDecision { get; set; }
        public string FinalDecisionAr { get; set; }
        public string UniversityList { get; set; }
        public int Year { get; set; }
        public DateTime ActionDate { get; set; }
        public string School { get; set; }
        public AcademicDegree ProgramType { get; internal set; }
        public CountryOfStudy ProgramCountry { get; internal set; }
        public string OtherProgramCountry { get; internal set; }
        public string OtherProgramUniversity { get; internal set; }
        public University ProgramUniversity { get; internal set; }
        public string OtherProgramFaculty { get; internal set; }
        public string ProgramFaculty { get; internal set; }
        public StudyType ProgramStudyType { get; internal set; }
        public StudySystem ProgramStudySystem { get; internal set; }
        public string ProgramPeriod { get; internal set; }
        public string CertificateDate { get; set; }
        public string WizardActiveStep { get; set; }
        public bool JoinedOtherUniversities { get; set; }
        public string AssignedTo { get; set; }
        

    }

}
