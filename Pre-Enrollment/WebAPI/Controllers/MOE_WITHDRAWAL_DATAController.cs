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
    public class MOE_WITHDRAWAL_DATAController : ApiController
    {
        // GET: api/MOE_WITHDRAWAL_DATA
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET: api/MOE_WITHDRAWAL_DATA/5
        [Route("api/GetWithdrawalByRefId/{RefId}")]
        public MOE_WITHDRAWAL_DATA_Model GetByRefId(string RefId)
        {
            MOE_WITHDRAWAL_DATA_Model oModel = MOE_WITHDRAWAL_DATA_Repository.GetWithdrawalByRefId(RefId);
            return oModel;
        }
        [Route("api/GetWithdrawalByQId/{QID}")]
        public MOE_WITHDRAWAL_DATA_Model GetByQId(string QID)
        {
            MOE_WITHDRAWAL_DATA_Model oModel = MOE_WITHDRAWAL_DATA_Repository.GetWithdrawalByQId(QID);
            return oModel;
        }

        // POST: api/MOE_WITHDRAWAL_DATA
        public DBOperationResult Post(MOE_WITHDRAWAL_DATA_Model oModel)
        {
            return MOE_WITHDRAWAL_DATA_Repository.Insert(oModel);
        }

        // PUT: api/MOE_WITHDRAWAL_DATA/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE: api/MOE_WITHDRAWAL_DATA/5
        public void Delete(int id)
        {
        }
    }
}
