using JavaProgrammingContest.DataAccess.Context;
using JavaProgrammingContest.Process.Compiler;
using JavaProgrammingContest.Process.Runner;
using JavaProgrammingContest.Web.API;
using JavaProgrammingContest.Web.App_Start;
using Moq;
using NUnit.Framework;

namespace JavaProgrammingContest.Web.Tests.Api{
    [TestFixture]
    public class BuildControllerTests {
        private BuildController _controller;
        private Mock<IDbContext> _contextMock;
        private static IRunner _runner;
        private static ICompiler _compiler;
        private BuildController.BuildJob _buildjob;
        [SetUp]
        public void SetUp()
        {
            _contextMock = new Mock<IDbContext>(); 
            MapperConfig.Configure();
            _compiler = new TestCompiler();
            _controller = new BuildController(_contextMock.Object, _compiler);
            _buildjob = new BuildController.BuildJob{ Code="test" };

        }

        [Test]
        public void GetUserSettingsWithValidId()
        {
            Assert.AreEqual("Hello World!", _controller.Post(_buildjob));
          
        }
    
    }
}