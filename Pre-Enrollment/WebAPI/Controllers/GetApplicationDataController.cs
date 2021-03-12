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
    public class GetApplicationDataController : ApiController
    {
        // GET: api/GetApplicationData
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET: api/GetApplicationData/5
        public string Get(int id)
        {
            return "value";
        }

        // POST: api/GetApplicationData
        [Route("api/SearchApplications/")]
        [HttpPost]
        public List<ApplicationDataModel> Post(SearchApplicationPageFilters searchapplicationpagefilters)
        {
            TransactionLogsRepository.Insert(new TransactionsLog { CreatedDate = DateTime.Now, ShortDescription = "Application Data", Description = "inside controller", UserID = "" });
            List<ApplicationDataModel> list = new List<ApplicationDataModel>();
            TransactionLogsRepository.Insert(new TransactionsLog { CreatedDate = DateTime.Now, ShortDescription = "Application Data", Description = "after define list", UserID = "" });
            TransactionLogsRepository.Insert(new TransactionsLog { CreatedDate = DateTime.Now, ShortDescription = "Application Data", Description = "before calling search by,"+ Environment.MachineName, UserID = "" });
            list = MOE_APPLICATION_DATA_Repository.SearchBy(searchapplicationpagefilters);
            TransactionLogsRepository.Insert(new TransactionsLog { CreatedDate = DateTime.Now, ShortDescription = "Application Data", Description = "after calling search by", UserID = "" });
            TransactionLogsRepository.Insert(new TransactionsLog { CreatedDate = DateTime.Now, ShortDescription = "Application Data", Description = "before rturn controller, count"+list.Count.ToString(), UserID = "" });
            return list;
        }

            // PUT: api/GetApplicationData/5
            public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE: api/GetApplicationData/5
        public void Delete(int id)
        {
        }
    }
}
