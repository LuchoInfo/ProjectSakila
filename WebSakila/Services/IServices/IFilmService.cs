using WebSakila.Models.Dto;

namespace WebSakila.Services.IServices
{
    public interface IFilmService
    {
        Task<T> GetAll<T>();
        Task<T> Get<T>(int id);
        Task<T> Create<T>(FilmCreateDto dto);
        Task<T> Update<T>(FilmUpdateDto dto);
        Task<T> Delete<T>(int id);
    }
}

