using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FeedbacksManagementApi.Migrations
{
    /// <inheritdoc />
    public partial class ChangeNameProbabilityToSimilarity : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Porbablity",
                table: "Feedbacks",
                newName: "Similarity");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Similarity",
                table: "Feedbacks",
                newName: "Porbablity");
        }
    }
}
