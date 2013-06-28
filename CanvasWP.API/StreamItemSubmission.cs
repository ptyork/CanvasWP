using Newtonsoft.Json;
using System;

namespace CanvasWP.API
{
    public class StreamItemSubmission : StreamItemBase
    {
        [JsonProperty("assignment_id")]
        public long AssignmentId { get; set; }

        [JsonProperty("submitted_at")]
        public DateTime? SubmittedAt { get; set; }

        [JsonProperty("grade")]
        public String Grade { get; set; }

        [JsonProperty("grade_matches_current_submission")]
        public bool GradeMatchesCurrentSubmission { get; set; }

        [JsonProperty("score")]
        public int? Score { get; set; }

        [JsonProperty("assignment")]
        public Assignment Assignment { get; set; }

        [JsonProperty("submission_comments")]
        public SubmissionComment[] SubmissionComments { get; set; }

        [JsonProperty("preview_url")]
        public String PreviewUrl { get; set; }

        [JsonProperty("attachments")]
        public Attachment[] Attachments{ get; set; }

        [JsonProperty("attempt")]
        public int? Attempt { get; set; }

        [JsonProperty("workflow_state")]
        public String WorkflowState { get; set; }

        [JsonProperty("grader_id")]
        public long? GraderId { get; set; }

        [JsonProperty("submission_type")]
        public String SubmissionType { get; set; }

        [JsonProperty("course")]
        public Course Course { get; set; }

        [JsonProperty("user_id")]
        public long? UserId { get; set; }

        [JsonProperty("user")]
        public User User { get; set; }

        [JsonProperty("group_id")]
        public int? GroupId { get; set; }
    }
}
