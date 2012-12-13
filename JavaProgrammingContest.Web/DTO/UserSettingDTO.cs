namespace JavaProgrammingContest.Web.DTO{
    public class UserSettingDTO{
        public int Id { get; set; }
        public string Theme { get; set; }
        public bool MatchBrackets { get; set; }
        public bool AutoIndent { get; set; }
        public int TabSize { get; set; }
        public bool LineWrapping { get; set; }
        public bool IntelliSense { get; set; }
        public int ParticipantId { get; set; }
    }
}