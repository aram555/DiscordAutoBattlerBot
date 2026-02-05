using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebBattler.DAL.Migrations
{
    /// <inheritdoc />
    public partial class NeightboursAndArmyMovement : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ProvinceNeighbours",
                columns: table => new
                {
                    NeighboursId = table.Column<int>(type: "int", nullable: false),
                    ProvinceEntityId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProvinceNeighbours", x => new { x.NeighboursId, x.ProvinceEntityId });
                    table.ForeignKey(
                        name: "FK_ProvinceNeighbours_Provinces_NeighboursId",
                        column: x => x.NeighboursId,
                        principalTable: "Provinces",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ProvinceNeighbours_Provinces_ProvinceEntityId",
                        column: x => x.ProvinceEntityId,
                        principalTable: "Provinces",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_ProvinceNeighbours_ProvinceEntityId",
                table: "ProvinceNeighbours",
                column: "ProvinceEntityId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ProvinceNeighbours");
        }
    }
}
