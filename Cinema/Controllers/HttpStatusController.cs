using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace Cinema.Controllers
{
    public class HttpStatusController : Controller
    {
        [HttpGet("statuscode/{code}")]
        public IActionResult Index(HttpStatusCode code)
        {
            return View(code);
        }
    }
}
