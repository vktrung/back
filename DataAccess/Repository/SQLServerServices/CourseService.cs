using AutoMapper;
using BussinessObject.DTO.Course;
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
    public class CourseService : ICourseRepository
    {
        private readonly prn231Context _context;
        private readonly IMapper _mapper;

        public CourseService(prn231Context context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public bool DeleteGradeDistribution(int courseId)
        {
            try
            {
                List<Grade> listGradeToDelete = _context.Grades.Where(g => g.CourseId == courseId).ToList();
                _context.Grades.RemoveRange(listGradeToDelete);
                _context.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public List<CourseDTO> GetAllCourses()
        {
            var result = _context.Courses.ToList();
            var resultDTO = _mapper.Map<List<CourseDTO>>(result);
            return resultDTO;
        }

        public bool AddCourse(CourseDTO course)
        {
            try
            {
                var newCourse = _mapper.Map<Course>(course);

                _context.Courses.Add(newCourse);
                _context.SaveChanges();

                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}
