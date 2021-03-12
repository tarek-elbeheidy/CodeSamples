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
    public class SeatReservationFeesController : ApiController
    {
        // GET: api/SeatReservationFees
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        [Route("api/CheckConfirmedApplications/{QID}")]
        public List<SeatReservationFeeModel> GetByQID(string QID)
        {
            List<SeatReservationFeeModel> list = SeatReservationFeesRepository.GetByQID(QID);
            return list;
        }

        [Route("api/GetSeatReservationFee/{ApplicationReference}")]
        public List<SeatReservationFeeModel> Get(string ApplicationReference)
        {
            List<SeatReservationFeeModel> list = SeatReservationFeesRepository.Get(ApplicationReference);
            return list;
        }

        [Route("api/GetSeatReservationFeeByRefAndID/{ApplicationReference}/{QID}")]
        public List<SeatReservationFeeModel> GetByRefAndID(string ApplicationReference,string QID)
        {
            List<SeatReservationFeeModel> list = SeatReservationFeesRepository.GetByRefAndID(ApplicationReference,QID);
            return list;
        }


        [Route("api/GetSeatReservationFee/{Term}/{SchoolCode}/{Grade}")]
        public int Get(int? Term,string SchoolCode,string Grade)
        {
            int ApplicationCount = SeatReservationFeesRepository.Get(Term,SchoolCode,Grade);
            return ApplicationCount;
        }


        [Route("api/GetSeatReservationFee/{Term}/{SchoolCode}")]
        public int GetAllConfirmed(int? Term, string SchoolCode)
        {
            int ApplicationCount = SeatReservationFeesRepository.GetAllConfirmed(Term, SchoolCode);
            return ApplicationCount;
        }


        public DBOperationResult Post(SeatReservationFee  seatReservationFee)
        {
            //note : this used for insert and update
            return SeatReservationFeesRepository.Insert(seatReservationFee);

        }

        // PUT: api/SeatReservationFees/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE: api/SeatReservationFees/5
        public void Delete(int id)
        {
        }
    }
}
