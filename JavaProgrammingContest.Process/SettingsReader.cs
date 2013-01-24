using System.Configuration;

namespace JavaProgrammingContest.Process{
    public interface ISettingsReader{
        string GetValueAsString(string key);
        object GetValueAsObject(string key);
    }

    public class SettingsReader : ISettingsReader{
        private readonly AppSettingsReader _appSettingsReader;

        public SettingsReader(){
            _appSettingsReader = new AppSettingsReader();
        }

        public string GetValueAsString(string key){
            return _appSettingsReader.GetValue(key, typeof (string)).ToString();
        }

        public object GetValueAsObject(string key){
            return _appSettingsReader.GetValue(key, typeof (object));
        }
    }
}