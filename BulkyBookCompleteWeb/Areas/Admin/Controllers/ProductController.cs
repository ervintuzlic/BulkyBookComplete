using BulkyBookComplete.DataAccess.Repository.IRepository;
using BulkyBookComplete.Models;
using Microsoft.AspNetCore.Mvc;

namespace BulkyBookCompleteWeb.Controllers
{
    [Area("Admin")]
    public class ProductController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;

        public ProductController(IUnitOfWork obj)
        {
            _unitOfWork = obj;
        }

        public IActionResult Index()
        {
            IEnumerable<Product> ProductList = _unitOfWork.Product.GetAll();
            return View(ProductList);
        }

        //GET
        public IActionResult Create()
        {
            return View();
        }

        //POST 
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Product Product)
        {
            if (ModelState.IsValid)
            {
                _unitOfWork.Product.Add(Product);
                _unitOfWork.Save();
                TempData["success"] = "Cover Type created successfully!";
                return RedirectToAction("Index");
            }
            TempData["error"] = "Cover Type wasn't created ModelState is invalid!";
            return View(Product);
        }

        //GET
        public IActionResult Edit(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
                TempData["error"] = "Cover Type wasn't found!";
            }

            var ProductFromDb = _unitOfWork.Product.GetFirstOrDefault(u => u.Id == id);

            if(ProductFromDb == null)
            {
                return NotFound();
            }

            return View(ProductFromDb);

        }

        //POST
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Product obj) {
            if (ModelState.IsValid)
            {
                _unitOfWork.Product.Update(obj);
                _unitOfWork.Save();
                TempData["success"] = "Cover Type has been successfully updated!";
                return RedirectToAction("Index");
            }
            return View(obj);
        }

        //GET
        public IActionResult Delete(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
                TempData["error"] = "Cover Type wasn't found!";
            }

            var ProductFromDb = _unitOfWork.Product.GetFirstOrDefault(u => u.Id == id);

            if (ProductFromDb == null)
            {
                return NotFound();
            }

            return View(ProductFromDb);

        }

        //POST
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeletePOST(int? id)
        {
            var obj = _unitOfWork.Product.GetFirstOrDefault(u => u.Id == id);

            if(obj == null)
            {
                TempData["error"] = "Cover Type isn't valid";
                return RedirectToAction("Index");
            }
            _unitOfWork.Product.Remove(obj);
            _unitOfWork.Save();
            TempData["success"] = "Cover Type has been successfully deleted!";
            return RedirectToAction("Index");

        }
    }
}
