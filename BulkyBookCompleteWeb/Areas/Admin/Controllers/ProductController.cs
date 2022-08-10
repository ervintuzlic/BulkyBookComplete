using BulkyBookComplete.DataAccess.Repository.IRepository;
using BulkyBookComplete.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Linq;

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
        public IActionResult Upsert(int? id)
        {
            Product product = new();

            // ** Projections using Select
            IEnumerable<SelectListItem> CategoryList = _unitOfWork.Category.GetAll().Select(
                u=>new SelectListItem
                {
                    Text = u.Name,
                    Value = u.Id.ToString(),
                }
            );

            IEnumerable<SelectListItem> CoverTypeList = _unitOfWork.CoverType.GetAll().Select(
                u => new SelectListItem
                {
                    Text = u.Name,
                    Value = u.Id.ToString(),
                }
            );

            if (id == null || id == 0)
            {
                // create product
                ViewBag.CategoryList = CategoryList;
                return View(product);

            }

            else
            {
                // update product
            }

            return View(product);

        }

        //POST
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Upsert(Product obj) {
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
