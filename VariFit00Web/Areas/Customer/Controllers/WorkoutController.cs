using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.BlazorIdentity.Pages;
using System.Security.Claims;
using VariFit00.DataAccess.Repository.IRepository;
using VariFit00.Models;
using VariFit00.Models.ViewModels;

namespace VariFit00Web.Areas.Customer.Controllers
{
    [Area("Customer")]
    [Authorize]

    public class WorkoutController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        public WorkoutController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }


        public IActionResult Index()
        {
            var claimsIdentity = (ClaimsIdentity)User.Identity;
            var userId = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier).Value;
            var userRole = claimsIdentity.FindFirst(ClaimTypes.Role).Value;

            List<Workout> workoutsList;

            if (userRole == "Admin")
            {
                workoutsList = _unitOfWork.Workout.GetAll().ToList();

            }
            else
            {
                workoutsList = _unitOfWork.Workout.GetAll(w => w.ApplicationUserId == userId).ToList();
            }

            return View(workoutsList);
        }



        public IActionResult EquipmentAndMuscleSelection()
        {
            var EmsVM = new EquipmentAndMuscleSelectionVM
            {
                AllEquipmentList = _unitOfWork.Equipment.GetAll().Select(u => new SelectListItem
                {
                    Text = u.Name,
                    Value = u.Id.ToString()
                }),
                AllMuscleList = _unitOfWork.Muscle.GetAll().Select(m => new SelectListItem
                {
                    Text = m.Name,
                    Value = m.Id.ToString()
                })
            };
            return View(EmsVM);
        }

        [HttpGet]
        public IActionResult FilterExesBySelection(int[] selectedEquipments, int[] selectedMuscles)  // paramaterii sunt populati din cele 2 selectii din viewul EquipmentAndMuscleSelection !
        {
            int[] equipmentIdsList = selectedEquipments.ToArray();
            int[] muscleIdsList = selectedMuscles.ToArray();
            var ExercisesFiltering = _unitOfWork.Exercise.GetAll(includeProperties: "Equipment,MuscleExercises,MuscleExercises.Muscle")
                    .Where(exe => (equipmentIdsList.Contains(exe.Equipment.Id) || exe.Equipment.Id == 1))
                    .Where(exe => muscleIdsList.Any(muId => exe.MuscleExercises.Any(me => me.MuscleId == muId)))
                    .Select(u => new SelectListItem
                    {
                        Text = u.Name,
                        Value = u.Id.ToString()
                    });

            FilterExesBySelectionDTO filterExesBySelectionDTO = new()
            {
                ExercisesFilteredBySelection = ExercisesFiltering,
                Workout = new Workout(),
                //SelectedEquipments = equipmentIdsList.ToList(),
                //SelectedMuscles = muscleIdsList.ToList()
            };
            return View(filterExesBySelectionDTO);
        }


        [HttpPost]
        public IActionResult FilterExesBySelection(FilterExesBySelectionDTO filterDTO)
        {
            var claimsIdentity = (ClaimsIdentity)User.Identity;
            var userId = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier).Value;
            var userName = claimsIdentity.FindFirst(ClaimTypes.Name).Value;
 
                var workout = new Workout   //toate sunt completate in viewul curent prin input
                {
                    Name = filterDTO.Workout.Name,
                    Creator = userName,
                    MusclesTargetedIds = GetMusclesTargetedIds(filterDTO.SelectedExercises),
                    EquipmentChosenIds = GetEquipmentChosenIds(filterDTO.SelectedExercises),
                    ApplicationUserId = userId
                };

                if (!string.IsNullOrEmpty(workout.EquipmentChosenIds))
                {
                    var equipmentIds = workout.EquipmentChosenIds.Split(',').Select(int.Parse).ToList();
                    workout.EquipmentChosenNames = string.Join(", ", _unitOfWork.Equipment
                        .GetAll()
                        .Where(e => equipmentIds.Contains(e.Id))
                        .Select(e => e.Name));
                }
                else
                {
                    workout.EquipmentChosenNames = "";
                }

                if (!string.IsNullOrEmpty(workout.MusclesTargetedIds))
                {
                    var muscleIds = workout.MusclesTargetedIds.Split(',').Select(int.Parse).ToList();
                    workout.MusclesTargetedNames = string.Join(", ", _unitOfWork.Muscle
                        .GetAll()
                        .Where(m => muscleIds.Contains(m.Id))
                        .Select(m => m.Name));
                }
                else
                {
                    workout.MusclesTargetedNames = "";
                }

                _unitOfWork.Workout.Add(workout);
                _unitOfWork.Save();         //salvat Workout

                foreach (var exerciseId in filterDTO.SelectedExercises)
                {
                    _unitOfWork.WorkoutExercise.Add(new WorkoutExercise
                    {
                        WorkoutId = workout.Id,
                        ExerciseId = exerciseId,
                    });
                }
                _unitOfWork.Save();         //salvat WorkoutExercise

                TempData["success"] = "Workout created successfully";
                return RedirectToAction("Index", "Workout");
        }


        private string GetMusclesTargetedIds(List<int> selectedExercises)
        {
            var muscleIds = _unitOfWork.Exercise.GetAll(includeProperties: "Equipment,MuscleExercises,MuscleExercises.Muscle")
                .Where(exe => selectedExercises.Contains(exe.Id) && exe.MuscleExercises != null) // Check if MuscleExercises is not null
                .SelectMany(exe => exe.MuscleExercises.Select(me => me.MuscleId))
                .Distinct()
                .ToList();

            return string.Join(",", muscleIds);
        }

        private string GetEquipmentChosenIds(List<int> selectedExercises)
        {
            var equipmentIds = _unitOfWork.Exercise.GetAll(includeProperties: "Equipment")
                .Where(exe => selectedExercises.Contains(exe.Id) && exe.Equipment != null) // Check if Equipment is not null
                .Select(exe => exe.Equipment.Id)
                .Distinct()
                .ToList();

            return string.Join(",", equipmentIds);
        }

        public IActionResult OverviewWorkout(int id)
        {
            var workout = _unitOfWork.Workout.Get(w => w.Id == id);

            var equipmentIds = workout.EquipmentChosenIds?.Split(',').Select(int.Parse).ToList();
            var equipmentNames = _unitOfWork.Equipment.GetAll().Where(e => equipmentIds != null && equipmentIds.Contains(e.Id)).Select(e => e.Name);

            var muscleIds = workout.MusclesTargetedIds?.Split(',').Select(int.Parse).ToList();
            var muscleNames = _unitOfWork.Muscle.GetAll().Where(m => muscleIds != null && muscleIds.Contains(m.Id)).Select(m => m.Name);

            var oVM = new OverviewWorkoutVM
            {
                Workout = workout,
                EquipmentNames = string.Join(", ", equipmentNames),
                MuscleNames = string.Join(", ", muscleNames),
                Exercises = _unitOfWork.WorkoutExercise.GetAll().Where(we => we.WorkoutId == id).Select(e => new SelectListItem
                {
                    Text = _unitOfWork.Exercise.Get(ex => ex.Id == e.ExerciseId).Name,
                    Value = e.ExerciseId.ToString()
                })
            };

            return View(oVM);
        }

        [HttpGet]
        public IActionResult AddExeToWorkout(int workoutId)
        {
            var workout = _unitOfWork.Workout.Get(w => w.Id == workoutId);
            if (workout == null)
            {
                return NotFound();
            }

            var existingExercises = _unitOfWork.WorkoutExercise.GetAll()
                .Where(we => we.WorkoutId == workoutId)
                .Select(we => we.ExerciseId)
                .ToList();

            var addExeToWorkoutDTO = new AddExeToWorkoutDTO
            {
                Workout = workout,
                ExercisesFilteredBySelection = GetFilteredExercises(workout.EquipmentChosenIds, workout.MusclesTargetedIds),    //parametrii metodei GetFilteredExercises sunt primiti prin hidden input din view-ul OverviewWorkout
                WorkoutId = workout.Id,
                ExistingExercises = existingExercises
            };

            return View(addExeToWorkoutDTO);
        }

        [HttpPost]
        public IActionResult AddExeToWorkout(AddExeToWorkoutDTO addExeToWorkoutDTO)
        {
            if (ModelState.IsValid)
            {
                var workoutId = addExeToWorkoutDTO.WorkoutId;

                _unitOfWork.WorkoutExercise.Add(new WorkoutExercise
                {
                    WorkoutId = workoutId,
                    ExerciseId = addExeToWorkoutDTO.SelectedEx
                });


                _unitOfWork.Save();
                TempData["success"] = "Exercises added to the workout successfully";
                return RedirectToAction("OverviewWorkout", new { id = workoutId });
            }
            else
            {
                Console.WriteLine("Error on else AddeExeToWorkout!");
                //filterDTO.ExercisesFilteredBySelection = GetFilteredExercises(filterDTO.Workout.EquipmentChosenIds, filterDTO.Workout.MusclesTargetedIds);
                return View(addExeToWorkoutDTO);
            }
        }

        [HttpPost]
        public IActionResult CreateVariation(int workoutId)
        {
            var originalWorkout = _unitOfWork.Workout.Get(w => w.Id == workoutId);
            if (originalWorkout == null)
            {
                return NotFound();
            }


            string baseName;
            int currentVariationNumber = 0;

            // Check if the workout name already contains "_var"
            var nameParts = originalWorkout.Name.Split("_var");
            if (nameParts.Length > 1 && int.TryParse(nameParts.Last(), out currentVariationNumber))
            {
                baseName = string.Join("_var", nameParts.Take(nameParts.Length - 1));
            }
            else
            {
                baseName = originalWorkout.Name;
            }

            // Get all existing variations of the baseName
            var existingVariations = _unitOfWork.Workout.GetAll()
                .Where(w => w.Name.StartsWith(baseName + "_var"))
                .Select(w => w.Name)
                .ToList();

            // Find the highest variation number
            int maxVariationNumber = existingVariations
                .Select(name => name.Split("_var").Last())
                .Where(numStr => int.TryParse(numStr, out _))
                .Select(int.Parse)
                .DefaultIfEmpty(0)
                .Max();

            // Determine the new variation number
            int newVariationNumber = maxVariationNumber + 1;

            // New workout name will be baseName + "_var" + newVariationNumber
            var newWorkoutName = $"{baseName}_var{newVariationNumber}";


            var newWorkout = new Workout
            {
                Name = newWorkoutName,
                Creator = originalWorkout.Creator,
                EquipmentChosenIds = originalWorkout.EquipmentChosenIds,
                MusclesTargetedIds = originalWorkout.MusclesTargetedIds,
                ApplicationUserId = originalWorkout.ApplicationUserId
            };

            if (!string.IsNullOrEmpty(newWorkout.EquipmentChosenIds))
            {
                var equipmentIds = newWorkout.EquipmentChosenIds.Split(',').Select(int.Parse).ToList();
                newWorkout.EquipmentChosenNames = string.Join(", ", _unitOfWork.Equipment
                    .GetAll()
                    .Where(e => equipmentIds.Contains(e.Id))
                    .Select(e => e.Name));
            }
            else
            {
                newWorkout.EquipmentChosenNames = "";
            }

            if (!string.IsNullOrEmpty(newWorkout.MusclesTargetedIds))
            {
                var muscleIds = newWorkout.MusclesTargetedIds.Split(',').Select(int.Parse).ToList();
                newWorkout.MusclesTargetedNames = string.Join(", ", _unitOfWork.Muscle
                    .GetAll()
                    .Where(m => muscleIds.Contains(m.Id))
                    .Select(m => m.Name));
            }
            else
            {
                newWorkout.MusclesTargetedNames = "";
            }



            _unitOfWork.Workout.Add(newWorkout);
            _unitOfWork.Save(); 

            var originalExercises = _unitOfWork.WorkoutExercise.GetAll()
                .Where(we=>we.WorkoutId == workoutId)
                .Select(we=>we.ExerciseId)
                .ToList();

            var possibleExercises = _unitOfWork.Exercise.GetAll(includeProperties:("Equipment,MuscleExercises"))
                .Where(exe => !originalExercises.Contains(exe.Id)
                    && originalWorkout.EquipmentChosenIds.Split(',').Contains(exe.Equipment.Id.ToString())
                    && exe.MuscleExercises.Any(me => originalWorkout.MusclesTargetedIds.Split(',').Contains(me.MuscleId.ToString())))
                .Select(exe=>exe.Id)
                .ToList();


            var random = new Random();
            var exercisesToUse = possibleExercises.OrderBy(x => random.Next()).Take(originalExercises.Count).ToList();

            if (exercisesToUse.Count < originalExercises.Count)
            {
                var remainingCount = originalExercises.Count - exercisesToUse.Count;
                var additionalExercises = originalExercises.OrderBy(x => random.Next()).Take(remainingCount).ToList();
                exercisesToUse.AddRange(additionalExercises);
            }

            foreach (var exerciseId in exercisesToUse)
            {
                _unitOfWork.WorkoutExercise.Add(new WorkoutExercise
                {
                    WorkoutId = newWorkout.Id,
                    ExerciseId = exerciseId
                });
            }

            _unitOfWork.Save();

            TempData["success"] = newWorkoutName+ " created !";
            return RedirectToAction("OverviewWorkout", new { id = newWorkout.Id });

        }

        private IEnumerable<SelectListItem> GetFilteredExercises(string equipmentIds, string muscleIds)
        {
            int[] equipmentIdsList = equipmentIds?.Split(',').Select(int.Parse).ToArray() ?? Array.Empty<int>();
            int[] muscleIdsList = muscleIds?.Split(',').Select(int.Parse).ToArray() ?? Array.Empty<int>();

            return _unitOfWork.Exercise.GetAll(includeProperties: "Equipment,MuscleExercises,MuscleExercises.Muscle")
                .Where(exe => (equipmentIdsList.Contains(exe.Equipment.Id) || exe.Equipment.Id == 1))
                .Where(exe => muscleIdsList.Any(muId => exe.MuscleExercises.Any(me => me.MuscleId == muId)))
                .Select(u => new SelectListItem
                {
                    Text = u.Name,
                    Value = u.Id.ToString()
                });
        }


        public IActionResult Delete(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }
            Workout workoutFromDb = _unitOfWork.Workout.Get(u => u.Id == id);
            if (workoutFromDb == null)
            {
                return NotFound();
            }
            return View(workoutFromDb);
        }

        [HttpPost, ActionName("Delete")]
        public IActionResult DeletePOST(int? id)
        {
            Workout workoutFromDb = _unitOfWork.Workout.Get(u => u.Id == id);
            if (workoutFromDb == null)
            {
                return NotFound();
            }
            _unitOfWork.Workout.Remove(workoutFromDb);

            var existingWorkoutExercises = _unitOfWork.WorkoutExercise.GetAll()
                                                    .Where(me => me.WorkoutId == workoutFromDb.Id);
            foreach (var workoutExercise in existingWorkoutExercises)
            {
                _unitOfWork.WorkoutExercise.Remove(workoutExercise);
            }

            _unitOfWork.Save();
            TempData["success"] = "Workout deleted sucessfully";
            return RedirectToAction("Index");
        }

        [HttpPost]
        public IActionResult DeleteWorkoutExercise(int workoutId, int exerciseId)
        {
            Console.WriteLine($"WorkoutId: {workoutId}, ExerciseId: {exerciseId}");
            var workoutExercise = _unitOfWork.WorkoutExercise.Get(we => we.WorkoutId == workoutId && we.ExerciseId == exerciseId);
            if (workoutExercise == null)
            {
                return NotFound();
            }

            _unitOfWork.WorkoutExercise.Remove(workoutExercise);
            _unitOfWork.Save();
            TempData["success"] = "Exercise deleted from workout";

            return RedirectToAction("OverviewWorkout", new { id = workoutId });
        }

        [HttpPost]
        public IActionResult PublishWorkout(int id, bool isPublished)
        {
            var workout = _unitOfWork.Workout.Get(w => w.Id == id);
            if (workout == null)
            {
                return Json(new { success = false, message = "Workout not found." });
            }
            workout.IsPublished = isPublished;
            _unitOfWork.Save();
            return Json(new { success = true, message = "Workout published successfully." });
        }

        //public IActionResult Community()
        //{
        //    var workouts = _unitOfWork.Workout.GetAll()
        //        .Where(w => w.IsPublished)
        //        .ToList();
        //    return View(workouts);
        //}

        public IActionResult Community()
        {
            var workouts = _unitOfWork.Workout.GetAll()
                .Where(w => w.IsPublished)
                .Select(w => new
                {
                    Workout = w,
                    TimesFavorited = _unitOfWork.UserFavoriteWorkout.GetAll(ufw => ufw.WorkoutId == w.Id).Count(),
                    MusclesTargetedNames = w.MusclesTargetedNames,
                    EquipmentChosenNames = w.EquipmentChosenNames
                })
                .ToList();

            var communityWorkouts = workouts.Select(w => new CommunityWorkoutViewModel
            {
                Id = w.Workout.Id,
                Name = w.Workout.Name,
                Creator = w.Workout.Creator,
                TimesFavorited = w.TimesFavorited,
                MusclesTargetedNames = w.MusclesTargetedNames,
                EquipmentChosenNames = w.EquipmentChosenNames
            }).ToList();

            return View(communityWorkouts);
        }




        public IActionResult ViewCommunityWorkout(int id)
        {
            var workout = _unitOfWork.Workout.Get(w => w.Id == id);

            var equipmentIds = workout.EquipmentChosenIds?.Split(',').Select(int.Parse).ToList();
            var equipmentNames = _unitOfWork.Equipment.GetAll().Where(e => equipmentIds != null && equipmentIds.Contains(e.Id)).Select(e => e.Name);

            var muscleIds = workout.MusclesTargetedIds?.Split(',').Select(int.Parse).ToList();
            var muscleNames = _unitOfWork.Muscle.GetAll().Where(m => muscleIds != null && muscleIds.Contains(m.Id)).Select(m => m.Name);

            var oVM = new OverviewWorkoutVM
            {
                Workout = workout,
                EquipmentNames = string.Join(", ", equipmentNames),
                MuscleNames = string.Join(", ", muscleNames),
                Exercises = _unitOfWork.WorkoutExercise.GetAll().Where(we => we.WorkoutId == id).Select(e => new SelectListItem
                {
                    Text = _unitOfWork.Exercise.Get(ex => ex.Id == e.ExerciseId).Name,
                    Value = e.ExerciseId.ToString()
                })
            };

            return View(oVM);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult SaveFavorite(int workoutId)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (userId == null)
            {
                return BadRequest("User not logged in.");
            }

            var existingFavorite = _unitOfWork.UserFavoriteWorkout.Get(ufw => ufw.ApplicationUserId == userId && ufw.WorkoutId == workoutId);

            if (existingFavorite != null)
            {

                //return BadRequest("Workout already saved to favorites.");
                TempData["warning"] = "Workout already saved to favorites";
               // return RedirectToAction("Community", "Workout");
                return RedirectToAction("ViewCommunityWorkout", new { id = workoutId });


            }

            // Ensure the Workout entity is attached to the DbContext
            var workout = _unitOfWork.Workout.Get(w => w.Id == workoutId);
            if (workout == null)
            {
                return NotFound("Workout not found.");
            }

            var favorite = new UserFavoriteWorkout
            {
                ApplicationUserId = userId,
                WorkoutId = workoutId,
                Workout = workout // Attach the Workout entity
            };

            _unitOfWork.UserFavoriteWorkout.Add(favorite);
            _unitOfWork.Save();

            //return Ok("Workout saved to favorites successfully.");



            TempData["success"] = "Workout saved to favorites successfully";
            return RedirectToAction("ViewCommunityWorkout", new { id = workoutId });

        }

        public IActionResult Favorites()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (userId == null)
            {
                // Handle the case when the user is not logged in
                return RedirectToAction("Login", "Account"); // Redirect to the login page or display an error message
            }

            var favoriteWorkoutIds = _unitOfWork.UserFavoriteWorkout.GetAll(ufw => ufw.ApplicationUserId == userId)
                .Select(ufw => ufw.WorkoutId)
                .ToList();

            var favoriteWorkouts = _unitOfWork.Workout.GetAll()
                .Where(w => favoriteWorkoutIds.Contains(w.Id))
                .ToList();

            return View(favoriteWorkouts);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult RemoveFavorite(int workoutId)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (userId == null)
            {
                return BadRequest("User not logged in.");
            }

            var favoriteWorkout = _unitOfWork.UserFavoriteWorkout.Get(ufw => ufw.ApplicationUserId == userId && ufw.WorkoutId == workoutId);
            if (favoriteWorkout == null)
            {
                return NotFound("Favorite workout not found.");
            }

            _unitOfWork.UserFavoriteWorkout.Remove(favoriteWorkout);
            _unitOfWork.Save();

            TempData["success"] = "Workout removed from favorites successfully";
            return RedirectToAction("Favorites", "Workout");
        }



    }
}
