using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace AndroidBinderator.Model
{
    public class DevOpsConfig
    {
        [JsonProperty("organization")]
        public string Organization { get; set; }

        [JsonProperty("feed")]
        public string Feed { get; set; }

        [JsonProperty("personalAccessToken")]
        public string PersonalAccessToken { get; set; }
    }
}
