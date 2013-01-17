using System;
using System.IO;
using System.Web;
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

    public interface IFilePathCreator{
        string CreateFilePath(int participantId, string appName);
    }

    public class TestFilePathCreator : IFilePathCreator{
        public string CreateFilePath(int participantId, string appName){
            var currentPath = AppDomain.CurrentDomain.BaseDirectory;
            currentPath = Path.Combine(currentPath, "temp");
            currentPath = Path.Combine(currentPath, participantId.ToString());
            Directory.CreateDirectory(currentPath);
            var fileName = appName + ".java";
            return Path.Combine(currentPath, fileName);
        }
    }

    public class FilePathCreator: IFilePathCreator{
        public string CreateFilePath(int participantId, string appName){
            var currentPath = HttpContext.Current.Server.MapPath("~/");
            currentPath = Path.Combine(currentPath, "temp");
            currentPath = Path.Combine(currentPath, participantId.ToString());
            Directory.CreateDirectory(currentPath);
            var fileName = appName + ".java";
            return Path.Combine(currentPath, fileName);
        }
    }
}