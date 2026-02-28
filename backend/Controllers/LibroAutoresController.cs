using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using backend.Data;
using backend.Models;
using backend.DTOs;

namespace backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LibroAutoresController : ControllerBase
    {
        private readonly BibliotecaContext _context;

        public LibroAutoresController(BibliotecaContext context)
        {
            _context = context;
        }

        // GET: api/LibroAutores
        [HttpGet]
        public async Task<ActionResult<IEnumerable<LibroAutorDTO>>> GetLibroAutores()
        {
            var libroAutores = await _context.LibroAutores
                .Include(la => la.Libro)
                .Include(la => la.Autor)
                .Select(la => new LibroAutorDTO
                {
                    Id = la.Id,
                    LibroId = la.LibroId,
                    LibroTitulo = la.Libro.Titulo,
                    AutorId = la.AutorId,
                    AutorNombreCompleto = la.Autor.Nombre + " " + la.Autor.Apellido
                })
                .ToListAsync();

            return Ok(libroAutores);
        }

        // GET: api/LibroAutores/5
        [HttpGet("{id}")]
        public async Task<ActionResult<LibroAutorDTO>> GetLibroAutor(int id)
        {
            var la = await _context.LibroAutores
                .Include(x => x.Libro)
                .Include(x => x.Autor)
                .FirstOrDefaultAsync(x => x.Id == id);

            if (la == null)
                return NotFound(new { message = "Relación no encontrada" });

            return Ok(new LibroAutorDTO
            {
                Id = la.Id,
                LibroId = la.LibroId,
                LibroTitulo = la.Libro.Titulo,
                AutorId = la.AutorId,
                AutorNombreCompleto = la.Autor.Nombre + " " + la.Autor.Apellido
            });
        }

        // POST: api/LibroAutores
        [HttpPost]
        public async Task<ActionResult<LibroAutorDTO>> CreateLibroAutor(LibroAutorCreateDTO dto)
        {
            var libro = await _context.Libros.FindAsync(dto.LibroId);
            if (libro == null)
                return BadRequest(new { message = "El libro especificado no existe" });

            var autor = await _context.Autores.FindAsync(dto.AutorId);
            if (autor == null)
                return BadRequest(new { message = "El autor especificado no existe" });

            var exists = await _context.LibroAutores
                .AnyAsync(la => la.LibroId == dto.LibroId && la.AutorId == dto.AutorId);

            if (exists)
                return BadRequest(new { message = "Esta relación libro-autor ya existe" });

            var libroAutor = new LibroAutor
            {
                LibroId = dto.LibroId,
                AutorId = dto.AutorId
            };

            _context.LibroAutores.Add(libroAutor);
            await _context.SaveChangesAsync();

            var result = new LibroAutorDTO
            {
                Id = libroAutor.Id,
                LibroId = libroAutor.LibroId,
                LibroTitulo = libro.Titulo,
                AutorId = libroAutor.AutorId,
                AutorNombreCompleto = autor.Nombre + " " + autor.Apellido
            };

            return CreatedAtAction(nameof(GetLibroAutor), new { id = libroAutor.Id }, result);
        }

        // PUT: api/LibroAutores/5
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateLibroAutor(int id, LibroAutorCreateDTO dto)
        {
            var libroAutor = await _context.LibroAutores.FindAsync(id);

            if (libroAutor == null)
                return NotFound(new { message = "Relación no encontrada" });

            var libro = await _context.Libros.FindAsync(dto.LibroId);
            if (libro == null)
                return BadRequest(new { message = "El libro especificado no existe" });

            var autor = await _context.Autores.FindAsync(dto.AutorId);
            if (autor == null)
                return BadRequest(new { message = "El autor especificado no existe" });

            libroAutor.LibroId = dto.LibroId;
            libroAutor.AutorId = dto.AutorId;

            await _context.SaveChangesAsync();

            return Ok(new { message = "Relación actualizada correctamente" });
        }

        // DELETE: api/LibroAutores/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteLibroAutor(int id)
        {
            var libroAutor = await _context.LibroAutores.FindAsync(id);

            if (libroAutor == null)
                return NotFound(new { message = "Relación no encontrada" });

            _context.LibroAutores.Remove(libroAutor);
            await _context.SaveChangesAsync();

            return Ok(new { message = "Relación eliminada correctamente" });
        }
    }
}
