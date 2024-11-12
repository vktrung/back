﻿using BussinessObject.DTO.Role;
using BussinessObject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Repository.Interfaces
{
    public interface IRoleRepository
    {
        List<RoleDTO> GetRoleGraded();
    }
}
