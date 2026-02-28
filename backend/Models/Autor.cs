using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace backend.Models
{
    public class Autor
    {
        [Key]
        [Column("autor_id")]
        public int AutorId { get; set; }

        [Required]
        [StringLength(100)]
        [Column("nombre")]
        public string Nombre { get; set; } = string.Empty;

        [Required]
        [StringLength(100)]
        [Column("apellido")]
        public string Apellido { get; set; } = string.Empty;

        [Column("fecha_nacimiento")]
        public DateTime? FechaNacimiento { get; set; }

        [StringLength(100)]
        [Column("nacionalidad")]
        public string? Nacionalidad { get; set; }

        [JsonIgnore]
        public ICollection<LibroAutor> LibroAutores { get; set; } = new List<LibroAutor>();
    }
}
