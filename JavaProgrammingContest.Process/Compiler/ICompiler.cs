using JavaProgrammingContest.Domain.Entities;
using JavaProgrammingContest.Process.Compiler.Model;

namespace JavaProgrammingContest.Process.Compiler{
    public interface ICompiler{
        CompilerResult CompileFromPlainText(Participant participant, string code);
    }
}