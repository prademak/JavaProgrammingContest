using JavaProgrammingContest.Domain.Entities;
using JavaProgrammingContest.Process.Runner;
using JavaProgrammingContest.Process.Runner.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace JavaProgrammingContest.Web.Tests.Api
{
    class TestRunner : IRunner
    {
        public RunResult Run(Participant participant)
        {
            return new RunResult { Error = "", Output = "Hello user:" + participant.Id, RunTime = 100 }; 
        }

        public RunResult RunAndCheckInput(Participant participant)
        {
            return new RunResult { Error = "", Output = "Hello user:" + participant.Id, RunTime = 100 }; 
        }
    }
}
