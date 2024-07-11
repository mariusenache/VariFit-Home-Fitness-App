using Microsoft.EntityFrameworkCore.Update.Internal;
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
    public class MuscleExerciseRepository : Repository<MuscleExercise>, IMuscleExerciseRepository
    {
        private ApplicationDbContext _db;
        public MuscleExerciseRepository(ApplicationDbContext db):base(db)
        {
            _db=db;
        }
        public void Update(MuscleExercise obj)
        {
            _db.MuscleExercises.Update(obj);
        }
    }
}
