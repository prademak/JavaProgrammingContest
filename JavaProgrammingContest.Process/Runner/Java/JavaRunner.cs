using JavaProgrammingContest.Domain.Entities;
using JavaProgrammingContest.Process.Runner.Model;

namespace JavaProgrammingContest.Process.Runner.Java{
    public class JavaRunner : IRunner{
        public IRunnerProcess RunnerProcess { get; set; }
        public IWorkingFolder WorkingFolder { get; set; }

        public RunResult Run(Participant participant){
            var argument = CreateRunArgument(participant);
            return RunnerProcess.Run(argument);
        }

        public RunResult RunAndCheckInput(Participant participant){
            var argument = CreateRunArgument(participant);
            return RunnerProcess.Run(argument, participant.Progress.Assignment.RunCodeInput);
        }

        private string GetFilePath(int participantId){
            var workingFolder = WorkingFolder.GetWorkingFolder(participantId);
            return string.Format("\"{0}\"", workingFolder);
        }

        private string CreateRunArgument(Participant participant){
            return "-cp " + GetFilePath(participant.Id) + " Solution";
        }
    }
}