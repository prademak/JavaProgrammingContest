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
            var participant = _context.Participants.Find(id);
            if (participant == null)
                throw new HttpResponseException(HttpStatusCode.NotFound);

            var userSetting = participant.UserSetting;
            if (userSetting == null){
                participant.UserSetting = new UserSetting()
                    {
                        AutoIndent = true,
                        IntelliSense = true,
                        LineWrapping = true,
                        MatchBrackets = true,
                        TabSize = 6,
                        Theme = "eclipse",
                        Participant = participant
                    };
                userSetting = participant.UserSetting;
            }

            return Mapper.Map<UserSetting, UserSettingDTO>(userSetting);
        }
    }
}