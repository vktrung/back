using AutoMapper;
using DataAccess.DataAccess;
using DataAccess.Repository.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace SystemAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private IUserRepository _repository;
        public UserController(IUserRepository repository)
        {
            _repository = repository;
        }


        [HttpGet("GetUser/{username}/{password}")]
        public IActionResult GetUser(string username, string password)
        {
            try
            {
                var result = _repository.GetUser(username, password);
                return Ok(result);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpGet("GetStudent/{username}")]
        public IActionResult GetStudent(string username)
        {
            try
            {
                var result = _repository.GetStudent(username);
                return Ok(result);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpGet("GetStudentInSession/{sessionId}")]
        public IActionResult GetStudentInSession(int sessionId)
        {
            try
            {
                var result = _repository.GetStudentInSession(sessionId);
                return Ok(result);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }


        [HttpGet("GetStudentByCourseId/{courseId}")]
        public IActionResult GetStudentByCourseId(int courseId)
        {
            try
            {
                var result = _repository.GetStudentByCourseId(courseId);
                return Ok(result);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpGet("GetUsersByRole/{roleId}")]
        public IActionResult GetUsersByRole(int roleId)
        {
            try
            {
                var result = _repository.GetUsersByRole(roleId);
                return Ok(result);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }



    }
}
