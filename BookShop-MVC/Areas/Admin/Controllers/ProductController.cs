using System.Linq;
using BookShop_MVC.DataAccess.Repository.IRepository;
using BookShop_MVC.Models;
using BookShop_MVC.Models.ViewModels;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace BookShop_MVC.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ProductController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IWebHostEnvironment _hostEnvironment;

        public ProductController(IUnitOfWork unitOfWork, IWebHostEnvironment hostEnvironment)
        {
            _unitOfWork = unitOfWork;
            _hostEnvironment = hostEnvironment;
        }
        // GET
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult Upsert(int? id)
        {
            ProductVM productVm = new ProductVM()
            {
                Product = new Product(),
                CategoryList = _unitOfWork.Category.GetAll().Select(I => new SelectListItem
                {
                    Text = I.Name,
                    Value = I.Id.ToString()
                }),
                CoverTypeList = _unitOfWork.CoverType.GetAll().Select(I => new SelectListItem
                {
                    Text = I.Name,
                    Value = I.Id.ToString()
                })
            };
            if (id == null)
            {
                return View(productVm);
            }

            productVm.Product = _unitOfWork.Product.Get(id.GetValueOrDefault());
            if (productVm.Product == null)
            {
                return NotFound();
            }
            return View(productVm);
        }
        
        // [HttpPost]
        // [ValidateAntiForgeryToken]
        // public IActionResult Upsert(Product Product)
        // {
        //     if (ModelState.IsValid)
        //     {
        //         if (Product.Id == 0)
        //         {
        //             _unitOfWork.Product.Add(Product);
        //             
        //         }
        //         else
        //         {
        //             _unitOfWork.Product.Update(Product);
        //         }
        //         _unitOfWork.Save();
        //         return RedirectToAction(nameof(Index));
        //     }
        //     return View(Product);
        // }
        
        
        
        
        
        #region Api Calls

        [HttpGet]
        public IActionResult GetAll()
        {
            var allObj = _unitOfWork.Product.GetAll(includeProperties:"Category,CoverType");
            return Json(new {data = allObj});
        }
        [HttpDelete]
        public IActionResult Delete(int id)
        {
            var objFromDb = _unitOfWork.Product.Get(id);
            if (objFromDb == null)
            {
                return Json(new {success = false, message = "Error while Deleting"});
            }

            _unitOfWork.Product.Remove(objFromDb);
            _unitOfWork.Save();
            return Json(new {success = true, message = "Delete successful"});
        }
        #endregion

    }
}