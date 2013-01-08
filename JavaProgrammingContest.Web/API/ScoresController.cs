using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using AutoMapper;
using JavaProgrammingContest.DataAccess.Context;
using JavaProgrammingContest.Domain.Entities;
using JavaProgrammingContest.Process.Compiler;
using JavaProgrammingContest.Process.Runner;
using JavaProgrammingContest.Web.DTO;
using WebMatrix.WebData;

namespace JavaProgrammingContest.Web.API{
    /// <summary>
    ///     Controller for interface interaction with the database object Score.
    /// </summary>
    public class ScoresController : ApiController{
        private readonly IRunner _runner;
        private readonly ICompiler _compiler;
        private readonly IDbContext _context;

        public ScoresController(IDbContext context, ICompiler compiler, IRunner runner){
            _context = context;
            _compiler = compiler;
            _runner = runner;
        }

        /// <summary>
        /// Returns a list of scores from 
        /// </summary>
        public IEnumerable<ScoreDTO> GetScoresFromCurrentUser(){
            var participant = _context.Participants.Find(WebSecurity.GetUserId(User.Identity.Name));
            return Mapper.Map<IEnumerable<Score>, IEnumerable<ScoreDTO>>(participant.Scores);
        }

        /// <summary>
        /// Build and runs an assignment and checks the scores for the user submitted code.
        /// </summary>
        /// <param name="runJob">See RunController.</param>
        /// <returns></returns>
        public HttpResponseMessage Post(RunController.RunJob runJob){
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

        private static double GetTimeDifference(DateTime startTime){
            var elapsed = DateTime.Now - startTime;
            var timeDifference = elapsed.TotalSeconds;
            return Math.Floor(timeDifference*100)/100;
        }

        private static Score CreateScore(Assignment assignment, Participant participant, bool correctOutput, double timeDifference){
            return new Score{
                Assignment = assignment,
                IsCorrectOutput = correctOutput,
                Participant = participant,
                TimeSpent = timeDifference
            };
        }
    }
}