using System.Web.Http;
using JavaProgrammingContest.DataAccess.Context;
using JavaProgrammingContest.DataAccess.TestSupport;
using JavaProgrammingContest.Domain.Entities;
using JavaProgrammingContest.Web.API;
using JavaProgrammingContest.Web.App_Start;
using Moq;
using NUnit.Framework;

namespace JavaProgrammingContest.Web.Tests.Api{
    [TestFixture]
    public class UserSettingsControllerTests{
        private UserSettingsController _controller;
        private Mock<IDbContext> _contextMock;

        [SetUp]
        public void SetUp(){
            _contextMock = new Mock<IDbContext>();
            _controller = new UserSettingsController(_contextMock.Object);
            MapperConfig.Configure();
        }

        [Test]
        public void GetUserSettingsWithValidId(){
            var testId = 1;
            var sampleData = CreateSampleData(1);
            _contextMock.Setup(m => m.Participants).Returns(sampleData);
            Assert.AreEqual(sampleData.Find(testId).UserSetting.Id, _controller.Get(testId).Id);
        }

        [Test]
        [ExpectedException(typeof(HttpResponseException))]
        public void GetUserSettingsWithInvalidId(){
            var testId = 100;
            var sampleData = CreateSampleData(1);
            _contextMock.Setup(m => m.Participants).Returns(sampleData);
            _controller.Get(testId);
        }

        private static FakeParticipantSet CreateSampleData(int nrOfRecords){
            var sampleData = new FakeParticipantSet();

            for (var i = 1; i <= nrOfRecords; i++)
            {
                sampleData.Add(new Participant
                    {
                        Id = i,
                        UserSetting = new UserSetting()
                            {
                                Id = i,
                                Theme = "eclipse",
                                MatchBrackets = true,
                                AutoIndent = true,
                                TabSize = 3,
                                LineWrapping = false,
                                IntelliSense = true,
                                Participant = new Participant()
                                    {
                                        Id = i
                                    }
                            }
                    });
            }
            return sampleData;
        }
    }
}