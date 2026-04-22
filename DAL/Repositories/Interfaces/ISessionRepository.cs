using DAL.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace DAL.Repositories.Interfaces
{
    public interface ISessionRepository : IRepository<Session>
    {
        IEnumerable<Session> GetAllSessionsWithTrainerAndCategory();
        public Session? GetSessionsWithTrainerAndCategory(int sessionId);
        int GetCountOfBookedSlots(int sessionId);
    }
}
