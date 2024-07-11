using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VariFit00.DataAccess.Data;
using VariFit00.DataAccess.Repository.IRepository;
using VariFit00.Models;

namespace VariFit00.DataAccess.Repository
{
    public class UnitOfWork : IUnitOfWork
    {

        private ApplicationDbContext _db;
        public IEquipmentRepository Equipment { get; private set; }
        public IExerciseRepository Exercise { get; private set; }
        public IMuscleRepository Muscle { get; private set; }
        public IMuscleExerciseRepository MuscleExercise {  get; set; }
        public IWorkoutRepository Workout { get; private set; }
        public IWorkoutExerciseRepository WorkoutExercise { get; set; }
        public IApplicationUserRepository ApplicationUser { get; private set; }
        public IUserFavoriteWorkoutRepository UserFavoriteWorkout { get; set; }

        public UnitOfWork(ApplicationDbContext db)
        {
            _db = db;
            Equipment = new EquipmentRepository(_db);
            Exercise = new ExerciseRepository(_db);
            Muscle = new MuscleRepository(_db);
            MuscleExercise = new MuscleExerciseRepository(_db);
            Workout = new WorkoutRepository(_db);
            WorkoutExercise = new WorkoutExerciseRepository(_db);
            ApplicationUser = new ApplicationUserRepository(_db);
            UserFavoriteWorkout = new UserFavoriteWorkoutRepository(_db);
        }

        public void Save()
        {
            _db.SaveChanges();
        }
    }
}
