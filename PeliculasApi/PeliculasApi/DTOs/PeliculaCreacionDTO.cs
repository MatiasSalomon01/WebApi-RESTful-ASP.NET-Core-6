using Microsoft.AspNetCore.Mvc;
using PeliculasApi.Helpers;
using PeliculasApi.Validaciones;

namespace PeliculasApi.DTOs
{
    public class PeliculaCreacionDTO: PeliculaPatchDTO
    {
        [PesoArchivoValidacion(4)]
        [TipoArchivoValidaciones(GrupoTipoArchivos.Imagen)]
        public IFormFile Poster { get; set; }
        [ModelBinder(BinderType = typeof(TypeBinder<List<int>>))]
        public List<int> GeneroIds { get; set; }
        [ModelBinder(BinderType = typeof(TypeBinder<List<ActorPeliculaDTO>>))]
        public List<ActorPeliculaDTO> Actores { get; set;}
    }
}
