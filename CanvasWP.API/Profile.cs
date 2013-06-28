using Newtonsoft.Json;
using System;

namespace CanvasWP.API
{
    public class Profile
    {
        [JsonProperty("id")]
        public long Id { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("sortable_name")]
        public string SortableName { get; set; }

        [JsonProperty("primary_email")]
        public string PrimaryEmail { get; set; }

        [JsonProperty("login_id")]
        public string LoginId { get; set; }

        [JsonProperty("sis_user_id")]
        public string SisUserId { get; set; }

        [JsonProperty("sis_login_id")]
        public string SisLoginId { get; set; }

        [JsonProperty("avatar_url")]
        public string AvatarUrl { get; set; }
    }
}
