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
    public class MOE_WITHDRAWAL_REASONController : ApiController
    {
        // GET: api/MOE_WITHDRAWAL_REASON
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET: api/MOE_WITHDRAWAL_REASON/5
        [Route("api/GetWithdrawalReasons")]
        public List<MOE_WITHDRAWAL_REASON_Model> GetAll()
        {
            return MOE_WITHDRAWAL_REASON_Repository.GetWithdrawalReasons();
        }

        // POST: api/MOE_WITHDRAWAL_REASON
        public void Post([FromBody]string value)
        {
        }

        // PUT: api/MOE_WITHDRAWAL_REASON/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE: api/MOE_WITHDRAWAL_REASON/5
        public void Delete(int id)
        {
        }
    }
}
