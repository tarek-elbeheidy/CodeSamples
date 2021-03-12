
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOEHE.PSPES.Models
{
    public class PSPESConstants
    {
        public const string ArabicLanguage = "Arabic";
        public const string EnglishLanguage = "English";
        public const string RelationshipsCodesetID = "92D0E1C5-E44F-DD11-87FA-0019D1638957";
        public const string MaritalStatusCodesetID = "38AE2021-F233-DD11-9F58-0019D1638957";
        public const string EmployerTypesCodesetID = "B4FCB267-0334-DD11-9F58-0019D1638957";
        public const string CurriculumsCodesetID = "5F0C99EB-C251-4789-BA92-95FC0B683B71";
        public const string InsertionDone = "INSERTION_DONE";
        public const string NoAcadData = "NO_ACAD_DATA";
        public const string NoGuardianData = "NO_GUARDIAN_DATA";
        public const string NoContactData = "NO_CONTACT_DATA";
        public const string NoHealthData = "NO_HEALTH_DATA";
        internal const string QsecDomain = "LDAP://sec.gov.qa";
        internal const string EdusecDomain = "LDAP://secedu.qa";
        public const string TestInvitation = "TestInvitation";
        public const string TestRejection = "TestRejection";
        public const string TestResultpass = "TestResultpass";
        public const string TestResultFail = "TestResultFail";
        public const string InterviewInvitation = "InterviewInvitation";
        public const string InterviewRejection = "InterviewRejection";
        public const string InterviewResultpass = "InterviewResultpass";
        public const string InterviewResultFailed = "InterviewResultFailed";
        public const string SeatreservationFeesInvite = "SeatreservationFeesInvite";
        public const string Seatreservationconfirmation = "Seatreservationconfirmation";
        public const string ArabicApplicationExistError = "عفوا, لقد تم تقديم طلب سابق لنفس الطالب في نفس الصف ";
        public const string EnglishApplicationExistError = "Sorry, a previous application has been submitted for the same student in the same grade";
        public const string EnglishApplicationError = "Error occurred while saving ";
        public const string ArabicApplicationError = "حدث خطأ اثناء الحفظ ";
        public const string ArabicApplicationConfirmation = "تم تقديم الطلب الخاص بالطالب /الطالبة {0} في مدرسة {1} بنجاح رقم الطلب: {2} وتم ارسال رساله تاكيد الي رقم{3}";
        public const string EnglishApplicationConfirmation = "Application for {0} in school {1} has been submitted successfully with Refrence No.: {2} and confirmation message sent to {3}";
        public const string ArabicAgeError = "لا يمكن تسجيل الطالب /الطالبة في هذا الصف نظرا لعدم تطابق شرط العمر";
        public const string EnglishAgeError = "Student application can not be submitted for this grade for violating age restrictions";
        public const string ArabicApplicationConfirmationSMS = "عزيزي ولي الامر، شكرا لك على طلب القبول للتسجيل  في {0}  لابنكم / ابنتكم  {1}-{2}  للصف: {3}. يرجى استخدام الرقم المرجعي  :{4}  لمزيد من التواصل مع المدرسة. سيتم إرسال التحديثات المتعلقة بابنكم / ابنتكم عبر الرسائل القصيرة على هاتفك النقال المسجل";
        public const string EnglishApplicationConfirmationSMS = "Dear Parent, Thank you for seeking admission in {0} for your child: {1} - {2} for Grade:{3}. Please use the reference number:{4} for further communication with school. Updates related to your child will be sent to you via SMS on your registered Mobile";
        public const string ArabicNoSchoolError = "عفوا، لم يتم تحديد مدرسة لهذا المستخدم،يرجى التواصل مع الإدارة المختصة";
        public const string EnglishNoSchoolError = "Sorry, No School is assigned for your user, please contact your system administrator.";
        public const string ArabicSupportingDocConfirmation = "تم حفظ الوثائق بنجاح  ";
        public const string EnglishSupportingDocConfirmation = "Documents Saved Successfully ";
        public const string EnglishSupportingDocError = "Error occurred while saving ";
        public const string ArabicSupportingDocError = "حدث خطأ اثناء الحفظ ";
        public const string PrivateSchoolOfficeSharePointGroup = "PrivateSchoolOffice";
        public const string PrivateSchoolUsersSharePointGroup = "PrivateSchoolUsers";

        public const string EnglishSaveError = "Error occurred while saving ";
        public const string ArabicSaveError = "حدث خطأ اثناء الحفظ ";

        //Enrollment Controlling Error Messages

        public const string ArabicEnrollmentOutOfDateError = "تقديم الطلبات مغلق الآن لهذه المدرسة";
        public const string EnglishEnrollmentOutOfDateError = "Enrollment is closed now for this school";
        public const string ArabicEnrollmentMOICountryError = "لم يتم التعرف على جنسية الطالب";
        public const string EnglishEnrollmentMOICountryError = "Student Nationality is not is not available ";
        public const string ArabicEnrollmentNationalityError = "تقديم الطلبات مغلق الآن لجنسية الطالب ";
        public const string EnglishEnrollmentNationalityError = "Enrollment is closed now for student nationality ";
        public const string ArabicEnrollmentGradeError = "تقديم الطلبات مغلق الآن لهذا الصف";
        public const string EnglishEnrollmentGradeError = "Enrollment is closed now for this grade";


        //guardian mobile not provided
        public const string ArabicApplicationNoGuardianMobile = "لم يتم تحديد رقم الجوال الخاص بولي الأمر للتواصل، برجاء الذهاب الى بيانات ولى الأمر لإدخال الرقم ";
        public const string EnglishApplicationNoGuardianMobile = "Guardian Mobile Number was not provided, please navigate to Guardian Information to provide the number";



        //guardian mobile not provided
        public const string ArabicApplicationNoSpecialNeedLearningDifficulties = "لم يتم اختيار  ذوي الاحتياجات الخاصة(نعم/لا)أو/و صعوبات التعلم(نعم/لا)، برجاء الذهاب الى المعلومات الصحية لإدخال المعلومات";
        public const string EnglishApplicationNoSpecialNeedLearningDifficulties = "Please choose  Special Need and/or Learning difficulties status, please navigate to Health Information to provide the data";

        //student has confirmed seat in other school

        public const string ArabicConfirmedApplicationExistError = "هذا الطالب مسجل في هذا العام الأكاديمي";
        public const string EnglishConfirmedApplicationExistError = "This student has been registered before in this academic year";


        //student invalid refernce number for Edit

        public const string ArabicInvalidRefernceNumber = "لم يتم تحديد رقم مرجعي";
        public const string EnglishInvalidRefernceNumber = "No Reference Number provided";



        //student exception has expired

        public const string ArabicExceptionExpired = "انتهت صلاحية هذا الاستثناء ، يرجى الاتصال بمكتب المدارس الخاصة لتمديد الاستثناء";
        public const string EnglishExceptionExpired = "This exception has expired, please contact Private School Office to extend the exception";


        //student trying to repeat the year

        public const string ArabicRepeatYearError = "هذا الصف سيسمح للطالب بإعادة السنة ، يرجى الاتصال بمكتب المدارس الخاصة للحصول على استثناء";
        public const string EnglishRepeatYearError = "This grade will let the student repeat the year, please contact Private School Office to get an exception";

        //Error message to check valid qatari id --> Added by Veer on 10 July 2018
        public const string err_valid_qid_en = "Please enter a valid Student QID";
        public const string err_valid_qid_ar = "الرجاء ادخال رقم شخصي صحيح للطالب";

        //Error No data available of the student --> Added by Veer on 10 July 2018
        public const string err_nodata_en = "No available data for this student in Ministry of Interior, please contact helpdesk";
        public const string err_nodata_ar = "لا يوجد بيانات متاحة للطالب في وزارة الداخلية، برجاء التواصل مع ادارة الدعم الفني";

        //Error Not valid finalized application --> Added by Veer on 11 July 2018
        public const string err_notvalid_qid_en = "It's not valid finalized application, please contact helpdesk";
        public const string err_notvalid_qid_ar = "It's not valid finalized application, please contact helpdesk";

        public const string err_service_unavailable_en = "Service is currently unavaliable, please contact helpdesk";
        public const string err_service_unavailable_ar = "الخدمة غير متاحة حاليا، برجاء التواصل مع ادارة الدعم الفني";

        //Constant value added by Veer on15 July 2018
        public const string WithdrawApplication = "WithdrawApplication";

        //Redirect Page added by Veer on 23 July 2018;
        public const string RedirectPage = "/_layouts/15/closeConnection.aspx?loginasanotheruser=true";

        //CONTENT FIELDS FOR REPORT ADDED BY VEER ON 24 JULY 2018
        #region REPORT CONSTANTS

        //SCHOOL INFO
        public const string REPORT_SCHOOL_TERM = "MOE_TERM,";
        public const string REPORT_SCHOOL_SCHOOLCODE = "MOE_SCHOOL_CODE,";
        public const string REPORT_SCHOOL_SCHOOLNAME_EN = "MOE_SCHOOL_NAME_ENG,";
        public const string REPORT_SCHOOL_SCHOOLNAME_AR = "MOE_SCHOOL_NAME_ARA,";
        public const string REPORT_STUDENT_GRADE = "MOE_APPLIED_GRADE,";
        public const string REPORT_SCHOOL_CURRICULUM_EN = "MOE_CURRICULUM_NAMEEN,";
        public const string REPORT_SCHOOL_CURRICULUM_AR = "MOE_CURRICULUM_NAMEAR,";
        public const string REPORT_SCHOOL_GENDER = "MOE_SCHOOL_GENDER,";
        public const string REPORT_SCHOOL_STATUS = "IsApplicationFinalized,";

        //PERSONAL INFO
        public const string REPORT_SERIEL_ID = "ID,";
        public const string REPORT_STUDENT_QID = "STUDENT_QID,";
        public const string REPORT_STUDENT_NAME_EN = "STUDENT_NAME_EN,";
        public const string REPORT_STUDENT_NAME_AR = "STUDENT_NAME_AR,";
        public const string REPORT_STUDENT_APPLICATIONNUMBER = "MOE_APPL_REF_NBR,";
        public const string REPORT_STUDENT_BIRTHDATE = "MOE_DOB,";
        public const string REPORT_STUDENT_NATIONALITY_EN = "MOE_COUNTRY_ENGLISH_NAME,";
        public const string REPORT_STUDENT_NATIONALITY_AR = "MOE_COUNTRY_ARABIC_NAME,";
        public const string REPORT_STUDENT_REGIONALAREA = "MOE_RESIDENTIAL_AREA,";
        public const string REPORT_STUDENT_TRANSPORTATION = "MOE_AVAIL_TRANSPORT,";
        public const string REPORT_STUDENT_APPLICATIONDATE = "APPLICATION_DATE,";
        public const string REPORT_STUDENT_GENDER = "MOE_GENDER,";

        //GUARDIAN INFO

        public const string REPORT_GUARDIAN_QID = "MOE_RELATED_QID,";
        public const string REPORT_GUARDIAN_NAME_EN = "GUARDIAN_NAME_EN,";
        public const string REPORT_GUARDIAN_NAME_AR = "GUARDIAN_NAME_AR,";
        public const string REPORT_GUARDIAN_GENDER = "GUARDIAN_GENDER,";
        public const string REPORT_GUARDIAN_RELATIONSHIP_EN = "MOE_RELATIONSHIP_NAMEEN,";
        public const string REPORT_GUARDIAN_RELATIONSHIP_AR = "MOE_RELATIONSHIP_NAMEAR,";
        public const string REPORT_GUARDIAN_NATIONALITY_EN = "GUARDIAN_NATIONALITY_EN,";
        public const string REPORT_GUARDIAN_NATIONALITY_AR = "GUARDIAN_NATIONALITY_AR,";
        public const string REPORT_GUARDIAN_MOBILE = "GUARDIAN_MOBILE_NUMBER,";
        public const string REPORT_GUARDIAN_HOMENUMBER = "GUARDIAN_HOME_PHONE_NUMBER,";
        public const string REPORT_GUARDIAN_EMAIL = "GUARDIAN_EMAIL,";
        public const string REPORT_GUARDIAN_EMPLOYERSECTOR_ID = "GUARDIAN_EMPLOYER_TYPE_ID,";
        public const string REPORT_GUARDIAN_EMPLOYERSECTOR_NAME_EN = "GUARDIAN_EMPLOYER_TYPE_NAME_EN,";
        public const string REPORT_GUARDIAN_EMPLOYERSECTOR_NAME_AR = "GUARDIAN_EMPLOYER_TYPE_NAME_AR,";
        public const string REPORT_GUARDIAN_EMPLOYER_ID = "GUARDIAN_EMPLOYER_ID,";
        public const string REPORT_GUARDIAN_EMPLOYER_NAME_EN = "GUARDIAN_EMPLOYER_NAME_EN,";
        public const string REPORT_GUARDIAN_EMPLOYER_NAME_AR = "GUARDIAN_EMPLOYER_NAME_AR,";

        //ACADEMIC INFO
        public const string REPORT_CURRENTYEAR_TERM = "CURRENT_YEAR_TERM,";
        public const string REPORT_CURRENTYEAR_SCHOOLCODE = "CURRENT_YEAR_SCHOOL_CODE,";
        public const string REPORT_CURRENTYEAR_SCHOOLNAME_EN = "MOE_CURRENT_SCHOOLNAME_ENG,";
        public const string REPORT_CURRENTYEAR_SCHOOLNAME_AR = "MOE_CURRENT_SCHOOLNAME_ARA,";
        public const string REPORT_CURRENTYEAR_GRADE = "CURRENT_YEAR_GRADE,";
        public const string REPORT_CURRENTYEAR_CURRICULUM_EN = "CURRENT_CURRICULUM_NAMEEN,";
        public const string REPORT_CURRENTYEAR_CURRICULUM_AR = "CURRENT_CURRICULUM_NAMEAR,";
        public const string REPORT_CURRENTYEAR_RESULTS = "CURRENT_YEAR_RESULT,";

        public const string REPORT_PREVIOUSYEAR_TERM = "PREVIOUS_YEAR_TERM,";
        public const string REPORT_PREVIOUSYEAR_SCHOOLCODE = "PREVIOUS_YEAR_SCHOOL_CODE,";
        public const string REPORT_PREVIOUSYEAR_SCHOOLNAME_EN = "MOE_PREVIOUS_SCHOOLNAME_ENG,";
        public const string REPORT_PREVIOUSYEAR_SCHOOLNAME_AR = "MOE_PREVIOUS_SCHOOLNAME_ARA,";
        public const string REPORT_PREVIOUSYEAR_GRADE = "PREVIOUS_YEAR_GRADE,";
        public const string REPORT_PREVIOUSYEAR_CURRICULUM_EN = "PREVIOUS_CURRICULUM_NAMEEN,";
        public const string REPORT_PREVIOUSYEAR_CURRICULUM_AR = "PREVIOUS_CURRICULUM_NAMEAR,";
        public const string REPORT_PREVIOUSYEAR_RESULTS = "PREVIOUS_YEAR_RESULT,";

        //HEALTH INFO

        public const string REPORT_HEALTHINFO_CARD = "HEALTH_CARD_NUMBER,";
        public const string REPORT_HEALTHINFO_CENTER = "HEALTH_CENTER_NAME,";
        public const string REPORT_HEALTHINFO_FITTOSTUDY = "FIT_TO_STUDY,";
        public const string REPORT_HEALTHINFO_SPECIALNEED = "MOE_SPL_NEEDS,";
        public const string REPORT_HEALTHINFO_LEARNINGDIFFICULTIES = "MOE_LEARNING_DIFFICULTIES,";
        public const string REPORT_HEALTHINFO_HEALTHISSUES = "HEALTH_PROBLEMS_DETAILS,";

        //FINALIZATION INFO

        public const string REPORT_FINALIZE_DOCUMENTLIST = "MOE_DOCUMENT_LOCATION,";

        //TEST INFO
        //public const string REPORT_TEST_CALLEDFORTEST = "WithdrawApplication";
        public const string REPORT_TEST_DATETIME = "TEST_DATETIME,";
        //public const string REPORT_TEST_REJECTED = "WithdrawApplication";
        //public const string REPORT_TEST_REASON = "WithdrawApplication";
        public const string REPORT_TEST_FINALRESULTS = "MOE_TEST_RESULTS,";

        //INTERVIEW INFO
        //public const string INTERVIEW_CALLEDFORINTERVIEW = "WithdrawApplication";
        public const string REPORT_INTERVIEW_DATETIME = "INTERVIEW_DATETIME,";
        //public const string REPORT_INTERVIEW_REJECTED = "WithdrawApplication";
        //public const string REPORT_INTERVIEW_REASON = "WithdrawApplication";
        public const string REPORT_INTERVIEW_FINALRESULTS = "MOE_INTERVIEW_RESULTS,";

        //SEAT RESERVATION INFO
        //public const string REPORT_RESERVE_CALLEDFORPAY = "WithdrawApplication";
        public const string REPORT_RESERVE_AMOUNT = "RESERVE_FEE,";
        public const string REPORT_RESERVE_DATETIME = "MOE_FEES_PAID_DATE,";

        //FEE PAID
        public const string REPORT_FEE_AMOUNT = "RESERVE_FEE,";
        public const string REPORT_FEE_DATETIME = "MOE_FEES_PAID_DATE,";

        #endregion
    }
}
