using AutoMapper;
using BussinessObject.DTO.GradeType;
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
    public class GradeTypeService : IGradeTypeRepository
    {
        private readonly prn231Context _context;
        private readonly IMapper _mapper;

        public GradeTypeService(prn231Context context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public bool CreateGradeType(CreateGradeTypeDTO gtDTO)
        {
            bool isNameExists = _context.GradeTypes.Any(gt => gt.Name.ToLower().Trim() == gtDTO.Name.ToLower().Trim());
            if (isNameExists)
            {
                return false;
            }

            GradeType gt = _mapper.Map<GradeType>(gtDTO);
            gt.Name = gt.Name.Trim();


            var passCondition = _context.PassConditions
    .FirstOrDefault(pc => pc.ComparisonTypeId == gt.PassCondition.ComparisonTypeId && pc.GradeValue == gt.PassCondition.GradeValue);

            if (passCondition != null)
            {
                gt.PassConditionId = passCondition.Id;
            }
            gt.PassCondition = null;

            _context.Add(gt);
            return _context.SaveChanges() == 1;
        }

        public bool DeleteGradeType(int gradeTypeId)
        {
            GradeType gradeTyoeToDelete = _context.GradeTypes.FirstOrDefault(gt => gt.Id == gradeTypeId);
            if (gradeTyoeToDelete != null)
            {
                _context.GradeTypes.Remove(gradeTyoeToDelete);
                return _context.SaveChanges() == 1;
            }
            else
            {
                return false;
            }
        }

        public List<GetGradeTypeDTO> GetAllGradeType()
        {
            List<GradeType> result = _context.GradeTypes
                .Include(gt => gt.GradedByRoleNavigation)
                .Include(gt => gt.PassCondition)
                .ThenInclude(pc => pc.ComparisonType)
                .ToList();
            List<GetGradeTypeDTO> resultDTO = _mapper.Map<List<GetGradeTypeDTO>>(result);
            return resultDTO;
        }

        public GradeDistributionDTO GetDistribution(int gradeTypeId, int courseId)
        {
            GradeDistributionDTO distribution = new GradeDistributionDTO();

            if (_context.Grades.FirstOrDefault(g => g.CourseId == courseId && g.GradeTypeId == gradeTypeId) == null) {
                return null;
            }

            distribution.quantityInGradeType = _context.Grades.Where(g => g.CourseId == courseId && g.GradeTypeId == gradeTypeId).ToList().Count;

            distribution.weight = _context.Grades.FirstOrDefault(g => g.CourseId == courseId && g.GradeTypeId == gradeTypeId).Weight * distribution.quantityInGradeType;

            return distribution;

        }

        public bool UpdateGradeType(int gradeTypeId, int gradedByRole, string newCcomparisonType, int newGradeValue)
        {
            GradeType gt = _context.GradeTypes.FirstOrDefault(gt => gt.Id == gradeTypeId);
            if (gt != null)
            {

                PassCondition p = _context.PassConditions.FirstOrDefault(p => p.ComparisonType.Name.Equals(newCcomparisonType) && p.GradeValue == newGradeValue);
                if (p != null)
                {
                    gt.PassConditionId = p.Id;

                    gt.GradedByRole = gradedByRole;

                    //return _context.SaveChanges() == 1;
                    _context.SaveChanges();
                    return true;
                }
            }
            return false;
        }
    }
}
