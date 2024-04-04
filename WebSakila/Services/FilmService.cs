using UtilitySakila;
using WebSakila.Models;
using WebSakila.Services.IServices;
using WebSakila.Models.Dto;

namespace WebSakila.Services
{
    public class FilmService : BaseService, IFilmService
    {
        public readonly IHttpClientFactory _httpClient;
        private string _filmUrl;
        public FilmService(IHttpClientFactory httpClient, IConfiguration configuration) : base(httpClient)
        {
            _httpClient = httpClient;
            _filmUrl = configuration.GetValue<string>("ServiceUrls:API_URL");
        }

        public Task<T> Update<T>(FilmUpdateDto dto)
        {
            return SendAsync<T>(new APIRequest()
            {
                APIType = DS.APIType.PUT,
                Data = dto,
                Url = _filmUrl + "/api/Film/" + dto.FilmId
            });
        }

        public Task<T> Create<T>(FilmCreateDto dto)
        {
            return SendAsync<T>(new APIRequest()
            {
                APIType = DS.APIType.POST,
                Data = dto,
                Url = _filmUrl + "/api/Film"
            });
        }

        public Task<T> Get<T>(int id)
        {
            return SendAsync<T>(new APIRequest()
            {
                APIType = DS.APIType.GET,
                Url = _filmUrl + "/api/Film/" + id
            });
        }

        public Task<T> GetAll<T>()
        {
            return SendAsync<T>(new APIRequest()
            {
                APIType = DS.APIType.GET,
                Url = _filmUrl + "/api/Film"
            });
        }

        public Task<T> Delete<T>(int id)
        {
            return SendAsync<T>(new APIRequest()
            {
                APIType = DS.APIType.DELETE,
                Url = _filmUrl + "/api/Film/" + id
            });
        }
    }
}

