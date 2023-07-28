using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace Cinema.Controllers
{
    public class ErrorController : Controller
    {
        [HttpGet("statuscode={code}")]
        [Route("Error/statuscode={code}")]
        public IActionResult Index(HttpStatusCode code)
        {
            return View(code);
        }
    }
}
