using BulkyBook.DataAccess.Repository.IRepository;
using BulkyBook.Models;
using BulkyBook.Utility;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BulkyBookWeb.Controllers;
[Area("Admin")]
[Authorize(Roles = SD.ROLE_ADMIN)]
public class CoverTypeController : Controller

{

    private readonly IUnitOfWork _unitOfWork;

    public CoverTypeController(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public IActionResult Index()
    {
        var data = _unitOfWork.CoverType.GetAll();
        return View(data);
    }


    public ActionResult Create()
    {
        return View();
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Create(CoverType obj)
    {
        if (ModelState.IsValid)
        {
            _unitOfWork.CoverType.Add(obj);
            _unitOfWork.Save();
            return RedirectToAction("Index");
        }

        return View(obj);
    }

    public IActionResult Edit(int id)
    {
        if (id == null || id == 0)
        {
            return NotFound();
        }

        var data = _unitOfWork.CoverType.GetFirstOrDefault(x => x.Id == id);
        if (data == null) return NotFound();

        return View(data);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Edit(CoverType obj)
    {
        if (ModelState.IsValid)
        {
            _unitOfWork.CoverType.Update(obj);
            _unitOfWork.Save();
            TempData["Success"] = "Cover type updated succesfully";
            return RedirectToAction("Index");
        }
        return View(obj);
    }

    public IActionResult Delete(int? id)
    {
        if (id == null || id == 0)
        {
            return NotFound();
        }

        var data = _unitOfWork.CoverType.GetFirstOrDefault(x => x.Id == id);
        if (data == null) return NotFound();

        return View(data);
    }

    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public IActionResult DeletePOST(int? id)
    {
        var data = _unitOfWork.CoverType.GetFirstOrDefault(x => x.Id == id);

        if (data == null)
        {
            return NotFound();
        }
        _unitOfWork.CoverType.Remove(data);
        _unitOfWork.Save();
        TempData["success"] = "Cover type deleted successfully";
        return RedirectToAction("Index");
    }
}
