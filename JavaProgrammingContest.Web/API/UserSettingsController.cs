using System.Collections.Generic;
using System.Net;
using System.Web.Http;
using JavaProgrammingContest.DataAccess.Context;
using JavaProgrammingContest.Domain.Entities;

namespace JavaProgrammingContest.Web.API{
    public class UserSettingsController : ApiController{
        private readonly IDbContext _context;

        public UserSettingsController(IDbContext context){
            _context = context;
        }

        public IEnumerable<UserSetting> Get(){
            throw new HttpResponseException(HttpStatusCode.NotImplemented);
        }

        public UserSetting Get(int id){
            var userSetting = _context.UserSettings.Find(id);
            if (userSetting == null)
                throw new HttpResponseException(HttpStatusCode.NotFound);
            return userSetting;
        }
    }
}