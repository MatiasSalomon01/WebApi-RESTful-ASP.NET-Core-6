using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PeliculasApi.DTOs;
using PeliculasApi.Entidades;
using PeliculasApi.Servicios;

namespace PeliculasApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ActorController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;
        private readonly IAlmacenadorArchivos _almacenadorArchivos;
        private readonly string contenedor = "actors";

        public ActorController(ApplicationDbContext context, IMapper mapper, IAlmacenadorArchivos almacenadorArchivos)
        {
            _context = context;
            _mapper = mapper;
            _almacenadorArchivos = almacenadorArchivos;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var result = _mapper.Map<List<ActorDTO>>(await _context.Actores
                .OrderBy(actor => actor.Id)
                .ToListAsync());

            return Ok(result);
        }

        [HttpGet("{id:int}", Name = "ObtenerActorPorId")]
        public async Task<IActionResult> GetById(int id)
        {
            var result = await _context.Actores.FirstOrDefaultAsync(actor => actor.Id == id);

            if (result == null) return NotFound($"Actor {id} no encontrado");

            var mappedResult = _mapper.Map<ActorDTO>(result);
            return Ok(mappedResult);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromForm] ActorCreacionDTO actorCreacionDTO)
        {
            var result = _mapper.Map<Actor>(actorCreacionDTO);

            if(actorCreacionDTO.Foto != null)
            {
                using (var memoryStream = new MemoryStream())
                {
                    await actorCreacionDTO.Foto.CopyToAsync(memoryStream);
                    var contenido = memoryStream.ToArray();
                    var extension = Path.GetExtension(actorCreacionDTO.Foto.FileName);
                    result.Foto = await _almacenadorArchivos.GuardarArchivo(contenido, extension, contenedor, actorCreacionDTO.Foto.ContentType);
                }
            }

            _context.Actores.Add(result);
            await _context.SaveChangesAsync();

            var mappedResult = _mapper.Map<ActorDTO>(result);
            return CreatedAtRoute("ObtenerActorPorId", new { id = result.Id }, mappedResult);
        }

        [HttpPut("{id:int}")]
        public async Task<IActionResult> Update(int id, [FromForm] ActorCreacionDTO actorCreacionDTO)
        {
            var actorDB = await _context.Actores.FirstOrDefaultAsync(a => a.Id == id);

            if(actorDB == null) return NotFound();

            actorDB = _mapper.Map(actorCreacionDTO, actorDB);

            if(actorDB.Foto != null)
            {
                using (var memoryStream = new MemoryStream())
                {
                    await actorCreacionDTO.Foto.CopyToAsync(memoryStream);
                    var contenido = memoryStream.ToArray();
                    var extension = Path.GetExtension(actorCreacionDTO.Foto.FileName);
                    actorDB.Foto = await _almacenadorArchivos.EditarArchivo(contenido, extension, contenedor, actorDB.Foto, actorCreacionDTO.Foto.ContentType);
                }
            }
            _context.Actores.Update(actorDB);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> Delete(int id)
        {
            var exists = await _context.Actores.AnyAsync(actor => actor.Id == id);

            if (!exists) return NotFound();

            _context.Actores.Remove(new Actor() { Id = id });
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}
