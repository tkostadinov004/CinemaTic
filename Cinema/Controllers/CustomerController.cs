using Microsoft.AspNetCore.Mvc;

namespace Cinema.Controllers
{
    public class CustomerController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
