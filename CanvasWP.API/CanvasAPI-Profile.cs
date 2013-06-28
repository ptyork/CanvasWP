using Newtonsoft.Json;
using System;

namespace CanvasWP.API
{
    public partial class CanvasAPI
    {
        public event AsyncCompletedEventHandler<Profile> GetProfileCompleted;

        public void GetProfileAsync(string userId, object userState = null)
        {
            BeginGet("/api/v1/users/" + userId + "/profile", null, new RequestCompletedDelegate(_GetProfileCompleted), userState);
        }

        private void _GetProfileCompleted(String responseText, string nextURL, object userState, Exception ex)
        {
            if (ex != null)
            {
                GetProfileCompleted(this, new AsyncCompletedEventArgs<Profile>(null, ex, false, userState));
            }
            else
            {
                try
                {
                    Profile profile = JsonConvert.DeserializeObject<Profile>(responseText);
                    GetProfileCompleted(this, new AsyncCompletedEventArgs<Profile>(profile, null, false, userState));
                }
                catch (Exception e)
                {
                    GetProfileCompleted(this, new AsyncCompletedEventArgs<Profile>(null, e, false, userState));
                }
            }
        }
    }
}
