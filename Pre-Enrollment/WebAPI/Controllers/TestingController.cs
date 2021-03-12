using MOEHE.PSPES.WebAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace MOEHE.PSPES.WebAPI.Controllers
{
    public class TestingController : ApiController
    {
        // GET: api/Testing
        [Route("api/Testing/hamada")]
        [HttpGet]
        public List<TestingModel> welcomeHamada()
        {
            return TestingRepository.Get();
        }

        // GET: api/Testing/5
        public TestingModel Get(int testingId)
        {
            return TestingRepository.Get(testingId);
        }

        // POST: api/Testing
        public void Post([FromBody]string value)
        {
        }

        // PUT: api/Testing/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE: api/Testing/5
        public void Delete(int id)
        {
        }
    }
}
