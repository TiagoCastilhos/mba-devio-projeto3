using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Coldmart.Pagamentos.Data.Migrations
{
    /// <inheritdoc />
    public partial class MigrationInicialPagamentos : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "DadosCartao",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    NumeroCartao = table.Column<string>(type: "TEXT", maxLength: 20, nullable: false),
                    NomeTitular = table.Column<string>(type: "TEXT", maxLength: 60, nullable: false),
                    DataValidade = table.Column<DateTimeOffset>(type: "TEXT", nullable: false),
                    CodigoSeguranca = table.Column<string>(type: "TEXT", maxLength: 10, nullable: false),
                    DataCriacao = table.Column<DateTimeOffset>(type: "TEXT", nullable: false),
                    Deletado = table.Column<bool>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DadosCartao", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Pagamento",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    CartaoId = table.Column<Guid>(type: "TEXT", nullable: false),
                    Status = table.Column<string>(type: "TEXT", maxLength: 20, nullable: false),
                    DataAtualizacao = table.Column<DateTimeOffset>(type: "TEXT", nullable: false),
                    Valor = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    MatriculaId = table.Column<Guid>(type: "TEXT", nullable: false),
                    DataCriacao = table.Column<DateTimeOffset>(type: "TEXT", nullable: false),
                    Deletado = table.Column<bool>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Pagamento", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Pagamento_DadosCartao_CartaoId",
                        column: x => x.CartaoId,
                        principalTable: "DadosCartao",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Pagamento_CartaoId",
                table: "Pagamento",
                column: "CartaoId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Pagamento");

            migrationBuilder.DropTable(
                name: "DadosCartao");
        }
    }
}
