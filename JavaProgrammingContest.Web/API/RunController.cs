using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace JavaProgrammingContest.Web.API{
    public class RunController : ApiController{
        public RunController() {}

        public HttpResponseMessage Run(RunJob runJob){
            var buildResult = new BuildResult();
            buildResult.CompileTime = 3000;
            buildResult.Error = "";
            buildResult.Output = "========== Build: 2 succeeded, 0 failed, 3 up-to-date, 2 skipped ==========";

            return Request.CreateResponse(HttpStatusCode.Created,
                new RunResult{
                    BuildResult = buildResult,
                    Output = "Hello World!",
                    RunTime = 3000
                });
        }
    }

    public class RunJob {}

    public class RunResult{
        public BuildResult BuildResult { get; set; }
        public int RunTime { get; set; }
        public string Output { get; set; }
    }
}