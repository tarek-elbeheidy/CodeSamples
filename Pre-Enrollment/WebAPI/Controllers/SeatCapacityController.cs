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
    public class SeatCapacityController : ApiController
    {
        [Route("api/GetSeatCapacity/{Term}/{schoolCode}/{Grade}/{seatDistrib}/{PreEnrollSeats}")]
        public List<SeatCapacityModel> Get(int Term, string schoolCode, string Grade, int seatDistrib, int PreEnrollSeats)
        {
            List<SeatCapacityModel> list = SeatCapacityRepository.Get( Term,  schoolCode,  Grade,  seatDistrib,  PreEnrollSeats);
            return list;
        }


        [Route("api/CheckExistsSeatCapacity/{Term}/{schoolCode}/{Grade}")]
        [HttpGet]
        public List<SeatCapacityModel> CheckExist(int Term, string schoolCode, string Grade)
        {
            List<SeatCapacityModel> list = SeatCapacityRepository.CheckExist(Term, schoolCode, Grade);
            return list;
        }

        [Route("api/CheckNumberofGrades/{Term}/{schoolCode}")]
        [HttpGet]
        public List<SeatCapacityModel> CheckNumberofGrades(int Term, string schoolCode)
        {
            List<SeatCapacityModel> list = SeatCapacityRepository.CheckNumberofGrades(Term, schoolCode);
            return list;
        }


        // PUT: api/AddedCapacity/5
        public DBOperationResult Post(SeatCapacityModel SeatCapModel)
        {
            //note : this used for insert and update
            return SeatCapacityRepository.Insert(SeatCapModel);

        }


    }
}
