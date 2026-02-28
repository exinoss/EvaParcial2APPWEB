namespace backend.DTOs
{
    public class LibroDTO
    {
        public int LibroId { get; set; }
        public string Titulo { get; set; } = string.Empty;
        public string? Genero { get; set; }
        public DateTime? FechaPublicacion { get; set; }
        public string Isbn { get; set; } = string.Empty;
        public List<AutorDTO> Autores { get; set; } = new List<AutorDTO>();
    }

    public class LibroCreateDTO
    {
        public string Titulo { get; set; } = string.Empty;
        public string? Genero { get; set; }
        public DateTime? FechaPublicacion { get; set; }
        public string Isbn { get; set; } = string.Empty;
        public List<int> AutorIds { get; set; } = new List<int>();
    }
}
