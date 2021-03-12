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
    public class EnrollmentControllingController : ApiController
    {
        // GET: api/EnrollmentControlling
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        [Route("api/GetEnrollmentControlling/{schoolCode}")]
        public EnrollmentControllingModel Get(string schoolCode)
        {
            EnrollmentControllingModel enrollmentControlling_Model = new EnrollmentControllingModel();
            try
            {

                enrollmentControlling_Model = EnrollmentControllingRepository.Get(schoolCode);
            }
            catch (Exception ex)
            {

                string s = ex.Message;

            }
            return enrollmentControlling_Model;
        }

        [Route("api/UpdateEnrollmentControlling")]
        public DBOperationResult Post(EnrollmentControllingModel enrollmentControllingModel)
        {
            return EnrollmentControllingRepository.Update(enrollmentControllingModel);
        }

        // PUT: api/EnrollmentControlling/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE: api/EnrollmentControlling/5
        public void Delete(int id)
        {
        }
    }
}
