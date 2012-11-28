using JavaProgrammingContest.Process.Compiler.Model;

namespace JavaProgrammingContest.Process.Compiler{
    public interface ICompiler{
        CompilerResult CompileFromPlainText(string code);
    }
}