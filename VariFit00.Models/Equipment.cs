using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace VariFit00.Models
{
    public class Equipment
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [DisplayName("Equipment Name")]
        public string Name { get; set; }
        [ValidateNever]
        public ICollection<Exercise> Exercises { get; set; }
        [ValidateNever]
        public ICollection<MuscleExercise> MuscleExercises { get; set; }
    }
}
