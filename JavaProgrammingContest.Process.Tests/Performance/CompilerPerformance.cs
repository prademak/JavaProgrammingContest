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
        }

        private void OpenBrowser(object obj){
            var driver = new ChromeDriver("C:\\Program Files (x86)\\Google\\Chrome\\Application");
            driver.Navigate().GoToUrl(GetAbsoluteUrl("/home/index"));
            Assert.AreEqual("Title", driver.Title);
        }
    }
}
