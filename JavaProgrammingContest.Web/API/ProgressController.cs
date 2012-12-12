using System;
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

        public ProgressController(IDbContext context){
            _context = context;
            
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="assignmentId"></param>
        /// <returns>HttpStatusCode.Ok + the progress object when given assignment is in progress by the currently logged in user</returns>
        /// <returns>HttpStatusCode.NotFound when given assignment is not in progress by the currently logged in user</returns>
        public HttpResponseMessage Get(int assignmentId){
            var participant = _context.Participants.Find(WebSecurity.GetUserId(User.Identity.Name));
            return participant.Progress.Assignment.Id == assignmentId
                       ? Request.CreateResponse(HttpStatusCode.OK, participant.Progress)
                       : Request.CreateErrorResponse(HttpStatusCode.NotFound,
                           "Given assignment is not in progress by the currently logged in user.");
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <param name="progress"></param>
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
            } catch (Exception ex){
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, "Error while saving to database.");
            }

            return Request.CreateResponse(HttpStatusCode.OK, new {progress.Id, progress.StartTime});
        }
    }
}