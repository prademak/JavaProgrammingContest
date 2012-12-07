using System.Net;
using System.Net.Http;
using System.Security.Principal;
using System.Web.Http;
using JavaProgrammingContest.Process.Compiler;
using JavaProgrammingContest.Process.Compiler.Model;
using WebMatrix.WebData;

namespace JavaProgrammingContest.Web.API
{
    public class BuildController : ApiController
    {
        private readonly ICompiler _compiler;

        public BuildController(ICompiler compiler)
        {
            _compiler = compiler;
        }

        public HttpResponseMessage Post(BuildJob buildJob)
        {
            IIdentity usr = User.Identity;
            int test = WebSecurity.GetUserId(User.Identity.Name);

            CompilerResult result = _compiler.CompileFromPlainText(buildJob.Code);

            //TODO use CompilerResult class as response
            return Request.CreateResponse(HttpStatusCode.Created,
                                          new BuildResult
                                              {
                                                  Output = result.StandardOutput,
                                                  Error = result.StandardError,
                                                  CompileTime = result.CompilationTime
                                              });
        }
    }

    public class BuildJob
    {
        public string Code { get; set; }
    }

    public class BuildResult
    {
        public int CompileTime { get; set; }
        public string Error { get; set; }
        public string Output { get; set; }
    }
}