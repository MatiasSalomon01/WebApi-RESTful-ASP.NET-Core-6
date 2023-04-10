using System.ComponentModel.DataAnnotations;

namespace WebApiAutores.DTOs
{
    public class EditarAdminDTO
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }
    }
}
