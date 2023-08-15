using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;

namespace CinemaTic.Web.Areas.Owner.Controllers.BaseControllers
{
    [Area("Owner")]
    [Authorize(Roles = "Owner")]
    public class OwnerController : Controller
    {
    }
}
