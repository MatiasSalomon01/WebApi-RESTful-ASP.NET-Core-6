using NetTopologySuite.Geometries;
using System.ComponentModel.DataAnnotations;

namespace PeliculasApi.Entidades
{
    public class SalaDeCine : IId
    {
        public int Id { get; set; }
        [Required]
        [MaxLength(120)]
        public string Nombre { get; set;}
        public Point Ubicacion { get; set;}
        public List<PeliculaSalaDeCine> PeliculasSalaDeCines { get; set; }
    }
}
