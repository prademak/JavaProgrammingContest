using System;
using System.Collections.ObjectModel;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Web.Security;
using JavaProgrammingContest.DataAccess.Context;
using JavaProgrammingContest.Domain.Entities;
using WebMatrix.WebData;

namespace JavaProgrammingContest.DataAccess.Migrations{
    internal sealed class Configuration : DbMigrationsConfiguration<JavaProgrammingContestContext>{
        public Configuration(){
            AutomaticMigrationsEnabled = true;
        }

        protected override void Seed(JavaProgrammingContestContext context){
            SetupWebSercurity();
            InsertSampleData(context);
        }

        private static void SetupWebSercurity() {
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
        }

        private static void InsertSampleData(JavaProgrammingContestContext context){
        
            var ass1 = new Assignment{
                CodeGiven =
                    "// Sample class\nclass Solution {\n\tpublic static void main(String[] args){\n\t\tSystem.out.println(\"Hello World!\");\n\t}\n}\n",
                Description =
                    "Print like: '2 3 5 7 11'",
                Title = "Print 30 primenumbers",
                MaxSolveTime = 900,
                RunCodeInput = string.Empty,
                RunCodeOuput = "2 3 5 7 11 13 17 19 23 29 31 37 41 43 47 53 59 61 67 71 73 79 83 89 97 101 103 107 109 113"
            };
            var ass2 = new Assignment
            {
                CodeGiven =
                    "// Sample class\nimport java.io.BufferedReader;\nimport java.io.IOException;\nimport java.io.InputStreamReader; \n\nclass Solution {\n\tpublic static void main(String[] args) throws IOException{\n\t\t BufferedReader br = new BufferedReader(new InputStreamReader(System.in));\n\t\tSystem.out.println(br.readLine());\n\t}\n}\n",
                Description =
                    "In this assignment you have to sort a row of numbers. \n\n Example: \n Input: 1 3 2 6 9 6 7 \n Output: 1 2 3 6 6 7 9 ",
                Title = "Sort",
                MaxSolveTime = 900,
                RunCodeInput = "1 3 2 6 9 6 7",
                RunCodeOuput = "1 2 3 6 6 7 9"

            };
            var contest = new Contest{
                IsActive = true,
                Name = "Info Support Code Contest December",
                Assignments = new Collection<Assignment>{ass1, ass2}
            };

            context.Contests.AddOrUpdate(contest);
            context.SaveChanges();
        }
    }
}