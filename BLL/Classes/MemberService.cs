using AutoMapper;
using BLL.Interfaces;
using BLL.ViewModels.MemberViewModels;
using DAL.Entities;
using DAL.Repositories.Classes;
using DAL.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace BLL.Classes
{
    public class MemberService : IMemberService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public MemberService(IUnitOfWork unitOfWork , IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        public IEnumerable<MemberViewModel> GetAllMember()
        {
            var members = _unitOfWork.GetRepository<Member>().GetAll();
            if (members is null || !members.Any())
                return [];

            return _mapper.Map<IEnumerable<MemberViewModel>>(members);
        }


        public bool CreateMember(CreateMemberViewModel model)
        {
            var isEmailExists = _unitOfWork.GetRepository<Member>().GetAll().Any(m => m.Email == model.Email);
            var isPhoneExists = _unitOfWork.GetRepository<Member>().GetAll().Any(m => m.Phone == model.Phone);
            if (isEmailExists || isPhoneExists) return false;

            var member = _mapper.Map<Member>(model);
            _unitOfWork.GetRepository<Member>().Add(member);
            return _unitOfWork.SaveChanges()>0;   
        }

        public MemberViewModel? GetMemberDetails(int memberid)
        {
            var member = _unitOfWork.GetRepository<Member>().GetById(memberid);
            if (member is null) return null;

            var viewModel = _mapper.Map<MemberViewModel>(member);

            var memberShip = _unitOfWork.GetRepository<MemberShip>().GetAll()
                .FirstOrDefault(ms => ms.MemberId == memberid && ms.Status == "Active");

            if (memberShip is not null)
            {
                viewModel.MemberShipStartDate = memberShip.CreatedAt.ToShortDateString();
                viewModel.MemberShipEndDate = memberShip.EndDate.ToShortDateString();

                var plan = _unitOfWork.GetRepository<Plan>().GetById(memberShip.PlanId);
                if (plan is not null)
                {
                    viewModel.PlanName = plan.Name;
                }
            }
            return viewModel;
        }

        public HealthRecordViewModel? GetMemberHealthRecord(int memberid)
        {
            var healthRecord = _unitOfWork.GetRepository<HealthRecord>().GetById(memberid);
            if (healthRecord is null) return null;
            return _mapper.Map<HealthRecordViewModel>(healthRecord);
        }

        public MemberToUpdateViewModel? GetMemberToUpdate(int memberid)
        {
            var member = _unitOfWork.GetRepository<Member>().GetById(memberid);
            if (member is null) return null;

            return _mapper.Map<MemberToUpdateViewModel>(member);
        }

        public bool UpdateMemberDetails(int memberid, MemberToUpdateViewModel memberToUpdatemodel)
        {  
            var emailExist = _unitOfWork.GetRepository<Member>()
                .GetAll(m => m.Email == memberToUpdatemodel.Email && m.Id != memberid).Any();

            var phoneExist = _unitOfWork.GetRepository<Member>()
                .GetAll(m => m.Phone == memberToUpdatemodel.Phone && m.Id != memberid).Any();
            if (emailExist || phoneExist) return false;

            var member = _unitOfWork.GetRepository<Member>().GetById(memberid);
            if (member is null) return false;

            _mapper.Map(memberToUpdatemodel, member);
            _unitOfWork.GetRepository<Member>().Update(member);
            return _unitOfWork.SaveChanges() > 0;
        }

        public bool DeleteMember(int memberid)
        {
            var member = _unitOfWork.GetRepository<Member>().GetById(memberid);
            if (member is null) return false;

            var hasActiveSessions = _unitOfWork.GetRepository<MemberSession>()
                                    .GetAll(s=>s.MemberId==memberid && 
                                    s.Session.StartDate > DateTime.Now).Any();

            if (hasActiveSessions) return false;

            var memberShips = _unitOfWork.GetRepository<MemberShip>()
                              .GetAll(ms => ms.MemberId == memberid);
            if (memberShips.Any())
                foreach(var memberShip in memberShips )
                {
                    _unitOfWork.GetRepository<MemberShip>().Delete(memberShip);
                }
             _unitOfWork.GetRepository<Member>().Delete(member);
            return _unitOfWork.SaveChanges()>0;
        }
    }
}
