using System.IO;
using System.Web;
using JavaProgrammingContest.Process.Compiler.Model;

namespace JavaProgrammingContest.Process.Compiler.Java
{
    public class JavaCompiler : ICompiler
    {
        public ICompilerProcess CompilerProcess { get; set; }

        public CompilerResult CompileFromPlainText(string code)
        {
            string javaFile = CreateFilePath("HelloWorldApp");
            File.WriteAllText(javaFile, code);
            return CompilerProcess.Compile(string.Format("\"{0}\"", javaFile));
        }

        public string CreateFilePath(string appName)
        {
            string currentPath = Directory.GetCurrentDirectory();
            string test = HttpContext.Current.Server.MapPath("~/");
            string fileName = appName + ".java";
            return Path.Combine(test, fileName);
        }
    }
}