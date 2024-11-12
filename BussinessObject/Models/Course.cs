using System;
using System.Collections.Generic;

namespace BussinessObject.Models
{
    public partial class Course
    {
        public Course()
        {
            Grades = new HashSet<Grade>();
            Sessions = new HashSet<Session>();
        }

        public int Id { get; set; }
        public string Code { get; set; } = null!;
        public string Name { get; set; } = null!;

        public virtual ICollection<Grade> Grades { get; set; }
        public virtual ICollection<Session> Sessions { get; set; }
    }
}
