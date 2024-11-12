using BussinessObject.DTO.User;
using BussinessObject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Repository.Interfaces
{
    public interface IUserRepository
    {
        GetUserDTO GetStudent(string username);
        List<GetUserDTO> GetStudentInSession(int sessionId);
        //List<GetUserDTO> GetStudentByGradeId(int gradeId);
        List<GetUserDTO> GetStudentByCourseId(int courseId);
        List<GetUserDTO> GetUsersByRole(int roleId);
        GetUserDTO GetUser(string username, string password);
    }
}
