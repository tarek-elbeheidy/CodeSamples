using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ITWORX.MOEHEWF.UCE.Entities
{
    public class Applicants
    {
        public string NationalID { get; set; }
        public DateTime BirthDate { get; set; }
        public Nationality Nationality { get; set; }
        public NationalityCategory NationalityCategory { get; set; }
        public string MobileNumber { get; set; }
        public string Email { get; set; }
        public ApplicantsAttachments ApplicantsAttachments { get; set; }
    }
}
