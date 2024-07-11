using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Net.Http.Headers;
using VariFit00.DataAccess.Repository.IRepository;
using VariFit00.Models;
using VariFit00.Models.ViewModels;
using VariFit00.Utility;

namespace VariFit00Web.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = SD.Role_Admin)]
    public class ExerciseController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        public ExerciseController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }


        public IActionResult Index()
        {
            List<Exercise> objExesList = _unitOfWork.Exercise.GetAll(includeProperties: "Equipment,MuscleExercises,MuscleExercises.Muscle").ToList();
            
            return View(objExesList);
        }

        public IActionResult Upsert(int? id)
        {
            ExerciseVM exerciseVM = new()
            {
                EquipmentList = _unitOfWork.Equipment.GetAll().Select(u => new SelectListItem
                {
                    Text = u.Name,
                    Value = u.Id.ToString()
                }),
                MuscleList = _unitOfWork.Muscle.GetAll().Select(m => new SelectListItem {
                    Text = m.Name,
                    Value = m.Id.ToString()
                }),
                Exercise = new Exercise()                
            };
 
            if (id == null || id == 0)
            {   //create
                return View(exerciseVM);
            }
            else
            {   //update
                exerciseVM.Exercise = _unitOfWork.Exercise.Get(u=>u.Id == id);
                return View(exerciseVM);
            }
        }
        [HttpPost]
        public IActionResult Upsert(ExerciseVM exerciseVM)
        {
            if (ModelState.IsValid)
            {
                if(exerciseVM.Exercise.Id == 0)     //CREATE
                {
                    _unitOfWork.Exercise.Add(exerciseVM.Exercise);
                    _unitOfWork.Save();

                    foreach (var muscleId in exerciseVM.SelectedMuscles)    //SelectedMuscles se populeaza prin (selectia din) View 
                    {
                        _unitOfWork.MuscleExercise.Add(new MuscleExercise
                        {
                            ExerciseId = exerciseVM.Exercise.Id,
                            MuscleId = muscleId
                        });
                    }
                    _unitOfWork.Save();

                    TempData["success"] = "Exercise created succesfully";
                }
                else    //UPDATE
                {
                    _unitOfWork.Exercise.Update(exerciseVM.Exercise);

                    // First, delete, then add NEW MuscleExercise records for the ExerciseId
                    var existingMuscleExercises = _unitOfWork.MuscleExercise.GetAll()
                                                    .Where(me => me.ExerciseId == exerciseVM.Exercise.Id);
                    foreach (var existingMExercise in existingMuscleExercises)
                    {
                        _unitOfWork.MuscleExercise.Remove(existingMExercise);
                    }
                    foreach (var muscleId in exerciseVM.SelectedMuscles)
                    {
                        _unitOfWork.MuscleExercise.Add(new MuscleExercise
                        {
                            ExerciseId = exerciseVM.Exercise.Id,
                            MuscleId = muscleId
                        });
                    }

                    _unitOfWork.Save();
                    TempData["success"] = "Exercise updated succesfully";
                }
                
                return RedirectToAction("Index", "Exercise");
            }
            else
            {
                exerciseVM.EquipmentList = _unitOfWork.Equipment.GetAll().Select(u => new SelectListItem
                {
                    Text = u.Name,
                    Value = u.Id.ToString()
                });
                exerciseVM.MuscleList = _unitOfWork.Muscle.GetAll().Select(m => new SelectListItem
                {
                    Text = m.Name,
                    Value = m.Id.ToString()
                });
                return View(exerciseVM);
            }
        }

        public IActionResult Delete(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }
            Exercise exeFromDb = _unitOfWork.Exercise.Get(u => u.Id == id);
            if (exeFromDb == null)
            {
                return NotFound();
            }
            return View(exeFromDb);
        }
        [HttpPost, ActionName("Delete")]
        public IActionResult DeletePOST(int? id)
        {
            Exercise exeFromDb = _unitOfWork.Exercise.Get(u => u.Id == id);
            if (exeFromDb == null || id == 0)
            {
                return NotFound();
            }
            _unitOfWork.Exercise.Remove(exeFromDb);
            _unitOfWork.Save();
            TempData["success"] = "Exercise deleted succesfully";
            return RedirectToAction("Index", "Exercise");
        }
    }
}
