using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using MOEHE.PSPES.WebAPI.Models;
using MOEHE.PSPES.WebAPI.Repository;

namespace MOEHE.PSPES.WebAPI.Controllers
{
    public class InterviewResultController : ApiController
    {

        [Route("api/GetInterviewResult/{AppRefNum}/{QID}")]
        [HttpGet]
        public List<InterviewResultModel> Get(string AppRefNum,string QID )
        {
            List<InterviewResultModel> list = InterviewResultRepository.GetBy(AppRefNum,QID);
            return list;
        }

        // PUT: api/AddedCapacity/5
        public DBOperationResult Post(InterviewResultModel IntrResultModel)
        {

            return InterviewResultRepository.Insert(IntrResultModel);

        }


    }
}
