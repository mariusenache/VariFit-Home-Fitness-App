using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VariFit00.Models
{
    public class Muscle
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string GroupOfMuscle { get; set; }
        [ValidateNever]
        public ICollection<MuscleExercise> MuscleExercises{ get; set; }

    }
}
