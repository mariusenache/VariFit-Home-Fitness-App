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
    public class ExerciseRepository : Repository<Exercise>, IExerciseRepository
    {
        private ApplicationDbContext _db;
        public ExerciseRepository(ApplicationDbContext db):base(db)
        {
            _db=db;
        }
        public void Update(Exercise obj)
        {
            _db.Exercises.Update(obj);
        }
    }
}
