using DAL.Data;
using DAL.Entities;
using DAL.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace DAL.Repositories.Classes
{
    internal class TrainerRepository : GenericRepository<Trainer>, ITrainerRepository
    {
        private readonly GymDbContext _dbContext;
        public TrainerRepository(GymDbContext gymdbContext) : base(gymdbContext)
        {
            _dbContext = gymdbContext;
        }
    }
}
