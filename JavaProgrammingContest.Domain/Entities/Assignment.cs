using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace JavaProgrammingContest.Domain.Entities{
    public class Assignment{
        public int Id { get; set; }

        [Required]
        [Display(Name = "Titel")]
        public string Title { get; set; }

        [Required]
        [Display(Name = "Omschrijving")]
        public string Description { get; set; }

        [Required]
        [Display(Name = "Gegeven Code")]
        public string CodeGiven { get; set; }

        [Required]
        [Display(Name = "Tijdslimiet (sec)")]
        public double MaxSolveTime { get; set; }

        [Display(Name = "Input tekst wanneer opdracht actief is.")]
        public string RunCodeInput { get; set; }

        [Required]
        [Display(Name = "Verwachte output wanneer opdracht actief is.")]
        public string RunCodeOuput { get; set; }

        public virtual Contest Contest { get; set; }
        public virtual ICollection<Score> Scores { get; set; }
    }
}