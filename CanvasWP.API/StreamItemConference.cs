using Newtonsoft.Json;
using System;

namespace CanvasWP.API
{
    public class StreamItemConference : StreamItemBase
    {
        [JsonProperty("web_conference_id")]
        public long WebConferenceId { get; set; }
    }
}
