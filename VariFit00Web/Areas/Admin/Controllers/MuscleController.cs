using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using VariFit00.DataAccess.Data;
using VariFit00.DataAccess.Repository.IRepository;
using VariFit00.Models;
using VariFit00.Utility;


namespace VariFit00Web.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = SD.Role_Admin)]
    public class MuscleController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        public MuscleController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public IActionResult Index()
        {
            List<Muscle> objMuscleList = _unitOfWork.Muscle.GetAll().ToList();
            return View(objMuscleList);
        }

        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Create(Muscle obj)
        {
            if (obj.Name != null && obj.Name.ToLower() == "test")
            {
                ModelState.AddModelError("name", "Test is not valid name [this is server side validation]");
            }
            if (ModelState.IsValid)
            {
                _unitOfWork.Muscle.Add(obj);
                _unitOfWork.Save();
                TempData["success"] = "Muscle created succesfully";
                return RedirectToAction("Index", "Muscle");
            }
            return View(obj);
        }

        public IActionResult Edit(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }
            Muscle muscleFromDb = _unitOfWork.Muscle.Get(u => u.Id == id);
            if (muscleFromDb == null)
            {
                return NotFound();
            }
            return View(muscleFromDb);
        }
        [HttpPost]
        public IActionResult Edit(Muscle obj)
        {
            if (ModelState.IsValid)
            {
                _unitOfWork.Muscle.Update(obj);
                _unitOfWork.Save();
                TempData["success"] = "Muscle udated succesfully";
                return RedirectToAction("Index", "Muscle");
            }
            return View(obj);
        }

        public IActionResult Delete(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }
            Muscle muscleFromDb = _unitOfWork.Muscle.Get(u => u.Id == id);
            if (muscleFromDb == null)
            {
                return NotFound();
            }
            return View(muscleFromDb);
        }
        [HttpPost, ActionName("Delete")]
        public IActionResult DeletePOST(int? id)
        {
            Muscle muscleFromDb = _unitOfWork.Muscle.Get(u => u.Id == id);
            if (muscleFromDb == null || id == 0)
            {
                return NotFound();
            }
            _unitOfWork.Muscle.Remove(muscleFromDb);
            _unitOfWork.Save();
            TempData["success"] = "Muscle deleted succesfully";
            return RedirectToAction("Index", "Muscle");
        }
    }
}
