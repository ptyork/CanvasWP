using Newtonsoft.Json;
using System;

namespace CanvasWP.API
{
    public class StreamItemConversation : StreamItemBase
    {
        [JsonProperty("conversation_id")]
        public long ConversationId { get; set; }

        [JsonProperty("private")]
        public bool IsPrivate { get; set; }

        [JsonProperty("participant_count")]
        public int? ParticipantCount { get; set; }

    }
}
