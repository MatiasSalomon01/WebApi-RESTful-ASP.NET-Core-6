using System.ComponentModel.DataAnnotations;

namespace WebApiAutores.DTOs
{
    public class CrendencialesUsuario
    {
        [Required]
        [EmailAddress]
        public string Email{ get; set; }
        [Required]
        public string Password{ get; set; }
    }
}
