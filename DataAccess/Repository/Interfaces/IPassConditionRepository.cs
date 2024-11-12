using BussinessObject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Repository.Interfaces
{
    public interface IPassConditionRepository
    {
        int GetPassConditionId(int comparationTypeId, int gradeValue);
    }
}
