using JavaProgrammingContest.DataAccess.Context;
using JavaProgrammingContest.Web.App_Start;
using JavaProgrammingContest.Web.Controllers;
using Moq;
using NUnit.Framework;
using System.Web.Mvc;

namespace JavaProgrammingContest.Web.Tests.Controllers
{
    [TestFixture]
    public class HomeControllerTests
    {
        private Mock<IDbContext> _contextMock;
        private HomeController _controller;
        private Mock<IWebSecurity> _WebSecurity { get; set; }

        [SetUp]
        public void Init()
        {
            _contextMock = new Mock<IDbContext>();
            _controller = new HomeController();
            MapperConfig.Configure();
        }
        [Test]
        public void Home_showsIndexView()
        {

            var result = _controller.Index() as ViewResult;

            Assert.IsNotNull(result);
        }

        [Test]
        public void Home_showsAboutView()
        {

            var result = _controller.About() as ViewResult;

            Assert.IsNotNull(result);
        }

        [Test]
        public void Home_showsContactView()
        {

            var result = _controller.Contact() as ViewResult;

            Assert.IsNotNull(result);
        }

    }
}