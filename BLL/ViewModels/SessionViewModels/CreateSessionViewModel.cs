using BLL.ViewModels.MemberViewModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace BLL.ViewModels.SessionViewModels
{
    public class CreateSessionViewModel
    {
        
        public string Description { get; set; }=null!;
        public int SessionCategoryId { get; set; }
        public int SessionTrainerId { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public int Capacity { get; set; }
    }
}
