using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PloomesAPI.Migrations
{
    /// <inheritdoc />
    public partial class testemigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Clientes",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CodigoExterno = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    Inclusao = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Alteracao = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Excluido = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Clientes", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Clientes");
        }
    }
}
