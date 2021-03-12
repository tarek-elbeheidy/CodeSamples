using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MOEHE.PSPES.WebAPI.Models;

namespace MOEHE.PSPES.WebAPI.Repository
{
    public class BuildingCapacityRepository
    {

        public static List<BuildingCapacityModel> Get( string schoolCode)
        {

            List<BuildingCapacityModel> BuildingCap = new List<BuildingCapacityModel>();

            //if (Grade == "All" && DocumentTypeID == 0)
            // {
            //Then load all schools Document type for all grades
            using (PrivateScoolsPreEnrollmentEntities222 DB = new PrivateScoolsPreEnrollmentEntities222())
            {
                DB.Database.Connection.Open();

                BuildingCap = DB.BuildingCapacities.Where(D => (D.SchoolCode == schoolCode)).Select(D => new BuildingCapacityModel
                {
                 BuildingCapacity1 = D.BuildingCapacity1,
                   

                }).ToList();

            }
            return BuildingCap;
        }


    }
}