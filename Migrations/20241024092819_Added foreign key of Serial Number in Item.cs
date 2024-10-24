using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MyApp.Migrations
{
    /// <inheritdoc />
    public partial class AddedforeignkeyofSerialNumberinItem : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_SerialNumbers_ItemId",
                table: "SerialNumbers");

            migrationBuilder.CreateIndex(
                name: "IX_SerialNumbers_ItemId",
                table: "SerialNumbers",
                column: "ItemId");

            migrationBuilder.CreateIndex(
                name: "IX_Items_SerialNumberId",
                table: "Items",
                column: "SerialNumberId");

            migrationBuilder.AddForeignKey(
                name: "FK_Items_SerialNumbers_SerialNumberId",
                table: "Items",
                column: "SerialNumberId",
                principalTable: "SerialNumbers",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Items_SerialNumbers_SerialNumberId",
                table: "Items");

            migrationBuilder.DropIndex(
                name: "IX_SerialNumbers_ItemId",
                table: "SerialNumbers");

            migrationBuilder.DropIndex(
                name: "IX_Items_SerialNumberId",
                table: "Items");

            migrationBuilder.CreateIndex(
                name: "IX_SerialNumbers_ItemId",
                table: "SerialNumbers",
                column: "ItemId",
                unique: true,
                filter: "[ItemId] IS NOT NULL");
        }
    }
}
