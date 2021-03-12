using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace MOEHE.PSPES.WebAPI.Controllers
{
    public class MOE_PREENROLLMENT_DATA_VIEWController : ApiController
    {
        // GET: api/MOE_PREENROLLMENT_DATA_VIEW
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET: api/MOE_PREENROLLMENT_DATA_VIEW/5
        public string Get(int id)
        {
            return "value";
        }

        // POST: api/MOE_PREENROLLMENT_DATA_VIEW
        public void Post([FromBody]string value)
        {
        }

        // PUT: api/MOE_PREENROLLMENT_DATA_VIEW/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE: api/MOE_PREENROLLMENT_DATA_VIEW/5
        public void Delete(int id)
        {
        }
    }
}
