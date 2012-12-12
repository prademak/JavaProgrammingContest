namespace JavaProgrammingContest.Domain.Entities{
    public class Score{
        public int Id { get; set; }
        public double TimeSpent { get; set; }
        public bool IsCorrectOutput { get; set; }

        public Assignment Assignment { get; set; }
        public Participant Participant { get; set; }
    }
}