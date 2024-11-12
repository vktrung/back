using BussinessObject.DTO.Grade;
using BussinessObject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Repository.Interfaces
{
    public interface IGradeRepository
    {
        ResultForCreateGradeDTO CreateGrade(int courseId, List<CreateGradeDTO> ListGDTO);

        List<GetGradeDTO> GetGradesBySessionGradedByTeacher(int sessionId);

        List<GetGradeDTO> GetGradesBySessionGradedByKhaoThi(int sessionId);

        List<GetGradeDTO> GetGradesGradedByKhaoThi();
    }
}
