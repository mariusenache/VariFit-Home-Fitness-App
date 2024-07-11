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
    public class WorkoutExerciseRepository : Repository<WorkoutExercise>, IWorkoutExerciseRepository
    {
        private ApplicationDbContext _context;
        public WorkoutExerciseRepository(ApplicationDbContext context):base(context)
        {
            _context = context;
        }
        
        public void Update(WorkoutExercise obj)
        {
            _context.WorkoutExercises.Update(obj);
        }
    }
}
