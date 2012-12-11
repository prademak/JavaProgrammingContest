using System;
using System.ComponentModel.DataAnnotations;

namespace JavaProgrammingContest.Domain.Entities{
    public class Progress{
        public int Id { get; set; }
        public DateTime StartTime { get; set; }

        [Required]
        public Participant Participant { get; set; }
        public ContestAssignment ContestAssignment { get; set; }
    }
}