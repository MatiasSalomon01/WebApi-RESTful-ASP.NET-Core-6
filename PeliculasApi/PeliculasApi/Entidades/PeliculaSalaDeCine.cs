namespace PeliculasApi.Entidades
{
    public class PeliculaSalaDeCine
    {
        public int PeliculaId { get; set; }
        public int SalaDeCineId { get; set; }
        public Pelicula Pelicula { get; set; }
        public SalaDeCine SalaDeCine { get; set; }
    }
}
