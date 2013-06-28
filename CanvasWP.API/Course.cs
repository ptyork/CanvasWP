using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace CanvasWP.API
{
    public class Course
    {
        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("course_code")]
        public string CourseCode { get; set; }

        [JsonProperty("html_url")]
        public string HtmlUrl { get; set; }

        [JsonProperty("account_id")]
        public long AccountId { get; set; }

        [JsonProperty("id")]
        public long Id { get; set; }

        //A list of enrollments linking the current user to the course.
        [JsonProperty("enrollments")]
        public Dictionary<string,string>[] Enrollments { get; set; }

        //The SIS id of the course, if defined.
        [JsonProperty("sis_course_id")]
        public string SisCourseId { get; set; }

        [JsonProperty("start_at")]
        public DateTime? StartAt { get; set; }

        [JsonProperty("end_at")]
        public DateTime? EndAt { get; set; }

        //calendar
        [JsonProperty("calendar")]
        public Dictionary<string, string> Calendar { get; set; }

        //Number of submissions needing grading for all the course assignments. Only returned if include[]=needs_grading_count
        [JsonProperty("needs_grading_count")]
        public int? NeedsGradingCount { get; set; }
    }
}
