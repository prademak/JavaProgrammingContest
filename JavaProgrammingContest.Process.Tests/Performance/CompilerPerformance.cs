using System.Threading;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;

namespace JavaProgrammingContest.Process.Tests.Performance{
    [TestFixture]
    public class Simulation : SeleniumTest{
        public Simulation()
            : base("JavaProgrammingContest.Web") {}

        [Test]
<<<<<<< HEAD
        public void EndToEndTest(){
            ChromeDriver.Navigate().GoToUrl(GetAbsoluteUrl("/"));
            ChromeDriver.FindElementByLinkText("Log in").Click();
            ChromeDriver.FindElementById("UserName").SendKeys("admin");
            ChromeDriver.FindElementById("Password").SendKeys("!nf0suPP0r7");
            ChromeDriver.FindElementsByTagName("form")[1].Submit();
            ChromeDriver.FindElementByLinkText("Start Programmeren").Click();
            ChromeDriver.FindElementByLinkText("Start de tijd!").Click();
            ChromeDriver.FindElementByLinkText("Editor").Click();
            ChromeDriver.FindElementByLinkText("Submit").Click();
            ChromeDriver.FindElementById("assignmentSubmitModal").FindElement(By.LinkText("Submit")).Click();

            Thread.Sleep(5000);
=======
        public void Test(){
            //for (int i = 0; i < 2; i++){
            //    var t = new Thread(OpenBrowser);
            //    t.Start();
            //    t.Join();    
            //}

//            TestInitialize();

            //ChromeDriver.Navigate().GoToUrl(GetAbsoluteUrl("/"));
           // Thread.Sleep(50000);
>>>>>>> fe46ed6a49515e0e878f8cc100f135dc323e80a3
        }

        private void OpenBrowser(object obj){
            var driver = new ChromeDriver("C:\\Program Files (x86)\\Google\\Chrome\\Application");
            driver.Navigate().GoToUrl(GetAbsoluteUrl("/home/index"));
            Assert.AreEqual("Title", driver.Title);
        }
    }
}