using MOEHE.PSPES.WebAPI.Models;
using System;
using MOEHE.PSPES.WebAPI;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MOEHE.PSPES.WebAPI
{
    public class TestingRepository
    {
        /// <summary>
        /// Used to get all Testings
        /// </summary>
        /// <returns></returns>
        public static List<TestingModel> Get()
        {
            using (PrivateScoolsPreEnrollmentEntities222 DB = new PrivateScoolsPreEnrollmentEntities222())
            {

                List<TestingModel> Testings = DB.Testings.Select(D => new TestingModel
                {
                    ID = D.ID,
                    TestName = D.TestName

                }).ToList();



                return Testings;
            }
        }

        /// <summary>
        /// Used to get testing with id
        /// </summary>
        /// <returns></returns>
        public static TestingModel Get(int testingID)
        {
            using (PrivateScoolsPreEnrollmentEntities222 DB = new PrivateScoolsPreEnrollmentEntities222())
            {

                TestingModel Testing = DB.Testings.Where(D => D.ID == testingID).Select(D => new TestingModel
                {
                    ID = D.ID,
                    TestName = D.TestName

                }).FirstOrDefault();



                return Testing;
            }
        }
    }
}