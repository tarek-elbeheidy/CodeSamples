using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json;

namespace MOEHE.PSPES.WebAPI.Repository.NSIS
{
    public partial class MarksData
    {
        [JsonProperty("GetMarksResult")]
        public GetMarksResult GetMarksResult { get; set; }
    }

    public partial class GetMarksResult
    {
        [JsonProperty("Semester1")]
        public Semester[] Semester1 { get; set; }

        [JsonProperty("Semester2")]
        public Semester[] Semester2 { get; set; }
    }

    public partial class Semester
    {
        [JsonProperty("Percentage")]
        public long Percentage { get; set; }

        [JsonProperty("SchoolCourse")]
        public string SchoolCourse { get; set; }
    }

    public partial class MarksData
    {
        public static MarksData FromJson(string json)
        {
            return JsonConvert.DeserializeObject<MarksData>(json, Converter.Settings);
        }
    }

}
