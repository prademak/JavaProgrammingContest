using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using JavaProgrammingContest.DataAccess;
using JavaProgrammingContest.Domain.Entities;

namespace JavaProgrammingContest.Service{
    public class AssignmentsController : ApiController{
        private readonly IRepository<Assignment> _assignmentsRepository;

        public AssignmentsController(IRepository<Assignment> assignmentsRepository){
            _assignmentsRepository = assignmentsRepository;
        }

        public IEnumerable<Assignment> Get(){
            return _assignmentsRepository.GetAll();
        }

        public Assignment Get(int id){
            var assignment = _assignmentsRepository.GetById(id);

            if (assignment == null)
                throw new HttpResponseException(HttpStatusCode.NotFound);

            return assignment;
        }

        public HttpResponseMessage Post(Assignment assignment){
            if (!ModelState.IsValid)
                throw new HttpResponseException(HttpStatusCode.BadRequest);

            try{
                _assignmentsRepository.Add(assignment);
                _assignmentsRepository.SaveChanges();

                var response = Request.CreateResponse(HttpStatusCode.Created, assignment);

                //TODO set to correct route!
                var uri = Url.Link("route here", new{id = assignment.Id});
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
                var dbAssignment = _assignmentsRepository.GetById(id);

                //dbAssignment.X = assignment.X;
                //TODO set properties

                _assignmentsRepository.SaveChanges();
            } catch (Exception){
                throw new HttpResponseException(HttpStatusCode.InternalServerError);
            }
        }

        public void Delete(int id){
            try{
                var assignment = _assignmentsRepository.GetById(id);
                if (assignment == null)
                    throw new HttpResponseException(HttpStatusCode.NotFound);

                _assignmentsRepository.Remove(assignment);
            } catch (Exception){
                throw new HttpResponseException(HttpStatusCode.InternalServerError);
            }
        }
    }
}