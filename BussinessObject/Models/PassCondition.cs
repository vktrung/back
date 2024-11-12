using System;
using System.Collections.Generic;

namespace BussinessObject.Models
{
    public partial class PassCondition
    {
        public PassCondition()
        {
            GradeTypes = new HashSet<GradeType>();
        }

        public int Id { get; set; }
        public int? ComparisonTypeId { get; set; }
        public int? GradeValue { get; set; }

        public virtual ComparisonType? ComparisonType { get; set; }
        public virtual ICollection<GradeType> GradeTypes { get; set; }
    }
}
