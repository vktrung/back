using BussinessObject.DTO.StudentGrade;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Repository.Interfaces
{
    public interface IStudentGradeRepository
    {
        bool CheckStudentGradeExist(int gradeId, int studentId);
        bool DeleteStudentGrade(int gradeId, int studentId);
        bool GradedForStudent(int gradeId, int studentId, decimal value);

        bool UpdateGradeForStudent(int gradeId, int studentId, decimal newValue);

        GetGradeForStudentDTO GetGradeForStudentByGradeId(int gradeId, int studentId);

        StudentViewGradeDTO ViewGrade(int studentId, int courseId);

      //  TeacherViewGradeDTO GetStudentGradesInSession(int sessionId);

       

    }
}
