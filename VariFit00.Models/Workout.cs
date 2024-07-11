using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VariFit00.Models
{
    public class Workout
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Creator { get; set; }
        [ValidateNever]
        public string MusclesTargetedIds { get; set; }
        [ValidateNever]
        public string EquipmentChosenIds { get; set; }
        [ValidateNever]
        public string ?MusclesTargetedNames { get; set; }
        [ValidateNever]
        public string ?EquipmentChosenNames { get; set; }
        [ValidateNever]
        public ICollection<WorkoutExercise> WorkoutExercises { get; set; }

        [ValidateNever]
        public ICollection<UserFavoriteWorkout> UserFavoriteWorkouts { get; set; }


        public string? ApplicationUserId { get; set; }
        [ForeignKey("ApplicationUserId")]
        [ValidateNever]
        public ApplicationUser? ApplicationUser { get; set; }
        
        [ValidateNever]
        public bool IsPublished { get; set; } 
    }
}
