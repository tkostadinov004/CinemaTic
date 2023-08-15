using CinemaTic.Web.Areas.Owner.Controllers.BaseControllers;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace CinemaTic.Web.Areas.Owner.Controllers
{
    public class DashboardController : OwnerController
    {
        public async Task<IActionResult> Index()
        {
            return View();
        }
    }
}
