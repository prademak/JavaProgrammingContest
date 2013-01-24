using JavaProgrammingContest.DataAccess.Context;
using JavaProgrammingContest.DataAccess.TestSupport;
using JavaProgrammingContest.Domain.Entities;
using JavaProgrammingContest.Web.Controllers;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JavaProgrammingContest.Web.Tests.Controllers
{
    [TestFixture]
    class ScoresControllerTests
    {
        private Mock<IDbContext> _contextMock;
        private ScoresController _controller;
        private ParticipantScore _scoreCalc;
        private Leaderboard _leaderboard;

        [SetUp]
        public void SetUp()
        {
            _contextMock = new Mock<IDbContext>();
            _controller = new ScoresController(_contextMock.Object);
            ICollection<Assignment> col = new Collection<Assignment>();
            var ass = new Assignment { Id = 1, Title = "Test", Scores = new Collection<Score>() };
            var participant = new Participant { Id = 1, Email = "vin" };
            ass.Scores.Add(new Score { Assignment = ass, Id = 1, IsCorrectOutput = true, Participant = participant, TimeSpent = 199 });
            col.Add(ass);
            Contest cont = new Contest{ Assignments = col, Id = 1, IsActive = true, Name = "Contest A" } ;
            _scoreCalc = new ParticipantScore(cont, participant);
            FakeDbSet<Contest> condb = new FakeDbSet<Contest>();
            condb.Add(cont);
            FakeDbSet<Participant> pardb = new FakeDbSet<Participant>();
            pardb.Add(participant);
            _leaderboard = new Leaderboard(condb, pardb);

        }

        

        [Test]
        public void Scores_TestIfCompletePercentageCalculatorWorks()
        {
            var com = _scoreCalc.CompletePercentage;
            Assert.AreEqual(100.0, com);
        }
         

    }
}
