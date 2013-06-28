using Newtonsoft.Json;
using System;

namespace CanvasWP.API
{
    public class StreamItemCollaboration : StreamItemBase
    {
        [JsonProperty("collaboration_id")]
        public long CollaborationId { get; set; }

    }
}
