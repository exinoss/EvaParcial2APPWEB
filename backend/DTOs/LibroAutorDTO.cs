namespace backend.DTOs
{
    public class LibroAutorDTO
    {
        public int Id { get; set; }
        public int LibroId { get; set; }
        public string LibroTitulo { get; set; } = string.Empty;
        public int AutorId { get; set; }
        public string AutorNombreCompleto { get; set; } = string.Empty;
    }

    public class LibroAutorCreateDTO
    {
        public int LibroId { get; set; }
        public int AutorId { get; set; }
    }
}
