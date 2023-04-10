using AutoMapper;
using AutoMapper.QueryableExtensions;
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
        [HttpGet]
        public async Task<ActionResult<List<LibroDTO>>> GetAll()
        {
            var result = await _context.Libros.ProjectTo<LibroDTO>(_mapper.ConfigurationProvider).ToListAsync();
            return result;
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult<LibroDTO>> GetById(int id)
        {
            var result = await _context.Libros
                .Include(l => l.Comentarios)
                .Include(l => l.AutoresLibros)
                .ThenInclude(l => l.Autor)
                .FirstOrDefaultAsync(l => l.Id == id);
            return _mapper.Map<LibroDTO>(result);
        }

        [HttpPost]
        public async Task<ActionResult> Post(LibroCreacionDTO libro)
        {
            //var result = await _context.Autores.AnyAsync(l => l.Id == libro.AutorId);
            //if (!result) return BadRequest($"No existe el autor Id: {libro.AutorId}");

            if(libro.AutoresId == null) return BadRequest("No se puede creaer un libro sin autores");

            var autoresId = await _context.Autores
                .Where(a => libro.AutoresId.Contains(a.Id))
                .Select(a => a.Id)
                .ToListAsync();

            if(libro.AutoresId.Count != autoresId.Count)
            {
                return BadRequest("No existe uno de los autores enviados");
            }

            _context.Libros.Add(_mapper.Map<Libro>(libro));
            await _context.SaveChangesAsync();
            return Ok();
        }
    }
}
