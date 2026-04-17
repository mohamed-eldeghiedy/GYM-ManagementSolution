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
            var session = _unitOfWork.GetRepository<Session>().GetAll();
            if (session is null || !session.Any())
                return [];
            return _mapper.Map<IEnumerable<SessionViewModel>>(session);

        }
        public SessionViewModel? GetSessionDetails(int sessionId)
        {

            var session = _unitOfWork.GetRepository<Session>().GetAll;
            if (session is null )
                return null;
            return _mapper.Map<SessionViewModel>(session);
        }
        public bool CreateSession(UpdateSessionViewModel model)
        {
            throw new NotImplementedException();
        }

        public bool DeleteSession(int sessionId)
        {
            throw new NotImplementedException();
        }



       

        public UpdateSessionViewModel? GetSessionToUpdate(int sessionId)
        {
            throw new NotImplementedException();
        }

        public bool UpdateSession(int sessionId, UpdateSessionViewModel model)
        {
            throw new NotImplementedException();
        }
    }
}
