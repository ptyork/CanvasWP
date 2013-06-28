using Newtonsoft.Json;
using System;

namespace CanvasWP.API
{
    public class User
    {
        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("id")]
        public long Id { get; set; }

        [JsonProperty("avatar_url")]
        public string AvatarUrl { get; set; }

        [JsonProperty("login_id")]
        public string LoginId { get; set; }

        [JsonProperty("sortable_name")]
        public string SortableName { get; set; }
        
        [JsonProperty("short_name")]
        public string ShortName { get; set; }
    }
}
