using System;
using JavaProgrammingContest.DataAccess.Context;
using JavaProgrammingContest.Domain.Entities;
using JavaProgrammingContest.Process.Compiler;
using JavaProgrammingContest.Process.Compiler.Java;
using JavaProgrammingContest.Process.Runner;
using JavaProgrammingContest.Process.Runner.Java;
using JavaProgrammingContest.Web.API;
using Moq;
using NUnit.Framework;

namespace JavaProgrammingContest.Web.Tests.Api{
    [TestFixture]
    public class ScoresControllerTests {
        private static ScoresController _controller;
        private static Mock<IDbContext> _contextMock;
        private static IRunner _runner;
        private static ICompiler _compiler;

        [SetUp]
        public void SetUp()
        {
            _contextMock = new Mock<IDbContext>();
            _compiler = new JavaCompiler();
            _runner = new JavaRunner();
            _controller = new ScoresController(_contextMock.Object, _compiler, _runner);
        }

        [Test]
        public void TestIfScoreTimeDifferenceOfZeroIsCorrect()
        {
            const double timeDifference = 0.0;
            var score = createSampleScore(true, timeDifference);

            Assert.IsTrue(score.TimeSpent == timeDifference);
        }

        [Test]
        public void TestIfScoreTimeDifferenceOfOneDotOneIsCorrect()
        {
            const double timeDifference = 1.1;
            var score = createSampleScore(true, timeDifference);

            Assert.IsTrue(score.TimeSpent == timeDifference);
        }

        [Test]
        public void TestIfScoreCorrectOutputWhenTrueIsCorrect()
        {
            const bool correctOutput = true;
            var score = createSampleScore(correctOutput, 0);

            Assert.IsTrue(score.IsCorrectOutput == correctOutput);
        }
        [Test]
        public void TestIfScoreCorrectOutputWhenFalseIsCorrect()
        {
            const bool correctOutput = false;
            var score = createSampleScore(correctOutput, 0);

            Assert.IsTrue(score.IsCorrectOutput == correctOutput);
        }

        private Score createSampleScore(bool correctOutput, double timeDifference){
            var participant = new Participant{Id = 0, Email = "Participant" + 0, Interested = true};
            var assignment = new Assignment{
                CodeGiven =
                    "// Sample class\nclass HelloWorldApp {\n\tpublic static void main(String[] args){\n\t\tSystem.out.println(\"Hello World!\");\n\t}\n}\n",
                Description =
                    "Print the following text: \"Hello World!\"",
                Title = "Hello World!",
                MaxSolveTime = 900,
                RunCodeInput = "",
                RunCodeOuput = "Hello World!"
            };

            //Score score = ScoresController.CreateScore(assignment, participant, correctOutput, timeDifference);

            return new Score();
        }
    }
}