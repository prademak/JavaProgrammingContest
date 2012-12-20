using WebMatrix.WebData;

namespace JavaProgrammingContest.Web.App_Start{
    /// <summary>
    ///     Database configuration.
    /// </summary>
    public static class DatabaseConfig{
        /// <summary>
        ///     Initializes the database.
        /// </summary>
        public static void InitializeDatabase(){
            WebSecurity.InitializeDatabaseConnection(
                "JavaProgrammingContest",
                "Participants",
                "Id",
                "Email", true);
        }
    }
}