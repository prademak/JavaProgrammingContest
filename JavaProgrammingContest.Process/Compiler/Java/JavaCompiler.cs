using System.IO;
using JavaProgrammingContest.Domain.Entities;
using JavaProgrammingContest.Process.Compiler.Model;

namespace JavaProgrammingContest.Process.Compiler.Java{
    public class JavaCompiler : ICompiler{
        public ICompilerProcess CompilerProcess { get; set; }
        public IFilePathCreator FilePathCreator { get; set; }

        public CompilerResult CompileFromPlainText(Participant participant, string code){
            var javaFile = FilePathCreator.CreateFilePath(participant.Id, "Solution");
            File.WriteAllText(javaFile, code);
            return CompilerProcess.Compile(string.Format("\"{0}\"", javaFile));
        }
    }
}