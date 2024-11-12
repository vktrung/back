using AutoMapper;
using BussinessObject.DTO.GradeType;
using BussinessObject.Models;
using DataAccess.DataAccess;
using DataAccess.Repository.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace SystemAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GradeTypeController : ControllerBase
    {
        private IGradeTypeRepository _repository;
        public GradeTypeController(IGradeTypeRepository repository)
        {
            _repository = repository;
        }


        [HttpGet("GetAllGradeType")]
        public IActionResult GetAllGradeType() {
            try
            {
                var result = _repository.GetAllGradeType();
                return Ok(result);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpPost("CreateGradeType")]
        public IActionResult CreateGradeType(CreateGradeTypeDTO gtDTO) {
            try
            {
                var result = _repository.CreateGradeType(gtDTO);
                return Ok(result);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }


        [HttpPatch("UpdateGradeType/{gradeTypeId}/{gradedByRole}/{newCcomparisonType}/{newGradeValue}")]
        public IActionResult UpdateGradeType(int gradeTypeId, int gradedByRole, string newCcomparisonType, int newGradeValue)
        {
            try
            {
                var result = _repository.UpdateGradeType(gradeTypeId, gradedByRole, newCcomparisonType, newGradeValue);
                return Ok(result);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }


        [HttpDelete("DeleteGradeType/{gradeTypeId}")]
        public IActionResult DeleteGradeType(int gradeTypeId)
        {
            try
            {
                var result = _repository.DeleteGradeType(gradeTypeId);
                return Ok(result);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpGet("GetDistribution/{gradeTypeId}/{courseId}")]

        public IActionResult GetDistribution(int gradeTypeId, int courseId)
        {
            try
            {
                var result = _repository.GetDistribution(gradeTypeId, courseId);
                return Ok(result);
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
