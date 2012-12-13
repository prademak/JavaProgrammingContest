using System.Net;
using System.Web.Http;
using AutoMapper;
using JavaProgrammingContest.DataAccess.Context;
using JavaProgrammingContest.Domain.Entities;
using JavaProgrammingContest.Web.DTO;

namespace JavaProgrammingContest.Web.API{
    public class UserSettingsController : ApiController{
        private readonly IDbContext _context;

        public UserSettingsController(IDbContext context){
            _context = context;
        }

        public UserSettingDTO Get(int id){
            var userSetting = _context.UserSettings.Find(id);
            if (userSetting == null)
                throw new HttpResponseException(HttpStatusCode.NotFound);
            return Mapper.Map<UserSetting, UserSettingDTO>(userSetting);
        }
    }
}