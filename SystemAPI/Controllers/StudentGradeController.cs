using DataAccess.Repository.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;

namespace SystemAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentGradeController : ControllerBase
    {
        private IStudentGradeRepository _repository;
        public StudentGradeController(IStudentGradeRepository repository)
        {
            _repository = repository;
        }

        [HttpPost("GradedForStudent/{gradeId}/{studentId}/{value}")]
        public async Task<IActionResult> GradedForStudent(int gradeId, int studentId, decimal value)
        {
            try
            {
                var result = _repository.GradedForStudent(gradeId, studentId, value);
                return Ok(result);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpPatch("UpdateGradeForStudent/{gradeId}/{studentId}/{value}")]

        public IActionResult UpdateGradeForStudent(int gradeId, int studentId, decimal value)
        {
            try
            {
                var result = _repository.UpdateGradeForStudent(gradeId, studentId, value);
                return Ok(result);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpGet("ViewGrade/{studentId}/{courseid}")]

        public IActionResult ViewGrade(int studentId, int courseid)
        {
            try
            {
                var result = _repository.ViewGrade(studentId, courseid);
                return Ok(result);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpGet("CheckStudentGradeExist/{gradeId}/{studentId}")]

        public IActionResult CheckStudentGradeExist(int gradeId, int studentId)
        {
            try
            {
                var result = _repository.CheckStudentGradeExist(gradeId, studentId);
                return Ok(result);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }


        [HttpDelete("DeleteStudentGrade/{gradeId}/{studentId}")]

        public IActionResult DeleteStudentGrade(int gradeId, int studentId)
        {
            try
            {
                var result = _repository.DeleteStudentGrade(gradeId, studentId);
                return Ok(result);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }


        [HttpGet("GetGradeForStudentByGradeId/{gradeId}/{studentId}")]

        public IActionResult GetGradeForStudentByGradeId(int gradeId, int studentId)
        {
            try
            {
                var result = _repository.GetGradeForStudentByGradeId(gradeId, studentId);
                return Ok(result);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }



    }
}
