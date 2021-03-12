using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ITWORX.MOEHEWF.Common.Entities
{
   [Serializable()]
    public class University
    {
        public int ID { get; set; }
        public string EnglishTitle { get; set; }
        public string ArabicTitle { get; set; }
        public string SelectedID { get; set; }
        public string SelectedTitle { get; set; }
        public CountryOfStudy CountryOfStudy { get; set; }
        public bool HEDD { get; set; }
        public int Year { get; set; }
        public bool CHED { get; set; }
        public bool IsOnline { get; set; }
    }
}
