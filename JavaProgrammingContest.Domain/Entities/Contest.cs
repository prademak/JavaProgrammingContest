using System;

namespace JavaProgrammingContest.Domain.Entities{
    public class Contest
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Event { get; set; }
        public DateTime Date { get; set; }
    }
}