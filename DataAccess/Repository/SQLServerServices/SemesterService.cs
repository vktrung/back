using AutoMapper;
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
    public class SemesterService : ISemesterRepository
    {

        private readonly prn231Context _context;
        private readonly IMapper _mapper;

        public SemesterService(prn231Context context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public string ChangeSemesterStatus()
        {
            _context.Database.ExecuteSqlRaw($"UPDATE Semester\r\nSET status = CASE\r\n    WHEN status = 1 THEN 0\r\n    WHEN status = 0 THEN 1\r\nEND;\r\n");

            if (IsOnGoing())
            {
                return "Semester is on going";
            }
            else
            {
                return "Semester is not started";
            }
        }

        public bool IsOnGoing()
        {
            bool result = _context.Semesters.Any(s => s.Status == true);
            return result;
        }

    }
}
