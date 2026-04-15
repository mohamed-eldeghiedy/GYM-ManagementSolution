using DAL.Data;
using DAL.Entities;
using DAL.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace DAL.Repositories.Classes
{
    public class UnitOfWork (GymDbContext _dbContext): IUnitOfWork
    {
        private readonly Dictionary<Type, object> _repositories=[];
        public IRepository<TEntity> GetRepository<TEntity>() where TEntity : BaseEntity, new()
        {
            var entityType = typeof(TEntity);

            if (_repositories.TryGetValue(entityType, out var repo)) 
                return (IRepository<TEntity>)repo;

            var newRepo = new GenericRepository<TEntity>(_dbContext);
            _repositories[entityType] = newRepo;
            return newRepo;
           
        }

        public int SaveChanges()
        => _dbContext.SaveChanges();
    }
}
