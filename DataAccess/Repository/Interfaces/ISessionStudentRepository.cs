using BussinessObject.DTO.SessionStudent;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Repository.Interfaces
{
    public interface ISessionStudentRepository
    {
        decimal GetAvgGrade(int courseId, int studentId);

        GetStatusDTO GetStatus(int courseId, int studentId);

        bool AddSessionStudent(SessionStudentDTO sessionStudentDTO);

        List<GetStudentBySession> GetSessionStudentsBySessionId(int sessionId);
    }
}
