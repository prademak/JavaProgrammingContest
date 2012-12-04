using System.Collections.Generic;

namespace JavaProgrammingContest.Domain.Entities{
    public class Participant{
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string Insert { get; set; }
        public string LastName { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
        public bool Interested { get; set; }

        public ICollection<Score> Scores { get; set; }
        public UserSetting UserSetting { get; set; }
        public Progress Progress { get; set; }
    }
}