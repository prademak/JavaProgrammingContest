using System.IO;
using System.Web;
using JavaProgrammingContest.Process.Runner.Model;

namespace JavaProgrammingContest.Process.Runner.Java{
    //todo refactor arguments, something like IJavaRunArgumentsProvider
    public class JavaRunner : IRunner{
        public IRunnerProcess RunnerProcess { get; set; }

        public RunResult Run(){
            var argument =  "-cp "+ CreateFilePath() + " HelloWorldApp";
            return RunnerProcess.Run(argument);
        }

        public string CreateFilePath(){
            var path = HttpContext.Current.Server.MapPath("~/");
            path = path.Remove(path.LastIndexOf('\\'));
            return string.Format("\"{0}\"", path );
        }
    }
}