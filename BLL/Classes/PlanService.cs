using AutoMapper;
using BLL.Interfaces;
using BLL.ViewModels.PLanViewModels;
using DAL.Entities;
using DAL.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace BLL.Classes
{
    public class PlanService : IPlanService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public PlanService(IUnitOfWork unitOfWork , IMapper mapper)
        {
           _unitOfWork = unitOfWork;
           _mapper = mapper;
        }
        public IEnumerable<PLanViewModel> GetAllPlans()
        {
            var Plans = _unitOfWork.GetRepository<Plan>().GetAll();
                if (Plans is null || !Plans.Any()) return [];

            return _mapper.Map<IEnumerable<PLanViewModel>>(Plans);
        }

        public PLanViewModel? GetPlanById(int id)
        {
           var plan =_unitOfWork.GetRepository<Plan>().GetById(id);
            if (plan is null) return null;
           
           return _mapper?.Map<PLanViewModel>(plan);
        }

        public UpdatePlanViewModel? GetPlanToUpdate(int id)
        {
            var plan = _unitOfWork.GetRepository<Plan>().GetById(id);
            if (plan is null || plan.IsActive == false || HasActiveMemberShip(id)) return null;
            return _mapper?.Map<UpdatePlanViewModel>(plan);
        }
        private bool HasActiveMemberShip(int planId)
        { 
            var activeMemberShips = _unitOfWork.GetRepository<MemberShip>()
                .GetAll(m=>m.Id == planId && m.Status =="Active");
            return activeMemberShips.Any();
        }

        public bool ToggleStatus(int id)
        {
            var Repo= _unitOfWork.GetRepository<Plan>();
            var plan = Repo.GetById(id);
            if(plan is null || HasActiveMemberShip(id)) return false;
            plan.IsActive = !plan.IsActive == true? false : true ;
            plan.UpdatedAt = DateTime.Now;

            _unitOfWork.GetRepository<Plan>().Update(plan);
            return _unitOfWork.SaveChanges() > 0;
        }

        public bool UpdatePlan(int id, UpdatePlanViewModel updatePlan)
        {
            var plan = _unitOfWork.GetRepository<Plan>().GetById(id);
            if (plan is null || HasActiveMemberShip(id)) return false;

            (plan.Description, plan.DurationDays, plan.UpdatedAt, plan.Price)
                = (updatePlan.Description, updatePlan.DurationDays,DateTime.Now , updatePlan.Price);

            _unitOfWork.GetRepository<Plan>().Update(plan);
            return _unitOfWork.SaveChanges()>0;
        }
    }
}
