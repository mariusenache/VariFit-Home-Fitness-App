using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VariFit00.Models.ViewModels
{
    public class WorkoutVM
    {
        public Workout Workout { get; set; }
        public string ExerciseList { get; set; } = string.Empty;   //exercises available for selection (POPUPLARE LISTA)

        public List<int> SelectedExercises { get; set; }    // exes selectate - adaug in WorkoutExercises, prin controler cu foreach(var exId in workoutVM.SelectedExercises)

        //public int[] EquipmentIdsList { get; set; }     //astea 2 sunt ca sa repopuleze lista in caz de invalidModelState = else-ul din [POST]Get din controller
        //public int[] MuscleIdsList { get; set; }


        [ValidateNever]
        public IEnumerable<SelectListItem> EquipmentList { get; set; }     //equipments available for selection  ( SE POPULEAZA LISTA cu acesti SelectListItem)
        [ValidateNever]
        public IEnumerable<SelectListItem> MuscleList { get; set; }        //muscles avalable for selection (...same...)

        [ValidateNever]
        public List<int> SelectedEquipments { get; set; }              // ids pentru GetExesByEqAndMuscle dupa selectie (*obs: pentru crearea unui exercise, un singur eqId a fost selectat din view, SelectedEquipments astea nu au treaba, ele sunt ptr viewul GetExe....)
        public List<int> SelectedMuscles { get; set; }                  // muschi selectati = adauga in MuscleExercises 

    }
}
