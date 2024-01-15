using Microsoft.EntityFrameworkCore;
using ProyectMultiTenant.Domain.IRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace ProyectMultiTenant.Repository.MSSql
{
    public class BaseRepository<T> : IBaseRepository<T> where T : class
    {
        protected ApplicationDbContext context;
        protected readonly DbSet<T> dbSetEntity;
        public BaseRepository(ApplicationDbContext context)
        {
            this.context = context;
            dbSetEntity = this.context.Set<T>();
        }
        public async Task Delete(T entity)
        {
            context.Remove(entity);
            await context.SaveChangesAsync();
        }
        public async Task DeleteRange(List<T> entityList)
        {
            context.RemoveRange(entityList);
            await context.SaveChangesAsync();
        }

        public async Task<T> Find(object id)
        {
            var entity = await dbSetEntity.FindAsync(id);
            dbSetEntity.Attach(entity).State = EntityState.Detached;
            return entity;
        }

        public async Task<T> FindPredicate(Expression<Func<T, bool>> predicate)
        {
            return await dbSetEntity.Where(predicate).FirstOrDefaultAsync();
        }

        public async Task<List<T>> FindAllPredicate(Expression<Func<T, bool>> predicate)
        {
            return await dbSetEntity.Where(predicate).ToListAsync();
        }

        public async Task<bool> Exist(Expression<Func<T, bool>> predicate)
        {
            return await dbSetEntity.AnyAsync(predicate);
        }
        public async Task<List<T>> GetAll()
        {
            return await dbSetEntity.AsNoTracking().ToListAsync();
        }
        public async Task Add(T entity)
        {
            await dbSetEntity.AddAsync(entity);
            await context.SaveChangesAsync();
            dbSetEntity.Attach(entity).State = EntityState.Detached;
        }

        public async Task AddRange(IEnumerable<T> entityList)
        {
            await dbSetEntity.AddRangeAsync(entityList);
            await context.SaveChangesAsync();
            foreach (var item in entityList)
            {
                dbSetEntity.Attach(item).State = EntityState.Detached;
            }
        }
        public async Task Update(T entity)
        {
            dbSetEntity.Attach(entity);
            context.Update(entity);
            await context.SaveChangesAsync();
            dbSetEntity.Attach(entity).State = EntityState.Detached;
        }
        public async Task UpdatePartial(T entity, params Expression<Func<T, object>>[] properties)
        {
            dbSetEntity.Attach(entity);
            //context.Entry(entity).State = EntityState.Unchanged;
            foreach (var property in properties)
            {
                context.Entry(entity).Property(property).IsModified = true;
            }
            await context.SaveChangesAsync();
        }
        public async Task UpdatePartialNotIncluding(T entity, params Expression<Func<T, object>>[] properties)
        {
            dbSetEntity.Attach(entity);
            context.Entry(entity).State = EntityState.Modified;
            foreach (var property in properties)
            {
                context.Entry(entity).Property(property).IsModified = false;
            }
            await context.SaveChangesAsync();
        }
    }
}
