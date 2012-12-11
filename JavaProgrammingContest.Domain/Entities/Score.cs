namespace JavaProgrammingContest.Domain.Entities{
    public class Score{
        public int Id { get; set; }
        public double TimeSpent { get; set; }
        public bool IsCorrectOutput { get; set; }

        public virtual Assignment Assignment { get; set; }
        public virtual Participant Participant { get; set; }
    }
}