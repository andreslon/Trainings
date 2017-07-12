using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Linq;

namespace Excelsior.API.Controllers.v0
{
    [Route("api/v0/[controller]")]
    [Authorize]
    public class IdentityController : Controller
    {
        
        [HttpGet]
        public IActionResult Get()
        {
            return Json(from c in User.Claims
                        select new { c.Type, c.Value });
        }
    }
}
