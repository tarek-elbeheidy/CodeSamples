using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MOEHE.PSPES.WebAPI.Models
{
    [Serializable]
    public class ListOfValues_Model
    {

        public string Code { get; set; }
        public string DescriptionArabic { get; set; }
        public string DescriptionEnglish { get; set; }
        public string ID { get; set; }
        public List<CustomCodes> customCodes = new List<CustomCodes>();
    }
    [Serializable]
    public class CustomCodes
    {
        public string Code { get; set; }
        public string DescriptionArabic { get; set; }
        public string DescriptionEnglish { get; set; }
    }
}