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
    public class MOE_APPL_SUPPORT_DOCSController : ApiController
    {
        // GET: api/MOE_APPL_SUPPORT_DOCS
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET: api/MOE_APPL_SUPPORT_DOCS/5
        public string Get(int id)
        {
            return "value";
        }

        [Route("api/GetApplicationSupportingDocs/{ApplicantReference}/{QID}")]
        public List<MOE_APPL_SUPPORT_DOCS_Model> Get(string ApplicantReference,string QID)
        {
            List<MOE_APPL_SUPPORT_DOCS_Model>  supporitngDocs = MOE_APPL_SUPPORT_DOCS_Repository.GetBy(ApplicantReference,QID);
            return supporitngDocs;//MOE_BIO_DATA_Repository.Get(id).Result;
        }

        public DBOperationResult Post(MOE_APPL_SUPPORT_DOCS_Model_Not_Serializable app_support_docsDataModel)
        {
            //note : this used for insert and update
            return MOE_APPL_SUPPORT_DOCS_Repository.Insert(app_support_docsDataModel);

        }

        // PUT: api/MOE_APPL_SUPPORT_DOCS/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE: api/MOE_APPL_SUPPORT_DOCS/5
        public void Delete(int id)
        {
        }
    }
}
