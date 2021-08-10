using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace StuudentEntry.Models
{
    public class Marks
    {
        [JsonProperty(PropertyName = "id")]
        public string Id { get; set; }
        public string marks1 { get; set; }
        public string marks2 { get; set; }
        public string marks3 { get; set; }

        public string percent { get; set; }

    }
}