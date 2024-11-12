using BussinessObject.DTO.Session;
using BussinessObject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Repository.Interfaces
{
    public interface ISessionRepository
    {
        List<GetSessionDTO> GetSessionsByTeacherId(int teacherId);

        List<GetSessionDTO> GetSessionsByStudentId(int studentId);

        bool AddSession(SessionDTO sessionDto);

        List<GetSessionDTO> GetAllSessions();
    }
}
