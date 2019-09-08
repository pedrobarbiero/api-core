using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Business.Interfaces;
using Business.Models;
using Data.Context;
using Microsoft.EntityFrameworkCore;

namespace Data.Repositories
{
    public abstract class Repository<TEntity> : IRepository<TEntity> where TEntity : Entity, new()
    {
        protected readonly StockContext _db;
        protected readonly DbSet<TEntity> _dbSet;
        protected Repository(StockContext db)
        {
            _db = db;
            _dbSet = _db.Set<TEntity>();
        }
        public async Task<IEnumerable<TEntity>> Get(Expression<Func<TEntity, bool>> predicate)
        {
            return await _dbSet.AsNoTracking()
                               .Where(predicate)
                               .ToListAsync();
        }

        public virtual async Task<List<TEntity>> GetAll()
        {
            return await _dbSet.AsNoTracking().ToListAsync();
        }

        public virtual async Task<TEntity> GetById(Guid id)
        {
            return await _dbSet.AsNoTracking()
                        .FirstOrDefaultAsync(t => t.Id == id);
        }

        public virtual async Task Add(TEntity obj)
        {
            _dbSet.Add(obj);
            await SaveChanges();
        }
        public virtual async Task Update(TEntity obj)
        {
            _dbSet.Update(obj);
            await SaveChanges();
        }

        public virtual async Task Delete(Guid id)
        {
            _dbSet.Remove(new TEntity { Id = id });
            await SaveChanges();
        }
        public async Task<int> SaveChanges()
        {
            return await _db.SaveChangesAsync();
        }
        public void Dispose()
        {
            _db?.Dispose();
        }

    }
}
