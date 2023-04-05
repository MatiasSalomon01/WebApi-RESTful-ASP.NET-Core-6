using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApiAutores.DTOs;
using WebApiAutores.Entidades;

namespace WebApiAutores.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class LibrosController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;


        public LibrosController(ApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult<LibroDTO>> Get(int id)
        {
            var result = await _context.Libros.FirstOrDefaultAsync(l => l.Id == id);
            return _mapper.Map<LibroDTO>(result);
        }

        [HttpPost]
        public async Task<ActionResult> Post(LibroCreacionDTO libro)
        {
            //var result = await _context.Autores.AnyAsync(l => l.Id == libro.AutorId);
            //if (!result) return BadRequest($"No existe el autor Id: {libro.AutorId}");

            _context.Libros.Add(_mapper.Map<Libro>(libro));
            await _context.SaveChangesAsync();
            return Ok();
        }
    }
}
