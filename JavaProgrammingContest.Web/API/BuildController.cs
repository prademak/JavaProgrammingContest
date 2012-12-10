using System;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using JavaProgrammingContest.DataAccess.Context;
using JavaProgrammingContest.Domain.Entities;
using JavaProgrammingContest.Process.Compiler;
using WebMatrix.WebData;

namespace JavaProgrammingContest.Web.API{
    public class BuildController : ApiController{
        private readonly ICompiler _compiler;
        private readonly IDbContext _context;

        public BuildController(IDbContext context, ICompiler compiler){
            _context = context;
            _compiler = compiler;
        }

        public HttpResponseMessage Post(BuildJob buildJob){
            var userid = _context.Participants.Find(WebSecurity.GetUserId(User.Identity.Name));

            Assignment currentAssignment;
            
            try{
                 currentAssignment= GetCurrentAssignment(userid);
            } catch (Exception ex){
                return Request.CreateErrorResponse(HttpStatusCode.NotFound, "Currently loged-in user hasn't started an assigment yet!");
            }

            var result = _compiler.CompileFromPlainText(buildJob.Code);

            //TODO use CompilerResult class as response
            return Request.CreateResponse(HttpStatusCode.Created,
                new BuildResult{
                    Output = result.StandardOutput,
                    Error = result.StandardError,
                    CompileTime = result.CompilationTime
                });
        }

        private Assignment GetCurrentAssignment(Participant userid){
            return _context.Participants.Find(userid).Progress.ContestAssignment.Assignment;
        }
    }

    public class BuildJob{
        public string Code { get; set; }
    }

    public class BuildResult{
        public int CompileTime { get; set; }
        public string Error { get; set; }
        public string Output { get; set; }
    }
}