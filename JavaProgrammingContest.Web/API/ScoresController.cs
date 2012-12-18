using System.Net;
using System.Net.Http;
using System.Web.Http;
using JavaProgrammingContest.DataAccess.Context;
using JavaProgrammingContest.Domain.Entities;
using JavaProgrammingContest.Process.Compiler;
using JavaProgrammingContest.Process.Runner;
using WebMatrix.WebData;
using System;

namespace JavaProgrammingContest.Web.API{
    /// <summary>
    ///     Controller for interface interaction with the database object Score.
    /// </summary>
    public class ScoresController : ApiController
    {
        /// <summary>
        ///     Stores a Runner instance.
        /// </summary>
        private readonly IRunner _runner;

        /// <summary>
        ///     Stores a Compiler instance.
        /// </summary>
        private readonly ICompiler _compiler;

        /// <summary>
        ///     Stores the Database Context
        /// </summary>
        private readonly IDbContext _context;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="context"></param>
        /// <param name="compiler"></param>
        /// <param name="runner"></param>
        public ScoresController(IDbContext context, ICompiler compiler, IRunner runner)
        {
            _context = context;
            _compiler = compiler;
            _runner = runner;
        }

        /// <summary>
        ///     Build and runs an assignment and checks the scores for the user submitted code.
        /// </summary>
        /// <param name="runJob">See RunController.</param>
        /// <returns></returns>
        public HttpResponseMessage Post(RunJob runJob)
        {
            var participant = _context.Participants.Find(WebSecurity.GetUserId(User.Identity.Name));
            _compiler.CompileFromPlainText(participant, runJob.Code);
            var runResult = _runner.RunAndCheckInput(participant);
            var correctOutput = (runResult.Output.Trim().Equals(participant.Progress.Assignment.RunCodeOuput));
            var timeDifference = GetTimeDifference(participant.Progress.StartTime);

            var score = CreateScore(participant.Progress.Assignment, participant, correctOutput, timeDifference);

            _context.Scores.Add(score);
            _context.Progresses.Remove(participant.Progress);
            _context.SaveChanges();

            return Request.CreateResponse(HttpStatusCode.Created);
        }

        public static double GetTimeDifference(DateTime startTime)
        {
            var elapsed = DateTime.Now - startTime;
            var timeDifference = elapsed.TotalSeconds;
            timeDifference = Math.Floor(timeDifference * 100) / 100;

            return timeDifference;
        }

        public static Score CreateScore(Assignment assignment, Participant participant, bool correctOutput, double timeDifference)
        {
            var score = new Score
            {
                Assignment = assignment,
                IsCorrectOutput = correctOutput,
                Participant = participant,
                TimeSpent = timeDifference
            };

            return score;
        }
    }
}