using Newtonsoft.Json;
using System;

namespace CanvasWP.API
{
    class Role
    {
        [JsonProperty("id")]
        public long Id { get; set; }
    }
}
