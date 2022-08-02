using BulkyBookCompleteWeb.Data;
using Microsoft.AspNetCore.Mvc;

namespace BulkyBookCompleteWeb.Controllers
{
    public class CategoryController : Controller
    {

        //** Create private readonly ApplicationDBContext variable
        private readonly ApplicationDBContext _db;

        //** Assign db that we got to the _db variable
        public CategoryController(ApplicationDBContext db)
        {
            _db = db;
        }

        public IActionResult Index()
        {
            //** Retrieve all Category objects in list from db and assign it to objCategoryList variable
            var objCategoryList = _db.Categories.ToList();
            return View();
        }
    }
}
