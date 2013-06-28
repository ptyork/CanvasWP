using Newtonsoft.Json;
using System;

namespace CanvasWP.API
{
    public class Assignment
    {
        [JsonProperty("assignment_group_id")]
        public long? AssignmentGroupId { get; set; }

        [JsonProperty("course_id")]
        public long? CourseId { get; set; }

        [JsonProperty("due_at")]
        public DateTime? DueAt { get; set; }

        // "pass_fail" | "percent" | "letter_grade" | "points"
        [JsonProperty("grading_type")]
        public string GradingType { get; set; }

        [JsonProperty("id")]
        public long Id { get; set; }

        [JsonProperty("muted")]
        public bool Muted { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("title")]
        public string Title { get; set; }

        [JsonProperty("description")]
        public string Description { get; set; }

        [JsonProperty("html_url")]
        public string HtmlUrl { get; set; }

        [JsonProperty("needs_grading_count")]
        public int? NeedsGradingCount { get; set; }

        [JsonProperty("points_possible")]
        public int PointsPossible { get; set; }

        [JsonProperty("position")]
        public int Position { get; set; }

        [JsonProperty("submission_types")]
        public string[] SubmissionTypes { get; set; }
    }
}
