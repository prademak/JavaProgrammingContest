using JavaProgrammingContest.DataAccess.Context;
using JavaProgrammingContest.Process.Compiler;
using JavaProgrammingContest.Process.Compiler.Java;
using JavaProgrammingContest.Process.Runner;
using JavaProgrammingContest.Process.Runner.Java;
using JavaProgrammingContest.Web.API;
using Moq;
using NUnit.Framework;

namespace JavaProgrammingContest.Web.Tests.Api{
    [TestFixture]
    public class ScoresControllerTests{
        private static ScoresController _controller;
        private static Mock<IDbContext> _contextMock;
        private static IRunner _runner;
        private static ICompiler _compiler;

        [SetUp]
        public void SetUp(){
            _contextMock = new Mock<IDbContext>();
            _compiler = new JavaCompiler();
            _runner = new JavaRunner();
            _controller = new ScoresController(_contextMock.Object, _compiler, _runner);
        }
    }
}