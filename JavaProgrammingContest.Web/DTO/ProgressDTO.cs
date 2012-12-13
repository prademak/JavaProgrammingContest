using System;

namespace JavaProgrammingContest.Web.DTO{
    public class ProgressDTO{
        public int Id { get; set; }
        public DateTime StartTime { get; set; }
        public int ParticipantId { get; set; }
        public int AssignmentId { get; set; }
    }
}