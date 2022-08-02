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

        // GET
        public IActionResult Create()
        {
            return View();
        }
        // POST
        [HttpPost]
        //** ValidateAntiForgeryToken is there to automatically inject a key and it's validated at start so it helps with Forgery attack
        [ValidateAntiForgeryToken]
        public IActionResult Create(Category obj)
        {
            //** ModelState.IsValid checks if the model passes all requirements (required, etc)
            if (ModelState.IsValid)
            {
                _db.Categories.Add(obj);
                _db.SaveChanges();
                //** RedirectToAction it will find Index inside the same controller -> comma if want to specify other controller
                return RedirectToAction("Index");
            }
            return View(obj);
        }
    }
}
