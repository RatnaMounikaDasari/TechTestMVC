using System.Collections.Generic;

namespace TechTestMVC.Models
{
    public class TestResultViewModel
    {
        // Task 1
        public List<int> EvenNumbers { get; set; }
        public List<int> DivisibleNumbers { get; set; }

        // Task 2
        public bool FirstEventScheduled { get; set; }
        public bool SecondEventScheduled { get; set; }
        public bool Cancelled { get; set; }
        public List<Event> RemainingEvents { get; set; }
    }
}
