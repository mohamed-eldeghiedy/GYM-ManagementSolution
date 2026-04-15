using DAL.Data;
using DAL.Entities;
using DAL.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace DAL.Repositories.Classes
{
    public class GenericRepository<TEntity> : IRepository<TEntity>
        where TEntity : BaseEntity, new()
    {
        private readonly GymDbContext _dbContext;
        public GenericRepository(GymDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public void Add(TEntity entity)
        {
            _dbContext.Set<TEntity>().Add(entity);
            
        }

        public void Delete(TEntity entity)
        {
           
            _dbContext.Set<TEntity>().Remove(entity);
                 
        }

        public IEnumerable<TEntity> GetAll(Func<TEntity, bool>? condition = null)
        {
            if(condition == null)
                return _dbContext.Set<TEntity>().AsNoTracking().ToList();
            else
                return _dbContext.Set<TEntity>().AsNoTracking().Where(condition).ToList();
        }
        

        public TEntity GetById(int id)
        => _dbContext.Set<TEntity>().Find(id);

        public void Update(TEntity entity)
        {
            _dbContext.Set<TEntity>().Update(entity);
           
        }
    }
}
