using PeliculasApi.Validaciones;
using System.ComponentModel.DataAnnotations;

namespace PeliculasApi.DTOs
{
    public class ActorCreacionDTO
    {
        [Required]
        [MaxLength(120)]
        public string Nombre { get; set; }
        public DateTime FechaNacimiento { get; set; }
        [PesoArchivoValidacion(4)] //en MB
        [TipoArchivoValidaciones(GrupoTipoArchivos.Imagen)]
        public IFormFile Foto { get; set; }
    }
}
