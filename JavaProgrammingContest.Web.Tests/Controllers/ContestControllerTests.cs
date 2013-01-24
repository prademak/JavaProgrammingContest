using JavaProgrammingContest.DataAccess.Context;
using JavaProgrammingContest.DataAccess.TestSupport;
using JavaProgrammingContest.Domain.Entities;
using JavaProgrammingContest.Web.Controllers;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JavaProgrammingContest.Web.Tests.Controllers
{
    [TestFixture]
    class ContestControllerTests
    {
        private Mock<IDbContext> _contextMock;
        private ScoresController _controller;

        [SetUp]
        public void SetUp()
        {
            _contextMock = new Mock<IDbContext>();
            _controller = new ScoresController(_contextMock.Object);
        }

        [Test]
        public void ScoresControllerReturnsAView()
        {
            Assert.IsNotNull(_controller.Index());
        }
    }
}
