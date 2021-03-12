using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MOEHE.PSPES.WebAPI.Repository
{
    public class MOE_ENROLLMENT_EXCEPTION_TYPES_Repository
    {
        public static List<Models.MOE_Enrollment_Exception_Types> Get()
        {
            List<Models.MOE_Enrollment_Exception_Types> Exception_Types = new List<Models.MOE_Enrollment_Exception_Types>();

            //if (Grade == "All" && DocumentTypeID == 0)
            // {
            //Then load all schools Document type for all grades
            using (PrivateScoolsPreEnrollmentEntities222 DB = new PrivateScoolsPreEnrollmentEntities222())
            {
                DB.Database.Connection.Open();
                Exception_Types = DB.MOE_Enrollment_Exception_Types.Select(D=> new Models.MOE_Enrollment_Exception_Types() {
                    AttachmentRequired = D.AttachmentRequired,
                    ExceptionTypeName = D.ExceptionTypeName,
                    ExceptionTypeNameAR = D.ExceptionTypeNameAR,
                    ID = D.ID

                }).ToList();
            }
            return Exception_Types;
        }
    }
}