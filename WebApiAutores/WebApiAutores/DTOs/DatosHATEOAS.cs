namespace WebApiAutores.DTOs
{
    public class DatosHATEOAS
    {
        public string Enlace{ get; private set; }
        public string Descripcion{ get; private set; }
        public string Metodo{ get; private set; }

        public DatosHATEOAS(string enlace, string descripcion, string metodo)
        {
            Enlace = enlace;
            Descripcion = descripcion;
            Metodo = metodo;
        }
    }
}
