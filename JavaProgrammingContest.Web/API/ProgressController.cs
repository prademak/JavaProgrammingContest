using System;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using AutoMapper;
using JavaProgrammingContest.DataAccess.Context;
using JavaProgrammingContest.Domain.Entities;
using JavaProgrammingContest.Web.API;
using JavaProgrammingContest.Web.DTO;
using WebMatrix.WebData;

namespace JavaProgrammingContest.Web.API{
    /// <summary>
    ///     Controller for interface interraction with the Progress of assignments within the competition system.
    /// </summary>
    [Authorize]
    public class ProgressController : ApiController{
        /// <summary>
        ///     Stores the Database Context
        /// </summary>
        private readonly IDbContext _context;

        /// <summary>
        ///     Constructs the Progress WebAPI Controller.
        /// </summary>
        /// <param name="context">Database Context</param>
        public ProgressController(IDbContext context){
            _context = context;
        }

        /// <summary>
        ///     Used to check if the assignment with assignmentId is in progress, and if so, retrieves the data.
        ///     Usage is /api/progress/?assignmentId=id, where id is the identifier you'd like to use.
        /// </summary>
        /// <param name="assignmentId">The identifier of the assignment to chck progress for.</param>
        /// <returns>HttpStatusCode.Ok + the progress object when given assignment is in progress by the currently logged in user</returns>
        /// <returns>HttpStatusCode.NotFound when given assignment is not in progress by the currently logged in user</returns>
        public HttpResponseMessage Get(int assignmentId){
            var participant = _context.Participants.Find(WebSecurity.GetUserId(User.Identity.Name));
            return participant.Progress == null
                        ? Request.CreateErrorResponse(HttpStatusCode.NotFound, "No assignments in progress for current user.")
                        : participant.Progress.Assignment.Id == assignmentId
                             ? Request.CreateResponse(HttpStatusCode.OK, Mapper.Map<Progress, ProgressDTO>(participant.Progress))
                             : Request.CreateErrorResponse(HttpStatusCode.NotFound,
                                 "Given assignment is not in progress by the currently logged in user.");
        }

        /// <summary>
        ///     Create a new progress item for the currently logged in user and the given assignment.
        /// </summary>
        /// <param name="id">Identifier of the assignment to create a progress item for.</param>
        /// <param name="progress">May be empty, or contain at least the StartTime (Which is currently ignored)</param>
        /// <returns></returns>
        public HttpResponseMessage Put(int id, Progress progress){
            var participant = _context.Participants.Find(WebSecurity.GetUserId(User.Identity.Name));

            if (participant.Progress != null)
                return Request.CreateErrorResponse(HttpStatusCode.Forbidden, "Can't start another assignment.");

            var assignment = _context.Assignments.Find(id);
            progress.Assignment = assignment;
            progress.Participant = participant;
            progress.StartTime = DateTime.Now;

            try{
                _context.Progresses.Add(progress);
                _context.SaveChanges();
            } catch (SqlException ex){
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, "Error while saving to database.");
            }

            return Request.CreateResponse(HttpStatusCode.OK, Mapper.Map<Progress, ProgressDTO>(progress));
        }

        /// <summary>
        ///     Delete the progress for a specific assignment and the currently logged in user.
        /// </summary>
        /// <param name="id">Assignment id to look for.</param>
        /// <returns></returns>
        public HttpResponseMessage Delete(){
            // Tried to fixx the "not being able to start an assignment if you reloaded the page during one" problem.
            var participant = _context.Participants.Find(WebSecurity.GetUserId(User.Identity.Name));

            if (participant.Progress == null)
                return Request.CreateErrorResponse(HttpStatusCode.Forbidden, "No assignment was started.");

            try{

                var curProgress = (from progress in _context.Progresses.ToList()
                                    where progress.Participant == participant
                                    select progress);
                var timeDifference = GetTimeDifference(curProgress.First().StartTime);
                if (timeDifference > curProgress.First().Assignment.MaxSolveTime) timeDifference = curProgress.First().Assignment.MaxSolveTime;
                var score = CreateScore(curProgress.First().Assignment, participant, false, timeDifference);

                _context.Scores.Add(score);
                _context.Progresses.Remove(curProgress.First());
                
                _context.SaveChanges();
            }catch (SqlException ex){
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, "Error while saving to database.");
            }

            return Request.CreateResponse(HttpStatusCode.OK);
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