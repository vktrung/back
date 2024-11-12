using DataAccess.Repository.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace SystemAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SemesterController : ControllerBase
    {

        private ISemesterRepository _repository;
        public SemesterController(ISemesterRepository repository)
        {
            _repository = repository;
        }

        [HttpGet("IsSemesterOnGoing")]

        public IActionResult IsSemesterOnGoing()
        {
            try
            {
                bool result = _repository.IsOnGoing();
                return Ok(result);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpPut("ChangeSemesterStatus")]
        public IActionResult ChangeSemesterStatus()
        {
            try
            {
                string result = _repository.ChangeSemesterStatus();
                return Ok(result);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
    }
}
