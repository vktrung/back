using AutoMapper;
using BussinessObject.DTO.SessionStudent;
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
    public class SessionStudentService : ISessionStudentRepository
    {

        private readonly prn231Context _context;
        private readonly IMapper _mapper;

        public SessionStudentService(prn231Context context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public decimal GetAvgGrade(int courseId, int studentId)
        {
            List<int> listGradeIdInCourse = _context.Grades
                .Where(c => c.CourseId == courseId)
                .Select(c => c.Id)
                .ToList();



            List<StudentGrade> listStudentGrade = _context.StudentGrades
                .Where(sg => listGradeIdInCourse.Contains(sg.GradeId) && sg.StudentId == studentId).ToList();


            if (listGradeIdInCourse.Count != listStudentGrade.Count)
            {
                return 0;
            }

            decimal totalGrade = 0;
            foreach (StudentGrade item in listStudentGrade)
            {
                totalGrade += (item.Value ?? 0) * GetWeight(item.GradeId);
            }

            decimal avgGrade = totalGrade / 100;
            return avgGrade;
        }



        public GetStatusDTO GetStatus(int courseId, int studentId)
        {
            GetStatusDTO result = new GetStatusDTO
            {
                isPass = false,
                msg = null
            };

            StringBuilder messageBuilder = new StringBuilder();

            // tìm ra những danh mục điểm có trong môn học (ví dụ PRN231: Progresstest, lab, PE)
            List<int> listGradeTypeIdInCourse = GetListGradeTypeIdInCourse(courseId);

            // lặp qua từng danh mục điểm
            foreach (var gradeTypeId in listGradeTypeIdInCourse)
            {
                // tìm điều kiện pass của từng danh mục điểm
                string comparasionType = GetComparasionType(gradeTypeId);
                int gradeValueCondition = GetPassconditionValue(gradeTypeId);

                // tìm ra có những điểm nào trong danh mục điểm ( ví dụ: danh mục Progresstest có 3 điểm: Progresstest 1-2-3)
                List<int> listGradeId = GetGradeIdInCourseByGradeType(courseId, gradeTypeId);
                if (listGradeId.Count == 0)
                {
                    continue;
                }


                decimal totalGradeValueInGradeType = 0;
                foreach (var gradeId in listGradeId)
                {
                    decimal gradeValue = GetGradeValue(gradeId, studentId);
                    totalGradeValueInGradeType += gradeValue;
                }
                // tìm ra điểm trung bình của danh mục điểm
                decimal avgGradeValueInGradeType = totalGradeValueInGradeType / listGradeId.Count();

                //check điểm tb của danh mục điểm với pass condition
                switch (comparasionType)
                {
                    case ">":
                        if (avgGradeValueInGradeType <= gradeValueCondition)
                        {
                            messageBuilder.Append(GetGradeTypeName(gradeTypeId) + ":" + avgGradeValueInGradeType + " <= " + gradeValueCondition + ". ");
                        }
                        break;
                    case ">=":
                        if (avgGradeValueInGradeType < gradeValueCondition)
                        {
                            messageBuilder.Append(GetGradeTypeName(gradeTypeId) + ":" + avgGradeValueInGradeType + " < " + gradeValueCondition + ". ");
                        }
                        break;
                }
            }
            // tìm điểm tb của môn
            decimal avgGrade = GetAvgGrade(courseId, studentId);

            if (avgGrade < 5)
            {
                messageBuilder.Append("Avg grade:" + avgGrade + " < 5.");
            }

            result.msg = messageBuilder.ToString();
            if (string.IsNullOrEmpty(result.msg))
            {
                result.isPass = true;
            }

            return result;
        }

        // tìm trọng số của một đầu điểm ( ví dụ: Progress test 1: 10%)
        public int GetWeight(int gradeId)
        {
            int weight = _context.Grades.FirstOrDefault(g => g.Id == gradeId).Weight;
            return weight;
        }

        // tìm ra có những danh mục điểm nào trong môn học
        public List<int> GetListGradeTypeIdInCourse(int courseId)
        {
            List<int> listGradeTypeIdInCourse = _context.Grades
                                                   .Where(c => c.CourseId == courseId)
                                                   .Select(c => c.GradeTypeId)
                                                   .Distinct()
                                                   .ToList();
            return listGradeTypeIdInCourse;
        }

        // tìm điểm số của sinh viên ở 1 đầu điểm
        public decimal GetGradeValue(int gradeId, int studentId)
        {
            StudentGrade sg = _context.StudentGrades.FirstOrDefault(g => g.GradeId == gradeId && g.StudentId == studentId);
            if (sg != null)
            {
                decimal value = (decimal)sg.Value;
                return value;
            }
            else
            {
                return 0;// nếu điểm chuawa được chấm, thì FirstOrDefault sẽ không tìm được, tạm tính gradeValue = 0
            }
        }

        // tìm kiểu so sánh > / >=
        public string GetComparasionType(int gradeTypeId)
        {
            GradeType gt = _context.GradeTypes.Include(gt => gt.PassCondition).ThenInclude(gt => gt.ComparisonType)
                .FirstOrDefault(gt => gt.Id == gradeTypeId);


            return gt.PassCondition.ComparisonType.Name;
        }

        // tìm giá trị pass
        public int GetPassconditionValue(int gradeTypeId)
        {
            int value = (int)_context.GradeTypes.FirstOrDefault(gt => gt.Id == gradeTypeId).PassCondition.GradeValue;
            return value;
        }

        // tìm ra một danh mục điểm có những đầu điểm nào
        public List<int> GetGradeIdInCourseByGradeType(int courseId, int gradeTypeId)
        {
            List<Grade> listGrade = _context.Grades
                .Where(g => g.CourseId == courseId && g.GradeTypeId == gradeTypeId).ToList();

            List<int> listGradeId = new List<int>();
            foreach (var item in listGrade)
            {
                listGradeId.Add(item.Id);
            }

            return listGradeId;
        }


        public string GetGradeTypeName(int gradeTypeId)
        {
            return _context.GradeTypes.FirstOrDefault(gt => gt.Id == gradeTypeId).Name;
        }

        public bool AddSessionStudent(SessionStudentDTO sessionStudentDTO)
        {
            // Kiểm tra xem học sinh đã tồn tại trong session chưa
            var existingSessionStudent = _context.SessionStudents
                .FirstOrDefault(ss => ss.SessionId == sessionStudentDTO.SessionId && ss.StudentId == sessionStudentDTO.StudentId);

            if (existingSessionStudent != null)
            {
                // Học sinh đã tồn tại trong session này
                return false;
            }

            // Thêm mới học sinh vào session
            var sessionStudent = new SessionStudent
            {
                SessionId = sessionStudentDTO.SessionId,
                StudentId = sessionStudentDTO.StudentId,
                AvgGragde = null,
                Status = null
            };

            _context.SessionStudents.Add(sessionStudent);
            _context.SaveChanges();
            return true;
        }

        public List<GetStudentBySession> GetSessionStudentsBySessionId(int sessionId)
        {
            return _context.SessionStudents
                 .Where(ss => ss.SessionId == sessionId)
                 .Select(ss => new GetStudentBySession
                 {
                     SessionId = ss.SessionId,
                     StudentId = ss.StudentId,
                     StudentName = ss.Student.Username // Lấy tên học sinh từ bảng User
                 })
                 .ToList();
        }
    }
}
