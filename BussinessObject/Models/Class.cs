using System;
using System.Collections.Generic;

namespace BussinessObject.Models
{
    public partial class Class
    {
        public Class()
        {
            Sessions = new HashSet<Session>();
        }

        public int Id { get; set; }
        public string Name { get; set; } = null!;

        public virtual ICollection<Session> Sessions { get; set; }
    }
}
