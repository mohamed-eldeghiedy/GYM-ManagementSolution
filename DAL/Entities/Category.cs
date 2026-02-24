using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Entities
{
    internal class Category : BaseEntity
    {
        public string CategoryName { get; set; }

        public ICollection<Session> Sessions { get; set; }
    }
}

