using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace APIWeather.Infrastructure.Migrations
{
    public partial class adicionandoTabelaUsuarioseFK : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "USuarios",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Nome = table.Column<string>(type: "nvarchar(150)", nullable: false),
                    UserName = table.Column<string>(type: "nvarchar(20)", nullable: false),
                    Senha = table.Column<string>(type: "nvarchar(50)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_USuarios", x => x.Id);
                });

        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "USuarios");
        }
    }
}
