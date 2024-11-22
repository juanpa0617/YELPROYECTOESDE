using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace YELPROYECTOESDE.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Comodidades",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nombre = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Descripcion = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Estado = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Comodidades", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Tipos",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nombre = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tipos", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Alojamientos",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nombre = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Capacidad = table.Column<int>(type: "int", nullable: false),
                    Descripcion = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TipoId = table.Column<int>(type: "int", nullable: false),
                    ImagenUrl = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Alojamientos", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Alojamientos_Tipos_TipoId",
                        column: x => x.TipoId,
                        principalTable: "Tipos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "DetallesAlojamientoComodidad",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IdAlojamiento = table.Column<int>(type: "int", nullable: false),
                    ComodidadId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DetallesAlojamientoComodidad", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DetallesAlojamientoComodidad_Alojamientos_IdAlojamiento",
                        column: x => x.IdAlojamiento,
                        principalTable: "Alojamientos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_DetallesAlojamientoComodidad_Comodidades_ComodidadId",
                        column: x => x.ComodidadId,
                        principalTable: "Comodidades",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Alojamientos_TipoId",
                table: "Alojamientos",
                column: "TipoId");

            migrationBuilder.CreateIndex(
                name: "IX_DetallesAlojamientoComodidad_ComodidadId",
                table: "DetallesAlojamientoComodidad",
                column: "ComodidadId");

            migrationBuilder.CreateIndex(
                name: "IX_DetallesAlojamientoComodidad_IdAlojamiento",
                table: "DetallesAlojamientoComodidad",
                column: "IdAlojamiento");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "DetallesAlojamientoComodidad");

            migrationBuilder.DropTable(
                name: "Alojamientos");

            migrationBuilder.DropTable(
                name: "Comodidades");

            migrationBuilder.DropTable(
                name: "Tipos");
        }
    }
}
