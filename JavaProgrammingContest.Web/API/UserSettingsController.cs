using System.Net;
using System.Web.Http;
using AutoMapper;
using JavaProgrammingContest.DataAccess.Context;
using JavaProgrammingContest.Domain.Entities;
using JavaProgrammingContest.Web.DTO;

namespace JavaProgrammingContest.Web.API{
    /// <summary>
    ///     Controller for interface interaction with the database object UserSettings.
    /// </summary>
    public class UserSettingsController : ApiController{
        /// <summary>
        ///     Stores the Database Context
        /// </summary>
        private readonly IDbContext _context;

        /// <summary>
        ///     Construct User Settings Object
        /// </summary>
        /// <param name="context"></param>
        public UserSettingsController(IDbContext context){
            _context = context;
        }

        /// <summary>
        ///     Get the user settings for the current user.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public UserSettingDTO Get(int id){
            var userSetting = _context.UserSettings.Find(id);
            if (userSetting == null)
                throw new HttpResponseException(HttpStatusCode.NotFound);
            return Mapper.Map<UserSetting, UserSettingDTO>(userSetting);
        }
    }
}