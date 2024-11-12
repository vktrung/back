using AutoMapper;
using BussinessObject.DTO.Class;
using BussinessObject.Models;
using DataAccess.DataAccess;
using DataAccess.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace DataAccess.Repository.SQLServerServices
{
    public class ClassService : IClassRepository
    {
        private readonly prn231Context _context;
        private readonly IMapper _mapper;

        public ClassService(prn231Context context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public bool AddClass(ClassDTO classDto)
        {
            try
            {
                var classEntity = _mapper.Map<Class>(classDto);

                _context.Classes.Add(classEntity);

                _context.SaveChanges();

                return true;
            }
            catch
            {
                return false;
            }
        }

        public async Task<bool> ExistsByNameAsync(string name)
        {
            return await _context.Classes.AnyAsync(c => c.Name == name);
        }

        public List<ClassDTO> GetClasses()
        {
            List<Class> result = _context.Classes.ToList();


            List<ClassDTO> resultDTO = _mapper.Map<List<ClassDTO>>(result);

            return resultDTO;
        }
    }
}
