using System;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using VillaAPI.Data;
using VillaAPI.Logger;
using VillaAPI.models;

namespace VillaAPI.Repository
{
	public class Repository<T> : IRepository<T> where T : class
	{
        
        private readonly ApplicationDbContext _db;
        //private readonly ILogging _logger;
        //private readonly IMapper _mapper;
        internal DbSet<T> dbSet;
        //public Repository(ILogging logger, ApplicationDbContext dbContext, IMapper mapper)
        //{
        //    _logger = logger;
        //    _db = dbContext;
        //    _mapper = mapper;
        //    this.dbSet = _db.Set<T>();

        //}
        public Repository(ApplicationDbContext dbContext)
        {
            _db = dbContext;
            this.dbSet = _db.Set<T>();

        }

        public async Task CreateAsync(T entity)
        {
           // _logger.Log("create the villas", "");
            await dbSet.AddAsync(entity);
            await SaveAsync();
        }

        public async Task<T> GetAsync(Expression<Func<T, bool>> filter = null, bool tracked = true)
        {
            IQueryable<T> query = dbSet;
            if (!tracked)
            {
                query = query.AsNoTracking();
            }
            if (filter != null)
            {
                query = query.Where(filter);
            }
            return await query.FirstOrDefaultAsync();
        }

        public async Task<List<T>> GetAllAsync(Expression<Func<T, bool>>? filter = null)
        {
            // This query is not exicuting here. 
            IQueryable<T> query = dbSet;
            if (filter != null)
            {
                query = query.Where(filter);
            }
            // query will exicute here that is called deferred exicution. Tolist() cause immediate exicution.
            return await query.ToListAsync();

        }

        public async Task RemoveAsync(T entity)
        {
            dbSet.Remove(entity);
            await SaveAsync();
        }

        public async Task SaveAsync()
        {
            await _db.SaveChangesAsync();
        }
    }
}

