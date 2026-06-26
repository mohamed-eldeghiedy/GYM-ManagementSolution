using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace BLL.ViewModels.PLanViewModels
{
    public class UpdatePlanViewModel
    {
        [Required(ErrorMessage = "Plan name is required")]
        [StringLength(150, MinimumLength = 2, ErrorMessage = "Plan name must be between 2 and 150 characters")]
        public string PlanName { get; set; } = null!;

        [Required(ErrorMessage = "Description is required")]
        [StringLength(1000, MinimumLength = 5, ErrorMessage = "Description must be between 5 and 1000 characters")]
        public string Description { get; set; } = null!;

        [Required(ErrorMessage = "Duration (in days) is required")]
        [Range( 1, 365, ErrorMessage = "Duration must be at least 1 day and at most 365 days")]
        public int DurationDays { get; set; }

        [Required(ErrorMessage = "Price is required")]
        [Range(0.01, 1000000, ErrorMessage = "Price must be greater than 0")]
        public decimal Price { get; set; }
    }
}
