using AutoMapper;
using PeliculasApi.DTOs;
using PeliculasApi.Entidades;

namespace PeliculasApi.Helpers
{
    public class AutomapperProfiles: Profile
    {
        public AutomapperProfiles()
        {
            CreateMap<Genero, GeneroDTO>().ReverseMap();
            CreateMap<GeneroCreacionDTO, Genero>();

            CreateMap<Actor, ActorDTO>().ReverseMap();
            CreateMap<ActorCreacionDTO, Actor>()
                .ForMember(src => src.Foto, options => options.Ignore());
            CreateMap<ActorPatchDTO, Actor>().ReverseMap();
            
            CreateMap<Pelicula, PeliculaDTO>().ReverseMap();
            CreateMap<PeliculaCreacionDTO, Pelicula>()
                .ForMember(src => src.Poster, options => options.Ignore())
                .ForMember(src => src.PeliculaActores, options => options.MapFrom(MapPeliculaActores))
                .ForMember(src => src.PeliculaGeneros, options => options.MapFrom(MapPeliculaGeneros));

            CreateMap<PeliculaPatchDTO, Pelicula>().ReverseMap();
        }

        private List<PeliculaGenero> MapPeliculaGeneros(PeliculaCreacionDTO peliculaCreacionDTO, Pelicula pelicula)
        {
            var result = new List<PeliculaGenero>();
            if (peliculaCreacionDTO.GeneroIds == null) return result;

            foreach (var id in peliculaCreacionDTO.GeneroIds)
            {
                result.Add(new PeliculaGenero { GeneroId = id });
            }

            return result;
        }

        private List<PeliculaActor> MapPeliculaActores(PeliculaCreacionDTO peliculaCreacionDTO, Pelicula pelicula)
        {
            var result = new List<PeliculaActor>();
            if (peliculaCreacionDTO.GeneroIds == null) return result;

            foreach (var actor in peliculaCreacionDTO.Actores)
            {
                result.Add(new PeliculaActor { ActorId = actor.ActorId, Personaje = actor.Personaje });
            }

            return result;
        }
    }
}
