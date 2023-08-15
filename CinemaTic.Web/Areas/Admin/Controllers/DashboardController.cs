using CinemaTic.Web.Areas.Admin.Controllers.BaseControllers;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace CinemaTic.Web.Areas.Admin.Controllers
{
    public class DashboardController : AdminController
    {
        public async Task<IActionResult> Index()
        {
            return View();
        }
    }
}
