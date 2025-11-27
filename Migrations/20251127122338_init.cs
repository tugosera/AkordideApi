using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AkordideApi.Migrations
{
    /// <inheritdoc />
    public partial class init : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Kolmkolad",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Pohitoon = table.Column<int>(type: "int", nullable: false),
                    Tahis = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Tyypp = table.Column<string>(type: "nvarchar(8)", maxLength: 8, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Kolmkolad", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Lood",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nimetus = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Lood", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Taktid",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    KolmkolaId = table.Column<int>(type: "int", nullable: false),
                    LuguId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Taktid", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Taktid_Kolmkolad_KolmkolaId",
                        column: x => x.KolmkolaId,
                        principalTable: "Kolmkolad",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Taktid_Lood_LuguId",
                        column: x => x.LuguId,
                        principalTable: "Lood",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Taktid_KolmkolaId",
                table: "Taktid",
                column: "KolmkolaId");

            migrationBuilder.CreateIndex(
                name: "IX_Taktid_LuguId",
                table: "Taktid",
                column: "LuguId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Taktid");

            migrationBuilder.DropTable(
                name: "Kolmkolad");

            migrationBuilder.DropTable(
                name: "Lood");
        }
    }
}
