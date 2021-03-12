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
    public class AddedGradeController : ApiController
    {


        [Route("api/GetAddedGrades/{Year}/{SchoolCode}")]
        public List<AddedGradeModel> Get(int Year, string SchoolCode)
        {
            List<AddedGradeModel> list = AddedGradeRepository.GetAddedGrades(Year, SchoolCode);
            return list;
        }

        // PUT: api/AddedCapacity/5
        public DBOperationResult Post(AddedGradeModel AddedGRDModel)
        {
            //note : this used for insert and update
            return AddedGradeRepository.Insert(AddedGRDModel);

        }


    }
}
