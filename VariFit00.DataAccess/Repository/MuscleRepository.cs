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
    public class MuscleRepository : Repository<Muscle>, IMuscleRepository
    {
        private ApplicationDbContext _db;
        public MuscleRepository(ApplicationDbContext db):base(db)
        {
            _db=db;
        }
        public void Update(Muscle obj)
        {
            _db.Muscles.Update(obj);
        }
    }
}
