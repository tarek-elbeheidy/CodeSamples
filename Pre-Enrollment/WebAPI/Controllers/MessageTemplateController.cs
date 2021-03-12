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
    public class MessageTemplateController : ApiController
    {
        // GET: api/MessageTemplate
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        [Route("api/GetMessageTemplate/{MessageTemplateTitle}/{MessageTemplateID}")]
        public MessageTemplate Get(string MessageTemplateTitle, int MessageTemplateID)
        {
            MessageTemplate messageTemplate = MessageTemplateRepository.GetBy(MessageTemplateTitle,MessageTemplateID);
            return messageTemplate;
        }

        // GET: api/MessageTemplate/5
        public string Get(int id)
        {
            return "value";
        }

        // POST: api/MessageTemplate
        public void Post([FromBody]string value)
        {
        }

        // PUT: api/MessageTemplate/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE: api/MessageTemplate/5
        public void Delete(int id)
        {
        }
    }
}
