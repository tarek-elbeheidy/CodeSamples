using MOEHE.PSPES.WebAPI.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace MOEHE.PSPES.WebAPI.Controllers
{
    public class V_Schools_Grades_AgesController : ApiController
    {
        // GET: api/V_Schools_Grades_Ages
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET: api/V_Schools_Grades_Ages/5
        [Route("api/GetSchoolGradesAndAges/{Term}/{schoolCode}")]
        public List<V_Schools_Grades_Ages> Get(int Term, string schoolCode)
        {
            return V_Schools_Grades_Ages_Repository.GetSchoolGradesAndAges(Term,schoolCode);
        }

        // POST: api/V_Schools_Grades_Ages
        public void Post([FromBody]string value)
        {
        }

        // PUT: api/V_Schools_Grades_Ages/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE: api/V_Schools_Grades_Ages/5
        public void Delete(int id)
        {
        }
    }
}
