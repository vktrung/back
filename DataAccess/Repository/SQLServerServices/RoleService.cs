using AutoMapper;
using BussinessObject.DTO.Role;
using BussinessObject.Models;
using DataAccess.DataAccess;
using DataAccess.Repository.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Repository.SQLServerServices
{
    public class RoleService : IRoleRepository
    {
        private readonly prn231Context _context;
        private readonly IMapper _mapper;

        public RoleService(prn231Context context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public List<RoleDTO> GetRoleGraded()
        {
            List<Role> listR = _context.Roles
                .Where(r => r.RoleName.Equals("Phong Khao Thi") || r.RoleName.Equals("Teacher"))
                .ToList();
            List<RoleDTO> listRDTO = _mapper.Map<List<RoleDTO>>(listR);
            return listRDTO;
        }
    }
}
