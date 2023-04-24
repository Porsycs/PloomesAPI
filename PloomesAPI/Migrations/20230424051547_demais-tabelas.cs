using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PloomesAPI.Migrations
{
    /// <inheritdoc />
    public partial class demaistabelas : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Usuarios",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UsuarioLogin = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UsuarioPassword = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    RefreshToken = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ExpireTokenTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CodigoExterno = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Inclusao = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Alteracao = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Excluido = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Usuarios", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Usuarios");
        }
    }
}
