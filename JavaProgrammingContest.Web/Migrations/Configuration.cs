using System.Data.Entity.Migrations;
using System.Linq;
using System.Web.Security;
using JavaProgrammingContest.Web.Models;
using WebMatrix.WebData;

namespace JavaProgrammingContest.Web.Migrations
{
    internal sealed class Configuration : DbMigrationsConfiguration<UsersContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
        }

        protected override void Seed(UsersContext context)
        {
            WebSecurity.InitializeDatabaseConnection(
                "DefaultConnection",
                "UserProfile",
                "UserId",
                "UserName", autoCreateTables: true);

            if (!Roles.RoleExists("Administrator"))
                Roles.CreateRole("Administrator");

            if (!WebSecurity.UserExists("lelong37"))
                WebSecurity.CreateUserAndAccount(
                    "johndoe",
                    "testing",
                    new {Mobile = "+19725000000"});

            if (!Roles.GetRolesForUser("johndoe").Contains("Administrator"))
                Roles.AddUsersToRoles(new[] {"johndoe"}, new[] {"Administrator"});
        }
    }
}