using DAL.Entities.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Entities
{
    public class Trainer: GymUser
    {
        Specialties Specialty { get; set; }

        public ICollection<Session> TrainerSessions { get; set; }
    }
}
