using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MOEHE.PSPES.WebAPI.Repository
{
    public class MOI_COUNTRY_MAP_Repository
    {
        public static List<MOI_Country_Map> Get()
        {
            List<MOI_Country_Map> Countries = new List<MOI_Country_Map>();

            //if (Grade == "All" && DocumentTypeID == 0)
            // {
            //Then load all schools Document type for all grades
            using (PrivateScoolsPreEnrollmentEntities222 DB = new PrivateScoolsPreEnrollmentEntities222())
            {
                DB.Database.Connection.Open();
                Countries = DB.MOI_Country_Map.ToList();
            }
            return Countries;
        }
    }
}