using PeliculasApi.Validaciones;
using System.ComponentModel.DataAnnotations;

namespace PeliculasApi.DTOs
{
    public class PeliculaCreacionDTO: PeliculaPatchDTO
    {
        [PesoArchivoValidacion(4)]
        [TipoArchivoValidaciones(GrupoTipoArchivos.Imagen)]
        public IFormFile Poster { get; set; }
    }
}
