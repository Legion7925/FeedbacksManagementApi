using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FeedbacksManagementApi.Migrations
{
    /// <inheritdoc />
    public partial class AddedCustomerToCaseTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "FkIdCustomer",
                table: "Cases",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Cases_FkIdCustomer",
                table: "Cases",
                column: "FkIdCustomer");

            migrationBuilder.AddForeignKey(
                name: "FK_Cases_Customers_FkIdCustomer",
                table: "Cases",
                column: "FkIdCustomer",
                principalTable: "Customers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Cases_Customers_FkIdCustomer",
                table: "Cases");

            migrationBuilder.DropIndex(
                name: "IX_Cases_FkIdCustomer",
                table: "Cases");

            migrationBuilder.DropColumn(
                name: "FkIdCustomer",
                table: "Cases");
        }
    }
}
