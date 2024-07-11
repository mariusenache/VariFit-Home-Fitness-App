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
    public class WorkoutRepository : Repository<Workout>, IWorkoutRepository
    {
        private ApplicationDbContext _db;
        public WorkoutRepository(ApplicationDbContext db):base(db)
        {
            _db = db;
        }
        public void Update(Workout obj)
        {
            _db.Workouts.Update(obj);
        }
    }
}
