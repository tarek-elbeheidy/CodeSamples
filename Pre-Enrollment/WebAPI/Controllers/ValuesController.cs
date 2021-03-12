using MOEHE.PSPES.WebAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace MOEHE.PSPES.WebAPI.Controllers
{
   // [Authorize]
    public class ValuesController : ApiController
    {
        // GET api/values
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" , "value3" , "value4" };
        }
        // GET api/values/5
        public string Get(int id)
        {
            return "value";
        }

        [Route("Information/{id}/{StationID}")]
        public MOE_BIO_DATA_Model Get(string id, string StationID)
        {
            MOE_BIO_DATA_Model model = new MOE_BIO_DATA_Model()
            {
MOE_ARABIC_NAME = "hiiiiiii"
            };
            return model;
            //dummy data
            //return " is simply dummy text of the printing and typesetting industry. Lorem Ipsum has been the industry's standard dummy text ever since the 1500s, when an unknown printer took a galley of type and scrambled it to make a type specimen book. It has survived not only five centuries, but also the leap into electronic typesetting, remaining essentially unchanged. It was popularised in the 1960s with the release of Letraset sheets containing Lorem ";
        }

        [HttpGet]
        [Route("testMehod")]
        public string test()
        {
            return "basha";
        }



        [HttpPost]
        [Route("PostGreeting")]
        public string greeting([FromBody]string name)
        {
            return "hello "+name;
        }



        // POST api/values
        public void Post([FromBody]string value)
        {
        }

        //// PUT api/values/5
        //public void Put(int id, [FromBody]string value)
        //{
        //}

        //// DELETE api/values/5
        //public void Delete(int id)
        //{
        //}
    }
}
