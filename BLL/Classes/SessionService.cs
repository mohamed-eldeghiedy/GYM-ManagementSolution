using AutoMapper;
using BLL.Interfaces;
using BLL.ViewModels.SessionViewModels;
using DAL.Entities;
using DAL.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace BLL.Classes
{
    public class SessionService : ISessionService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public SessionService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        public IEnumerable<SessionViewModel> GetAllSessions()
        {
            var sessions = _unitOfWork.SessionRepository.GetAllSessionsWithTrainerAndCategory();
            if (sessions is null || !sessions.Any())
                return [];
            var mappedSessions = _mapper.Map<IEnumerable<SessionViewModel>>(sessions);
            foreach (var session in mappedSessions)
                session.AvailableSlots = session.Capacity - _unitOfWork.SessionRepository.GetCountOfBookedSlots(session.Id);
            return mappedSessions;
        }
        public SessionViewModel? GetSessionDetails(int sessionId)
        {

            var session = _unitOfWork.SessionRepository.GetSessionsWithTrainerAndCategory(sessionId);
            if (session is null )
                return null;
            var mappedSessions = _mapper.Map<SessionViewModel>(session);
            mappedSessions.AvailableSlots = mappedSessions.Capacity - _unitOfWork.SessionRepository.GetCountOfBookedSlots(sessionId);
            return mappedSessions;
        }
        public bool CreateSession(CreateSessionViewModel session)
        {
            if(!IsTrainerExists(session.SessionTrainerId) ||
                !IsCategoryExists(session.SessionCategoryId) ||
                !IsDateTimeValid(session.StartDate, session.EndDate)) return false;
            if(session.Capacity>25 || session.Capacity <0) return false;

            var mappedSession = _mapper.Map<Session>(session);
            _unitOfWork.SessionRepository.Add(mappedSession);
            return _unitOfWork.SaveChanges() > 0;
        }

        public UpdateSessionViewModel? GetSessionToUpdate(int sessionId)
        {
            var session = _unitOfWork.SessionRepository.GetById(sessionId);
            if (!IsSessionAvailableForUpdate(session))
                return null;

            return _mapper.Map<UpdateSessionViewModel>(session);
        }

        public bool UpdateSession(int sessionId, UpdateSessionViewModel model)
        {
            var session = _unitOfWork.SessionRepository.GetById(sessionId);
            if (!IsSessionAvailableForUpdate(session)) return false;
            if (!IsTrainerExists(session.CategoryId) ||
                !IsCategoryExists(session.CategoryId) ||
                !IsDateTimeValid(session.StartDate, session.EndDate)) return false;
            _mapper.Map(model, session);
            session.UpdatedAt = DateTime.Now;
            return _unitOfWork.SaveChanges() > 0;

        }


        public bool DeleteSession(int sessionId)
        {
            var session = _unitOfWork.SessionRepository.GetById(sessionId);
            if(!IsSessionAvailableForDelete(session)) return false;
            _unitOfWork.SessionRepository.Delete(session);
            return _unitOfWork.SaveChanges() > 0;
        }

        private bool IsSessionAvailableForDelete(Session session)
        {
            if ( session.EndDate < DateTime.Now) return false;
            if (_unitOfWork.SessionRepository.GetCountOfBookedSlots(session.Id) > 0)
                return false;
            return true;
        }
        private bool IsSessionAvailableForUpdate(Session session)
        {
            if(session is null || (session.EndDate<DateTime.Now) 
                || (session.StartDate<DateTime.Now)) return false;
            if(_unitOfWork.SessionRepository.GetCountOfBookedSlots(session.Id)>0) 
                return false;
            return true;
        }

        private bool IsTrainerExists(int trainerId)
            => _unitOfWork.GetRepository<Trainer>().GetById(trainerId) != null;
        private bool IsCategoryExists(int categoryId)
            => _unitOfWork.GetRepository<Trainer>().GetById(categoryId) != null;
        private bool IsDateTimeValid(DateTime StartDate, DateTime EndDate)
            => StartDate < EndDate;


        
    }
}
