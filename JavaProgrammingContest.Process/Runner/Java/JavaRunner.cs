using System.IO;
using System.Web;
using JavaProgrammingContest.Domain.Entities;
using JavaProgrammingContest.Process.Runner.Model;

namespace JavaProgrammingContest.Process.Runner.Java{
    //todo refactor arguments, something like IJavaRunArgumentsProvider
    public class JavaRunner : IRunner{
        public IRunnerProcess RunnerProcess { get; set; }

        public RunResult Run(Participant participant){
            var argument = "-cp " + CreateFilePath(participant.Id) + " Solution";
            return RunnerProcess.Run(argument);
        }

        public RunResult RunAndCheckInput(Participant participant)
        {
            var argument = "-cp " + CreateFilePath(participant.Id) + " Solution";
            return RunnerProcess.Run(argument, participant.Progress.Assignment.RunCodeInput);
        }

        public string CreateFilePath(int id){
            var path = HttpContext.Current.Server.MapPath("~/");

            path = Path.Combine(path, "temp");
            path = Path.Combine(path, id.ToString());
          //  path = path.Remove(path.LastIndexOf('\\'));
            return string.Format("\"{0}\"", path );
        }
    }
}