using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace backend.Migrations
{
    /// <inheritdoc />
    public partial class inicial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Autores",
                columns: table => new
                {
                    autor_id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    nombre = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    apellido = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    fecha_nacimiento = table.Column<DateTime>(type: "datetime2", nullable: true),
                    nacionalidad = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Autores", x => x.autor_id);
                });

            migrationBuilder.CreateTable(
                name: "Libros",
                columns: table => new
                {
                    libro_id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    titulo = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    genero = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    fecha_publicacion = table.Column<DateTime>(type: "datetime2", nullable: true),
                    isbn = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Libros", x => x.libro_id);
                });

            migrationBuilder.CreateTable(
                name: "LibroAutores",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    libro_id = table.Column<int>(type: "int", nullable: false),
                    autor_id = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LibroAutores", x => x.id);
                    table.ForeignKey(
                        name: "FK_LibroAutores_Autores_autor_id",
                        column: x => x.autor_id,
                        principalTable: "Autores",
                        principalColumn: "autor_id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_LibroAutores_Libros_libro_id",
                        column: x => x.libro_id,
                        principalTable: "Libros",
                        principalColumn: "libro_id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_LibroAutores_autor_id",
                table: "LibroAutores",
                column: "autor_id");

            migrationBuilder.CreateIndex(
                name: "IX_LibroAutores_libro_id",
                table: "LibroAutores",
                column: "libro_id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "LibroAutores");

            migrationBuilder.DropTable(
                name: "Autores");

            migrationBuilder.DropTable(
                name: "Libros");
        }
    }
}
