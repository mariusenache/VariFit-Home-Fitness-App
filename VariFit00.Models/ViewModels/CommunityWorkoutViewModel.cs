using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VariFit00.Models.ViewModels
{
    public class CommunityWorkoutViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Creator { get; set; }
        public int TimesFavorited { get; set; }
        public string MusclesTargetedNames { get; set; }
        public string EquipmentChosenNames { get; set; }
    }

}
