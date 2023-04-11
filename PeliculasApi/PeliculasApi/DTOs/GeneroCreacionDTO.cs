using System.ComponentModel.DataAnnotations;

namespace PeliculasApi.DTOs
{
    public class GeneroCreacionDTO
    {
        [Required]
        [MaxLength(50)]
        public string Nombre{ get; set; }
    }
}
