using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebBattler.DAL.Migrations
{
    /// <inheritdoc />
    public partial class NeightboursFix : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Armies_Provinces_ProvinceId",
                table: "Armies");

            migrationBuilder.DropForeignKey(
                name: "FK_Cities_Provinces_ProvinceId",
                table: "Cities");

            migrationBuilder.DropForeignKey(
                name: "FK_ProvinceNeighbours_Provinces_NeighboursId",
                table: "ProvinceNeighbours");

            migrationBuilder.DropForeignKey(
                name: "FK_ProvinceNeighbours_Provinces_ProvinceEntityId",
                table: "ProvinceNeighbours");

            migrationBuilder.DropForeignKey(
                name: "FK_Provinces_Countries_CountryId",
                table: "Provinces");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Provinces",
                table: "Provinces");

            migrationBuilder.RenameTable(
                name: "Provinces",
                newName: "ProvinceEntity");

            migrationBuilder.RenameIndex(
                name: "IX_Provinces_CountryId",
                table: "ProvinceEntity",
                newName: "IX_ProvinceEntity_CountryId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ProvinceEntity",
                table: "ProvinceEntity",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Armies_ProvinceEntity_ProvinceId",
                table: "Armies",
                column: "ProvinceId",
                principalTable: "ProvinceEntity",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Cities_ProvinceEntity_ProvinceId",
                table: "Cities",
                column: "ProvinceId",
                principalTable: "ProvinceEntity",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_ProvinceEntity_Countries_CountryId",
                table: "ProvinceEntity",
                column: "CountryId",
                principalTable: "Countries",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_ProvinceNeighbours_ProvinceEntity_NeighboursId",
                table: "ProvinceNeighbours",
                column: "NeighboursId",
                principalTable: "ProvinceEntity",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ProvinceNeighbours_ProvinceEntity_ProvinceEntityId",
                table: "ProvinceNeighbours",
                column: "ProvinceEntityId",
                principalTable: "ProvinceEntity",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Armies_ProvinceEntity_ProvinceId",
                table: "Armies");

            migrationBuilder.DropForeignKey(
                name: "FK_Cities_ProvinceEntity_ProvinceId",
                table: "Cities");

            migrationBuilder.DropForeignKey(
                name: "FK_ProvinceEntity_Countries_CountryId",
                table: "ProvinceEntity");

            migrationBuilder.DropForeignKey(
                name: "FK_ProvinceNeighbours_ProvinceEntity_NeighboursId",
                table: "ProvinceNeighbours");

            migrationBuilder.DropForeignKey(
                name: "FK_ProvinceNeighbours_ProvinceEntity_ProvinceEntityId",
                table: "ProvinceNeighbours");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ProvinceEntity",
                table: "ProvinceEntity");

            migrationBuilder.RenameTable(
                name: "ProvinceEntity",
                newName: "Provinces");

            migrationBuilder.RenameIndex(
                name: "IX_ProvinceEntity_CountryId",
                table: "Provinces",
                newName: "IX_Provinces_CountryId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Provinces",
                table: "Provinces",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Armies_Provinces_ProvinceId",
                table: "Armies",
                column: "ProvinceId",
                principalTable: "Provinces",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Cities_Provinces_ProvinceId",
                table: "Cities",
                column: "ProvinceId",
                principalTable: "Provinces",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_ProvinceNeighbours_Provinces_NeighboursId",
                table: "ProvinceNeighbours",
                column: "NeighboursId",
                principalTable: "Provinces",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ProvinceNeighbours_Provinces_ProvinceEntityId",
                table: "ProvinceNeighbours",
                column: "ProvinceEntityId",
                principalTable: "Provinces",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Provinces_Countries_CountryId",
                table: "Provinces",
                column: "CountryId",
                principalTable: "Countries",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
