using Microsoft.AspNetCore.Mvc;

namespace BulkyBookCompleteWeb.Controllers
{
    public class CategoryController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
