using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebApiAutores.Entidades
{
    public class Libro
    {
        public int Id{ get; set; }
        [Required]
        public string Titulo{ get; set; }
        [Column(TypeName = "Timestamp")]
        public DateTime? FechaPublicacion { get; set; }
        public List<Comentario> Comentarios{ get; set; }
        public List<AutorLibro> AutoresLibros{ get; set; }
    }
}
