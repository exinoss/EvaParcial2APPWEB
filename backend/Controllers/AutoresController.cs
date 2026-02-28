using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using backend.Data;
using backend.Models;
using backend.DTOs;

namespace backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AutoresController : ControllerBase
    {
        private readonly BibliotecaContext _context;

        public AutoresController(BibliotecaContext context)
        {
            _context = context;
        }

        // GET: api/Autores
        [HttpGet]
        public async Task<ActionResult<IEnumerable<AutorDTO>>> GetAutores()
        {
            var autores = await _context.Autores
                .Select(a => new AutorDTO
                {
                    AutorId = a.AutorId,
                    Nombre = a.Nombre,
                    Apellido = a.Apellido,
                    FechaNacimiento = a.FechaNacimiento,
                    Nacionalidad = a.Nacionalidad
                })
                .ToListAsync();

            return Ok(autores);
        }

        // GET: api/Autores/5
        [HttpGet("{id}")]
        public async Task<ActionResult<AutorDTO>> GetAutor(int id)
        {
            var autor = await _context.Autores.FindAsync(id);

            if (autor == null)
                return NotFound(new { message = "Autor no encontrado" });

            return Ok(new AutorDTO
            {
                AutorId = autor.AutorId,
                Nombre = autor.Nombre,
                Apellido = autor.Apellido,
                FechaNacimiento = autor.FechaNacimiento,
                Nacionalidad = autor.Nacionalidad
            });
        }

        // POST: api/Autores
        [HttpPost]
        public async Task<ActionResult<AutorDTO>> CreateAutor(AutorCreateDTO dto)
        {
            var autor = new Autor
            {
                Nombre = dto.Nombre,
                Apellido = dto.Apellido,
                FechaNacimiento = dto.FechaNacimiento,
                Nacionalidad = dto.Nacionalidad
            };

            _context.Autores.Add(autor);
            await _context.SaveChangesAsync();

            var result = new AutorDTO
            {
                AutorId = autor.AutorId,
                Nombre = autor.Nombre,
                Apellido = autor.Apellido,
                FechaNacimiento = autor.FechaNacimiento,
                Nacionalidad = autor.Nacionalidad
            };

            return CreatedAtAction(nameof(GetAutor), new { id = autor.AutorId }, result);
        }

        // PUT: api/Autores/5
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateAutor(int id, AutorCreateDTO dto)
        {
            var autor = await _context.Autores.FindAsync(id);

            if (autor == null)
                return NotFound(new { message = "Autor no encontrado" });

            autor.Nombre = dto.Nombre;
            autor.Apellido = dto.Apellido;
            autor.FechaNacimiento = dto.FechaNacimiento;
            autor.Nacionalidad = dto.Nacionalidad;

            await _context.SaveChangesAsync();

            return Ok(new { message = "Autor actualizado correctamente" });
        }

        // DELETE: api/Autores/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAutor(int id)
        {
            var autor = await _context.Autores.FindAsync(id);

            if (autor == null)
                return NotFound(new { message = "Autor no encontrado" });

            _context.Autores.Remove(autor);
            await _context.SaveChangesAsync();

            return Ok(new { message = "Autor eliminado correctamente" });
        }
    }
}
