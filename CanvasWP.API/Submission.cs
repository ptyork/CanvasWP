using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace CanvasWP.API
{
    public class Submission
    {
        public class SubmissionComment
        {
            [JsonProperty("author_id")]
            public long? AuthorId { get; set; }

            [JsonProperty("author_name")]
            public string AuthorName { get; set; }

            [JsonProperty("comment")]
            public string Comment { get; set; }

            [JsonProperty("created_at")]
            [JsonConverter(typeof(Newtonsoft.Json.Converters.IsoDateTimeConverter))]
            public DateTime CreatedAt { get; set; }

            [JsonProperty("media_comment")]
            public MediaComment MediaComment { get; set; }
        }

        [JsonProperty("assignment_id")]
        public long AssignmentId { get; set; }

        [JsonProperty("user_id")]
        public long? UserId { get; set; }

        [JsonProperty("grader_id")]
        public long? GraderId { get; set; }

        [JsonProperty("submission_type")]
        public string SubmissionType { get; set; }

        [JsonProperty("submitted_at")]
        [JsonConverter(typeof(Newtonsoft.Json.Converters.IsoDateTimeConverter))]
        public DateTime? SubmittedAt { get; set; }

        [JsonProperty("score")]
        public decimal? Score { get; set; }

        [JsonProperty("attempt")]
        public int? Attempt { get; set; }

        [JsonProperty("body")]
        public string Body { get; set; }

        [JsonProperty("grade")]
        public string Grade { get; set; }

        [JsonProperty("grade_matches_current_submission")]
        public bool? GradeMatchesCurrentSubmission { get; set; }

        [JsonProperty("preview_url")]
        public string PreviewUrl { get; set; }

        [JsonProperty("html_url")]
        public string HtmlUrl { get; set; }

        [JsonProperty("url")]
        public string Url { get; set; }

        [JsonProperty("submission_comments")]
        public List<SubmissionComment> SubmissionComments { get; set; }

        [JsonProperty("assignment")]
        public Assignment Assignment { get; set; }

        [JsonProperty("course")]
        public Course Course { get; set; }

        [JsonProperty("user")]
        public User User { get; set; }
    }
}
