using AutoMapper;
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
    public class PassConditionService : IPassConditionRepository
    {
        private readonly prn231Context _context;
        private readonly IMapper _mapper;

        public PassConditionService(prn231Context context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }


        public int GetPassConditionId(int comparationTypeId, int gradeValue)
        {
            PassCondition pc = _context.PassConditions
                .FirstOrDefault(pc => pc.ComparisonTypeId == comparationTypeId && pc.GradeValue == gradeValue);
            return pc.Id;
        }
    }
}
