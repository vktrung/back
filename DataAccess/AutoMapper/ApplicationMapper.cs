using AutoMapper;
using BussinessObject.DTO.Class;
using BussinessObject.DTO.ComparisonType;
using BussinessObject.DTO.Course;
using BussinessObject.DTO.Grade;
using BussinessObject.DTO.GradeType;
using BussinessObject.DTO.Role;
using BussinessObject.DTO.Session;
using BussinessObject.DTO.SessionStudent;
using BussinessObject.DTO.StudentGrade;
using BussinessObject.DTO.User;
using BussinessObject.Models;
using DataAccess.Repository.Interfaces;
using DataAccess.Repository.SQLServerServices;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.AutoMapper
{
    public class ApplicationMapper : Profile
    {
        public ApplicationMapper()
        {

            CreateMap<SessionStudent, SessionStudentDTO>()
                .ForMember(dest => dest.SessionId, opt => opt.MapFrom(src => src.SessionId))
                .ForMember(dest => dest.StudentId, opt => opt.MapFrom(src => src.StudentId))
                .ReverseMap();

            CreateMap<GradeType, CreateGradeTypeDTO>()
               .ForMember(dest => dest.ComparasionTypeId, otp => otp.MapFrom(src => src.PassCondition.ComparisonTypeId))
               .ForMember(dest => dest.GradeValue, otp => otp.MapFrom(src => src.PassCondition.GradeValue))
               .ReverseMap();

            CreateMap<Role, RoleDTO>().ReverseMap();

            CreateMap<Class, ClassDTO>().ReverseMap();

            CreateMap<ComparisonType, ComparisonTypeDTO>().ReverseMap();

            CreateMap<GradeType, GetGradeTypeDTO>()
                .ForMember(desc => desc.GradedByRoleId, otp => otp.MapFrom(src => src.GradedByRole))
                .ForMember(desc => desc.GradedByRoleName, otp => otp.MapFrom(src => src.GradedByRoleNavigation.RoleName))
                .ForMember(desc => desc.ComparisonType, otp => otp.MapFrom(src => src.PassCondition.ComparisonType.Name))
                .ForMember(desc => desc.ComparisonValue, otp => otp.MapFrom(src => src.PassCondition.GradeValue))

                .ReverseMap();

            CreateMap<Course, CourseDTO>().ReverseMap();

            //  CreateMap<Grade, CreateGradeDTO>().ReverseMap();

            CreateMap<Session, GetSessionDTO>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.CourseId, opt => opt.MapFrom(src => src.CourseId))
                .ForMember(dest => dest.CourseName, opt => opt.MapFrom(src => src.Course.Name))
                .ForMember(dest => dest.ClassId, opt => opt.MapFrom(src => src.ClassId))
                .ForMember(dest => dest.ClassName, opt => opt.MapFrom(src => src.Class.Name))
                .ForMember(dest => dest.TeahcerId, opt => opt.MapFrom(src => src.TeahcerId))
                .ForMember(dest => dest.TeacherName, opt => opt.MapFrom(src => src.Teahcer.Username))
                .ReverseMap();

            CreateMap<User, GetUserDTO>().ReverseMap();

            CreateMap<Grade, GetGradeDTO>()
                .ForMember(desc => desc.GradeTypeName, otp => otp.MapFrom(src => src.GradeType.Name))
                .ForMember(desc => desc.CourseName, otp => otp.MapFrom(src => src.Course.Code))
                .ReverseMap();


            CreateMap<Grade, GradeDTO>()
                .ForMember(dest => dest.GradeId, otp => otp.MapFrom(src => src.Id))
                .ForMember(dest => dest.GradeName, otp => otp.MapFrom(src => src.Name))
                .ForMember(dest => dest.Weight, otp => otp.MapFrom(src => src.Weight))
                .ReverseMap();

            CreateMap<GradeType, GradeTypeDTO>()
                .ForMember(desc => desc.GradeTypeId, otp => otp.MapFrom(src => src.Id))
                .ForMember(desc => desc.GradeTypeName, otp => otp.MapFrom(src => src.Name))
                .ReverseMap();

            CreateMap<StudentViewGradeDTO, StudentViewGradeDTO>()
                .ReverseMap();

            CreateMap<SessionDTO, Session>()
                .ForMember(dest => dest.CourseId, opt => opt.MapFrom(src => src.CourseId))
                .ForMember(dest => dest.TeahcerId, opt => opt.MapFrom(src => src.TeacherId))
                .ForMember(dest => dest.ClassId, opt => opt.MapFrom(src => src.ClassId))
                .ForMember(dest => dest.Id, opt => opt.Ignore())
                .ForMember(dest => dest.SessionStudents, opt => opt.Ignore())
                .ForMember(dest => dest.Class, opt => opt.Ignore())
                .ForMember(dest => dest.Course, opt => opt.Ignore())
                .ForMember(dest => dest.Teahcer, opt => opt.Ignore());
        }
    }
}
