using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace VariFit00.DataAccess.Repository.IRepository
{
    public interface IRepository<T> where T : class
    {
        //T - Equipment sau orice alt obiect
        IEnumerable<T> GetAll(Expression<Func<T, bool>>? filter = null, string ? includeProperties = null);
        T Get(Expression <Func<T, bool>> filter, string? includeProperties = null);
        void Add(T entity);
        void Remove(T entity);
        void RemoveRange(IEnumerable<T> entity);

    }
}
