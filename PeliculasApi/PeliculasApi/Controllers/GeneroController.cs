using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ActionConstraints;
using Microsoft.EntityFrameworkCore;
using PeliculasApi.DTOs;
using PeliculasApi.Entidades;

namespace PeliculasApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GeneroController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;

        public GeneroController(ApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        [HttpGet()]
        public async Task<IActionResult> GetAll() 
        {
            var result = _mapper.Map<List<GeneroDTO>>(await _context.Generos
                .OrderBy(genre => genre.Id)
                .ToListAsync());

            return Ok(result);
        }

        [HttpGet("{id:int}", Name = "ObtenterGeneroPorId")]
        public async Task<IActionResult> GetById(int id)
        {
            var result = await _context.Generos.FirstOrDefaultAsync(genre => genre.Id == id);

            if(result == null) return NotFound($"Genero {id} no encontrado");

            var mappedResult = _mapper.Map<GeneroDTO>(result);
            return Ok(mappedResult);
        }

        [HttpPost]
        public async Task<IActionResult> Create(GeneroCreacionDTO generoCreacionDTO)
        {
            var result = _mapper.Map<Genero>(generoCreacionDTO);
            _context.Generos.Add(result);
            await _context.SaveChangesAsync();

            return CreatedAtRoute("ObtenterGeneroPorId", new { id = result.Id }, result);
        }

        [HttpPut("{id:int}")]
        public async Task<IActionResult> Update(int id, GeneroCreacionDTO generoCreacionDTO)
        {
            var result = _mapper.Map<Genero>(generoCreacionDTO);
            result.Id = id;

            _context.Generos.Update(result);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> Delete(int id)
        {
            var exists = await _context.Generos.AnyAsync(genre => genre.Id == id);

            if(!exists) return NotFound();

            _context.Remove(new Genero() { Id = id});
            await _context.SaveChangesAsync();
            
            return NoContent();
        }
    }
}