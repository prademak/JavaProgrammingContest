using System.Configuration;
using JavaProgrammingContest.Process.Compiler.Model;

namespace JavaProgrammingContest.Process.Compiler.Java.Helpers{
    public class JavaCompilerProcess : System.Diagnostics.Process, ICompilerProcess{
        public JavaCompilerProcess(){
            StartInfo.UseShellExecute = false;
            StartInfo.RedirectStandardOutput = true;
            StartInfo.RedirectStandardError = true;
            StartInfo.FileName = new AppSettingsReader().GetValue("javaCompiler", typeof(System.String)).ToString();

        }
    

        public CompilerResult Compile(string arguments){
            StartInfo.Arguments = arguments;
            
            Start();

            var compilerResult = new CompilerResult{
                StandardOutput = StandardOutput.ReadToEnd(),
                StandardError = StandardError.ReadToEnd(),
                CompilationTime = (int) ExitTime.Subtract(StartTime).TotalMilliseconds
            };

            WaitForExit();

            return compilerResult;
        }
    }
}