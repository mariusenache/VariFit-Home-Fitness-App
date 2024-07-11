using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VariFit00.DataAccess.Repository.IRepository
{
    public interface IUnitOfWork
    {
        IExerciseRepository Exercise {  get; }
        IEquipmentRepository Equipment { get; }
        IMuscleRepository Muscle { get; }
        IMuscleExerciseRepository MuscleExercise { get; set; }
        IWorkoutRepository Workout { get; }
        IWorkoutExerciseRepository WorkoutExercise { get; set; }
        IApplicationUserRepository ApplicationUser { get; }
        IUserFavoriteWorkoutRepository UserFavoriteWorkout { get; set; }
        void Save();
    }
}
