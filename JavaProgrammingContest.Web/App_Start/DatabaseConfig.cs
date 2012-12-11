using WebMatrix.WebData;

namespace JavaProgrammingContest.Web.App_Start{
    public class DatabaseConfig{
        public static void InitializeDatabase(){
            WebSecurity.InitializeDatabaseConnection(
                "JavaProgrammingContest",
                "Participants",
                "Id",
                "Email", true);
        }
    }
}