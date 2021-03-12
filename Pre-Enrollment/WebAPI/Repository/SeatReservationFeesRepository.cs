using MOEHE.PSPES.WebAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MOEHE.PSPES.WebAPI.Repository
{
    public class SeatReservationFeesRepository
    {

        public static List<SeatReservationFeeModel> Get(string ApplicantReferenceNumber)
        {

            List<SeatReservationFeeModel> supportingDoc = new List<SeatReservationFeeModel>();

                //Then load All schools Document type for specific grade
                using (PrivateScoolsPreEnrollmentEntities222 DB = new PrivateScoolsPreEnrollmentEntities222())
                {
                    DB.Database.Connection.Open();

                    
                 supportingDoc = DB.SeatReservationFees.Where(D => (D.ApplicantReferenceNumber == ApplicantReferenceNumber)).Select(D => new SeatReservationFeeModel
                {
                    ApplicantReferenceNumber = D.ApplicantReferenceNumber,
                    FeesPaidDate =  D.FeesPaidDate,
                    CreatedBy = D.CreatedBy,
                    CreatedDate = D.CreatedDate,
                    ID = D.ID,
                    NationalID = D.NationalID,
                    ReservationFeesAmount = D.ReservationFeesAmount
                    
                }).ToList();
                
            }



            return supportingDoc;

        }

        public static List<SeatReservationFeeModel> GetByRefAndID(string ApplicantReferenceNumber,string QID)
        {
            decimal QIDDec = decimal.Parse(QID);
            List<SeatReservationFeeModel> supportingDoc = new List<SeatReservationFeeModel>();

            //Then load All schools Document type for specific grade
            using (PrivateScoolsPreEnrollmentEntities222 DB = new PrivateScoolsPreEnrollmentEntities222())
            {
                DB.Database.Connection.Open();


                supportingDoc = DB.SeatReservationFees.Where(D => (D.ApplicantReferenceNumber == ApplicantReferenceNumber && D.NationalID==QIDDec)).Select(D => new SeatReservationFeeModel
                {
                    ApplicantReferenceNumber = D.ApplicantReferenceNumber,
                    FeesPaidDate = D.FeesPaidDate,
                    CreatedBy = D.CreatedBy,
                    CreatedDate = D.CreatedDate,
                    ID = D.ID,
                    NationalID = D.NationalID,
                    ReservationFeesAmount = D.ReservationFeesAmount

                }).ToList();

            }



            return supportingDoc;

        }

        public static List<SeatReservationFeeModel> GetByQID(string studentQID)
        {

            decimal decimalStudentQID = decimal.Parse(studentQID);
            List<SeatReservationFeeModel> supportingDoc = new List<SeatReservationFeeModel>();

            //Then load All schools Document type for specific grade
            using (PrivateScoolsPreEnrollmentEntities222 DB = new PrivateScoolsPreEnrollmentEntities222())
            {
                DB.Database.Connection.Open();

                supportingDoc = DB.SeatReservationFees.Where(D => (D.NationalID == decimalStudentQID)).Select(D => new SeatReservationFeeModel
                {
                    ApplicantReferenceNumber = D.ApplicantReferenceNumber,
                    FeesPaidDate =  D.FeesPaidDate,
                    CreatedBy = D.CreatedBy,
                    CreatedDate = D.CreatedDate,
                    ID = D.ID,
                    NationalID = D.NationalID,
                    ReservationFeesAmount = D.ReservationFeesAmount
                    
                }).ToList();


            }



            return supportingDoc;

        }


        public static int Get(int? Term,string SchoolCode,string Grade)
        {

            int AppliedApplications = 0;

            //Then load All schools Document type for specific grade
            using (PrivateScoolsPreEnrollmentEntities222 DB = new PrivateScoolsPreEnrollmentEntities222())
            {
                DB.Database.Connection.Open();
                //tarek el beheidy 2/5/2018 added condition of checking the length of national id in order to 
                //exclude the manually deactivated finalized applications as per request from PSO
                //tarek el beheidy 9/5/2018 added condition to check if isApplicationFinalized == true, to count the correct number of confirmed applications
                AppliedApplications = DB.SeatReservationFees.Where(D => (D.MOE_Application_DATA.MOE_TERM == Term && D.MOE_Application_DATA.MOE_SCHOOL_CODE==SchoolCode &&D.MOE_Application_DATA.MOE_APPLIED_GRADE==Grade && D.NationalID.Value.ToString().Length>5 && D.MOE_Application_DATA.IsApplicationFinalized == true )).Select(D => D).ToList().Count();


            }




            return AppliedApplications;

        }






        public static DBOperationResult Insert(SeatReservationFee  seatReservationFee)
        {
            DBOperationResult ReturnValue = new DBOperationResult();
            try
            {

                // int CurrentSupportingDocID = 0;
                List<SeatReservationFeeModel> CheckIfExit = GetByRefAndID(seatReservationFee.ApplicantReferenceNumber,seatReservationFee.NationalID.Value.ToString());
                if (CheckIfExit.Count > 0)
                {

                    //then update
                    using (PrivateScoolsPreEnrollmentEntities222 DB = new PrivateScoolsPreEnrollmentEntities222())
                    {
                        DB.Database.Connection.Open();


                        foreach (var item in CheckIfExit)
                        {


                            SeatReservationFee SeatReservationFeeForUpdate = DB.SeatReservationFees.Where(D => D.ApplicantReferenceNumber == seatReservationFee.ApplicantReferenceNumber && D.NationalID == seatReservationFee.NationalID).FirstOrDefault();

                            SeatReservationFeeForUpdate.FeesPaidDate = seatReservationFee.FeesPaidDate;
                            SeatReservationFeeForUpdate.NationalID = seatReservationFee.NationalID;
                            SeatReservationFeeForUpdate.ReservationFeesAmount = seatReservationFee.ReservationFeesAmount;


                            DB.SaveChanges();
                        }
                    }

                }
                else
                {
                    using (PrivateScoolsPreEnrollmentEntities222 DB = new PrivateScoolsPreEnrollmentEntities222())
                    {
                        DB.Database.Connection.Open();

                        int ApplicationID = DB.MOE_Application_DATA.Where(D => D.MOE_APPL_REF_NBR ==seatReservationFee.ApplicantReferenceNumber && D.NATIONAL_ID == seatReservationFee.NationalID).Select(D => D).FirstOrDefault().ID;
                        MOE_Application_DATA ApplicationToFinalize = DB.MOE_Application_DATA.Where(D => D.MOE_APPL_REF_NBR ==seatReservationFee.ApplicantReferenceNumber && D.NATIONAL_ID == seatReservationFee.NationalID).Select(D => D).FirstOrDefault();
                        SeatReservationFee NewwSeatReservationFee = new  SeatReservationFee
                        {
                             ApplicantReferenceNumber=seatReservationFee.ApplicantReferenceNumber,
                              ReservationFeesAmount=seatReservationFee.ReservationFeesAmount,
                               NationalID=seatReservationFee.NationalID,
                                FeesPaidDate=seatReservationFee.FeesPaidDate,
                                 CreatedBy=seatReservationFee.CreatedBy,
                                  CreatedDate=DateTime.Now,
                                   ApplicationDataID= ApplicationID


                        };

                        DB.SeatReservationFees.Add(NewwSeatReservationFee);
                        DB.SaveChanges();
                        ReturnValue.InsertedID = NewwSeatReservationFee.ID;

                        //tarek el beheidy added this t5o fix the applications that is being finalized without setting isApplicationFinalized to true
                        ApplicationToFinalize.IsApplicationFinalized = true;
                        DB.SaveChanges();

                    }

                }



            }

            catch (Exception ex)
            {




            }

            return ReturnValue;



        }



        public static int GetAllConfirmed(int? Term, string SchoolCode)
        {

            int AppliedApplications = 0;

            //Then load All schools Document type for specific grade
            using (PrivateScoolsPreEnrollmentEntities222 DB = new PrivateScoolsPreEnrollmentEntities222())
            {
                DB.Database.Connection.Open();

                AppliedApplications = DB.SeatReservationFees.Where(D => (D.MOE_Application_DATA.MOE_TERM == Term && D.MOE_Application_DATA.MOE_SCHOOL_CODE == SchoolCode )).Select(D => D).ToList().Count();


            }



            return AppliedApplications;

        }



    }
}