using MOEHE.PSPES.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace MOEHE.PSPES.Repository
{
   public  class TermRepository
    {
        public static async Task<List<TermModel>> GetTerms()
        {
            using (HttpClient cons = Utility.GetHttpClientConnection())
            {

                List<TermModel> AllTerms = new List<TermModel>();
                try
                {
                    HttpResponseMessage res = await cons.GetAsync(string.Format("api/Term"));

                    res.EnsureSuccessStatusCode();
                    if (res.IsSuccessStatusCode)
                    {
                        AllTerms = await res.Content.ReadAsAsync<List<TermModel>>();
                    }
                }
                catch (Exception ex)
                {
                    //this mean service down (Server may be changed)
                }

                return AllTerms;
            }
        }

    }
}
