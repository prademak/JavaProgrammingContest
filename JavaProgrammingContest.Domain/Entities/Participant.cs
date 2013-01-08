using System.Collections.Generic;

namespace JavaProgrammingContest.Domain.Entities{
    public class Participant{
        public int Id { get; set; }
        public string Email { get; set; }

        public virtual ICollection<Score> Scores { get; set; }
        public virtual UserSetting UserSetting { get; set; }
        public virtual Progress Progress { get; set; }
    }
}