using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebBattler.DAL.Migrations
{
    /// <inheritdoc />
    public partial class ProductionFix : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Cost",
                table: "UnitSamples",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Cost",
                table: "ProductionOrders",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Cost",
                table: "UnitSamples");

            migrationBuilder.DropColumn(
                name: "Cost",
                table: "ProductionOrders");
        }
    }
}
