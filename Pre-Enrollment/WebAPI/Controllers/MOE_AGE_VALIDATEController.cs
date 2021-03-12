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
    public class MOE_AGE_VALIDATEController : ApiController
    {
        // GET: api/MOE_AGE_VALIDATE
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }
        #region Get Curriculum ID BY School Code added by VEER on 24 July 2018
        [Route("api/GetCurriculumIDBySchoolCode/{term}/{schoolCode}")]
        public string Get(string term, string schoolCode)
        {
            return MOE_AGE_VALIDATE_Repository.GetCurriculumIDBySchoolCode(term, schoolCode);
        }
        #endregion

        [Route("api/GetAgeValidate/{term}/{schoolCode}/{schoolType}/{schoolCurriculumID}/{grade}/{passedID}")]
        // GET: api/MOE_AGE_VALIDATE/5
        public MOE_AGE_VALIDATE_Model Get(string term, string schoolCode, string schoolType, string schoolCurriculumID, string grade,string passedID)
        {
            return MOE_AGE_VALIDATE_Repository.Get(term,schoolCode,schoolType,schoolCurriculumID,grade,int.Parse(passedID));
        }


        [Route("api/GetGradeList/{term}/{schoolCode}/{schoolType}/{schoolCurriculumID}")]
        // GET: api/MOE_AGE_VALIDATE/5
        public List<MOE_AGE_VALIDATE_Model> Get(string term, string schoolCode, string schoolType, string schoolCurriculumID)
        {
            return MOE_AGE_VALIDATE_Repository.GetGradeList(term,schoolCode, schoolType, schoolCurriculumID);
        }

        [Route("api/GetGradeListWithCurriculumOnly/{term}/{schoolType}/{schoolCurriculumID}")]
        // GET: api/MOE_AGE_VALIDATE/5
        public List<MOE_AGE_VALIDATE_Model> Get(string term, string schoolType, string schoolCurriculumID)
        {
            return MOE_AGE_VALIDATE_Repository.GetGradeListWithCurriculumOnly(term, schoolType, schoolCurriculumID);
        }


        

        [Route("api/GetGradeAgeRange/{term}/{schoolCode}/{schoolType}/{schoolCurriculumID}/{GradeLevel}")]
        // GET: api/MOE_AGE_VALIDATE/5
        public MOE_AGE_VALIDATE_Model Get(string term, string schoolCode, string schoolType, string schoolCurriculumID, string GradeLevel)
        {
            return MOE_AGE_VALIDATE_Repository.GetGradeAgeRange(term,schoolCode, schoolType, schoolCurriculumID,GradeLevel);
        }

        // POST: api/MOE_AGE_VALIDATE
        public void Post([FromBody]string value)
        {
        }

        // PUT: api/MOE_AGE_VALIDATE/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE: api/MOE_AGE_VALIDATE/5
        public void Delete(int id)
        {
        }
    }
}
