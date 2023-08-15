using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Data;

namespace CinemaTic.Web.Areas.Admin.Controllers.BaseControllers
{
    [Area("Admin")]
    [Authorize(Roles = "Administrator")]
    public class AdminController : Controller
    {
    }
}
