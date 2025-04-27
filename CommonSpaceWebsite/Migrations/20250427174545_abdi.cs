using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CommonSpaceWebsite.Migrations
{
    /// <inheritdoc />
    public partial class abdi : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "CleaningTasks",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CleaningTasks", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Password = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "CleaningWeeks",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    StartDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    EndDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CleanerId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CleaningWeeks", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CleaningWeeks_Users_CleanerId",
                        column: x => x.CleanerId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "CleaningWeekTasks",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    WeekId = table.Column<int>(type: "int", nullable: false),
                    TaskId = table.Column<int>(type: "int", nullable: false),
                    IsCompleted = table.Column<bool>(type: "bit", nullable: false),
                    CompletedDate = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CleaningWeekTasks", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CleaningWeekTasks_CleaningTasks_TaskId",
                        column: x => x.TaskId,
                        principalTable: "CleaningTasks",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_CleaningWeekTasks_CleaningWeeks_WeekId",
                        column: x => x.WeekId,
                        principalTable: "CleaningWeeks",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ShoppingItems",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IsPurchased = table.Column<bool>(type: "bit", nullable: false),
                    PurchasedById = table.Column<int>(type: "int", nullable: true),
                    WeekId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ShoppingItems", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ShoppingItems_CleaningWeeks_WeekId",
                        column: x => x.WeekId,
                        principalTable: "CleaningWeeks",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ShoppingItems_Users_PurchasedById",
                        column: x => x.PurchasedById,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.SetNull);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CleaningWeeks_CleanerId",
                table: "CleaningWeeks",
                column: "CleanerId");

            migrationBuilder.CreateIndex(
                name: "IX_CleaningWeekTasks_TaskId",
                table: "CleaningWeekTasks",
                column: "TaskId");

            migrationBuilder.CreateIndex(
                name: "IX_CleaningWeekTasks_WeekId",
                table: "CleaningWeekTasks",
                column: "WeekId");

            migrationBuilder.CreateIndex(
                name: "IX_ShoppingItems_PurchasedById",
                table: "ShoppingItems",
                column: "PurchasedById");

            migrationBuilder.CreateIndex(
                name: "IX_ShoppingItems_WeekId",
                table: "ShoppingItems",
                column: "WeekId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CleaningWeekTasks");

            migrationBuilder.DropTable(
                name: "ShoppingItems");

            migrationBuilder.DropTable(
                name: "CleaningTasks");

            migrationBuilder.DropTable(
                name: "CleaningWeeks");

            migrationBuilder.DropTable(
                name: "Users");
        }
    }
}
