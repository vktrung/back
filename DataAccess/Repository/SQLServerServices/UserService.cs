using AutoMapper;
using BussinessObject.DTO.User;
using BussinessObject.Models;
using DataAccess.DataAccess;
using DataAccess.Repository.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Repository.SQLServerServices
{
    public class UserService : IUserRepository
    {

        private readonly prn231Context _context;
        private readonly IMapper _mapper;

        public UserService(prn231Context context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public List<GetUserDTO> GetStudentByCourseId(int courseId)
        {
            List<int> listSessionId = _context.Sessions.Where(ss => ss.CourseId == courseId).Select(ss => ss.Id).ToList();

            List<int> listStudentId = _context.SessionStudents.Where(ssst => listSessionId.Contains(ssst.SessionId)).Select(ssst => ssst.StudentId).ToList();

            List<User> listStudent = _context.Users.Where(u => listStudentId.Contains(u.Id)).ToList();

            List<GetUserDTO> listStudentDTO = _mapper.Map<List<GetUserDTO>>(listStudent);

            return listStudentDTO;
        }

        public List<GetUserDTO> GetStudentInSession(int sessionId)
        {
            List<int> listStudentId = _context.SessionStudents.Where(ss => ss.SessionId == sessionId).Select(ss => ss.StudentId).ToList();
            List<User> listStudent = _context.Users.Where(u => listStudentId.Contains(u.Id)).ToList();

            List<GetUserDTO> listStudentDTO = _mapper.Map<List<GetUserDTO>>(listStudent);

            return listStudentDTO;
        }

        public GetUserDTO GetStudent(string username)
        {
            User u = _context.Users.FirstOrDefault(u => u.Username == username && u.RoleId == 4);
            GetUserDTO uDTO = _mapper.Map<GetUserDTO>(u);
            if (u == null)
            {
                return null;
            }
            return uDTO;
        }

        public GetUserDTO GetUser(string username, string password)
        {
            User u = _context.Users.FirstOrDefault(u => u.Username == username && u.Password == password);
            GetUserDTO uDTO = _mapper.Map<GetUserDTO>(u);
            if (u == null)
            {
                return null;
            }
            return uDTO;
        }

        public List<GetUserDTO> GetUsersByRole(int roleId)
        {

            List<User> listUser = _context.Users.Where(u => u.RoleId == roleId).ToList();

            List<GetUserDTO> listUserDTO = _mapper.Map<List<GetUserDTO>>(listUser);

            return listUserDTO;
        }
    }
}
