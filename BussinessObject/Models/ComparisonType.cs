using System;
using System.Collections.Generic;

namespace BussinessObject.Models
{
    public partial class ComparisonType
    {
        public ComparisonType()
        {
            PassConditions = new HashSet<PassCondition>();
        }

        public int Id { get; set; }
        public string Name { get; set; } = null!;

        public virtual ICollection<PassCondition> PassConditions { get; set; }
    }
}
