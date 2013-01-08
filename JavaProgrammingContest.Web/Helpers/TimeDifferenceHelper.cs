using System;
using JavaProgrammingContest.Domain.Entities;

namespace JavaProgrammingContest.Web.Helpers{
    public class TimeDifferenceHelper{
        public static double GetTimeDifference(DateTime startTime){
            var elapsed = DateTime.Now - startTime;
            var timeDifference = elapsed.TotalSeconds;
            timeDifference = Math.Floor(timeDifference*100)/100;

            return timeDifference;
        }
    }

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