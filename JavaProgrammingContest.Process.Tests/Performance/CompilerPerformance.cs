using System.Threading;
using NUnit.Framework;
using OpenQA.Selenium.Chrome;

namespace JavaProgrammingContest.Process.Tests.Performance{
    [TestFixture]
    public class Simulation : SeleniumTest{
        public Simulation()
            : base("JavaProgrammingContest"){
        }

        [Test]
        public void Test(){
            //for (int i = 0; i < 2; i++){
            //    var t = new Thread(OpenBrowser);
            //    t.Start();
            //    t.Join();    
            //}

            TestInitialize();

            //ChromeDriver.Navigate().GoToUrl(GetAbsoluteUrl("/"));
            Thread.Sleep(50000);
        }

        private void OpenBrowser(object obj){
            var driver = new ChromeDriver("C:\\Program Files (x86)\\Google\\Chrome\\Application");
            driver.Navigate().GoToUrl(GetAbsoluteUrl("/home/index"));
            Assert.AreEqual("Title", driver.Title);
        }
    }
}