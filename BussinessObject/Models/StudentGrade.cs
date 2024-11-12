using System;
using System.Collections.Generic;

namespace BussinessObject.Models
{
    public partial class StudentGrade
    {
        public int StudentId { get; set; }
        public int GradeId { get; set; }
        public decimal? Value { get; set; }

        public virtual Grade Grade { get; set; } = null!;
        public virtual User Student { get; set; } = null!;
    }
}
