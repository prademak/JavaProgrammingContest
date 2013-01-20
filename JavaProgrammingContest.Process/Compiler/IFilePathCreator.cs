using System;
using System.IO;
using System.Web;

namespace JavaProgrammingContest.Process.Compiler.Java{
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

    public class FilePathCreator : IFilePathCreator{
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