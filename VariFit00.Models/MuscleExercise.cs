using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VariFit00.Models
{
    public class MuscleExercise
    {
        public int MuscleId { get; set; }
        public int ExerciseId {  get; set; }
        public Muscle Muscle { get; set; }
        public Exercise Exercise { get; set; }
    }
}
