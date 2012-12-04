using System.Collections.Generic;

namespace JavaProgrammingContest.Domain.Entities{
    public class ContestAssignment{
        public int Id { get; set; }

        public Assignment Assignment { get; set; }
        public Contest Contest { get; set; }
        public ICollection<Score> Scores { get; set; }
        public ICollection<Progress> Progresses { get; set; }
    }
}