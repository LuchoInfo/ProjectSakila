using ApiSakila.Models;
using ApiSakila.Repository.IRepository;

namespace ApiSakila.Repository
{
    public class LanguageRepository : Repository<Language>, ILanguageRepository
    {
        private readonly SakilaDbContext _dbContext;
        public LanguageRepository(SakilaDbContext dbContext) :base(dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Language> Update(Language entity)
        {
            entity.LastUpdate = DateTime.Now;
            _dbContext.Languages.Update(entity);
            await _dbContext.SaveChangesAsync();
            return entity;
        }
    }
}
