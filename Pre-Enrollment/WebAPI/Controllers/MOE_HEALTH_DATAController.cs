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
    public class MOE_HEALTH_DATAController : ApiController
    {
        // GET: api/MOE_HEALTH_DATA
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET: api/MOE_HEALTH_DATA/5
        
        [Route("api/GetHealthInfo/{QID}")]
        public MOE_HEALTH_DATA_Model Get(string QID)
        {
            MOE_HEALTH_DATA_Model healthModel = MOE_HEALTH_DATA_Repository.Get(QID);
            return healthModel;
        }

        [Route("api/GetHealthInfoByRef/{ApplicantReference}")]
        public MOE_HEALTH_DATA_Model GetBy(string ApplicantReference)
        {
            MOE_HEALTH_DATA_Model healthModel = MOE_HEALTH_DATA_Repository.GetBy(ApplicantReference);
            return healthModel;
        }

        public DBOperationResult Post(MOE_HEALTH_DATA_Model healthDataModel)
        {
            //note : this used for insert and update
            return MOE_HEALTH_DATA_Repository.Insert(healthDataModel);

        }

        // PUT: api/MOE_HEALTH_DATA/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE: api/MOE_HEALTH_DATA/5
        public void Delete(int id)
        {
        }
    }
}
