using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOEHE.PSPES.Models
{
   public class SeatReservationFeeModel
    {
        public int ID { get; set; }
        public Nullable<decimal> NationalID { get; set; }
        public string ApplicantReferenceNumber { get; set; }
        public Nullable<long> ReservationFeesAmount { get; set; }
        public Nullable<System.DateTime> FeesPaidDate { get; set; }
        public Nullable<System.DateTime> CreatedDate { get; set; }
        public string CreatedBy { get; set; }
        public Nullable<bool> IsActive { get; set; }

    }
    public partial class SeatReservationFee
    {
        public int ID { get; set; }
        public Nullable<decimal> NationalID { get; set; }
        public string ApplicantReferenceNumber { get; set; }
        public Nullable<long> ReservationFeesAmount { get; set; }
        public Nullable<System.DateTime> FeesPaidDate { get; set; }
        public Nullable<System.DateTime> CreatedDate { get; set; }
        public string CreatedBy { get; set; }
    }


}
