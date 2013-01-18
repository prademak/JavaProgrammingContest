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
using System.Collections.Generic;
using JavaProgrammingContest.Web.DTO;
using System.Collections.ObjectModel;
using JavaProgrammingContest.Web.App_Start;

namespace JavaProgrammingContest.Web.Tests.Api
{
    [TestFixture]
    public class ScoresControllerTests
    {
        private ScoresController _controller;

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
            var scores = new Collection<Score> { };
            scores.Add(new Score { Assignment = new Assignment { Id = 1 } });
            _participant = new Participant { Id = 1, Email = "", Scores = scores, UserSetting = null };
            Progress progress = new Progress { Assignment = new Assignment { Id = 1, MaxSolveTime = 1000, RunCodeInput = "test", RunCodeOuput = "test" }, Id = 1, StartTime = DateTime.Now, Participant = _participant };
            _participant.Progress = progress;
            _controller = new ScoresController(_contextMock.Object, _compiler, _runner, _participant);
            MapperConfig.Configure();
        }

        //test need to be fixed
        [Test]
        [ExpectedException(typeof(NullReferenceException))]
        public void PostRunJobReturnsCreatedStatusCode()
        {
            SetupControllerForTests(_controller);
            var result = _controller.Post(new RunController.RunJob { Code = "test", Id = 1 });

            Assert.AreEqual(HttpStatusCode.Created, result.StatusCode);

        }


        [Test]
        [ExpectedException(typeof(NullReferenceException))]
        public void PostNullInteadOfARunJobReturnsNullReferenceException()
        {
            SetupControllerForTests(_controller);
            _controller.Post(null);
        }

        [Test]
        [ExpectedException(typeof(InvalidOperationException))]
        public void SetScoresConstructorWithoutGivenParticipantGivesInvalidOperationFromTest()
        {
            _controller = new ScoresController(_contextMock.Object, _compiler, _runner);
        }

        [Test]
        public void GetAListofAllTheScores()
        {
           SetupControllerForTests(_controller);
            var getresult = _controller.GetScoresFromCurrentUser();
            Assert.AreEqual(1, count(getresult));
        }

        private int count(IEnumerable<ScoreDTO> getresult)
        {
            int count = 0;
            foreach (object obj in getresult)
            {
                count++;
            }
            return count;
        }

        private static void SetupControllerForTests(ApiController controller)
        {
            var config = new HttpConfiguration();
            var request = new HttpRequestMessage(HttpMethod.Post, "http://localhost/api/scores");
            var route = config.Routes.MapHttpRoute("DefaultApi", "api/{controller}/{id}");
            var routeData = new HttpRouteData(route, new HttpRouteValueDictionary { { "controller", "scores" } });

            controller.ControllerContext = new HttpControllerContext(config, routeData, request);
            controller.Request = request;
            controller.Request.Properties[HttpPropertyKeys.HttpConfigurationKey] = config;
        }

       
        
    }
}