﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BussinessObject.DTO.Session
{
    public class GetSessionDTO
    {
        public int Id { get; set; }
        public int CourseId { get; set; }
        public string CourseName { get; set; }
        public int ClassId { get; set; }
        public string ClassName { get; set; }
        public int TeahcerId { get; set; }
        public string TeacherName { get; set; }
    }
}
