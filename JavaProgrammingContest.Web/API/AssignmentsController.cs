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
            return new[]{
                new Assignment{
                    Id = 1,
                    CodeGiven = "// Sample class\nclass HelloWorldApp {\n\tpublic static void main(String[] args){\n\t\tSystem.out.println(\"Hello World!\");\n\t}\n}\n",
                    Description = "Nullam ac venenatis arcu. Curabitur vitae malesuada sapien. Nam cursus, odio eget mollis rutrum, arcu ipsum pharetra diam, quis rhoncus magna sem non augue. Sed mollis rutrum dui, sed consequat ipsum congue eu. In luctus, orci id semper vehicula, neque lectus tristique lectus, eu interdum risus dolor non erat. Nullam ipsum eros, dignissim ac cursus non, ultrices vitae leo. Pellentesque mollis nisi ut orci euismod ac gravida magna aliquet. Aenean mi urna, fermentum ac lobortis condimentum, tincidunt in leo.",
                    Title = "This is a sample assignment",
                    MaxSolveTime = 900,
                },
                new Assignment{
                    Id = 2,
                    CodeGiven = "",
                    Description = "Quis rhoncus magna sem non augue. Sed mollis rutrum dui, sed consequat ipsum congue eu. In luctus, orci id semper vehicula, neque lectus tristique lectus, eu interdum risus dolor non erat. Nullam ipsum eros, dignissim ac cursus non, ultrices vitae leo. Pellentesque mollis nisi ut orci euismod ac gravida magna aliquet. Aenean mi urna, fermentum ac lobortis condimentum, tincidunt in leo.",
                    Title = "Another awesome assignment",
                    MaxSolveTime = 900
                }
            };

            // TODO: Schijf geef terug _context dot Assignments
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
                return CreatePostResponse(assignment);
            } catch (Exception){
                throw new HttpResponseException(HttpStatusCode.InternalServerError);
            }
        }

        private HttpResponseMessage CreatePostResponse(Assignment assignment){
            var response = Request.CreateResponse(HttpStatusCode.Created, assignment);
            //TODO set header.location
            return response;
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