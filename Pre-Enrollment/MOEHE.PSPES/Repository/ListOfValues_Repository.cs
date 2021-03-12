using MOEHE.PSPES.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace MOEHE.PSPES.Repository
{
    class ListOfValues_Repository
    {
        public static async Task<List<ListOfValues_Model>> GetListOfValues(string codesetID)
        {
            using (HttpClient cons = Utility.GetHttpClientConnection())
            {

                List<ListOfValues_Model> lov_Model = new List<ListOfValues_Model>();
                try
                {
                    HttpResponseMessage res = await cons.GetAsync(string.Format("api/GetListOfValues/{0}", codesetID));

                    res.EnsureSuccessStatusCode();
                    if (res.IsSuccessStatusCode)
                    {
                        lov_Model = await res.Content.ReadAsAsync<List<ListOfValues_Model>>();
                    }
                }
                catch (Exception ex)
                {
                    //this mean service down (Server may be changed)
                }

                return lov_Model;
            }
        }
          
    }
}
