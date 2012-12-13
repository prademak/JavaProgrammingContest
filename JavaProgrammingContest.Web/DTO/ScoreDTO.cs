namespace JavaProgrammingContest.Web.DTO{
    public class ScoreDTO{
        public int Id { get; set; }
        public double TimeSpent { get; set; }
        public bool IsCorrectOutput { get; set; }
        public int AssignmentId { get; set; }
        public int ParticipantId { get; set; }
    }
}