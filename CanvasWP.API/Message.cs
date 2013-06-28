using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace CanvasWP.API
{
    public class Message
    {
        [JsonProperty("id")]
        public long Id { get; set; }

        [JsonProperty("created_at")]
        [JsonConverter(typeof(Newtonsoft.Json.Converters.IsoDateTimeConverter))]
        public DateTime CreatedAt { get; set; }

        [JsonProperty("body")]
        public string Body { get; set; }

        [JsonProperty("author_id")]
        public long? AuthorId { get; set; }

        [JsonProperty("generated")]
        public bool SystemGenerated { get; set; }

        [JsonProperty("media_comment")]
        public MediaComment MediaComment { get; set; }

        [JsonProperty("forwarded_messages")]
        public List<Message> ForwardedMessages { get; set; }

        [JsonProperty("attachments")]
        public List<Attachment> Attachments { get; set; }
    }
}
