using JavaProgrammingContest.DataAccess.Context;
using JavaProgrammingContest.DataAccess.TestSupport;
using JavaProgrammingContest.Domain.Entities;
using JavaProgrammingContest.Web.App_Start;
using JavaProgrammingContest.Web.Controllers;
using JavaProgrammingContest.Web.DTO;
using JavaProgrammingContest.Web.Models;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using System.Web.Http.Controllers;
using System.Web.Http.Hosting;
using System.Web.Http.Routing;
using System.Web.Mvc;
using System.Web.Routing;


namespace JavaProgrammingContest.Web.Tests.Controllers
{
    [TestFixture]
    class AssignmentControllerTests
    {
        private Mock<IDbContext> _contextMock;
        private AssignmentController _controller;
        private LogonModel _logmod;
        private Mock<IWebSecurity> _WebSecurity { get; set; }

        [SetUp]
        public void Init()
        {
            _contextMock = new Mock<IDbContext>();
            _WebSecurity = new Mock<IWebSecurity>();
            _logmod = new LogonModel();
            _controller = new AssignmentController(_contextMock.Object);
            MapperConfig.Configure();
        }
        [Test]
        public void Assignment_getListOfAllAssignments()
        {
            _contextMock.Setup(m => m.Assignments).Returns(CreateSampleData(5));
            var result = _controller.Index() as ViewResult;

            Assert.IsNotNull(result);
        }
        [Test]
        public void Assignment_createView()
        {
 
            var result = _controller.Create() as ViewResult;

            Assert.IsNotNull(result);
        }

        [Test]
        public void Assignment_editView()
        {

            _contextMock.Setup(m => m.Assignments).Returns(CreateSampleData(5));
            var result = _controller.Edit(4) as ViewResult;
            Assert.IsNotNull(result);
        }
        [Test]
        public void Assignment_deleteView()
        {

            _contextMock.Setup(m => m.Assignments).Returns(CreateSampleData(5));
            var result = _controller.Delete(4) as ViewResult;
            Assert.IsNotNull(result);
        }


        [Test]
        [ExpectedException]
        public void Assignment_PostEditAssignment()
        {
            _contextMock.Setup(m => m.Assignments).Returns(CreateSampleData(5));
      
            Assignment ass1 = new Assignment { Id = 3, Title = "test" };
            SetupControllerForTests(_controller);
           
            var result = _controller.Edit(3, ass1) as ViewResult;
            Assert.IsNotNull(_contextMock.Object.Assignments.Find(3));
        }
        [Test]
        public void Assignment_getDetailsOfAssignment()
        {
            _contextMock.Setup(m => m.Assignments).Returns(CreateSampleData(5));
            var result = _controller.Details(4) as ViewResult;
            Assert.IsNotNull(result);
        }

        [Test]
        public void Assignment_PostCreateAssignment()
        {
            Assignment ass1 = new Assignment { Id = 3, Title = "title 3" };
            SetupControllerForTests(_controller);
            _contextMock.Setup(m => m.Assignments).Returns(CreateSampleData(2));

            Assert.IsNull(_contextMock.Object.Assignments.Find(3));
            var result = _controller.Create(ass1);
            Assert.IsNotNull(_contextMock.Object.Assignments.Find(3));
        }

        [Test]
        public void Assignment_DeleteAssignmentConfirm()
        {
        
            SetupControllerForTests(_controller);
            _contextMock.Setup(m => m.Assignments).Returns(CreateSampleData(3));
            Assert.IsNotNull(_contextMock.Object.Assignments.Find(3));
            var result = _controller.DeleteConfirmed(3);
            Assert.IsNull(_contextMock.Object.Assignments.Find(3)); 
        }

        [Test]
        public void Assignment_getDetailsOfNotExistingAssignment()
        {
            _contextMock.Setup(m => m.Assignments).Returns(CreateSampleData(5));
            var result = _controller.Details(7);
            Assert.IsInstanceOf<HttpNotFoundResult>(result);
        }

        private static void SetupControllerForTests(Controller controller)
        {
            var config = new HttpConfiguration();
            var Routes = new RouteCollection();
            RouteConfig.RegisterRoutes(Routes);

            var Request = new Mock<HttpRequestBase>(MockBehavior.Strict);
            Request.SetupGet(x => x.ApplicationPath).Returns("/");
            Request.SetupGet(x => x.Url).Returns(new Uri("http://localhost/assignment", UriKind.Absolute));
            Request.SetupGet(x => x.ServerVariables).Returns(new System.Collections.Specialized.NameValueCollection());

            var Response = new Mock<HttpResponseBase>(MockBehavior.Strict);

            var Context = new Mock<HttpContextBase>(MockBehavior.Strict);
            Context.SetupGet(x => x.Request).Returns(Request.Object);
            Context.SetupGet(x => x.Response).Returns(Response.Object);
            controller.Url = new System.Web.Mvc.UrlHelper(new RequestContext(Context.Object, new RouteData()), Routes);


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
