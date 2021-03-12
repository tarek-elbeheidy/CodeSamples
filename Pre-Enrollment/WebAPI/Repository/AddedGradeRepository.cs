using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MOEHE.PSPES.WebAPI.Models;

namespace MOEHE.PSPES.WebAPI.Repository
{
    public class AddedGradeRepository
    {


        public static DBOperationResult Insert(AddedGradeModel AddedGRDModel)
        {
            DBOperationResult ReturnValue = new DBOperationResult();
            try
            {
                
                    using (PrivateScoolsPreEnrollmentEntities222 DB = new PrivateScoolsPreEnrollmentEntities222())
                    {
                    DB.Database.Connection.Open();
                    AddedGrade AddedGRD = new AddedGrade
                    {
                        ADDED_NUMBER_OF_GRADES = AddedGRDModel.ADDED_NUMBER_OF_GRADES,
                        USERID = AddedGRDModel.USERID,
                        DTTM = AddedGRDModel.DTTM,
                        MOE_SCHOOL_CODE = AddedGRDModel.MOE_SCHOOL_CODE,
                        MOE_SCHOOL_YEAR = AddedGRDModel.MOE_SCHOOL_YEAR

                    };


                    DB.AddedGrades.Add(AddedGRD);

                    DB.SaveChanges();
                    ReturnValue.InsertedID = AddedGRD.ID;

                }

                }



           

            catch (Exception ex)
            {




            }

            return ReturnValue;



        }


        //public static List<AddedGradeModel> Get(int Year, string schoolCode, int AddedCapacity)
        //{

        //    List<AddedCapacityModel> AddedCap = new List<AddedCapacityModel>();

        //    //if (Grade == "All" && DocumentTypeID == 0)
        //    // {
        //    //Then load all schools Document type for all grades
        //    using (PrivateScoolsPreEnrollmentEntities DB = new PrivateScoolsPreEnrollmentEntities())
        //    {
        //        DB.Database.Connection.Open();

        //        AddedCap = DB.MOE_ADDED_CAPACITY.Where(D => (D.MOE_SCHOOL_YEAR == Year && D.MOE_SCHOOL_CODE == schoolCode && D.MOE_ADDED_CAPACITY1 == AddedCapacity)).Select(D => new AddedCapacityModel
        //        {
        //            MOE_SCHOOL_YEAR = (int)D.MOE_SCHOOL_YEAR,
        //            MOE_SCHOOL_CODE = D.MOE_SCHOOL_CODE,
        //            MOE_ADDED_CAPACITY1 = (int)D.MOE_ADDED_CAPACITY1,

        //        }).ToList();

        //    }
        //    return AddedCap;
        //}


        public static List<AddedGradeModel> GetAddedGrades(int Year, string schoolCode)
        {

            List<AddedGradeModel> AddedGRD1 = new List<AddedGradeModel>();


            using (PrivateScoolsPreEnrollmentEntities222 DB = new PrivateScoolsPreEnrollmentEntities222())
            {
                DB.Database.Connection.Open();

                AddedGRD1 = DB.AddedGrades.Where(D => (D.MOE_SCHOOL_YEAR == Year && D.MOE_SCHOOL_CODE == schoolCode)).Select(D => new AddedGradeModel
                {
                    MOE_SCHOOL_YEAR = (int)D.MOE_SCHOOL_YEAR,
                    MOE_SCHOOL_CODE = D.MOE_SCHOOL_CODE,
                    USERID = D.USERID,
                    DTTM = D.DTTM,
                    ADDED_NUMBER_OF_GRADES = D.ADDED_NUMBER_OF_GRADES,


                }).ToList();

            }
            return AddedGRD1;
        }




    }
}