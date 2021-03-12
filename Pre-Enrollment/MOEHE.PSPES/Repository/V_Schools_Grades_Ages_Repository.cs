using MOEHE.PSPES.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace MOEHE.PSPES.Repository
{
    class V_Schools_Grades_Ages_Repository
    {
        public static async Task<List<V_Schools_Grades_Ages>> GetSchoolGradesAndAges(int Term, string schoolCode)
        {
            using (HttpClient cons = Utility.GetHttpClientConnection())
            {

                List<V_Schools_Grades_Ages> SchoolGradesAndAges = new List<V_Schools_Grades_Ages>();
                try
                {
                    HttpResponseMessage res = await cons.GetAsync(string.Format("api/GetSchoolGradesAndAges/{0}/{1}", Term, schoolCode));

                    res.EnsureSuccessStatusCode();
                    if (res.IsSuccessStatusCode)
                    {
                        SchoolGradesAndAges = await res.Content.ReadAsAsync<List<V_Schools_Grades_Ages>>();
                    }
                }
                catch (Exception ex)
                {
                    //this mean service down (Server may be changed)
                }

                return SchoolGradesAndAges;
            }
        }
    }
}
