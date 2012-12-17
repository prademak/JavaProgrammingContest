using System.Net;
using System.Net.Http;
using System.Web.Http;
using JavaProgrammingContest.DataAccess.Context;
using JavaProgrammingContest.Domain.Entities;
using JavaProgrammingContest.Process.Compiler;
using JavaProgrammingContest.Process.Runner;
using WebMatrix.WebData;

namespace JavaProgrammingContest.Web.API{
    /// <summary>
    ///     Controller for interface interaction with the database object Score.
    /// </summary>
    public class ScoresController : ApiController{
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
        public ScoresController(IDbContext context, ICompiler compiler, IRunner runner){
            _context = context;
            _compiler = compiler;
            _runner = runner;
        }

        /// <summary>
        ///     Build and runs an assignment and checks the scores for the user submitted code.
        /// </summary>
        /// <param name="runJob">See RunController.</param>
        /// <returns></returns>
        public HttpResponseMessage Post(RunJob runJob){
            var participant = _context.Participants.Find(WebSecurity.GetUserId(User.Identity.Name));
            _compiler.CompileFromPlainText(participant, runJob.Code);
            var runResult = _runner.Run();

            var correctOutput = (runResult.Output.Trim().Equals(participant.Progress.Assignment.RunCodeOuput));

            System.TimeSpan elapsed = System.DateTime.Now - participant.Progress.StartTime;
            double timeDifference = elapsed.TotalSeconds;

            timeDifference = System.Math.Floor(timeDifference * 100) / 100;

            var score = new Score{
                Assignment = participant.Progress.Assignment,
                IsCorrectOutput = correctOutput,
                Participant = participant,
                TimeSpent = timeDifference
            };

            _context.Scores.Add(score);
            _context.Progresses.Remove(participant.Progress);
            _context.SaveChanges();

            return Request.CreateResponse(HttpStatusCode.Created);
        }
    }
}