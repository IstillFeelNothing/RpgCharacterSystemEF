using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace RPG_Character_System.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Items",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Items", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Quests",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IsCompleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Quests", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Weapons",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false),
                    AttackPower = table.Column<int>(type: "int", nullable: false),
                    CharacterId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Weapons", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Weapons_Items_Id",
                        column: x => x.Id,
                        principalTable: "Items",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Characters",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Health = table.Column<int>(type: "int", nullable: false),
                    Level = table.Column<int>(type: "int", nullable: false),
                    EquippedWeaponId = table.Column<int>(type: "int", nullable: true),
                    Experience = table.Column<int>(type: "int", nullable: false),
                    ClassType = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Characters", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Characters_Weapons_EquippedWeaponId",
                        column: x => x.EquippedWeaponId,
                        principalTable: "Weapons",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.SetNull);
                });

            migrationBuilder.CreateTable(
                name: "CharacterQuests",
                columns: table => new
                {
                    CharactersId = table.Column<int>(type: "int", nullable: false),
                    QuestsId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CharacterQuests", x => new { x.CharactersId, x.QuestsId });
                    table.ForeignKey(
                        name: "FK_CharacterQuests_Characters_CharactersId",
                        column: x => x.CharactersId,
                        principalTable: "Characters",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CharacterQuests_Quests_QuestsId",
                        column: x => x.QuestsId,
                        principalTable: "Quests",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Mages",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false),
                    SpellDamage = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Mages", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Mages_Characters_Id",
                        column: x => x.Id,
                        principalTable: "Characters",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Warriors",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false),
                    BonusDamage = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Warriors", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Warriors_Characters_Id",
                        column: x => x.Id,
                        principalTable: "Characters",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Items",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { 1, "Steel Sword" },
                    { 2, "Arcane Staff" }
                });

            migrationBuilder.InsertData(
                table: "Quests",
                columns: new[] { "Id", "Description", "IsCompleted", "Title" },
                values: new object[,]
                {
                    { 1, "Slay the dragon in the northern mountains.", false, "Defeat the Dragon" },
                    { 2, "Recover the legendary blade from ancient ruins.", false, "Find the Lost Sword" }
                });

            migrationBuilder.InsertData(
                table: "Weapons",
                columns: new[] { "Id", "AttackPower", "CharacterId" },
                values: new object[,]
                {
                    { 1, 20, null },
                    { 2, 25, null }
                });

            migrationBuilder.InsertData(
                table: "Characters",
                columns: new[] { "Id", "ClassType", "EquippedWeaponId", "Experience", "Health", "Level", "Name" },
                values: new object[,]
                {
                    { 1, 0, 1, 300, 120, 5, "Thorin" },
                    { 2, 1, 2, 400, 80, 6, "Elora" }
                });

            migrationBuilder.InsertData(
                table: "Mages",
                columns: new[] { "Id", "SpellDamage" },
                values: new object[] { 2, 100 });

            migrationBuilder.InsertData(
                table: "Warriors",
                columns: new[] { "Id", "BonusDamage" },
                values: new object[] { 1, 110 });

            migrationBuilder.CreateIndex(
                name: "IX_CharacterQuests_QuestsId",
                table: "CharacterQuests",
                column: "QuestsId");

            migrationBuilder.CreateIndex(
                name: "IX_Characters_EquippedWeaponId",
                table: "Characters",
                column: "EquippedWeaponId",
                unique: true,
                filter: "[EquippedWeaponId] IS NOT NULL");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CharacterQuests");

            migrationBuilder.DropTable(
                name: "Mages");

            migrationBuilder.DropTable(
                name: "Warriors");

            migrationBuilder.DropTable(
                name: "Quests");

            migrationBuilder.DropTable(
                name: "Characters");

            migrationBuilder.DropTable(
                name: "Weapons");

            migrationBuilder.DropTable(
                name: "Items");
        }
    }
}
