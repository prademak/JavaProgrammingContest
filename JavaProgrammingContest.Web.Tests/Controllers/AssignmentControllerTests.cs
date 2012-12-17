using System;
using System.Web.Mvc;
using JavaProgrammingContest.DataAccess.Context;
using JavaProgrammingContest.Domain.Entities;
using JavaProgrammingContest.Web.Controllers;
using Moq;
using NUnit.Framework;

namespace JavaProgrammingContest.Web.Tests.Controllers{
    [TestFixture]
    public class AssignmentControllerTests {
        private AssignmentController _controller;
        private Mock<IDbContext> _contextMock;

        [SetUp]
        public void SetUp()
        {
            _contextMock = new Mock<IDbContext>();
         
            _controller = new AssignmentController(_contextMock.Object);
        }

        [Test]
        public void AssignmentControllerReturnsAView()
        {
        

         
       //    Assert.NotNull(result); 
        }
    }
}