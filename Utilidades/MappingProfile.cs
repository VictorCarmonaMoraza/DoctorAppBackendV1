using AutoMapper;
using Models.DTOs;
using Models.Entities;

namespace Utilidades
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Especialidad,EspecialidadDto>()
                .ForMember(dest => dest.Estado, mapper => mapper.MapFrom(src => src.Estado ? 1 : 0));
        }
    }
}
