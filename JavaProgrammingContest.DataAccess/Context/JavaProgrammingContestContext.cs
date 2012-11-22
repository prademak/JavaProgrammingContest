using System.Data.Entity;
using JavaProgrammingContest.Domain.Entities;

namespace JavaProgrammingContest.DataAccess.Context{
    public class JavaProgrammingContestContext : DbContext{
        public JavaProgrammingContestContext()
            : base("JavaProgrammingContestDB") {}

        public IDbSet<Contest> Contests { get; set; }
        public IDbSet<Assignment> Assignments { get; set; }
        public IDbSet<Participant> Participants { get; set; }
        public IDbSet<Score> Scores { get; set; }
        public IDbSet<Result> Results { get; set; }
    }
}