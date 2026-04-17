using BLL.ViewModels.MemberViewModels;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace BLL.ViewModels.SessionViewModels
{
    public class CreateSessionViewModel
    {
        [Required(ErrorMessage = "Description is required")]
        [StringLength(500, MinimumLength = 5, ErrorMessage = "Description must be between 5 and 500 characters")]
        public string Description { get; set; } = null!;

        [Required(ErrorMessage = "Category is required")]
        [Range(1, int.MaxValue, ErrorMessage = "Please select a valid category")]
        public int SessionCategoryId { get; set; }

        [Required(ErrorMessage = "Trainer is required")]
        [Range(1, int.MaxValue, ErrorMessage = "Please select a valid trainer")]
        public int SessionTrainerId { get; set; }

        [Required(ErrorMessage = "Start date is required")]
        [DataType(DataType.DateTime)]
        public DateTime StartDate { get; set; }

        [Required(ErrorMessage = "End date is required")]
        [DataType(DataType.DateTime)]
        public DateTime EndDate { get; set; }

        [Required(ErrorMessage = "Capacity is required")]
        [Range(1, 100, ErrorMessage = "Capacity must be between 1 and 100")]
        public int Capacity { get; set; }
    }
}

