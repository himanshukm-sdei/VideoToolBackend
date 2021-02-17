using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Entities
{
    public class SesionCounts
    {
        public int PreviousCount { get; set; }
        public int WaitingCount { get; set; }
        public int UpcomingCount { get; set; }
        public int AllCount { get; set; }
        public int AvailableCount { get; set; }
        public int Approved { get; set; }
        public int Pending { get; set; }
        public int Rejected { get; set; }
        public int Delivered { get; set; }
        public int Cancelled { get; set; }
    }
}
