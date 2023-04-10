using System.ComponentModel.DataAnnotations;

namespace WebApiAutores.DTOs
{
    public class LibroCreacionDTO
    {
        [Required]
        public string Titulo{ get; set; }
        public DateTime FechaPublicacion{ get; set; }
        public List<int> AutoresId{ get; set; }
    }
}
