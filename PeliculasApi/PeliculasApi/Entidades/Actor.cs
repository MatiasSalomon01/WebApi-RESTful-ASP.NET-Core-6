using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace PeliculasApi.Entidades
{
    public class Actor
    {
        public int Id{ get; set; }
        [Required]
        [MaxLength(120)]
        public string Nombre{ get; set; }
        public DateTime FechaNacimiento{ get; set; }
        public string Foto{ get; set; }

        public List<PeliculaActor> PeliculaActores { get; set; }

    }
}
