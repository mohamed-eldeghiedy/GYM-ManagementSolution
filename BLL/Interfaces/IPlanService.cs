using BLL.ViewModels.PLanViewModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace BLL.Interfaces
{
    public interface IPlanService
    {
        IEnumerable<PLanViewModel> GetAllPlans();
        PLanViewModel? GetPlanById(int id);
        UpdatePlanViewModel? GetPlanToUpdate(int id);
        bool UpdatePlan( int id, UpdatePlanViewModel plan);
        bool ToggleStatus(int id);
    }
}
