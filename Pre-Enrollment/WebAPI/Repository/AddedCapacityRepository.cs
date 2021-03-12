using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MOEHE.PSPES.WebAPI.Models;

namespace MOEHE.PSPES.WebAPI.Repository
{
    public class AddedCapacityRepository
    {

        public static DBOperationResult Insert(AddedCapacityModel AddedCapModel)
        {
            DBOperationResult ReturnValue = new DBOperationResult();
            try
            {
                // int CurrentSupportingDocID = 0;
                List<AddedCapacityModel> CheckIfExit = GetADC(AddedCapModel.MOE_SCHOOL_YEAR, AddedCapModel.MOE_SCHOOL_CODE);
               
                if (CheckIfExit.Count > 0)
                {

                    //then update
                    using (PrivateScoolsPreEnrollmentEntities222 DB = new PrivateScoolsPreEnrollmentEntities222())
                    {
                        DB.Database.Connection.Open();


                        foreach (var item in CheckIfExit)
                        {


                            MOE_ADDED_CAPACITY AddedCapacityForUpdate = DB.MOE_ADDED_CAPACITY.Where(D => D.MOE_SCHOOL_YEAR==item.MOE_SCHOOL_YEAR &&D.MOE_SCHOOL_CODE == item.MOE_SCHOOL_CODE && D.MOE_ADDED_CAPACITY1==item.MOE_ADDED_CAPACITY1).FirstOrDefault();

                            AddedCapacityForUpdate.MOE_ADDED_CAPACITY1 = AddedCapModel.MOE_ADDED_CAPACITY1;
                            AddedCapacityForUpdate.MOE_SCHOOL_CODE = AddedCapModel.MOE_SCHOOL_CODE;
                            AddedCapacityForUpdate.MOE_SCHOOL_YEAR = AddedCapModel.MOE_SCHOOL_YEAR;

                            //if ((bool)supportingDocumentForUpdate.IsRequiredForPSO)
                            //{
                            //    //if the document required by PSO it will be froce required also for school
                            //    supportingDocumentForUpdate.IsRequiredForPSO = supportingDocsModel.IsRequiredForPSO; 
                            //    supportingDocumentForUpdate.IsRequiredForSchool = supportingDocsModel.IsRequiredForPSO;
                            //}




                            DB.SaveChanges();
                        }
                    }

                }
                else
                {
                    using (PrivateScoolsPreEnrollmentEntities222 DB = new PrivateScoolsPreEnrollmentEntities222())
                    {
                        DB.Database.Connection.Open();
                        MOE_ADDED_CAPACITY AddedCap = new MOE_ADDED_CAPACITY
                        {
                            MOE_ADDED_CAPACITY1 = AddedCapModel.MOE_ADDED_CAPACITY1,
                            MOE_SCHOOL_CODE = AddedCapModel.MOE_SCHOOL_CODE,
                            MOE_SCHOOL_YEAR = AddedCapModel.MOE_SCHOOL_YEAR,

                        };


                        DB.MOE_ADDED_CAPACITY.Add(AddedCap);

                        DB.SaveChanges();
                        ReturnValue.InsertedID = AddedCap.ID;

                    }

                }



            }

            catch (Exception ex)
            {




            }

            return ReturnValue;



        }


        public static List<AddedCapacityModel> Get(int Year, string schoolCode, int AddedCapacity)
        {

            List<AddedCapacityModel> AddedCap = new List<AddedCapacityModel>();

            //if (Grade == "All" && DocumentTypeID == 0)
            // {
            //Then load all schools Document type for all grades
            using (PrivateScoolsPreEnrollmentEntities222 DB = new PrivateScoolsPreEnrollmentEntities222())
            {
                DB.Database.Connection.Open();

                AddedCap = DB.MOE_ADDED_CAPACITY.Where(D => (D.MOE_SCHOOL_YEAR == Year && D.MOE_SCHOOL_CODE == schoolCode && D.MOE_ADDED_CAPACITY1 == AddedCapacity)).Select(D => new AddedCapacityModel
                {
                    MOE_SCHOOL_YEAR = (int) D.MOE_SCHOOL_YEAR,
                    MOE_SCHOOL_CODE = D.MOE_SCHOOL_CODE,
                    MOE_ADDED_CAPACITY1 = (int) D.MOE_ADDED_CAPACITY1,

                }).ToList();

            }
              return AddedCap;
        }


        public static List<AddedCapacityModel> GetADC(int Year, string schoolCode)
        {

            List<AddedCapacityModel> AddedCap = new List<AddedCapacityModel>();

        
            using (PrivateScoolsPreEnrollmentEntities222 DB = new PrivateScoolsPreEnrollmentEntities222())
            {
                DB.Database.Connection.Open();

                AddedCap = DB.MOE_ADDED_CAPACITY.Where(D => (D.MOE_SCHOOL_YEAR == Year && D.MOE_SCHOOL_CODE == schoolCode )).Select(D => new AddedCapacityModel
                {
                    MOE_SCHOOL_YEAR = (int) D.MOE_SCHOOL_YEAR,
                    MOE_SCHOOL_CODE = D.MOE_SCHOOL_CODE,
                    MOE_ADDED_CAPACITY1 =(int) D.MOE_ADDED_CAPACITY1,

                }).ToList();

            }
            return AddedCap;
        }



    }
}