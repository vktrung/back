using System;
using System.Collections.Generic;

namespace BussinessObject.Models
{
    public partial class Role
    {
        public Role()
        {
            GradeTypes = new HashSet<GradeType>();
            Users = new HashSet<User>();
        }

        public int RoleId { get; set; }
        public string RoleName { get; set; } = null!;

        public virtual ICollection<GradeType> GradeTypes { get; set; }
        public virtual ICollection<User> Users { get; set; }
    }
}
