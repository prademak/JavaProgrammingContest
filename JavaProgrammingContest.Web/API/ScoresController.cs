using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using AutoMapper;
using JavaProgrammingContest.DataAccess.Context;
using JavaProgrammingContest.Domain.Entities;
using JavaProgrammingContest.Process.Compiler;
using JavaProgrammingContest.Process.Runner;
using JavaProgrammingContest.Process.Runner.Model;
using JavaProgrammingContest.Web.DTO;
using JavaProgrammingContest.Web.Helpers;
using WebMatrix.WebData;

namespace JavaProgrammingContest.Web.API{
    /// <summary>
    ///     Controller for interface interaction with the database object Score.
    /// </summary>
    public class ScoresController : ApiController{
        private readonly IRunner _runner;
        private readonly ICompiler _compiler;
        private readonly IDbContext _context;
        private readonly Participant _participant;

        public ScoresController(IDbContext context, ICompiler compiler, IRunner runner, Participant participant = null){
            _context = context;
            _compiler = compiler;
            _runner = runner;
            _participant = participant ?? GetCurrentParticipant();
        }

        [ExcludeFromCodeCoverage]
        private Participant GetCurrentParticipant(){
            var participant = _context.Participants.Find(WebSecurity.GetUserId(User.Identity.Name));
            return participant;
        }

        /// <summary>
        /// Returns a list of scores from 
        /// </summary>
        public IEnumerable<ScoreDTO> GetScoresFromCurrentUser(){
            return Mapper.Map<IEnumerable<Score>, IEnumerable<ScoreDTO>>(_participant.Scores);
        }

        /// <summary>
        /// Build and runs an assignment and checks the scores for the user submitted code.
        /// </summary>
        /// <param name="runJob">See RunController.</param>
        /// <returns></returns>
        public HttpResponseMessage Post(RunController.RunJob runJob){
            _compiler.CompileFromPlainText(_participant, runJob.Code);
            var runResult = _runner.RunAndCheckInput(_participant);
            var correctOutput = IsCorrectOutput(runResult);
            var timeDifference = TimeDifferenceHelper.GetTimeDifference(_participant.Progress.StartTime);

            var score = ScoreHelper.CreateScore(_participant.Progress.Assignment, _participant, correctOutput, timeDifference);

            _context.Scores.Add(score);
            _context.Progresses.Remove(_participant.Progress);
            _context.SaveChanges();

            return Request.CreateResponse(HttpStatusCode.Created);
        }

        private bool IsCorrectOutput(RunResult runResult){
            return (runResult.Output.Trim().Equals(_participant.Progress.Assignment.RunCodeOuput));
        }
    }
}