 using System;
using System.Collections.Generic;
using System.Text;

namespace BLL.ViewModels.MemberViewModels
{
    public class HealthRecordViewModel
    {
        public decimal Weight { get; set; }
        public decimal Height { get; set; }
        public string BloodType { get; set; }
        public string Notes { get; set; }
    }
}
