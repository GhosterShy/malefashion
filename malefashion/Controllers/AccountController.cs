using Microsoft.AspNetCore.Mvc;

namespace malefashion.Controllers
{
    public class AccountController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
