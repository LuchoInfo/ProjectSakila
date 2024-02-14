using ApiSakila.Models;

namespace ApiSakila.Repository.IRepository
{
    public interface ILanguageRepository : IRepository<Language>
    {
        Task<Language> Update(Language entity);
    }
}
