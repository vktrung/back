using BussinessObject.DTO.Course;
using BussinessObject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Repository.Interfaces
{
    public interface ICourseRepository
    {
        List<CourseDTO> GetAllCourses();
        bool DeleteGradeDistribution(int courseId);
        bool AddCourse(CourseDTO course);
    }
}
