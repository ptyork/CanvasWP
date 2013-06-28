using Newtonsoft.Json;
using System;

namespace CanvasWP.API
{
    public class AssignmentGroup
    {
        [JsonProperty("position")]
        public int? Position { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        //[JsonProperty("rules")]
        //public int Rules { get; set; }

        [JsonProperty("group_weight")]
        public int? GroupWeight { get; set; }

        [JsonProperty("id")]
        public long Id { get; set; }
    }
}
