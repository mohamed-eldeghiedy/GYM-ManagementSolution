using DAL.Entities;
using DAL.Repositories.Classes;
using System;
using System.Collections.Generic;
using System.Text;

namespace DAL.Repositories.Interfaces
{
    public interface IUnitOfWork
    {
        public ISessionRepository SessionRepository { get; }
        IRepository<TEntity> GetRepository<TEntity>() where TEntity : BaseEntity, new();

        int SaveChanges();
    }
}
