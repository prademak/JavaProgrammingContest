﻿using System.Data.Entity;
using JavaProgrammingContest.Domain.Entities;

namespace JavaProgrammingContest.DataAccess.Context{
    public class JavaProgrammingContestContext : DbContext, IDbContext{
        public JavaProgrammingContestContext()
            : base("JavaProgrammingContestDB") {}

        public IDbSet<Contest> Contests { get; set; }
        public IDbSet<Assignment> Assignments { get; set; }
        public IDbSet<Participant> Participants { get; set; }
        public IDbSet<Score> Scores { get; set; }
        public IDbSet<Result> Results { get; set; }
        public IDbSet<UserSetting> UserSettings { get; set; }
    }

    public interface IDbContext{
        IDbSet<Contest> Contests { get; set; }
        IDbSet<Assignment> Assignments { get; set; }
        IDbSet<Participant> Participants { get; set; }
        IDbSet<Score> Scores { get; set; }
        IDbSet<Result> Results { get; set; }
        IDbSet<UserSetting> UserSettings { get; set; }

        int SaveChanges();
    }
}