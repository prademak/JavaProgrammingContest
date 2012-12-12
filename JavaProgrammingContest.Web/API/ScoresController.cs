using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using JavaProgrammingContest.DataAccess.Context;
using JavaProgrammingContest.Domain.Entities;
using JavaProgrammingContest.Process.Compiler;
using JavaProgrammingContest.Process.Runner;
using WebMatrix.WebData;

namespace JavaProgrammingContest.Web.API
{
    public class ScoresController : ApiController
    {
          private readonly IRunner _runner;
        private readonly ICompiler _compiler;
        private readonly IDbContext _context;

        public ScoresController(IDbContext context, ICompiler compiler, IRunner runner)
        {
            _context = context;
            _compiler = compiler;
            _runner = runner;
        }

        public HttpResponseMessage Scores(RunJob runJob){
            var participant = _context.Participants.Find(WebSecurity.GetUserId(User.Identity.Name));
            var result = _compiler.CompileFromPlainText(participant, runJob.Code);
            var runResult = _runner.Run();
            var assignment = _context.Assignments.Find(runJob.Id);
            bool correctOutput = (runResult.Output.Trim().Equals(assignment.RunCodeOuput));
            var score = new Score
                {
                    Assignment = assignment,
                    IsCorrectOutput = correctOutput,
                    Participant = participant,
                    TimeSpent = 100
                };
            _context.Scores.Add(score); 
           _context.SaveChanges();

            return Request.CreateResponse(HttpStatusCode.Created,
                new RunResult{
                    BuildResult = new BuildResult{
                        Output = result.StandardOutput,
                        Error = (runResult.Output.Trim().Equals(assignment.RunCodeOuput)).ToString(),
                        CompileTime = result.CompilationTime
                    },
                    Output = runResult.Output,
                    RunTime = runResult.RunTime
                });
        } 
    }
}
