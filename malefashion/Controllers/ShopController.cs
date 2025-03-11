using Microsoft.AspNetCore.Mvc;

namespace malefashion.Controllers
{
    public class ShopController : Controller
    {
        public IActionResult Shop()
        {
            return View();
        }
    }
}
