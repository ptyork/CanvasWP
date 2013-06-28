using Newtonsoft.Json;
using System;

namespace CanvasWP.API
{
    class DiscussionTopic
    {
        [JsonProperty("created_at")]
        public string CreatedAt { get; set; }

        [JsonProperty("has_more_replies")]
        public bool? HasMoreReplies { get; set; }

        [JsonProperty("id")]
        public long Id { get; set; }

        [JsonProperty("message")]
        public string Message { get; set; }

        [JsonProperty("recent_replies")]
        public TopicComment[] RecentReplies { get; set; }

        [JsonProperty("updated_at")]
        public string UpdatedAt { get; set; }

        [JsonProperty("user_id")]
        public long UserId { get; set; }

        [JsonProperty("user_name")]
        public string UserName { get; set; }
    }
}
