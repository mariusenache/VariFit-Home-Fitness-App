using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using VariFit00.DataAccess.Data;
using VariFit00.DataAccess.Repository.IRepository;
using VariFit00.Models;

namespace VariFit00.DataAccess.Repository
{
    public class EquipmentRepository : Repository<Equipment>, IEquipmentRepository
    {
        private ApplicationDbContext _db;
        public EquipmentRepository(ApplicationDbContext db) : base(db) 
        {
            _db = db;
        }

        public void Update(Equipment obj)
        {
            _db.Equipments.Update(obj);
        }
    }
}
