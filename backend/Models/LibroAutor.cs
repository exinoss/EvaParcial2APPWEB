using System.ComponentModel.DataAnnotations.Schema;

namespace backend.Models
{
    public class LibroAutor
    {
        [Column("id")]
        public int Id { get; set; }

        [Column("libro_id")]
        public int LibroId { get; set; }

        [ForeignKey("LibroId")]
        public Libro Libro { get; set; } = null!;

        [Column("autor_id")]
        public int AutorId { get; set; }

        [ForeignKey("AutorId")]
        public Autor Autor { get; set; } = null!;
    }
}
