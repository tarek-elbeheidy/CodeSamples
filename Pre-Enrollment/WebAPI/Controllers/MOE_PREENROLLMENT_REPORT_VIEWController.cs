using MOEHE.PSPES.WebAPI.Models;
using MOEHE.PSPES.WebAPI.Repository;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Web.Http;

namespace MOEHE.PSPES.WebAPI.Controllers
{
    public class MOE_PREENROLLMENT_REPORT_VIEWController : ApiController
    {
        // GET: api/MOE_PREENROLLMENT_REPORT_VIEW
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        [Route("api/GetReportByQid/{QID}")]
        public List<MOE_PREENROLLMENT_REPORT_VIEW_Model> GetByQID(string QID)
        {
            return MOE_PREENROLLMENT_REPORT_VIEW_Repository.GetDataByQID(QID);
        }
        [HttpPost]
        [Route("api/GetReportData")]
        public DataTable GetReportData(Dictionary<string, string> dictionary)
        {
            return MOE_PREENROLLMENT_REPORT_VIEW_Repository.GetAllData(dictionary);
        }

        // POST: api/MOE_PREENROLLMENT_REPORT_VIEW
        public void Post([FromBody]string value)
        {
        }

        // PUT: api/MOE_PREENROLLMENT_REPORT_VIEW/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE: api/MOE_PREENROLLMENT_REPORT_VIEW/5
        public void Delete(int id)
        {
        }
    }
}
