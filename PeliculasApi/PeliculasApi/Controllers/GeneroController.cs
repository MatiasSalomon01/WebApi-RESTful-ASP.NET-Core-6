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
    public class GeneroController : CustomBaseController
    {
        public GeneroController(ApplicationDbContext context, IMapper mapper) : base(context, mapper)
        {}

        [HttpGet()]
        public async Task<IActionResult> GetAll() 
        {
            return await GetAll<Genero, GeneroDTO>();
        }

        [HttpGet("{id:int}", Name = "ObtenerGeneroPorId")]
        public async Task<IActionResult> GetById(int id)
        {
            return await GetById<Genero, GeneroDTO>(id);
        }

        [HttpPost]
        public async Task<IActionResult> Create(GeneroCreacionDTO generoCreacionDTO)
        {
            return await Create<GeneroCreacionDTO, Genero, GeneroDTO>(generoCreacionDTO, "ObtenerGeneroPorId");
        }

        [HttpPut("{id:int}")]
        public async Task<IActionResult> Update(int id, GeneroCreacionDTO generoCreacionDTO)
        {
            return await Update<GeneroCreacionDTO, Genero>(id, generoCreacionDTO);
        }

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> Delete(int id)
        {
            return await Delete<Genero>(id);
        }
    }
}