using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MOEHE.PSPES.WebAPI.Repository
{
    public class MOE_DOUBLE_CURRICULUM_MAPPING_Repository
    {
        public static string GetCurriculumIDMapping(string doubleCurriculumID)
        {




            MOE_DOUBLE_CURRICULUM_MAPPING double_curriculum_model = new MOE_DOUBLE_CURRICULUM_MAPPING();
            try
            {
                using (PrivateScoolsPreEnrollmentEntities222 DB = new PrivateScoolsPreEnrollmentEntities222())
                {
                    double_curriculum_model = DB.MOE_DOUBLE_CURRICULUM_MAPPING.Where(D => D.DOUBLE_CURRICULUM_ID == doubleCurriculumID).Select(i=>i).FirstOrDefault();
                }

            }
            catch (Exception ex)
            {

                string s = ex.Message;

            }


            

            return double_curriculum_model.CURRICULUM_ID;



        }
    }
}