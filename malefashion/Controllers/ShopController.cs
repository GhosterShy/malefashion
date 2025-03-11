using Microsoft.AspNetCore.Mvc;

namespace malefashion.Controllers
{
    public class ShopController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
