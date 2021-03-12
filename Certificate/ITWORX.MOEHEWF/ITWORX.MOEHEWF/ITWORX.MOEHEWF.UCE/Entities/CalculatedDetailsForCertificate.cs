using ITWORX.MOEHE.Utilities.Logging;
using ITWORX.MOEHEWF.Common.Entities;
using Microsoft.SharePoint;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ITWORX.MOEHEWF.UCE.Entities
{
   
  public   class CalculatedDetailsForCertificate
    {
        public int ID { get; set; }
        public CountryOfStudy Country { get; set; }
        public string OtherCountry { get; set; }
        public University Univesrity { get; set; }
        public string OtherUniversity { get; set; }
        public string Faculty { get; set; }
        public string OtherFaculty { get; set; }
        public StudySystem StudySystem { get; set; }
       // public StudyType StudyType { get; set; }
        public Request Request { get; set; }
        public string StudyingPeriod { get; set; }


    }
}
