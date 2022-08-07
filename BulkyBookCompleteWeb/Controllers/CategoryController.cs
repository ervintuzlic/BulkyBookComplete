using BulkyBookComplete.DataAccess;
using BulkyBookComplete.DataAccess.Repository.IRepository;
using BulkyBookComplete.Models;
using Microsoft.AspNetCore.Mvc;

namespace BulkyBookCompleteWeb.Controllers
{
    public class CategoryController : Controller
    {

        //** Create private readonly ApplicationDBContext variable
        private readonly IUnitOfWork _unitOfWork;

        //** Assign db that we got to the _db variable
        public CategoryController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public IActionResult Index()
        {
            //** Retrieve all Category objects in list from db and assign it to IEnumerable<Category>
            IEnumerable<Category> objCategoryList = _unitOfWork.Category.GetAll();
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
            if (obj.Name == obj.DisplayOrder.ToString())
            {
                //** ModelState.AddModelError -> Sets custom error message
                //** For each input it's important to name the attribute where we want our error mesage to show up
                ModelState.AddModelError("Name", "Display Order cannot be the same as Name");
            }

            //** ModelState.IsValid checks if the model passes all requirements (required, etc)
            if (ModelState.IsValid)
            {
                _unitOfWork.Category.Add(obj);
                //** SaveChanges is refreshing the db
                _unitOfWork.Save();
                //** RedirectToAction it will find Index inside the same controller -> comma if want to specify other controller
                TempData["success"] = "Category created successfully";
                return RedirectToAction("Index");
            }
            return View(obj);
        }


        // GET
        public IActionResult Edit(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }

            //** _db.Categories.Single => returns only one element that's found, if there are no elements that are being searched it will throw exception
            //** _db.Categories.SingleOrDefault => returns only one element every time if there are no elements it will return empty. It will throw exception if there are more than one element. 

            //** _db.Categories.First => If more than 1 elements are found only the first one will be returned
            //** _db.Categories.FirstOrDefault => If more than 1 elements are found only the first one will be returned

            //** _db.Categories.Find => Tries to find that based on Primary key of the table

            //** Pass expression where we have generic object u that goes to u.Id and if that matches with id it will return the first one.
            var categoryFromDb = _unitOfWork.Category.GetFirstOrDefault(x => x.Id == id);

            //** Pass expression where we have generic object u that goes to u.Id and if that matches with id it will return it.
            //** var categoryFromDb = _db.Categories.SingleOrDefault(u=>u.Id == id);

            //var categoryFromDb = _db.Categories.Find(id);

            if (categoryFromDb == null)
            {
                return NotFound();
            }

            return View(categoryFromDb);
        }

        // POST
        [HttpPost]
        //** ValidateAntiForgeryToken is there to automatically inject a key and it's validated at start so it helps with Forgery attack
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Category obj)
        {
            if (obj.Name == obj.DisplayOrder.ToString())
            {
                //** ModelState.AddModelError -> Sets custom error message
                //** For each input it's important to name the attribute where we want our error mesage to show up
                ModelState.AddModelError("Name", "Display Order cannot be the same as Name");
            }

            //** ModelState.IsValid checks if the model passes all requirements (required, etc)
            if (ModelState.IsValid)
            {
                //** No need manual update it will take a look at obj find it's primary key retrieve from db see what things change and update db
                _unitOfWork.Category.Update(obj);
                //** SaveChanges is refreshing the db
                _unitOfWork.Save();
                //** RedirectToAction it will find Index inside the same controller -> comma if want to specify other controller
                TempData["success"] = "Category updated successfully";
                return RedirectToAction("Index");
            }
            return View(obj);
        }


        // GET Remove
        public IActionResult Delete(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }

            //var categoryFromDb = _db.Categories.Find(id);

            var categoryFromDb = _unitOfWork.Category.GetFirstOrDefault(u => u.Id == id);

            if (categoryFromDb == null)
            {
                return NotFound();
            }

            return View(categoryFromDb);
        }


        // POST
        [HttpPost,ActionName("Delete")]
        //** ValidateAntiForgeryToken is there to automatically inject a key and it's validated at start so it helps with Forgery attack
        [ValidateAntiForgeryToken]
        public IActionResult DeletePOST(int? id)
        {
            var obj = _unitOfWork.Category.GetFirstOrDefault(u => u.Id == id);
            if (obj == null)
            {
                return NotFound();
            }

            _unitOfWork.Category.Remove(obj);
            _unitOfWork.Save();
            TempData["success"] = "Category deleted successfully";
            return RedirectToAction("Index");
        }
    }
}
