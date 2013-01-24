using System.Collections.Generic;
using System.Data.Entity;
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
            return View(new Leaderboard(_context.Contests, _context.Participants));
        }
    }

    public class Leaderboard
    {
        public IEnumerable<ContestScore> Contest { get; set; }
        private readonly IDbSet<Contest> _contestsSet;
        private readonly IDbSet<Participant> _participantSet;

        public Leaderboard(IDbSet<Contest> contestsSet, IDbSet<Participant> participantList)
        {
            _contestsSet = contestsSet;
            _participantSet = participantList;
            CreateLeaderboard();
        }

        private void CreateLeaderboard()
        {
            var contestScores = new List<ContestScore>();

            if (_contestsSet != null)
                foreach (var contest in _contestsSet)
                    if (contest.Assignments != null)
                        contestScores.Add(new ContestScore(contest, _participantSet));
                

            Contest = contestScores;
        }
    }

    public class ContestScore
    {
        public string ContestName { get; set; }
        public IEnumerable<ParticipantScore> Participants { get; set; }
        private readonly Contest _contest;
        private IDbSet<Participant> ParticipantList { get; set; }

        public ContestScore(Contest contest, IDbSet<Participant> participantList)
        {
            _contest = contest;
            ContestName = contest.Name;
            ParticipantList = participantList;
            AddParticipantScores();
        }

        private void AddParticipantScores()
        {
            var participantScores = new List<ParticipantScore>();
            var idList = GetUniqueIdList();

            if (idList != null)
                foreach (var id in idList)
                    foreach (var participant in ParticipantList)
                        if (id == participant.Id)
                            participantScores.Add(new ParticipantScore(_contest, id, participant.Email, participant.Name, participant.Functie));

            Participants = participantScores;
        }

        private IEnumerable<int> GetUniqueIdList()
        {
            var idList = new List<int>();

            foreach (var assignment in _contest.Assignments)
                foreach (var score in assignment.Scores)
                {
                    var newId = score.Participant.Id;
                    var idfound = false;

                    foreach (var oldId in idList)
                        if (oldId == newId)
                            idfound = true;

                    if (!idfound)
                        idList.Add(newId);
                }

            return idList;
        }
    }

    public class ParticipantScore
    {
        public string Email { get; set; }
        public string Name { get; set; }
        public string Functie { get; set; }
        public int AssignmentsCompleted { get; set; }
        public int AssignmentsMade { get; set; }
        public double AverageTime { get; set; }
        public double CompletePercentage { get; set; }

        public ParticipantScore(Contest contest, int id, string email, string name, string functie)
        {
            Email = email;
            Name = name;
            Functie = functie;
            SetScore(contest, id);
        }

        private void SetScore(Contest contest, int id)
        {
            var assignmentsMade = 0;
            var assignmentsCompleted = 0;
            var totalTime = 0.0;

            foreach (var assignment in contest.Assignments)
                foreach (var score in assignment.Scores)
                    if (id == score.Participant.Id)
                    {
                        assignmentsMade++;
                        totalTime += score.TimeSpent;
                        if (score.IsCorrectOutput)
                            assignmentsCompleted++;
                    }

            AssignmentsMade = assignmentsMade;
            AssignmentsCompleted = assignmentsCompleted;
            AverageTime = totalTime / assignmentsMade + 0.0;
            CompletePercentage = CalculateCompletePercentage(assignmentsMade, assignmentsCompleted);
        }

        private double CalculateCompletePercentage(int assignmentsMade, int assignmentsComplete)
        {
            var completePercentage = 0.0;
            var made = assignmentsMade;
            var complete = assignmentsComplete;

            if (assignmentsComplete != 0 && assignmentsMade != 0)
            {
                completePercentage = (complete / made) * 100;
            }

            return completePercentage;
        }
    }
}