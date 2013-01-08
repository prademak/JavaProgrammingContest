using System.Linq;
using JavaProgrammingContest.Domain.Entities;

namespace JavaProgrammingContest.DataAccess.TestSupport{
    public class FakeParticipantSet : FakeDbSet<Participant>{
        public override Participant Find(params object[] keyValues){
            return Local.SingleOrDefault(a => a.Id == (int) keyValues[0]);
        }
    }
}