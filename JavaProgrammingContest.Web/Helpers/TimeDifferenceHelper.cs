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

    
}