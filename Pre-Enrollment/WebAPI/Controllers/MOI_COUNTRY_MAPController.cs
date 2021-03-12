using MOEHE.PSPES.WebAPI.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace MOEHE.PSPES.WebAPI.Controllers
{
    public class MOI_COUNTRY_MAPController : ApiController
    {
        // GET: api/MOI_COUNTRY_MAP
        [Route("api/GetMOICountries")]
        [HttpGet]
        public List<MOI_Country_Map> Get()
        {
            return MOI_COUNTRY_MAP_Repository.Get();
        }

        // GET: api/MOI_COUNTRY_MAP/5
        public string Get(int id)
        {
            return "value";
        }

        // POST: api/MOI_COUNTRY_MAP
        public void Post([FromBody]string value)
        {
        }

        // PUT: api/MOI_COUNTRY_MAP/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE: api/MOI_COUNTRY_MAP/5
        public void Delete(int id)
        {
        }
    }
}
