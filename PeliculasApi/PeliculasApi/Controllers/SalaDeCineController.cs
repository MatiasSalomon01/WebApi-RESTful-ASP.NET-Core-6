using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PeliculasApi.DTOs;
using PeliculasApi.Entidades;

namespace PeliculasApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SalaDeCineController : CustomBaseController
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;

        public SalaDeCineController(ApplicationDbContext context, IMapper mapper) : base(context, mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            return await GetAll<SalaDeCine, SalaDeCineDTO>();
        }

        [HttpGet("{id:int}", Name = "ObtenerSalaDeCinePorId")]
        public async Task<IActionResult> GetById(int id)
        {
            return await GetById<SalaDeCine, SalaDeCineDTO>(id);
        }

        [HttpPost]
        public async Task<IActionResult> Create(SalaDeCineCreacionDTO salaDeCineCreacionDTO)
        {
            return await Create<SalaDeCineCreacionDTO, SalaDeCine, SalaDeCineDTO>(salaDeCineCreacionDTO, "ObtenerSalaDeCinePorId");
        }

        [HttpPut("{id:int}")]
        public async Task<IActionResult> Update(int id, SalaDeCineCreacionDTO salaDeCineCreacionDTO)
        {
            return await Update<SalaDeCineCreacionDTO, SalaDeCine>(id, salaDeCineCreacionDTO);
        }

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> Delete(int id)
        {
            return await Delete<SalaDeCine>(id);
        }
    }
}
