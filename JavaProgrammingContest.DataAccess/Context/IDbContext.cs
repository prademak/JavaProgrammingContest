using System.Data.Entity;
using JavaProgrammingContest.Domain.Entities;

namespace JavaProgrammingContest.DataAccess.Context{
    public interface IDbContext{
        IDbSet<Contest> Contests { get; set; }
        IDbSet<Assignment> Assignments { get; set; }
        IDbSet<Participant> Participants { get; set; }
        IDbSet<Score> Scores { get; set; }
        IDbSet<Progress> Progresses { get; set; }
        IDbSet<UserSetting> UserSettings { get; set; }

        int SaveChanges();
    }
}