using JavaProgrammingContest.Domain.Entities;

namespace JavaProgrammingContest.Web.Helpers{
    public class ScoreHelper{
        public static Score CreateScore(Assignment assignment, Participant participant, bool correctOutput, double timeDifference){
            var score = new Score{
                Assignment = assignment,
                IsCorrectOutput = correctOutput,
                Participant = participant,
                TimeSpent = timeDifference
            };

            return score;
        }
    }
}