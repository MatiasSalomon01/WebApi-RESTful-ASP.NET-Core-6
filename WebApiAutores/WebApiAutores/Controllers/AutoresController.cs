﻿using AutoMapper;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApiAutores.DTOs;
using WebApiAutores.Entidades;
using WebApiAutores.Utilidades;

namespace WebApiAutores.Controllers
{
    [ApiController]
    [Route("[controller]")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Policy = "EsAdmin")]

    public class AutoresController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;
        private readonly IConfiguration _configuration;

        public AutoresController(ApplicationDbContext context, IMapper mapper, IConfiguration configuration)
        {
            _context = context;
            _mapper = mapper;
            _configuration = configuration;
        }

        [HttpGet]
        public async Task<ActionResult<List<AutorDTO>>> GetAll([FromQuery] PaginacionDTO paginacionDTO)
        {

            var queryable = _context.Autores.AsQueryable();
            await HttpContext.InsertarParametrosPaginacionCabecera(queryable);

            //var result = await _context.Autores.ToListAsync();
            var result = await queryable
                .OrderBy(x => x.Nombre)
                .Paginar(paginacionDTO)
                .ToListAsync();

            if (result == null) return NotFound();
            return _mapper.Map<List<AutorDTO>>(result);
        }

        [HttpGet("{id:int}", Name = "obtenerAutor")]
        [AllowAnonymous]
        public async Task<ActionResult<AutorDTOConLibros>> GetById(int id)
        {
            var result = await _context.Autores
                .Include(a => a.AutoresLibros)
                .ThenInclude(a => a.Libro)
                .FirstOrDefaultAsync(a => a.Id == id);
            if (result == null) return NotFound();
            return _mapper.Map<AutorDTOConLibros>(result); ;
        }


        [HttpPost]
        public async Task<ActionResult> Create(AutorCreacionDTO autor)
        {
            var result = _mapper.Map<Autor>(autor);
            _context.Autores.Add(result);
            await _context.SaveChangesAsync();

            var autorDTO = _mapper.Map<AutorDTO>(result);

            return CreatedAtRoute("obtenerAutor", new {id = autorDTO.Id}, autorDTO);
        }

        [HttpPut("{id:int}")]
        public async Task<ActionResult> Update(AutorCreacionDTO autorCreacionDTO, int id)
        {
            //if (autor.Id != id) return BadRequest("El id del autor no coincide con la URL");
            var existe = await _context.Autores.AnyAsync(a => a.Id == id);
            if(!existe) return NotFound();

            var autor = _mapper.Map<Autor>(autorCreacionDTO);
            autor.Id = id;
            _context.Autores.Update(autor);
            await _context.SaveChangesAsync();
            return NoContent();
        }

        [HttpDelete("{id:int}")]
        public async Task<ActionResult> Delete(int id)
        {
            var result = await _context.Autores.AnyAsync(a => a.Id == id);
            if (!result) return NotFound();

            _context.Remove(new Autor() { Id = id});
            await _context.SaveChangesAsync();
            return Ok();
        }
    }
}
