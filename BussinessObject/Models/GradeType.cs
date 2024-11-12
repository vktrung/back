using System;
using System.Collections.Generic;

namespace BussinessObject.Models
{
    public partial class GradeType
    {
        public GradeType()
        {
            Grades = new HashSet<Grade>();
        }

        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public string? Description { get; set; }
        public int? PassConditionId { get; set; }
        public int GradedByRole { get; set; }

        public virtual Role GradedByRoleNavigation { get; set; } = null!;
        public virtual PassCondition? PassCondition { get; set; }
        public virtual ICollection<Grade> Grades { get; set; }
    }
}
