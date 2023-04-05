﻿using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApiAutores.Entidades;

namespace WebApiAutores.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AutoresController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public AutoresController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<List<Autor>>> GetAll()
        {
            var result = await _context.Autores.ToListAsync();
            if (result == null) return NotFound();
            return result;
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult<Autor>> GetById(int id)
        {
            var result = await _context.Autores.FirstOrDefaultAsync(a => a.Id == id);
            if (result == null) return NotFound();
            return result;
        }


        [HttpPost]
        public async Task<ActionResult> Create(Autor autor)
        {
            _context.Autores.Add(autor);
            await _context.SaveChangesAsync();
            return Ok();
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
