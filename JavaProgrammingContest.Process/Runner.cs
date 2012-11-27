using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace JavaProgrammingContest.Process
{
    public class Runner : IRunner
    {
        private const string ExecuteBin = "java";

        public RunResult RunCode(string classFile, string[] rightOutput, IEnumerable<string> input = null)
        {
            var rr = new RunResult();
            System.Diagnostics.Process javaExecute = NewProcess(ExecuteBin, classFile);
            javaExecute.Start();
            if (input != null)
            {
                GiveInputToProcess(input, javaExecute);
            }

            IEnumerable<string> outp = GetOutputFromProcess(javaExecute);
            rr.OutputIsRight = rightOutput.SequenceEqual(outp);
            rr.Output = outp.ToString();
            javaExecute.WaitForExit();
            javaExecute.Close();
            return rr;
        }

        private static IEnumerable<string> GetOutputFromProcess(System.Diagnostics.Process javaExecute)
        {
            var outp = new List<string>();
            while (!javaExecute.StandardOutput.EndOfStream)
            {
                outp.Add(javaExecute.StandardOutput.ReadLine());
            }
            return outp;
        }

        private static void GiveInputToProcess(IEnumerable<string> input, System.Diagnostics.Process javaExecute)
        {
            StreamWriter inputWriter = javaExecute.StandardInput;
            foreach (string inp in input)
            {
                inputWriter.WriteLine(inp);
            }
            inputWriter.Close();
        }


        private static System.Diagnostics.Process NewProcess(String fileName, String arguments)
        {
            var javaExecute = new System.Diagnostics.Process();
            javaExecute.StartInfo.FileName = fileName;
            javaExecute.StartInfo.Arguments = arguments;
            javaExecute.StartInfo.UseShellExecute = false;
            javaExecute.StartInfo.RedirectStandardOutput = true;
            javaExecute.StartInfo.RedirectStandardError = true;
            javaExecute.StartInfo.RedirectStandardInput = true;
            return javaExecute;
        }
    }
}