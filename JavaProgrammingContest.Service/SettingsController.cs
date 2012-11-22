using System.Collections.Generic;
using System.Web.Http;
using JavaProgrammingContest.DataAccess;
using JavaProgrammingContest.Domain.Entities;

namespace JavaProgrammingContest.Service{
    public class UserSettingsController : ApiController{
        private readonly IRepository<UserSetting> _userSettingsRepository;

        public UserSettingsController(IRepository<UserSetting> userSettingsRepository){
            _userSettingsRepository = userSettingsRepository;
        }

        public IEnumerable<UserSetting> Get(){
            return _userSettingsRepository.GetAll();
        }

        public UserSetting Get(int id){
            return _userSettingsRepository.GetById(id);
        }
    }
}