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
    public class MOE_SCHOOLController : ApiController
    {
        [Route("api/GetSchoolInfo/{schoolYearID}/{schoolCode}/{isPublic}")]
        public MOE_SCHOOL_Model Get(string schoolYearID, string schoolCode, string isPublic)
        {
            MOE_SCHOOL_Model schoolModel = MOE_SCHOOL_Repository.Get(schoolYearID, schoolCode, isPublic);
            return schoolModel;
        }


        [Route("api/GetSchoolGrade/{schoolYearID}/{schoolCode}/{isPublic}/{NullValue}")]
        public List<schoolGrade> Get(string schoolYearID, string schoolCode, string isPublic,string NullValue)
        {
            List<schoolGrade> schoolModel = MOE_SCHOOL_Repository.GetByGrade(schoolYearID, schoolCode, isPublic);
            return schoolModel;
        }

        // GET: api/MOE_SCHOOL/5
        [Route("api/GetAllSchools/{schoolYearID}/{isPublic}")]
        public List<MOE_SCHOOL_Model> Get(string schoolYearID, string isPublic)
        {
            List<MOE_SCHOOL_Model> schools = MOE_SCHOOL_Repository.Get(schoolYearID,  isPublic);
            return schools;
        }

        
        // GET: api/MOE_SCHOOL/5
        [Route("api/GetQatarSchools/{schoolYearID}")]
        public List<MOE_SCHOOL_Model> Get(string schoolYearID)
        {
            List<MOE_SCHOOL_Model> schools = MOE_SCHOOL_Repository.Get(schoolYearID);
            return schools;
        }

        
       
        // PUT: api/MOE_SCHOOL/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE: api/MOE_SCHOOL/5
        public void Delete(int id)
        {
        }

        #region added code for close and share data to NSIS modified by Veer on 16 July 2018
        // POST: api/MOE_SCHOOL
        public DBOperationResult Post(MOE_SCHOOL_Model oModel)
        {
            return MOE_SCHOOL_Repository.Update(oModel);
        }
        [Route("api/GetSchoolsListDB/")]
        public List<MOE_SCHOOL_Model> Get()
        {
            List<MOE_SCHOOL_Model> schools = MOE_SCHOOL_Repository.GetSchoolsListDB();
            return schools;
        }
        #endregion
    }
}
