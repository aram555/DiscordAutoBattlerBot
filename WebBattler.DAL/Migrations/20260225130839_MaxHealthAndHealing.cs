using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebBattler.DAL.Migrations
{
    /// <inheritdoc />
    public partial class MaxHealthAndHealing : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<float>(
                name: "MaxHealth",
                table: "Units",
                type: "real",
                nullable: false,
                defaultValue: 0f);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "MaxHealth",
                table: "Units");
        }
    }
}
