using BulkyBook.DataAccess.Repository.IRepository;
using BulkyBook.Models;
using BulkyBook.Utility;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BulkyBookWeb.Areas.Admin.Controllers;
[Area("Admin")]
[Authorize(Roles = SD.ROLE_ADMIN)]
public class CompanyController : Controller
{
    private readonly IUnitOfWork _unitOfWork;


    public CompanyController(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }
    public IActionResult Index()
    {
        return View();
    }

    // GET: ProductController/Create
    public IActionResult Upsert(int? id)
    {
        Company obj = new();
        if (id == 0 || id == null)
        {
            return View(obj);
        }
        else
        {
            obj = _unitOfWork.Company.GetFirstOrDefault(i => i.Id == id);
            return View(obj);
        }
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Upsert(Company obj)
    {
        if (ModelState.IsValid)
        {


            if (obj.Id == 0)
            {
                _unitOfWork.Company.Add(obj);
            }
            else
            {
                _unitOfWork.Company.Update(obj);
            }
            _unitOfWork.Save();
            TempData["success"] = "Company created successfully";
            return RedirectToAction("Index");
        }
        return View(obj);
    }



    //API CALLS

    public IActionResult GetAll()
    {
        var companies = _unitOfWork.Company.GetAll();
        return Json(new { data = companies });
    }

    [HttpDelete]
    public IActionResult Delete(int? id)
    {
        var obj = _unitOfWork.Company.GetFirstOrDefault(x => x.Id == id);
        if (obj == null)
        {
            return Json(new { success = false, error = "Error while deleting" });
        }

        _unitOfWork.Company.Remove(obj);
        _unitOfWork.Save();
        return Json(new { success = true, message = "Delete Succesfull" });

    }
}

