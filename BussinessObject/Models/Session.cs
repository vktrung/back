using System;
using System.Collections.Generic;

namespace BussinessObject.Models
{
    public partial class Session
    {
        public Session()
        {
            SessionStudents = new HashSet<SessionStudent>();
        }

        public int Id { get; set; }
        public int CourseId { get; set; }
        public int TeahcerId { get; set; }
        public int ClassId { get; set; }

        public virtual Class Class { get; set; } = null!;
        public virtual Course Course { get; set; } = null!;
        public virtual User Teahcer { get; set; } = null!;
        public virtual ICollection<SessionStudent> SessionStudents { get; set; }
    }
}
