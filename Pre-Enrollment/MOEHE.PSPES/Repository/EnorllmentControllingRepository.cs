using MOEHE.PSPES.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace MOEHE.PSPES.Repository
{
    class EnorllmentControllingRepository
    {
        public static async Task<EnrollmentControllingModel> GetEnrollmentControllingData(string schoolCode)
        {
            using (HttpClient cons = Utility.GetHttpClientConnection())
            {

                EnrollmentControllingModel enrollmentControlling_Model = new EnrollmentControllingModel();
                try
                {
                    HttpResponseMessage res = await cons.GetAsync("api/GetEnrollmentControlling/" + schoolCode);

                    res.EnsureSuccessStatusCode();
                    if (res.IsSuccessStatusCode)
                    {
                        enrollmentControlling_Model = await res.Content.ReadAsAsync<EnrollmentControllingModel>();
                    }
                }
                catch (Exception ex)
                {
                    //this mean service down (Server may be changed)
                }

                return enrollmentControlling_Model;
            }
        }

        public static async Task<DBOperationResult> UpdateEnrollmentControllingData(EnrollmentControllingModel enrollmentControllingModel)
        {
            using (HttpClient cons = Utility.GetHttpClientConnection())
            {

                DBOperationResult ReturnedResult = new DBOperationResult();
                try
                {
                    HttpResponseMessage res = await cons.PostAsJsonAsync("api/UpdateEnrollmentControlling/",enrollmentControllingModel);

                    res.EnsureSuccessStatusCode();
                    if (res.IsSuccessStatusCode)
                    {
                        ReturnedResult = await res.Content.ReadAsAsync<DBOperationResult>();
                    }
                }
                catch (Exception ex)
                {
                    //this mean service down (Server may be changed)
                }

                return ReturnedResult;
            }
        }



    }
}
