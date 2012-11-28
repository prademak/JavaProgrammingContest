﻿using JavaProgrammingContest.Process.Runner.Model;

namespace JavaProgrammingContest.Process.Runner.Java.Helpers{
    public class JavaRunnerProcess : System.Diagnostics.Process, IRunnerProcess{
        public JavaRunnerProcess(){
            StartInfo.UseShellExecute = false;
            StartInfo.RedirectStandardOutput = true;
            StartInfo.RedirectStandardError = true;
            StartInfo.FileName = "C:\\glassfish3\\jdk7\\bin\\java"; //todo isettingsprovider using appconfig
        }

        public RunResult Run(string arguments){
            StartInfo.Arguments = arguments;

            Start();

            var runResult = new RunResult{
                Output = StandardOutput.ReadToEnd(),
                Error = StandardError.ReadToEnd(),
                RunTime = (int)ExitTime.Subtract(StartTime).TotalMilliseconds
            };

            WaitForExit();

            return runResult;
        }
    }
}