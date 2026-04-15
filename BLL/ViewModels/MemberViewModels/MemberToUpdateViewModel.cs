using System;
using System.Collections.Generic;
using System.Text;

namespace BLL.ViewModels.MemberViewModels
{
    public class MemberToUpdateViewModel
    {
        public string Name { get; set; }
        public string? PhotoUrl { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public int BuildingNumber { get; set; }
        public string Street { get; set; }
        public string City { get; set; }
    }
}
