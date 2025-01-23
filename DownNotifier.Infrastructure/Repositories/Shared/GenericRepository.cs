using DownNotifier.Application.Repositories.Shared;
using DownNotifier.Domain.Entities.Shared;
using DownNotifier.Infrastructure.Context;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace DownNotifier.Infrastructure.Repositories.Shared
{
    internal class GenericRepository<T> : IRepository<T> where T : BaseEntity
    {
        protected readonly DownNotifierDbContext downNotifierDbContext;

        public GenericRepository(DownNotifierDbContext downNotifierDbContext)
        {
            this.downNotifierDbContext = downNotifierDbContext;
        }
        public T Add(T entity)
        {
            EntityEntry<T> createdEntity = downNotifierDbContext.Set<T>().Add(entity);
            return createdEntity.Entity;
        }

        public bool Delete(int id)
        {
            T? entityToDelete = GetById(id);
            if (entityToDelete == null)
                return false;
            downNotifierDbContext.Set<T>().Remove(entityToDelete);
            return true;
        }

        public T? GetById(int id)
        {
            return downNotifierDbContext.Set<T>().FirstOrDefault(s => s.Id == id);
        }

        public T? Get(Expression<Func<T, bool>> filter)
        {
            return downNotifierDbContext.Set<T>().FirstOrDefault(filter);
        }

        public List<T> GetAll(Expression<Func<T, bool>>? filter = null)
        {
            if (filter != null)
            {
                return downNotifierDbContext.Set<T>().Where(filter).ToList();
            }
            return downNotifierDbContext.Set<T>().ToList();
        }

        public void SaveChanges()
        {
            downNotifierDbContext.SaveChanges();
        }

        public bool Update(T entity)
        {
            downNotifierDbContext.Set<T>().Update(entity);
            return true;
        }

        public bool UpdateAll(List<T> entities)
        {
            downNotifierDbContext.Set<T>().UpdateRange(entities);
            return true;
        }
    }
}
