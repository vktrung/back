using System;
using System.Collections.Generic;

namespace BussinessObject.Models
{
    public partial class SessionStudent
    {
        public int SessionId { get; set; }
        public int StudentId { get; set; }
        public decimal? AvgGragde { get; set; }
        public bool? Status { get; set; }

        public virtual Session Session { get; set; } = null!;
        public virtual User Student { get; set; } = null!;
    }
}
