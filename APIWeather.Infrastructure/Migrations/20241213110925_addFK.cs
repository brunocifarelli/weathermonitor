using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace APIWeather.Infrastructure.Migrations
{
    public partial class addFK : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_USuarios",
                table: "USuarios");

            migrationBuilder.RenameTable(
                name: "USuarios",
                newName: "Usuarios");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Usuarios",
                table: "Usuarios",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_CidadesFavoritas_UsuarioId",
                table: "CidadesFavoritas",
                column: "UsuarioId");

            migrationBuilder.AddForeignKey(
                name: "FK_CidadesFavoritas_Usuarios_UsuarioId",
                table: "CidadesFavoritas",
                column: "UsuarioId",
                principalTable: "Usuarios",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CidadesFavoritas_Usuarios_UsuarioId",
                table: "CidadesFavoritas");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Usuarios",
                table: "Usuarios");

            migrationBuilder.DropIndex(
                name: "IX_CidadesFavoritas_UsuarioId",
                table: "CidadesFavoritas");

            migrationBuilder.RenameTable(
                name: "Usuarios",
                newName: "USuarios");

            migrationBuilder.AddPrimaryKey(
                name: "PK_USuarios",
                table: "USuarios",
                column: "Id");
        }
    }
}
