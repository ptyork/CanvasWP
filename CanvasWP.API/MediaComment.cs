using Newtonsoft.Json;
using System;

namespace CanvasWP.API
{
    public class MediaComment
    {
        [JsonProperty("media_id")]
        public long? MediaId { get; set; }

        [JsonProperty("media_type")]
        public string MediaType { get; set; }

        [JsonProperty("display_name")]
        public string DisplayName { get; set; }

        [JsonProperty("content_type")]
        public string ContentType { get; set; }

        [JsonProperty("url")]
        public string Url { get; set; }
    }
}
