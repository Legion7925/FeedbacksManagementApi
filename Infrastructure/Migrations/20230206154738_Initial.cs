using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class Initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Customers",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NameAndFamily = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Phone = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Customers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Pages",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PageUrl = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PageName = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Pages", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Products",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Products", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Specialties",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Specialties", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Tags",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tags", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FamilyName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Username = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Password = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Updated = table.Column<DateTime>(type: "datetime2", nullable: false),
                    PasswordReset = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Cases",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Source = table.Column<byte>(type: "tinyint", nullable: false),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    SourceAddress = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Resources = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FkIdCustomer = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Cases", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Cases_Customers_FkIdCustomer",
                        column: x => x.FkIdCustomer,
                        principalTable: "Customers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Feedbacks",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ReferralDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    RespondDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: true),
                    State = table.Column<byte>(type: "tinyint", nullable: false),
                    SerialNumber = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: true),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Respond = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Source = table.Column<byte>(type: "tinyint", nullable: false),
                    SourceAddress = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FkIdCustomer = table.Column<int>(type: "int", nullable: false),
                    FkIdProduct = table.Column<int>(type: "int", nullable: false),
                    Resources = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Similarity = table.Column<byte>(type: "tinyint", nullable: true),
                    Priorty = table.Column<byte>(type: "tinyint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Feedbacks", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Feedbacks_Customers_FkIdCustomer",
                        column: x => x.FkIdCustomer,
                        principalTable: "Customers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Feedbacks_Products_FkIdProduct",
                        column: x => x.FkIdProduct,
                        principalTable: "Products",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Experts",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NameAndFamily = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    NationalCode = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    BirthDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    PassportNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Gender = table.Column<byte>(type: "tinyint", nullable: false),
                    Nationality = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Language = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Education = table.Column<byte>(type: "tinyint", nullable: false),
                    FieldOfStudy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Occupation = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    MobileNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Address = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: false),
                    FkIdExpertise = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Experts", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Experts_Specialties_FkIdExpertise",
                        column: x => x.FkIdExpertise,
                        principalTable: "Specialties",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PageUser",
                columns: table => new
                {
                    PagesId = table.Column<int>(type: "int", nullable: false),
                    UsersId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PageUser", x => new { x.PagesId, x.UsersId });
                    table.ForeignKey(
                        name: "FK_PageUser_Pages_PagesId",
                        column: x => x.PagesId,
                        principalTable: "Pages",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PageUser_Users_UsersId",
                        column: x => x.UsersId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "UserPages",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FkIdUser = table.Column<int>(type: "int", nullable: false),
                    FkIdPage = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserPages", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UserPages_Pages_FkIdPage",
                        column: x => x.FkIdPage,
                        principalTable: "Pages",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UserPages_Users_FkIdUser",
                        column: x => x.FkIdUser,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "FeedbackTag",
                columns: table => new
                {
                    FeedbacksId = table.Column<int>(type: "int", nullable: false),
                    TagsId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FeedbackTag", x => new { x.FeedbacksId, x.TagsId });
                    table.ForeignKey(
                        name: "FK_FeedbackTag_Feedbacks_FeedbacksId",
                        column: x => x.FeedbacksId,
                        principalTable: "Feedbacks",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_FeedbackTag_Tags_TagsId",
                        column: x => x.TagsId,
                        principalTable: "Tags",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ExpertFeedback",
                columns: table => new
                {
                    ExpertsId = table.Column<int>(type: "int", nullable: false),
                    FeedbacksId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ExpertFeedback", x => new { x.ExpertsId, x.FeedbacksId });
                    table.ForeignKey(
                        name: "FK_ExpertFeedback_Experts_ExpertsId",
                        column: x => x.ExpertsId,
                        principalTable: "Experts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ExpertFeedback_Feedbacks_FeedbacksId",
                        column: x => x.FeedbacksId,
                        principalTable: "Feedbacks",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ExpertFeedbacks",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FkIdFeedback = table.Column<int>(type: "int", nullable: false),
                    FkIdExpert = table.Column<int>(type: "int", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ExpertFeedbacks", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ExpertFeedbacks_Experts_FkIdExpert",
                        column: x => x.FkIdExpert,
                        principalTable: "Experts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ExpertFeedbacks_Feedbacks_FkIdFeedback",
                        column: x => x.FkIdFeedback,
                        principalTable: "Feedbacks",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Cases_FkIdCustomer",
                table: "Cases",
                column: "FkIdCustomer");

            migrationBuilder.CreateIndex(
                name: "IX_ExpertFeedback_FeedbacksId",
                table: "ExpertFeedback",
                column: "FeedbacksId");

            migrationBuilder.CreateIndex(
                name: "IX_ExpertFeedbacks_FkIdExpert",
                table: "ExpertFeedbacks",
                column: "FkIdExpert");

            migrationBuilder.CreateIndex(
                name: "IX_ExpertFeedbacks_FkIdFeedback",
                table: "ExpertFeedbacks",
                column: "FkIdFeedback");

            migrationBuilder.CreateIndex(
                name: "IX_Experts_FkIdExpertise",
                table: "Experts",
                column: "FkIdExpertise");

            migrationBuilder.CreateIndex(
                name: "IX_Feedbacks_FkIdCustomer",
                table: "Feedbacks",
                column: "FkIdCustomer");

            migrationBuilder.CreateIndex(
                name: "IX_Feedbacks_FkIdProduct",
                table: "Feedbacks",
                column: "FkIdProduct");

            migrationBuilder.CreateIndex(
                name: "IX_FeedbackTag_TagsId",
                table: "FeedbackTag",
                column: "TagsId");

            migrationBuilder.CreateIndex(
                name: "IX_PageUser_UsersId",
                table: "PageUser",
                column: "UsersId");

            migrationBuilder.CreateIndex(
                name: "IX_UserPages_FkIdPage",
                table: "UserPages",
                column: "FkIdPage");

            migrationBuilder.CreateIndex(
                name: "IX_UserPages_FkIdUser",
                table: "UserPages",
                column: "FkIdUser");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Cases");

            migrationBuilder.DropTable(
                name: "ExpertFeedback");

            migrationBuilder.DropTable(
                name: "ExpertFeedbacks");

            migrationBuilder.DropTable(
                name: "FeedbackTag");

            migrationBuilder.DropTable(
                name: "PageUser");

            migrationBuilder.DropTable(
                name: "UserPages");

            migrationBuilder.DropTable(
                name: "Experts");

            migrationBuilder.DropTable(
                name: "Feedbacks");

            migrationBuilder.DropTable(
                name: "Tags");

            migrationBuilder.DropTable(
                name: "Pages");

            migrationBuilder.DropTable(
                name: "Users");

            migrationBuilder.DropTable(
                name: "Specialties");

            migrationBuilder.DropTable(
                name: "Customers");

            migrationBuilder.DropTable(
                name: "Products");
        }
    }
}
