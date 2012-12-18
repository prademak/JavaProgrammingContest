using System.Configuration;
using JavaProgrammingContest.Process.Runner.Model;

namespace JavaProgrammingContest.Process.Runner.Java.Helpers{
    public class JavaRunnerProcess : System.Diagnostics.Process, IRunnerProcess{
        public JavaRunnerProcess(){
            StartInfo.UseShellExecute = false;
            StartInfo.RedirectStandardInput = true;
            StartInfo.RedirectStandardOutput = true;
            StartInfo.RedirectStandardError = true;
            StartInfo.FileName = new AppSettingsReader().GetValue("java_path", typeof(System.String)).ToString();
        }

        public RunResult Run(string arguments){
            StartInfo.Arguments = arguments;
            Start();
            var runResult = new RunResult
            {
                Output = StandardOutput.ReadToEnd(),
                Error = StandardError.ReadToEnd(),
                RunTime = (int)ExitTime.Subtract(StartTime).TotalMilliseconds
            };
            WaitForExit();
            return runResult;
        }

        public RunResult Run(string arguments, string input)
        {
            StartInfo.Arguments = arguments;
            Start(); 
            var stdInput = StandardInput;
            stdInput.WriteLine(input);

            var runResult = new RunResult
            {
                Output = StandardOutput.ReadToEnd(),
                Error = StandardError.ReadToEnd(),
                RunTime = (int)ExitTime.Subtract(StartTime).TotalMilliseconds
            };
            WaitForExit();
            return runResult;
        } 

    }
}