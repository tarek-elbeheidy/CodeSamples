using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Newtonsoft.Json;

namespace MOEHE.PSPES.WebAPI.Repository.NSIS
{

    // To parse this JSON data, add NuGet 'Newtonsoft.Json' then do:
    //
    //    using QuickType;
    //
    //    var welcome = Welcome.FromJson(jsonString);

  
        public partial class ListOfValueData
    {
            [JsonProperty("GetCodesResult")]
            public List<GetCodesResult> GetCodesResult { get; set; }
        }

        public partial class GetCodesResult
        {
            [JsonProperty("Code")]
            public string Code { get; set; }

            [JsonProperty("CustomCodes")]
            public List<CustomCode> CustomCodes { get; set; }

            [JsonProperty("DescriptionArabic")]
            public string DescriptionArabic { get; set; }

            [JsonProperty("DescriptionEnglish")]
            public string DescriptionEnglish { get; set; }

            [JsonProperty("ID")]
            public string Id { get; set; }
        }

        public partial class CustomCode
        {
            [JsonProperty("Code")]
            public string Code { get; set; }

            [JsonProperty("DescriptionArabic")]
            public string DescriptionArabic { get; set; }

            [JsonProperty("DescriptionEnglish")]
            public string DescriptionEnglish { get; set; }
        }

        public partial class ListOfValueData
    {
            public static ListOfValueData FromJson(string json) => JsonConvert.DeserializeObject<ListOfValueData>(json, Converter.Settings);
        }

        public static class Serialize
        {
            public static string ToJson(this ListOfValueData self) => JsonConvert.SerializeObject(self, Converter.Settings);
        }

        
    }


