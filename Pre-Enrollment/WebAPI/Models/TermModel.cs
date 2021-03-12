using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MOEHE.PSPES.WebAPI.Models
{
    public class TermModel
    {
        public string TermName { set; get; }
        public string TermCode { set; get; }
        public bool IsActive { set; get; }
        public string ACADEMIC_YEAR_DESC { get; set; }
    }
}