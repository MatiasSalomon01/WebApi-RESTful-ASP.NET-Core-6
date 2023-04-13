using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NetTopologySuite.Geometries;
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
        private readonly GeometryFactory _geometryFactory;

        public SalaDeCineController(ApplicationDbContext context, IMapper mapper, GeometryFactory geometryFactory) : base(context, mapper)
        {
            _context = context;
            _mapper = mapper;
            _geometryFactory = geometryFactory;
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

        [HttpGet("Cercanos")]
        public async Task<IActionResult> GetNearCinemaRooms([FromQuery] SalaDeCineCercanoFiltroDTO filtro)
        {
            var ubicacionUsuario = _geometryFactory.CreatePoint(new Coordinate(filtro.Longitud, filtro.Latitud));
            var salasDeCine = await _context.SalaDeCine
                .OrderBy(x => x.Ubicacion.Distance(ubicacionUsuario))
                .Where(x => x.Ubicacion.IsWithinDistance(ubicacionUsuario, filtro.DistanciaEnKms*1000))
                .Select(x => new SalaDeCineCercanoDTO
                {
                    Id = x.Id,
                    Nombre = x.Nombre,
                    Latitud = x.Ubicacion.Y,
                    Longitud = x.Ubicacion.X,
                    DistanciaEnMetros = x.Ubicacion.Distance(ubicacionUsuario)
                }).ToListAsync();

            return Ok(salasDeCine);
        }

        [HttpPost]
        public async Task<IActionResult> Create(SalaDeCineCreacionDTO salaDeCineCreacionDTO)
        {
            return await Create<SalaDeCineCreacionDTO, SalaDeCine, SalaDeCineDTO>(salaDeCineCreacionDTO, "ObtenerSalaDeCinePorId");
        }

        [HttpPost("{id:int}/AgregarPelicula/{peliculaId}")]
        public async Task<IActionResult> AddMovieToCinemaRoom(int id, int peliculaId)
        {
            var peliculasSalaDeCine = new PeliculaSalaDeCine()
            { 
                PeliculaId = peliculaId, SalaDeCineId = id
            };

            _context.PeliculasSalasDeCines.Add(peliculasSalaDeCine);
            await _context.SaveChangesAsync();
            return NoContent();
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
