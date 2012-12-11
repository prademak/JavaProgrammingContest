using System.Data.Entity.Migrations;
using System.Linq;
using System.Web.Security;
using JavaProgrammingContest.DataAccess.Context;
using JavaProgrammingContest.Domain.Entities;
using WebMatrix.WebData;

namespace JavaProgrammingContest.DataAccess.Migrations
{
    internal sealed class Configuration : DbMigrationsConfiguration<JavaProgrammingContestContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
        }

        protected override void Seed(JavaProgrammingContestContext context)
        {
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
                    new { Interested = false });

            if (!Roles.GetRolesForUser("johndoe@mail.com").Contains("Administrator"))
                Roles.AddUsersToRoles(new[] { "johndoe@mail.com" }, new[] { "Administrator" });

            var ass1 = new Assignment
                {
                    Id = 1,
                    CodeGiven =
                        "// Sample class\nclass HelloWorldApp {\n\tpublic static void main(String[] args){\n\t\tSystem.out.println(\"Hello World!\");\n\t}\n}\n",
                    Description =
                        "Print the following text: Hello,World!",
                    Title = "Hello, Word!",
                    MaxSolveTime = 900
                };
            var ass2 = new Assignment
            {
                Id = 2,
                CodeGiven =
                    "// Sample class\nclass HelloWorldApp {\n\tpublic static void main(String[] args){\n\t\tSystem.out.println(\"Hello World!\");\n\t}\n}\n",
                Description =
                    "Print like: '2 3 5 7 11'",
                Title = "Print 30 primenumbers",
                MaxSolveTime = 900
            };
            var contest = new Contest { Id = 1, IsActive = true, Name = "Info Support Contest December" };

            var contestAssignment = new ContestAssignment { Assignment = ass1, Contest = contest };
            var contestAssignment2 = new ContestAssignment { Assignment = ass2, Contest = contest };

            context.ContestAssignments.AddOrUpdate(contestAssignment);
            context.ContestAssignments.AddOrUpdate(contestAssignment2);

            context.SaveChanges();
        }
    }
}