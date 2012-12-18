using JavaProgrammingContest.Domain.Entities;
using JavaProgrammingContest.Process.Runner.Model;

namespace JavaProgrammingContest.Process.Runner{
    public interface IRunner{
        RunResult Run(Participant participant);
        RunResult RunAndCheckInput(Participant participant);
    }
}