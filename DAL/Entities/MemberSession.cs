using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Entities
{
    internal class MemberSession : BaseEntity
    {
        public bool isAttended { get; set; }

        public int MemberId { get; set; }
        public Member Member { get; set; }

        public int SessionId { get; set; }
        public Session Session { get; set; }
    
    }
}
