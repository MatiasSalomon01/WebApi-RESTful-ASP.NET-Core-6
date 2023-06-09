﻿using AutoMapper;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PeliculasApi.DTOs;
using PeliculasApi.Entidades;
using PeliculasApi.Helpers;
using PeliculasApi.Migrations;
using PeliculasApi.Servicios;

namespace PeliculasApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PeliculaController : CustomBaseController
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;
        private readonly IAlmacenadorArchivos _almacenadorArchivos;
        private readonly string contenedor = "movies";

        public PeliculaController(ApplicationDbContext context, IMapper mapper, IAlmacenadorArchivos almacenadorArchivos): base(context, mapper)
        {
            _context = context;
            _mapper = mapper;
            _almacenadorArchivos = almacenadorArchivos;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var top = 5;
            var hoy = DateTime.Today;

            var proximosEstrenos = await _context.Peliculas
                .Where(x => x.FechaEstreno > hoy)
                .OrderBy(x => x.FechaEstreno)
                .Take(top).
                ToListAsync();

            var enCines = await _context.Peliculas
                .Where(x => x.EnCines)
                .Take(top)
                .ToListAsync();

            var resultado = new PeliculasIndexDTO
            {
                FuturosEstrenos = _mapper.Map<List<PeliculaDTO>>(proximosEstrenos),
                EnCines = _mapper.Map<List<PeliculaDTO>>(enCines)
            };

            return Ok(resultado);
        }

        [HttpGet("filtro")]
        public async Task<IActionResult> Filtrar([FromQuery] FiltroPeliculasDTO filtroPeliculasDTO)
        {
            var peliculasQueryable = _context.Peliculas.AsQueryable();

            if (!string.IsNullOrEmpty(filtroPeliculasDTO.Titulo))
            {
                peliculasQueryable = peliculasQueryable
                    .Where(x => x.Titulo
                    .Contains(filtroPeliculasDTO.Titulo));
            }

            if (filtroPeliculasDTO.EnCines)
            {
                peliculasQueryable = peliculasQueryable
                    .Where(x => x.EnCines);
            }

            if (filtroPeliculasDTO.ProximosEstrenos)
            {
                peliculasQueryable = peliculasQueryable
                    .Where(x => x.FechaEstreno > DateTime.Today);
            }

            if (filtroPeliculasDTO.GeneroId != 0)
            {
                peliculasQueryable = peliculasQueryable
                    .Where(x => x.PeliculaGeneros
                    .Select(y => y.GeneroId)
                    .Contains(filtroPeliculasDTO.GeneroId));
            }

            await HttpContext.InsertarParametrosPaginacion(peliculasQueryable,
                filtroPeliculasDTO.CantidadRegistroPorPagina);

            var peliculas = await peliculasQueryable.Paginar(filtroPeliculasDTO.Paginacion).ToListAsync();

            return Ok(_mapper.Map<List<PeliculaDTO>>(peliculas));
        }

        [HttpGet("{id:int}", Name = "ObtenerPeliculaPorId")]
        public async Task<IActionResult> GetById(int id)
        {
            var pelicula = await _context.Peliculas
                .Include(x => x.PeliculaActores).ThenInclude(x => x.Actor)
                .Include(x => x.PeliculaGeneros).ThenInclude(x => x.Genero)
                .FirstOrDefaultAsync(p => p.Id == id);
            if(pelicula== null) return NotFound();

            pelicula.PeliculaActores = pelicula.PeliculaActores.OrderBy(x => x.Orden).ToList();
            //pelicula.PeliculaActores = pelicula.PeliculaActores.OrderBy(x => x.Orden).ToList();

            var result = _mapper.Map<PeliculaDetallesDTO>(pelicula);
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
            return await Patch<Pelicula, PeliculaPatchDTO>(id, patchDocument);
        }

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> Delete(int id)
        {
            return await Delete<Pelicula>(id);
        }
    }
}
