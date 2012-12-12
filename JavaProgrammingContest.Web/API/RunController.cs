using System.Net;
using System.Net.Http;
using System.Web.Http;
using JavaProgrammingContest.DataAccess.Context;
using JavaProgrammingContest.Process.Compiler;
using JavaProgrammingContest.Process.Runner;
using WebMatrix.WebData;

namespace JavaProgrammingContest.Web.API{
    public class RunController : ApiController{
        private readonly IRunner _runner;
        private readonly ICompiler _compiler;
        private readonly IDbContext _context;

        public RunController(IDbContext context, ICompiler compiler, IRunner runner){
            _context = context;
            _compiler = compiler;
            _runner = runner;
        }

        public HttpResponseMessage Run(RunJob runJob){
            var participant = _context.Participants.Find(WebSecurity.GetUserId(User.Identity.Name));
            var result = _compiler.CompileFromPlainText(participant, runJob.Code);
            var runResult = _runner.Run();

            var response = new RunResult{
                BuildResult = new BuildResult{
                    Output = result.StandardOutput,
                    Error = result.StandardError,
                    CompileTime = result.CompilationTime
                },
                Output = result.StandardError != "" ? "Build failed. See build tab" : runResult.Output,
                RunTime = runResult.RunTime
            };

            return Request.CreateResponse(HttpStatusCode.Created, response);
        }
    }

    public class RunJob{
        public string Code { get; set; }
        public int Id { get; set; }
    }

    public class RunResult{
        public BuildResult BuildResult { get; set; }
        public int RunTime { get; set; }
        public string Output { get; set; }
    }
}