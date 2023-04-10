using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Npgsql.Internal.TypeHandlers;
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

        [HttpGet("{id:int}", Name = "obtenerLibro")]
        public async Task<ActionResult<LibroDTOConAutores>> GetById(int id)
        {
            var result = await _context.Libros
                .Include(l => l.Comentarios)
                .Include(l => l.AutoresLibros)
                .ThenInclude(l => l.Autor)
                .FirstOrDefaultAsync(l => l.Id == id);
            return _mapper.Map<LibroDTOConAutores>(result);
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
            var result = _mapper.Map<Libro>(libro);

            _context.Libros.Add(result);
            await _context.SaveChangesAsync();

            var libroDTO = _mapper.Map<LibroDTO>(result);

            return CreatedAtRoute("obtenerLibro", new { id = libroDTO.Id }, libroDTO);
        }

        [HttpPut("{id:int}")]
        public async Task<ActionResult> Put(int id, LibroCreacionDTO libroCreacionDTO)
        {
            var libroDB = await _context.Libros
                .Include(a => a.AutoresLibros)
                .FirstOrDefaultAsync(x => x.Id == id);

            if (libroDB == null) return NotFound();

            libroDB = _mapper.Map(libroCreacionDTO, libroDB);

            await _context.SaveChangesAsync();

            return NoContent();
        }

        [HttpPatch("{id:int}")]
        public async Task<ActionResult> Patch(int id, JsonPatchDocument<LibroPatchDTO> patchDocument)
        {
            if (patchDocument == null) return BadRequest();

            var libroDB = await _context.Libros.FirstOrDefaultAsync(l => l.Id == id);
            if(libroDB == null) return NotFound();

            var libroDTO = _mapper.Map<LibroPatchDTO>(libroDB);
            patchDocument.ApplyTo(libroDTO, ModelState);

            var esValido = TryValidateModel(libroDTO);
            if(!esValido) return BadRequest(ModelState);

            _mapper.Map(libroDTO, libroDB);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        [HttpDelete("{id:int}")]
        public async Task<ActionResult> Delete(int id)
        {
            var result = await _context.Libros.AnyAsync(a => a.Id == id);
            if (!result) return NotFound();

            _context.Remove(new Libro() { Id = id });
            await _context.SaveChangesAsync();
            return Ok();
        }
    }
}
