using JavaProgrammingContest.Process.Compiler.Model;

namespace JavaProgrammingContest.Process.Compiler{
    public interface ICompilerProcess{
        CompilerResult Compile(string arguments);
    }
}