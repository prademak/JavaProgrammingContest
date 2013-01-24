using JavaProgrammingContest.DataAccess.Context;
using JavaProgrammingContest.Web.App_Start;
using JavaProgrammingContest.Web.Controllers;
using Moq;
using NUnit.Framework;
using System.Web.Mvc;

namespace JavaProgrammingContest.Web.Tests.Controllers{
    [TestFixture]
    public class EditorControllerTests {
        private Mock<IDbContext> _contextMock;
        private EditorController _controller; 
        private Mock<IWebSecurity> _WebSecurity { get; set; }

        [SetUp]
        public void Init()
        {
            _contextMock = new Mock<IDbContext>(); 
            _controller = new EditorController();
            MapperConfig.Configure();
        }
        [Test]
        public void Editor_showsView()
        {
         
            var result = _controller.Index() as ViewResult;

            Assert.IsNotNull(result);
        }
    
    }
}