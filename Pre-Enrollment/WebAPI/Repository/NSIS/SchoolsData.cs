using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json;

namespace MOEHE.PSPES.WebAPI.Repository.NSIS
{
    public partial class SchoolsData
    {
        [JsonProperty("GetSchoolsResult")]
        public GetSchoolsResult[] GetSchoolsResult { get; set; }
    }

    public partial class GetSchoolsResult
    {
        [JsonProperty("ArabicName")]
        public string ArabicName { get; set; }

        [JsonProperty("CategoryCode")]
        public string CategoryCode { get; set; }

        [JsonProperty("CategoryDescription")]
        public string CategoryDescription { get; set; }

        [JsonProperty("EnglishName")]
        public string EnglishName { get; set; }

        [JsonProperty("GradeLevels")]
        public GradeLevel[] GradeLevels { get; set; }

        [JsonProperty("ID")]
        public string Id { get; set; }

        [JsonProperty("ProgramTypeDescription")]
        public string ProgramTypeDescription { get; set; }

        [JsonProperty("ProgramTypeID")]
        public string ProgramTypeId { get; set; }

        [JsonProperty("StudentGenderCode")]
        public string StudentGenderCode { get; set; }
    }

    public partial class GradeLevel
    {
        [JsonProperty("Code")]
        public string Code { get; set; }

        [JsonProperty("Hommerooms")]
        public Hommeroom[] Hommerooms { get; set; }
    }

    public partial class Hommeroom
    {
        [JsonProperty("Capacity")]
        public long Capacity { get; set; }

        [JsonProperty("EnrollmentCounts")]
        public long EnrollmentCounts { get; set; }

        [JsonProperty("Name")]
        public string Name { get; set; }
    }

    public partial class SchoolsData
    {
        public static SchoolsData FromJson(string json)
        {
            return JsonConvert.DeserializeObject<SchoolsData>(json, Converter.Settings);
        }
    }
}
