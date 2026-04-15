using DAL.Data;
using DAL.Entities;
using DAL.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace DAL.Repositories.Classes
{
    public class MemberRepository : GenericRepository<Member>,IMemberRepository
    {
        private readonly GymDbContext _dbContext ;
        public MemberRepository(GymDbContext gymdbContext) : base(gymdbContext)
        {
            _dbContext = gymdbContext;
        }
    }
}
