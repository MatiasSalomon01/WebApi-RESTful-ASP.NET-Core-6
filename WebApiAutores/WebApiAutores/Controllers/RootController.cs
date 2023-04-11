using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebApiAutores.DTOs;

namespace WebApiAutores.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RootController : ControllerBase
    {
        [HttpGet(Name = "obtener-root")]
        public ActionResult<IEnumerable<DatosHATEOAS>> Get() 
        {
            var datosHateoas = new List<DatosHATEOAS>
            {
                new DatosHATEOAS(enlace: Url.Link("obtener-root", new { }), descripcion: "self", metodo: "GET")
            };
            return datosHateoas;
        }
    }
}
