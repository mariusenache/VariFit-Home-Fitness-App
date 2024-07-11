using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VariFit00.Models.ViewModels
{
    public class AddExeToWorkoutDTO
    {
        [ValidateNever]
        public IEnumerable<SelectListItem> ExercisesFilteredBySelection { get; set; }   //exercises available for selection (POPUPLARE LISTA)
        [ValidateNever]
        public Workout Workout { get; set; }
        public int WorkoutId { get; set; }
        public int SelectedEx {  get; set; }
        [ValidateNever]
        public IEnumerable<int> ExistingExercises { get; set; }
    }
}
