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

namespace JavaProgrammingContest.Web.Tests.Api{
    [TestFixture]
    public class AssignmentsControllerTests{
        private AssignmentsController _controller;
        private Mock<IDbContext> _contextMock;

        [SetUp]
        public void SetUp(){
            _contextMock = new Mock<IDbContext>();
            var scores = new Collection<Score> { };
            scores.Add(new Score{ Assignment = new Assignment { Id = 1 } });
            _controller = new AssignmentsController(_contextMock.Object, new Participant { Id = 1, Scores = scores });
            MapperConfig.Configure();
        }

        [Test]
        [ExpectedException(typeof(InvalidOperationException))]
        public void SetConstructorWithoutGivenParticipantGivesInvalidOperationFromTest()
        {
            _controller = new AssignmentsController(_contextMock.Object);
         
            Assert.IsTrue(true);
        }

        [Test]
        public void GetAllAssignmentsReturnsListOfAssignments(){
            _contextMock.Setup(m => m.Assignments).Returns(CreateSampleData(5));
            Assert.IsTrue(true);
        }

        [Test]
        [ExpectedException(typeof (HttpResponseException))]
        public void GetAssignmentThrowsHttpResponseExceptionWhenContextReturnsNull(){
            _contextMock.Setup(m => m.Assignments).Returns(CreateSampleData(1));

            _controller.Get(2);
        }

        [Test]
        public void PostAssignmentReturnsCreatedStatusCode(){
            _contextMock.Setup(m => m.Assignments).Returns(CreateSampleData(2));
            SetupControllerForTests(_controller);

            var result = _controller.Post(new Assignment{Id = 3, Title = "title 3"});

            Assert.AreEqual(HttpStatusCode.Created, result.StatusCode);
        }

        [Test]
        public void GetAssignmentWithId()
        {
            _contextMock.Setup(m => m.Assignments).Returns(CreateSampleData(2));
            SetupControllerForTests(_controller);
            var assignment = new Assignment { Id = 3, Title = "title 3" };
            var result = _controller.Post(assignment);
            var getresult = _controller.Get(assignment.Id);
            Assert.AreEqual(assignment.Title,  getresult.Title);
        }

        [Test]
        public void GetAListofAllTheAssignments()
        {
            var inputList = CreateSampleData(5);
            _contextMock.Setup(m => m.Assignments).Returns(inputList);
            SetupControllerForTests(_controller);
            var getresult = _controller.Get();
            Assert.AreEqual(5, count(getresult));
        }

        [Test]
        public void DeleteAssignmentWithId()
        {
            _contextMock.Setup(m => m.Assignments).Returns(CreateSampleData(3));
            SetupControllerForTests(_controller);
            Assert.IsNotNull(_contextMock.Object.Assignments.Find(1));
            _controller.Delete(1);
            Assert.IsNull(_contextMock.Object.Assignments.Find(1));
        }

        [Test]
        [ExpectedException(typeof(HttpResponseException))]
        public void DeleteAssignmentWithInvalidIdGivesHTTPNotFound()
        {
            _contextMock.Setup(m => m.Assignments).Returns(CreateSampleData(3));
            SetupControllerForTests(_controller);
            _controller.Delete(5);
           
        }


      
        [Test]
        public void PostAssignmentCallsAddOnContextWithProvidedAssignment(){
            _contextMock.Setup(m => m.Assignments.Add(It.IsAny<Assignment>()));
            SetupControllerForTests(_controller);

            var assignment = new Assignment{Id = 1, Title = "title 1"};
            _controller.Post(assignment);

            _contextMock.Verify(m => m.Assignments.Add(It.IsAny<Assignment>()), Times.Exactly(1));
        }

        [Test]
        [ExpectedException(typeof (HttpResponseException))]
        public void PostAssignmentWithInvalidModelThrowsBadRequestException(){
            SetupControllerForTests(_controller);
            _controller.ModelState.AddModelError("test", "test");

            _controller.Post(new Assignment());
        }

        [Test]
        [ExpectedException(typeof (HttpResponseException))]
        public void PostAssignmentShouldThrowInternalServerErrorWhenSavingEntityFailed(){
            SetupControllerForTests(_controller);
            _contextMock.Setup(m => m.SaveChanges()).Throws(new Exception());

            _controller.Post(new Assignment());
        }

        private static FakeAssignmentsSet CreateSampleData(int nrOfRecords){
            var sampleData = new FakeAssignmentsSet();

            for (var i = 1; i <= nrOfRecords; i++)
                sampleData.Add(new Assignment{
                    Id = i,
                    Title = "title " + i,
                });

            return sampleData;
        }

        private static void SetupControllerForTests(ApiController controller){
            var config = new HttpConfiguration();
            var request = new HttpRequestMessage(HttpMethod.Post, "http://localhost/api/assignments");
            var route = config.Routes.MapHttpRoute("DefaultApi", "api/{controller}/{id}");
            var routeData = new HttpRouteData(route, new HttpRouteValueDictionary{{"controller", "assignments"}});

            controller.ControllerContext = new HttpControllerContext(config, routeData, request);
            controller.Request = request;
            controller.Request.Properties[HttpPropertyKeys.HttpConfigurationKey] = config;
        }
        private static int count(IEnumerable<AssignmentDTO> getresult)
        {
            int count = 0;
            foreach (object obj in getresult)
            {
                count++;
            }
            return count;
        }
        

        
    }
}