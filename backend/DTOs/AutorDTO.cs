namespace backend.DTOs
{
    public class AutorDTO
    {
        public int AutorId { get; set; }
        public string Nombre { get; set; } = string.Empty;
        public string Apellido { get; set; } = string.Empty;
        public DateTime? FechaNacimiento { get; set; }
        public string? Nacionalidad { get; set; }
    }

    public class AutorCreateDTO
    {
        public string Nombre { get; set; } = string.Empty;
        public string Apellido { get; set; } = string.Empty;
        public DateTime? FechaNacimiento { get; set; }
        public string? Nacionalidad { get; set; }
    }
}
