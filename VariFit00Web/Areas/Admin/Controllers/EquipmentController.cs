using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using VariFit00.DataAccess.Data;
using VariFit00.DataAccess.Repository.IRepository;
using VariFit00.Models;
using VariFit00.Utility;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;


namespace VariFit00Web.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = SD.Role_Admin)]
    public class EquipmentController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        public EquipmentController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public IActionResult Index()
        {
            List<Equipment> objEquipmentList = _unitOfWork.Equipment.GetAll().ToList();
            return View(objEquipmentList);
        }

        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Create(Equipment obj)
        {
            if (obj.Name != null && obj.Name.ToLower() == "test")
            {
                ModelState.AddModelError("name", "Test is not valid name [this is server side validation]");
            }
            if (ModelState.IsValid)
            {
                _unitOfWork.Equipment.Add(obj);
                _unitOfWork.Save();
                TempData["success"] = "Equipment created succesfully";
                return RedirectToAction("Index", "Equipment");
            }
            return View(obj);
        }

        public IActionResult Edit(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }
            Equipment equipmentFromDb = _unitOfWork.Equipment.Get(u => u.Id == id);
            if (equipmentFromDb == null)
            {
                return NotFound();
            }
            return View(equipmentFromDb);
        }
        [HttpPost]
        public IActionResult Edit(Equipment obj)
        {
            if (ModelState.IsValid)
            {
                _unitOfWork.Equipment.Update(obj);
                _unitOfWork.Save();
                TempData["success"] = "Equipment udated succesfully";
                return RedirectToAction("Index", "Equipment");
            }
            return View(obj);
        }

        public IActionResult Delete(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }
            Equipment equipmentFromDb = _unitOfWork.Equipment.Get(u => u.Id == id);
            if (equipmentFromDb == null)
            {
                return NotFound();
            }
            return View(equipmentFromDb);
        }
        [HttpPost, ActionName("Delete")]
        public IActionResult DeletePOST(int? id)
        {
            Equipment equipmentDeletingFromDb = _unitOfWork.Equipment.Get(u => u.Id == id);
            if (equipmentDeletingFromDb == null || id == 0)
            {
                return NotFound();
            }
            _unitOfWork.Equipment.Remove(equipmentDeletingFromDb);
            _unitOfWork.Save();
            TempData["success"] = "Equipment deleted succesfully";
            return RedirectToAction("Index", "Equipment");
        }

    }
}
