using BussinessObject.DTO.Grade;
using BussinessObject.Models;
using DataAccess.Repository.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace SystemAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GradeController : ControllerBase
    {
        private IGradeRepository _repository;
        public GradeController(IGradeRepository repository)
        {
            _repository = repository;
        }

        [HttpGet("GetGradesBySessionGradedByTeacher/{sessionId}")]
        public IActionResult GetGradesBySessionGradedByTeacher(int sessionId)
        {
            try
            {
                var result = _repository.GetGradesBySessionGradedByTeacher(sessionId);
                return Ok(result);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpGet("GetGradesBySessionGradedByKhaoThi/{sessionId}")]
        public IActionResult GetGradesBySessionGradedByKhaoThi(int sessionId)
        {
            try
            {
                var result = _repository.GetGradesBySessionGradedByKhaoThi(sessionId);
                return Ok(result);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpGet("GetAllGradeGradedByKhaoThi")]
        public IActionResult GetGradesGradedByKhaoThi()
        {
            try
            {
                var result = _repository.GetGradesGradedByKhaoThi();
                return Ok(result);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
        
        [HttpPost("CreateGrade/{courseId}")]
        public IActionResult CreateGrade(int courseId, [FromBody] List<CreateGradeDTO> ListGDTO)
        {
            try
            {
                var resultDTO = _repository.CreateGrade(courseId, ListGDTO);
                return Ok(resultDTO);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
    }
}
