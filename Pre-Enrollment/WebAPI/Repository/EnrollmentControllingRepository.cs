using MOEHE.PSPES.WebAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MOEHE.PSPES.WebAPI.Repository
{
    public class EnrollmentControllingRepository
    {
        public static EnrollmentControllingModel Get(string schoolCode)
        {



            using (PrivateScoolsPreEnrollmentEntities222 DB = new PrivateScoolsPreEnrollmentEntities222())
            {

                EnrollmentControllingModel Enrollment_Controlling_Model = new EnrollmentControllingModel();
                Enrollment_Controlling_Model = DB.EnrollementControllings.Where(D => D.SchoolCode == schoolCode).Select(D => new EnrollmentControllingModel
                {
                    SchoolCode = D.SchoolCode,
                    CountryCodes = D.CountryCodes,
                    GradeCodes = D.GradeCodes,
                    FromDate = D.FromDate,
                    ToDate = D.ToDate,
                    ID = D.ID

                }).FirstOrDefault();



                //this means the academic data is stored in our DB, then return it
                return Enrollment_Controlling_Model;

            }

        }
        public static DBOperationResult Update(EnrollmentControllingModel enrollment_controlling_model)
        {

            DBOperationResult ReturnValue = new DBOperationResult();

            using (PrivateScoolsPreEnrollmentEntities222 DB = new PrivateScoolsPreEnrollmentEntities222())
            {

                EnrollementControlling Enrollment_Controlling_Model_for_Update = DB.EnrollementControllings.Where(D => D.SchoolCode == enrollment_controlling_model.SchoolCode && D.ID == enrollment_controlling_model.ID).Select(D => D).FirstOrDefault();

                Enrollment_Controlling_Model_for_Update.SchoolCode = enrollment_controlling_model.SchoolCode;
                    Enrollment_Controlling_Model_for_Update.CountryCodes = enrollment_controlling_model.CountryCodes;
                    Enrollment_Controlling_Model_for_Update.GradeCodes = enrollment_controlling_model.GradeCodes;
                    Enrollment_Controlling_Model_for_Update.FromDate = enrollment_controlling_model.FromDate;
                    Enrollment_Controlling_Model_for_Update.ToDate = enrollment_controlling_model.ToDate;
                Enrollment_Controlling_Model_for_Update.ID = enrollment_controlling_model.ID;

                DB.SaveChanges();
                ReturnValue.EnglishResult = PSPESConstants.InsertionDone;



                //this means the academic data is stored in our DB, then return it
                return ReturnValue;

            }
        }
    }
}