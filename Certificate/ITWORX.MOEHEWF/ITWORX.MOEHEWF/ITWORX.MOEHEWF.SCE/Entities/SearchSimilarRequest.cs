using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ITWORX.MOEHEWF.SCE.Entities
{
    public class SearchSimilarRequest
    {
        public int? Id { get; set; }
        public string RequestNumber { get; set; }
        public string RequestDate { get; set; }
        public string ApplicantNumber { get; set; }
        public string ApplicantName { get; set; }
        public string Nationality { get; set; }
        public string CertificateOrigin { get; set; }
        public string SchoolType { get; set; }
        public string CertificateType { get; set; }
        public string RequestUrl { get; set; }
        public int RequestStatusId { get; set; }
        public int RegisteredSchoolId { get; set; }
    }
}
