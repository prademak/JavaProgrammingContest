using System;

namespace JavaProgrammingContest.Process
{
    internal interface ICompiler
    {
        CompileResult Compile(String arguments);
    }
}