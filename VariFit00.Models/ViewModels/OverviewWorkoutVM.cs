using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VariFit00.Models.ViewModels
{
    public class OverviewWorkoutVM
    {
        public Workout Workout { get; set; }
        public IEnumerable<SelectListItem> Exercises { get; set; }


        public string EquipmentNames { get; set; }
        public string MuscleNames { get; set; }
    }
}
