using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace P02_FootballBetting.Data.Migrations
{
    public partial class Final : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "PlayerId",
                table: "Bets",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "PlayerStatistic",
                columns: table => new
                {
                    GameId = table.Column<int>(type: "int", nullable: false),
                    PlayerId = table.Column<int>(type: "int", nullable: false),
                    ScoredGoals = table.Column<int>(type: "int", nullable: false),
                    Assists = table.Column<int>(type: "int", nullable: false),
                    MinutesPlayed = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PlayerStatistic", x => new { x.PlayerId, x.GameId });
                    table.ForeignKey(
                        name: "FK_PlayerStatistic_Games_GameId",
                        column: x => x.GameId,
                        principalTable: "Games",
                        principalColumn: "GameId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PlayerStatistic_Players_PlayerId",
                        column: x => x.PlayerId,
                        principalTable: "Players",
                        principalColumn: "PlayerId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Bets_PlayerId",
                table: "Bets",
                column: "PlayerId");

            migrationBuilder.CreateIndex(
                name: "IX_PlayerStatistic_GameId",
                table: "PlayerStatistic",
                column: "GameId");

            migrationBuilder.AddForeignKey(
                name: "FK_Bets_Players_PlayerId",
                table: "Bets",
                column: "PlayerId",
                principalTable: "Players",
                principalColumn: "PlayerId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Bets_Players_PlayerId",
                table: "Bets");

            migrationBuilder.DropTable(
                name: "PlayerStatistic");

            migrationBuilder.DropIndex(
                name: "IX_Bets_PlayerId",
                table: "Bets");

            migrationBuilder.DropColumn(
                name: "PlayerId",
                table: "Bets");
        }
    }
}
