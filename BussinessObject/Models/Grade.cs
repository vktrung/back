using System;
using System.Collections.Generic;

namespace BussinessObject.Models
{
    public partial class Grade
    {
        public Grade()
        {
            StudentGrades = new HashSet<StudentGrade>();
        }

        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public int GradeTypeId { get; set; }
        public int CourseId { get; set; }
        public int Weight { get; set; }

        public virtual Course Course { get; set; } = null!;
        public virtual GradeType GradeType { get; set; } = null!;
        public virtual ICollection<StudentGrade> StudentGrades { get; set; }
    }
}
