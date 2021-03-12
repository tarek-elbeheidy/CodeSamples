using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ITWORX.MOEHEWF.Common.Entities
{
  
    public class Applicants
    {
        public int ID { get; set; }
        public string PersonalID { get; set; }
        public string ApplicantName { get; set; }
        public DateTime BirthDate { get; set; }
        public Nationality Nationality { get; set; }
        public NationalityCategory NationalityCategory { get; set; }
        public string MobileNumber { get; set; }
        public string ApplicantEmail { get; set; }
        public int Region { get; set; }
        public int Street { get; set; }
        public int BuildingNo { get; set; }
        public int ApartmentNo { get; set; }
        public int PostalNumber { get; set; }
        public string DetailedAddress { get; set; }
        public ApplicantsAttachments ApplicantsAttachments { get; set; }
        public bool ApplicantGender { get; set; }

        public string ArabicName { get; set; }
        public string EnglishName { get; set; }
    }
}
