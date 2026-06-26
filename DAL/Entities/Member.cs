using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Entities
{
    public class Member : GymUser
    {
        public string? photo { get; set; }

        public HealthRecord HealthRecord { get; set; }

        public ICollection<MemberShip> MemberShips { get; set; }

        public ICollection<MemberSession> MemberSessions { get; set; }
    }
}
