using System;

namespace JavaProgrammingContest.Process
{
    public class Compiler : ICompiler
    {
        private const String CompilerBin = @"javac";

        public CompileResult Compile(String arguments)
        {
            var cr = new CompileResult();
            System.Diagnostics.Process javaExecute = NewProcess(CompilerBin, arguments);
            javaExecute.Start();
            DateTime startTime = javaExecute.StartTime;
            String errorMessage = javaExecute.StandardError.ReadToEnd();
            cr.CompileTime = (javaExecute.ExitTime - startTime).TotalSeconds;
            javaExecute.Close();
            cr.CompileFailed = errorMessage.Length > 0;
            cr.ClassFile = arguments.Replace(".java", "");
            return cr;
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