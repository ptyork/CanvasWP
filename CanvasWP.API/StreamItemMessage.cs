using Newtonsoft.Json;
using System;

namespace CanvasWP.API
{
    public class StreamItemMessage : StreamItemBase
    {
        [JsonProperty("message_id")]
        public long? MessageId { get; set; }

        [JsonProperty("notification_category")]
        public string NotificationCategory { get; set; }

        public long? AssignmentId
        {
            get { return GetIdFromUrl("assignments"); }
        }

        public long? SubmissionId
        {
            get { return GetIdFromUrl("submissions"); }
        }
    }
}
