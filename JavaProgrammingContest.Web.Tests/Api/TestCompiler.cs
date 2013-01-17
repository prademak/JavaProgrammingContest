using JavaProgrammingContest.Domain.Entities;
using JavaProgrammingContest.Process.Compiler;
using JavaProgrammingContest.Process.Compiler.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace JavaProgrammingContest.Web.Tests.Api
{
    class TestCompiler : ICompiler
    {

        public ICompilerProcess CompilerProcess { get; set; }

        public CompilerResult CompileFromPlainText(Participant participant, string code)
        {
            CompilerResult compResultTest = new CompilerResult { CompilationTime = 100, StandardError = "", StandardOutput = "Hello World!" };
            return compResultTest;

        }
    }
}

