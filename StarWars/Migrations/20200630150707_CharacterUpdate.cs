using Microsoft.EntityFrameworkCore.Migrations;

namespace StarWars.Migrations
{
    public partial class CharacterUpdate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Character_Character_CharacterId",
                table: "Character");

            migrationBuilder.DropIndex(
                name: "IX_Character_CharacterId",
                table: "Character");

            migrationBuilder.DropColumn(
                name: "CharacterId",
                table: "Character");

            migrationBuilder.CreateTable(
                name: "Friend",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(nullable: true),
                    CharacterId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Friend", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Friend_Character_CharacterId",
                        column: x => x.CharacterId,
                        principalTable: "Character",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Friend_CharacterId",
                table: "Friend",
                column: "CharacterId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Friend");

            migrationBuilder.AddColumn<int>(
                name: "CharacterId",
                table: "Character",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Character_CharacterId",
                table: "Character",
                column: "CharacterId");

            migrationBuilder.AddForeignKey(
                name: "FK_Character_Character_CharacterId",
                table: "Character",
                column: "CharacterId",
                principalTable: "Character",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
