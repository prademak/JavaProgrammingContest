using System;
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

        public ChromeDriver ChromeDriver { get; set; }

        [TestFixtureSetUp]
        public void TestInitialize(){
            StartIIS();
            ChromeDriver = new ChromeDriver("C:\\Program Files (x86)\\Google\\Chrome\\Application");
        }

        [TestFixtureTearDown]
        public void TestCleanup(){
            if (_iisProcess.HasExited == false)
                _iisProcess.Kill();

            ChromeDriver.Quit();
        }

        private void StartIIS(){
            var applicationPath = GetApplicationPath(_applicationName);
            var programFiles = Environment.GetFolderPath(Environment.SpecialFolder.ProgramFiles);

            _iisProcess = new System.Diagnostics.Process{
                StartInfo ={
                    FileName = programFiles + @"\IIS Express\iisexpress.exe",
                    Arguments = string.Format("/path:\"{0}\" /port:{1}", applicationPath, IISPort)
                    //RedirectStandardError = true,
                    //RedirectStandardOutput = true,
                    //UseShellExecute = false
                }
            };

            _iisProcess.Start();

            //var s1 = _iisProcess.StandardOutput.ReadToEnd();
            //var s2 = _iisProcess.StandardError.ReadToEnd();
            //Debug.WriteLine(s1);
            //Debug.WriteLine(s2);

            //_iisProcess.WaitForExit();
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