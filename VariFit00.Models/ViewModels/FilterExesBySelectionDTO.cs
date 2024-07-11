using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VariFit00.Models.ViewModels
{
    public class FilterExesBySelectionDTO
    {
        [ValidateNever]
        public IEnumerable<SelectListItem> ExercisesFilteredBySelection { get; set; }   //exercises available for selection (POPUPLARE LISTA)
        public Workout Workout { get; set; }
        public List<int> SelectedExercises { get; set; }    // exes selectate - adaug in WorkoutExercises, prin controler cu foreach(var exId in workoutVM.SelectedExercises)
        //public List<int> SelectedEquipments { get; set; } // Add this property
        //public List<int> SelectedMuscles { get; set; } // Add this property
    }
}
