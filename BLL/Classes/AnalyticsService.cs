using BLL.Interfaces;
using BLL.ViewModels.AnalyticsViewModels;
using DAL.Data;
using DAL.Entities;
using DAL.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace BLL.Classes
{
    public class AnalyticsService : IAnalyticsService
    {
        private readonly IUnitOfWork _unitOfWork;

        public AnalyticsService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public AnalyticsViewModel GetAnalyticsData()
        {
            var session = _unitOfWork.GetRepository<Session>().GetAll();
            return new AnalyticsViewModel()
            {
                ActiveMembers = _unitOfWork.GetRepository<MemberShip>().GetAll(m=>m.Status =="IsActive").Count(),
                TotalMembers = _unitOfWork.GetRepository<Member>().GetAll().Count(),
                Trainers = _unitOfWork.GetRepository<Trainer>().GetAll().Count(),
                UpComingSessions = session.Count(s=>s.StartDate > DateTime.Now),
                OnGoingSessions = session.Count(s=>s.StartDate <= DateTime.Now && s.EndDate >= DateTime.Now),
                CompletedSessions = session.Count(s=>s.EndDate < DateTime.Now)

            };
        }
    }
}
