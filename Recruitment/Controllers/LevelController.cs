using Microsoft.AspNetCore.Mvc;
using Recruitment.Entities;

namespace Recruitment.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class LevelController : Controller
    {

        [HttpGet("GetAll")]
        public async Task<JsonResult> GetCandidateList()
        {
            List<Level> levelList = new();
            return Json(levelList);
        }
    }
}
