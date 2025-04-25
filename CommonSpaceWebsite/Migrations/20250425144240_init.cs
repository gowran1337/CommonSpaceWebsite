using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CommonSpaceWebsite.Migrations
{
    /// <inheritdoc />
    public partial class init : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "CleaningTask",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CleaningTask", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "User",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Password = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_User", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "CleaningWeek",
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
                    table.PrimaryKey("PK_CleaningWeek", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CleaningWeek_User_CleanerId",
                        column: x => x.CleanerId,
                        principalTable: "User",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "CleaningWeekTask",
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
                    table.PrimaryKey("PK_CleaningWeekTask", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CleaningWeekTask_CleaningTask_TaskId",
                        column: x => x.TaskId,
                        principalTable: "CleaningTask",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_CleaningWeekTask_CleaningWeek_WeekId",
                        column: x => x.WeekId,
                        principalTable: "CleaningWeek",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ShoppingItem",
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
                    table.PrimaryKey("PK_ShoppingItem", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ShoppingItem_CleaningWeek_WeekId",
                        column: x => x.WeekId,
                        principalTable: "CleaningWeek",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ShoppingItem_User_PurchasedById",
                        column: x => x.PurchasedById,
                        principalTable: "User",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.SetNull);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CleaningWeek_CleanerId",
                table: "CleaningWeek",
                column: "CleanerId");

            migrationBuilder.CreateIndex(
                name: "IX_CleaningWeekTask_TaskId",
                table: "CleaningWeekTask",
                column: "TaskId");

            migrationBuilder.CreateIndex(
                name: "IX_CleaningWeekTask_WeekId",
                table: "CleaningWeekTask",
                column: "WeekId");

            migrationBuilder.CreateIndex(
                name: "IX_ShoppingItem_PurchasedById",
                table: "ShoppingItem",
                column: "PurchasedById");

            migrationBuilder.CreateIndex(
                name: "IX_ShoppingItem_WeekId",
                table: "ShoppingItem",
                column: "WeekId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CleaningWeekTask");

            migrationBuilder.DropTable(
                name: "ShoppingItem");

            migrationBuilder.DropTable(
                name: "CleaningTask");

            migrationBuilder.DropTable(
                name: "CleaningWeek");

            migrationBuilder.DropTable(
                name: "User");
        }
    }
}
