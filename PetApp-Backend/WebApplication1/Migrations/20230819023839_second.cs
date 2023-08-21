using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebApplication1.Migrations
{
    /// <inheritdoc />
    public partial class second : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Mascotas",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                    .Annotation("SqlServer:Identity", "1,1"),
                    Nombre= table.Column<string>(nullable: false),
                    Raza = table.Column<string>(type: "nvarchar(max)",nullable: false),
                    Color = table.Column<string>(type: "nvarchar(max)", nullable:true),
                    Peso= table.Column<decimal>(type:"real", nullable: false),
                    FechaAlta = table.Column<DateTime>(type:"datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("Pk_Pets", x => x.Id);
                }

                );
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PetsDB");
        }
    }
}
