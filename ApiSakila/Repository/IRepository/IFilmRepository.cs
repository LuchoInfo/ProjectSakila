using ApiSakila.Models;

namespace ApiSakila.Repository.IRepository
{
    public interface IFilmRepository : IRepository<Film>
    {
        Task<Film> Update(Film entity);
    }
}
