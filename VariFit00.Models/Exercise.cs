using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

namespace VariFit00.Models
{
    public class Exercise
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        [Range(1,5)]
        public int Level { get; set; }

        public int EquipmentId { get; set; }
        [ForeignKey("EquipmentId")]
        [ValidateNever]
        public Equipment Equipment { get; set; }

        [ValidateNever]
        public ICollection<MuscleExercise> MuscleExercises { get; set; }
        [ValidateNever]
        public ICollection<WorkoutExercise> WorkoutExercises { get; set; }


    }
}
