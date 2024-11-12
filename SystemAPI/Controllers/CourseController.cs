using BussinessObject.DTO.Course;
using DataAccess.Repository.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace SystemAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CourseController : ControllerBase
    {
        private ICourseRepository _repository;
        public CourseController(ICourseRepository repository)
        {
            _repository = repository;
        }

        [HttpGet("GetAllCourse")]
        public IActionResult GetAllCourse()
        {
            try
            {
                var result = _repository.GetAllCourses();
                return Ok(result);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }

        }

        [HttpDelete("DeleteGradeDistribution/{courseId}")]

        public IActionResult DeleteCourse(int courseId)
        {
            try
            {
                var result = _repository.DeleteGradeDistribution(courseId);
                return Ok(result);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpPost("AddCourse")]
        public IActionResult AddCourse([FromBody] CourseDTO course)
        {
            try
            {
                var result = _repository.AddCourse(course);
                if (result)
                {
                    return Ok("Course added successfully.");
                }
                else
                {
                    return BadRequest("Failed to add course.");
                }
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }


    }
}
