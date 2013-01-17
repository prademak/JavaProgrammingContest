using System;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using JavaProgrammingContest.DataAccess.Context;
using JavaProgrammingContest.Domain.Entities;
using JavaProgrammingContest.Process.Compiler;
using JavaProgrammingContest.Process.Runner;
using WebMatrix.WebData;
using System.Diagnostics.CodeAnalysis;

namespace JavaProgrammingContest.Web.API
{
    /// <summary>
    ///     Controller for interface interaction with the processing objects.
    /// </summary>
    public class RunController : ApiController
    {
        /// <summary>
        ///     Stores a runner instance.
        /// </summary>
        private readonly IRunner _runner;

        /// <summary>
        ///     Stores a Compiler instance.
        /// </summary>
        private readonly ICompiler _compiler;

        /// <summary>
        ///     Stores the Database Context
        /// </summary>
        private readonly IDbContext _context;

        /// <summary>
        ///     API Run-processor Interface
        /// </summary>
        /// <param name="context">Database Context</param>
        /// <param name="compiler">Compiler to use.</param>
        /// <param name="runner">Runner to use.</param>
        /// 
        private Participant _participant;
        public RunController(IDbContext context, ICompiler compiler, IRunner runner, Participant participant = null)
        {
            _context = context;
            _compiler = compiler;
            _runner = runner;
            _participant = participant == null ? getCurrentParticipant() : participant;
        }

        [ExcludeFromCodeCoverage]
        private Participant getCurrentParticipant()
        {
            var participant = _context.Participants.Find(WebSecurity.GetUserId(User.Identity.Name));
            return participant;
        }

        /// <summary>
        ///     Runs a piece of code through the runner.
        /// </summary>
        /// <param name="runJob"></param>
        /// <returns></returns>
        public HttpResponseMessage Run(RunJob runJob)
        { 
            var result = _compiler.CompileFromPlainText(_participant, runJob.Code);
            var runResult = _runner.Run(_participant);

             var response = new RunResult{
                BuildResult = new BuildController.BuildResult{
                    Output = result.StandardOutput,
                    Error = result.StandardError,
                    CompileTime = result.CompilationTime
                },
                Error = runResult.Error,
                Output = result.StandardError.Length > 0 ? "Build failed. See build tab" : runResult.Output,
                RunTime = runResult.RunTime
            };

            return Request.CreateResponse(HttpStatusCode.Created, response);
        }

        /// <summary>
        ///     Defines the datastructure for a run job.
        /// </summary>
        public class RunJob
        {
            /// <summary>
            ///     Code to run.
            /// </summary>
            public string Code { get; set; }

            /// <summary>
            ///     Assignment Identifier where code is submitted for.
            /// </summary>
            public int Id { get; set; }
        }


        /// <summary>
        ///     Defined the Result of a run job.
        /// </summary>
        public class RunResult
        {
            /// <summary>
            ///     Result of the Build Job.
            /// </summary>
            public BuildController.BuildResult BuildResult { get; set; }

            /// <summary>
            ///     Time it took to run the code.
            /// </summary>
            public int RunTime { get; set; }

            public string Error { get; set; }
            /// <summary>
            ///     Output that was given by the code.
            /// </summary>
            public string Output { get; set; }
        }
    }
}