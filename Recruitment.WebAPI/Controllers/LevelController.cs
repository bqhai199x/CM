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

        [HttpGet("list")]
        public async Task<IActionResult> GetLevelList()
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

        [HttpGet("get")]
        public async Task<IActionResult> GetLevel(int id)
        {
            try
            {
                Level level = await _unitOfWork.Level.GetByIdAsync(id);
                return Json(level);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpPost("delete")]
        public async Task<IActionResult> DeleteLevel(List<int> idList)
        {
            try
            {
                int result = await _unitOfWork.Level.DeleteManyAsync(idList);
                return Json(result);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpPost("add")]
        public async Task<IActionResult> Addlevel(Level level)
        {
            try
            {
                int result = await _unitOfWork.Level.AddAsync(level);
                return Json(result > 0);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpPost("update")]
        public async Task<IActionResult> UpdateLevel(Level level)
        {
            try
            {
                int result = await _unitOfWork.Level.UpdateAsync(level);
                return Json(result > 0);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
    }
}
