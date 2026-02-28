using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace backend.Models
{
    public class Libro
    {
        [Key]
        [Column("libro_id")]
        public int LibroId { get; set; }

        [Required]
        [StringLength(200)]
        [Column("titulo")]
        public string Titulo { get; set; } = string.Empty;

        [StringLength(100)]
        [Column("genero")]
        public string? Genero { get; set; }

        [Column("fecha_publicacion")]
        public DateTime? FechaPublicacion { get; set; }

        [Required]
        [StringLength(20)]
        [Column("isbn")]
        public string Isbn { get; set; } = string.Empty;

        [JsonIgnore]
        public ICollection<LibroAutor> LibroAutores { get; set; } = new List<LibroAutor>();
    }
}
