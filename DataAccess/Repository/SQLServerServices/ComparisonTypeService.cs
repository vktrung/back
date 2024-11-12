using AutoMapper;
using BussinessObject.DTO.ComparisonType;
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
    public class ComparisonTypeService : IComparisonTypeRepositorycs
    {
        private readonly prn231Context _context;
        private readonly IMapper _mapper;

        public ComparisonTypeService(prn231Context context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public List<ComparisonTypeDTO> GetComparisonType()
        {
            List<ComparisonType> result = _context.ComparisonTypes.ToList();

            List<ComparisonTypeDTO> resultDTO = _mapper.Map<List<ComparisonTypeDTO>>(result);
            return resultDTO;
        }
    }
}
