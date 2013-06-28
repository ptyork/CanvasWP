using Newtonsoft.Json;
using System;

namespace CanvasWP.API
{
    public class StreamItemBase 
    {
        [JsonProperty("created_at")]
        public DateTime CreatedAt { get; set; }

        [JsonProperty("updated_at")]
        public DateTime? UpdatedAt { get; set; }

        [JsonProperty("id")]
        public long Id { get; set; }

        [JsonProperty("title")]
        public string Title { get; set; }

        [JsonProperty("message")]
        public string Message { get; set; }

        [JsonProperty("body")]
        public string Body { get; set; }

        [JsonProperty("type")]
        public string TypeString { get; set; }

        public ItemTypes ItemType
        {
            get
            {
                return (ItemTypes)Enum.Parse(typeof(ItemTypes), TypeString, true);
            }
        }
                
        public enum ItemTypes { 
            DiscussionTopic,
            Announcement,
            Conversation,
            Message,
            Submission,
            Conference,
            Collaboration
        }

        [JsonProperty("context_type")]
        public string ContextType { get; set; }

        [JsonProperty("html_url")]
        public string HtmlUrl { get; set; }

        protected long? GetIdFromUrl(string id)
        {
            string[] u = this.HtmlUrl.Split('/');
            for (int i = 0; i < u.Length; i++)
            {
                if (id.Equals(u[i].ToLower()))
                {
                    try
                    {
                        return long.Parse(u[i + 1]);
                    }
                    catch
                    {
                        return null;
                    }
                }
            }
            return null;
        }

        public long? CourseId
        {
            get { return GetIdFromUrl("courses"); }
        }
    }
}
