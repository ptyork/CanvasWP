using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace CanvasWP.API
{
    public partial class CanvasAPI
    {
        public event AsyncCompletedEventHandler<Conversation> GetConversationCompleted;
        public event AsyncCompletedEventHandler<List<Conversation>> GetConversationsCompleted;
        public event AsyncCompletedEventHandler<List<Conversation>> GetMoreConversationsCompleted;

        private string _nextConversationsURL = null;

        public void GetConversationAsync(long id, object userState = null)
        {
            BeginGet("/api/v1/conversations/" + id, null, new RequestCompletedDelegate(_GetConversationCompleted), userState);
        }

        private void _GetConversationCompleted(String responseText, string nextURL, object userState, Exception ex)
        {
            if (ex != null)
            {
                GetConversationCompleted(this, new AsyncCompletedEventArgs<Conversation>(null, ex, false, userState));
            }
            else
            {
                try
                {
                    Conversation conversation = JsonConvert.DeserializeObject<Conversation>(responseText);
                    GetConversationCompleted(this, new AsyncCompletedEventArgs<Conversation>(conversation, null, false, userState));
                }
                catch (Exception e)
                {
                    GetConversationCompleted(this, new AsyncCompletedEventArgs<Conversation>(null, e, false, userState));
                }
            }
        }

        public void GetConversationsAsync(object userState = null)
        {
            BeginGet("/api/v1/conversations", null, new RequestCompletedDelegate(_GetConversationsCompleted), userState);
        }

        private void _GetConversationsCompleted(String responseText, string nextURL, object userState, Exception ex)
        {
            if (ex != null)
            {
                GetConversationsCompleted(this, new AsyncCompletedEventArgs<List<Conversation>>(null, ex, false, userState));
            }
            else
            {
                try
                {
                    List<Conversation> conversations = JsonConvert.DeserializeObject<List<Conversation>>(responseText);
                    _nextConversationsURL = nextURL;
                    GetConversationsCompleted(this, new AsyncCompletedEventArgs<List<Conversation>>(conversations, null, false, userState));
                }
                catch (Exception e)
                {
                    GetConversationsCompleted(this, new AsyncCompletedEventArgs<List<Conversation>>(null, e, false, userState));
                }
            }
        }

        public bool HasMoreConversations
        {
            get
            {
                return !String.IsNullOrEmpty(_nextConversationsURL);
            }
        }

        public void GetMoreConversationsAsync(object userState = null)
        {
            if (HasMoreConversations)
                BeginGet(_nextConversationsURL, null, new RequestCompletedDelegate(_GetMoreConversationsCompleted), userState);
        }

        private void _GetMoreConversationsCompleted(String responseText, string nextURL, object userState, Exception ex)
        {
            if (ex != null)
            {
                GetMoreConversationsCompleted(this, new AsyncCompletedEventArgs<List<Conversation>>(null, ex, false, userState));
            }
            else
            {
                try
                {
                    List<Conversation> conversations = JsonConvert.DeserializeObject<List<Conversation>>(responseText);
                    _nextConversationsURL = nextURL;
                    GetMoreConversationsCompleted(this, new AsyncCompletedEventArgs<List<Conversation>>(conversations, null, false, userState));
                }
                catch (Exception e)
                {
                    GetMoreConversationsCompleted(this, new AsyncCompletedEventArgs<List<Conversation>>(null, e, false, userState));
                }
            }
        }

    }
}
