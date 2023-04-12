using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

namespace PeliculasApi.DTOs
{
    public class PaginacionDTO
    {
        public int Pagina { get; set; } = 1;
        private int cantidadRegistrosPorPagina = 10;
        private readonly int cantidadMaximaRegistroPorPagina = 50;
        public int CantidadRegistrosPorPagina
        {
            get => cantidadRegistrosPorPagina;  
            set
            {
                cantidadRegistrosPorPagina = (value > cantidadMaximaRegistroPorPagina) ? cantidadMaximaRegistroPorPagina : value;
            }
        }

    }
}
