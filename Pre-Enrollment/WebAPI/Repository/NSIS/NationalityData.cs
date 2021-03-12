using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json;

namespace MOEHE.PSPES.WebAPI.Repository.NSIS
{
    public partial class NationalitiyData
    {
        [JsonProperty("GetCodesResult")]
        public GetCodesResult[] GetCodesResult { get; set; }
    }

   
    public partial class NationalitiyData
    {
        public static NationalitiyData FromJson(string json)
        {
            return JsonConvert.DeserializeObject<NationalitiyData>(json, Converter.Settings);
        }
    }
}
