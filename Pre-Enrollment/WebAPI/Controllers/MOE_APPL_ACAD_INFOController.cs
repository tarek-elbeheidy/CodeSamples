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
    public class MOE_APPL_ACAD_INFOController : ApiController
    {
        // GET: api/MOE_APPL_ACAD_INFO
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET: api/MOE_APPL_ACAD_INFO/5
        [Route("api/GetAcadInfo/{id}/{requestedTerm}")]
        public MOE_APPL_ACAD_INFO_Model Get(string id, int requestedTerm)
        {
            MOE_APPL_ACAD_INFO_Model model = MOE_APPL_ACAD_INFO_Repository.Get(id,requestedTerm);
            return model;//MOE_BIO_DATA_Repository.Get(id).Result;
        }

        // GET: api/MOE_APPL_ACAD_INFO/5
        [Route("api/GetAcadInfoByRef/{application_reference_number}")]
        public List<MOE_APPL_ACAD_INFO_Model> Get(string application_reference_number)
        {
            List<MOE_APPL_ACAD_INFO_Model> models = MOE_APPL_ACAD_INFO_Repository.GetACADInfoByAppRef(application_reference_number);
            return models;//MOE_BIO_DATA_Repository.Get(id).Result;
        }

        public DBOperationResult Post(MOE_APPL_ACAD_INFO_Model app_acadDataModel)
        {
            //note : this used for insert and update
            return MOE_APPL_ACAD_INFO_Repository.Insert(app_acadDataModel);

        }

        // PUT: api/MOE_APPL_ACAD_INFO/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE: api/MOE_APPL_ACAD_INFO/5
        public void Delete(int id)
        {
        }

        #region Added new web method - Withdrawal Form by Veer on 11 July 2018
        // GET: api/MOE_APPL_ACAD_INFO/5
        [Route("api/GetAcadamicInfo/{id}")]
        public MOE_APPL_ACAD_INFO_Model GetInfo(string id)
        {
            MOE_APPL_ACAD_INFO_Model model = MOE_APPL_ACAD_INFO_Repository.GetData(id);
            return model;
        }
        #endregion
    }
}
