using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using JavaProgrammingContest.DataAccess.Context;
using JavaProgrammingContest.Domain.Entities;
using WebMatrix.WebData;

namespace JavaProgrammingContest.Web.API{
    [Authorize]
    public class ProgressController : ApiController{
        private readonly IDbContext _context;
        private readonly Participant _participant;

        public ProgressController(IDbContext context){
            _context = context;
            _participant = _context.Participants.Find(WebSecurity.GetUserId(User.Identity.Name));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="assignmentId"></param>
        /// <returns>HttpStatusCode.Ok + the progress object when given assignment is in progress by the currently logged in user</returns>
        /// <returns>HttpStatusCode.NotFound when given assignment is not in progress by the currently logged in user</returns>
        public HttpResponseMessage Get(int assignmentId){
            return _participant.Progress.ContestAssignment.Assignment.Id == assignmentId
                       ? Request.CreateResponse(HttpStatusCode.OK, _participant.Progress)
                       : Request.CreateErrorResponse(HttpStatusCode.NotFound,
                           "Given assignment is not in progress by the currently logged in user.");
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="assignmentId"></param>
        /// <param name="progress"></param>
        /// <returns></returns>
        public HttpResponseMessage Put(int assignmentId, Progress progress){
            if (!ModelState.IsValid)
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, "Paramters not valid.");

            if (_participant.Progress != null)
                return Request.CreateErrorResponse(HttpStatusCode.Forbidden, "Can't start another assignment.");

            var assignment = _context.Assignments.Find(assignmentId);
            progress.ContestAssignment = assignment.ContestAssignments.First();
            progress.Participant = _participant;

            try{
                _context.Progresses.Add(progress);
                _context.SaveChanges();
            } catch (System.Exception){
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, "Error while saving to database.");
            }

            return Request.CreateResponse(HttpStatusCode.OK, progress);
        }
    }
}