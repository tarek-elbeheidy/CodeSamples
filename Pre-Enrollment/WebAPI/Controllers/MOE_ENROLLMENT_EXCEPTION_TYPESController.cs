using MOEHE.PSPES.WebAPI.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace MOEHE.PSPES.WebAPI.Controllers
{
    public class MOE_ENROLLMENT_EXCEPTION_TYPESController : ApiController
    {
        //// GET: api/MOE_ENROLLMENT_EXCEPTION_TYPES
        //public IEnumerable<string> Get()
        //{
        //    return new string[] { "value1", "value2" };
        //}

        // GET: api/MOE_ENROLLMENT_EXCEPTION_TYPES/

            [Route("api/GetExceptionTypes")]
        public List<Models.MOE_Enrollment_Exception_Types> Get()
        {
            return MOE_ENROLLMENT_EXCEPTION_TYPES_Repository.Get();
        }

        // GET: api/MOE_ENROLLMENT_EXCEPTION_TYPES/5
        public string Get(int id)
        {
            return "value";
        }

        // POST: api/MOE_ENROLLMENT_EXCEPTION_TYPES
        public void Post([FromBody]string value)
        {
        }

        // PUT: api/MOE_ENROLLMENT_EXCEPTION_TYPES/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE: api/MOE_ENROLLMENT_EXCEPTION_TYPES/5
        public void Delete(int id)
        {
        }
    }
}
