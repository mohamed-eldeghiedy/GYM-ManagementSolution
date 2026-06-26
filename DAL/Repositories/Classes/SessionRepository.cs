using DAL.Data;
using DAL.Entities;
using DAL.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace DAL.Repositories.Classes
{
    public class SessionRepository : GenericRepository<Session>, ISessionRepository
    {
        private readonly GymDbContext _dbContext;

        public SessionRepository( GymDbContext dbContext) : base(dbContext) 
        {
            _dbContext = dbContext;
        }
        public IEnumerable<Session> GetAllSessionsWithTrainerAndCategory()
        {
           return _dbContext.Sessions
                .Include(s => s.SessionTrainer)
                .Include(s => s.SessionCategory)
                .ToList();
        }

        public int GetCountOfBookedSlots(int sessionId)
        {
            return _dbContext.MemberSessions.Count(b => b.SessionId == sessionId);
        }

        public Session? GetSessionsWithTrainerAndCategory(int sessionId)
        {
            return _dbContext.Sessions
                .Include(s => s.SessionTrainer)
                .Include(s => s.SessionCategory)
                .FirstOrDefault(s => s.Id == sessionId);
        }
    }
}
