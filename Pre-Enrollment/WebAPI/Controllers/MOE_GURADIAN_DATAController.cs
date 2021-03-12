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
    public class MOE_GURADIAN_DATAController : ApiController
    {
        // GET: api/MOE_GURADIAN_DATA
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        
        [Route("api/GetStudentContacts/{QID}")]
        public List<MOE_GUARDIAN_DATA_Model> Get(string QID)
        {
            List<MOE_GUARDIAN_DATA_Model> studentContacts = MOE_GUARDIAN_DATA_Repository.Get(QID);
            return studentContacts;
        }


       
        public DBOperationResult Post(MOE_GUARDIAN_DATA_Model_Not_Serializable guardianDataModel)
        {
            //note : this used for insert and update
            DBOperationResult result = MOE_GUARDIAN_DATA_Repository.Insert(guardianDataModel);
            return result;

        }

        // PUT: api/MOE_GURADIAN_DATA/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE: api/MOE_GURADIAN_DATA/5
        public void Delete(int id)
        {
        }
    }
}
