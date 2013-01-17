using System;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Controllers;
using System.Web.Http.Hosting;
using System.Web.Http.Routing;
using JavaProgrammingContest.DataAccess.Context;
using JavaProgrammingContest.DataAccess.TestSupport;
using JavaProgrammingContest.Domain.Entities;
using JavaProgrammingContest.Web.API;
using Moq;
using NUnit.Framework;
using JavaProgrammingContest.Process.Compiler;
using JavaProgrammingContest.Process.Runner;

namespace JavaProgrammingContest.Web.Tests.Api
{
    [TestFixture]
    public class RunControllerTests
    {
        private RunController _controller;

        private static ICompiler _compiler;
        private Participant _participant;
        private Mock<IDbContext> _contextMock;
        private static IRunner _runner;

        [SetUp]
        public void SetUp()
        {
            _contextMock = new Mock<IDbContext>();

            _compiler = new TestCompiler();
            _runner = new TestRunner();
            _participant = new Participant { Email = "", Id = 12 };
            _controller = new RunController(_contextMock.Object, _compiler, _runner, _participant);
        }


        [Test]
        public void RunRunJobReturnsCreatedStatusCode()
        {
            SetupControllerForTests(_controller);
            var result = _controller.Run(new RunController.RunJob { Code = "Hello user:", Id = 1});
            Assert.AreEqual(HttpStatusCode.Created, result.StatusCode);
            
        }


        [Test]
        [ExpectedException(typeof(NullReferenceException))]
        public void PostNullInteadOfARunJobReturnsNullReferenceException()
        {
            SetupControllerForTests(_controller);
            _controller.Run(null);
        }

        [Test]
        [ExpectedException(typeof(InvalidOperationException))]
        public void SetRunConstructorWithoutGivenParticipantGivesInvalidOperationFromTest()
        {
            _controller = new RunController(_contextMock.Object, _compiler, _runner);
        }

        private static void SetupControllerForTests(ApiController controller)
        {
            var config = new HttpConfiguration();
            var request = new HttpRequestMessage(HttpMethod.Post, "http://localhost/api/run");
            var route = config.Routes.MapHttpRoute("DefaultApi", "api/{controller}/{id}");
            var routeData = new HttpRouteData(route, new HttpRouteValueDictionary { { "controller", "run" } });

            controller.ControllerContext = new HttpControllerContext(config, routeData, request);
            controller.Request = request;
            controller.Request.Properties[HttpPropertyKeys.HttpConfigurationKey] = config;
        }
    }
}