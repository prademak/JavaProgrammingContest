using System.Collections.Generic;
using System.Net;
using System.Web.Http;
using JavaProgrammingContest.DataAccess;
using JavaProgrammingContest.Domain.Entities;

namespace JavaProgrammingContest.Web.API{
    public class UserSettingsController : ApiController{
        private readonly IRepository<UserSetting> _userSettingsRepository;

        public UserSettingsController(IRepository<UserSetting> userSettingsRepository){
            _userSettingsRepository = userSettingsRepository;
        }

        public IEnumerable<UserSetting> Get(){
            throw new HttpResponseException(HttpStatusCode.NotImplemented);
        }

        public UserSetting Get(int id){
            return _userSettingsRepository.GetById(id);
        }
    }
}