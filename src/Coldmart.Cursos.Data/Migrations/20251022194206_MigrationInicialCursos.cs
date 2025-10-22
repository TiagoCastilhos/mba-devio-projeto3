using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Coldmart.Cursos.Data.Migrations
{
    /// <inheritdoc />
    public partial class MigrationInicialCursos : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Curso",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    Nome = table.Column<string>(type: "TEXT", maxLength: 60, nullable: false),
                    DuracaoTotal = table.Column<double>(type: "REAL", nullable: false),
                    DataCriacao = table.Column<DateTimeOffset>(type: "TEXT", nullable: false),
                    Deletado = table.Column<bool>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Curso", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Aula",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    CursoId = table.Column<Guid>(type: "TEXT", nullable: false),
                    Titulo = table.Column<string>(type: "TEXT", maxLength: 60, nullable: false),
                    Duracao = table.Column<double>(type: "REAL", nullable: false),
                    DataCriacao = table.Column<DateTimeOffset>(type: "TEXT", nullable: false),
                    Deletado = table.Column<bool>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Aula", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Aula_Curso_CursoId",
                        column: x => x.CursoId,
                        principalTable: "Curso",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ConteudoProgramatico",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    CursoId = table.Column<Guid>(type: "TEXT", nullable: false),
                    Titulo = table.Column<string>(type: "TEXT", maxLength: 50, nullable: false),
                    Descricao = table.Column<string>(type: "TEXT", maxLength: 100, nullable: false),
                    DataCriacao = table.Column<DateTimeOffset>(type: "TEXT", nullable: false),
                    Deletado = table.Column<bool>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ConteudoProgramatico", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ConteudoProgramatico_Curso_CursoId",
                        column: x => x.CursoId,
                        principalTable: "Curso",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Aula_CursoId",
                table: "Aula",
                column: "CursoId");

            migrationBuilder.CreateIndex(
                name: "IX_ConteudoProgramatico_CursoId",
                table: "ConteudoProgramatico",
                column: "CursoId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Aula");

            migrationBuilder.DropTable(
                name: "ConteudoProgramatico");

            migrationBuilder.DropTable(
                name: "Curso");
        }
    }
}
