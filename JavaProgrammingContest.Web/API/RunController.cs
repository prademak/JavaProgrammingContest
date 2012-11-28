using System.Net;
using System.Net.Http;
using System.Web.Http;
using JavaProgrammingContest.Process.Compiler;
using JavaProgrammingContest.Process.Runner;

namespace JavaProgrammingContest.Web.API{
    public class RunController : ApiController{
        private readonly IRunner _runner;
        private readonly ICompiler _compiler;

        public RunController(ICompiler compiler, IRunner runner){
            _compiler = compiler;
            _runner = runner;
        }

        public HttpResponseMessage Run(RunJob runJob){
            var result = _compiler.CompileFromPlainText(runJob.Code);
            var runResult = _runner.Run();

            return Request.CreateResponse(HttpStatusCode.Created,
                new RunResult{
                    BuildResult = new BuildResult{
                        Output = result.StandardOutput,
                        Error = result.StandardError,
                        CompileTime = result.CompilationTime
                    },
                    Output = runResult.Output,
                    RunTime = runResult.RunTime
                });
        }
    }

    public class RunJob{
        public string Code { get; set; }
    }

    public class RunResult{
        public BuildResult BuildResult { get; set; }
        public int RunTime { get; set; }
        public string Output { get; set; }
    }
}