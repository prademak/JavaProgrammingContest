using System;

namespace JavaProgrammingContest.Domain.Entities{
    public class Progress{
        public int Id { get; set; }
        public DateTime StartTime { get; set; }

        public Participant Participant { get; set; }
        public ContestAssignment ContestAssignment { get; set; }
    }
}