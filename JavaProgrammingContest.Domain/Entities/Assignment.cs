namespace JavaProgrammingContest.Domain.Entities{
    public class Assignment{
        public int Id { get; set; }
        
        public string Title { get; set; }
        public string Description { get; set; }
        
        public string CodeGiven { get; set; }
        
        public double TargetSolveTime { get; set; }
        public double TargetCompileTime { get; set; }
    }
}