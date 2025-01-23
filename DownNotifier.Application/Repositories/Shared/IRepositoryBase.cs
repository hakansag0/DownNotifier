using DownNotifier.Domain.Entities.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace DownNotifier.Application.Repositories.Shared
{
    public interface IRepository<T> where T : BaseEntity
    {
        public List<T> GetAll(Expression<Func<T, bool>>? filter = null);
        public T? GetById(int id);
        public T? Get(Expression<Func<T, bool>> filter);
        public T Add(T entity);
        public bool Update(T entity);
        public bool UpdateAll(List<T> entities);
        public bool Delete(int id);

        public void SaveChanges();
    }
}
