using System.Data.Entity;
using JavaProgrammingContest.Domain.Entities;

namespace JavaProgrammingContest.DataAccess.Context{
    public class JavaProgrammingContestContext : DbContext, IDbContext{
        public JavaProgrammingContestContext()
            : base("JavaProgrammingContest"){
            Configuration.ProxyCreationEnabled = true;
            Configuration.LazyLoadingEnabled = true;
        }

        public IDbSet<Contest> Contests { get; set; }
        public IDbSet<Assignment> Assignments { get; set; }
        public IDbSet<Participant> Participants { get; set; }
        public IDbSet<Score> Scores { get; set; }
        public IDbSet<Progress> Progresses { get; set; }
        public IDbSet<UserSetting> UserSettings { get; set; }
    }
}