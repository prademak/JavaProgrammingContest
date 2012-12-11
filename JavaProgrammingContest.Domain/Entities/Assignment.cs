using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace JavaProgrammingContest.Domain.Entities{
    public class Assignment{
        public int Id { get; set; }
        [Required]
        [Display(Name = "Title")]
        public string Title { get; set; }
        [Required]
        [Display(Name = "Description")]
        public string Description { get; set; }
        [Required]
        [Display(Name = "Code given")]
        public string CodeGiven { get; set; }
        [Required]
        [Display(Name = "Max Solve Time")]
        public double MaxSolveTime { get; set; }

        public ICollection<ContestAssignment> ContestAssignments { get; set; } 
    }
}