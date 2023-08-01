using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace CinemaTic.Web.Controllers
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
