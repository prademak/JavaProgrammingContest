using System.Linq;
using JavaProgrammingContest.Domain.Entities;

namespace JavaProgrammingContest.DataAccess.TestSupport{
    public class FakeUserSettingsSet : FakeDbSet<UserSetting>{
        public override UserSetting Find(params object[] keyValues){
            return Local.SingleOrDefault(a => a.Id == (int) keyValues[0]);
        }
    }
}