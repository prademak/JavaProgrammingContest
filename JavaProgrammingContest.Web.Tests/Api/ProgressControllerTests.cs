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
using JavaProgrammingContest.Web.App_Start;
using System.Collections.Generic;
using JavaProgrammingContest.Web.DTO;
using System.Collections;
using System.Collections.ObjectModel;

namespace JavaProgrammingContest.Web.Tests.Api
{
    [TestFixture]
    public class ProgressControllerTests
    {
        private ProgressController _controller;
        private Mock<IDbContext> _contextMock;
        private Participant _participant;

        [SetUp]
        public void SetUp()
        {
            _contextMock = new Mock<IDbContext>();
            _participant = new Participant { Id = 1, Email = "", Scores = null, UserSetting = null };
            Progress progress = new Progress { Assignment = new Assignment { Id = 1, MaxSolveTime = 1000 }, Id = 1, StartTime = DateTime.Now, Participant = _participant };
            _participant.Progress = progress;
            _controller = new ProgressController(_contextMock.Object, _participant);
            MapperConfig.Configure();

        }

        [Test]
        [ExpectedException(typeof(InvalidOperationException))]
        public void SetConstructorWithoutGivenParticipantGivesInvalidOperationFromTest()
        {
            _controller = new ProgressController(_contextMock.Object);

            Assert.IsTrue(true);
        }


        [Test]
      
        public void GetProgressThrowsHttpResponseExceptionWhenContextReturnsNull()
        {
            SetupControllerForTests(_controller);
            var result = _controller.Get(11);
            Assert.AreEqual(HttpStatusCode.NotFound, result.StatusCode);
        }

        [Test]
        public void GetProgressWithId()
        {
            SetupControllerForTests(_controller);

            var result = _controller.Get(1);
            Assert.AreEqual(HttpStatusCode.OK, result.StatusCode);
        }

        [Test]
        public void PutProgressReturnsCreatedStatusCode()
        {
            _contextMock = new Mock<IDbContext>();
            var list = CreateSampleData(3);
            _contextMock.Setup(m => m.Assignments).Returns(list);
            _contextMock.Setup(m => m.Progresses);
            _participant = new Participant { Id = 1, Email = "", Scores = null, UserSetting = null, Progress = null };
            _controller = new ProgressController(_contextMock.Object, _participant);
            SetupControllerForTests(_controller);

            Progress progress = new Progress { Assignment = list.Find(1), Id = 2, Participant = _participant, StartTime = DateTime.Now };
            var result = _controller.Put(1, progress);
            Assert.AreEqual(HttpStatusCode.InternalServerError, result.StatusCode);
        }

        [Test]
        public void PutProgressWhenAlreadyDoingAssignmentIsForbidden()
        {
            SetupControllerForTests(_controller);
            Assignment assignment = new Assignment { Id = 2, MaxSolveTime = 1000 };
                
            Progress progress = new Progress {Assignment = assignment , Id = 2, Participant = _participant };
            var result = _controller.Put(2, progress);
            Assert.AreEqual(HttpStatusCode.Forbidden, result.StatusCode);
        }

        [Test]
        public void DeleteProgress()
        {
            SetupControllerForTests(_controller);
            var request = _controller.Delete();
         
        }

        [Test]
        public void DeleteProgressWhenNoProgressIsStarted()
        {
            _contextMock = new Mock<IDbContext>();
       
            _participant = new Participant { Id = 1, Email = "", Scores = null, UserSetting = null, Progress = null };
            _controller = new ProgressController(_contextMock.Object, _participant);
            SetupControllerForTests(_controller);

            var request = _controller.Delete();
        }



        private static FakeAssignmentsSet CreateSampleData(int nrOfRecords)
        {
            var sampleData = new FakeAssignmentsSet();

            for (var i = 1; i <= nrOfRecords; i++)
                sampleData.Add(new Assignment
                {
                    Id = i,
                    Title = "title " + i,
                });

            return sampleData;
        }

        private static void SetupControllerForTests(ApiController controller)
        {
            var config = new HttpConfiguration();
            var request = new HttpRequestMessage(HttpMethod.Post, "http://localhost/api/progress");
            var route = config.Routes.MapHttpRoute("DefaultApi", "api/{controller}/{id}");
            var routeData = new HttpRouteData(route, new HttpRouteValueDictionary { { "controller", "progress" } });

            controller.ControllerContext = new HttpControllerContext(config, routeData, request);
            controller.Request = request;
            controller.Request.Properties[HttpPropertyKeys.HttpConfigurationKey] = config;
        }



    }
}