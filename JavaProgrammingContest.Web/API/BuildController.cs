using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Security;
using System.Web.UI;
using JavaProgrammingContest.Process.Compiler;

namespace JavaProgrammingContest.Web.API{
    public class BuildController : ApiController{
        private readonly ICompiler _compiler;

        public BuildController(ICompiler compiler){
            _compiler = compiler;
        }

        public HttpResponseMessage Post(BuildJob buildJob){
            var user = Membership.GetUser(User.Identity.Name);
            var result = _compiler.CompileFromPlainText(buildJob.Code);
            
            //TODO use CompilerResult class as response
            return Request.CreateResponse(HttpStatusCode.Created,
                new BuildResult{
                    Output = result.StandardOutput,
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