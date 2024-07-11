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
    public class UserFavoriteWorkoutRepository : Repository<UserFavoriteWorkout>, IUserFavoriteWorkoutRepository
    {
        private ApplicationDbContext _context;
        public UserFavoriteWorkoutRepository(ApplicationDbContext context):base(context)
        {
            _context = context;
        }
        
        public void Update(UserFavoriteWorkout obj)
        {
            _context.UserFavoriteWorkouts.Update(obj);
        }
    }
}
