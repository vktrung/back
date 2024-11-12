using BussinessObject.DTO.Class;
using DataAccess.Repository.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace SystemAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClassController : ControllerBase
    {
        private IClassRepository _classRepository;

        public ClassController(IClassRepository classRepository)
        {
            _classRepository = classRepository;
        }

        [HttpGet("GetAllClasses")]
        public IActionResult GetAllClasses()
        {
            try
            {
                var result = _classRepository.GetClasses();
                return Ok(result);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpPost("AddClass")]
        public IActionResult AddClass([FromBody] ClassDTO classDto)
        {
            try
            {
                bool isAdded = _classRepository.AddClass(classDto);

                if (isAdded)
                    return Ok("Class added successfully.");
                else
                    return BadRequest("Failed to add class.");
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpGet("ExistsByName/{name}")]
        public async Task<IActionResult> ExistsByName(string name)
        {
            bool exists = await _classRepository.ExistsByNameAsync(name);
            return Ok(exists);
        }
    }
}
