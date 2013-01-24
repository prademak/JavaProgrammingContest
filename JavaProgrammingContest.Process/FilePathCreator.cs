using System.Globalization;
using System.IO;
using System.Web;

namespace JavaProgrammingContest.Process{
    public interface IWorkingFolder{
        string GetWorkingFolder(int participantId);
    }

    public class WorkingFolder : IWorkingFolder{
        public string GetWorkingFolder(int participantId){
            var workingFolder = HttpContext.Current.Server.MapPath("~/");
            workingFolder = Path.Combine(workingFolder, "temp");
            return Path.Combine(workingFolder, participantId.ToString(CultureInfo.InvariantCulture));
        }
    }
}