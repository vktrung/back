using BussinessObject.DTO.Class;
using BussinessObject.DTO.ComparisonType;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Repository.Interfaces
{
    public interface IClassRepository
    {
        List<ClassDTO> GetClasses();
        bool AddClass(ClassDTO classDto);
        Task<bool> ExistsByNameAsync(string name);
    }
}
