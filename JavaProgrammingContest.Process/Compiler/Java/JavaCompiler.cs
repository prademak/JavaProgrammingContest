using System.IO;
using JavaProgrammingContest.Domain.Entities;
using JavaProgrammingContest.Process.Compiler.Model;

namespace JavaProgrammingContest.Process.Compiler.Java{
    public class JavaCompiler : ICompiler{
        public ICompilerProcess CompilerProcess { get; set; }
        public IWorkingFolder WorkingFolder { get; set; }

        public CompilerResult CompileFromPlainText(Participant participant, string code){
            var workingFolder = WorkingFolder.GetWorkingFolder(participant.Id);

            const string fileName = "Solution" + ".java";
            Directory.CreateDirectory(workingFolder);
            var filePath = Path.Combine(workingFolder, fileName);

            File.WriteAllText(filePath, code);
            return CompilerProcess.Compile(string.Format("\"{0}\"", filePath));
        }
    }
}