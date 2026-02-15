using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebBattler.DAL.Migrations
{
    /// <inheritdoc />
    public partial class TurnBaseGame : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "BuildTurns",
                table: "UnitSamples",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "GameSessionId",
                table: "Countries",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "BuildTurns",
                table: "BuildingSamples",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Profit",
                table: "BuildingSamples",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "GameSessions",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    GuildId = table.Column<decimal>(type: "decimal(20,0)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CurrentTurn = table.Column<int>(type: "int", nullable: false),
                    CurrentYear = table.Column<int>(type: "int", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GameSessions", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ProductionOrders",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    OwnerId = table.Column<decimal>(type: "decimal(20,0)", nullable: false),
                    GameSessionId = table.Column<int>(type: "int", nullable: false),
                    OrderType = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Status = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Quantity = table.Column<int>(type: "int", nullable: false),
                    UnitSampleId = table.Column<int>(type: "int", nullable: true),
                    BuildingSampleId = table.Column<int>(type: "int", nullable: true),
                    ArmyId = table.Column<int>(type: "int", nullable: true),
                    CityId = table.Column<int>(type: "int", nullable: true),
                    StartTurn = table.Column<int>(type: "int", nullable: false),
                    ReadyTurn = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductionOrders", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ProductionOrders_Armies_ArmyId",
                        column: x => x.ArmyId,
                        principalTable: "Armies",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ProductionOrders_BuildingSamples_BuildingSampleId",
                        column: x => x.BuildingSampleId,
                        principalTable: "BuildingSamples",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ProductionOrders_Cities_CityId",
                        column: x => x.CityId,
                        principalTable: "Cities",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ProductionOrders_GameSessions_GameSessionId",
                        column: x => x.GameSessionId,
                        principalTable: "GameSessions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ProductionOrders_UnitSamples_UnitSampleId",
                        column: x => x.UnitSampleId,
                        principalTable: "UnitSamples",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Countries_GameSessionId",
                table: "Countries",
                column: "GameSessionId");

            migrationBuilder.CreateIndex(
                name: "IX_GameSessions_GuildId",
                table: "GameSessions",
                column: "GuildId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_ProductionOrders_ArmyId",
                table: "ProductionOrders",
                column: "ArmyId");

            migrationBuilder.CreateIndex(
                name: "IX_ProductionOrders_BuildingSampleId",
                table: "ProductionOrders",
                column: "BuildingSampleId");

            migrationBuilder.CreateIndex(
                name: "IX_ProductionOrders_CityId",
                table: "ProductionOrders",
                column: "CityId");

            migrationBuilder.CreateIndex(
                name: "IX_ProductionOrders_GameSessionId",
                table: "ProductionOrders",
                column: "GameSessionId");

            migrationBuilder.CreateIndex(
                name: "IX_ProductionOrders_UnitSampleId",
                table: "ProductionOrders",
                column: "UnitSampleId");

            migrationBuilder.AddForeignKey(
                name: "FK_Countries_GameSessions_GameSessionId",
                table: "Countries",
                column: "GameSessionId",
                principalTable: "GameSessions",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Countries_GameSessions_GameSessionId",
                table: "Countries");

            migrationBuilder.DropTable(
                name: "ProductionOrders");

            migrationBuilder.DropTable(
                name: "GameSessions");

            migrationBuilder.DropIndex(
                name: "IX_Countries_GameSessionId",
                table: "Countries");

            migrationBuilder.DropColumn(
                name: "BuildTurns",
                table: "UnitSamples");

            migrationBuilder.DropColumn(
                name: "GameSessionId",
                table: "Countries");

            migrationBuilder.DropColumn(
                name: "BuildTurns",
                table: "BuildingSamples");

            migrationBuilder.DropColumn(
                name: "Profit",
                table: "BuildingSamples");
        }
    }
}
