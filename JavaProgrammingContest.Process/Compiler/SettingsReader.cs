using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JavaProgrammingContest.Process.Compiler.Java.Helpers;

namespace JavaProgrammingContest.Process.Compiler
{

    public class SettingsReader : ISettingsReader
    {
        private readonly AppSettingsReader _appSettingsReader;

        public SettingsReader()
        {
            _appSettingsReader = new AppSettingsReader();
        }

        public string GetValueAsString(string key)
        {
            return _appSettingsReader.GetValue(key, typeof(string)).ToString();
        }

        public object GetValueAsObject(string key)
        {
            return _appSettingsReader.GetValue(key, typeof(object));
        }
    }
}
