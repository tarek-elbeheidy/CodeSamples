using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MOEHE.PSPES.WebAPI.Models;

namespace MOEHE.PSPES.WebAPI.Repository
{
    public class SeatCapacityRepository
    {

        public static DBOperationResult Insert(SeatCapacityModel SeatCapModel)
        {
            DBOperationResult ReturnValue = new DBOperationResult();
            try
            {
                // int CurrentSupportingDocID = 0;
                //List<SeatCapacityModel> CheckIfExit = Get(SeatCapModel.MOE_TERM, SeatCapModel.MOE_SCHOOL_CODE, SeatCapModel.MOE_SCHOOL_GRADE, SeatCapModel.MOE_SEAT_DISTRIBUTION, SeatCapModel.MOE_PREENROL_SEATS);
                List<SeatCapacityModel> CheckIfExit = CheckExist(SeatCapModel.MOE_TERM, SeatCapModel.MOE_SCHOOL_CODE, SeatCapModel.MOE_SCHOOL_GRADE);

                if (CheckIfExit.Count > 0)
                {

                    //then update
                    using (PrivateScoolsPreEnrollmentEntities222 DB = new PrivateScoolsPreEnrollmentEntities222())
                    {
                        DB.Database.Connection.Open();


                        foreach (var item in CheckIfExit)
                        {


                            //MOE_SEAT_CAPACITY SeatCapacityForUpdate = DB.MOE_SEAT_CAPACITY.Where (D => D.ID == item.ID).FirstOrDefault();

                            MOE_SEAT_CAPACITY SeatCapacityForUpdate = DB.MOE_SEAT_CAPACITY.Where(D => D.MOE_TERM == item.MOE_TERM &&D.MOE_SCHOOL_CODE==item.MOE_SCHOOL_CODE && D.MOE_SCHOOL_GRADE==item.MOE_SCHOOL_GRADE ).FirstOrDefault();

                            SeatCapacityForUpdate.MOE_TERM  = SeatCapModel.MOE_TERM;
                            SeatCapacityForUpdate.MOE_SCHOOL_CODE = SeatCapModel.MOE_SCHOOL_CODE;
                            SeatCapacityForUpdate.MOE_SCHOOL_GRADE = SeatCapModel.MOE_SCHOOL_GRADE;
                            SeatCapacityForUpdate.MOE_SEAT_DISTRIBUTION = SeatCapModel.MOE_SEAT_DISTRIBUTION;
                            SeatCapacityForUpdate.MOE_PREENROL_SEATS = SeatCapModel.MOE_PREENROL_SEATS;
                            SeatCapacityForUpdate.MOE_TRANSACTION_DTMM = SeatCapModel.MOE_TRANSACTION_DTMM;
                            SeatCapacityForUpdate.MOE_USERID = SeatCapModel.MOE_USERID;
                           SeatCapacityForUpdate.MOE_CURRENT_ENROLLED = SeatCapModel.MOE_CURRENT_ENROLLED;

                           
                            DB.SaveChanges();
                        }
                    }

                }
                else
                {
                    using (PrivateScoolsPreEnrollmentEntities222 DB = new PrivateScoolsPreEnrollmentEntities222())
                    {
                        DB.Database.Connection.Open();
                        MOE_SEAT_CAPACITY SeatCap = new MOE_SEAT_CAPACITY
                        {
                            MOE_TERM = SeatCapModel.MOE_TERM,
                            MOE_SCHOOL_CODE = SeatCapModel.MOE_SCHOOL_CODE,
                            MOE_SCHOOL_GRADE = SeatCapModel.MOE_SCHOOL_GRADE,
                            MOE_SEAT_DISTRIBUTION = SeatCapModel.MOE_SEAT_DISTRIBUTION,
                            MOE_PREENROL_SEATS = SeatCapModel.MOE_PREENROL_SEATS,
                            MOE_TRANSACTION_DTMM = SeatCapModel.MOE_TRANSACTION_DTMM,
                            MOE_USERID = SeatCapModel.MOE_USERID,
                            MOE_CURRENT_ENROLLED = SeatCapModel.MOE_CURRENT_ENROLLED
                                                       


                    };


                        DB.MOE_SEAT_CAPACITY.Add(SeatCap);

                        DB.SaveChanges();
                        ReturnValue.InsertedID = SeatCap.ID;

                    }

                }



            }

            catch (Exception ex)
            {




            }

            return ReturnValue;



        }


        public static List<SeatCapacityModel> Get(int Term, string schoolCode, string Grade,int seatDistrib, int PreEnrollSeats)
        {

            List<SeatCapacityModel> SeatCap = new List<SeatCapacityModel>();

            //if (Grade == "All" && DocumentTypeID == 0)
            // {
            //Then load all schools Document type for all grades
            using (PrivateScoolsPreEnrollmentEntities222 DB = new PrivateScoolsPreEnrollmentEntities222())
            {
                DB.Database.Connection.Open();

                SeatCap = DB.MOE_SEAT_CAPACITY.Where (D => (D.MOE_TERM==Term && D.MOE_SCHOOL_CODE == schoolCode && D.MOE_SCHOOL_GRADE==Grade && D.MOE_SEAT_DISTRIBUTION == seatDistrib && D.MOE_PREENROL_SEATS==PreEnrollSeats )).Select(D => new SeatCapacityModel
                {
                  MOE_TERM = (int)D.MOE_TERM,
                  MOE_SCHOOL_CODE = D.MOE_SCHOOL_CODE,
                  MOE_SCHOOL_GRADE = D.MOE_SCHOOL_GRADE,
                  MOE_SEAT_DISTRIBUTION = (int)D.MOE_SEAT_DISTRIBUTION,
                  MOE_PREENROL_SEATS = (int)D.MOE_PREENROL_SEATS,
                    MOE_TRANSACTION_DTMM = (DateTime)D.MOE_TRANSACTION_DTMM,
                    MOE_USERID = D.MOE_USERID


                }).ToList();

            }
            return SeatCap;
        }



        public static List<SeatCapacityModel> CheckExist(int Term, string schoolCode, string Grade)
        {

            List<SeatCapacityModel> SeatCap2 = new List<SeatCapacityModel>();

            //if (Grade == "All" && DocumentTypeID == 0)
            // {
            //Then load all schools Document type for all grades
            using (PrivateScoolsPreEnrollmentEntities222 DB = new PrivateScoolsPreEnrollmentEntities222())
            {
                DB.Database.Connection.Open();

                SeatCap2 = DB.MOE_SEAT_CAPACITY.Where(D => (D.MOE_TERM == Term && D.MOE_SCHOOL_CODE == schoolCode && D.MOE_SCHOOL_GRADE == Grade )).Select(D => new SeatCapacityModel
                {
                    MOE_TERM = (int)D.MOE_TERM,
                    MOE_SCHOOL_CODE = D.MOE_SCHOOL_CODE,
                    MOE_SCHOOL_GRADE = D.MOE_SCHOOL_GRADE,
                    MOE_SEAT_DISTRIBUTION = (int)D.MOE_SEAT_DISTRIBUTION,
                    MOE_PREENROL_SEATS = (int)D.MOE_PREENROL_SEATS,
                    MOE_TRANSACTION_DTMM = (DateTime)D.MOE_TRANSACTION_DTMM,
                    MOE_USERID = D.MOE_USERID,
                   MOE_CURRENT_ENROLLED =(int) D.MOE_CURRENT_ENROLLED,
                   ID = D.ID

                }).ToList();

            }
            return SeatCap2;
        }




        public static List<SeatCapacityModel> CheckNumberofGrades(int Term, string schoolCode)
        {

            List<SeatCapacityModel> SeatCap3 = new List<SeatCapacityModel>();

            //if (Grade == "All" && DocumentTypeID == 0)
            // {
            //Then load all schools Document type for all grades
            using (PrivateScoolsPreEnrollmentEntities222 DB = new PrivateScoolsPreEnrollmentEntities222())
            {
                DB.Database.Connection.Open();

                SeatCap3 = DB.MOE_SEAT_CAPACITY.Where(D => (D.MOE_TERM == Term && D.MOE_SCHOOL_CODE == schoolCode)).Select(D => new SeatCapacityModel
                {
                    MOE_TERM = (int)D.MOE_TERM,
                    MOE_SCHOOL_CODE = D.MOE_SCHOOL_CODE,
                    MOE_SCHOOL_GRADE = D.MOE_SCHOOL_GRADE,
                    MOE_SEAT_DISTRIBUTION = (int)D.MOE_SEAT_DISTRIBUTION,
                    MOE_PREENROL_SEATS = (int)D.MOE_PREENROL_SEATS,
                    MOE_TRANSACTION_DTMM = (DateTime)D.MOE_TRANSACTION_DTMM,
                    MOE_USERID = D.MOE_USERID,
                    MOE_CURRENT_ENROLLED = (int)D.MOE_CURRENT_ENROLLED

                }).ToList();

            }
            return SeatCap3;
        }





    }
}