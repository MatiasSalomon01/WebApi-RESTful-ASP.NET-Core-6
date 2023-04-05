using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApiAutores.DTOs;
using WebApiAutores.Entidades;

namespace WebApiAutores.Controllers
{
    [ApiController]
    [Route("Libros/{libroId:int}/Comentarios")]
    public class ComentariosController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;
        public ComentariosController(ApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        [HttpGet]
        public async Task<ActionResult<List<ComentarioDTO>>> GetAll(int libroId)
        {
            var comentarios = await _context.Comentarios
                .Where(c => c.LibroId == libroId)
                .ToListAsync();
            return Ok(_mapper.Map<List<ComentarioDTO>>(comentarios));

        }

        [HttpPost]
        public async Task<ActionResult> Create(int libroId, ComentarioCreacionDTO comentarioDTO) 
        {
            var result = await _context.Libros.AnyAsync(l => l.Id == libroId);
            if (!result) return NotFound();

            var comentario = _mapper.Map<Comentario>(comentarioDTO);
            comentario.LibroId = libroId;
            _context.Add(comentario);
            await _context.SaveChangesAsync();
            return Ok();
        }
    }
}
