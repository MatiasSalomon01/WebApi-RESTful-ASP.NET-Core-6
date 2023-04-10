﻿using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApiAutores.DTOs;
using WebApiAutores.Entidades;

namespace WebApiAutores.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AutoresController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;

        public AutoresController(ApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<List<AutorDTO>>> GetAll()
        {
            var result = await _context.Autores.ToListAsync();
            if (result == null) return NotFound();
            return _mapper.Map<List<AutorDTO>>(result);
        }

        [HttpGet("{id:int}", Name = "obtenerAutor")]
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
        public async Task<ActionResult> Update(Autor autor, int id)
        {
            if (autor.Id != id) return BadRequest("El id del autor no coincide con la URL");

            _context.Autores.Update(autor);
            await _context.SaveChangesAsync();
            return Ok();
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
