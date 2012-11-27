using System;
using System.Collections.Generic;
using System.Linq;

namespace JavaProgrammingContest.Process
{
    internal class Program
    {
        private const String Javac = @"javac";
        private const String Arguments = @"helloWorld.java";
        private static double CompileTime;

        public Program()
        {
            CompileTime = 0;
        }

        private static void Main(string[] args)
        {
            RunExercise();
            Console.ReadLine();
        }

        private static void RunExercise()
        {
            var javaExecute = CreateProcess(Javac, Arguments);
            String errorMessage = CompileJavaCode(javaExecute);

            javaExecute = CreateProcess("java", "helloWorld");
            String[] input = { "2", "5", "1 1 1 2 2", "5", "2 1 3 1 2" };
            String[] rightOutput = { "Hello, World" };
            String[] outputJava = RunJavaCode(javaExecute, input);

            Console.Write(rightOutput.SequenceEqual(outputJava));
        }

        private static String[] RunJavaCode(System.Diagnostics.Process javaExecute, IEnumerable<string> input)
        {
            var outp = new List<string>();
            javaExecute.Start();

            var inputWriter = javaExecute.StandardInput;
            foreach (string inp in input)
            {
                inputWriter.WriteLine(inp);
            }
            inputWriter.Close();

            while (!javaExecute.StandardOutput.EndOfStream)
            {
                outp.Add(javaExecute.StandardOutput.ReadLine());
            }

            javaExecute.WaitForExit();
            javaExecute.Close();

            return outp.ToArray();
        }

        private static String CompileJavaCode(System.Diagnostics.Process javaExecute)
        {
            javaExecute.Start();
            DateTime startTime = javaExecute.StartTime;

            String errorMessage = javaExecute.StandardError.ReadToEnd();

            DateTime exitTime = javaExecute.ExitTime;
            TimeSpan timeSpan = exitTime - startTime;
            CompileTime = timeSpan.TotalSeconds;
            Console.WriteLine("Compile time: " + CompileTime + " seconds.");

            return errorMessage;
        }

        private static System.Diagnostics.Process CreateProcess(String fileName, String arguments)
        {
            var javaExecute = new System.Diagnostics.Process
            {
                StartInfo =
                {
                    FileName = fileName,
                    Arguments = arguments,
                    UseShellExecute = false,
                    RedirectStandardOutput = true,
                    RedirectStandardError = true,
                    RedirectStandardInput = true
                }
            };

            return javaExecute;
        }
    }
}