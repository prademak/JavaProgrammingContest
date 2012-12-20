using AutoMapper;
using JavaProgrammingContest.Domain.Entities;
using JavaProgrammingContest.Web.DTO;

namespace JavaProgrammingContest.Web.App_Start{
    /// <summary>
    ///     Maps Database Objects
    /// </summary>
    public static class MapperConfig{
        /// <summary>
        ///     Maps Database Objects
        /// </summary>
        public static void Configure(){
            Mapper.CreateMap<Assignment, AssignmentDTO>();
            Mapper.CreateMap<Contest, ContestDTO>();
            Mapper.CreateMap<Participant, ParticipantDTO>();
            Mapper.CreateMap<Progress, ProgressDTO>();
            Mapper.CreateMap<Score, ScoreDTO>();
            Mapper.CreateMap<UserSetting, UserSettingDTO>();
        }
    }
}