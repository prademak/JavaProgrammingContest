﻿using System;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using JavaProgrammingContest.DataAccess.Context;
using JavaProgrammingContest.Domain.Entities;
using JavaProgrammingContest.Process.Compiler;
using JavaProgrammingContest.Process.Runner;
using WebMatrix.WebData;

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
        public RunController(IDbContext context, ICompiler compiler, IRunner runner)
        {
            _context = context;
            _compiler = compiler;
            _runner = runner;
        }

        /// <summary>
        ///     Runs a piece of code through the runner.
        /// </summary>
        /// <param name="runJob"></param>
        /// <returns></returns>
        public HttpResponseMessage Run(RunJob runJob)
        {

            if (runJob == null)
            {
                runJob = new RunJob { Code = "// Sample class\nclass Solution {\n\tpublic static void main(String[] args){\n\t\tSystem.out.println(\"Hello World!\");\n\t}\n}\n" };
            }
          

            var participant = _context.Participants.Find(WebSecurity.GetUserId(User.Identity.Name));
            if (participant == null)
            {
                Random random = new Random();
                var id = random.Next(9000000);
                participant = new Participant { Email = "vincent@mail.nl", Id = id };
            }
            var result = _compiler.CompileFromPlainText(participant, runJob.Code);
            var runResult = _runner.Run(participant);

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