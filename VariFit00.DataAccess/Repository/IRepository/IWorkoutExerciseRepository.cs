﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VariFit00.Models;

namespace VariFit00.DataAccess.Repository.IRepository
{
    public interface IWorkoutExerciseRepository : IRepository <WorkoutExercise>
    {
        void Update(WorkoutExercise workoutExerciseObj); 
    }
}
