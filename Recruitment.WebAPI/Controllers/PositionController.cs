using Microsoft.AspNetCore.Mvc;
using Recruitment.Core.Entities;
using Recruitment.Infrastructure.Interfaces;

namespace Recruitment.WebAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PositionController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;

        public PositionController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        [HttpGet("list")]
        public async Task<IActionResult> GetPositionList()
        {
            try
            {
                List<Position> positionlist = await _unitOfWork.Position.GetAllAsync();
                return Json(positionlist);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpGet("get")]
        public async Task<IActionResult> GetPosition(int id)
        {
            try
            {
                Position position = await _unitOfWork.Position.GetByIdAsync(id);
                return Json(position);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpPost("delete")]
        public async Task<IActionResult> DeletePosition(List<int> idList)
        {
            try
            {
                int result = await _unitOfWork.Position.DeleteManyAsync(idList);
                return Json(result);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpPost("add")]
        public async Task<IActionResult> AddPosition(Position position)
        {
            try
            {
                int result = await _unitOfWork.Position.AddAsync(position);
                return Json(result > 0);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpPost("update")]
        public async Task<IActionResult> UpdatePosition(Position position)
        {
            try
            {
                int result = await _unitOfWork.Position.UpdateAsync(position);
                return Json(result > 0);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
    }
}
