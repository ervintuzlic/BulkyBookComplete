using BulkyBookCompleteWeb.Data;
using BulkyBookCompleteWeb.Models;
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
            //** Retrieve all Category objects in list from db and assign it to IEnumerable<Category>
            IEnumerable<Category> objCategoryList = _db.Categories;
            return View(objCategoryList);
        }
    }
}
