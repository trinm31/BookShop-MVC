using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using BookShop_MVC.DataAccess.Repository.IRepository;
using BookShop_MVC.Models;
using BookShop_MVC.Models.ViewModels;
using BookShop_MVC.Utility;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace BookShop_MVC.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = SD.Role_Admin)]
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
        public async Task<IActionResult> Upsert(int? id)
        {
            IEnumerable<Category> CateLists = await _unitOfWork.Category.GetAllAsync();
            ProductVM productVm = new ProductVM()
            {
                Product = new Product(),
                CategoryList = CateLists.Select(I => new SelectListItem
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
        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task <IActionResult> Upsert(ProductVM ProductVM)
        {
            if (ModelState.IsValid)
            {
                string webRootPath = _hostEnvironment.WebRootPath;
                var files = HttpContext.Request.Form.Files;
                if (files.Count > 0)
                {
                    string fileName = Guid.NewGuid().ToString();
                    var uploads = Path.Combine(webRootPath, @"images/products");
                    var extension = Path.GetExtension(files[0].FileName);
                    if (ProductVM.Product.ImageUrl != null)
                    {
                        // to edit path so we need to delete the old path and update new one
                        var imagePath = Path.Combine(webRootPath, ProductVM.Product.ImageUrl.TrimStart('/'));
                        if (System.IO.File.Exists(imagePath))
                        {
                            System.IO.File.Delete(imagePath);
                        }
                    }

                    using (var filesStreams =
                        new FileStream(Path.Combine(uploads, fileName + extension), FileMode.Create))
                    {
                        files[0].CopyTo(filesStreams);
                    }

                    ProductVM.Product.ImageUrl = @"/images/products/" + fileName + extension;
                }
                else
                {
                    //update without change the images
                    if (ProductVM.Product.Id != 0)
                    {
                        Product objFromDb = _unitOfWork.Product.Get(ProductVM.Product.Id);
                        ProductVM.Product.ImageUrl = objFromDb.ImageUrl;
                    }
                }
                if (ProductVM.Product.Id == 0)
                {
                    _unitOfWork.Product.Add(ProductVM.Product);
                }
                else
                {
                    _unitOfWork.Product.Update(ProductVM.Product);
                }
                _unitOfWork.Save();
                return RedirectToAction(nameof(Index));
            }
            else
            {
                IEnumerable<Category> CateLits = await _unitOfWork.Category.GetAllAsync();
                ProductVM.CategoryList = CateLits.Select(I => new SelectListItem
                {
                    Text = I.Name,
                    Value = I.Id.ToString()
                });
                ProductVM.CoverTypeList = _unitOfWork.CoverType.GetAll().Select(I => new SelectListItem
                {
                    Text = I.Name,
                    Value = I.Id.ToString()
                });
                if (ProductVM.Product.Id!=0)
                {
                    ProductVM.Product = _unitOfWork.Product.Get(ProductVM.Product.Id);
                }
            }
            return View(ProductVM);
        }
        
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
            string webRootPath = _hostEnvironment.WebRootPath;
            // to edit path so we need to delete the old path and update new one
            var imagePath = Path.Combine(webRootPath, objFromDb.ImageUrl.TrimStart('/'));
            if (System.IO.File.Exists(imagePath))
            {
                System.IO.File.Delete(imagePath);
            }
            _unitOfWork.Product.Remove(objFromDb);
            _unitOfWork.Save();
            return Json(new {success = true, message = "Delete successful"});
        }
        #endregion

    }
}