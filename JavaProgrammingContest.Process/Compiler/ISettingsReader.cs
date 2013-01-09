namespace JavaProgrammingContest.Process.Compiler{
    public interface ISettingsReader{
        string GetValueAsString(string key);
        object GetValueAsObject(string key);
    }
}