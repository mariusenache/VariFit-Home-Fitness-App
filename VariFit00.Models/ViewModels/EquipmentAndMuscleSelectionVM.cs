using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VariFit00.Models.ViewModels
{
    public class EquipmentAndMuscleSelectionVM
    {
        public IEnumerable<SelectListItem> AllEquipmentList { get; set; }     //equipments available for selection  ( SE POPULEAZA LISTA cu acesti SelectListItem)
        public IEnumerable<SelectListItem> AllMuscleList { get; set; }        //muscles avalable for selection (...same...)

        public List<int> SelectedEquipments { get; set; }              // ids pentru dupa selectie 
        public List<int> SelectedMuscles { get; set; }                  // muschi selectati = adauga in MuscleExercises 
    }
}
