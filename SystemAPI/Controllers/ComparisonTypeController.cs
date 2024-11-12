using DataAccess.Repository.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace SystemAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ComparisonTypeController : ControllerBase
    {
        private IComparisonTypeRepositorycs _repository;
        public ComparisonTypeController(IComparisonTypeRepositorycs repository)
        {
            _repository = repository;
        }

        [HttpGet("GetComparisonTypes")]
        public IActionResult GetRoleGraded()
        {
            try
            {
                var result = _repository.GetComparisonType();
                return Ok(result);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
            
        }
    }
}
