using System.IO;
using System.Web;
using JavaProgrammingContest.Domain.Entities;
using JavaProgrammingContest.Process.Compiler.Model;

namespace JavaProgrammingContest.Process.Compiler.Java{
    public class JavaCompiler : ICompiler{
        public ICompilerProcess CompilerProcess { get; set; }

        public CompilerResult CompileFromPlainText(Participant participant, string code){
            var javaFile = CreateFilePath(participant.Id, "Solution");
            File.WriteAllText(javaFile, code);
            return CompilerProcess.Compile(string.Format("\"{0}\"", javaFile));
        }

        public string CreateFilePath(int id, string appName){
            //TODO currentPath will possibly not work when app is published

            var currentPath = HttpContext.Current.Server.MapPath("~/");
            currentPath = Path.Combine(currentPath, "temp");
            currentPath = Path.Combine(currentPath, id.ToString());
            Directory.CreateDirectory(currentPath);
            var fileName = appName + ".java";
            return Path.Combine(currentPath, fileName);
        }
    }
}