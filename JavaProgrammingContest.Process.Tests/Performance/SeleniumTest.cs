using System;
using System.Collections.Generic;
using System.IO;
using NUnit.Framework;
using OpenQA.Selenium.Chrome;

namespace JavaProgrammingContest.Process.Tests.Performance{
    [TestFixture]
    public abstract class SeleniumTest{
        private const int IISPort = 2020;
        private readonly string _applicationName;
        private System.Diagnostics.Process _iisProcess;

        protected SeleniumTest(string applicationName){
            _applicationName = applicationName;
        }

        //public List<ChromeDriver> ChromeDrivers { get; set; }

        [SetUp]
        public void TestInitialize(){
            StartIIS();

            //for (var i = 0; i < _numDrivers; i++)
            //    ChromeDrivers.Add(new ChromeDriver("C:\\Program Files (x86)\\Google\\Chrome\\Application"));
        }

        [TestFixtureTearDown]
        public void TestCleanup(){
            if (_iisProcess.HasExited == false)
                _iisProcess.Kill();

            //foreach (var chromeDriver in ChromeDrivers)
            //    chromeDriver.Quit();
        }

        private void StartIIS(){
            var applicationPath = GetApplicationPath(_applicationName);
            var programFiles = Environment.GetFolderPath(Environment.SpecialFolder.ProgramFiles);

            _iisProcess = new System.Diagnostics.Process{
                StartInfo ={
                    FileName = programFiles + @"\IIS Express\iisexpress.exe",
                    Arguments = string.Format("/path:\"{0}\" /port:{1}", applicationPath, IISPort)
                }
            };

            _iisProcess.Start();
        }

        protected virtual string GetApplicationPath(string applicationName){
            var solutionFolder = Path.GetDirectoryName(Path.GetDirectoryName(Path.GetDirectoryName(AppDomain.CurrentDomain.BaseDirectory)));
            return Path.Combine(solutionFolder, applicationName);
        }

        public string GetAbsoluteUrl(string relativeUrl){
            if (!relativeUrl.StartsWith("/"))
                relativeUrl = "/" + relativeUrl;
            return String.Format("http://localhost:{0}{1}", IISPort, relativeUrl);
        }
    }
}