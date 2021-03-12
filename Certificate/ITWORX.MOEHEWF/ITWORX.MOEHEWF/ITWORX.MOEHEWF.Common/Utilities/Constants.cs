using ITWORX.MOEHE.Utilities.Logging;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ITWORX.MOEHEWF.Common.Utilities
{
    public enum Gender
    {
        Male = 0,
        Female = 1
    }
    public enum TemplateType
    {
        Certificates = 1,
        TermsAndConditions = 2
    }
    public enum RequestStatus
    {
        UCEDraft = 1,
        UCESubmitted = 2,
        UCEReceptionistReviewInformation = 3,
        UCEReceptionistNeedsClarification = 4,
        UCReceptionistClarificationReply = 5,
        UCEProgramManagerReview = 6,
        UCEReceptionistMissingInformation = 7,
        UCEProgramEmployeeReview = 8,
        UCEAsianAndEuropianEmployeeReview = 9,
        UCEProgramEmployeeNeedsClarification = 10,
        UCProgramEmployeeClarificationReply = 11,
        UCEProgramManagerReviewRecommendation = 12,
        UCEProgramEmployeeMissingInformation = 13,
        UCEAsianAndEuropianEmployeeMissingInformation = 14,
        UCEHeadManagerReviewRecommendation = 15,
        UCEProgramManagerMissingRecommendationFromHeadManager = 16,
        UCEHeadManagerAccepted = 17,
        UCEHeadManagerRejected = 18,
        UCEAssistantUndersecretaryReviewRecommendation = 19,
        UCETechnicalCommitteeReviewRecommendationFromHeadManager = 20,
        UCELegalEmployeePendingReview = 21,
        UCEProgramManagerMissingRecommendationFromAssistantUndersecretary = 22,
        UCEAssistantUndersecretaryAccepted = 23,
        UCEAssistantUndersecretaryRejected = 24,
        UCETechnicalCommitteeReviewRecommendationAssistantUndersecretary = 25,
        UCEAssistantUndersecretaryMissingInformation = 26,
        UCEHeadManagerMissingInformationTechnicalCommittee = 27,
        UCEAssistantUndersecretaryAcceptDecisionTechnicalCommittee = 28,
        UCEAssistantUndersecretaryRejectDecisionTechnicalCommittee = 29,
        UCEHeadManagereAcceptDecisionTechnicalCommitte = 30,
        UCEHeadManagerRejectDecisionTechnicalCommittee = 31,
        UCELegalEmployeeSendNotes = 32,
        UCELegalEmployeeMissingInformation = 33,
        UCECulturalMissionNeedsStatement = 34,
        UCECulturalMissionStatementReply = 35,
        UCEClosedByRejection = 36,
        UCEClosedByAcceptance = 37,
        UCEExternalCommunicationAddBook = 38,
        UCEApplicantClarificationReply = 39,
        UCECulturalMissionSuspend = 40,
        UCEExternalCommunicationSuspend = 41,
        UCEHigherEduInstitutesNeedsStatement = 42,
        UCEHigherEduInstitutesStatementReply = 43,

        PADraft = 44,
        PASubmitted = 45,
        PAEmployeeNeedsClarification = 46,
        PAEmployeeClarificationReplay = 47,
        PAProgramManagerReviewRecommendation = 48,
        PAProgramEmployeeMissingInformation = 49,
        PAHeadManagerReviewRecommendation = 50,
        PAHeadEmployeeMissingInformation = 51,
        PAHeadManagerAccepted = 52,
        PAHeadManagerRejected = 53,
        PAEmployeeReviewInformation = 54,
        //Order is very important, please leave it as it is with the current enum values as is.
        RegistrationPassword = 55,
        RegistrationVerificationCode = 56,
        PAProgramManagerReject = 57,
        PAProgramManagerAcceptance = 58,


        UCEDraftForClarification = 59,
        PACulturalMissionNeedsStatement = 60,
        PACulturalMissionStatementReply = 61,
        PAHigherEduInstitutesNeedsStatement = 62,
        PAHigherEduInstitutesStatementReply = 63,

        PADraftForClarification = 64,
        HEDDMessage = 65,
        CHEDMessage = 66,

        ForgetPassword = 67,
        //SCE 
        SCEDraft = 68,
        SCESubmitted = 69,
        SCEReturnForUnauthorized = 70,
        SCEEquivalenceEmployeeReassign = 71,
        SCEEmployeeNeedsClarification = 72,
        SCEEmployeeClarificationReply = 73,
        SCECulturalMissionNeedsStatement = 74,
        SCECulturalMissionStatementReply = 75,
        SCEExternalCommunicationAddBook = 76,
        SCEExternalCommunicationReply = 77,
        SCEAcceptedRecommendation = 78,
        SCERejectedRecommendationSectionManager = 79,
        SCERejectedRecommendationDepartmentManager = 80,
        SCESectionManagerMissingInformation = 81,
        SCESectionManagerAccepted = 82,
        SCESectionManagerRejected = 83,
        SCEDepartmentManagerReviewRecommendation = 84,
        SCEDepartmentManagerMissingRecommendation = 85,
        SCEDepartmentManagerAcceptDecision = 86,
        SCEDepartmentManagerRejectDecision = 87,
        SCEReopenRequest = 88,
        SCEAttendApplicant = 89,
        //  PAExternalCommunicationSuspend = 65,
        // PAProgramEmployeeReview = 66,
        // PAAsianAndEuropianEmployeeMissingInformation = 67,
        //PAApplicantClarificationReply = 68,
        //PACulturalMissionSuspend = 69,



        PAReceptionistNeedsClarification = 1000,
        PAReceptionistReviewInformation = 300,
        PAReceptionistClarificationReply = 500,
        PAProgramManagerReview = 600,
        PAReceptionistMissingInformation = 700,
        PAAsianAndEuropianEmployeeReview = 900,
        PAProgramEmployeeNeedsClarification = 1000,
        PAProgramEmployeeClarificationReply = 1100,

        PAProgramManagerMissingRecommendationFromHeadManager = 1600,
        PAAssistantUndersecretaryReviewRecommendation = 1900,
        PATechnicalCommitteeReviewRecommendationFromHeadManager = 2000,
        PALegalEmployeePendingReview = 2100,
        PAProgramManagerMissingRecommendationFromAssistantUndersecretary = 2200,
        PAAssistantUndersecretaryAccepted = 2300,
        PAAssistantUndersecretaryRejected = 2400,
        PATechnicalCommitteeReviewRecommendationAssistantUndersecretary = 2500,
        PAAssistantUndersecretaryMissingInformation = 2600,
        PAHeadManagerMissingInformationTechnicalCommittee = 2700,
        PAAssistantUndersecretaryAcceptDecisionTechnicalCommittee = 2800,
        PAAssistantUndersecretaryRejectDecisionTechnicalCommittee = 2900,
        PAHeadManagereAcceptDecisionTechnicalCommitte = 3000,
        PAHeadManagerRejectDecisionTechnicalCommittee = 3100,
        PALegalEmployeeSendNotes = 3200,
        PALegalEmployeeMissingInformation = 3300,
        PAClosedByRejection = 3600,
        PAClosedByAcceptance = 3700,
        PAExternalCommunicationAddBook = 3800

    }
    public enum RequestType
    {
        CertificateEquivalency = 1,
        PriorApproval = 2,
        Schooling = 3

    }
    public enum NotificationType
    {
        Email = 1,
        SMS = 0
    }

    public enum ExternalBookRequestStatus
    {
        New = 0,
        Edit = 1,
        Reply = 3,
        Saved = 4,
        Sent = 5,
        View = 6
    }

    public enum RequestViews
    {
        AllItems = 1,
        ApplicantInProgress = 2,
        ApplicantAccepted = 3,
        ApplicantRejected = 4,
        ApplicantNeedsClarification = 5,
        ApplicantCurrentRequest = 6,
        ApplicantDraft = 7,
        ApplicantPendingForReview = 8,
        ApplicantClosedByAccepted = 9,
        ApplicantClosedByRejected = 10,
        ApplicantSuspend = 11


    }

    public class Constants
    {

        public static string DelegationTemplate = "DelegationTemplate";
        public static string AcademicDegree = "AcademicDegree";
        public static string PaymentRecords = "PaymentRecords";
        public static string Requests = "Requests";
        public static string PARequests = "PARequests";
        public static string PAEquationOfficerProcedures = "PAEquationOfficerProcedures";
        public static string SimilarRequests = "SimilarRequests";
        public static string RequestPhase = "RequestPhase";
        public static string RequestStatus = "RequestStatus";
        public static string RequestTypes = "RequestTypes";
        public static string CalculatedDetailsForCertificate = "CalculatedDetailsForCertificate";
        public static string OrganizationalLettersAttachments = "OrganizationlLettersAttachments";
        public static string Configuration = "Configuration";
        public static string Nationality = "Nationality";
        public static string NationalityCategory = "NationalityCategory";
        public static string Applicants = "Applicants";
        public static string ApplicantsAttachments = "ApplicantsAttachments";
        public static string HEWebUrl = "/";// "/en/HE"
        public static string AttachmentType = "AttachmentType";
        public static string ApplicantsGroup = "FBA_Users";
        public static string University = "University";
        public static string UniversityList = "UniversityLists";
        public static string CountryOfStudy = "CountryOfStudy";
        public static string VerificationErrorInfo = "VerificationErrorInfo";
        public static string UserVerificationCode = "UserVerificationCode";
        public static string Notifications = "Notifications";
        public static string RequestHistoricalRecords = "RequestsHistory";
        public static string ReceptionistGroupName = "Receptionists";
        //public static string CulturalMissionGroupName = "Cultural Mission Employees";
        public static string ProgramManagerGroupName = "Program Managers";
        public static string ArabicProgEmployeeGroupName = "Arabian and Asian Program Employees";
        public static string EuropeanProgEmployeeGroupName = "European Program Employees";
        public static string TechnicalCommitteeGroupName = "Technical Committee Employees";
        public static string LegalAffairsGroupName = "Legal Affairs Employees";
        public static string AssistUndersecretaryGroupName = "Assistant UnderSecretary Employees";
        public static string DepartmentManagerGroupName = "Head Managers";
        public static string ApplicanstGroupName = "Applicants";
        public static string EmployeeAsApplicant = "Employees As Applicants";
        public static string SecretaryGroupName = "Secretarial Employees";
        public static string HigherEducationalInstitutionsGroupName = "Higher Educational Institutions";
        public static string CulturalMissionBritainGroupName = "Cultural Mission Britain";
        public static string CulturalMissionAustraliaGroupName = "Cultural Mission Australia";
        public static string CulturalMissionFranceGroupName = "Cultural Mission France";
        public static string CulturalMissionUSAGroupName = "Cultural Mission USA";
        public static string CulturalMissionCanadaGroupName = "Cultural Mission Canada";
        public static string CulturalMissionJordanGroupName = "Cultural Mission Jordan";
        public static string StatementOrganizations = "StatementOrganizations";
        public static string HETemplates = "HETemplates";
        public static string CertificatesAttachments = "CertificatesAttachments";
        public static string CalculatedDetailsForCertificateAttachments = "CalculatedDetailsForCertificateAttachments";
        public static string PACalculatedDetailsForCertificateAttachments = "PACalculatedDetailsForCertificateAttachments";
        public static string PACalculatedDetailsForCertificate = "PACalculatedDetailsForCertificate";
        public static string PARequestsAttachments = "PARequestsAttachments";
        public static string DelegationDocuments = "DelegationDocuments";
        public static string PADelegationDocuments = "PADelegationDocuments";
        public static int SearchLimit = 100;
        public static string TermsandConditions = "TermsAndConditions";
        public static string SCERequests = "SCERequests";
        public static string SCENotifications = "SCENotifications";


        public static string SCEEquivalenceEmployeesGroupName = "SCE Equivalence Employees";
        public static string SCEDepartmentManagersGroupName = "SCE Department Managers";
        public static string SCESectionManagers = "SCE Section Managers";
        public static string SCEHigherEducationalInstitutionsGroupName = "SCE Higher Educational Institutions";
        public static string SCECulturalMissionBritainGroupName = "SCE Cultural Mission Britain";
        public static string SCECulturalMissionAustraliaGroupName = "SCE Cultural Mission Australia";
        public static string SCECulturalMissionFranceGroupName = "SCE Cultural Mission France";
        public static string SCECulturalMissionUSAGroupName = "SCE Cultural Mission USA";
        public static string SCECulturalMissionCanadaGroupName = "SCE Cultural Mission Canada";
        public static string SCECulturalMissionJordanGroupName = "SCE Cultural Mission Jordan";
        public static string SCERequestsAttachments = "SCERequestsAttachments";
        public static string ReassignEmployees = "Reassign Employees";
        public static string FinancialManagementGroupName = "Financial Management";

        public static Dictionary<int, int> PAValues = new Dictionary<int, int>() {
            {36,53 },
            {37,52 }

             };

        public static Dictionary<int, int> SCEValues = new Dictionary<int, int>()
        {
            {36,83 },
            {37,82 }
        };

    }
}
