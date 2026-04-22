using System;
using System.Collections.Generic;
using System.Text;

namespace BLL.ViewModels.AnalyticsViewModels
{
    public class AnalyticsViewModel
    {
        public int TotalMembers { get; set; }
        public int ActiveMembers { get; set; }
        public int Trainers { get; set; }
        public int UpComingSessions { get; set; }
        public int OnGoingSessions { get; set; }
        public int CompletedSessions { get; set; }
             
    }
}
