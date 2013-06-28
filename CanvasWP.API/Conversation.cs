using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace CanvasWP.API
{
    public class Conversation
    {
        // A struct containing two dictionaries. Each is keyed by the course/group id and
        // contains a list of strings representing the membership role(s) for the course/group.
        public struct AudienceContextStruct
        {
            public Dictionary<long, List<string>> Courses { get; set; }
            public Dictionary<long, List<string>> Groups { get; set; }
        }

        [JsonProperty("avatar_url")]
        public string AvatarUrl { get; set; }

        [JsonProperty("audience_contexts")]
        public AudienceContextStruct AudienceContexts { get; set; }

        [JsonProperty("last_message")]
        public string LastMessage { get; set; }

        [JsonProperty("last_message_at")]
        [JsonConverter(typeof(Newtonsoft.Json.Converters.IsoDateTimeConverter))]
        public DateTime LastMessageAt { get; set; }

        [JsonProperty("private_")]
        public bool IsPrivate { get; set; }

        [JsonProperty("id")]
        public long Id { get; set; }

        [JsonProperty("message_count")]
        public int? MessageCount { get; set; }

        [JsonProperty("audience")]
        public List<long> Audience { get; set; }

        //public string workflow_state;
        [JsonProperty("workflow_state")]
        public string WorkflowState { get; set; }

        [JsonProperty("participants")]
        public List<User> Participants { get; set; }

        [JsonProperty("messages")]
        public List<Message> Messages { get; set; }

        [JsonProperty("starred")]
        public bool Starred { get; set; }

        [JsonProperty("subscribed")]
        public bool Subscribed { get; set; }

        [JsonProperty("properties")]
        public List<string> Properties { get; set; }

        [JsonProperty("submissions")]
        public List<Submission> Submissions { get; set; }
    }
}
