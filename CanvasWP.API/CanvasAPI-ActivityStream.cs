using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace CanvasWP.API
{
    public partial class CanvasAPI
    {
        public event AsyncCompletedEventHandler<List<object>> GetActivityStreamCompleted;
        public event AsyncCompletedEventHandler<List<object>> GetMoreActivityStreamCompleted;

        private string _nextActivityStreamURL = null;

        public void GetActivityStreamAsync(object userState = null)
        {
            BeginGet("/api/v1/users/self/activity_stream", null, new RequestCompletedDelegate(_GetActivityStreamCompleted), userState);
        }

        private void _GetActivityStreamCompleted(String responseText, string nextURL, object userState, Exception ex)
        {
            if (ex != null)
            {
                GetActivityStreamCompleted(this, new AsyncCompletedEventArgs<List<object>>(null, ex, false, userState));
            }
            else
            {
                try
                {
                    List<object> TmpObjects = JsonConvert.DeserializeObject<List<object>>(responseText);
                    List<object> streamItems = ConvertObjectsToList(TmpObjects);
                    _nextActivityStreamURL = nextURL;
                    GetActivityStreamCompleted(this, new AsyncCompletedEventArgs<List<object>>(streamItems, null, false, userState));
                }
                catch (Exception e)
                {
                    GetActivityStreamCompleted(this, new AsyncCompletedEventArgs<List<object>>(null, e, false, userState));
                }
            }
        }

        public bool HasMoreActivityStream
        {
            get
            {
                return !String.IsNullOrEmpty(_nextActivityStreamURL);
            }
        }

        public void GetMoreActivityStreamAsync(object userState = null)
        {
            if (HasMoreActivityStream)
                BeginGet(_nextActivityStreamURL, null, new RequestCompletedDelegate(_GetMoreActivityStreamCompleted), userState);
        }

        private void _GetMoreActivityStreamCompleted(String responseText, string nextURL, object userState, Exception ex)
        {
            if (ex != null)
            {
                GetMoreActivityStreamCompleted(this, new AsyncCompletedEventArgs<List<object>>(null, ex, false, userState));
            }
            else
            {
                try
                {
                    List<object> TmpObjects = JsonConvert.DeserializeObject<List<object>>(responseText);
                    List<object> streamItems = ConvertObjectsToList(TmpObjects);
                    _nextActivityStreamURL = nextURL;
                    GetMoreActivityStreamCompleted(this, new AsyncCompletedEventArgs<List<object>>(streamItems, null, false, userState));
                }
                catch (Exception e)
                {
                    GetMoreActivityStreamCompleted(this, new AsyncCompletedEventArgs<List<object>>(null, e, false, userState));
                }
            }
        }

        /// <summary>
        /// Convert the items from the generic object, to its appropriate type and add it to the list
        /// </summary>
        /// <param name="objects"></param>
        /// <returns></returns>
        private List<object> ConvertObjectsToList(List<object> objects)
        {

            List<object> ret = new List<object>();

            foreach (object obj in objects)
            {
                //Get the base object first, then we will convert it to its subtype
                StreamItemBase item = JsonConvert.DeserializeObject<StreamItemBase>(obj.ToString());
                switch (item.ItemType)
                {
                    case StreamItemBase.ItemTypes.Announcement:
                        ret.Add(JsonConvert.DeserializeObject<StreamItemAnnouncement>(obj.ToString()));
                        break;
                    case StreamItemBase.ItemTypes.Collaboration:
                        ret.Add(JsonConvert.DeserializeObject<StreamItemCollaboration>(obj.ToString()));
                        break;
                    case StreamItemBase.ItemTypes.Conference:
                        ret.Add(JsonConvert.DeserializeObject<StreamItemConference>(obj.ToString()));
                        break;
                    case StreamItemBase.ItemTypes.Conversation:
                        ret.Add(JsonConvert.DeserializeObject<StreamItemConversation>(obj.ToString()));
                        break;
                    case StreamItemBase.ItemTypes.DiscussionTopic:
                        ret.Add(JsonConvert.DeserializeObject<StreamItemDiscussionTopic>(obj.ToString()));
                        break;
                    case StreamItemBase.ItemTypes.Message:
                        ret.Add(JsonConvert.DeserializeObject<StreamItemMessage>(obj.ToString()));
                        break;
                    case StreamItemBase.ItemTypes.Submission:
                        ret.Add(JsonConvert.DeserializeObject<StreamItemSubmission>(obj.ToString()));
                        break;
                    default:
                        ret.Add(item);
                        break;
                }
            }

            return ret;

        }

    }
}
