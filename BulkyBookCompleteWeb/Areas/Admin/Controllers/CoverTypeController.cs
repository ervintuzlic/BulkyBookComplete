using BulkyBookComplete.DataAccess.Repository.IRepository;
using BulkyBookComplete.Models;
using Microsoft.AspNetCore.Mvc;

namespace BulkyBookCompleteWeb.Controllers
{
    [Area("Admin")]
    public class CoverTypeController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;

        public CoverTypeController(IUnitOfWork obj)
        {
            _unitOfWork = obj;
        }

        public IActionResult Index()
        {
            IEnumerable<CoverType> coverTypeList = _unitOfWork.CoverType.GetAll();
            return View(coverTypeList);
        }

        //GET
        public IActionResult Create()
        {
            return View();
        }

        //POST 
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(CoverType coverType)
        {
            if (ModelState.IsValid)
            {
                _unitOfWork.CoverType.Add(coverType);
                _unitOfWork.Save();
                TempData["success"] = "Cover Type created successfully!";
                return RedirectToAction("Index");
            }
            TempData["error"] = "Cover Type wasn't created ModelState is invalid!";
            return View(coverType);
        }
    }
}
