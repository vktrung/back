using BussinessObject.DTO.SessionStudent;
using DataAccess.Repository.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace SystemAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SessionStudentController : ControllerBase
    {
        private ISessionStudentRepository _repository;
        public SessionStudentController(ISessionStudentRepository repository)
        {
            _repository = repository;
        }

        [HttpGet("GetAvgGrade/{courseId}/{studentId}")]
        public IActionResult GetAvgGrade(int courseId, int studentId)
        {
            try
            {
                var result = _repository.GetAvgGrade(courseId, studentId);
                return Ok(result);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpGet("GetStatus/{courseId}/{studentId}")]
        public IActionResult GetStatus(int courseId, int studentId)
        {
            try
            {
                var result = _repository.GetStatus(courseId, studentId);
                return Ok(result);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpPost("AddStudentToSession")]
        public IActionResult AddStudentToSession([FromBody] SessionStudentDTO sessionStudentDTO)
        {
            if (sessionStudentDTO == null)
            {
                return BadRequest("Invalid data.");
            }

            bool isAdded = _repository.AddSessionStudent(sessionStudentDTO);

            if (isAdded)
            {
                return Ok("Student added successfully to the session.");
            }
            else
            {
                return BadRequest("Student already exists in this session.");
            }
        }

        [HttpGet("GetSessionStudents/{sessionId}")]
        public IActionResult GetSessionStudents(int sessionId)
        {
            var students = _repository.GetSessionStudentsBySessionId(sessionId);
            if (students == null || students.Count == 0)
            {
                return NotFound("No students found for the specified session.");
            }

            return Ok(students);
        }

    }
}
