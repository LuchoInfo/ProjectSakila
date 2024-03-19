using ApiSakila.Models;
using ApiSakila.Repository.IRepository;

namespace ApiSakila.Repository
{
    public class FilmRepository : Repository<Film>, IFilmRepository
    {
        private readonly SakilaDbContext _dbContext;

        public FilmRepository(SakilaDbContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Film> Update(Film entity)
        {
            entity.LastUpdate = DateTime.Now;
            _dbContext.Films.Update(entity);
            await _dbContext.SaveChangesAsync();
            return entity;
        }
    }
}
