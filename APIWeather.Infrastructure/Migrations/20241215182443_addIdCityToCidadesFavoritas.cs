using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace APIWeather.Infrastructure.Migrations
{
    public partial class addIdCityToCidadesFavoritas : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "CityId",
                table: "CidadesFavoritas",
                type: "nvarchar(30)",
                nullable: false,
                defaultValue: "");

        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CityId",
                table: "CidadesFavoritas");
        }
    }
}
