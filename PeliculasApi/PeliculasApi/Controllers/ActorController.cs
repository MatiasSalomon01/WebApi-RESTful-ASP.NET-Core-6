using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PeliculasApi.DTOs;
using PeliculasApi.Entidades;

namespace PeliculasApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ActorController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;

        public ActorController(ApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
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
        public async Task<IActionResult> Create(ActorCreacionDTO actorCreacionDTO)
        {
            var result = _mapper.Map<Actor>(actorCreacionDTO);
            _context.Actores.Add(result);
            await _context.SaveChangesAsync();

            var mappedResult = _mapper.Map<ActorDTO>(result);

            return CreatedAtRoute("ObtenerActorPorId", new { id = result.Id }, mappedResult);
        }

        [HttpPut("{id:int}")]
        public async Task<IActionResult> Update(int id, ActorCreacionDTO actorCreacionDTO)
        {
            var result = _mapper.Map<Actor>(actorCreacionDTO);
            result.Id = id;

            _context.Actores.Update(result);
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
