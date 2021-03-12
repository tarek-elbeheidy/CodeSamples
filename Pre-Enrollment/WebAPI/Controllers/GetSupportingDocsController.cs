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
    public class GetSupportingDocsController : ApiController
    {
        // GET: api/GetSupportingDocs
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET: api/GetSupportingDocs/5
        public string Get(int id)
        {
            return "value";
        }

        // POST: api/GetSupportingDocs
        public List<SupportingDocsModel> Post(SupportingDocsModel supportingDocumentModel)
        {
            List<SupportingDocsModel> list = new List<SupportingDocsModel>();


            if (supportingDocumentModel.SeacrhByAll)
            {
              list  = SupportingDocsRepository.Get(supportingDocumentModel.Term, supportingDocumentModel.SchoolCode, supportingDocumentModel.Grade, supportingDocumentModel.Curriculum, supportingDocumentModel.DocumentTypeID);

            }
            else if (supportingDocumentModel.SeacrhByTermAndSchoolCode)
            {
               list= SupportingDocsRepository.GetBy(supportingDocumentModel.Term, supportingDocumentModel.SchoolCode, "",supportingDocumentModel.CurriculumID);
            }
            else if(supportingDocumentModel.SeacrhByTermAndSchoolCodeAndGrade)
            {
                list = SupportingDocsRepository.GetBy(supportingDocumentModel.Term, supportingDocumentModel.SchoolCode, supportingDocumentModel.Grade, supportingDocumentModel.CurriculumID);

            }
            else if (supportingDocumentModel.SeacrhByTermAndSchoolCodeAndGradeAndDocumentType)
            {
                list = SupportingDocsRepository.GetBy(supportingDocumentModel.Term, supportingDocumentModel.SchoolCode, supportingDocumentModel.Grade,int.Parse(supportingDocumentModel.DocumentTypeID.ToString()),supportingDocumentModel.CurriculumID);

            }
            else if (supportingDocumentModel.SeacrhByTermAndSchoolCodeAndDocumentType)
            {
                list = SupportingDocsRepository.GetBy(supportingDocumentModel.Term, supportingDocumentModel.SchoolCode,int.Parse(supportingDocumentModel.DocumentTypeID.ToString()),supportingDocumentModel.CurriculumID);

            }
            return list;
        }

        // PUT: api/GetSupportingDocs/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE: api/GetSupportingDocs/5
        public void Delete(int id)
        {
        }
    }
}
