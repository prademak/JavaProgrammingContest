using System.Data.Entity;

namespace JavaProgrammingContest.DataAccess.Context{
    public class JavaProgrammingContestContextInitializer : DropCreateDatabaseIfModelChanges<JavaProgrammingContestContext> {}
}