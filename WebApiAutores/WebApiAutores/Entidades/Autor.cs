using System.ComponentModel.DataAnnotations;

namespace WebApiAutores.Entidades
{
    public class Autor
    {
        public int Id{ get; set; }
        [Required]
        public string Nombre { get; set; }
        public List<AutorLibro> AutoresLibros { get; set; }

    }
}                                 
                                  