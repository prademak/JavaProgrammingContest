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

        [Display(Name = "Input text when assignments runs.")]
        public string RunCodeInput { get; set; }

        [Required]
        [Display(Name = "Expected output when assignment runs.")]
        public string RunCodeOuput { get; set; }

        public virtual Contest Contest { get; set; }
        public virtual ICollection<Score> Scores { get; set; }
    }
}