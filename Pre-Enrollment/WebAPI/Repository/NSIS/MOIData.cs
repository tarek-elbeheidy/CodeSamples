using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;

namespace MOEHE.PSPES.WebAPI.Repository.NSIS
{
   
        public partial class MOIData
        {
            [JsonProperty("QatarId")]
            public string QatarId { get; set; }

            [JsonProperty("EnglishName")]
            public string EnglishName { get; set; }

            [JsonProperty("ArabicName")]
            public string ArabicName { get; set; }

            [JsonProperty("DOB")]
            public string Dob { get; set; }

            [JsonProperty("Gender")]
            public string Gender { get; set; }

            [JsonProperty("CountryNameAr")]
            public string CountryNameAr { get; set; }

            [JsonProperty("CountryNameEn")]
            public string CountryNameEn { get; set; }

            [JsonProperty("CountryCode")]
            public string CountryCode { get; set; }

            [JsonProperty("Status")]
            public string Status { get; set; }

            [JsonProperty("StatusDate")]
            public string StatusDate { get; set; }

            [JsonProperty("LastUpdatedDate")]
            public string LastUpdatedDate { get; set; }
        }

        public partial class MOIData
    {
            public static MOIData FromJson(string json) => JsonConvert.DeserializeObject<MOIData>(json,Converter.Settings);
        }

        public static class MOISerialize
        {
            public static string ToJson(this MOIData self) => JsonConvert.SerializeObject(self, Converter.Settings);
        }

        internal class Converter
        {
            public static readonly JsonSerializerSettings Settings = new JsonSerializerSettings
            {
                MetadataPropertyHandling = MetadataPropertyHandling.Ignore,
                DateParseHandling = DateParseHandling.None,
                Converters = {
                new IsoDateTimeConverter()
                {
                    DateTimeStyles = DateTimeStyles.AssumeUniversal,
                },
            },
            };
        }
    
}