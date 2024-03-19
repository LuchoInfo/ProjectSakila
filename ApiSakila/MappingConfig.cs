using ApiSakila.Models;
using ApiSakila.Models.Dto;
using AutoMapper;

namespace ApiSakila
{
    public class MappingConfig : Profile
    {
        public MappingConfig() 
        {
            CreateMap<Language, LanguageDto>().ReverseMap();
            CreateMap<Film, FilmDto>().ReverseMap();
            CreateMap<Film, FilmDto>().ReverseMap();

            CreateMap<Film, FilmCreateDto>().ReverseMap();
            CreateMap<Film, FilmUpdateDto>().ReverseMap();
            //CreateMap<Language, LanguageCreateDto>().ReverseMap();
        }
    }
}
