using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AkordideApi.Migrations
{
    /// <inheritdoc />
    public partial class initt : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Taktid_Kolmkolad_KolmkolaId",
                table: "Taktid");

            migrationBuilder.DropForeignKey(
                name: "FK_Taktid_Kolmkolad_KolmkolaId1",
                table: "Taktid");

            migrationBuilder.DropForeignKey(
                name: "FK_Taktid_Lood_LuguId",
                table: "Taktid");

            migrationBuilder.DropIndex(
                name: "IX_Taktid_KolmkolaId1",
                table: "Taktid");

            migrationBuilder.DropColumn(
                name: "KolmkolaId1",
                table: "Taktid");

            migrationBuilder.AddForeignKey(
                name: "FK_Taktid_Kolmkolad_KolmkolaId",
                table: "Taktid",
                column: "KolmkolaId",
                principalTable: "Kolmkolad",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Taktid_Lood_LuguId",
                table: "Taktid",
                column: "LuguId",
                principalTable: "Lood",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Taktid_Kolmkolad_KolmkolaId",
                table: "Taktid");

            migrationBuilder.DropForeignKey(
                name: "FK_Taktid_Lood_LuguId",
                table: "Taktid");

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
                name: "FK_Taktid_Kolmkolad_KolmkolaId",
                table: "Taktid",
                column: "KolmkolaId",
                principalTable: "Kolmkolad",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Taktid_Kolmkolad_KolmkolaId1",
                table: "Taktid",
                column: "KolmkolaId1",
                principalTable: "Kolmkolad",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Taktid_Lood_LuguId",
                table: "Taktid",
                column: "LuguId",
                principalTable: "Lood",
                principalColumn: "Id");
        }
    }
}
