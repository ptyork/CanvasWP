using Newtonsoft.Json;
using System;

namespace CanvasWP.API
{
    public class StreamItemAnnouncement : StreamItemBase
    {
        [JsonProperty("announcement_id")]
        public long AnnouncementId { get; set; }

        [JsonProperty("total_root_discussion_entries")]
        public int? TotalRootDiscussionEntries { get; set; }

        [JsonProperty("require_initial_post")]
        public bool RequireInitialPost { get; set; }

        [JsonProperty("user_has_posted")]
        public string UserHasPosted { get; set; }

        [JsonProperty("root_discussion_entries")]
        public TopicComment[] RootDiscussionEntries { get; set; }

    }
}
