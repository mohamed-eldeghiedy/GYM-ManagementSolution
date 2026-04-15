using System;
using System.Collections.Generic;
using System.Text;

namespace BLL.ViewModels.PLanViewModels
{
    public class UpdatePlanViewModel
    {
        public string PlanName { get; set; }=null!;
        public string Description { get; set; }=null!;
        public int DurationDays { get; set; }
        public decimal Price { get; set; }
    }
}
