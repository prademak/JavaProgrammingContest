using JavaProgrammingContest.Process.Runner.Model;

namespace JavaProgrammingContest.Process.Runner.Java.Helpers{
    public class JavaRunnerProcess : System.Diagnostics.Process, IRunnerProcess{
        public ISettingsReader AppSettingsReader { get; set; }

        public JavaRunnerProcess(){
            StartInfo.UseShellExecute = false;
            StartInfo.RedirectStandardInput = true;
            StartInfo.RedirectStandardOutput = true;
            StartInfo.RedirectStandardError = true;
            StartInfo.FileName = AppSettingsReader.GetValueAsString("java_path");
        }

        public RunResult Run(string arguments){
            return Run(arguments, null);
        }

        public RunResult Run(string arguments, string input){
            StartInfo.Arguments = arguments;
            Start();

            if (!string.IsNullOrEmpty(input))
                StandardInput.WriteLine(input);

            var runResult = CreateRunResult();
            WaitForExit();
            return runResult;
        }

        private RunResult CreateRunResult(){
            return new RunResult{
                Output = StandardOutput.ReadToEnd(),
                Error = StandardError.ReadToEnd(),
                RunTime = (int) ExitTime.Subtract(StartTime).TotalMilliseconds
            };
        }
    }
}