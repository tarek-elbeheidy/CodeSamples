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
    public class Enrollment_Exception_AttachmentController : ApiController
    {
        // GET: api/Enrollment_Exception_Attachment
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET: api/Enrollment_Exception_Attachment/5
        [Route("api/GetExceptionAttachments/{ExceptionID}")]
        public List<Enrollment_Exception_Attachment_Model>Get(int ExceptionID)
        {
            return ENROLLMENT_EXCEPTION_ATTACHMENT_Repository.Get(ExceptionID);
        }

        // POST: api/Enrollment_Exception_Attachment
        public DBOperationResult Post(Enrollment_Exception_Attachment_Model Exception_Attachment)
        {
            DBOperationResult result = ENROLLMENT_EXCEPTION_ATTACHMENT_Repository.Insert(Exception_Attachment);
            return result;
        }

        // PUT: api/Enrollment_Exception_Attachment/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE: api/Enrollment_Exception_Attachment/5
        public void Delete(int id)
        {
        }
    }
}
