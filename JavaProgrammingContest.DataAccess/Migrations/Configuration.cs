using System.Data.Entity.Migrations;
using System.Linq;
using System.Web.Security;
using JavaProgrammingContest.DataAccess.Context;
using WebMatrix.WebData;

namespace JavaProgrammingContest.DataAccess.Migrations{
    internal sealed class Configuration : DbMigrationsConfiguration<JavaProgrammingContestContext>{
        public Configuration(){
            AutomaticMigrationsEnabled = true;
        }

        protected override void Seed(JavaProgrammingContestContext context){
            WebSecurity.InitializeDatabaseConnection(
                "JavaProgrammingContest",
                "Participants",
                "Id",
                "Email", true);

            if (!Roles.RoleExists("Administrator"))
                Roles.CreateRole("Administrator");

            if (!WebSecurity.UserExists("johndoe@mail.com"))
                WebSecurity.CreateUserAndAccount(
                    "johndoe@mail.com",
                    "testing",
                    new{Interested = false});

            if (!Roles.GetRolesForUser("johndoe@mail.com").Contains("Administrator"))
                Roles.AddUsersToRoles(new[]{"johndoe@mail.com"}, new[]{"Administrator"});
        }
    }
}