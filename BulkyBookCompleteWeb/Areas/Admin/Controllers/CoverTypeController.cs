using BulkyBookComplete.DataAccess.Repository.IRepository;
using Microsoft.AspNetCore.Mvc;

namespace BulkyBookCompleteWeb.Areas.Admin.Controllers
{
    public class CoverTypeController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;

        public CoverTypeController(IUnitOfWork obj)
        {
            _unitOfWork = obj;
        }



        public IActionResult Index()
        {
            return View();
        }
    }
}
