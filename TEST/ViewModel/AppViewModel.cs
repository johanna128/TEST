using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TEST.ViewModel
{
    public class AppViewModel
    { 
        public IList<AppData> AppDataList { get; set; }
        public string SuccessMessage { get; set; }
    }

    public class AppData
    {
        [JsonProperty("name")]
        public string name { get; set; }

        [JsonProperty("chrome_web_origin")]
        public string chrome_web_origin { get; set; }

        [JsonProperty("id")]
        public string id { get; set; }
    }
}