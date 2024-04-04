using AutoMapper;
using WebSakila.Models.Dto;

namespace WebSakila
{
    public class MappingConfig : Profile
    {
        public MappingConfig()
        {
            CreateMap<FilmDto, FilmCreateDto>().ReverseMap();
            CreateMap<FilmDto, FilmUpdateDto>().ReverseMap();

        }
    }
}
