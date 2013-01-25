using System;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Controllers;
using System.Web.Http.Hosting;
using System.Web.Http.Routing;
using JavaProgrammingContest.DataAccess.Context;
using JavaProgrammingContest.Domain.Entities;
using JavaProgrammingContest.Process.Compiler;
using JavaProgrammingContest.Web.API;
using Moq;
using NUnit.Framework;

namespace JavaProgrammingContest.Web.Tests.Api{
    [TestFixture]
    public class BuildControllerTests{
        private BuildController _controller;

        private static ICompiler _compiler;
        private Participant _participant;
        private Mock<IDbContext> _contextMock;

        [SetUp]
        public void SetUp(){
            _contextMock = new Mock<IDbContext>();

            _compiler = new TestCompiler();
            _participant = new Participant{Email = "", Id = 12};
            _controller = new BuildController(_contextMock.Object, _compiler, _participant);
        }

        [Test]
        public void PostBuildJobReturnsCreatedStatusCode(){
            SetupControllerForTests(_controller);
            var result = _controller.Post(new BuildController.BuildJob{Code = "test"});
            Assert.AreEqual(HttpStatusCode.Created, result.StatusCode);
        }

        [Test]
        [ExpectedException(typeof (NullReferenceException))]
        public void PostNullInteadOfABuildjobReturnsNullReferenceException(){
            SetupControllerForTests(_controller);
            _controller.Post(null);
        }

        [Test]
        [ExpectedException(typeof (InvalidOperationException))]
        public void SetBuildConstructorWithoutGivenParticipantGivesInvalidOperationFromTest(){
            _controller = new BuildController(_contextMock.Object, _compiler);
        }

        private static void SetupControllerForTests(ApiController controller){
            var config = new HttpConfiguration();
            var request = new HttpRequestMessage(HttpMethod.Post, "http://localhost/api/build");
            var route = config.Routes.MapHttpRoute("DefaultApi", "api/{build}/{id}");
            var routeData = new HttpRouteData(route, new HttpRouteValueDictionary{{"controller", "build"}});

            controller.ControllerContext = new HttpControllerContext(config, routeData, request);
            controller.Request = request;
            controller.Request.Properties[HttpPropertyKeys.HttpConfigurationKey] = config;
        }
    }
}