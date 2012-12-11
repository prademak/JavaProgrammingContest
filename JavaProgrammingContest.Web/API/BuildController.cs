using System.Net;
using System.Net.Http;
using System.Web.Http;
using JavaProgrammingContest.DataAccess.Context;
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
            var participant = _context.Participants.Find(WebSecurity.GetUserId(User.Identity.Name));
            var result = _compiler.CompileFromPlainText(participant, buildJob.Code);

            //TODO use CompilerResult class as response
            return Request.CreateResponse(HttpStatusCode.Created,
                new BuildResult{
                    Output = result.StandardOutput == "" ? "Build succeeded" : "",
                    Error = result.StandardError,
                    CompileTime = result.CompilationTime
                });
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