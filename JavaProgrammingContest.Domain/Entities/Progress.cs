using System;
using System.ComponentModel.DataAnnotations;

namespace JavaProgrammingContest.Domain.Entities{
    public class Progress{
        public int Id { get; set; }
        public DateTime StartTime { get; set; }

        [Required]
        public virtual Participant Participant { get; set; }
        public virtual Assignment Assignment { get; set; }
    }
}