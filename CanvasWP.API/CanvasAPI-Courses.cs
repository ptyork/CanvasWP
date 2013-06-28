using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace CanvasWP.API
{
    public partial class CanvasAPI
    {
        public event AsyncCompletedEventHandler<List<Course>> GetCoursesCompleted;

        public void GetCoursesAsync(object userState = null)
        {
            BeginGet("/api/v1/courses", null, new RequestCompletedDelegate(_GetCoursesCompleted), userState);
        }

        private void _GetCoursesCompleted(String responseText, string nextURL, object userState, Exception ex)
        {
            if (ex != null)
            {
                GetCoursesCompleted(this, new AsyncCompletedEventArgs<List<Course>>(null, ex, false, null));
            }
            else
            {
                try
                {
                    List<Course> courses = JsonConvert.DeserializeObject<List<Course>>(responseText);
                    GetCoursesCompleted(this, new AsyncCompletedEventArgs<List<Course>>(courses, null, false, null));
                }
                catch (Exception e)
                {
                    //GetCoursesEventArgs args = new GetCoursesEventArgs(null, e, false, null);
                    //GetCoursesCompleted(this, args);
                    GetCoursesCompleted(this, new AsyncCompletedEventArgs<List<Course>>(null, e, false, null));
                }
            }
        }
    }
}
