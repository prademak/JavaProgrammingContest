using JavaProgrammingContest.Process.Runner.Model;

namespace JavaProgrammingContest.Process.Runner{
    public interface IRunnerProcess{
        RunResult Run(string arguments);
    }
}