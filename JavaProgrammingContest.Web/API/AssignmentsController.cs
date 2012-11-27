using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using JavaProgrammingContest.DataAccess.Context;
using JavaProgrammingContest.Domain.Entities;

namespace JavaProgrammingContest.Web.API{
    public class AssignmentsController : ApiController{
        private readonly IDbContext _context;

        public AssignmentsController(IDbContext context){
            _context = context;
        }

        public IEnumerable<Assignment> Get(){
            return _context.Assignments;
        }

        public Assignment Get(int id){
            var assignment = _context.Assignments.Find(id);

            if (assignment == null)
                throw new HttpResponseException(HttpStatusCode.NotFound);

            return assignment;
        }

        public HttpResponseMessage Post(Assignment assignment){
            if (!ModelState.IsValid)
                throw new HttpResponseException(HttpStatusCode.BadRequest);

            try{
                _context.Assignments.Add(assignment);
                _context.SaveChanges();

                var response = Request.CreateResponse(HttpStatusCode.Created, assignment);

                var uri = Url.Link("DefaultApi", new{id = assignment.Id});
                if (uri != null)
                    response.Headers.Location = new Uri(uri);

                return response;
            } catch (Exception){
                throw new HttpResponseException(HttpStatusCode.InternalServerError);
            }
        }

        public void Put(int id, Assignment assignment){
            if (!ModelState.IsValid)
                throw new HttpResponseException(HttpStatusCode.BadRequest);

            try{
                var dbAssignment = _context.Assignments.Find(id);

                //TODO set properties

                _context.SaveChanges();
            } catch (Exception){
                throw new HttpResponseException(HttpStatusCode.InternalServerError);
            }
        }

        public void Delete(int id){
            try{
                var assignment = _context.Assignments.Find(id);
                if (assignment == null)
                    throw new HttpResponseException(HttpStatusCode.NotFound);

                _context.Assignments.Remove(assignment);
            } catch (Exception){
                throw new HttpResponseException(HttpStatusCode.InternalServerError);
            }
        }
    }
}