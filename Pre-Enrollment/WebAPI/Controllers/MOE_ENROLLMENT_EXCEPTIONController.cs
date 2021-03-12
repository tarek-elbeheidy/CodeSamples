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
    public class MOE_ENROLLMENT_EXCEPTIONController : ApiController
    {
        // GET: api/MOE_ENROLLMENT_EXCEPTION
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        [Route("api/GetEnrollmentException/{QID}/{Term}")]
        public List<MOE_ENROLLMENT_EXCEPTION> Get(string QID, int Term)
        {
            return MOE_ENROLLMENT_EXCEPTION_Repository.Get(QID, Term);
        }

        [Route("api/GetEnrollmentExceptionBySchool/{QID}/{Term}/{SchoolCode}")]
        public List<MOE_ENROLLMENT_EXCEPTION> GetBySchool(string QID, int Term, string SchoolCode)
        {
            return MOE_ENROLLMENT_EXCEPTION_Repository.GetBySchool(QID, Term,SchoolCode);
        }

        [Route("api/GetEnrollmentExceptionBySchoolAndGrade/{QID}/{Term}/{SchoolCode}/{Grade}")]
        public MOE_ENROLLMENT_EXCEPTION GetBySchoolAndGrade(string QID, int Term, string SchoolCode,string Grade)
        {
            return MOE_ENROLLMENT_EXCEPTION_Repository.GetBySchoolAndGrade(QID, Term, SchoolCode,Grade);
        }


        public DBOperationResult Post(MOE_ENROLLMENT_EXCEPTION_Model student_exception)
        {
            //note : this used for insert and update
            return MOE_ENROLLMENT_EXCEPTION_Repository.Insert(student_exception);

        }
      

        // PUT: api/MOE_ENROLLMENT_EXCEPTION/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE: api/MOE_ENROLLMENT_EXCEPTION/5
        public void Delete(int id)
        {
        }
    }
}
