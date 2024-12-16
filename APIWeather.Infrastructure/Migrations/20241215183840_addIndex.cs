using APIWeather.Domain.Aggregate.CidadesFavoritas;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace APIWeather.Infrastructure.Migrations
{
    public partial class addIndex : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("CREATE UNIQUE NONCLUSTERED INDEX" +
                                 " [UX_CidadesFavoritas_CityId]" +
                                 " ON [CidadesFavoritas]" +
                                 "  ([UsuarioId] ASC,[CityId] ASC)" +
                                 " WITH (STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF) ON [PRIMARY];");

        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("DROP INDEX[UX_CidadesFavoritas_CityId] ON[CidadesFavoritas];");
        }
    }
}
