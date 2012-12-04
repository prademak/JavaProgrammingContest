using System;
using System.IO;
using JavaProgrammingContest.Process.Compiler.Model;

namespace JavaProgrammingContest.Process.Compiler.Java{
    public class JavaCompiler : ICompiler{
        public ICompilerProcess CompilerProcess { get; set; }

        public CompilerResult CompileFromPlainText(string code){
            var javaFile = CreateFilePath("HelloWorldApp");
            File.WriteAllText(javaFile, code);
            return CompilerProcess.Compile(string.Format("-verbose \"{0}\"", javaFile));
        }

        public string CreateFilePath(string appName){
            var currentPath = Directory.GetCurrentDirectory();
            var fileName = appName + ".java";
            return Path.Combine(currentPath, fileName);
        }
    }   


    public class CodeStoragePathHelper{
        public string GetCodeStoragePath(ProgrammingLanguage programmingLanguage){
            var current = Directory.GetCurrentDirectory();
            var sub = "code/";
            throw new NotImplementedException();
        }
    }

    public enum ProgrammingLanguage{
        Java
    }
}