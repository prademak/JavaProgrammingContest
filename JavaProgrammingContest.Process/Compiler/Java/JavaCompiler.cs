using System.IO;
using System.Security.AccessControl;
using System.Web;
using JavaProgrammingContest.Domain.Entities;
using JavaProgrammingContest.Process.Compiler.Model;

namespace JavaProgrammingContest.Process.Compiler.Java{
    public class JavaCompiler : ICompiler{
        public ICompilerProcess CompilerProcess { get; set; }

        public CompilerResult CompileFromPlainText(Participant participant, string code){
            var javaFile = CreateFilePath(participant, "HelloWorldApp");
            File.WriteAllText(javaFile, code);
            return CompilerProcess.Compile(string.Format("\"{0}\"", javaFile));
        }

        public string CreateFilePath(Participant participant, string appName){
            //TODO currentPath will possibly not work when app is published

            var currentPath = HttpContext.Current.Server.MapPath("~/");

            var fileName = appName + ".java";
            return Path.Combine(currentPath, fileName);
        }
    }
}