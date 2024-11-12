using AutoMapper;
using DataAccess.DataAccess;
using DataAccess.Repository.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace SystemAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RoleController : ControllerBase
    {

        private IRoleRepository _repository;
        public RoleController(IRoleRepository repository)
        {
            _repository = repository;
        }

        [HttpGet("GetRoleGraded")]
        public IActionResult GetRoleGraded() {
            try
            {
                var result = _repository.GetRoleGraded();
                return Ok(result);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
          
        }

    }
}
