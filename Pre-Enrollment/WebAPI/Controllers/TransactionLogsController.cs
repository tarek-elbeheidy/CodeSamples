
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
    public class TransactionLogsController : ApiController
    {


        // GET: api/TransactionsLog/
        //[Route("api/GetTransactionLog/")]
        //public List<TransactionsLog> Get()
        //{
        //    List<TransactionsLog> allTransactions = TransactionLogsRepository.Get();
        //    return allTransactions;
        //}
        // GET: api/TransactionsLog/
        //[Route("api/GetTransactionLogsByUserID/{UserID}")]
        //public List<TransactionsLog> Get()
        //{
        //    List<TransactionsLog> allTransactions = TransactionLogsRepository.Get();
        //    return allTransactions;
        //}


        // POST: api/TransactionsLog
        [Route("api/TransactionsLog")]
        public void Post(TransactionsLog Log)
        {
            TransactionLogsRepository.Insert(Log);
        }

        // PUT: api/TransactionsLog/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE: api/TransactionsLog/5
        public void Delete(int id)
        {
        }
    }
}
