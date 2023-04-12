using AutoMapper;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PeliculasApi.DTOs;
using PeliculasApi.Entidades;
using PeliculasApi.Helpers;

namespace PeliculasApi.Controllers
{
    public class CustomBaseController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;
        
        public CustomBaseController(ApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        #region CRUD
        protected async Task<IActionResult> GetAll<TEntidad, TDTO>() where TEntidad : class
        {
            var entidades = await _context.Set<TEntidad>()
                .AsNoTracking()
                .ToListAsync();

            var dtos = _mapper.Map<List<TDTO>>(entidades);
            return Ok(dtos);
        }

        protected async Task<IActionResult> GetById<TEntidad, TDTO>(int id) where TEntidad : class, IId //TEntidad tiene que implementar IId
        {
            var entidad = await _context.Set<TEntidad>()
                .AsNoTracking()
                .FirstOrDefaultAsync(x => x.Id == id);

            if (entidad == null) return NotFound();

            var dto = _mapper.Map<TDTO>(entidad);
            return Ok(dto);
        }

        protected async Task<IActionResult> Create<TCreacion, TEntidad, TLectura>(TCreacion creacionDTO, string nombreRuta) where TEntidad: class, IId
        {
            var entidad = _mapper.Map<TEntidad>(creacionDTO);
            _context.Set<TEntidad>().Add(entidad);
            await _context.SaveChangesAsync();

            var dtoLectura = _mapper.Map<TLectura>(entidad);

            return CreatedAtRoute(nombreRuta, new { id = entidad.Id }, dtoLectura);
        }

        protected async Task<IActionResult> Update<TCreacion, TEntidad>(int id, TCreacion creacionDTO) where TEntidad: class, IId
        {
            var entidad = _mapper.Map<TEntidad>(creacionDTO);
            entidad.Id = id;

            _context.Set<TEntidad>().Update(entidad);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        protected async Task<IActionResult> Delete<TEntidad>(int id) where TEntidad: class, IId, new() //Constructor vacio para la entidad generica
        {
            var exists = await _context.Set<TEntidad>().AnyAsync(x => x.Id == id);

            if (!exists) return NotFound();

            _context.Set<TEntidad>().Remove(new TEntidad() { Id = id });
            await _context.SaveChangesAsync();

            return NoContent();
        }
        #endregion

        #region OTHERS
        protected async Task<IActionResult> GetAllWithFilters<TEntidad, TDTO>(PaginacionDTO paginacionDTO) where TEntidad : class
        {
            var queryable = _context.Set<TEntidad>().AsQueryable();

            await HttpContext.InsertarParametrosPaginacion(queryable, paginacionDTO.CantidadRegistrosPorPagina);

            var entidades = await queryable.Paginar(paginacionDTO).ToListAsync();

            var result = _mapper.Map<List<TDTO>>(entidades);

            return Ok(result);
        }

        protected async Task<IActionResult> Patch<TEntidad, TDTO>(int id, JsonPatchDocument<TDTO> patchDocument) where TEntidad: class, IId where TDTO: class
        {
            if (patchDocument == null) return BadRequest();

            var entidadDB = await _context.Set<TEntidad>().FirstOrDefaultAsync(a => a.Id == id);
            if (entidadDB == null) return NotFound();

            var entidadDTO = _mapper.Map<TDTO>(entidadDB);
            patchDocument.ApplyTo(entidadDTO, ModelState);

            var esValido = TryValidateModel(entidadDTO);
            if (!esValido) return BadRequest(ModelState);

            _mapper.Map(entidadDTO, entidadDB);

            await _context.SaveChangesAsync();
            return NoContent();
        }
        #endregion{

    }
}
