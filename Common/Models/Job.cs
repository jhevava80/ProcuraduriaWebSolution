using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace Common.Models
{
    public class Job
    {
        [JsonProperty("id")]
        public long Id { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("createdDate")]
        public DateTimeOffset CreatedDate { get; set; }

        [JsonProperty("modifiedDate")]
        public DateTimeOffset ModifiedDate { get; set; }

        [JsonProperty("status")]
        public object Status { get; set; }

        [JsonProperty("user")]
        public object User { get; set; }
    }
}
