using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CinemaTic.Web.Areas.Customer.Controllers.BaseControllers
{
    [Area("Customer")]
    [Authorize(Roles = "Customer")]
    public class CustomerController : Controller
    {
    }
}
