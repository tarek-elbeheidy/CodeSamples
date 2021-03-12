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
    public class SupportingDocsController : ApiController
    {
        // GET: api/SupportingDocs
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET: api/SupportingDocs/5
        public string Get(int id)
        {
            return "value";
        }

        [Route("api/GetSupportingDocss/{Term}/{schoolCode}/{Grade}/{Curriculum}/{DocumentType}")]
        public List<SupportingDocsModel> Get(string Term, string schoolCode, string Grade,string Curriculum,int DocumentType)
        {
            List<SupportingDocsModel> list = SupportingDocsRepository.Get(Term, schoolCode, Grade,Curriculum,DocumentType);
            return list;
        }

        // POST: api/SupportingDocs
        public DBOperationResult Post(SupportingDocsModel  supportingDocumentModel)
        {
            //note : this used for insert and update
            return SupportingDocsRepository.Insert(supportingDocumentModel);

        }

        // PUT: api/SupportingDocs/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE: api/SupportingDocs/5
        public void Delete(int id)
        {
        }
    }
}
