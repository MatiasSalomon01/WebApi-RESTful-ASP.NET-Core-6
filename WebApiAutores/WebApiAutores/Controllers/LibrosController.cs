using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApiAutores.Entidades;

namespace WebApiAutores.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class LibrosController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public LibrosController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult<Libro>> Get(int id)
        {
            return await _context.Libros.FirstOrDefaultAsync(l => l.Id == id);
        }

        //[HttpPost]
        //public async Task<ActionResult> Post(Libro libro)
        //{
        //    var result = await _context.Autores.AnyAsync(l => l.Id == libro.AutorId);
        //    if (!result) return BadRequest($"No existe el autor Id: {libro.AutorId}");

        //    _context.Libros.Add(libro);
        //    await _context.SaveChangesAsync();
        //    return Ok();
        //}
    }
}
