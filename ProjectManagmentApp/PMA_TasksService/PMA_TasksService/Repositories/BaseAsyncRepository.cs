using AutoMapper;
using Microsoft.EntityFrameworkCore;
using PMA_TasksService.Data;
using PMA_TasksService.Repositories.Interfaces;
using System.Linq.Expressions;

namespace PMA_TasksService.Repositories
{
    public class BaseAsyncRepository<T,U> : IBaseAsyncRepository<T,U> where U : class
    {
        protected readonly DbContext _dbContext;
        protected readonly IMapper _mapper;

        public BaseAsyncRepository(DbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        public async Task<IReadOnlyList<T>> GetAllAsync(int limit = 100)
        {
            var Entities = await _dbContext.Set<U>().Take(limit).ToListAsync();

            return _mapper.Map<IReadOnlyList<T>>(Entities);
        }

        public async Task<IReadOnlyList<T>> GetAsync(Expression<Func<U, bool>> predicate)
        {
            var Entities = await _dbContext.Set<U>().Where(predicate).ToListAsync();

            return _mapper.Map<IReadOnlyList<T>>(Entities);
        }

        public async Task<IReadOnlyList<T>> GetAsync(Expression<Func<U, bool>> predicate = null, Func<IQueryable<U>, IOrderedQueryable<U>> orderBy = null, string includeString = null, bool disableTracking = true)
        {
            IQueryable<U> query = _dbContext.Set<U>();

            List<U> Entities;

            if (disableTracking) query = query.AsNoTracking();

            if (!string.IsNullOrWhiteSpace(includeString)) query = query.Include(includeString);

            if (predicate != null) query = query.Where(predicate);


            if (orderBy != null)
            {
                Entities = await orderBy(query).ToListAsync();
            }

            else
            {
                Entities = await query.ToListAsync();
            }


            return _mapper.Map<IReadOnlyList<T>>(Entities);
            
        }

        public async Task<IReadOnlyList<T>> GetAsync(Expression<Func<U, bool>> predicate = null, Func<IQueryable<U>, IOrderedQueryable<U>> orderBy = null, List<Expression<Func<U, object>>> includes = null, bool disableTracking = true, int limit = 100)
        {
            IQueryable<U> query = _dbContext.Set<U>();

            List<U> Entities;

            if (disableTracking) query = query.AsNoTracking();

            if (includes != null) query = includes.Aggregate(query, (current, include) => current.Include(include));

            if (predicate != null) query = query.Where(predicate);

            query.Take(limit);

            if (orderBy != null)
            {
                Entities = await orderBy(query).ToListAsync();
            }
            else
            {
                Entities = await query.ToListAsync();
            }

            return _mapper.Map<IReadOnlyList<T>>(Entities);
        }

        public virtual async Task<T> GetByIdAsync(int id)
        {
            var Entity = await _dbContext.Set<U>().FindAsync(id);

            return _mapper.Map<T>(Entity);
        }

        public async Task<T> AddAsync(T entity)
        {
            var entityModel = _mapper.Map<U>(entity);
            var addedEntity = _dbContext.Set<U>().Add(entityModel);
            await _dbContext.SaveChangesAsync();
            return _mapper.Map<T>(addedEntity.Entity); 
        }

        public async Task<T> UpdateAsync(T entity)
        {
            var entityModel = _mapper.Map<U>(entity);

            _dbContext.Set<U>().Update(entityModel);

            await _dbContext.SaveChangesAsync();

            return entity;
        }

        public async Task<T> DeleteAsync(T entity)
        {
            var entityModel = _mapper.Map<U>(entity);

            _dbContext.Set<U>().Remove(entityModel);

            await _dbContext.SaveChangesAsync();

            return entity;
        }
    }
}
