using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebBattler.DAL.Migrations
{
    /// <inheritdoc />
    public partial class AddedMoney : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Money",
                table: "Countries",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Money",
                table: "Countries");
        }
    }
}
