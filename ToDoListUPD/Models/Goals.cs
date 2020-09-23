using System;
using System.Collections.Generic;

namespace ToDoListUPD.Models
{
    public partial class Goals
    {
        public int GoalId { get; set; }
        public string GoalName { get; set; }

        public DateTime Date { get; set; }

        public GoalStatus GoalStatus { get; set; }
    }
}
