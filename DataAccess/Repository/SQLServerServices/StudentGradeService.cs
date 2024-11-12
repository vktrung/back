using AutoMapper;
using BussinessObject.DTO.Grade;
using BussinessObject.DTO.StudentGrade;
using BussinessObject.DTO.User;
using BussinessObject.Models;
using DataAccess.DataAccess;
using DataAccess.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace DataAccess.Repository.SQLServerServices
{
    public class StudentGradeService : IStudentGradeRepository
    {
        private readonly prn231Context _context;
        private readonly IMapper _mapper;

        public StudentGradeService(prn231Context context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public bool CheckStudentGradeExist(int gradeId, int studentId)
        {
            StudentGrade sg = _context.StudentGrades.FirstOrDefault(sg => sg.GradeId == gradeId && sg.StudentId == studentId);
            if (sg == null)
            {
                return false;
            }
            return true;
        }

        public bool DeleteStudentGrade(int gradeId, int studentId)
        {
            StudentGrade sg = _context.StudentGrades.FirstOrDefault(sg => sg.GradeId == gradeId && sg.StudentId == studentId);
            if (sg != null)
            {
                _context.Remove(sg);
                return _context.SaveChanges() == 1;
            }
            return false;
        }

        public bool GradedForStudent(int gradeId, int studentId, decimal value)
        {
            StudentGrade sg = new StudentGrade();
            sg.StudentId = studentId;
            sg.GradeId = gradeId;
            sg.Value = value;
            _context.StudentGrades.Add(sg);

            return _context.SaveChanges() == 1;

        }

        public bool UpdateGradeForStudent(int gradeId, int studentId, decimal newValue)
        {
            StudentGrade sg = _context.StudentGrades.FirstOrDefault(sg => sg.GradeId == gradeId && sg.StudentId == studentId);
            if (sg != null)
            {
                sg.Value = newValue;
                return _context.SaveChanges() == 1; ;
            }
            else
            {
                return false;
            }
        }

        public StudentViewGradeDTO ViewGrade(int studentId, int courseId)
        {
            StudentViewGradeDTO dto = new StudentViewGradeDTO();

            List<GradeType> listGradeTypeInCourse = _context.GradeTypes
                   .Where(gt => gt.Grades.Select(g => g.CourseId).Contains(courseId)).ToList();
            List<GradeTypeDTO> listGradeTypeDTOInCourse = _mapper.Map<List<GradeTypeDTO>>(listGradeTypeInCourse);

            foreach (var gradeTypeDTO in listGradeTypeDTOInCourse)
            {
                //List<StudentGrade> listGradeInGradeType = _context.StudentGrades
                //    .Include(sg => sg.Grade)
                //    .Where(sg => sg.StudentId == studentId
                //            && sg.Grade.CourseId == courseId
                //            && sg.Grade.GradeTypeId == gradeTypeDTO.GradeTypeId)
                //    .ToList();
                List<Grade> listGradeInGradeType = _context.Grades.Where(g => g.GradeTypeId == gradeTypeDTO.GradeTypeId && g.CourseId == courseId).ToList();

                List<GradeDTO> listGradeDTOInGradeType = _mapper.Map<List<GradeDTO>>(listGradeInGradeType);
                foreach (var gradeDTO in listGradeDTOInGradeType)
                {
                    StudentGrade sg = _context.StudentGrades.FirstOrDefault(sg => sg.StudentId == studentId && sg.GradeId == gradeDTO.GradeId);
                    if (sg == null)
                    {
                        gradeDTO.Value = "No data";
                    }
                    else
                    {
                        gradeDTO.Value = sg.Value.ToString();
                    }
                }
                gradeTypeDTO.Grades = listGradeDTOInGradeType;
            }

            dto.StudentId = studentId;
            dto.StudentName = _context.Users.FirstOrDefault(u => u.Id == studentId).Username;

            dto.CourseId = courseId;
            dto.CourseCode = _context.Courses.FirstOrDefault(c => c.Id == courseId).Code;
            dto.CourseName = _context.Courses.FirstOrDefault(c => c.Id == courseId).Name;

            dto.GradeTypes = listGradeTypeDTOInCourse;
            return dto;
        }

        GetGradeForStudentDTO IStudentGradeRepository.GetGradeForStudentByGradeId(int gradeId, int studentId)
        {
            GetGradeForStudentDTO result = new GetGradeForStudentDTO();

            StudentGrade studentGradeFound = _context.StudentGrades.FirstOrDefault(sg => sg.StudentId == studentId && sg.GradeId == gradeId);
            if (studentGradeFound != null)
            {
                result.GradeValue = studentGradeFound.Value.ToString();
            }
            else
            {
                result.GradeValue = "No data";
            }
            return result;
        }
    }


    //public TeacherViewGradeDTO GetStudentGradesInSession(int sessionId)
    //{
    //    TeacherViewGradeDTO result = new TeacherViewGradeDTO();

    //    // Lấy danh sách sinh viên trong session
    //    List<int> listStudentId = _context.SessionStudents
    //        .Where(ss => ss.SessionId == sessionId)
    //        .Select(ss => ss.StudentId)
    //        .ToList();

    //    List<User> listStudent = _context.Users
    //        .Where(u => listStudentId.Contains(u.Id))
    //        .ToList();

    //    List<GetUserDTO> listStudentDTO = _mapper.Map<List<GetUserDTO>>(listStudent);

    //    result.ListStudent = listStudentDTO;

    //    // Lấy danh sách các điểm  trong session
    //    int courrseId = _context.Sessions.FirstOrDefault(ss => ss.Id == sessionId).CourseId;
    //    List<Grade> listGrade = _context.Grades.Where(g => g.CourseId == courrseId).ToList();

    //    List<GetGradeDTO> listGradeDTO = _mapper.Map<List<GetGradeDTO>>(listGrade);

    //    result.ListGrade = listGradeDTO;


    //    result.GradeTable = new string[listStudent.Count][];
    //    for (int i = 0; i < listStudent.Count; i++)
    //    {
    //        result.GradeTable[i] = new string[listGrade.Count];
    //    }


    //    for (int i = 0; i < listStudent.Count; i++)
    //    {
    //        for (int j = 0; j < listGrade.Count; j++)
    //        {
    //            result.GradeTable[i][j] = GetGradeValue(listStudent[i].Id, listGrade[j].Id);
    //        }
    //    }


    //    return result;
    //}
    //public string GetGradeValue(int studentId, int gradeId) { 
    //    string result = _context.StudentGrades.FirstOrDefault(sg => sg.StudentId == studentId && sg.GradeId == gradeId).Value.ToString();

    //    return result;
    //}


    //public List<StudentGradesDTO> GetStudentGradesInSession(int sessionId)
    //{
    //    // Lấy danh sách sinh viên trong session
    //    List<int> listStudentId = _context.SessionStudents
    //        .Where(ss => ss.SessionId == sessionId)
    //        .Select(ss => ss.StudentId)
    //        .ToList();

    //    List<User> listStudent = _context.Users
    //        .Where(u => listStudentId.Contains(u.Id))
    //        .ToList();

    //    // Lấy danh sách các điểm của sinh viên trong session
    //    var studentGrades = _context.StudentGrades
    //        .Where(sg => listStudentId.Contains(sg.StudentId))
    //        .Include(sg => sg.Grade)
    //        .ThenInclude(g => g.GradeType)
    //        .ToList();

    //    // Tạo Dictionary để tra cứu điểm theo sinh viên
    //    var gradesDict = studentGrades
    //        .GroupBy(sg => sg.StudentId)
    //        .ToDictionary(
    //            g => g.Key,
    //            g => g.ToDictionary(
    //                sg => sg.Grade.Name, 
    //                sg => new GradeDetail
    //                {
    //                    GradeId = sg.Grade.Id,
    //                    GradeValue = sg.Value
    //                }
    //            )
    //        );

    //    // Tạo danh sách kết quả
    //    List<StudentGradesDTO> result = new List<StudentGradesDTO>();
    //    foreach (var student in listStudent)
    //    {
    //        var studentGradesDTO = new StudentGradesDTO
    //        {
    //            StudentId = student.Id,
    //            StudentName = student.Username, 
    //            Grades = gradesDict.ContainsKey(student.Id) ? gradesDict[student.Id] : new Dictionary<string, GradeDetail>()
    //        };

    //        result.Add(studentGradesDTO);
    //    }

    //    return result;
    //}
}
