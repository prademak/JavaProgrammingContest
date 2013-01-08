using System.Collections.Generic;
using System.Web.Mvc;
using JavaProgrammingContest.DataAccess.Context;
using JavaProgrammingContest.Domain.Entities;

namespace JavaProgrammingContest.Web.Controllers{
    /// <summary>
    ///     Scoreboard controller.
    /// </summary>
    public class ScoresController : Controller{
        /// <summary>
        ///     Database Context
        /// </summary>
        private readonly IDbContext _context;

        /// <summary>
        ///    Scores controller constructor
        /// </summary>
        /// <param name="context">Database Context to use for the Controller</param>
        public ScoresController(IDbContext context){
            _context = context;
        }

        /// <summary>
        ///     Index for this Controller
        /// </summary>
        /// <returns></returns>
        public ActionResult Index()
        {
            return View(CreateLeaderboard());
        }

        public Leaderboard CreateLeaderboard()
        {
            Leaderboard lb = new Leaderboard();

            List<ContestScore> listC = new List<ContestScore>();

            foreach (var contest in _context.Contests)
            {
                if (contest.Assignments != null)
                {
                    List<ParticipantScore> listP = new List<ParticipantScore>();

                    ContestScore cs = new ContestScore();
                    cs.ContestName = contest.Name;


                    listP.Add(new ParticipantScore());

                    cs.Participants = AddParticipantScores(contest);
                    listC.Add(cs);
                }
            }

            lb.Contest = listC;

            return lb;
        }

        private List<ParticipantScore> AddParticipantScores(Contest contest)
        {
            List<ParticipantScore> pList = new List<ParticipantScore>();
            List<int> IdList = new List<int>();

            IdList = GetUniqueIdList(contest);

            foreach (var ID in IdList)
            {
                foreach (var participant in _context.Participants)
                {
                    if (ID == participant.Id)
                    {
                        ParticipantScore ps = CreateParticipantScore(contest, ID);
                        ps.Email = participant.Email;

                        pList.Add(ps);
                    }
                }
            }

            return pList;
        }

        private List<int> GetUniqueIdList(Contest contest)
        {
            List<int> IDList = new List<int>();

            foreach (var assignment in contest.Assignments)
            {
                foreach (var score in assignment.Scores)
                {
                    int newID = score.Participant.Id;
                    bool IDfound = false;

                    foreach (var oldID in IDList)
                    {
                        if (oldID == newID)
                            IDfound = true;
                    }

                    if (!IDfound)
                        IDList.Add(newID);
                }
            }

            return IDList;
        }

        private double CalculateCompletePercentage(int assignmentsMade, int assignmentsComplete)
        {
            double completePercentage = 0;
            double made = assignmentsMade;
            double complete = assignmentsComplete;

            if (assignmentsComplete != 0 && assignmentsMade != 0)
            {
                completePercentage = (complete / made) * 100;
            }

            return completePercentage;
        }

        private ParticipantScore CreateParticipantScore(Contest contest, int ID)
        {
            ParticipantScore ps = new ParticipantScore();

            int assignmentsMade = 0;
            int assignmentsCompleted = 0;
            double totalTime = 0;

            foreach (var assignment in contest.Assignments)
            {
                foreach (var score in assignment.Scores)
                {
                    if (ID == score.Participant.Id)
                    {
                        assignmentsMade++;
                        totalTime += score.TimeSpent;
                        if (score.IsCorrectOutput)
                            assignmentsCompleted++;
                    }
                }
            }

            ps.AssignmentsMade = assignmentsMade;
            ps.AssignmentsCompleted = assignmentsCompleted;
            ps.AverageTime = totalTime / assignmentsMade;
            ps.CompletePercentage = CalculateCompletePercentage(assignmentsMade, assignmentsCompleted);

            return ps;
        }
    }

    public class Leaderboard
    {
        public IEnumerable<ContestScore> Contest { get; set; }
    }

    public class ContestScore
    {
        public string ContestName { get; set; }
        public IEnumerable<ParticipantScore> Participants { get; set; }
    }

    public class ParticipantScore
    {
        public string Email { get; set; }
        public int AssignmentsCompleted { get; set; }
        public int AssignmentsMade { get; set; }
        public double AverageTime { get; set; }
        public double CompletePercentage { get; set; }
    }
}