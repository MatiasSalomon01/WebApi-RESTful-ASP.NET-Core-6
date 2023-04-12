using PeliculasApi.Validaciones;
using System.ComponentModel.DataAnnotations;

namespace PeliculasApi.DTOs
{
    public class ActorCreacionDTO : ActorPatchDTO
    {
        [PesoArchivoValidacion(4)] //en MB
        [TipoArchivoValidaciones(GrupoTipoArchivos.Imagen)]
        public IFormFile Foto { get; set; }
    }
}
