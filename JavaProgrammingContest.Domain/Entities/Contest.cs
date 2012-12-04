﻿using System.Collections.Generic;

namespace JavaProgrammingContest.Domain.Entities{
    public class Contest{
        public int Id { get; set; }
        public string Name { get; set; }
        public bool IsActive { get; set; }

        public ICollection<ContestAssignment> ContestAssignments { get; set; }
    }
}