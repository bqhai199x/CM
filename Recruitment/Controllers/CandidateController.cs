using Microsoft.AspNetCore.Mvc;
using Recruitment.Entities;

namespace Recruitment.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CandidateController : Controller
    {
        [HttpGet("GetAll")]
        public async Task<JsonResult> GetCandidateList()
        {
            List<Candidate> candidateList = new();
            return Json(candidateList);
        }
    }
}
