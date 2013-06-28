using Newtonsoft.Json;
using System;
using System.ComponentModel;

namespace CanvasWP.API
{
    public partial class CanvasAPI
    {
        public event AsyncCompletedEventHandler GetAccessTokenCompleted;

        public class UserAuthorizationResult
        {
            public class UserCredentials
            {
                [JsonProperty("name")]
                public string UserName { get; set; }

                [JsonProperty("id")]
                public long UserID { get; set; }
            }

            [JsonProperty("user")]
            public UserCredentials Credentials { get; set; }

            [JsonProperty("access_token")]
            public string AccessToken { get; set; }
        }

        public void GetAccessTokenAsync(string tempCode, object userState = null)
        {
            string endpoint = "/login/oauth2/token";
            string postData = "client_id=63&redirect_uri=urn:ietf:wg:oauth:2.0:oob&client_secret=XbQpJYlviE7mbNzXTXoAITSBN1cg72eZAlAAWcLj5hEY4RCcqBqUAOzTpGkzbulr&code=" + tempCode;
            BeginPost(endpoint, null, postData, new RequestCompletedDelegate(_GetAccessTokenCompleted));
        }

        private void _GetAccessTokenCompleted(String responseText, string nextURL, object userState, Exception ex)
        {
            if (ex != null)
            {
                GetAccessTokenCompleted(this, new AsyncCompletedEventArgs(ex, false, userState));
            }
            else
            {
                try
                {
                    UserAuthorizationResult atc = JsonConvert.DeserializeObject<UserAuthorizationResult>(responseText);
                    this.AccessToken = atc.AccessToken;
                    this.UserID = atc.Credentials.UserID;
                    GetAccessTokenCompleted(this, new AsyncCompletedEventArgs(null, false, userState));
                }
                catch (Exception e)
                {
                    GetAccessTokenCompleted(this, new AsyncCompletedEventArgs(e, false, userState));
                }
            }
        }
    }
}
