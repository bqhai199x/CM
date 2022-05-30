using Microsoft.AspNetCore.Mvc;
using Recruitment.Core.Entities;
using Recruitment.Infrastructure.Interfaces;

namespace Recruitment.WebAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class LevelController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;

        public LevelController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        [HttpGet("GetAll")]
        public async Task<IActionResult> GetCandidateList()
        {
            try
            {
                List<Level> levelList = await _unitOfWork.Level.GetAllAsync();
                return Json(levelList);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
    }
}
