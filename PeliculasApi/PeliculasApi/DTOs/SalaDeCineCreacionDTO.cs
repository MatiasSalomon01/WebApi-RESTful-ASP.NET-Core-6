using System.ComponentModel.DataAnnotations;

namespace PeliculasApi.DTOs
{
    public class SalaDeCineCreacionDTO
    {
        [Required]
        [MaxLength(120)]
        public string Nombre { get; set; }

    }
}
