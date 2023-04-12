using AutoMapper;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PeliculasApi.DTOs;
using PeliculasApi.Entidades;
using PeliculasApi.Migrations;
using PeliculasApi.Servicios;

namespace PeliculasApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PeliculaController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;
        private readonly IAlmacenadorArchivos _almacenadorArchivos;
        private readonly string contenedor = "movies";

        public PeliculaController(ApplicationDbContext context, IMapper mapper, IAlmacenadorArchivos almacenadorArchivos)
        {
            _context = context;
            _mapper = mapper;
            _almacenadorArchivos = almacenadorArchivos;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var result = _mapper.Map<List<PeliculaDTO>>(await _context.Peliculas.ToListAsync());
            return Ok(result);
        }

        [HttpGet("{id:int}", Name = "ObtenerPeliculaPorId")]
        public async Task<IActionResult> GetById(int id)
        {
            var pelicula = await _context.Peliculas.FirstOrDefaultAsync(p => p.Id == id);
            if(pelicula== null) return NotFound();

            var result = _mapper.Map<PeliculaDTO>(pelicula);
            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromForm] PeliculaCreacionDTO peliculaCreacionDTO)
        {
            var pelicula = _mapper.Map<Pelicula>(peliculaCreacionDTO);

            if (peliculaCreacionDTO.Poster != null)
            {
                using (var memoryStream = new MemoryStream())
                {
                    await peliculaCreacionDTO.Poster.CopyToAsync(memoryStream);
                    var contenido = memoryStream.ToArray();
                    var extension = Path.GetExtension(peliculaCreacionDTO.Poster.FileName);
                    pelicula.Poster = await _almacenadorArchivos.GuardarArchivo(contenido, extension, contenedor, peliculaCreacionDTO.Poster.ContentType);
                }
            }
            AsignarOrdenActores(pelicula);
            _context.Peliculas.Add(pelicula);
            await _context.SaveChangesAsync();

            var result = _mapper.Map<PeliculaDTO>(pelicula);
            return CreatedAtRoute("ObtenerPeliculaPorId", new { id = result.Id }, result);
        }

        private void AsignarOrdenActores(Pelicula pelicula)
        {
            if(pelicula.PeliculaActores != null)
            {
                for(int i = 0; i < pelicula.PeliculaActores.Count; i++)
                {
                    pelicula.PeliculaActores[i].Orden = i;
                }
            }
        }

        [HttpPut("{id:int}")]
        public async Task<IActionResult> Update(int id, [FromForm] PeliculaCreacionDTO peliculaCreacionDTO)
        {
            var peliculaDB = await _context.Peliculas
                .Include(a => a.PeliculaActores)
                .Include(a => a.PeliculaGeneros)
                .FirstOrDefaultAsync(a => a.Id == id);

            if (peliculaDB == null) return NotFound();

            peliculaDB = _mapper.Map(peliculaCreacionDTO, peliculaDB);

            if (peliculaDB.Poster != null)
            {
                using (var memoryStream = new MemoryStream())
                {
                    await peliculaCreacionDTO.Poster.CopyToAsync(memoryStream);
                    var contenido = memoryStream.ToArray();
                    var extension = Path.GetExtension(peliculaCreacionDTO.Poster.FileName);
                    peliculaDB.Poster = await _almacenadorArchivos.EditarArchivo(contenido, extension, contenedor, peliculaDB.Poster, peliculaCreacionDTO.Poster.ContentType);
                }
            }
            AsignarOrdenActores(peliculaDB);
            _context.Peliculas.Update(peliculaDB);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        [HttpPatch("{id:int}")]
        public async Task<IActionResult> Patch(int id, [FromBody] JsonPatchDocument<PeliculaPatchDTO> patchDocument)
        {
            if (patchDocument == null) return BadRequest();

            var entidadDB = await _context.Peliculas.FirstOrDefaultAsync(a => a.Id == id);
            if (entidadDB == null) return NotFound();

            var entidadDTO = _mapper.Map<PeliculaPatchDTO>(entidadDB);
            patchDocument.ApplyTo(entidadDTO, ModelState);

            var esValido = TryValidateModel(entidadDTO);
            if (!esValido) return BadRequest(ModelState);

            _mapper.Map(entidadDTO, entidadDB);

            await _context.SaveChangesAsync();
            return NoContent();
        }

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> Delete(int id)
        {
            var exists = await _context.Peliculas.AnyAsync(actor => actor.Id == id);

            if (!exists) return NotFound();

            _context.Peliculas.Remove(new Pelicula() { Id = id });
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}
