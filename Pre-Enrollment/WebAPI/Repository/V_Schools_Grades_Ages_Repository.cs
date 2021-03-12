using MOEHE.PSPES.WebAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MOEHE.PSPES.WebAPI.Repository
{
    public class V_Schools_Grades_Ages_Repository
    {

        public static List<V_Schools_Grades_Ages> GetSchoolGradesAndAges(int Term, string schoolCode)
        {
            List<V_Schools_Grades_Ages> schoolGrades = new List<V_Schools_Grades_Ages>();
            try
            {
                using (PrivateScoolsPreEnrollmentEntities222 DB = new PrivateScoolsPreEnrollmentEntities222())
                {

                    schoolGrades = DB.V_Schools_Grades_Ages.Where(D => D.MOE_SCHOOL_CODE == schoolCode && D.MOE_TERM == Term).Select(P => P).OrderBy(c => c.Weight).ToList();
                }
            }
            catch (Exception ex)
            { }

            return schoolGrades;
        }
    }
}