using System.Collections.Generic;

namespace JavaProgrammingContest.Process
{
    internal interface IRunner
    {
        RunResult RunCode(string classFile, string[] rightOutput, IEnumerable<string> input = null);
    }
}