using AutoMapper;
using Films.Domain.DTOs;
using Films.Domain.Entities;

namespace Films.Application.Mappings;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        // Actor mappings
        CreateMap<Actor, ActorDto>()
            .ForMember(dest => dest.SexName, opt => opt.MapFrom(src => src.Sex != null ? src.Sex.Name : null));
        CreateMap<ActorCreateDto, Actor>();
        CreateMap<ActorUpdateDto, Actor>();

        // Film mappings
        CreateMap<Film, FilmDto>();
        CreateMap<FilmCreateDto, Film>();
        CreateMap<FilmUpdateDto, Film>();

        // DirectedBy mappings
        CreateMap<DirectedBy, DirectedByDto>();
        CreateMap<DirectedByCreateDto, DirectedBy>();
        CreateMap<DirectedByUpdateDto, DirectedBy>();
    }
}
