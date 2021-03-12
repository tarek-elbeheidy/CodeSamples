using MOEHE.PSPES.WebAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MOEHE.PSPES.WebAPI.Repository
{
    public class ENROLLMENT_EXCEPTION_ATTACHMENT_Repository
    {
        public static List<Enrollment_Exception_Attachment_Model> Get(int ExceptionID)
        {
            List<Enrollment_Exception_Attachment_Model> Exception_Attachments = new List<Enrollment_Exception_Attachment_Model>();

           
            using (PrivateScoolsPreEnrollmentEntities222 DB = new PrivateScoolsPreEnrollmentEntities222())
            {
                DB.Database.Connection.Open();
                Exception_Attachments = DB.Enrollment_Exception_Attachment.Where(D=>D.Enrollment_Exception_ID == ExceptionID).Select(D=> new Enrollment_Exception_Attachment_Model()
                {
                    DOCUMENT_LOCATION = D.DOCUMENT_LOCATION,
                    Enrollment_Exception_ID = D.Enrollment_Exception_ID,
                    Enrollment_Exception_Type_ID = D.Enrollment_Exception_Type_ID,
                    ExceptionTypeName = D.MOE_Enrollment_Exception_Types.ExceptionTypeName,
                    ExceptionTypeRequired = D.MOE_Enrollment_Exception_Types.AttachmentRequired,
                    ExceptionTypeID = D.MOE_Enrollment_Exception_Types.ID
                }).ToList();
            }
            return Exception_Attachments;
        }


        public static DBOperationResult Insert (Enrollment_Exception_Attachment_Model Exception_Attachment)
        {


            DBOperationResult ReturnValue = new DBOperationResult();
            Enrollment_Exception_Attachment_Model Exception_Attachment_Exist = new Enrollment_Exception_Attachment_Model();

            Exception_Attachment_Exist = CheckAttachmentExist(Exception_Attachment.Enrollment_Exception_ID, Exception_Attachment.Enrollment_Exception_Type_ID);

            if (Exception_Attachment_Exist !=null && Exception_Attachment_Exist.Enrollment_Exception_ID !=0)
            {
                
                using (PrivateScoolsPreEnrollmentEntities222 DB = new PrivateScoolsPreEnrollmentEntities222())
                {
                    DB.Database.Connection.Open();
                    Enrollment_Exception_Attachment_Model Exception_Attachment_to_Update = DB.Enrollment_Exception_Attachment.Where(D => D.Enrollment_Exception_ID == Exception_Attachment.Enrollment_Exception_ID && D.Enrollment_Exception_Type_ID == Exception_Attachment.Enrollment_Exception_Type_ID).Select(D => new Enrollment_Exception_Attachment_Model()
                    {
                        DOCUMENT_LOCATION = D.DOCUMENT_LOCATION,
                        ID = D.ID,
                        Enrollment_Exception_ID = D.Enrollment_Exception_ID,
                        Enrollment_Exception_Type_ID = D.Enrollment_Exception_Type_ID

                    }).FirstOrDefault();

                    Exception_Attachment_to_Update.Enrollment_Exception_ID = Exception_Attachment.Enrollment_Exception_ID;
                    Exception_Attachment_to_Update.Enrollment_Exception_Type_ID = Exception_Attachment.Enrollment_Exception_Type_ID;
                    Exception_Attachment_to_Update.DOCUMENT_LOCATION = Exception_Attachment.DOCUMENT_LOCATION;
                    DB.SaveChanges();
                    ReturnValue.EnglishResult = PSPESConstants.InsertionDone;
                }
            }

            else
            {
                using (PrivateScoolsPreEnrollmentEntities222 DB = new PrivateScoolsPreEnrollmentEntities222())
                {
                    DB.Database.Connection.Open();
                    Enrollment_Exception_Attachment Exception_Attachment_to_Insert = new Enrollment_Exception_Attachment()
                    {

                        Enrollment_Exception_ID = Exception_Attachment.Enrollment_Exception_ID,
                        Enrollment_Exception_Type_ID = Exception_Attachment.Enrollment_Exception_Type_ID,
                        
                        DOCUMENT_LOCATION = Exception_Attachment.DOCUMENT_LOCATION


                    };



                    DB.Enrollment_Exception_Attachment.Add(Exception_Attachment_to_Insert);
                    DB.SaveChanges();
                    ReturnValue.InsertedID = Exception_Attachment_to_Insert.ID;
                    ReturnValue.EnglishResult = PSPESConstants.InsertionDone;
                }
            }
            //using (PrivateScoolsPreEnrollmentEntities222 DB = new PrivateScoolsPreEnrollmentEntities222())
            //{
            //    DB.Database.Connection.Open();
            //    Exception_Attachments = DB.Enrollment_Exception_Attachment.Where(D => D.Enrollment_Exception_ID == ExceptionID).Select(D => D).ToList();
            //}
            return ReturnValue;
        }

        private static Enrollment_Exception_Attachment_Model CheckAttachmentExist(int enrollment_Exception_ID, int enrollment_Exception_Type_ID)
        {
            Enrollment_Exception_Attachment_Model Exception_Attachment = new Enrollment_Exception_Attachment_Model();
            using (PrivateScoolsPreEnrollmentEntities222 DB = new PrivateScoolsPreEnrollmentEntities222())
            {
                DB.Database.Connection.Open();
                Exception_Attachment = DB.Enrollment_Exception_Attachment.Where(D => D.Enrollment_Exception_ID == enrollment_Exception_ID && D.Enrollment_Exception_Type_ID == enrollment_Exception_Type_ID).Select(D => new Enrollment_Exception_Attachment_Model() {
                    DOCUMENT_LOCATION = D.DOCUMENT_LOCATION,
                    ID = D.ID,
                    Enrollment_Exception_ID = D.Enrollment_Exception_ID,
                    Enrollment_Exception_Type_ID = D.Enrollment_Exception_Type_ID

                }).FirstOrDefault();
            }
            return Exception_Attachment;
        }
    }
}