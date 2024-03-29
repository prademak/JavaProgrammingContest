using JavaProgrammingContest.Process.Compiler.Model;

namespace JavaProgrammingContest.Process.Compiler.Java.Helpers{
    public class JavaCompilerProcess : System.Diagnostics.Process, ICompilerProcess{
        public ISettingsReader AppSettingsReader { get; set; }

        public JavaCompilerProcess(){
            AppSettingsReader = new SettingsReader();
            StartInfo.UseShellExecute = false;
            StartInfo.RedirectStandardOutput = true;
            StartInfo.RedirectStandardError = true;
            StartInfo.FileName = AppSettingsReader.GetValueAsString("javac_path");
        }

        public CompilerResult Compile(string arguments){
            StartInfo.Arguments = arguments;

            Start();
            var compilerResult = CreateCompilerResult();
            WaitForExit();

            return compilerResult;
        }

        private CompilerResult CreateCompilerResult(){
            return new CompilerResult{
                StandardOutput = StandardOutput.ReadToEnd(),
                StandardError = StandardError.ReadToEnd(),
                CompilationTime = (int) ExitTime.Subtract(StartTime).TotalMilliseconds
            };
        }
    }
}