using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BussinessObject.DTO.Grade
{
    public class ResultForCreateGradeDTO
    {
        public bool IsSuccess { get; set; }
        public int NumberCreated { get; set; }
        public string? Msg { get; set; }

    }
}
