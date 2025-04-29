using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CommonSpaceWebsite.Migrations
{
    /// <inheritdoc />
    public partial class _12331231 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ShoppingItems_CleaningWeeks_WeekId",
                table: "ShoppingItems");

            migrationBuilder.AlterColumn<int>(
                name: "WeekId",
                table: "ShoppingItems",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<bool>(
                name: "IsPurchased",
                table: "ShoppingItems",
                type: "bit",
                nullable: true,
                oldClrType: typeof(bool),
                oldType: "bit");

            migrationBuilder.AddForeignKey(
                name: "FK_ShoppingItems_CleaningWeeks_WeekId",
                table: "ShoppingItems",
                column: "WeekId",
                principalTable: "CleaningWeeks",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ShoppingItems_CleaningWeeks_WeekId",
                table: "ShoppingItems");

            migrationBuilder.AlterColumn<int>(
                name: "WeekId",
                table: "ShoppingItems",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<bool>(
                name: "IsPurchased",
                table: "ShoppingItems",
                type: "bit",
                nullable: false,
                defaultValue: false,
                oldClrType: typeof(bool),
                oldType: "bit",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_ShoppingItems_CleaningWeeks_WeekId",
                table: "ShoppingItems",
                column: "WeekId",
                principalTable: "CleaningWeeks",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
