using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebBattler.DAL.Migrations
{
    /// <inheritdoc />
    public partial class MovementPoitsForArmy : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "CurrentTurnCount",
                table: "Armies",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CurrentTurnCount",
                table: "Armies");
        }
    }
}
