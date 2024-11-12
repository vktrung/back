using AutoMapper;
using BussinessObject.DTO.Session;
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
    public class SessionService : ISessionRepository
    {
        private readonly prn231Context _context;
        private readonly IMapper _mapper;

        public SessionService(prn231Context context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public List<GetSessionDTO> GetSessionsByTeacherId(int id)
        {
            List<Session> result = _context.Sessions
                .Include(s => s.Class)
                .Include(s => s.Course)
                .Include(s => s.Teahcer)
                .Where(s => s.TeahcerId == id).ToList();

            List<GetSessionDTO> resultDTO = _mapper.Map<List<GetSessionDTO>>(result);
            return resultDTO;
        }

        public List<GetSessionDTO> GetSessionsByStudentId(int studentId)
        {
            List<int> ListSessionId = _context.SessionStudents
                .FromSqlRaw($"SELECT sessionId FROM SessionStudent WHERE studentId = {studentId}")
                .Select(ss => ss.SessionId)
                .ToList();

            List<Session> result = _context.Sessions
                .Include(s => s.Class)
                .Include(s => s.Course)
                .Include(s => s.Teahcer)
                .Where(s => ListSessionId.Contains(s.Id)).ToList();

            List<GetSessionDTO> resultDTO = _mapper.Map<List<GetSessionDTO>>(result);
            return resultDTO;
        }

        public bool AddSession(SessionDTO sessionDto)
        {
            try
            {
                bool sessionExists = _context.Sessions
                    .Any(s => s.ClassId == sessionDto.ClassId && s.CourseId == sessionDto.CourseId);

                if (sessionExists)
                {
                    return false;
                }

                var sessionEntity = _mapper.Map<Session>(sessionDto);
                _context.Sessions.Add(sessionEntity);
                _context.SaveChanges();

                return true;
            }
            catch
            {
                return false;
            }
        }


        public List<GetSessionDTO> GetAllSessions()
        {
            List<Session> result = _context.Sessions
                 .Include(s => s.Class)
                 .Include(s => s.Course)
                 .Include(s => s.Teahcer).ToList();

            List<GetSessionDTO> resultDTO = _mapper.Map<List<GetSessionDTO>>(result);
            return resultDTO;
        }
    }
}
