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
    public class TestResultController : ApiController
    {

        [Route("api/GetTestResult/{AppRefNum}/{QID}")]
        [HttpGet]
        public List<TestResultModel> Get(string AppRefNum,string QID)
        {
            List<TestResultModel> list = TestResultRepository.GetBy(AppRefNum, QID);
            return list;
        }

        // PUT: api/AddedCapacity/5
        public DBOperationResult Post(TestResultModel tstResultModel)
        {

            return TestResultRepository.Insert(tstResultModel);

        }


    }
}
