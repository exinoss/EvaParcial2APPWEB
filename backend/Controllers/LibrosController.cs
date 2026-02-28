using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using backend.Data;
using backend.Models;
using backend.DTOs;

namespace backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LibrosController : ControllerBase
    {
        private readonly BibliotecaContext _context;

        public LibrosController(BibliotecaContext context)
        {
            _context = context;
        }

        // GET: api/Libros
        [HttpGet]
        public async Task<ActionResult<IEnumerable<LibroDTO>>> GetLibros()
        {
            var libros = await _context.Libros
                .Include(l => l.LibroAutores)
                    .ThenInclude(la => la.Autor)
                .Select(l => new LibroDTO
                {
                    LibroId = l.LibroId,
                    Titulo = l.Titulo,
                    Genero = l.Genero,
                    FechaPublicacion = l.FechaPublicacion,
                    Isbn = l.Isbn,
                    Autores = l.LibroAutores.Select(la => new AutorDTO
                    {
                        AutorId = la.Autor.AutorId,
                        Nombre = la.Autor.Nombre,
                        Apellido = la.Autor.Apellido,
                        FechaNacimiento = la.Autor.FechaNacimiento,
                        Nacionalidad = la.Autor.Nacionalidad
                    }).ToList()
                })
                .ToListAsync();

            return Ok(libros);
        }

        // GET: api/Libros/5
        [HttpGet("{id}")]
        public async Task<ActionResult<LibroDTO>> GetLibro(int id)
        {
            var libro = await _context.Libros
                .Include(l => l.LibroAutores)
                    .ThenInclude(la => la.Autor)
                .FirstOrDefaultAsync(l => l.LibroId == id);

            if (libro == null)
                return NotFound(new { message = "Libro no encontrado" });

            return Ok(new LibroDTO
            {
                LibroId = libro.LibroId,
                Titulo = libro.Titulo,
                Genero = libro.Genero,
                FechaPublicacion = libro.FechaPublicacion,
                Isbn = libro.Isbn,
                Autores = libro.LibroAutores.Select(la => new AutorDTO
                {
                    AutorId = la.Autor.AutorId,
                    Nombre = la.Autor.Nombre,
                    Apellido = la.Autor.Apellido,
                    FechaNacimiento = la.Autor.FechaNacimiento,
                    Nacionalidad = la.Autor.Nacionalidad
                }).ToList()
            });
        }

        // POST: api/Libros
        [HttpPost]
        public async Task<ActionResult<LibroDTO>> CreateLibro(LibroCreateDTO dto)
        {
            var libro = new Libro
            {
                Titulo = dto.Titulo,
                Genero = dto.Genero,
                FechaPublicacion = dto.FechaPublicacion,
                Isbn = dto.Isbn
            };

            _context.Libros.Add(libro);
            await _context.SaveChangesAsync();

            // Crear relaciones libro-autor
            if (dto.AutorIds != null && dto.AutorIds.Count > 0)
            {
                foreach (var autorId in dto.AutorIds)
                {
                    var autorExists = await _context.Autores.AnyAsync(a => a.AutorId == autorId);
                    if (autorExists)
                    {
                        _context.LibroAutores.Add(new LibroAutor
                        {
                            LibroId = libro.LibroId,
                            AutorId = autorId
                        });
                    }
                }
                await _context.SaveChangesAsync();
            }

            // Recargar con autores
            var result = await _context.Libros
                .Include(l => l.LibroAutores)
                    .ThenInclude(la => la.Autor)
                .FirstAsync(l => l.LibroId == libro.LibroId);

            var response = new LibroDTO
            {
                LibroId = result.LibroId,
                Titulo = result.Titulo,
                Genero = result.Genero,
                FechaPublicacion = result.FechaPublicacion,
                Isbn = result.Isbn,
                Autores = result.LibroAutores.Select(la => new AutorDTO
                {
                    AutorId = la.Autor.AutorId,
                    Nombre = la.Autor.Nombre,
                    Apellido = la.Autor.Apellido,
                    FechaNacimiento = la.Autor.FechaNacimiento,
                    Nacionalidad = la.Autor.Nacionalidad
                }).ToList()
            };

            return CreatedAtAction(nameof(GetLibro), new { id = libro.LibroId }, response);
        }

        // PUT: api/Libros/5
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateLibro(int id, LibroCreateDTO dto)
        {
            var libro = await _context.Libros
                .Include(l => l.LibroAutores)
                .FirstOrDefaultAsync(l => l.LibroId == id);

            if (libro == null)
                return NotFound(new { message = "Libro no encontrado" });

            libro.Titulo = dto.Titulo;
            libro.Genero = dto.Genero;
            libro.FechaPublicacion = dto.FechaPublicacion;
            libro.Isbn = dto.Isbn;

            // Actualizar relaciones: eliminar las anteriores y crear las nuevas
            _context.LibroAutores.RemoveRange(libro.LibroAutores);

            if (dto.AutorIds != null && dto.AutorIds.Count > 0)
            {
                foreach (var autorId in dto.AutorIds)
                {
                    var autorExists = await _context.Autores.AnyAsync(a => a.AutorId == autorId);
                    if (autorExists)
                    {
                        _context.LibroAutores.Add(new LibroAutor
                        {
                            LibroId = libro.LibroId,
                            AutorId = autorId
                        });
                    }
                }
            }

            await _context.SaveChangesAsync();

            return Ok(new { message = "Libro actualizado correctamente" });
        }

        // DELETE: api/Libros/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteLibro(int id)
        {
            var libro = await _context.Libros.FindAsync(id);

            if (libro == null)
                return NotFound(new { message = "Libro no encontrado" });

            _context.Libros.Remove(libro);
            await _context.SaveChangesAsync();

            return Ok(new { message = "Libro eliminado correctamente" });
        }
    }
}
