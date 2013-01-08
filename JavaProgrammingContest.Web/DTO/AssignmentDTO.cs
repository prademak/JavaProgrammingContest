namespace JavaProgrammingContest.Web.DTO{
    public class AssignmentDTO{
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string CodeGiven { get; set; }
        public double MaxSolveTime { get; set; }
        public bool HasBeenSubmitted { get; set; }
        public int ContestId { get; set; }
    }
}