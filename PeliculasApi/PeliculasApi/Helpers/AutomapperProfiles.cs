using AutoMapper;
using NetTopologySuite;
using NetTopologySuite.Geometries;
using PeliculasApi.DTOs;
using PeliculasApi.Entidades;

namespace PeliculasApi.Helpers
{
    public class AutomapperProfiles: Profile
    {
        public AutomapperProfiles(GeometryFactory geometryFactory)
        {
            CreateMap<Genero, GeneroDTO>().ReverseMap();
            CreateMap<GeneroCreacionDTO, Genero>();

            CreateMap<Actor, ActorDTO>().ReverseMap();
            CreateMap<ActorCreacionDTO, Actor>()
                .ForMember(src => src.Foto, opt => opt.Ignore());
            CreateMap<ActorPatchDTO, Actor>().ReverseMap();
            
            CreateMap<Pelicula, PeliculaDTO>().ReverseMap();
            CreateMap<PeliculaCreacionDTO, Pelicula>()
                .ForMember(src => src.Poster, opt => opt.Ignore())
                .ForMember(src => src.PeliculaActores, opt => opt.MapFrom(MapPeliculaActores))
                .ForMember(src => src.PeliculaGeneros, opt => opt.MapFrom(MapPeliculaGeneros));

            CreateMap<PeliculaPatchDTO, Pelicula>().ReverseMap();

            CreateMap<Pelicula, PeliculaDetallesDTO>()
                .ForMember(x => x.Generos, opt => opt.MapFrom(MapPeliculaGeneros))
                .ForMember(x => x.Actores, opt => opt.MapFrom(MapPeliculaActores));

            CreateMap<SalaDeCine, SalaDeCineDTO>()
                .ForMember(x => x.Latitud, opt => opt.MapFrom(y => y.Ubicacion.Y))
                .ForMember(x => x.Longitud, opt => opt.MapFrom(y => y.Ubicacion.X));

            CreateMap<SalaDeCineDTO, SalaDeCine>()
                .ForMember(x => x.Ubicacion, opt => opt.MapFrom(y =>
                    geometryFactory.CreatePoint(new Coordinate(y.Longitud, y.Latitud))));

            CreateMap<SalaDeCineCreacionDTO, SalaDeCine>()
                .ForMember(x => x.Ubicacion, opt => opt.MapFrom(y =>
                    geometryFactory.CreatePoint(new Coordinate(y.Longitud, y.Latitud))));
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
                result.Add(new PeliculaActor
                { 
                    ActorId = actor.ActorId, 
                    Personaje = actor.Personaje 
                });
            }

            return result;
        }

        private List<GeneroDTO> MapPeliculaGeneros(Pelicula pelicula, PeliculaDetallesDTO peliculaDetallesDTO)
        {
            var result = new List<GeneroDTO>();
            if (pelicula.PeliculaGeneros == null) return result;

            foreach (var generoPelicula in pelicula.PeliculaGeneros)
            {
                result.Add(new GeneroDTO
                {
                    Id = generoPelicula.GeneroId, 
                    Nombre = generoPelicula.Genero.Nombre 
                });
            }

            return result;
        }

        private List<ActorPeliculaDetalleDTO> MapPeliculaActores(Pelicula pelicula, PeliculaDetallesDTO peliculaDetallesDTO)
        {
            var result = new List<ActorPeliculaDetalleDTO>();
            if (pelicula.PeliculaActores == null) return result;

            foreach (var actorPelicula in pelicula.PeliculaActores)
            {
                result.Add(new ActorPeliculaDetalleDTO
                {
                    ActorId = actorPelicula.ActorId, 
                    Personaje = actorPelicula.Personaje, 
                    NombrePersona = actorPelicula.Actor.Nombre
                });
            }

            return result;
        }
    }
}
