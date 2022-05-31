using Microsoft.AspNetCore.Mvc;
using Recruitment.Core.Entities;
using Recruitment.Infrastructure.Interfaces;

namespace Recruitment.WebAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CandidateController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;

        public CandidateController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        [HttpGet("list")]
        public async Task<IActionResult> GetCandidateList()
        {
            try
            {
                List<Candidate> candidateList = await _unitOfWork.Candidate.GetAllAsync();
                return Json(candidateList);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpGet("get")]
        public async Task<IActionResult> GetCandidate(int id)
        {
            try
            {
                Candidate candidate = await _unitOfWork.Candidate.GetByIdAsync(id);
                return Json(candidate);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpPost("delete")]
        public async Task<IActionResult> DeleteCandidate(List<int> idList)
        {
            try
            {
                int result = await _unitOfWork.Candidate.DeleteManyAsync(idList);
                return Json(result);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpPost("add")]
        public async Task<IActionResult> AddCandidate(Candidate candidate)
        {
            try
            {
                int result = await _unitOfWork.Candidate.AddAsync(candidate);
                return Json(result > 0);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpPost("update")]
        public async Task<IActionResult> UpdateCandidate(Candidate candidate)
        {
            try
            {
                int result = await _unitOfWork.Candidate.UpdateAsync(candidate);
                return Json(result > 0);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpPost("search")]
        public async Task<IActionResult> SearchCandidate(Candidate.Condition condition)
        {
            try
            {
                List<Candidate> candidateList = await _unitOfWork.Candidate.SearchAsync(condition);
                return Json(candidateList);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
    }
}
