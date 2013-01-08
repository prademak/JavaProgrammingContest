using System.Linq;
using JavaProgrammingContest.Domain.Entities;

namespace JavaProgrammingContest.DataAccess.TestSupport{
    public class FakeAssignmentsSet : FakeDbSet<Assignment>{
        public override Assignment Find(params object[] keyValues){
            return Local.SingleOrDefault(a => a.Id == (int) keyValues[0]);
        }
    }
}