using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json;

namespaceMOEHE.PSPES.WebAPI.Repository.NSIS
{
    public partial class NsisEntity
    {
        [JsonProperty("GetCodesResult")]
        public GetCodesResult[] GetCodesResult { get; set; }
    }

    public partial class GetCodesResult
    {
        [JsonProperty("Code")]
        public string Code { get; set; }

        [JsonProperty("CustomCodes")]
        public object[] CustomCodes { get; set; }

        [JsonProperty("DescriptionArabic")]
        public string DescriptionArabic { get; set; }

        [JsonProperty("DescriptionEnglish")]
        public string DescriptionEnglish { get; set; }

        [JsonProperty("ID")]
        public string Id { get; set; }
    }

    public partial class NsisEntity
    {
        public static NsisEntity FromJson(string json)
        {
            return JsonConvert.DeserializeObject<NsisEntity>(json, Converter.Settings);
        }
    }

    public static class Serialize
    {
        public static string ToJson(this NsisEntity self)
        {
            return JsonConvert.SerializeObject(self, Converter.Settings);
        }
    }

    public class Converter
    {
        public static readonly JsonSerializerSettings Settings = new JsonSerializerSettings
        {
            MetadataPropertyHandling = MetadataPropertyHandling.Ignore,
            DateParseHandling = DateParseHandling.None,
        };
    }
}
