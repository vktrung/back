﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BussinessObject.DTO.User
{
    public class GetUserDTO
    {
        public int Id { get; set; }
        public string? Username { get; set; }
        public int? RoleId { get; set; }
    }
}
