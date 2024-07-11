using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VariFit00.Models
{
    public class WorkoutExercise
    {
        public int WorkoutId { get; set; }
        public int ExerciseId { get; set; }
        public Workout Workout { get; set; }
        public Exercise Exercise { get; set; }
    }
}
