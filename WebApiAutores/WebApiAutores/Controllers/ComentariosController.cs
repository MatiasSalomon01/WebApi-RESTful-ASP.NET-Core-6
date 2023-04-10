using AutoMapper;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
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
        private readonly UserManager<IdentityUser> userManager;

        public ComentariosController(ApplicationDbContext context, IMapper mapper, UserManager<IdentityUser> userManager)
        {
            _context = context;
            _mapper = mapper;
            this.userManager = userManager;
        }
        [HttpGet]
        public async Task<ActionResult<List<ComentarioDTO>>> GetAll(int libroId)
        {
            var comentarios = await _context.Comentarios
                .Where(c => c.LibroId == libroId)
                .ToListAsync();
            return Ok(_mapper.Map<List<ComentarioDTO>>(comentarios));

        }

        [HttpGet("{id:int}", Name = "obtenerComentario")]
        public async Task<ActionResult<ComentarioDTO>> GetById(int id)
        {
            var comentario = await _context.Comentarios.FirstOrDefaultAsync(c => c.Id == id);
            if(comentario == null) return NotFound();


            return _mapper.Map<ComentarioDTO>(comentario);

        }

        [HttpPost]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<ActionResult> Create(int libroId, ComentarioCreacionDTO comentarioCreacionDTO) 
        {

            var emailClaim = HttpContext.User.Claims.Where(c => c.Type == "email").FirstOrDefault();
            var email = emailClaim.Value;

            var usuario = await userManager.FindByEmailAsync(email);
            var usuarioId = usuario.Id;

            var result = await _context.Libros.AnyAsync(l => l.Id == libroId);
            if (!result) return NotFound();

            var comentario = _mapper.Map<Comentario>(comentarioCreacionDTO);
            comentario.LibroId = libroId;
            comentario.UsuarioId = usuarioId;
            _context.Add(comentario);
            await _context.SaveChangesAsync();

            var comentarioDTO = _mapper.Map<ComentarioDTO>(comentario);

            return CreatedAtRoute("obtenerComentario", new { id = comentario.Id, libroId }, comentarioDTO);
        }

        [HttpPut("{id:int}")]
        public async Task<ActionResult> Put(int libroId, int id, ComentarioCreacionDTO comentarioCreacionDTO)
        {
            var existeLibro = await _context.Libros.AnyAsync(l => l.Id == libroId);
            if(!existeLibro) return NotFound();
            var existeComentario = await _context.Comentarios.AnyAsync(l => l.Id == id);
            if (!existeComentario) return NotFound();

            var comentario = _mapper.Map<Comentario>(comentarioCreacionDTO);
            comentario.Id = id;
            comentario.LibroId = libroId;
            _context.Comentarios.Update(comentario);
            await _context.SaveChangesAsync();
            return NoContent();
        }
    }
}
