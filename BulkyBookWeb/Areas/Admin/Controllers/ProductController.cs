using BulkyBook.DataAccess.Repository.IRepository;
using BulkyBook.Models.ViewModel;
using BulkyBook.Utility;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Linq;

namespace BulkyBookWeb.Areas.Admin.Controllers;
[Area("Admin")]
[Authorize(Roles = SD.ROLE_ADMIN)]
public class ProductController : Controller
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IWebHostEnvironment _environment;

    public ProductController(IUnitOfWork unitOfWork, IWebHostEnvironment environment)
    {
        _unitOfWork = unitOfWork;
        _environment = environment;
    }
    public IActionResult Index()
    {
        return View();
    }

    // GET: ProductController/Create
    public IActionResult Upsert(int? id)
    {
        ProductVM obj = new()
        {
            Product = new(),
            CategoryList = _unitOfWork.Category.GetAll().Select(i => new SelectListItem
            {
                Text = i.Name,
                Value = i.Id.ToString()
            }),

            CoverTypeList = _unitOfWork.CoverType.GetAll().Select(i => new SelectListItem
            {
                Text = i.Name,
                Value = i.Id.ToString()
            })
        };
        if (id == 0 || id == null)
        {
            return View(obj);
        }
        else
        {
            obj.Product = _unitOfWork.Product.GetFirstOrDefault(i => i.Id == id);
            return View(obj);
        }
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Upsert(ProductVM obj, IFormFile? file)
    {
        if (ModelState.IsValid)
        {
            string wwwRootPath = _environment.WebRootPath;
            if (file != null)
            {
                string fileName = Guid.NewGuid().ToString();
                string uploads = Path.Combine(wwwRootPath, @"images\products");
                string extension = Path.GetExtension(file.FileName);


                if (obj.Product.ImageUrl != null)
                {
                    var oldImagePath = Path.Combine(wwwRootPath, obj.Product.ImageUrl.TrimStart('\\'));
                    if (System.IO.File.Exists(oldImagePath))
                    {
                        System.IO.File.Delete(oldImagePath);
                    }
                }

                using (var fileStream = new FileStream(Path.Combine(uploads, fileName + extension), FileMode.Create))
                {
                    file.CopyTo(fileStream);
                }
                obj.Product.ImageUrl = @"\images\products\" + fileName + extension;
            }

            if (obj.Product.Id == 0)
            {
                _unitOfWork.Product.Add(obj.Product);
            }
            else
            {
                _unitOfWork.Product.Update(obj.Product);
            }
            _unitOfWork.Save();
            TempData["success"] = "Product created successfully";
            return RedirectToAction("Index");
        }
        return View(obj);
    }



    //API CALLS

    public IActionResult GetAll()
    {
        var products = _unitOfWork.Product.GetAll(includeProperties: "Category,CoverType");
        return Json(new { data = products});
    }

    [HttpDelete]
    public IActionResult Delete(int? id)
    {
        var obj = _unitOfWork.Product.GetFirstOrDefault(x => x.Id == id);
        if(obj == null)
        {
            return Json(new {success = false , error = "Error while deleting"});
        }

        var oldImagePath = Path.Combine(_environment.WebRootPath, obj.ImageUrl.TrimStart('\\'));
        if (System.IO.File.Exists(oldImagePath))
        {
            System.IO.File.Delete(oldImagePath);
        }

        _unitOfWork.Product.Remove(obj);
        _unitOfWork.Save();
        return Json(new { success = true, message = "Delete Succesfull" });

    }
}

