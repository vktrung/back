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
    public class GradeService : IGradeRepository
    {
        private readonly prn231Context _context;
        private readonly IMapper _mapper;

        public GradeService(prn231Context context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }


        public ResultForCreateGradeDTO CreateGrade(int courseId, List<CreateGradeDTO> ListGDTO)
        {
            ResultForCreateGradeDTO result = new ResultForCreateGradeDTO();

            bool courseIsDistributed = _context.Grades.Any(g => g.CourseId == courseId);
            if (courseIsDistributed)
            {
                string courseName = _context.Courses.FirstOrDefault(c => c.Id == courseId).Code;
                result.IsSuccess = false;
                result.NumberCreated = 0;
                result.Msg = $"Distribute fail. Course {courseName} already Distributed";
                return result;
            }

            // check xem co trung GradeType không
            int length = ListGDTO.Count();
            for (int i = 0; i < length - 1; i++)
            {
                for (int j = i + 1; j < length; j++)
                {
                    if (ListGDTO[i].GradeTypeId == ListGDTO[j].GradeTypeId)
                    {
                        result.IsSuccess = false;
                        result.NumberCreated = 0;
                        result.Msg = "Distribute fail. Duplicate GradeType";
                        return result;
                    }
                }
            }

            // check xem tong weight cua cac dau diem du 100 hay chưa
            int totalWeight = 0;
            foreach (var gradeDTO in ListGDTO)
            {
                totalWeight += gradeDTO.WeightPerGrade * gradeDTO.NumberOfGrade;
            }
            if (totalWeight != 100)
            {
                result.IsSuccess = false;
                result.NumberCreated = 0;
                result.Msg = "Distribute fail. Total Weigh is not equal to 100";
                return result;
            }

            List<Grade> listG = new List<Grade>();
            foreach (var gradeDTO in ListGDTO)
            {
                int numberOfGrade = gradeDTO.NumberOfGrade;
                string gradeTypeName = _context.GradeTypes.FirstOrDefault(gt => gt.Id == gradeDTO.GradeTypeId).Name;
                for (int i = 1; i <= numberOfGrade; i++)
                {
                    Grade g = new Grade();
                    g.GradeTypeId = gradeDTO.GradeTypeId;
                    g.CourseId = courseId;
                    g.Weight = gradeDTO.WeightPerGrade;
                    if (numberOfGrade == 1)
                    {
                        g.Name = gradeTypeName;
                    }
                    else
                    {
                        g.Name = gradeTypeName + " " + i;
                    }

                    listG.Add(g);
                }
            }
            _context.Grades.AddRange(listG);
            result.IsSuccess = true;
            result.Msg = "Distribute Success";
            result.NumberCreated = _context.SaveChanges();

            return result;
        }

        public List<GetGradeDTO> GetGradesBySessionGradedByKhaoThi(int sessionId)
        {
            Session s = _context.Sessions.FirstOrDefault(s => s.Id == sessionId);

            List<Grade> listG = _context.Grades
                .Where(g => g.CourseId == s.CourseId && g.GradeType.GradedByRoleNavigation.RoleName.Equals("Phong Khao Thi"))
                .Include(g => g.GradeType)
                .Include(g => g.Course)
                .ToList();

            List<GetGradeDTO> listGDTO = _mapper.Map<List<GetGradeDTO>>(listG);

            return listGDTO;
        }

        public List<GetGradeDTO> GetGradesBySessionGradedByTeacher(int sessionId)
        {
            Session s = _context.Sessions.FirstOrDefault(s => s.Id == sessionId);

            List<Grade> listG = _context.Grades
                .Where(g => g.CourseId == s.CourseId && g.GradeType.GradedByRoleNavigation.RoleName.Equals("Teacher"))
                .Include(g => g.GradeType)
                .Include(g => g.Course)
                .ToList();

            List<GetGradeDTO> listGDTO = _mapper.Map<List<GetGradeDTO>>(listG);

            return listGDTO;
        }

        public List<GetGradeDTO> GetGradesGradedByKhaoThi()
        {
            List<int> listGradeTypeIdGradedByKhaoThi = _context.GradeTypes
                 .Where(gt => gt.GradedByRoleNavigation.RoleName.Equals("Phong Khao Thi"))
                 .Select(gt => gt.Id).ToList();

            List<Grade> listG = _context.Grades
                .Include(g => g.GradeType)
                .Include(g => g.Course)
                .Where(g => listGradeTypeIdGradedByKhaoThi.Contains(g.GradeTypeId))
                .ToList();

            List<GetGradeDTO> listGDTO = _mapper.Map<List<GetGradeDTO>>(listG);

            foreach (var gDTO in listGDTO)
            {
                // dem tong so sinh vien co hoc session co course co dau diem la gDTO
                List<int> listSessionId = _context.Sessions.Where(ss => ss.CourseId == gDTO.CourseId).Select(ss => ss.Id).ToList();

                List<int> listStudentId = _context.SessionStudents.Where(ssst => listSessionId.Contains(ssst.SessionId)).Select(ssst => ssst.StudentId).ToList();

                int total = listStudentId.Count;

                // dem so sinh vien da duoc cham diem
                int graded = 0;
                foreach (var studentId in listStudentId)
                {
                    var studentGradeFound = _context.StudentGrades.FirstOrDefault(sg => sg.StudentId == studentId && sg.GradeId == gDTO.Id);
                    if (studentGradeFound != null)
                    {
                        graded++;
                    }
                }
                gDTO.Completed = graded + "/" + total;
                if (total > 0) 
                {
                    gDTO.PercentCompleted = Math.Round((decimal)graded / total * 100, 2);
                }
                else
                {
                    gDTO.PercentCompleted = 0; 
                }
            }


            return listGDTO;
        }
    }
}
