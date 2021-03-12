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
    public class AddedCapacityController : ApiController
    {
        // GET: api/AddedCapacity
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET: api/AddedCapacity/5
        public string Get(int id)
        {
            return "value";
        }

        [Route("api/GetAddedCapacity/{Year}/{SchoolCode}")]
        public List<AddedCapacityModel> Get(int Year, string SchoolCode)
        {
            List<AddedCapacityModel> list = AddedCapacityRepository.GetADC(Year,SchoolCode);
            return list;
        }

        // PUT: api/AddedCapacity/5
        public DBOperationResult Post(AddedCapacityModel AddedCapModel)
        {
            //note : this used for insert and update
            return AddedCapacityRepository.Insert(AddedCapModel);

        }

        // DELETE: api/AddedCapacity/5
        public void Delete(int id)
        {
        }
    }
}
