using System.ComponentModel.DataAnnotations;

namespace PeliculasApi.Entidades
{
    public class Genero: IId
    {
        public int Id{ get; set; }
        [Required]
        [MaxLength(50)]
        public string Nombre{ get; set; }

        public List<PeliculaGenero> PeliculaGeneros { get; set; }
    }
}
