using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddProductIdToCaseBase : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "FkIdProduct",
                table: "Cases",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Cases_FkIdProduct",
                table: "Cases",
                column: "FkIdProduct");

            migrationBuilder.AddForeignKey(
                name: "FK_Cases_Products_FkIdProduct",
                table: "Cases",
                column: "FkIdProduct",
                principalTable: "Products",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Cases_Products_FkIdProduct",
                table: "Cases");

            migrationBuilder.DropIndex(
                name: "IX_Cases_FkIdProduct",
                table: "Cases");

            migrationBuilder.DropColumn(
                name: "FkIdProduct",
                table: "Cases");
        }
    }
}
