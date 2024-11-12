using BussinessObject.DTO.GradeType;
using BussinessObject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Repository.Interfaces
{
    public interface IGradeTypeRepository
    {
        List<GetGradeTypeDTO> GetAllGradeType();
        bool CreateGradeType(CreateGradeTypeDTO gtDTO);
        bool UpdateGradeType(int gradeTypeId, int gradedByRole, string newCcomparisonType, int newGradeValue);
        bool DeleteGradeType(int gradeTypeId);
        GradeDistributionDTO GetDistribution (int gradeTypeId, int courseId);
    }
}
