using System;
using System.Collections.Generic;
using System.Text;

namespace BLL.ViewModels.SessionViewModels
{
    public class UpdateSessionViewModel
    {
        public int SessionTrainerId { get; set; }
        public string Description { get; set; } = string.Empty;
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        
    }
}
