using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AkordideApi.Migrations
{
    /// <inheritdoc />
    public partial class init9 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "KolmkolaId1",
                table: "Taktid",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Taktid_KolmkolaId1",
                table: "Taktid",
                column: "KolmkolaId1");

            migrationBuilder.AddForeignKey(
                name: "FK_Taktid_Kolmkolad_KolmkolaId1",
                table: "Taktid",
                column: "KolmkolaId1",
                principalTable: "Kolmkolad",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Taktid_Kolmkolad_KolmkolaId1",
                table: "Taktid");

            migrationBuilder.DropIndex(
                name: "IX_Taktid_KolmkolaId1",
                table: "Taktid");

            migrationBuilder.DropColumn(
                name: "KolmkolaId1",
                table: "Taktid");
        }
    }
}
