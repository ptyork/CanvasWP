using Newtonsoft.Json;
using System;

namespace CanvasWP.API
{
    public class Enrollment
    {
        [JsonProperty("course_id")]
        public long CourseId { get; set; }

        [JsonProperty("course_section_id")]
        public long CourseSectionId { get; set; }

        [JsonProperty("enrollment_state")]
        public string EnrollmentState { get; set; }

        [JsonProperty("id")]
        public long Id { get; set; }

        [JsonProperty("limit_privileges_to_course_section")]
        public bool LimitPrivilegesToCourseSection { get; set; }

        [JsonProperty("root_account_id")]
        public long RootAccountId { get; set; }

        [JsonProperty("type")]
        public string Type { get; set; }

        [JsonProperty("user")]
        public User User { get; set; }

        [JsonProperty("user_id")]
        public long UserId { get; set; }
    }
}
