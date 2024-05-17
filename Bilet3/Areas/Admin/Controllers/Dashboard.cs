using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Bilet3.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles ="Admin")]
    public class Dashboard : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
