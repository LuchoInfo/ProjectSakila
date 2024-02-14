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
        }
    }
}
