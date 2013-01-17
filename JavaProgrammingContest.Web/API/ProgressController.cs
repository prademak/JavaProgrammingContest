using System;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using AutoMapper;
using JavaProgrammingContest.DataAccess.Context;
using JavaProgrammingContest.Domain.Entities;
using JavaProgrammingContest.Web.DTO;
using JavaProgrammingContest.Web.Helpers;
using WebMatrix.WebData;
using System.Diagnostics.CodeAnalysis;

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
        /// 
        private Participant _participant;
        public ProgressController(IDbContext context, Participant participant = null){
            _context = context;
          _participant = participant == null ? getCurrentParticipant() : participant;
        }

        /// <summary>
        ///     Used to check if the assignment with assignmentId is in progress, and if so, retrieves the data.
        ///     Usage is /api/progress/?assignmentId=id, where id is the identifier you'd like to use.
        /// </summary>
        /// <param name="assignmentId">The identifier of the assignment to chck progress for.</param>
        /// <returns>HttpStatusCode.Ok + the progress object when given assignment is in progress by the currently logged in user</returns>
        /// <returns>HttpStatusCode.NotFound when given assignment is not in progress by the currently logged in user</returns>
        public HttpResponseMessage Get(int assignmentId){

            return _participant.Progress == null
                       ? Request.CreateErrorResponse(HttpStatusCode.NotFound, "No assignments in progress for current user.")
                       : _participant.Progress.Assignment.Id == assignmentId
                             ? Request.CreateResponse(HttpStatusCode.OK, Mapper.Map<Progress, ProgressDTO>(_participant.Progress))
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
            if (_participant.Progress != null)
                return Request.CreateErrorResponse(HttpStatusCode.Forbidden, "Can't start another assignment.");

            var assignment = _context.Assignments.Find(id);
            progress.Assignment = assignment;
            progress.Participant = _participant;
            progress.StartTime = DateTime.Now;

            try{
                _context.Progresses.Add(progress);
                _context.SaveChanges();
            } catch (Exception){
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, "Error while saving to database.");
            }

            return Request.CreateResponse(HttpStatusCode.OK, Mapper.Map<Progress, ProgressDTO>(progress));
        }

        /// <summary>
        ///     Delete the progress for a specific assignment and the currently logged in user.
        /// </summary>
        public HttpResponseMessage Delete(){
            // Tried to fixx the "not being able to start an assignment if you reloaded the page during one" problem.

            if (_participant.Progress == null)
                return Request.CreateErrorResponse(HttpStatusCode.Forbidden, "No assignment was started.");

            try{
                var timeDifference = TimeDifferenceHelper.GetTimeDifference(_participant.Progress.StartTime);
                if (timeDifference > _participant.Progress.Assignment.MaxSolveTime)
                    timeDifference = _participant.Progress.Assignment.MaxSolveTime;
                var score = ScoreHelper.CreateScore(_participant.Progress.Assignment, _participant, false, timeDifference);

                _context.Scores.Add(score);
                _context.Progresses.Remove(_participant.Progress);

                _context.SaveChanges();
            } catch (Exception){
  
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, "Error while saving to database.");
            }

            return Request.CreateResponse(HttpStatusCode.OK);
        }
        [ExcludeFromCodeCoverage]
        private Participant getCurrentParticipant()
        {
            var participant = _context.Participants.Find(WebSecurity.GetUserId(User.Identity.Name));
            return participant;
        }
    }

       
}