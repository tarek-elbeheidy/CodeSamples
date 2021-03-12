using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json;

namespace MOEHE.PSPES.WebAPI.Repository.NSIS
{
    public partial class EnrollmentsData
    {
        [JsonProperty("GetEnrollmentsResult")]
        public GetEnrollmentsResult[] GetEnrollmentsResult { get; set; }
    }

    public partial class GetEnrollmentsResult
    {
        [JsonProperty("EntryDate")]
        public System.DateTime EntryDate { get; set; }

        [JsonProperty("ExitDate")]
        public object ExitDate { get; set; }

        [JsonProperty("GradeLevel")]
        public string GradeLevel { get; set; }

        [JsonProperty("HasPendingPayment")]
        public bool HasPendingPayment { get; set; }

        [JsonProperty("Homeroom")]
        public string Homeroom { get; set; }

        [JsonProperty("PromotionStatus")]
        public object PromotionStatus { get; set; }

        [JsonProperty("SchoolID")]
        public string SchoolId { get; set; }

        [JsonProperty("SchoolYearID")]
        public long SchoolYearId { get; set; }

        [JsonProperty("StudentID")]
        public string StudentId { get; set; }
    }

    public partial class EnrollmentsData
    {
        public static EnrollmentsData FromJson(string json)
        {
            return JsonConvert.DeserializeObject<EnrollmentsData>(json, Converter.Settings);
        }
    }
}
