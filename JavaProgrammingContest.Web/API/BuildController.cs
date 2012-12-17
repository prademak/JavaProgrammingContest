using System.Net;
using System.Net.Http;
using System.Web.Http;
using JavaProgrammingContest.DataAccess.Context;
using JavaProgrammingContest.Process.Compiler;
using WebMatrix.WebData;

namespace JavaProgrammingContest.Web.API{
    /// <summary>
    ///     Controller for interface interaction with the build-processing object.
    /// </summary>
    public class BuildController : ApiController{
        /// <summary>
        ///     Stores a Compiler instance.
        /// </summary>
        private readonly ICompiler _compiler;

        /// <summary>
        ///     Stores the Database Context
        /// </summary>
        private readonly IDbContext _context;

        /// <summary>
        ///     API Build Proccessor Interface
        /// </summary>
        /// <param name="context"></param>
        /// <param name="compiler"></param>
        public BuildController(IDbContext context, ICompiler compiler){
            _context = context;
            _compiler = compiler;
        }

        /// <summary>
        ///     
        /// </summary>
        /// <param name="buildJob"></param>
        /// <returns></returns>
        public HttpResponseMessage Post(BuildJob buildJob){
            var participant = _context.Participants.Find(WebSecurity.GetUserId(User.Identity.Name));
            var result = _compiler.CompileFromPlainText(participant, buildJob.Code);

            //TODO use CompilerResult class as response
            return Request.CreateResponse(HttpStatusCode.Created,
                new BuildResult{
                    Output = result.StandardOutput == "" && result.StandardError == "" ? "Build succeeded" : "",
                    Error = result.StandardError,
                    CompileTime = result.CompilationTime
                });
        }
    }

    /// <summary>
    ///     Defines the data-structure for a Build Job
    /// </summary>
    public class BuildJob{
        public string Code { get; set; }
    }

    /// <summary>
    ///     Result of a Build-Job.
    /// </summary>
    public class BuildResult{
        public int CompileTime { get; set; }
        public string Error { get; set; }
        public string Output { get; set; }
    }
}