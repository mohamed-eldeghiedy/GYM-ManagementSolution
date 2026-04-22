using System;
using System.Collections.Generic;
using System.Text;

namespace BLL.ViewModels.SessionViewModels
{
    public class SessionViewModel
    {
        public int Id { get; set; }

        public string CategoryName { get; set; } = string.Empty;

        public string TrainerName { get; set; } = string.Empty;

        public int AvailableSlots { get; set; }
        public int Capacity { get; set; }
        public string Description { get; set; } = string.Empty;
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public string DateDisplay => $"{StartDate:MMM dd, yyyy} ";
        public string TimeDisplay => $"{StartDate:hh:mm tt} - {EndDate:hh:mm tt}";
        public TimeSpan Duration => EndDate - StartDate;


        public string Status
        {
            get
            {
                if (DateTime.Now < StartDate)
                    return "Upcoming";
                else if (DateTime.Now >= StartDate && DateTime.Now <= EndDate)
                    return "Ongoing";
                else
                    return "Completed";
            }
        }

    }
}
