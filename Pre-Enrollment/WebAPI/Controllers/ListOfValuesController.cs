using MOEHE.PSPES.WebAPI.Models;
using MOEHE.PSPES.WebAPI.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace MOEHE.PSPES.WebAPI.Controllers
{
    public class ListOfValuesController : ApiController
    {
        // GET: api/ListOfValues
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        [Route("api/GetListOfValues/{codesetID}")]
        public List<ListOfValues_Model> Get(string codesetID)
        {
            List<ListOfValues_Model> listOfValues_Models = new List<ListOfValues_Model>();
            try
            {
               
                listOfValues_Models = ListOfValues_Repository.GetListOfValues(codesetID);
            }
            catch (Exception ex)
            {

                string s = ex.Message;

            }
            return listOfValues_Models;
        }

        [Route("api/GetCurriculumsString/{codesetID}/{param}")]
        public string Get(string codesetID,string param)
        {
            string curriculums = "";
            List<ListOfValues_Model> listOfValues_Models = new List<ListOfValues_Model>();
            try
            {

                listOfValues_Models = ListOfValues_Repository.GetListOfValues(codesetID);

                if(listOfValues_Models.Count>0)
                {
                    foreach (ListOfValues_Model item in listOfValues_Models)
                    {
                        curriculums += item.ID + ",";
                    }
                    
                }
            }
            catch (Exception ex)
            {

                string s = ex.Message;

            }
            return curriculums;
        }

        // POST: api/ListOfValues
        public void Post([FromBody]string value)
        {
        }

        // PUT: api/ListOfValues/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE: api/ListOfValues/5
        public void Delete(int id)
        {
        }
    }
}
