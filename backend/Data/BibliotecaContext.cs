using Microsoft.EntityFrameworkCore;
using backend.Models;

namespace backend.Data
{
    public class BibliotecaContext : DbContext
    {
        public BibliotecaContext(DbContextOptions<BibliotecaContext> options) : base(options) { }

        public DbSet<Libro> Libros { get; set; }
        public DbSet<Autor> Autores { get; set; }
        public DbSet<LibroAutor> LibroAutores { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Libro>().ToTable("Libros");
            modelBuilder.Entity<Autor>().ToTable("Autores");
            modelBuilder.Entity<LibroAutor>().ToTable("LibroAutores");

            modelBuilder.Entity<LibroAutor>()
                .HasOne(la => la.Libro)
                .WithMany(l => l.LibroAutores)
                .HasForeignKey(la => la.LibroId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<LibroAutor>()
                .HasOne(la => la.Autor)
                .WithMany(a => a.LibroAutores)
                .HasForeignKey(la => la.AutorId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
