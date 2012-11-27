using System;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace JavaProgrammingContest.Web.API{
    public class BuildController : ApiController{
        public BuildController() {}

        public HttpResponseMessage Post(BuildJob buildJob){
            return Request.CreateResponse(HttpStatusCode.Created,
                new BuildResult{
                    CompileTime = 3000,
                    Error =
                        @"C:\Users\martijn\Documents\Visual Studio 11\Projects\JavaProgrammingContest\JavaProgrammingContest\JavaProgrammingContest.Web\API\BuildController.cs(11,140,11,141): error CS1002: ; expected",
                    Output = @"========== Build: 2 succeeded, 0 failed, 3 up-to-date, 2 skipped =========="
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