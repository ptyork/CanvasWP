using Newtonsoft.Json;
using System;

namespace CanvasWP.API
{
    public class Attachment
    {
        [JsonProperty("filename")]
        public string Filename { get; set; }

        [JsonProperty("hidden")]
        public bool Hidden{ get; set; }

        [JsonProperty("id")]
        public long Id { get; set; }

        [JsonProperty("updated_at")]
        public DateTime? UpdatedAt { get; set; }

        [JsonProperty("hidden_for_user")]
        public bool HiddenForUser { get; set; }

        [JsonProperty("display_name")]
        public string DisplayName { get; set; }

        [JsonProperty("size")]
        public long Size { get; set; }

        [JsonProperty("unlock_at")]
        public DateTime? UnlockAt { get; set; }

        [JsonProperty("created_at")]
        public DateTime CreatedAt { get; set; }

        [JsonProperty("locked_for_user")]
        public bool LockedForUser { get; set; }

        [JsonProperty("content-type")]
        public string ContentType { get; set; }

        [JsonProperty("lock_at")]
        public DateTime? LockAt { get; set; }

        [JsonProperty("url")]
        public string Url { get; set; }

        [JsonProperty("locked")]
        public bool Locked { get; set; }
    }
}
