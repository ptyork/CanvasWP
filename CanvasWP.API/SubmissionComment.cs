using Newtonsoft.Json;
using System;

namespace CanvasWP.API
{
    public class SubmissionComment
    {
        [JsonProperty("author_id")]
        public long AuthorId { get; set; }

        [JsonProperty("author_name")]
        public string AuthorName { get; set; }

        [JsonProperty("comment")]
        public string Comment { get; set; }

        [JsonProperty("body")]
        public string Body { get; set; }

        [JsonProperty("created_at")]
        public string CreatedAt { get; set; }
    }
}
