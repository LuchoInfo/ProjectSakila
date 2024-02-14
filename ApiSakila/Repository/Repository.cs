using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using System.Linq;
using ApiSakila.Repository.IRepository;
using ApiSakila.Models;

namespace ApiSakila.Repository
{
    public class Repository<T> : IRepository<T> where T : class
    {
        private readonly SakilaDbContext _dbContext;
        internal DbSet<T> dbSet;

        public Repository(SakilaDbContext db)
        {
            _dbContext = db;
            this.dbSet = _dbContext.Set<T>();
        }


        public async Task Create(T entity)
        {
            await dbSet.AddAsync(entity);
            await Engrave();
        }

        public async Task Engrave()
        {
            await _dbContext.SaveChangesAsync();
        }

        public async Task<T> Get(Expression<Func<T, bool>> filter = null, bool tracked = true, string? includeProperties = null)
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

            if (includeProperties != null)  // "Villa,OtroModelo"
            {
                foreach (var includeProp in includeProperties.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
                {
                    query = query.Include(includeProp);
                }
            }

            return await query.FirstOrDefaultAsync();
        }

        public async Task<List<T>> GetAll(Expression<Func<T, bool>>? filter = null, string? includeProperties = null)
        {
            IQueryable<T> query = dbSet;
            if (filter != null)
            {
                query = query.Where(filter);
            }
            if (includeProperties != null)  // "Villa,OtroModelo"
            {
                foreach (var includeProp in includeProperties.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
                {
                    query = query.Include(includeProp);
                }
            }
            return await query.ToListAsync();
        }

        public async Task Delete(T entity)
        {
            dbSet.Remove(entity);
            await Engrave();
        }
    }
}
